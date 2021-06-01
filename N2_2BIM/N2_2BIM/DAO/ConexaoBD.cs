using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.DAO
{
    public class ConexaoBD
    {
        public static SqlConnection GetConexao()
        {
            //string strCon = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=MoveUp;integrated security=true";
            //string strCon = "Data Source=LAPTOP-9EADM12E; Database=MoveUp; integrated security=true";
            string strCon = "Data Source=GUSTAVO-PC;Initial Catalog=MoveUp;integrated security=true";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
