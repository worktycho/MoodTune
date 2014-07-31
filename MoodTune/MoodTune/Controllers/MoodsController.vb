
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

            Dim SkipInfo = DirectCast(Session("song_perfs"), Dictionary(Of String, SongInfo))
            If SkipInfo Is Nothing Then SkipInfo = New Dictionary(Of String, SongInfo)
            Dim chosenSongs As IEnumerable(Of Song)
            Dim rand = random.Next(10)
            Dim templist As List(Of Song)
            For Each SongTask In songs
                Dim Song = Await SongTask
                Dim SongTitle = SoundCloudMapper.TryGetSoundCloudTitle(Song.Name)
                If (SongTitle IsNot Nothing AndAlso SkipInfo.ContainsKey("SONGDATA_" & SongTitle)) Then
                    Dim songInfo = SkipInfo("SONGDATA_" & SongTitle)
                    If songInfo.ListenedRecently > 1 Then Continue For
                    If songInfo.played = 0 Then
                        Dim chance = random.Next(1 << songInfo.skiped)
                        If chance <> 0 Then Continue For
                    Else
                        Dim chance = random.NextDouble()
                        If chance < songInfo.played / (songInfo.played + songInfo.skiped) Then
                            templist = New List(Of Song)
                            templist.Add(Song)
                            chosenSongs = templist
                            Exit For
                        End If
                    End If
                End If
                If rand <> 0 Then
                    rand -= 1
                    Continue For
                End If

                templist = New List(Of Song)
                templist.Add(Song)
                chosenSongs = templist
                Exit For
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