<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/616836996/23.1.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1154893)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# BI Dashboard for ASP.NET Core - Content Security Policy (CSP)

This example demonstrates how to implement a nonce-based [Content Security Policy (CSP)](https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP) for an ASP.NET Core Application with Razor Pages through a HTTP response header.

Use the nonce-based approach to disallow inline script and style execution.

## Example Overview

In a page model (*DashboardModel.cs*), generate the nonce value. In this example, the [RandomNumberGenerator](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator?view=net-6.0) class is used to generate cryptographically strong random values. 

```cs
using System.Security.Cryptography;
//...
public string Nonce { get; set; }
public DashboardModel() {
    var nonceBytes = new byte[32];
    var generator = RandomNumberGenerator.Create();
    generator.GetBytes(nonceBytes);
    Nonce = Convert.ToBase64String(nonceBytes);
}
```

In the `OnGet` handler method, add a HTTP header with the Content Security Policy with the nonce for `script-src` and `style-src` directives:

```cs
public IActionResult OnGet() {
    HttpContext.Response.Headers.Add("Content-Security-Policy",
        "img-src data: https: http:;" +
        string.Format("script-src 'self' 'nonce-{0}';", Nonce) +
        string.Format("style-src 'self' 'nonce-{0}';", Nonce) 
            );
    return Page();
}
```
The new nonce value is generated each time the page loads. 

On the page (*Index.cshtml*), add the `@model` directive and pass the nonce value to `Nonce` method:

```html
@page
@model CSPDashboardExample.Models.DashboardModel

<div class="my-dashboard-container">
@(Html.DevExpress().Dashboard("dashboardControl1")
    .ControllerName("DefaultDashboard")
    .Nonce(Model.Nonce)
    .Width(null)
    .Height(null)
    .OnBeforeRender("onBeforeRender")
)
</div>
```

## Files to Review

- [DashboardModel.cs](./CS/CSPDashboardExample/Models/DashboardModel.cs)
- [Index.cshtml](./CS/CSPDashboardExample/Pages/Index.cshtml)

## Documentation

- [Content Security Policy in ASP.NET Core Applications](https://docs.devexpress.com/Dashboard/404187)
