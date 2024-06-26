﻿using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection;


namespace CapaDatos
{
    public class CD_Plato
    {
        public List<Plato> Listar()
        {
            List<Plato> Listar = new List<Plato>();

            try
            {
                using (SqlConnection oConection = new SqlConnection(Conexion.Conection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select p.IdPlato, p.Nombreplato, p.Ingredientes, p.Descripcion,");
                    sb.AppendLine("c.IdCategoria, c.Descripcion[DesCategoria],");
                    sb.AppendLine("p.Precio, p.Rutaimagen, p.Nombreimagen, p.Activo");
                    sb.AppendLine("from Plato p");
                    sb.AppendLine("inner join Categoria c on c.IdCategoria = p.IdCat");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConection);
                    cmd.CommandType = CommandType.Text;

                    oConection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Listar.Add(
                                new Plato()

                                {
                                    IdPlato = Convert.ToInt32(dr["IdPlato"]),
                                    Nombreplato = dr["NombrePlato"].ToString(),
                                    Ingredientes = dr["Ingredientes"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-CO")),
                                    Rutaimagen = dr["Rutaimagen"].ToString(),
                                    Nombreimagen = dr["Nombreimagen"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                });
                        }
                    }
                }
            }
            catch
            {
                Listar = new List<Plato>();
            }
            return Listar;
        }
        public int Registrar(Plato obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.Conection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarPlato", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombreplato);
                    cmd.Parameters.AddWithValue("Ingredientes", obj.Ingredientes);
                    cmd.Parameters.AddWithValue("Descripcion",obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdCat", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }

            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }
        public bool Editar(Plato obj, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.Conection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarPlato", oconexion);
                    cmd.Parameters.AddWithValue("IdPlato", obj.IdPlato);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombreplato);
                    cmd.Parameters.AddWithValue("Ingredientes", obj.Ingredientes);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdCat", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }

            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool GuardarDatosImagen(Plato obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.Conection))
                {

                    string query = "update Plato set Rutaimagen = @Rutaimagen, Nombreimagen = @Nombreimagen where IdPlato = @IdPlato";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@Rutaimagen", obj.Rutaimagen);
                    cmd.Parameters.AddWithValue("@Nombreimagen", obj.Nombreimagen);
                    cmd.Parameters.AddWithValue("@IdPlato", obj.IdPlato);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    if(cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo actulizar imagen";
                    }
                }
            }

            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return (resultado);
        }

        public bool Eliminar(int Id, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.Conection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarPlato", oconexion);
                    cmd.Parameters.AddWithValue("IdPlato", Id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }

            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public List<Plato> ListarPlatoporCategoria(int idcategoria)
        {
            List<Plato> Listar = new List<Plato>();

            try
            {
                using (SqlConnection oConection = new SqlConnection(Conexion.Conection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select distinct c.Descripcion from Plato p");
                    sb.AppendLine("inner join Categoria c on c.IdCategoria = p.IdCat");
                    sb.AppendLine("where c.IdCategoria = iif (@idcategoria = 0, c.IdCategoria,@idcategoria");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConection);
                    cmd.Parameters.AddWithValue("@idcategoria", idcategoria);
                    cmd.CommandType = CommandType.Text;

                    oConection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Listar.Add(
                                new Plato()

                                {
                                    IdPlato = Convert.ToInt32(dr["IdPlato"]),
                                    Descripcion = dr["Descripcion"].ToString(),
                                });
                        }
                    }
                }
            }
            catch
            {
                Listar = new List<Plato>();
            }
            return Listar;
        }
    }
}
