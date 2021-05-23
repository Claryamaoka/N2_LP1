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
        protected override SqlParameter[] CriaParametros(InstrutorViewModel model)
        {
            object imgByte = model.ImagemEmByte;
            if (imgByte == null)
                imgByte = new byte[0];  //DBNull.Value;

            SqlParameter[] parametros =
            {
                new SqlParameter("CPF", model.CPF),
                new SqlParameter("Id", model.Id),
                new SqlParameter("Nome", model.Nome),
                new SqlParameter("Foto", imgByte),
                new SqlParameter("dtNascimento", model.dtNascimento),
                new SqlParameter("Endereco", model.Endereco),
                new SqlParameter("Telefone", model.Telefone),
                new SqlParameter("Sexo", model.Sexo),                
            };

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
                Endereco = registro["Endereco"].ToString(),
                Telefone = registro["Telefone"].ToString(),
                Sexo = Convert.ToChar(registro["Sexo"])                
            };

            if (registro["Foto"] != DBNull.Value)
                c.ImagemEmByte = registro["imagem"] as byte[];

            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Instrutores";
        }
    }
}
