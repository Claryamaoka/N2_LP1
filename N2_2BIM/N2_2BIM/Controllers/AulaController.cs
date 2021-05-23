using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N2_2BIM.DAO;
using N2_2BIM.Models;

namespace N2_2BIM.Controllers
{
    public class AulaController : PadraoController<AulaViewModel>
    {
        public AulaController()
        {
            DAO = new AulaDAO();
            SugereProximoId = true;
        }

        protected override void ValidaDados(AulaViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (model.Ex1 <= 0)
                ModelState.AddModelError("Ex1", "Preencha este campo");
            
        }
    }
}
