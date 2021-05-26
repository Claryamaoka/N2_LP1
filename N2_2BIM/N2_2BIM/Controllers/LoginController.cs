using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N2_2BIM.DAO;
using N2_2BIM.Models;

namespace N2_2BIM.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            PreencheComboAlunoInstrutor();
            return View();
        }
        
        public void PreencheComboAlunoInstrutor()
        {
            ViewBag.AlunoInstrutor = new List<SelectListItem>();
            ViewBag.AlunoInstrutor.Add(new SelectListItem("Aluno", "A"));
            ViewBag.AlunoInstrutor.Add(new SelectListItem("Instrutor", "I"));
        }


        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult FazLogin(LoginViewModel l)
        {
            VerificaLogin(l);
            if (ModelState.IsValid == false)
            {
                return View("Index", l);
            }
            else
            {
                LoginDAO dao = new LoginDAO();

                //Consulta por meio de Function
                LoginViewModel login = dao.Consulta(l.Id);

                //consultar na sua tabela de usuários
                //se existe esse usuário e senha
                if (login.Id == l.Id && login.senha == l.senha)
                {
                    HttpContext.Session.SetString("Logado", "true");

                    if (login.Tipo == 'I') //o logado é um instrutor
                    {
                        ViewBag.AlunoInstrutor = "I";
                        InstrutorDAO instrutorDAO = new InstrutorDAO();
                        instrutorDAO.Consulta(l.Id);

                        //salvar esse usuario em algum local que possa ser acessado ao longo de todo site

                        return RedirectToAction("index", "Home");
                    }
                    else //o logado é um aluno
                    {
                        ViewBag.AlunoInstrutor = "A";
                        AlunoDAO alunoDAO = new AlunoDAO();
                        alunoDAO.Consulta(l.Id);

                        //salvar esse usuario em algum local que possa ser acessado ao longo de todo site

                        return RedirectToAction("index", "Home");
                    }
                }
                else
                {
                    ViewBag.Erro = "Usuário ou senha inválidos!";
                    return View("Index");
                }
            }
        }

        protected void VerificaLogin(LoginViewModel model)
        {
            if (model.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            if(string.IsNullOrEmpty(model.senha))
                ModelState.AddModelError("senha", "Preencha a senha!");
            if (char.IsWhiteSpace(model.Tipo))
                ModelState.AddModelError("Tipo", "Selecione!");
        }


    }
}
