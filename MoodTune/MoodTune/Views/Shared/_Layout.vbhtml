<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>@ViewData("Title")</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("https://w.soundcloud.com/player/api.js")
    @Scripts.Render("~/scripts/SoundPlayer.js")
    @Scripts.Render("~/scripts/analysis.js")
    <link href='http://fonts.googleapis.com/css?family=Lobster' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Francois+One' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Oxygen' rel='stylesheet' type='text/css'>
</head>
<body>
    <img src="@(Url.Content("~/Content/logolarge.png"))" style="width:199px;height:266px;position:absolute;right:10%;top:40%">
    @RenderBody()
    @Scripts.Render("~/bundles/jquery")
    @RenderSection("scripts", required:=False)
</body>
</html>
