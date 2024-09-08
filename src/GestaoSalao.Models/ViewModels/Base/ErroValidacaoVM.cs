using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoSalao.Models.ViewModels.Base
{
    public class ErroValidacaoVM
    {
        public int StatusCode { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public List<string> Detailed { get; set; }
    }
}
