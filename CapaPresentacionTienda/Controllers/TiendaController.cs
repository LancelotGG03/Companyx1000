using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            lista = new CN_Categoria().Listar();

            return Json(new {data=lista},JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ListarPlatoporCategoria(int idcategoria)
        {
            List<Plato> lista = new List<Plato>();
            lista = new CN_Plato().ListarPlatoporCategoria(idcategoria);

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ListarPlatos(int idcategoria)
        {
            List<Plato> lista = new List<Plato>();

            bool conversion;

            lista = new CN_Plato().Listar().Select(P => new Plato()
            {
                IdPlato = P.IdPlato,
                Nombreplato = P.Nombreplato,
                Descripcion = P.Descripcion,
                oCategoria = P.oCategoria,
                Precio = P.Precio,
                Rutaimagen = P.Rutaimagen,
                Base64 = CN_Recursos.ConvertirBase64(Path.Combine(P.Rutaimagen, P.Nombreimagen), out conversion),
                Extension = Path.GetExtension(P.Nombreimagen),
                Activo = P.Activo
            }).Where(p =>
                p.oCategoria.IdCategoria == (idcategoria == 0 ? p.oCategoria.IdCategoria : idcategoria)
            ).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);

        }
    }
}