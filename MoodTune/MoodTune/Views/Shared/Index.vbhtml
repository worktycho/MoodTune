@Code
    ViewData("Title") = "Mood Tune"
End Code

<script type="text/javascript">
    var selected = { hap: false, sad: false, dra: false, ins: false, mel: false, ang: false, cal: false, exi: false, ner: false };
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

</script>

<!--<h1>Welcome to the Mood Tune Website!</h1>-->
<div class="tags">
    <article class="tag" onclick="playAudio('hap');tagSelect('hap');"><span id="hap">Happy</span></article>
    <article class="tag" onclick="playAudio('sad');tagSelect('sad');"><span id="sad">Sad</span></article>
    <article class="tag" onclick="playAudio('dra');tagSelect('dra');"><span id="dra">Dramatic</span></article>

    <article class="tag" onclick="playAudio('ins');tagSelect('ins');"><span id="ins">Inspirational</span></article>
    <article class="tag" onclick="playAudio('mel');tagSelect('mel');"><span id="mel">Melancholic</span></article>
    <article class="tag" onclick="playAudio('ang');tagSelect('ang');"><span id="ang">Angry</span></article>

    <article class="tag" onclick="playAudio('cal');tagSelect('cal');"><span id="cal">Calm</span></article>
    <article class="tag" onclick="playAudio('exi');tagSelect('exi');"><span id="exi">Exited</span></article>
    <article class="tag" onclick="playAudio('ner');tagSelect('ner');"><span id="ner">Nervous</span></article>
</div>
<input type="button" class="rndbtn" onclick="randomise()" value="Randomise"><br>
<!--<audio controls>
	 <source src="http://media.w3.org/2010/07/bunny/04-Death_Becomes_Fur.mp4"
	         type='audio/mp4'>
	 <source src="http://media.w3.org/2010/07/bunny/04-Death_Becomes_Fur.oga"
	         type='audio/ogg; codecs=vorbis'>
	 <p>Your user agent does not support the HTML5 Audio element.</p>
	</audio>
	-->
