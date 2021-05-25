using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        protected override void PreencheDadosParaView(string Operacao, AulaViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);
            PreencheComboExercicios();
            PreencheComboAlunos();

            //pegar o Id do instrutor que está logado
            if (Operacao == "I")
            {
                model.IdInstrutor = 123;
            }

        }

        public void PreencheComboExercicios()
        {
            var daoExercicios = new ExercicioDAO();

            ViewBag.Exercicios = new List<SelectListItem>();
            ViewBag.Exercicios.Add(new SelectListItem("Selecione um exercicio...", "0"));

            foreach (var ex in daoExercicios.Listagem())
            {
                var elemento = new SelectListItem(ex.Nome, ex.Id.ToString());
                ViewBag.Exercicios.Add(elemento);
            }
        }


        public void PreencheComboAlunos()
        {
            //Listar apenas os alunos daquele instrutor
            var daoAlunos = new AlunoDAO();

            ViewBag.Alunos = new List<SelectListItem>();
            ViewBag.Alunos.Add(new SelectListItem("Selecione um aluno...", "0"));

            foreach (var ex in daoAlunos.Listagem())
            {
                var elemento = new SelectListItem(ex.Nome, ex.Id.ToString());
                ViewBag.Exercicios.Add(elemento);
            }
        }

        protected override void ValidaDados(AulaViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (model.Ex1 <= 0)
                ModelState.AddModelError("Ex1", "Preencha este campo");
            if (model.dataAula < DateTime.Now || model.dataAula == null)
                ModelState.AddModelError("dataAula", "Preencha este campo com uma data válida");

        }
    }
}
