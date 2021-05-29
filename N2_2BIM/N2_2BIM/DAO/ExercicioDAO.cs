using N2_2BIM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.DAO
{
    public class ExercicioDAO : PadraoDAO<ExercicioViewModel>
    {
        protected override SqlParameter[] CriaParametros(ExercicioViewModel model, string operacao)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("Descricao", model.Descricao);
            parametros[1] = new SqlParameter("Nome", model.Nome);
            parametros[2] = new SqlParameter("Id", model.Id);

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("Descricao", model.Descricao);
            param[1] = new SqlParameter("Nome", model.Nome);

            if (operacao == "A")
                return parametros;
            else
                return param;
        }

        protected override ExercicioViewModel MontaModel(DataRow registro)
        {
            var c = new ExercicioViewModel()
            {
                Nome = registro["Nome"].ToString(),
                Descricao = registro["Descricao"].ToString(),
                Id = Convert.ToInt32(registro["Id"])
            };

            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Exercicio";
        }
    }
}
