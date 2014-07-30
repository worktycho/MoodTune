Namespace MoodTune
    Public Class HomeController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /

        Function Index() As ActionResult
            Return View()
        End Function

        ' Post: /SetLearnedPrefs

        <HttpPost()>
        Function SetLearnedPrefs(prefs As Dictionary(Of String, Integer)) As ActionResult
            Session("song_perfs") = prefs
            Dim res = New ContentResult()
            Return res
        End Function

    End Class
End Namespace