function playAudio(mood) {
    getSongsByMood(mood, function (tracks) {
        console.log("1");
        var frame = document.createElement("iframe");
        frame.id = "sc-widget";
        $('body')[0].appendChild(frame);
        frame.src = "https://w.soundcloud.com/player/?auto_play=true&url=" + GetSoundCloudURL(tracks[0]);
    });
}

function GetSoundCloudURL(track) {
    return "http://api.soundcloud.com/tracks/" + track.Link;
}


function getSongsByMood(name, callback) {
    console.log(name)
    var ajax = $.getJSON("/Moods/" + name, {}, callback);
}

function playSongsByMood(name) {
    getSongsByMood(name, function (data) {
        console.log("callback")
        playSoundCloudAudio(data[0]);
    });
}

function playSoundCloudAudio(obj) {
    console.log(obj)
}