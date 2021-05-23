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
        protected override SqlParameter[] CriaParametros(AlunoViewModel model)
        {
            object imgByte = model.ImagemEmByte;
            if (imgByte == null)
                imgByte = new byte[0];  //DBNull.Value;

            SqlParameter[] parametros =
            {
                new SqlParameter("CPF", model.CPF),
                new SqlParameter("Nome", model.Nome),
                new SqlParameter("Foto", imgByte),
                new SqlParameter("dtNascimento", model.dtNascimento),
                new SqlParameter("Endereco", model.Endereco),
                new SqlParameter("Telefone", model.Telefone),
                new SqlParameter("Sexo", model.Sexo),
                new SqlParameter("CPFInstrutor", model.CPFInstrutor)            
            };

            return parametros;
        }

        protected override AlunoViewModel MontaModel(DataRow registro)
        {
            var c = new AlunoViewModel()
            {
                CPF = Convert.ToInt32(registro["CPF"]),
                Nome = registro["Nome"].ToString(),
                dtNascimento = Convert.ToDateTime(registro["dtNascimento"]),
                Endereco = registro["Endereco"].ToString(),
                Telefone = registro["Telefone"].ToString(),
                Sexo = Convert.ToChar(registro["Sexo"]),
                CPFInstrutor = Convert.ToInt32(registro["CPFInstrutor"])
            };

            if (registro["Foto"] != DBNull.Value)
                c.ImagemEmByte = registro["imagem"] as byte[];

            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Alunos";
        }
    }
}
