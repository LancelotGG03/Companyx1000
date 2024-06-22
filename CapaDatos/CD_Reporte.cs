using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Reporte
    {
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
