Namespace MoodTune
    Public Class MoodsController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Moods

        Async Function GetList() As Threading.Tasks.Task(Of ActionResult)
            Dim tagstring = Await LastFMTagFetcher.GetSongs()
            Return View(model:=New With {.tagstring = tagstring})
        End Function

    End Class
End Namespace