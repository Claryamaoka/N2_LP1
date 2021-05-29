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
        protected override SqlParameter[] CriaParametros(AulaViewModel model, string operacao)
        {
            object comp;
            if (model.Ex3 == null || model.Ex3 == 0)
                comp = DBNull.Value;
            else
                comp = model.Ex3;

            SqlParameter[] parametros =
            {
                new SqlParameter("Id", model.Id),
                new SqlParameter("IdInstrutor", model.IdInstrutor),
                new SqlParameter("IdAluno", model.IdAluno),
                new SqlParameter("Ex1", model.Ex1),
                new SqlParameter("Ex2", model.Ex2),
                new SqlParameter("Ex3", comp),
                new SqlParameter("dataAula", model.dataAula)

            };

            return parametros;
        }

        protected override AulaViewModel MontaModel(DataRow registro)
        {
            var c = new AulaViewModel()
            {
                Id = Convert.ToInt32(registro["Id"]),
                IdInstrutor = Convert.ToInt32(registro["IdInstrutor"]),
                IdAluno = Convert.ToInt32(registro["IdAluno"]),
                Ex1 = Convert.ToInt32(registro["Ex1"]),
                Ex2 = Convert.ToInt32(registro["Ex2"]),
                dataAula = Convert.ToDateTime(registro["dataAula"])
            };

            if (registro["Ex3"] != DBNull.Value)
                c.Ex3 = Convert.ToInt32(registro["Ex3"]);
            else
                c.Ex3 = 0;

            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Aulas";
        }
    }
}
