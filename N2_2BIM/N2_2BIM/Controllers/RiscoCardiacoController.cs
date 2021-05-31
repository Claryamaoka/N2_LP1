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
        private int somaPeso = 0;
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
            AlunoViewModel aluno;

            aluno = a.Consulta(_alunoId);

            int idade = DateTime.Now.Year - aluno.dtNascimento.Year;
            if(DateTime.Now.DayOfYear < aluno.dtNascimento.DayOfYear)
            {
                idade = idade - 1;
            }

            model.IdadeAluno = idade;
            if (aluno.Sexo == 'F')
                model.Sexo = "Feminino";
            else
                model.Sexo = "Masculino";
            return model;
        }

        public void PreparaComboFumo()
        {
            ViewBag.Fumo = new List<SelectListItem>();
            ViewBag.Fumo.Add(new SelectListItem("Selecione um...", "0"));
            ViewBag.Fumo.Add(new SelectListItem("Não fumante", "0 - Não fumante"));
            ViewBag.Fumo.Add(new SelectListItem("Charuto e/ou cachimbo", "1 - Charuto e/ou cachimbo"));
            ViewBag.Fumo.Add(new SelectListItem("10 cigarros ou menos por dia", "2 - 10 cigarros ou menos por dia"));
            ViewBag.Fumo.Add(new SelectListItem("11 a 20 cigarros por dia", "4 - 11 a 20 cigarros por dia"));
            ViewBag.Fumo.Add(new SelectListItem("21 a 30 cigarros por dia", "6 - 21 a 30 cigarros por dia"));
            ViewBag.Fumo.Add(new SelectListItem("mais de 31 cigarros por dia", "10 - mais de 31 cigarros por dia"));
        }

        public void PreparaComboAtividade()
        {
            ViewBag.Atividade = new List<SelectListItem>();
            ViewBag.Atividade.Add(new SelectListItem("Selecione um...", "0"));
            ViewBag.Atividade.Add(new SelectListItem("Esforço profissional e recreativo intenso", "1 - Esforço profissional e recreativo intenso"));
            ViewBag.Atividade.Add(new SelectListItem("Esforço profissional e recreativo moderado", "2 - Esforço profissional e recreativo moderado"));
            ViewBag.Atividade.Add(new SelectListItem("Trabalho sedentário e esforço recreativo intenso", "3 - Trabalho sedentário e esforço recreativo intenso"));
            ViewBag.Atividade.Add(new SelectListItem("Trabalho sedentário e esforço recreativo moderado", "5 - Trabalho sedentário e esforço recreativo moderado"));
            ViewBag.Atividade.Add(new SelectListItem("Trabalho sedentário e esforço recreativo leve", "6 - Trabalho sedentário e esforço recreativo leve"));
            ViewBag.Atividade.Add(new SelectListItem("Ausência completa de qualquer exercício", "8 - Ausência completa de qualquer exercício"));
        }

        public void PreparaComboDoencaFamilia()
        {
            ViewBag.DoencaFamilia = new List<SelectListItem>();
            ViewBag.DoencaFamilia.Add(new SelectListItem("Selecione um...", "0"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("Nenhuma história conhecida de cardiopatia", "1 - Nenhuma história conhecida de cardiopatia"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("1 parente c/ doença cardiaca e mais de 60 anos", "2 - 1 parente c/ doença cardiaca e mais de 60 anos"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("2 parentes c/ doença cardiaca e mais de 60 anos", "3 - 2 parentes c/ doença cardiaca e mais de 60 anos"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("1 parente c/ doença cardiaca e menos de 60 anos", "4 - 1 parente c/ doença cardiaca e menos de 60 anos"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("2 parentes c/ doença cardiaca e menos de 60 anos", "6 - 2 parentes c/ doença cardiaca e menos de 60 anos"));
            ViewBag.DoencaFamilia.Add(new SelectListItem("3 parentes c/ doença cardiaca e menos de 60 anos", "7 - 3 parentes c/ doença cardiaca e menos de 60 anos"));
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

        public IActionResult CalculaRiscoCardiaco(RiscoCardiacoViewModel model)
        {
            ValidaDados(model, "I");
            if (ModelState.IsValid == false)
            {
                return View(ViewParaCadastro, model);
            }
            else
            {
                model.IMC = CalculaIMC(model.Peso, model.Altura).ToString();
                int soma = 0;
                soma += VerificaIdade(model.IdadeAluno);
                soma += somaPeso;
                soma += CalculaPressao(model.Pressao);
                soma += CalculaColesterol(model.Colesterol);
                soma += CalculaAtividade(model.Atividade);
                soma += CalculaFumo(model.Fumo);
                soma += CalculaDoenca(model.DoencaFamilia);
                soma += CalculaSexo(model.Sexo, model.IdadeAluno);

                model.Resultado = CalculaResultado(soma);

                PreparaComboFumo();
                PreparaComboAtividade();
                PreparaComboDoencaFamilia();
                return View(ViewParaCadastro, model);
            }
        }

        public int VerificaIdade(int idade)
        {
            if (idade <= 20)
                return 1;
            else if (idade <= 30)
                return 2;
            else if (idade <= 40)
                return 3;
            else if (idade <= 50)
                return 4;
            else if (idade <= 60)
                return 6;
            else
                return 8;
        }
        public int CalculaSexo(string sexo, int idade)
        {
            if (idade <= 40 && sexo == "Feminino")
                return 1;
            else if (idade <= 50 && sexo == "Feminino")
                return 2;
            else if (sexo == "Feminino")
                return 3;
            else if (idade <= 40 && sexo == "Masculino")
                return 4;
            else if (idade <= 50 && sexo == "Masculino")
                return 6;
            else
                return 7;
        }
        public string CalculaIMC(double peso, double altura)
        {
            double imc = peso / (altura * altura);

            if (imc <= 16.9)
            {
                somaPeso = 0;
                return imc.ToString() + "kg/m² - Muito Abaixo do Peso";
            }  
            else if (imc <= 18.4)
            {
                somaPeso = 1;
                return imc.ToString() + "kg/m²  - Abaixo do peso";
            }
            else if (imc <= 24.9)
            {
                somaPeso = 2;
                return imc.ToString() + "kg/m²  - Peso normal";
            }
            else if (imc <= 29.9)
            {
                somaPeso = 3;
                return imc.ToString() + "kg/m²  - Acima do peso";
            }
            else if (imc <= 34.9)
            {
                somaPeso = 4;
                return imc.ToString() + "kg/m²  - Obesidade Grau I";
            }
            else if(imc <= 40)
            {
                somaPeso = 5;
                return imc.ToString() + "kg/m²  - Obesidade Grau II";
            }
            else
            {
                somaPeso = 7;
                return imc.ToString() + "kg/m²  - Obesidade Grau III";
            }

        }
        public int CalculaPressao(double i)
        {
            if (i <= 119)
                return 1;
            else if (i <= 139)
                return 2;
            else if (i <= 159)
                return 3;
            else if (i <= 179)
                return 4;
            else if (i <= 199)
                return 6;
            else
                return 8;
        }
        public int CalculaColesterol(double i)
        {
            if (i <= 180)
                return 1;
            else if (i <= 205)
                return 2;
            else if (i <= 230)
                return 3;
            else if (i <= 255)
                return 4;
            else if (i <= 280)
                return 5;
            else
                return 7;
        }
        public int CalculaAtividade(string at)
        {
            string x = at.Substring(0, 1);
            return int.Parse(x);
        }
        public int CalculaFumo(string fumo)
        {
            string x = fumo.Substring(0, 1);
            return int.Parse(x);
        }
        public int CalculaDoenca(string doen)
        {
            string x = doen.Substring(0, 1);
            return int.Parse(x);
        }
        public string CalculaResultado(int soma)
        {
            if (soma <= 11)
                return soma.ToString() + " - Sem risco";
            else if (soma <= 17)
                return soma.ToString() + " - Risco abaixo da média";
            else if (soma <= 24)
                return soma.ToString() + " - Risco médio";
            else if (soma <= 31)
                return soma.ToString() + " - Risco moderado";
            else if (soma <= 40)
                return soma.ToString() + " - Risco alto";
            else
                return soma.ToString() + " - Risco muito alto";
        }
    }
}

/*1 - Sem risco - de 6 a 11
2 - Risco abaixo da média - de 12 a 17
3 - Risco médio - de 18 a 24
4 - Risco moderado - de 25 a 31
5 - Risco alto - de 32 a 40
6 - Risco muito alto - 41 a 62*/
