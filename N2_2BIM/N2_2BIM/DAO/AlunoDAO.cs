using N2_2BIM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.DAO
{
    public class AlunoDAO : PadraoDAO<AlunoViewModel>
    {
        protected override SqlParameter[] CriaParametros(AlunoViewModel model, string operacao)
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
                new SqlParameter("Id", model.Id),
                new SqlParameter("CPF", model.CPF),
                new SqlParameter("Nome", model.Nome),
                new SqlParameter("Foto", imgByte),
                new SqlParameter("dtNascimento", model.dtNascimento),
                new SqlParameter("CEP", model.CEP),
                new SqlParameter("Rua", model.Rua),
                new SqlParameter("Bairro", model.Bairro),
                new SqlParameter("Complemento", comp),
                new SqlParameter("Numero", model.Numero),
                new SqlParameter("Telefone", model.Telefone),
                new SqlParameter("Sexo", model.Sexo),
                new SqlParameter("Instrutor", model.IdInstrutor),
                new SqlParameter("Senha", model.Senha)
                 
            };

            return parametros;
        }

        protected override AlunoViewModel MontaModel(DataRow registro)
        {
            var c = new AlunoViewModel()
            {
                Id = Convert.ToInt32(registro["Id"]),
                CPF = registro["CPF"].ToString(),
                Nome = registro["Nome"].ToString(),
                dtNascimento = Convert.ToDateTime(registro["dtNascimento"]),
                CEP = registro["CEP"].ToString(),
                Rua = registro["Rua"].ToString(),
                Bairro = registro["Bairro"].ToString(),
                Telefone = registro["Telefone"].ToString(),
                Sexo = Convert.ToChar(registro["Sexo"]),
                IdInstrutor = Convert.ToInt32(registro["IdInstrutor"]),
                Senha = registro["Senha"].ToString(),
                Numero = Convert.ToInt32(registro["Numero"])
            };

            if (registro["Foto"] != DBNull.Value)
                c.ImagemEmByte = registro["Foto"] as byte[];

            if (registro["Complemento"] != DBNull.Value)
                c.Complemento = registro["Complemento"].ToString();
            else
                c.Complemento = null;

            return c;
        }


        protected override void SetTabela()
        {
            Tabela = "Alunos";
        }
    }
}
