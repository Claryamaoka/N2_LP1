using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N2_2BIM.Models
{
    public class PadraoViewModel
    {
        //nenhum campo é de preenchimento obrigatório(pode ser null)
        //algumas classes utilizam apenas o id enquanto outras utilizam apenas o CPF
        public int? Id { get; set; }

        public int? CPF { get; set; }
    }
}
