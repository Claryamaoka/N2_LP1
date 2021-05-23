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
            throw new NotImplementedException();
        }

        protected override AulaViewModel MontaModel(DataRow registro)
        {
            throw new NotImplementedException();
        }

        protected override void SetTabela()
        {
            Tabela = "Aulas";
        }
    }
}
