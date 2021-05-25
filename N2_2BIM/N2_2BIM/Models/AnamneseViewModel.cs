using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.Models
{
    public class AnamneseViewModel : PadraoViewModel
    {
        public double Peso { get; set; }
        public double Altura { get; set; }
        public string Elasticidade { get; set; }
        public int IdInstrutor { get; set; }
        public int IdAluno { get; set; }

    }
}
/*Id int NOT NULL primary key identity (1,1),
    Peso decimal(10,2),
    Altura decimal(10,2),
    Elasticidade varchar(50)
    CPFInstrutor int foreign key references Instrutores(CPF),
    CPFAluno int foreign key references Alunos(CPF)
*/
