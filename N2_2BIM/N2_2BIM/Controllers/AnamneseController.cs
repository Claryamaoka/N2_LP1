using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N2_2BIM.DAO;
using N2_2BIM.Models;
using Microsoft.AspNetCore.Http;
using X.PagedList;

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
            string procedure = "spConsultaAnamneseInstrutor";

            try
            {
                int numeroPagina = (pagina ?? 1);

                //var lista = DAO.ConsultaDiferenciada(id, procedure);
                var lista = DAO.Listagem();
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
            return View("Print", id);
        }
    }
}
