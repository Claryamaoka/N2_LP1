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
            SqlParameter[] parametros =
            {
                new SqlParameter("Id", model.Id),
                new SqlParameter("Nome", model.Nome),
                new SqlParameter("Descricao", model.Descricao)
            };

            return parametros;
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

        public override ExercicioViewModel Consulta(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsultaExercicio", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        protected override void SetTabela()
        {
            Tabela = "Exercicio";
        }
    }
}
