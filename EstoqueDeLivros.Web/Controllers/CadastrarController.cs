using EstoqueDeLivros.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EstoqueDeLivros.Web.Controllers
{
    public class CadastrarController : Controller
    {

        [Authorize]
        public ActionResult Livro()
        {
            return View(LivroViewModel.ListaLivrosBanco());
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditarLivro(int id)
        {
            return Json(LivroViewModel.Editar(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExcluirLivro(int id)
        {
            return Json(LivroViewModel.Excluir(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult SalvarLivro(LivroViewModel modelLivros)
        {
            var result = "OK!";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                result = "AVISO!";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x=>x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    var id = modelLivros.Salvar();

                    if(id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        result = "ERRO!";
                    }
                }
                catch (Exception ex)
                {
                    result = "ERRO!";
                }
            }

            return Json(new { Result = result, Mensagens = mensagens, IdSalvo = idSalvo });
        }
    }
}