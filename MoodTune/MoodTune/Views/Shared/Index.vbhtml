@Code
    ViewData("Title") = "Mood Tune"
End Code

<script type="text/javascript">
    var selected = { Happy: false, sad: false, Dramatic: false, Inspirational: false, Melancholic: false, Angry: false, Calm: false, exited: false, nervous: false };
    var audio;

    function tagSelect(type) {
        selected[type] = !selected[type];
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
    }
    function updatePlayAudio() {
        console.log("UnImplemented")
    }
    function localData(key, value) {
        localStorage.setItem(key, value);
    }

    function getRandomMood() {
        var onlyselected = [];
        for (key in selected) {
            if (selected[key]) onlyselected.push(key);
        }
        return onlyselected[Math.random() * onlyselected.length];
    }

    function skipTrack() {
        var widget = SC.Widget("sc-widget")
        widget.getCurrentSound(function (sound) { skipAnalyse(sound.title) })
        getNextTrack(getRandomMood(), function (tracks) { widget.load(GetSoundCloudURL(tracks[0])) });
    }

</script>

<!--<h1>Welcome to the Mood Tune Website!</h1>-->
<div class="tags">
    <article class="tag" onclick="playAudio('Happy');tagSelect('Happy');"><img src="/icons/happy.gif"><span id="Happy">Happy</span></article>
    <article class="tag" onclick="playAudio('sad');tagSelect('sad');"><img src="/icons/sad.gif"><span id="sad">Sad</span></article>
    <article class="tag" onclick="playAudio('Dramatic');tagSelect('Dramatic');"><img src="/icons/dramatic.gif"><span id="Dramatic">Dramatic</span></article>

    <article class="tag" onclick="playAudio('Inspirational');tagSelect('Inspirational');"><img src="/icons/inspirational.gif"><span id="Inspirational">Inspirational</span></article>
    <article class="tag" onclick="playAudio('Melancholic');tagSelect('Melancholic');"><img src="/icons/melancholic.gif"><span id="Melancholic">Melancholic</span></article>
    <article class="tag" onclick="playAudio('Angry');tagSelect('Angry');"><img src="/icons/angry.gif"><span id="Angry">Angry</span></article>

    <article class="tag" onclick="playAudio('Calm');tagSelect('Calm');"><img src="/icons/calm.gif"><span id="Calm">Calm</span></article>
    <article class="tag" onclick="playAudio('exited');tagSelect('exited');"><img src="/icons/exited.gif"><span id="exited">Exited</span></article>
    <article class="tag" onclick="playAudio('nervous');tagSelect('nervous');"><img src="/icons/nervous.gif"><span id="nervous">Nervous</span></article>
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