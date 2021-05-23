﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.Models
{
    public class RiscoCardiacoViewModel: PadraoViewModel
    {
        public float Peso { get; set; }
        public float Pressao { get; set; }
        public float Colesterol { get; set; }
        public string Atividade { get; set; }//Frequencia de atividade física
        public string Fumo { get; set; } //fuma ou não? Em que quantidade
        public string DoencaFamilia { get; set; } //Possui historico de doenças na família? Quantos casos
        public int CPFInstrutor { get; set; }
        public int CPFAluno { get; set; }
    }
}
/*Id int NOT NULL primary key identity (1,1),
    Peso decimal(10,2),
    Pressao decimal(10,2),
    Colesterol decimal(10,2),
    Atividade varchar(50),
    Fumo varchar(50),
    DoencaFamilia varchar(50),
    CPFInstrutor int foreign key references Instrutores(CPF),
    CPFAluno int foreign key references Alunos(CPF)*/
