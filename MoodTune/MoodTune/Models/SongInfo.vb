Imports Newtonsoft.Json
Public Class SongInfo
    <JsonProperty(Required:=Required.Always)>
    Public played As Integer
    <JsonProperty(Required:=Required.Always)>
    Public skiped As Integer
    <JsonProperty(DefaultValueHandling:=DefaultValueHandling.Include, Required:=Required.Default)>
    Public ListenedRecently As Integer = 0
End Class
