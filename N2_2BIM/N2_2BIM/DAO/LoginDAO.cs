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
                

            };

            return parametros;
        }

        protected override LoginViewModel MontaModel(DataRow registro)
        {
            var c = new LoginViewModel()
            {
                CPF = Convert.ToInt32(registro["CPF"]),
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
