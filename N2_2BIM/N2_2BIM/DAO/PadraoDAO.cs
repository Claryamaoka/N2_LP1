using N2_2BIM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.DAO
{
    public abstract class PadraoDAO<T> where T : PadraoViewModel
    {
        protected PadraoDAO()
        {
            SetTabela();
        }

        protected string NomeProcedureListagem { get; set; } = "spListagem";

        protected string Tabela { get; set; }
        protected abstract SqlParameter[] CriaParametros(T model,string operacao);
        protected abstract T MontaModel(DataRow registro);
        protected abstract void SetTabela();
        protected bool ChaveIdentity { get; set; } = true;

        //retorna ultimo id inserido
        public virtual int Insert(T model)
        {
            string operacao = "I";
            int ultimoId = HelperDAO.ExecutaProc("spInsert_" + Tabela, CriaParametros(model,operacao), ChaveIdentity);
            return ultimoId;
        }

        public virtual void Update(T model)
        {
            string operacao = "A";
            HelperDAO.ExecutaProc("spUpdate_" + Tabela, CriaParametros(model,operacao));
        }


        public virtual void Delete(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Tabela)
            };
            HelperDAO.ExecutaProc("spDelete", p);
        }

        public virtual T Consulta(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsulta", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public virtual int ProximoId()
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spProximoId", p);
            return Convert.ToInt32(tabela.Rows[0][0]);
        }

        public virtual List<T> Listagem()
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect(NomeProcedureListagem, p);
            List<T> lista = new List<T>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaModel(registro));
            }
            return lista;
        }

        public virtual List<T> ConsultaDiferenciada(int id, string nomeSp)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect(nomeSp, p);
            List<T> lista = new List<T>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaModel(registro));
            }
            return lista;
        }

        
    }
}
