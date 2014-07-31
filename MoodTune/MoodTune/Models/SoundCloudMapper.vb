
Option Strict On

Public Class SoundCloudMapper
    Public Shared Async Function GetEmbedId(name As String) As Threading.Tasks.Task(Of String)
        Dim cache = HttpRuntime.Cache
        If cache("SoundCloudID." & name) IsNot Nothing Then
            Return CStr(cache("SoundCloudID." & name))
        End If
        Dim HTTPClient As New System.Net.Http.HttpClient

        Dim response = Await HTTPClient.GetAsync("http://api.soundcloud.com/tracks/?client_id=3a47d0eb7e8a8f8aed68e5efe6d4e491&q=" & name)
        Dim tracks = Await response.Content().ReadAsStringAsync

        Dim XML = XDocument.Parse(tracks)

        Dim trackid = XML.<tracks>.<track>.First().<id>.First().Value
        cache("SoundCloudID." & name) = trackid
        Return trackid
    End Function
End Class
