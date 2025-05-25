using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                if (articulo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No se recibio articulo");
                }

                string camposVacios = "";
                if (string.IsNullOrWhiteSpace(articulo.Codigo))
                    camposVacios += "| Codigo: No puede estar vacio. |";

                if (string.IsNullOrWhiteSpace(articulo.Nombre))
                    camposVacios += "| Nombre: No puede estar vacio. |";

                if (string.IsNullOrWhiteSpace(articulo.Descripcion))
                    camposVacios += "| Descripcion: No puede estar vacio. |";

                Marca marca = marcaNegocio.Listar().Find(x => x.Id == articulo.Marca);
                Categoria categoria = categoriaNegocio.Listar().Find(x => x.Id == articulo.Categoria);

                if (marca == null)
                {
                    camposVacios += "| Marca: Marca inexistente. |";
                }
                if (categoria == null)
                {
                    camposVacios += "| Categoria: Categoria inexistente. |";
                }

                if(articulo.Precio <= 0)
                {
                    camposVacios += "| Precio invalido: debe ser un valor mayor a 0. |";
                }

                if (camposVacios.Length > 0) 
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, camposVacios);
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
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Ocurrio un error al agregar el articulo.");
            }
        }

        // AGREGAR IMÁGENES
        // POST: api/Articulo/id
        public HttpResponseMessage Post(int id, [FromBody] List<string> imagenes)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> lista = negocio.Listar();

                Articulo encontrado = lista.Find(x => x.Id == id);

                if (encontrado == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "El articulo no existe.");
                }

                if (imagenes == null || imagenes.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No se recibieron imagenes.");
                }

                // Validacion de URLs
                string patronUrl = @"^(https?:\/\/)[\w\-]+(\.[\w\-]+)+[/#?]?.*$";
                foreach (string url in imagenes)
                {
                    // Si alguna esta vacia
                    if (string.IsNullOrWhiteSpace(url))
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Una o mas URLs estan vacias.");
                    }

                    // Si difiera del patron
                    if (!Regex.IsMatch(url, patronUrl))
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Una o mas URLs no son validas: " + url);
                    }
                }

                negocio.AgregarImagenes(id, imagenes);

                return Request.CreateResponse(HttpStatusCode.OK, "Imagenes agregadas correctamente al articulo.");
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Ocurrio un error al agregar las imagenes.");
            }
        }


        // MODIFICAR ARTÍCULO
        // PUT: api/Articulo/5
        public HttpResponseMessage Put(int id, [FromBody] ArticuloDto articulo)
        {
            try
            {
                if (articulo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No se recibieron las modificaciones a realizar.");
                }

                ArticuloNegocio negocio = new ArticuloNegocio();
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                List<Articulo> lista = negocio.Listar();
                Articulo nuevo = lista.Find(x => x.Id == id);

                if (nuevo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "El articulo no existe.");
                }

                string camposVacios = "";
                if (string.IsNullOrWhiteSpace(articulo.Codigo))
                    camposVacios += "| Codigo: No puede estar vacio. |";

                if (string.IsNullOrWhiteSpace(articulo.Nombre))
                    camposVacios += "| Nombre: No puede estar vacio. |";

                if (string.IsNullOrWhiteSpace(articulo.Descripcion))
                    camposVacios += "| Descripcion: No puede estar vacio. |";

                Marca marca = marcaNegocio.Listar().Find(x => x.Id == articulo.Marca);
                Categoria categoria = categoriaNegocio.Listar().Find(x => x.Id == articulo.Categoria);

                /*if (marca == null && categoria == null)
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

                if (articulo.Precio <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Precio invalido: debe ser un valor mayot a 0.");
                }*/

                if (marca == null)
                {
                    camposVacios += "| Marca: Marca inexistente. |";
                }
                if (categoria == null)
                {
                    camposVacios += "| Categoria: Categoria inexistente. |";
                }

                if (articulo.Precio <= 0)
                {
                    camposVacios += "| Precio invalido: debe ser un valor mayor a 0. |";
                }

                if (camposVacios.Length > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, camposVacios);
                }

                nuevo.Codigo = articulo.Codigo;
                nuevo.Nombre = articulo.Nombre;
                nuevo.Descripcion = articulo.Descripcion;
                nuevo.Marca = new Marca { Id = articulo.Marca };
                nuevo.Categoria = new Categoria { Id = articulo.Categoria };
                nuevo.Precio = articulo.Precio;

                negocio.modificarArticulo(nuevo);

                return Request.CreateResponse(HttpStatusCode.OK, "El articulo fue modificado correctamente.");
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Ocurrio un error al modificar el articulo.");
            }
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
