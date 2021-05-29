using N2_2BIM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.DAO
{
    public class AnamneseDAO : PadraoDAO<AnamneseViewModel>
    {
        protected override SqlParameter[] CriaParametros(AnamneseViewModel model, string operacao)
        {
            SqlParameter[] parametros =
           {
                new SqlParameter("IdInstrutor", model.IdInstrutor),
                new SqlParameter("IdAluno", model.IdAluno),
                new SqlParameter("Peso", model.Peso),
                new SqlParameter("Altura", model.Altura),
                new SqlParameter("Elasticidade", model.Elasticidade),
                new SqlParameter("DataAvaliacao", model.DataAvaliacao)

            };

            if (operacao == "A")
                parametros[6] = new SqlParameter("Id", model.Id);

            return parametros;
        }

        protected override AnamneseViewModel MontaModel(DataRow registro)
        {
            var c = new AnamneseViewModel()
            {
                IdInstrutor = Convert.ToInt32(registro["IdInstrutor"]),
                IdAluno = Convert.ToInt32(registro["IdAluno"]),
                Peso = Convert.ToDouble(registro["Peso"]),
                Altura = Convert.ToDouble(registro["Altura"]),
                Elasticidade = registro["Elasticidade"].ToString(),
                DataAvaliacao = Convert.ToDateTime(registro["DataAvaliacao"])
            };
            
            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Anamnese";
        }
    }
}
