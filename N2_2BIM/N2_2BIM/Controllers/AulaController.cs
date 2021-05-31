using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N2_2BIM.DAO;
using N2_2BIM.Models;
using Microsoft.AspNetCore.Http;
using X.PagedList;
using System.Threading;

namespace N2_2BIM.Controllers
{
    public class AulaController : PadraoController<AulaViewModel>
    {
        public AulaController()
        {
            DAO = new AulaDAO();
            SugereProximoId = true;
        }

        //Listar apenas as aulas cadastradas pelo instrutor logado OU
        //Listar apenas as aulas do usuário logado
        public override IActionResult Index(int? pagina = null)
        {
            int id = (int)HttpContext.Session.GetInt32("IdUsuario");
            string procedure = "spListaAulas";
            string aux;
            if (HttpContext.Session.GetString("TipoUsuario") == "I")
                aux = "1";
            else
                aux = "0";

            try
            {
                int numeroPagina = (pagina ?? 1);

                var lista = DAO.ConsultaDiferenciada(id,aux,null,procedure);
                lista = PreparaNomesParaLista(lista);
                return View(ViewParaListagem, lista.ToPagedList(numeroPagina, itensPorPagina));
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        //Permite que apareça o nome do aluno assim como o nome do exercicio na listagem de aulas
        //puxando ambos por seus respectivos Ids
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

            //pega o Id do instrutor que está logado
            if (Operacao == "I")
            {
                model.IdInstrutor = (int)HttpContext.Session.GetInt32("IdUsuario");
            }

        }

        /// <summary>
        /// Preenche a combo box de Exercicios com os Exercicios cadastrados
        /// </summary>
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

        /// <summary>
        /// Preenche a combo box de Alunos com os Alunos cadastrados daquele instrutor
        /// </summary>
        public void PreencheComboAlunos()
        {
            //Listar apenas os alunos daquele instrutor
            var daoAlunos = new AlunoDAO();
            int id = (int)HttpContext.Session.GetInt32("IdUsuario");

            ViewBag.Alunos = new List<SelectListItem>();
            ViewBag.Alunos.Add(new SelectListItem("Selecione um aluno...", "0"));

            //foreach (var ex in daoAlunos.ConsultaDiferenciada(id, "spConsultaAluno")
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

        public IActionResult FazConsultaAjax(string nomeAlunoAula, DateTime dataAula)
        {
            try
            {
                Thread.Sleep(1000); // para dar tempo de ver o gif na tela..rs
                if (nomeAlunoAula == null)
                    nomeAlunoAula = "";

                int id = (int)HttpContext.Session.GetInt32("IdUsuario");

                string aux;
                if (HttpContext.Session.GetString("TipoUsuario") == "I")
                    aux = "nomeAlunoAula";
                else
                    aux = "0";

                string obj;
                if (dataAula.ToShortDateString() == "01/01/0001")
                    obj = null;
                else
                    obj = dataAula.ToShortDateString();


                string procedure = "spListaAulas";

                var lista = (DAO as AulaDAO).ConsultaDiferenciada(id, aux, obj, procedure); // retorna todos os registro
                return PartialView("pvGrid", lista.ToPagedList(1, itensPorPagina));
            }
            catch
            {
                return Json(new { erro = true });
            }
        }
    }
}
