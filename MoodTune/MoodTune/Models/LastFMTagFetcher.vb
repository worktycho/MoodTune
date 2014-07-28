Public Class LastFMTagFetcher
    Public Shared Async Function GetSongs() As Threading.Tasks.Task(Of String)
        Dim songsfetcher As New Net.Http.HttpClient()
        Dim response = Await songsfetcher.GetAsync("http://ws.audioscrobbler.com/2.0/?method=tag.search&tag=disco")
        Dim responsetext As String = ""
        responsetext = Await response.Content.ReadAsStringAsync()
        Return responsetext
    End Function
End Class
