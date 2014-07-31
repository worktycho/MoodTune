Option Strict On
Option Infer On

Public Class LastFMTagFetcher

    Private Shared APIKey As String = "2f56d6db848d93c852aa61d070fe1a9b"

    Public Class FetcherException
        Inherits Exception

        Sub New(msg As String, ex As Net.WebException)
            MyBase.New(msg, ex)
        End Sub

    End Class

    Public Shared Async Function GetSongs(MoodName As String) As Threading.Tasks.Task(Of List(Of Song))
        Dim songsfetcher As New Net.Http.HttpClient()
        Dim response As Net.Http.HttpResponseMessage
        Try
            response = Await songsfetcher.GetAsync("http://ws.audioscrobbler.com/2.0/?method=tag.gettoptracks&tag=" & MoodName & "&api_key=" & APIKey)
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
End Class
