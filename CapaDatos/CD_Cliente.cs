﻿using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Cliente
    {
            public int Registrar(Cliente obj, out string Mensaje)
            {
                int idautogenerado = 0;

                Mensaje = string.Empty;
                try
                {
                    using (SqlConnection oconexion = new SqlConnection(Conexion.Conection))
                    {
                        SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oconexion);
                        cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                        cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                        cmd.Parameters.AddWithValue("Correo", obj.Correo);
                        cmd.Parameters.AddWithValue("Contraseña", obj.Contraseña);
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
            public List<Cliente> Listar()
            {
                List<Cliente> lista = new List<Cliente>();

                try
                {
                    using (SqlConnection oConection = new SqlConnection(Conexion.Conection))
                    {
                        string query = "select IdCliente, Nombres, Apellidos, Correo, Contraseña, Reestablecer from Cliente";

                        SqlCommand cmd = new SqlCommand(query, oConection);
                        cmd.CommandType = CommandType.Text;

                        oConection.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(
                                    new Cliente()
                                    {
                                        IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Apellidos = dr["Apellidos"].ToString(),
                                        Correo = dr["Correo"].ToString(),
                                        Contraseña = dr["Contraseña"].ToString(),
                                        Reestablecer = Convert.ToBoolean(dr["Reestablecer"])

                                    });
                            }
                        }
                    }
                }
                catch
                {
                    lista = new List<Cliente>();
                }
                return lista;
            }
            public bool ReestablecerClave(int idcliente, string Contraseña, out string Mensaje)
            {
                bool resultado = false;
                Mensaje = string.Empty;
                try
                {
                    using (SqlConnection oconexion = new SqlConnection(Conexion.Conection))
                    {
                        SqlCommand cmd = new SqlCommand("update cliente set Contraseña = @Contraseña, reestablecer = 1 where idcliente =@id", oconexion);
                        cmd.Parameters.AddWithValue("@id", idcliente);
                        cmd.Parameters.AddWithValue("@Contraseña", Contraseña);
                        cmd.CommandType = CommandType.Text;
                        oconexion.Open();
                        resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                }
                catch (Exception ex)
                {
                    resultado = false;
                    Mensaje = ex.Message;
                }
                return resultado;
            }
            public bool CambiarClave(int idcliente, string nuevaclave, out string Mensaje)
            {
                bool resultado = false;
                Mensaje = string.Empty;
                try
                {
                    using (SqlConnection oconexion = new SqlConnection(Conexion.Conection))
                    {
                        SqlCommand cmd = new SqlCommand("update cliente set Contraseña = @nuevaclave, reestablecer = 0 where idcliente =@id", oconexion);
                        cmd.Parameters.AddWithValue("@id", idcliente);
                        cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave);
                        cmd.CommandType = CommandType.Text;
                        oconexion.Open();
                        resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                }
                catch (Exception ex)
                {
                    resultado = false;
                    Mensaje = ex.Message;
                }
                return resultado;
            }
        } 
    }

