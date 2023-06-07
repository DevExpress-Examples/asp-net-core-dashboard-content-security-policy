Imports CSPDashboardExample.Models
Imports DevExpress.DashboardAspNetCore
Imports DevExpress.DashboardWeb
Imports Microsoft.AspNetCore.DataProtection
Imports Microsoft.AspNetCore.Mvc

Namespace CSPDashboardExample.Controllers

    Public Class DefaultDashboardController
        Inherits DashboardController

        Public Sub New(ByVal configurator As DashboardConfigurator, ByVal Optional dataProtectionProvider As IDataProtectionProvider? = Nothing)
            MyBase.New(configurator, dataProtectionProvider)
        End Sub

        Public Function Index() As IActionResult
            Return View(New DashboardModel())
        End Function
    End Class
End Namespace
