
Option Strict On

Imports Newtonsoft.Json

Namespace MoodTune
    Public Class MoodsController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Moods

        Async Function GetList() As Threading.Tasks.Task(Of ActionResult)

            Dim moodname As String = CStr(RouteData.Values("MoodName"))

            Dim cache = HttpRuntime.Cache
            Dim cachedmood As String = CStr(cache("Moods." & moodname))
            If cachedmood Is Nothing Then
                Dim origsongs = (Await LastFMTagFetcher.GetSongs(moodname)).Take(10).ToList()
                Dim awaitarray As New List(Of Threading.Tasks.Task(Of String))

                For i = 0 To origsongs.Count - 1
                    awaitarray.Add(SoundCloudMapper.GetEmbedId(origsongs(i).Name))
                Next

                For i = 0 To origsongs.Count - 1
                    origsongs(i).Link = Await awaitarray(i)
                Next
                Dim songsJSON = JsonConvert.SerializeObject(origsongs)
                cachedmood = songsJSON
                cache("Moods." & moodname) = cachedmood
            End If
            Dim result As New ContentResult
            result.Content = cachedmood
            Return result
        End Function

    End Class
End Namespace