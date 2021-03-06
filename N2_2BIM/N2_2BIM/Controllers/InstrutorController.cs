using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using N2_2BIM.DAO;
using N2_2BIM.Models;
using Microsoft.AspNetCore.Http;

namespace N2_2BIM.Controllers
{
    public class InstrutorController : PadraoController<InstrutorViewModel>
    {
        public InstrutorController()
        {
            DAO = new InstrutorDAO();
            SugereProximoId = true;
            
        }

        public override IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Cadastro = false;
                //Como é o instrutor qm está editando seu usuário o 
                //id não é passado por URL mas sim pela Session
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

        //SOBREESCREVER - Não pode validar o login se a pessoa estiver apenas cadastrando 
        public override void OnActionExecuting(ActionExecutingContext context)
        {   
            ViewBag.Logado = true;
            ViewBag.Cadastro = true;
        }

        public override IActionResult Index(int? pagina)
        {
            if(ViewBag.Operacao == "A")
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction("Index", "Login");
        }

        protected override void PreencheDadosParaView(string Operacao, InstrutorViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);
            PreencheComboSexo();
        }

        protected override void ValidaDados(InstrutorViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            //fazer consulta para ver se o CPF já foi cadastrado
            if (string.IsNullOrEmpty(model.CPF))
                ModelState.AddModelError("CPF", "Preencha este campo");
            else if (!ValidaCPF(model.CPF))
                ModelState.AddModelError("CPF", "Preencha este campo com um valor válido");

            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Telefone) && !ValidaTelefone(model.Telefone))
                ModelState.AddModelError("Telefone", "Preencha este campo");

            if (model.dtNascimento > DateTime.Now || model.dtNascimento.Year == DateTime.Now.Year)
                ModelState.AddModelError("dtNascimento", "Preencha este campo corretamente");
            if (char.IsWhiteSpace(model.Sexo) || model.Sexo == '0')
                ModelState.AddModelError("Sexo", "Preencha este campo");

            if(string.IsNullOrEmpty(model.CEP))
                ModelState.AddModelError("CEP", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Rua))
                ModelState.AddModelError("Rua", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Bairro))
                ModelState.AddModelError("Bairro", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Cidade))
                ModelState.AddModelError("Cidade", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Estado))
                ModelState.AddModelError("Estado", "Preencha este campo");
            if (model.Numero <= 0)
                ModelState.AddModelError("CEP", "Preencha este campo");

            //Imagem será obrigatorio apenas na inclusão.
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
                    InstrutorViewModel alu = DAO.Consulta(model.Id);
                    model.ImagemEmByte = alu.ImagemEmByte;
                }
                else
                {
                    model.ImagemEmByte = HelperController.ConvertImageToByte(model.Imagem);
                }
            }

        }
    }
}
