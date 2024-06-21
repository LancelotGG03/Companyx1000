using CapaEntidad;
using CapaNegocio;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capapresentacionadmin.Controllers
{
    public class MNTController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Plato()
        {
            return View();
        }
        //+++++++++++++++++++ CATEGORIA ++++++++++++++++++++++//
        #region Categoria
        [HttpGet]
        public JsonResult ListarCategoria()
        {
            List<Categoria> oLista = new List<Categoria>();

            oLista = new CN_Categoria().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {
                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //+++++++++++++++++++ PLATO ++++++++++++++++++++++//

        #region Plato

        [HttpGet]
        public JsonResult ListarPlato()
        {
            List<Plato> oLista = new List<Plato>();

            oLista = new CN_Plato().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarPlato(string objeto, HttpPostedFileBase archivoImagen)
        {
           
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Plato oPlato = new Plato();
            oPlato = JsonConvert.DeserializeObject<Plato>(objeto);

            decimal Precio;
            if (decimal.TryParse(oPlato.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-CO"), out Precio))
            {
                oPlato.Precio = Precio;
            }
            else
            {
                return Json(new { operacion_Exitosa = false, mensaje = "El formato del precio debe ser ##.###", JsonRequestBehavior.AllowGet });
            }



            if (oPlato.IdPlato == 0)
            {
                int IdProductoGenerado = new CN_Plato().Registrar(oPlato, out mensaje);

                if (IdProductoGenerado != 0)
                {
                    oPlato.IdPlato = IdProductoGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }


            }
            else
            {
                operacion_exitosa = new CN_Plato().Editar(oPlato, out mensaje);
            }

            if (operacion_exitosa)
            {
                if (archivoImagen != null)
                {
                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oPlato.IdPlato.ToString(), extension);

                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardar_imagen_exito = false;
                    }

                    if (guardar_imagen_exito)
                    {
                        oPlato.Rutaimagen = ruta_guardar;
                        oPlato.Nombreimagen = nombre_imagen;
                        bool respuesta = new CN_Plato().GuardarDatosImagen(oPlato, out mensaje);
                    }
                    else
                    {
                        mensaje = "El plato fue guardado pero hubo problemas con la imagen";
                    }
                }
            }

            return Json(new { operacion_Exitosa = operacion_exitosa, idGenerado = oPlato.IdPlato, mensaje = mensaje, JsonRequestBehavior.AllowGet });
            
        }

        [HttpPost]

        public JsonResult ImagenProducto(int id )
        {
            bool conversion;
            Plato oplato = new CN_Plato().Listar().Where(p => p.IdPlato == id).FirstOrDefault();

            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oplato.Rutaimagen,oplato.Nombreimagen), out conversion);

            return Json(new
            {
                conversion = conversion,
                textoBase64 = textoBase64,
                extension = Path.GetExtension(oplato.Nombreimagen)

            },
            
            JsonRequestBehavior.AllowGet
            
            );

        }

        [HttpPost]
        public JsonResult EliminarPlato(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Plato().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
