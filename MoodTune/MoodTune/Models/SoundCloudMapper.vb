
Option Strict On

Public Class SoundCloudMapper
    Public Shared Async Function GetEmbedId(name As String) As Threading.Tasks.Task(Of String)
        Dim cache = HttpRuntime.Cache
        If cache("SoundCloudID." & name) IsNot Nothing Then
            Debug.WriteLine("Cache Hit at SoundCloudID :" & name)
            Return CStr(cache("SoundCloudID." & name))
        End If

        Debug.WriteLine("Cache miss at SoundCloudID :" & name)
        Dim HTTPClient As New System.Net.Http.HttpClient

        Dim response = Await HTTPClient.GetAsync("http://api.soundcloud.com/tracks/?client_id=3a47d0eb7e8a8f8aed68e5efe6d4e491&q=" & name)
        Dim tracks = Await response.Content().ReadAsStringAsync

        Dim XML = XDocument.Parse(tracks)
        Dim track = XML.<tracks>.<track>.First()
        Dim trackid = track.<id>.First().Value
        Dim trackTitle = track.<title>.First().Value
        cache("SoundCloudID." & name) = trackid
        cache("SoundCloudTitle." & name) = trackTitle

        Debug.WriteLine("Cache store at SoundCloudID :" & name)
        Return trackid
    End Function

    Shared Function TryGetSoundCloudTitle(Name As String) As String
        Dim cache = HttpRuntime.Cache
        If cache("SoundCloudTitle." & Name) IsNot Nothing Then
            Debug.WriteLine("Cache Hit at SoundCloudTitle:" & Name)
            Return CStr(cache("SoundCloudTitle." & Name))
        End If

        Debug.WriteLine("Cache miss at SoundCloudTitle :" & Name)
    End Function

End Class
