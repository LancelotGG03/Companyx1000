using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Plato
    {

        private CD_Plato objCapaDato = new CD_Plato();

        public List<Plato> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Plato obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombreplato) || string.IsNullOrWhiteSpace(obj.Nombreplato))
            {
                Mensaje = "El nombre del plato no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Ingredientes) || string.IsNullOrWhiteSpace(obj.Ingredientes))
            {
                Mensaje = "Los ingredientes del plato no pueden ser vacios";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el precio del plato";
            }
            


            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }

        }

        public bool Editar(Plato obj, out string Mensaje)
        {

            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombreplato) || string.IsNullOrWhiteSpace(obj.Nombreplato))
            {
                Mensaje = "El nombre del plato no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Ingredientes) || string.IsNullOrWhiteSpace(obj.Ingredientes))
            {
                Mensaje = "Los ingredientes del plato no pueden ser vacios";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el precio del plato";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool GuardarDatosImagen(Plato obj, out string Mensaje)
        {
            return objCapaDato.GuardarDatosImagen(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }


    }
}
