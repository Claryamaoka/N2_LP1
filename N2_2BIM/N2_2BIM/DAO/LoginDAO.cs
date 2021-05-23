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
                new SqlParameter("CPF", model.CPF),
                new SqlParameter("senha", model.senha),
                new SqlParameter("Tipo", model.Tipo),
                new SqlParameter("Id", model.Id)
            };

            return parametros;
        }

        protected override LoginViewModel MontaModel(DataRow registro)
        {
            var c = new LoginViewModel()
            {
                Id = Convert.ToInt32(registro["Id"]),
                CPF = registro["CPF"].ToString(),
                senha = registro["senha"].ToString(),
                Tipo  = Convert.ToChar(registro["Tipo"])
            };
            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Login";
        }
    }
}
