using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.Models
{
    public class ExercicioViewModel:PadraoViewModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
/*Id int primary key not null IDENTITY(1,1),
	Nome varchar(max),
	Descricao varchar(max)*/
