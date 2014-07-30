
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
            Dim cachedsongs As List(Of Song) = DirectCast(cache("Moods." & moodname), List(Of Song))

            Dim random As New Random

            If cachedsongs Is Nothing Then
                cachedsongs = Await LastFMTagFetcher.GetSongs(moodname)
                cache("Moods." & moodname) = cachedsongs
            End If

            Dim SkipInfo As Dictionary(Of String, Integer) = DirectCast(Session("song_perfs"), Dictionary(Of String, Integer))
            If SkipInfo Is Nothing Then SkipInfo = New Dictionary(Of String, Integer)
            Dim chosenSongs As IEnumerable(Of Song)

            chosenSongs = From song In cachedsongs
                          Where Not (SkipInfo.ContainsKey(song.Name) AndAlso SkipInfo(song.Name) > 5)
                          Skip random.Next(50) Take 1

            For Each Song In chosenSongs
                Song.Link = Await SoundCloudMapper.GetEmbedId(Song.Name)
            Next

            Dim chosenSongsJSON As String = JsonConvert.SerializeObject(chosenSongs)
            Dim result As New ContentResult
            result.Content = chosenSongsJSON
            Return result
        End Function



    End Class
End Namespace