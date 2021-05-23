using N2_2BIM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.DAO
{
    public class AulaDAO : PadraoDAO<AulaViewModel>
    {
        protected override SqlParameter[] CriaParametros(AulaViewModel model)
        {
            SqlParameter[] parametros =
         {
                new SqlParameter("CPF", model.CPFInstrutor),
                new SqlParameter("CPF", model.CPFAluno),
                new SqlParameter("Ex1", model.Ex1),
                new SqlParameter("Ex2", model.Ex2),
                new SqlParameter("Ex3", model.Ex3),
                new SqlParameter("dataAula", model.dataAula)

            };

            return parametros;
        }

        protected override AulaViewModel MontaModel(DataRow registro)
        {
            var c = new AulaViewModel()
            {
                CPFInstrutor = Convert.ToInt32(registro["CPF"]),
                CPFAluno = Convert.ToInt32(registro["CPF"]),
                Ex1 = Convert.ToInt32(registro["Ex1"]),
                Ex2 = Convert.ToInt32(registro["Ex2"]),
                Ex3 = Convert.ToInt32(registro["Ex3"]),
                dataAula = Convert.ToDateTime(registro["dataAula"])
            };

            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Aulas";
        }
    }
}
