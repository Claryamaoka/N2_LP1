using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.Models
{
    public class LoginViewModel: PadraoViewModel
    {
        public string senha { get; set; }
        public char Tipo { get; set; }
    }
}
/* Id int primary key NOT NULL,
    senha varchar(50) NOT NULL,
	Tipo char(1)*/
