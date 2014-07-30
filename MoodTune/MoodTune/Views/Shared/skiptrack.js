

@Scripts.Render("~/scripts/SoundPlayer.js")


function skipAnalyse(nametrack)
{
	nametrack = "SONGDATA_" + nametrack;
	if (localStorage.getItem(nametrack)) {
		localStorage.setItem(nametrack,(Number(localStorage.getItem(nametrack)) + 1));
	} else {
		localStorage.setItem(nametrack,1);
	}
	return localStorage.getItem(nametrack);
}