using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GEBIntegrador.Dominio;
using System.Text.Json;

namespace GEBIntegrador.Web.Controllers
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ClaimsRequirementAttribute : TypeFilterAttribute
    {
        public ClaimsRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimsRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }

    public class ClaimsRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;

        public ClaimsRequirementFilter(Claim claim)
        {
            _claim = claim;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasClaim = false;
            if (_claim.Type == "Menu")
            {
                string menuJson = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Menu").Value;
                var jsonString2 = JsonSerializer.Deserialize<List<menu>>(menuJson); // Reemplaza esto con tu JSON
                List<menu> listaMenu = JsonSerializer.Deserialize<List<menu>>(menuJson);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true
                };

                List<string> urls = new List<string>();

                if (!string.IsNullOrEmpty(menuJson))
                {
                    foreach (var menu in jsonString2)
                    {
                        urls.AddRange(GetUrls(menu)
                            .Where(url => !string.IsNullOrEmpty(url)));
                    }
                }

                hasClaim = urls.Contains(_claim.Value);
            }

            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
        private static IEnumerable<string> GetUrls(menu menu)
        {
            if (menu == null)
                yield break;

            if (!string.IsNullOrEmpty(menu.v_url))
                yield return menu.v_url;

            if (menu.Inversen_id_menu_padreNavigation != null)
            {
                foreach (var subMenu in menu.Inversen_id_menu_padreNavigation)
                {
                    foreach (var subUrl in GetUrls(subMenu))
                    {
                        yield return subUrl;
                    }
                }
            }
        }
    }
}
