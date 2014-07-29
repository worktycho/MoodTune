
Option Strict On

Imports Newtonsoft.Json

Namespace MoodTune
    Public Class MoodsController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Moods

        Async Function GetList() As Threading.Tasks.Task(Of ActionResult)
            Dim songs = Await LastFMTagFetcher.GetSongs()
            Dim songsJSON = JsonConvert.SerializeObject(songs)

            Dim result As New ContentResult
            result.Content = songsJSON
            Return result
        End Function

    End Class
End Namespace