Imports Newtonsoft.Json
<JsonObject>
Public Class SongInfo
    <JsonProperty(Required:=Required.Always)>
    Public played As Integer
    <JsonProperty(DefaultValueHandling:=DefaultValueHandling.Include, Required:=Required.Default)>
    Public skiped As Integer = 0
    <JsonProperty(DefaultValueHandling:=DefaultValueHandling.Include, Required:=Required.Default)>
    Public ListenedRecently As Integer = 0
End Class
