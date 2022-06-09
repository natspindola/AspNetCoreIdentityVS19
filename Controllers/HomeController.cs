using AspNetCoreIdentityVS19.Models;
using KissLog;
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
        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [AllowAnonymous] //todos podem acessar
        public IActionResult Index()
        {
            _logger.Trace(message: "Usuario acessou a home!");

            return View();
        }

        public IActionResult Privacy()
        {
            throw new Exception(message: "Erro");
            return View();
        }

        [Authorize(Roles = "Admin")] //o usuário tem que estar autenticado e autorizado a acessar a página
        public IActionResult Secret()
        {
            try
            {
                throw new Exception(message: "Algo horrível aconteceu!");
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }

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

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "A página que você está procurando não existe. <br /> Em caso de dúvidas procure o suporte.";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso negado.";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelErro);
        }
    }
}
