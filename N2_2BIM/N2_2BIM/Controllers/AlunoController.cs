using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using N2_2BIM.DAO;
using N2_2BIM.Models;
using X.PagedList;

namespace N2_2BIM.Controllers
{
    public class AlunoController : PadraoController<AlunoViewModel>
    {
        public AlunoController()
        {
            DAO = new AlunoDAO();
            SugereProximoId = true;
        }

        //Método sobreescrito do método Edit
        public override IActionResult Edit(int id)
        {
            try
            {
                //se a pessoa quem estiver editando for o próprio Aluno 
                //seu id não será passado pela url, mas sim pela Session
                if(HttpContext.Session.GetString("TipoUsuario") == "A")
                    id = (int)HttpContext.Session.GetInt32("IdUsuario");

                ViewBag.Operacao = "A";
                var model = DAO.Consulta(id);
                if (model == null)
                    return RedirectToAction(ViewParaListagem);
                else
                {
                    PreencheDadosParaView("A", model);
                    return View(ViewParaCadastro, model);
                }
            }
            catch
            {
                return RedirectToAction(ViewParaListagem);
            }
        }

        protected override void PreencheDadosParaView(string Operacao, AlunoViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);
            PreencheComboSexo();

            //pega o Id do instrutor que está logado e salva na model do aluno
            //coloca senha default
            if (Operacao == "I")
            {
                model.IdInstrutor = (int)HttpContext.Session.GetInt32("IdUsuario");
                model.Senha = "senha123";
            }

            //Impede o Instrutor de ver e editar a senha do aluno depois de cadastrado
            if (HttpContext.Session.GetString("TipoUsuario") == "I" && Operacao == "A")
                ViewBag.Senha = "Close";
            else
                ViewBag.Senha = "Open";

        }

        /// <summary>
        /// Consultar pelo Id do Instrutor para listar os alunos na página
        /// </summary>
        /// <param name="pagina"></param>
        /// <returns></returns>
        public override IActionResult Index(int? pagina = null)
        {
            try
            {
                int id = (int)HttpContext.Session.GetInt32("IdUsuario");
                const int itensPorPagina = 5;
                int numeroPagina = (pagina ?? 1);

                //Responsável por listar apenas os alunos daquele instrutor
                if (HttpContext.Session.GetString("TipoUsuario") == "I")
                {
                    var lista = DAO.ConsultaDiferenciada(id,"spConsultaAluno");
                    return View(ViewParaListagem, lista.ToPagedList(numeroPagina, itensPorPagina));
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        //verifica os dados informados para salvar o aluno
        protected override void ValidaDados(AlunoViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (!ValidaCPF(model.CPF))
                ModelState.AddModelError("CPF", "Preencha este campo com um valor válido");

            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Telefone) && !ValidaTelefone(model.Telefone))
                ModelState.AddModelError("Telefone", "Preencha este campo");

            if (model.dtNascimento > DateTime.Now)
                ModelState.AddModelError("dtNascimento", "Preencha este campo");
            if (char.IsWhiteSpace(model.Sexo) || model.Sexo == '0')
                ModelState.AddModelError("Sexo", "Preencha este campo");

            if (string.IsNullOrEmpty(model.CEP))
                ModelState.AddModelError("CEP", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Rua))
                ModelState.AddModelError("Rua", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Bairro))
                ModelState.AddModelError("Bairro", "Preencha este campo");
            if (model.Numero <= 0)
                ModelState.AddModelError("Numero", "Preencha este campo");


            //Imagem será obrigario apenas na inclusão.
            //Na alteração iremos considerar a que já estava salva.            
            if (model.Imagem == null && operacao == "I")
                ModelState.AddModelError("Imagem", "Escolha uma imagem.");
            if (model.Imagem != null && model.Imagem.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada a 2 mb.");
            if (ModelState.IsValid)
            {
                //na alteração, se não foi informada a imagem, iremos manter a que já estava salva.
                if (operacao == "A" && model.Imagem == null)
                {
                    AlunoViewModel alu = DAO.Consulta(model.Id);
                    model.ImagemEmByte = alu.ImagemEmByte;
                }
                else
                {
                    model.ImagemEmByte = HelperController.ConvertImageToByte(model.Imagem);
                }
            }

            if(operacao == "A" && string.IsNullOrEmpty(model.Senha))
                ModelState.AddModelError("Senha", "Preencha este campo");
        }
    }
}
