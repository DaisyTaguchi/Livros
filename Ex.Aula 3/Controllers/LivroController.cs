using ExemploAula3.Data;
using ExemploAula3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploAula3.Controllers
{
    public class LivroController : Controller
    {
        private readonly DataContext dataContext;

        public LivroController(DataContext dc)
        {
            dataContext = dc;
        }

        //Requisição para exibir a pagina
        public IActionResult Create()
        {
            return View();
        }

        //Requisição do botaoo "submit"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Livro livro)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.TipoMensagem = "ERRO";
                ViewBag.Mensagem = "Os dados preenchidos estÃ£o incorretos";

                return View();
            }
            else
            {
                dataContext.Livros.Add(livro);
                dataContext.SaveChanges();

                TempData["TipoMensagem"] = "SUCESSO";
                TempData["Mensagem"] = "Livro cadastrado com sucesso";

                return RedirectToAction("Index");
            }
        }

        public IActionResult Index()
        {
            List<Livro> lista = dataContext.Livros.ToList();
            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Livro livro)
        {
            if (livro == null)
                return RedirectToAction("Index");
            else
            {
                if (livro.Titulo == null)
                    livro.Titulo = "";

                List<Livro> lista = dataContext.Livros.Where(x => x.Titulo.ToUpper().Contains(livro.Titulo.ToUpper())).ToList();
                List<Livro> livros1 = dataContext.Livros.Where(x => x.Editora.ToUpper().Contains(livro.Editora.ToUpper())).ToList();
                List<Livro> livros = dataContext.Livros.Where(x => x.Tipo.ToUpper().Contains(livro.Tipo.ToUpper())).ToList();

                return View(lista);

                //var lista2 =
                //    from Livro l in dataContext.Livros
                //    where l.Titulo.ToUpper().Contains(livro.Titulo.ToUpper())
                //    select l;

                //return View(lista2.ToList());
            }
        }

        

        public IActionResult Delete(int? Id)
        {
            if (Id.HasValue)
            {
                Livro livro = dataContext.Livros.FirstOrDefault(x => x.Id == Id);

                if (livro != null)
                {
                    dataContext.Livros.Remove(livro);
                    dataContext.SaveChanges();

                    TempData["TipoMensagem"] = "SUCESSO";
                    TempData["Mensagem"] = $"O livro '{livro.Titulo}' foi removido com sucesso";

                    return RedirectToAction("Index");
                }
            }

            return NoContent();
        }

        public IActionResult Edit(int? Id)
        {
            if (Id.HasValue)
            {
                Livro livro = dataContext.Livros.FirstOrDefault(x => x.Id == Id);
                if (livro != null)
                    return View(livro);
            }

            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Livro livro)
        {
            if (ModelState.IsValid)
            {
                if (dataContext.Livros.Any(x => x.Id == livro.Id))
                {
                    try
                    {
                        dataContext.Livros.Update(livro);
                        dataContext.SaveChanges();
                    }
                    catch
                    {
                        ViewBag.TipoMensagem = "ERRO";
                        ViewBag.Mensagem = "O livro não pode ser atualizado";

                        return View();
                    }

                    TempData["TipoMensagem"] = "SUCESSO";
                    TempData["Mensagem"] = "Livro atualizado com sucesso";
                    return RedirectToAction("Index");
                }
            }

            return NoContent();
        }

    }
}

