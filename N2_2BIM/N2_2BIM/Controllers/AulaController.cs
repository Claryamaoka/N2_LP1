using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N2_2BIM.DAO;
using N2_2BIM.Models;
using Microsoft.AspNetCore.Http;

namespace N2_2BIM.Controllers
{
    public class AulaController : PadraoController<AulaViewModel>
    {
        public AulaController()
        {
            DAO = new AulaDAO();
            SugereProximoId = true;
        }

        public override IActionResult Index(int? pagina = null)
        {
            var lista = DAO.Listagem();
            lista = PreparaNomesParaLista(lista);
            return View(ViewParaListagem, lista);
        }

        protected List<AulaViewModel> PreparaNomesParaLista(List<AulaViewModel> lista)
        {
            //Pesquisar pelo id para possui o nome
            AlunoViewModel aluno = new AlunoViewModel();
            ExercicioViewModel exercicio = new ExercicioViewModel();
            AlunoDAO a = new AlunoDAO();
            ExercicioDAO e = new ExercicioDAO();

            foreach (AulaViewModel item in lista)
            {
                aluno = a.Consulta(item.IdAluno);
                item.NomeAluno = aluno.Nome;

                exercicio = e.Consulta(item.Ex1);
                item.NomeExercicio1 = exercicio.Nome;

                exercicio = e.Consulta(item.Ex2);
                item.NomeExercicio2 = exercicio.Nome;

                if (item.Ex3 != 0)
                {
                    exercicio = e.Consulta(item.Ex3);
                    item.NomeExercicio3 = exercicio.Nome;
                }

            }
            return lista;
        }

        protected override void PreencheDadosParaView(string Operacao, AulaViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);
            PreencheComboExercicios();
            PreencheComboAlunos();

            //pegar o Id do instrutor que está logado
            if (Operacao == "I")
            {
                model.IdInstrutor = (int)HttpContext.Session.GetInt32("IdUsuario");
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
                ViewBag.Alunos.Add(elemento);
            }
        }

        protected override void ValidaDados(AulaViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (model.Ex1 <= 0)
                ModelState.AddModelError("Ex1", "Preencha este campo");
            if (model.Ex2 <= 0)
                ModelState.AddModelError("Ex2", "Preencha este campo");
            if (model.dataAula < DateTime.Now || model.dataAula == null)
                ModelState.AddModelError("dataAula", "Preencha este campo com uma data válida");

        }
    }
}
