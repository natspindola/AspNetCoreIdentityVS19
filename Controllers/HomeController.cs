using AspNetCoreIdentityVS19.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityVS19.Controllers
{
    [Authorize] //apenas quem tá autenticado pode acessar
    public class HomeController : Controller
    {
        [AllowAnonymous] //todos podem acessar
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")] //o usuário tem que estar autenticado e autorizado a acessar a página
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "PodeExcluir")] //o usuário tem acesso apenas se tiver a permissão "PodeExcluir" vinculado no perfil
        public IActionResult SecretClaim()
        {
            return View("Secret");
        }

        [Authorize(Policy = "PodeEscrever")] //o usuário tem acesso apenas se tiver a permissão "PodeExcluir" vinculado no perfil
        public IActionResult SecretClaimGravar()
        {
            return View("Secret");
        }

        [ClaimAuthorize("Produtos", "Ler")] 
        public IActionResult ClaimCustom()
        {
            return View("Secret");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
