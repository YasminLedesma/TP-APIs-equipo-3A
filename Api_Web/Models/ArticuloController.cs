using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dominio;
using Negocio

namespace Api_Web.Models
{
    public class ArticuloController : ApiController
    {
        // GET: api/Articulo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Articulo/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> lista = new List<Articulo>();

                lista = negocio.Listar();

                Articulo encontrado = lista.Find(x => x.Id == id);

                if (encontrado == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "El artículo no existe");
                }

                return Request.CreateResponse(HttpStatusCode.OK, encontrado);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error al procesar la informacion");

            }
        }

        // POST: api/Articulo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Articulo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Articulo/5
        public HttpResponseMessage Delete(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                negocio.eliminarArticulo(id);

                return Request.CreateResponse(HttpStatusCode.OK, "Articulo eliminado correctamente.");
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, "El id no existe.");
            }
        }
    }
}
