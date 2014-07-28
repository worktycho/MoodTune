Option Explicit On
Option Infer On

Public Class LastFMTagFetcher

    Private Shared APIKey As String = "2f56d6db848d93c852aa61d070fe1a9b"

    Public Shared Async Function GetSongs() As Threading.Tasks.Task(Of String)
        Dim songsfetcher As New Net.Http.HttpClient()
        Dim response = Await songsfetcher.GetAsync("http://ws.audioscrobbler.com/2.0/?method=tag.gettoptracks&tag=disco&api_key=" & APIKey)
        Dim responsetext = Await response.Content.ReadAsStringAsync()

        Dim x As New IO.StringReader(responsetext)
        Dim reader = System.Xml.XmlReader.Create(x)

        Return responsetext
    End Function
End Class
