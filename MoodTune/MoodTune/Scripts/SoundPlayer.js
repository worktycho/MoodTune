function playAudio(mood) {
    getSongsByMood(mood, function (tracks) {
        var frame = document.createElement("iframe");
        frame.id = "sc-widget";
        frame.src = "https://w.soundcloud.com/player/?url=" + GetSoundCloudURL(tracks[0]);
        $('body')[0].appendChild(frame);
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