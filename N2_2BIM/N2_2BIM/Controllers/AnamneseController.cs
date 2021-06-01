using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N2_2BIM.DAO;
using N2_2BIM.Models;
using Microsoft.AspNetCore.Http;
using X.PagedList;
using System.Threading;

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

        public override IActionResult Index(int? pagina = null)
        {
            int id = (int)HttpContext.Session.GetInt32("IdUsuario");
            string procedure = "spListarAnamnese";

            try
            {
                int numeroPagina = (pagina ?? 1);

                var lista = DAO.ConsultaDiferenciada(id,null,null, procedure);

                lista = PreparaNomeAlunoLista(lista);
                return View(ViewParaListagem, lista.ToPagedList(numeroPagina, itensPorPagina));
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        //Permite que o nome do usuário, sendo puxado por seu id, seja informado na listagem
        public List<AnamneseViewModel> PreparaNomeAlunoLista (List<AnamneseViewModel> lista)
        {
            AlunoDAO a = new AlunoDAO();
            AlunoViewModel aluno = new AlunoViewModel();

            foreach(AnamneseViewModel item in lista)
            {
                aluno = a.Consulta(item.IdAluno);
                item.NomeAluno = aluno.Nome;
            }

            return lista; 
        }

        //Salva o id do aluno que foi selecionado para poder preencher o campo IdAluno
        public override IActionResult Create(int? id = null)
        {
            AlunoDAO a = new AlunoDAO();
            AlunoViewModel aluno = new AlunoViewModel();

            ViewBag.Operacao = "I";
            AnamneseViewModel model = Activator.CreateInstance(typeof(AnamneseViewModel)) as AnamneseViewModel;
            _alunoId = (int)id;
            PreencheDadosParaView("I", model);
            return View(ViewParaCadastro, model);
        }

        protected override void PreencheDadosParaView(string Operacao, AnamneseViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);


            //pega o Id do instrutor que está logado 
            if (Operacao == "I")
            {
                model.IdInstrutor = (int)HttpContext.Session.GetInt32("IdUsuario");
                model.IdAluno = _alunoId;
                AlunoDAO a = new AlunoDAO();
                AlunoViewModel aluno = new AlunoViewModel();

                aluno = a.Consulta(model.IdAluno);
                model.NomeAluno = aluno.Nome;
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

        public IActionResult Print(int id)
        {
            AnamneseViewModel model = DAO.Consulta(id);
            AlunoDAO a = new AlunoDAO();
            AlunoViewModel aluno = new AlunoViewModel();

            aluno = a.Consulta(model.IdAluno);
            model.NomeAluno = aluno.Nome;
            
            return View("Print", model);
        }

        public IActionResult FazConsultaAjax(string idAluno, DateTime dataAvaliacao)
        {
            try
            {
                Thread.Sleep(1000); // para dar tempo de ver o gif na tela..rs
                if (idAluno == null)
                    idAluno = "";

                int id = (int)HttpContext.Session.GetInt32("IdUsuario");

                string obj;
                if (dataAvaliacao.ToShortDateString() == "01/01/0001")
                    obj = null;
                else
                    obj = dataAvaliacao.ToShortDateString();

                string procedure = "spListarAnamnese";

                var lista = (DAO as AnamneseDAO).ConsultaDiferenciada(id, idAluno, obj, procedure); // retorna todos os registro
                lista = PreparaNomeAlunoLista(lista);
                return PartialView("pvGrid", lista.ToPagedList(1, itensPorPagina));
            }
            catch
            {
                return Json(new { erro = true });
            }
        }
    }
}
