using N2_2BIM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.DAO
{
    public class InstrutorDAO : PadraoDAO<InstrutorViewModel>
    {
        protected override SqlParameter[] CriaParametros(InstrutorViewModel model, string operacao)
        {
            object imgByte = model.ImagemEmByte;
            if (imgByte == null)
                imgByte = new byte[0];  //DBNull.Value;

            object comp;
            if (model.Complemento == null)
                comp = DBNull.Value;
            else
                comp = model.Complemento;

            SqlParameter[] parametros =
            {
                new SqlParameter("CPF", model.CPF),
                new SqlParameter("Nome", model.Nome),
                new SqlParameter("Foto", imgByte),
                new SqlParameter("dtNascimento", model.dtNascimento),
                new SqlParameter("CEP", model.CEP),
                new SqlParameter("Rua", model.Rua),
                new SqlParameter("Bairro", model.Bairro),
                new SqlParameter("Numero", model.Numero),
                new SqlParameter("Complemento", comp),
                new SqlParameter("Telefone", model.Telefone),
                new SqlParameter("Sexo", model.Sexo),
                new SqlParameter("Senha", model.Senha)
            };

            if (operacao == "A")
                parametros[12] = new SqlParameter("Id", model.Id);

            return parametros;
        }

        protected override InstrutorViewModel MontaModel(DataRow registro)
        {
            var c = new InstrutorViewModel()
            {
                Id = Convert.ToInt32(registro["Id"]),
                CPF = registro["CPF"].ToString(),
                Nome = registro["Nome"].ToString(),
                dtNascimento = Convert.ToDateTime(registro["dtNascimento"]),
                CEP = registro["CEP"].ToString(),
                Rua = registro["Rua"].ToString(),
                Bairro = registro["Bairro"].ToString(),
                Numero = Convert.ToInt32(registro["Numero"]),
                Telefone = registro["Telefone"].ToString(),
                Senha = registro["Senha"].ToString(),
                Sexo = Convert.ToChar(registro["Sexo"])                
            };

            if (registro["Foto"] != DBNull.Value)
                c.ImagemEmByte = registro["imagem"] as byte[];

            if (registro["Complemento"] != DBNull.Value)
                c.Complemento = registro["Complemento"].ToString();
            else
                c.Complemento = null;

            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Instrutores";
        }
    }
}
