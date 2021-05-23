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
            throw new NotImplementedException();
        }

        protected override InstrutorViewModel MontaModel(DataRow registro)
        {
            throw new NotImplementedException();
        }

        protected override void SetTabela()
        {
            Tabela = "Instrutores";
        }
    }
}
