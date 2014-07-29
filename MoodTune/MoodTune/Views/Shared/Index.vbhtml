@Code
    ViewData("Title") = "Mood Tune"
End Code

<script type="text/javascript">
	function playAudio(type)
	{
		var audio = new Audio("test.mp3");
		audio.play();
	}

	function getSongsByMood(name, callback)
	{
	    console.log(name)
	    var ajax = $.getJSON( "/Moods/" + name, {}, callback);
	}

	function playSongsByMood(name)
	{
	    getSongsByMood(name, function (data) {
            console.log("callback")
	        playSoundCloudAudio(data[0]);
	    });
	}

	function playSoundCloudAudio(obj) {
        console.log(obj)
	}

</script>

<h1>Welcome to the Mood Tune Website!</h1>
<div class="tags">
	<article id="happy" class="tag">Happy</article>
	<article class="tag" onclick="playSongsByMood('sad');">Sad</article>
	<article class="tag">b</article>

	<article class="tag">c</article>
	<article class="tag">d</article>
	<article class="tag">e</article>

	<article class="tag">f</article>
	<article class="tag">g</article>
	<article class="tag">h</article>		
</div>