using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N2_2BIM.DAO;
using N2_2BIM.Models;
using X.PagedList;

namespace N2_2BIM.Controllers
{
    public class ExercicioController : PadraoController<ExercicioViewModel>
    {
        public ExercicioController()
        {
            DAO = new ExercicioDAO();
            SugereProximoId = true;
        }

        protected override void ValidaDados(ExercicioViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Preencha este campo");

            if (string.IsNullOrEmpty(model.Descricao))
                ModelState.AddModelError("Descricao", "Preencha este campo");
        }

    }
}
