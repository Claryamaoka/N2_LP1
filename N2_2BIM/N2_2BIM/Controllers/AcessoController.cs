using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace N2_2BIM.Controllers
{
    public class AcessoController : Controller
    {
        //Verifica constantemente se o usuário está logado
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //caso contrário o redireciona para a tela de login
            if (!HelperController.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }
    }
}
