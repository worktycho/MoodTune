Option Strict On
Option Infer On

Public Class LastFMTagFetcher

    Private Shared APIKey As String = "2f56d6db848d93c852aa61d070fe1a9b"

    Public Shared Async Function GetSongs(MoodName As String) As Threading.Tasks.Task(Of List(Of Song))
        Dim songsfetcher As New Net.Http.HttpClient()
        Dim response = Await songsfetcher.GetAsync("http://ws.audioscrobbler.com/2.0/?method=tag.gettoptracks&tag=" & MoodName & "&api_key=" & APIKey)
        Dim responsetext = Await response.Content.ReadAsStringAsync()

        Dim Xml = XDocument.Parse(responsetext)

        Dim tracks As IEnumerable(Of XElement) = Xml.<lfm>.<toptracks>.<track>

        Dim names = From track In tracks Select track.<name>

        Dim songs As New List(Of Song)
        For Each item In names
            Dim SongName As String = item.First().Value
            songs.Add(New Song With {.Name = SongName})
        Next

        Return songs
    End Function
End Class
