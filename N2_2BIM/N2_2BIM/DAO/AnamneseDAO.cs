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
        protected override SqlParameter[] CriaParametros(AnamneseViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override AnamneseViewModel MontaModel(DataRow registro)
        {
            throw new NotImplementedException();
        }

        protected override void SetTabela()
        {
            Tabela = "Anamnese";
        }
    }
}
