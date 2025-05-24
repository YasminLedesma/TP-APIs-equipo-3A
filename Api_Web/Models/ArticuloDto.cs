using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Web.Models
{
    public class ArticuloDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Marca { get; set; }
        public int Categoria { get; set; }
        public decimal Precio { get; set; }
    }
}