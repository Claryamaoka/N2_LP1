using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N2_2BIM.DAO;
using N2_2BIM.Models;

namespace N2_2BIM.Controllers
{
    public class RiscoCardiacoController : PadraoController<RiscoCardiacoViewModel>
    {
        private int _alunoId = 0;
        public RiscoCardiacoController()
        {
            DAO = new RiscoCardiacoDAO();
            SugereProximoId = true;
        }

        public override IActionResult Create(int id)
        {
            ViewBag.Operacao = "I";
            _alunoId = id;
            RiscoCardiacoViewModel model = Activator.CreateInstance(typeof(RiscoCardiacoViewModel)) as RiscoCardiacoViewModel;
            PreencheDadosParaView("I", model);
            return View(ViewParaCadastro, model);
        }

        protected override void PreencheDadosParaView(string Operacao, RiscoCardiacoViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);

            PreparaComboFumo();
            PreparaComboAtividade();
            PreparaComboDoencaFamilia();

            //pegar o Id do instrutor que está logado 
            if (Operacao == "I")
            {
                model.IdInstrutor = 123;
                model.IdAluno = _alunoId;
            }

        }

        public void PreparaComboFumo()
        {

        }

        public void PreparaComboAtividade()
        {

        }

        public void PreparaComboDoencaFamilia()
        {

        }

        protected override void ValidaDados(RiscoCardiacoViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);

            if (model.Pressao <= 0)
                ModelState.AddModelError("Pressao", "Preencha este campo");
            if (model.Peso <= 0)
                ModelState.AddModelError("Peso", "Preencha este campo");
            if (model.Colesterol <= 0)
                ModelState.AddModelError("Colesterol", "Preencha este campo");

            if (string.IsNullOrEmpty(model.Fumo))
                ModelState.AddModelError("Fumo", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Atividade))
                ModelState.AddModelError("Atividade", "Preencha este campo");
            if (string.IsNullOrEmpty(model.DoencaFamilia))
                ModelState.AddModelError("DoencaFamilia", "Preencha este campo");

        }
    }
}
