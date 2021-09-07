using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;

namespace DevIO.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormataDocumento(this RazorPage page, int tipoPessoa, string documento) => 
            tipoPessoa == 1 ? Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");

        public static string MarcarOpcao(this RazorPage page, int tipoPessoa, int valor) => 
            tipoPessoa == valor ? "checked" : "";

        public static bool IfClaim(this RazorPage page, string claimName, string claimValue) => 
            CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue);

        public static string IfClaimShow(this RazorPage page, string claimName, string claimValue) => 
            CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue) ? "" : "disabled";

        public static IHtmlContent IfClaimShow(this IHtmlContent page, HttpContext context, string claimName, string claimValue) => 
            CustomAuthorization.ValidarClaimsUsuario(context, claimName, claimValue) ? page : null;
    }
}