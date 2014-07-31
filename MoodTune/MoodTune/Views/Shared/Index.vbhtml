﻿@Code
    ViewData("Title") = "Mood Tune"
End Code

<script type="text/javascript">
    var selected = { Happy: false, sad: false, Dramatic: false, Inspirational: false, Melancholic: false, Angry: false, Calm: false, exited: false, nervous: false };
    var audio;

    function tagSelect(type) {
        selected[type] = !selected[type];
        if (!IsPlaying()) StartPlaying();
        console.log("Type '" + type + "' selected! State active = " + selected[type]);
        updateSel(type);
    }
    function updateSel(type) {
        //If no type is specified then update all
        if (type == null) { for (var typename in selected) updateSel(typename); return; }
        var sel = document.getElementById(type);
        if (selected[type]) sel = sel.className += " selected";
        else sel.className = sel.className.replace(/(?:^|\s)selected(?!\S)/g, '');
    }
    function randomise() {
        var type;
        for (type in selected) selected[type] = [true, false][Math.round(Math.random())];
        updateSel(null);
        if (!IsPlaying) StartPlaying();
    }

    var playing = false;
    function StartPlaying() {
        if (playing) console.log("Already started playing");
        playing = true;
        playAudio(
            getRandomMood(), 
            function () { 
                getNextTrack(
                    getRandomMood(),
                    function (tracks) {
                        widget.load(GetSoundCloudURL(tracks[0]), { auto_play: true })
                    });
            });
    }

    function IsPlaying() {
        return playing;
    }

    function getRandomMood() {
        var onlyselected = [];
        for (key in selected) {
            if (selected[key]) onlyselected.push(key);
        }
        var MoodId = Math.round(Math.random() * (onlyselected.length - 1));
        return onlyselected[MoodId];
    }

    function skipTrack() {
        var widget = SC.Widget("sc-widget")
        widget.getCurrentSound(function (sound) { skipAnalyse(sound.title) })
        getNextTrack(getRandomMood(), function (tracks) { widget.load(GetSoundCloudURL(tracks[0]), { auto_play: true }) });
    }

    @code
        If Session("Song_prefs") Is Nothing Then
    End Code
    (function () {
        setTimeout(function () {
            prefs = {}
            // TODO: Remove this loop
            for (var i = 0; i < localStorage.length; i++) {
                var key = localStorage.key(i);
                if (key.indexOf("SONGDATA_") == 0) {
                    prefs[key] = localStorage.getItem(key);
                }
            }
            $.ajax({
                url: '@(Url.Action("SetLearnedPrefs"))',
                type: 'POST',
                data: JSON.stringify({ prefs: prefs }),
                contentType: 'application/json; charset=utf-8',
                success: function () { console.log("successfully posted back prefs") }
            });
        }, 1000);
        
    })();
    @code
        End if
    End Code

</script>

<!--<h1>Welcome to the Mood Tune Website!</h1>-->
<div class="tags">
    <article class="tag" onclick="tagSelect('Happy');"><span id="Happy">Happy</span></article>
    <article class="tag" onclick="tagSelect('sad');"><span id="sad">Sad</span></article>
    <article class="tag" onclick="tagSelect('Dramatic');"><span id="Dramatic">Dramatic</span></article>

    <article class="tag" onclick="tagSelect('Inspirational');"><span id="Inspirational">Inspirational</span></article>
    <article class="tag" onclick="tagSelect('Melancholic');"><span id="Melancholic">Melancholic</span></article>
    <article class="tag" onclick="tagSelect('Angry');"><span id="Angry">Angry</span></article>

    <article class="tag" onclick="tagSelect('Calm');"><span id="Calm">Calm</span></article>
    <article class="tag" onclick="tagSelect('exited');"><span id="exited">Exited</span></article>
    <article class="tag" onclick="tagSelect('nervous');"><span id="nervous">Nervous</span></article>
</div>
<input type="button" class="rndbtn" onclick="randomise()" value="Randomise">
<input type="button" class="rndbtn" onclick="skipTrack()" value="SkipTrack"><br>
<!--<audio controls>
	 <source src="http://media.w3.org/2010/07/bunny/04-Death_Becomes_Fur.mp4"
	         type='audio/mp4'>
	 <source src="http://media.w3.org/2010/07/bunny/04-Death_Becomes_Fur.oga"
	         type='audio/ogg; codecs=vorbis'>
	 <p>Your user agent does not support the HTML5 Audio element.</p>
	</audio>
	-->