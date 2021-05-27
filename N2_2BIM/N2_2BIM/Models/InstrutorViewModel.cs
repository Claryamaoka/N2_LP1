using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.Models
{
    public class InstrutorViewModel : PadraoViewModel
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime dtNascimento { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Telefone { get; set; }
        public char Sexo { get; set; }
        public string Senha { get; set; }

        /// <summary>
        /// Imagem recebida do form pelo controller
        /// </summary>
        public IFormFile Imagem { get; set; }

        /// <summary>
        /// Imagem em bytes pronta para ser salva
        /// </summary>
        public byte[] ImagemEmByte { get; set; }

        /// <summary>
        /// Imagem usada para ser enviada ao form no formato para ser exibida
        /// </summary>
        public string ImagemEmBase64
        {
            get
            {
                if (ImagemEmByte != null)
                    return Convert.ToBase64String(ImagemEmByte);
                else
                    return "";
            }
        }
    }
}
/*CPF int primary key NOT NULL,
    Nome varchar(50), 
    dtNascimento smalldatetime, 
    Endereco varchar(50),
    Telefone varchar(50),
    Sexo char(1),
    Foto varbinary(50)*/

