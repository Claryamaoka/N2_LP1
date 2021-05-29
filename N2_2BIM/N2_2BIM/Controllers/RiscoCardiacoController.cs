using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N2_2BIM.DAO;
using N2_2BIM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public override IActionResult Index(int? pagina = null)
        {
            return RedirectToAction("Index", "Home");
        }

        public override IActionResult Create(int? id = null)
        {
            ViewBag.Operacao = "I";
            RiscoCardiacoViewModel model = Activator.CreateInstance(typeof(RiscoCardiacoViewModel)) as RiscoCardiacoViewModel;
            _alunoId = (int)id;
            PreencheDadosParaView("I", model);
            return View(ViewParaCadastro, model);
        }

        protected override void PreencheDadosParaView(string Operacao, RiscoCardiacoViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);

            model = PreencheAluno(model);

            PreparaComboFumo();
            PreparaComboAtividade();
            PreparaComboDoencaFamilia();

            //pegar o Id do instrutor que está logado 
            if (Operacao == "I")
            {
                model.IdInstrutor = (int)HttpContext.Session.GetInt32("IdUsuario");
                model.IdAluno = _alunoId;
            }

        }

        public RiscoCardiacoViewModel PreencheAluno(RiscoCardiacoViewModel model)
        {
            AlunoDAO a = new AlunoDAO();
            AlunoViewModel aluno = new AlunoViewModel();

            aluno = a.Consulta(model.Id);

            int idade = DateTime.Now.Year - aluno.dtNascimento.Year;
            if(DateTime.Now.DayOfYear < aluno.dtNascimento.DayOfYear)
            {
                idade = idade - 1;
            }

            model.IdadeAluno = idade;
            return model;
        }

        public void PreparaComboFumo()
        {
            ViewBag.Fumo = new List<SelectListItem>();
            ViewBag.Fumo.Add(new SelectListItem("Selecione um...", "0"));
            ViewBag.Fumo.Add(new SelectListItem("Não fumante", "Não fumante"));
            ViewBag.Fumo.Add(new SelectListItem("Charuto e/ou cachimbo", "Charuto e/ou cachimbo"));
            ViewBag.Fumo.Add(new SelectListItem("10 cigarros ou menos por dia", "10 cigarros ou menos por dia"));
            ViewBag.Fumo.Add(new SelectListItem("11 a 20 cigarros por dia", "11 a 20 cigarros por dia"));
            ViewBag.Fumo.Add(new SelectListItem("21 a 30 cigarros por dia", "21 a 30 cigarros por dia"));
            ViewBag.Fumo.Add(new SelectListItem("mais de 31 cigarros por dia", "mais de 31 cigarros por dia"));
        }

        public void PreparaComboAtividade()
        {
            ViewBag.Atividade = new List<SelectListItem>();
            ViewBag.Atividade.Add(new SelectListItem("Selecione um...", "0"));
            ViewBag.Atividade.Add(new SelectListItem("Esforço profissional e recreativo intenso", "Esforço profissional e recreativo intenso"));
            ViewBag.Atividade.Add(new SelectListItem("Esforço profissional e recreativo moderado", "Esforço profissional e recreativo moderado"));
            ViewBag.Atividade.Add(new SelectListItem("Trabalho sedentário e esforço recreativo intenso", "Trabalho sedentário e esforço recreativo intenso"));
            ViewBag.Atividade.Add(new SelectListItem("Trabalho sedentário e esforço recreativo moderado", "Trabalho sedentário e esforço recreativo moderado"));
            ViewBag.Atividade.Add(new SelectListItem("Trabalho sedentário e esforço recreativo leve", "Trabalho sedentário e esforço recreativo leve"));
            ViewBag.Atividade.Add(new SelectListItem("Ausência completa de qualquer exercício", "Ausência completa de qualquer exercício"));
        }

        public void PreparaComboDoencaFamilia()
        {
            ViewBag.DoencaFamilia = new List<SelectListItem>();
            ViewBag.DoencaFamilia.Add(new SelectListItem("Selecione um...", "0"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("Nenhuma história conhecida de cardiopatia", "Nenhuma história conhecida de cardiopatia"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("1 parente c/ doença cardiaca e mais de 60 anos", "1 parente c/ doença cardiaca e mais de 60 anos"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("2 parentes c/ doença cardiaca e mais de 60 anos", "2 parentes c/ doença cardiaca e mais de 60 anos"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("1 parente c/ doença cardiaca e menos de 60 anos", "1 parente c/ doença cardiaca e menos de 60 anos"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("2 parentes c/ doença cardiaca e menos de 60 anos", "2 parentes c/ doença cardiaca e menos de 60 anos"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("3 parentes c/ doença cardiaca e menos de 60 anos", "3 parentes c/ doença cardiaca e menos de 60 anos"));
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

            if (string.IsNullOrEmpty(model.Fumo)|| model.Fumo == "0")
                ModelState.AddModelError("Fumo", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Atividade) || model.Atividade == "0")
                ModelState.AddModelError("Atividade", "Preencha este campo");
            if (string.IsNullOrEmpty(model.DoencaFamilia) || model.DoencaFamilia == "0")
                ModelState.AddModelError("DoencaFamilia", "Preencha este campo");

        }
    }
}
