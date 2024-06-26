using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public List<Reporte> Ventas(string fechainicio, string fechafin)
        {
            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oConection = new SqlConnection(Conexion.Conection))
                {
                    

                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oConection);

                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Reporte()
                                {
                                    
                                    FechaVenta = dr["FechaVenta"].ToString(),
                                    Cliente = dr["Cliente"].ToString(),
                                    Plato = dr["Plato"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es_CO")),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                    Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es_CO"))

                                }

                            );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Reporte>();
            }
            return lista;
        }


        public DashBoard VerDashBoard()
        {
            DashBoard objeto = new DashBoard();

            try
            {
                using (SqlConnection oConection = new SqlConnection(Conexion.Conection))
                {
                    

                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oConection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new DashBoard()
                            {
                                TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                TotalPlato = Convert.ToInt32(dr["TotalPlato"]),
                            };
                        }
                    }
                }
            }
            catch
            {
                objeto = new DashBoard();
            }
            return objeto;
        }
    }
}
