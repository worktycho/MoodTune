﻿function playAudio(mood, onFinish) {
    getSongsByMood(mood, function (tracks) {
        var frame = document.createElement("iframe");
        frame.id = "sc-widget";
        $('#mainwrapper')[0].appendChild(frame);
        frame.src = "https://w.soundcloud.com/player/?auto_play=true&url=" + GetSoundCloudURL(tracks[0]);
        widget       = SC.Widget(frame);
        widget.bind(SC.Widget.Events.READY, function() {
            widget.bind(SC.Widget.Events.FINISH, function () {
                console.log("FINISH");
                onFinish();
        });});});
}

function GetSoundCloudURL(track) {
    return "http://api.soundcloud.com/tracks/" + track.Link;
}


function getSongsByMood(name, callback) {
    console.log(name)
    $.ajax("/Moods/" + name, {
        cache: false,
        success: function (resp) { var data = JSON.parse(resp); callback(data) },
        error: function (_, msg) {
            console.log("error " & msg);
            var element = document.createElement('H2');
            element.innerHTML = "Sorry, Something went wrong. Please try reloading the page or report it to one of team";
            $('#mainwrapper')[0].appendChild(element);
        }
    });
}

function playSongsByMood(name) {
    getSongsByMood(name, function (data) {
        console.log("callback")
        playSoundCloudAudio(data[0]);
    });
}



function getNextTrack(mood, callback) {
    return getSongsByMood(mood, callback);
}
