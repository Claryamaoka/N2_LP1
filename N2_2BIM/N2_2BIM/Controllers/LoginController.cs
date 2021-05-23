using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N2_2BIM.DAO;
using N2_2BIM.Models;

namespace N2_2BIM.Controllers
{
    public class LoginController : PadraoController<LoginViewModel>
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult FazLogin(LoginViewModel l)
        {
            LoginDAO dao = new LoginDAO();
            LoginViewModel login = dao.Consulta(l.CPF);

            //consultar na sua tabela de usuários
            //se existe esse usuário e senha
            if (login.CPF == l.CPF && login.senha == l.senha)
            {
                HttpContext.Session.SetString("Logado", "true");

                if(login.Tipo =='I') //o logado é um instrutor
                {
                    return RedirectToAction("index", "Home");
                }
                else //o logado é um aluno
                {
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
}
