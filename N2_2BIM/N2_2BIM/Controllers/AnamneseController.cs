using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N2_2BIM.DAO;
using N2_2BIM.Models;

namespace N2_2BIM.Controllers
{
    public class AnamneseController : PadraoController<AnamneseViewModel>
    {
        private int _alunoId = 0;
        public AnamneseController()
        {
            DAO = new AnamneseDAO();
            SugereProximoId = true;
        }

        public IActionResult Create(int id)
        {
            ViewBag.Operacao = "I";
            _alunoId = id;
            AnamneseViewModel model = Activator.CreateInstance(typeof(AnamneseViewModel)) as AnamneseViewModel;
            PreencheDadosParaView("I", model);
            return View(ViewParaCadastro, model);
        }

        protected override void PreencheDadosParaView(string Operacao, AnamneseViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);
            //pegar o Id do instrutor que está logado 
            if (Operacao == "I")
            {
                model.IdInstrutor = 123;
                model.IdAluno = _alunoId;
                model.DataAvaliacao = DateTime.Now;
            }

        }

        protected override void ValidaDados(AnamneseViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);

            if (model.Altura <=0)
                ModelState.AddModelError("Altura", "Preencha este campo");
            if (model.Peso <= 0)
                ModelState.AddModelError("Peso", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Elasticidade))
                ModelState.AddModelError("Elasticidade", "Preencha este campo");

        }
    }
}
