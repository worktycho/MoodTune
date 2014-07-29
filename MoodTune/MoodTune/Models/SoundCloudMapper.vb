
Option Strict On

Public Class SoundCloudMapper
    Public Shared Async Function GetEmbedId(name As String) As Threading.Tasks.Task(Of String)
        Dim HTTPClient As New System.Net.Http.HttpClient

        Dim response = Await HTTPClient.GetAsync("http://api.soundcloud.com/tracks/?client_id=3a47d0eb7e8a8f8aed68e5efe6d4e491&q=" & name)
        Dim tracks = Await response.Content().ReadAsStringAsync

        Dim XML = XDocument.Parse(tracks)

        Dim trackid = XML.<tracks>.<track>.First().<id>.First().Value

        Return trackid
    End Function
End Class
