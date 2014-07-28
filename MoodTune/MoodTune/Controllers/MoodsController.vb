Namespace MoodTune
    Public Class MoodsController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Moods

        Function GetList() As ActionResult
            Return View()
        End Function

    End Class
End Namespace