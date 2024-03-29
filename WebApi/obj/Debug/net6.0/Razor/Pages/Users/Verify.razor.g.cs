#pragma checksum "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/Pages/Users/Verify.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "40d6275bfedd6a63d1ecc1f5710823f9a7658037"
// <auto-generated/>
#pragma warning disable 1591
namespace WebApi.Pages.Users
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/_Imports.razor"
using WebApi;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/_Imports.razor"
using WebApi.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/Pages/Users/Verify.razor"
using WebApi.Services.UserService;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Users/Verify/{Token:guid}")]
    public partial class Verify : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "text-center");
            __builder.AddMarkupContent(2, "<h3>Verificación de cuenta</h3>\n    ");
            __builder.OpenElement(3, "p");
#nullable restore
#line (7,9)-(7,16) 24 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/Pages/Users/Verify.razor"
__builder.AddContent(4, Message);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(5, "\n    ");
            __builder.OpenElement(6, "button");
            __builder.AddAttribute(7, "class", "btn btn-primary");
            __builder.AddAttribute(8, "style", 
#nullable restore
#line 8 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/Pages/Users/Verify.razor"
                                            Style

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(9, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 8 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/Pages/Users/Verify.razor"
                                                             VerifyAccount

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(10, "VERIFICAR!");
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 12 "/Users/georgemitica/RiderProjects/AsteriskMenuApi/WebApi/Pages/Users/Verify.razor"
       

    [Parameter]
    public Guid Token { get; set; }

    private string Message { get; set; }
    private string Style { get; set; }

    private void VerifyAccount()
    {
        Style = "display: none;";
        Message = "Espere...";
        bool result = _userService.ActivateUser(Token);
        if (result)
        {
            Message = "Cuenta verificada, puedes iniciar sessión en la aplicación";
        }
        else
        {
            Message = "Error al verificar la cuenta, si el error persiste, contacte con nostros.";
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IUserService _userService { get; set; }
    }
}
#pragma warning restore 1591
