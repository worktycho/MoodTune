
Option Strict On

Imports Newtonsoft.Json

Namespace MoodTune
    Public Class MoodsController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Moods

        Async Function GetList() As Threading.Tasks.Task(Of ActionResult)

            Dim moodname As String = CStr(RouteData.Values("MoodName"))

            Dim origsongs = Await LastFMTagFetcher.GetSongs(moodname)
            Dim awaitarray As New List(Of Threading.Tasks.Task(Of String))

            For i = 0 To origsongs.Count - 1
                awaitarray.Add(SoundCloudMapper.GetEmbedId(origsongs(i).Name))
            Next

            For i = 0 To origsongs.Count - 1
                origsongs(i).Link = Await awaitarray(i)
            Next
            Dim songsJSON = JsonConvert.SerializeObject(origsongs)

            Dim result As New ContentResult
            result.Content = songsJSON
            Return result
        End Function

    End Class
End Namespace