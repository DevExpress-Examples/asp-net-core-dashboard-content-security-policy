Imports System.Security.Cryptography
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.AspNetCore.Mvc.RazorPages

Namespace CSPDashboardExample.Models

    Public Class DashboardModel
        Inherits PageModel

        Public Property Nonce As String

        Public Sub New()
            Dim nonceBytes = New Byte(31) {}
            Dim generator = RandomNumberGenerator.Create()
            generator.GetBytes(nonceBytes)
            Nonce = Convert.ToBase64String(nonceBytes)
        End Sub

        Public Function OnGet() As IActionResult
            HttpContext.Response.Headers.Add("Content-Security-Policy", "img-src data: https: http:;" & String.Format("script-src 'self' 'nonce-{0}';", Nonce) + String.Format("style-src 'self' 'nonce-{0}';", Nonce))
            Return Page()
        End Function
    End Class
End Namespace
