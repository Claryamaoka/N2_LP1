using N2_2BIM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.DAO
{
    public class RiscoCardiacoDAO : PadraoDAO<RiscoCardiacoViewModel>
    {
        protected override SqlParameter[] CriaParametros(RiscoCardiacoViewModel model)
        {
            SqlParameter[] parametros =
         {
                new SqlParameter("CPF", model.CPFInstrutor),
                new SqlParameter("CPF", model.CPFAluno),
                new SqlParameter("Peso", model.Peso),
                new SqlParameter("Pressao", model.Pressao),
                new SqlParameter("Colesterol", model.Colesterol),
                new SqlParameter("Atividade", model.Atividade),
                new SqlParameter("Fumo", model.Fumo),
                new SqlParameter("DoencaFamilia", model.DoencaFamilia)

            };

            return parametros;
        }

        protected override RiscoCardiacoViewModel MontaModel(DataRow registro)
        {
            var c = new RiscoCardiacoViewModel()
            {
                CPFInstrutor = Convert.ToInt32(registro["CPF"]),
                CPFAluno = Convert.ToInt32(registro["CPF"]),
                Peso = Convert.ToDouble(registro["Peso"]),
                Pressao = Convert.ToDouble(registro["Pressao"]),
                Colesterol = Convert.ToDouble(registro["Colesterol"]),
                Atividade = registro["Atividade"].ToString(),
                Fumo = registro["Fumo"].ToString(),
                DoencaFamilia = registro["DoencaFamilia"].ToString()

            };

            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Cardiaco";
        }
    }
}
