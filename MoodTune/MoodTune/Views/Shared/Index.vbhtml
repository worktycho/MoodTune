@Code
    ViewData("Title") = "Mood Tune"
End Code

<script type="text/javascript">
    var selected = { Happy: false, sad: false, Dramatic: false, Inspirational: false, Melancholic: false, Angry: false, Calm: false, Excited: false, nervous: false };
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
        if (!IsPlaying()) StartPlaying();
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
                var widget = SC.Widget("sc-widget")
                widget.getCurrentSound(function (sound) {
                    console.log("finished listening to " &sound);
                    ListenedAnalyse(sound.title)
                });
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

    (function () {
        setTimeout(function () {
            @code
        If Session("Song_prefs") Is Nothing Then
    End Code
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

    @code
        End if
    End Code
            UpdateCallback = function () {
               
            }
        }, 1000);
        
    })();

</script>

<!--<h1>Welcome to the Mood Tune Website!</h1>-->
<img src="@(Url.Content("~/Content/assets/logolarge.png"))">
<ul class="instructions">
    <li>1. Choose 1 or more moods.</li>
    <li>2. Wait a few seconds for the song to load.</li>
    <li>================</li>
    <li>Enjoy listening to a variety of music from the selected catagory!</li>
</ul>
<div class="tags">
    <article class="tag" onclick="tagSelect('Happy');"><img src="@(Url.Content("~/Content/icons/happy.gif"))"><span id="Happy">Happy</span></article>
    <article class="tag" onclick="tagSelect('sad');"><img src="@(Url.Content("~/Content/icons/sad.gif"))"><span id="sad">Sad</span></article>
    <article class="tag" onclick="tagSelect('Dramatic');"><img src="@(Url.Content("~/Content/icons/dramatic.gif"))"><span id="Dramatic">Dramatic</span></article>

    <article class="tag" onclick="tagSelect('Inspirational');"><img src="@(Url.Content("~/Content/icons/inspirational.gif"))"><span id="Inspirational">Inspirational</span></article>
    <article class="tag" onclick="tagSelect('Melancholic');"><img src="@(Url.Content("~/Content/icons/melancholic.gif"))"><span id="Melancholic">Melancholic</span></article>
    <article class="tag" onclick="tagSelect('Angry');"><img src="@(Url.Content("~/Content/icons/angry.gif"))"><span id="Angry">Angry</span></article>

    <article class="tag" onclick="tagSelect('Calm');"><img src="@(Url.Content("~/Content/icons/calm.gif"))"><span id="Calm">Calm</span></article>
    <article class="tag" onclick="tagSelect('excited');"><img src="@(Url.Content("~/Content/icons/excited.gif"))"><span id="excited">Excited</span></article>
    <article class="tag" onclick="tagSelect('nervous');"><img src="@(Url.Content("~/Content/icons/nervous.gif"))"><span id="nervous">Nervous</span></article>

</div>
<input type="button" class="rndbtn" onclick="randomise()" value="Randomise">
<input type="button" class="rndbtn" onclick="skipTrack()" value="SkipTrack" ><br>
<!--<audio controls>
	 <source src="http://media.w3.org/2010/07/bunny/04-Death_Becomes_Fur.mp4"
	         type='audio/mp4'>
	 <source src="http://media.w3.org/2010/07/bunny/04-Death_Becomes_Fur.oga"
	         type='audio/ogg; codecs=vorbis'>
	 <p>Your user agent does not support the HTML5 Audio element.</p>
	</audio>
	-->