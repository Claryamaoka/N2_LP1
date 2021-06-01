using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N2_2BIM.DAO;
using N2_2BIM.Models;
using X.PagedList;

namespace N2_2BIM.Controllers
{
    public abstract class PadraoController<T> : AcessoController where T : PadraoViewModel
    {
        protected PadraoDAO<T> DAO { get; set; }
        protected bool SugereProximoId { get; set; }

        public const int itensPorPagina = 5;

        protected string ViewParaListagem { get; set; } = "Index";
        protected string ViewParaCadastro { get; set; } = "Form";

        public virtual IActionResult Index(int? pagina = null)
        {
            try
            {
                if (HttpContext.Session.GetString("TipoUsuario") == "I")
                    ViewBag.AlunoInstrutor = "I";
                else
                    ViewBag.AlunoInstrutor = "A";

                int numeroPagina = (pagina ?? 1);

                var lista = DAO.Listagem();
                return View(ViewParaListagem, lista.ToPagedList(numeroPagina, itensPorPagina));
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public virtual IActionResult Create(int? id = null)
        {
            ViewBag.Operacao = "I";
            T model = Activator.CreateInstance(typeof(T)) as T;
            PreencheDadosParaView("I", model);
            return View(ViewParaCadastro, model);
        }

        //Apenas no caso de classes que utilizam o campo Id 
        //caso contrário pode ser reescrito 
        protected virtual void PreencheDadosParaView(string Operacao, T model)
        {
            if (SugereProximoId && Operacao == "I")
                model.Id = DAO.ProximoId();
        }

        public virtual IActionResult Salvar(T model, string Operacao)
        {
            try
            {
                ValidaDados(model, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreencheDadosParaView(Operacao, model);
                    return View(ViewParaCadastro, model);
                }
                else
                {
                    if (Operacao == "I")
                        DAO.Insert(model);
                    else
                        DAO.Update(model);
                    return RedirectToAction(ViewParaListagem);
                }
            }
            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                ViewBag.Operacao = Operacao;
                PreencheDadosParaView(Operacao, model);
                return View(ViewParaCadastro, model);
            }
        }

        protected virtual void ValidaDados(T model, string operacao)
        {
            if (operacao == "I" && DAO.Consulta(model.Id) != null)
                ModelState.AddModelError("Id", "Código já está em uso!");
            if (operacao == "A" && DAO.Consulta(model.Id) == null)
                ModelState.AddModelError("Id", "Este registro não existe!");
            if (model.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");

        }

        public virtual IActionResult Edit(int id)
        {
            try
            {
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

        public virtual IActionResult Delete(int id)
        {
            try
            {
                DAO.Delete(id);
                return RedirectToAction(ViewParaListagem);
            }
            catch(Exception erro)
            {
                string errosdfd = erro.Message;
                return RedirectToAction(ViewParaListagem);
                
                
            }
        }

        public static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool ValidaTelefone(string telefone)
        {
            if (telefone.All(char.IsDigit))
                return true;
            else
                return false;
        }

        public void PreencheComboSexo()
        {
            ViewBag.Sexo = new List<SelectListItem>();
            ViewBag.Sexo.Add(new SelectListItem("Selecione um sexo...", "0"));
            ViewBag.Sexo.Add(new SelectListItem("Feminino", "F"));
            ViewBag.Sexo.Add(new SelectListItem("Masculino", "M"));
        }

        /// <summary>
        /// Converte a imagem recebida no form em um vetor de bytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }
    }
}
