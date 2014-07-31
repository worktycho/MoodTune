Option Strict On
Option Infer On

Imports System.Threading.Tasks

Public Class LastFMTagFetcher

    Private Shared APIKey As String = "2f56d6db848d93c852aa61d070fe1a9b"

    Public Class FetcherException
        Inherits Exception

        Sub New(msg As String, ex As Net.WebException)
            MyBase.New(msg, ex)
        End Sub

    End Class

    Public Shared Async Function FetchSongs(MoodName As String, pagenum As Integer) As System.Threading.Tasks.Task(Of List(Of Song))
        Dim songsfetcher As New Net.Http.HttpClient()
        Dim response As Net.Http.HttpResponseMessage
        Try
            response = Await songsfetcher.GetAsync(String.Format("http://ws.audioscrobbler.com/2.0/?method=tag.gettoptracks&tag={0}&api_key={1}&page={2}", MoodName, APIKey, pagenum))
        Catch ex As Net.WebException
            Throw New FetcherException("Failed to access API", ex)
        End Try

        Dim responsetext = Await response.Content.ReadAsStringAsync()

        Dim Xml = XDocument.Parse(responsetext)

        Dim tracks As IEnumerable(Of XElement) = Xml.<lfm>.<toptracks>.<track>

        Dim songs As New List(Of Song)
        For Each item In tracks
            Dim SongName As String = item.<name>.First().Value
            Dim SongArtistName As String = item.<artist>.<name>.First().Value
            songs.Add(New Song With {.Name = SongName, .Artist = SongArtistName})
        Next
        Return songs
    End Function

    Public Shared Iterator Function GetSongs(MoodName As String) As IEnumerable(Of Threading.Tasks.Task(Of Song))
        Dim cache = HttpRuntime.Cache
        Dim pagenum = 0
        While True
            Dim cachename = "SongQueries." & MoodName & "." & pagenum
            Dim page = DirectCast(cache(cachename), List(Of Song))
            If page Is Nothing Then
                Debug.WriteLine("Cache Miss, name is: " & cachename)
                Dim Fetchtask = FetchSongs(MoodName, pagenum)
                Dim FetchedSongs As List(Of Song)
                Dim assignTask = Fetchtask.ContinueWith(Sub(finishedtask As Task(Of List(Of Song)))
                                                            FetchedSongs = finishedtask.Result
                                                            cache(cachename) = finishedtask.Result
                                                            Debug.WriteLine("Cache store, name is: " & cachename)
                                                        End Sub)
                Dim prevtask = assignTask
                For i As Integer = 0 To 49
                    Dim i2 = i
                    Dim currenttask = prevtask.ContinueWith(Of Song)(Function() As Song
                                                                         Return FetchedSongs(i2)
                                                                     End Function)
                    Yield currenttask
                    prevtask = currenttask
                Next
            Else
                Debug.WriteLine("Cache Hit on: " & cachename)
            End If
            For Each Song In page
                Dim task As New Threading.Tasks.TaskCompletionSource(Of Song)
                task.SetResult(Song)
                Yield task.Task
            Next
            pagenum += 1
        End While

    End Function
End Class
