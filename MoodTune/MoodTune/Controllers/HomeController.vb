
Imports Newtonsoft.Json

Namespace MoodTune
    Public Class HomeController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /

        Function Index() As ActionResult
            Return View()
        End Function

        ' Post: /SetLearnedPrefs

        <HttpPost()>
        Function SetLearnedPrefs(prefsJSON As String) As ActionResult
            Dim prefsJObj As Linq.JObject = JsonConvert.DeserializeObject(prefsJSON)
            prefsJObj = prefsJObj("prefs")
            Dim prefs As Dictionary(Of String, SongInfo)
            prefs = prefsJObj.Properties _
                .Select(Function(prop As Linq.JProperty, i As Integer) As KeyValuePair(Of String, Linq.JToken)
                            Return New KeyValuePair(Of String, Linq.JToken)(prop.Name, prop.Value)
                        End Function) _
                .Where(Function(kv As KeyValuePair(Of String, Linq.JToken)) As Boolean
                           Return TypeOf (kv.Value) Is Linq.JObject
                       End Function) _
                .Select(Function(kv As KeyValuePair(Of String, Linq.JToken)) As KeyValuePair(Of String, SongInfo)
                            Return New KeyValuePair(Of String, SongInfo)(kv.Key, New SongInfo With {.played = kv.Value("played"), .skiped = kv.Value("skiped")})
                        End Function) _
                .Where(Function(keyvalue As KeyValuePair(Of String, SongInfo)) As Boolean
                           Return keyvalue.Value IsNot Nothing
                       End Function) _
                .ToDictionary(Function(keyvalue As KeyValuePair(Of String, SongInfo)) As String
                                  Return keyvalue.Key
                              End Function,
                              Function(keyvalue As KeyValuePair(Of String, SongInfo)) As SongInfo
                                  Return keyvalue.Value
                              End Function)
            Session("song_perfs") = prefs
            Dim res = New ContentResult()
            Return res
        End Function

    End Class
End Namespace