using N2_2BIM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.DAO
{
    public class LoginDAO : PadraoDAO<LoginViewModel>
    {
        protected override SqlParameter[] CriaParametros(LoginViewModel model)
        {
            SqlParameter[] parametros =
        {
                new SqlParameter("Id", model.Id),
                new SqlParameter("senha", model.senha),
                new SqlParameter("Tipo", model.Tipo)
            };

            return parametros;
        }

        protected override LoginViewModel MontaModel(DataRow registro)
        {
            var c = new LoginViewModel()
            {
                Id = Convert.ToInt32(registro["Id"]),
                senha = registro["senha"].ToString(),
                Tipo  = Convert.ToChar(registro["Tipo"])
            };
            return c;
        }

        public override LoginViewModel Consulta(string id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("Id", id),
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsulta", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        protected override void SetTabela()
        {
            Tabela = "Login";
        }
    }
}
