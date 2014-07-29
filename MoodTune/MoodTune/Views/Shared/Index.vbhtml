@Code
    ViewData("Title") = "Mood Tune"
End Code

<script type="text/javascript">
	function playAudio(type)
	{
		var audio = new Audio("test.mp3");
		audio.play();
	}
</script>

<h1>Welcome to the Mood Tune Website!</h1>
<div class="tags">
	<article id="happy" class="tag">Happy</article>
	<article class="tag" onclick="playaudio('sad');">Sad</article>
	<article class="tag">b</article>

	<article class="tag">c</article>
	<article class="tag">d</article>
	<article class="tag">e</article>

	<article class="tag">f</article>
	<article class="tag">g</article>
	<article class="tag">h</article>		
</div>