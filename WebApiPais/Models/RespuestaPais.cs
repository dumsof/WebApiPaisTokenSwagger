using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPais.Models
{
    public class RespuestaPais : Respuesta
    {
        public IEnumerable<Pais> Pais { get; set; }
    }
}
