﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using N2_2BIM.DAO;
using N2_2BIM.Models;

namespace N2_2BIM.Controllers
{
    public class AlunoController : PadraoController<AlunoViewModel>
    {
        public AlunoController()
        {
            DAO = new AlunoDAO();
            SugereProximoId = true;
        }

        protected override void PreencheDadosParaView(string Operacao, AlunoViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);
            PreencheComboSexo();
            //pegar o Id do instrutor que está logado e salvar na model do aluno

            if (Operacao == "I")
            {
                model.IdInstrutor = (int)HttpContext.Session.GetInt32("IdUsuario");
                model.Senha = "senha123";
            }

        }

        //Consultar pelo Id do Instrutor para listar 
        //public override IActionResult Index(int? pagina = null)
        //{
        //    int id = (int)HttpContext.Session.GetInt32("IdUsuario");
        //    var lista = DAO.Consulta(id);
        //    return View(ViewParaListagem, lista);
        //}

        protected override void ValidaDados(AlunoViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (!ValidaCPF(model.CPF))
                ModelState.AddModelError("CPF", "Preencha este campo com um valor válido");

            //fazer consulta para ver se o CPF já foi cadastrado

            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Preencha este campo");
            if (string.IsNullOrEmpty(model.Telefone))
                ModelState.AddModelError("Telefone", "Preencha este campo");

            if (model.dtNascimento > DateTime.Now)
                ModelState.AddModelError("dtNascimento", "Preencha este campo");
            if (char.IsWhiteSpace(model.Sexo))
                ModelState.AddModelError("Sexo", "Preencha este campo");

            if(string.IsNullOrEmpty(model.CEP))
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
