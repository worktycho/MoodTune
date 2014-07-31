
Option Strict On

Imports Newtonsoft.Json
Imports System.Threading.Tasks

Namespace MoodTune
    Public Class MoodsController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Moods

        Async Function GetList() As Threading.Tasks.Task(Of ActionResult)

            Dim moodname As String = CStr(RouteData.Values("MoodName"))

            Dim cache = HttpRuntime.Cache

            Dim random As New Random
            Dim songs = LastFMTagFetcher.GetSongs(moodname)

            Dim SkipInfo As Dictionary(Of String, Integer) = DirectCast(Session("song_perfs"), Dictionary(Of String, Integer))
            If SkipInfo Is Nothing Then SkipInfo = New Dictionary(Of String, Integer)
            Dim chosenSongs As IEnumerable(Of Song)
            Dim SongId As Integer = random.Next(50)
            For Each SongTask In songs
                Dim Song = Await SongTask
                If (SkipInfo.ContainsKey(Song.Name) AndAlso SkipInfo(Song.Name) > 5) Then Continue For
                If SongId = 0 Then
                    Dim templist = New List(Of Song)
                    templist.Add(Song)
                    chosenSongs = templist
                    Exit For
                End If
                SongId -= 1
            Next

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