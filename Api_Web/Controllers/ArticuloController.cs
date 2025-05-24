using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Dominio;
using Negocio;

namespace Api_Web.Models
{
    public class ArticuloController : ApiController
    {
        // GET: api/Articulo
        public HttpResponseMessage Get()
        {
            List<Articulo> lista = new List<Articulo>();
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                lista = negocio.Listar();

                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }
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

        // ALTA ARTICULO
        // POST: api/Articulo
        public HttpResponseMessage Post([FromBody] ArticuloDto articulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            Marca marca = marcaNegocio.Listar().Find(x => x.Id == articulo.Marca);
            Categoria categoria = categoriaNegocio.Listar().Find(x => x.Id == articulo.Categoria);

            if (marca == null && categoria == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Marca y Categoria inexistentes.");
            }
            else if (marca == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Marca inexistente.");
            }
            else if (categoria == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Categoria inexistente.");
            }

            Articulo nuevo = new Articulo();
            nuevo.Codigo = articulo.Codigo;
            nuevo.Nombre = articulo.Nombre;
            nuevo.Descripcion = articulo.Descripcion;
            nuevo.Marca = new Marca { Id = articulo.Marca };
            nuevo.Categoria = new Categoria { Id = articulo.Categoria };
            nuevo.Precio = articulo.Precio;

            negocio.AgregarArticulo(nuevo);

            return Request.CreateResponse(HttpStatusCode.OK, "Articulo agregado correctamente.");
        }

        // AGREGAR IMAGENES
        // POST: api/Articulo/id
        public HttpResponseMessage Post(int id, [FromBody] List<string> imagenes)
        {

            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> lista = negocio.Listar();

            Articulo encontrado = lista.Find(x => x.Id == id);

            if (encontrado == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "El artículo no existe");
            }

            if (imagenes == null || imagenes.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No se recibieron Imagenes ");
            }

            negocio.AgregarImagenes(id, imagenes);

            return Request.CreateResponse(HttpStatusCode.OK, "Imagenes agregadas correctamente al articulo.");
        }

        // MODIFICAR ARTICULO
        // PUT: api/Articulo/5
        public HttpResponseMessage Put(int id, [FromBody] ArticuloDto articulo)
        {


            ArticuloNegocio negocio = new ArticuloNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            List<Articulo> lista = negocio.Listar();
            Articulo nuevo = lista.Find(x => x.Id == id);

            if (nuevo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "El artículo no existe");
            }

            Marca marca = marcaNegocio.Listar().Find(x => x.Id == articulo.Marca);
            Categoria categoria = categoriaNegocio.Listar().Find(x => x.Id == articulo.Categoria);

            if (articulo != null)
            {
                if (marca == null && categoria == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Marca y Categoría inexistentes.");
                }
                else if (marca == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Marca inexistente.");
                }
                else if (categoria == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Categoría inexistente.");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No se recibieron las modificaciones a realizar.");
            }

            nuevo.Codigo = articulo.Codigo;
            nuevo.Nombre = articulo.Nombre;
            nuevo.Descripcion = articulo.Descripcion;
            nuevo.Marca = new Marca { Id = articulo.Marca };
            nuevo.Categoria = new Categoria { Id = articulo.Categoria };
            nuevo.Precio = articulo.Precio;
            nuevo.Id = id;

            negocio.modificarArticulo(nuevo);

            return Request.CreateResponse(HttpStatusCode.OK, "El artículo modificado correctamente");
        }

        // DELETE: api/Articulo/5
        public HttpResponseMessage Delete(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            Articulo articulo = negocio.Listar().Find(x => x.Id == id);

            if(articulo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "El artículo no existe");
            }

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
