using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.Models
{
    public class AulaViewModel: PadraoViewModel
    {
        public int IdInstrutor { get; set; }
        public int IdAluno { get; set; }

        //apenas os IDS dos exercicios, não pode haver repetição
        public int Ex1 { get; set; }
        public int Ex2 { get; set; }
        public int Ex3 { get; set; }
        public DateTime dataAula { get; set; }

        //Nome do aluno para exibição
        public string NomeAluno { get; set; }
        //Nome dos Exercicios para exibição
        public string NomeExercicio1 { get; set; }
        public string NomeExercicio2 { get; set; }
        public string NomeExercicio3 { get; set; }
    }
}
/*Id int primary key not null IDENTITY(1,1),
	CPFInstrutor int foreign key references Instrutores(CPF),
	CPFAluno int foreign key references Alunos(CPF),
	Ex1 int foreign key references Exercicio(Id),
	Ex2 int foreign key references Exercicio(Id),
	Ex3 int foreign key references Exercicio(Id)*/
