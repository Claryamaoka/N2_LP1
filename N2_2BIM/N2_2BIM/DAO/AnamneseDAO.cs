﻿using N2_2BIM.Models;
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
            SqlParameter[] parametros =
           {
                new SqlParameter("CPF", model.CPFInstrutor),
                new SqlParameter("CPF", model.CPFAluno),
                new SqlParameter("Peso", model.Peso),
                new SqlParameter("Altura", model.Altura),
                new SqlParameter("Elasticidade", model.Elasticidade)
               
            };

            return parametros;
        }

        protected override AnamneseViewModel MontaModel(DataRow registro)
        {
            var c = new AnamneseViewModel()
            {
                CPFInstrutor = Convert.ToInt32(registro["CPF"]),
                CPFAluno = Convert.ToInt32(registro["CPF"]),
                Peso = Convert.ToDouble(registro["Peso"]),
                Altura = Convert.ToDouble(registro["Altura"]),
                Elasticidade = registro["Elasticidade"].ToString()

            };
            
            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Anamnese";
        }
    }
}
