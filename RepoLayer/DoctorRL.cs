using Microsoft.Extensions.Configuration;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepoLayer
{
    public class DoctorRL : IDoctorRL
    {
        private readonly IConfiguration iconfiguration;

        public DoctorRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

        public IEnumerable<Appoinment> GetAllApoointments(int DId)
        {
            try
            {
                List<Appoinment> lstAppoinments = new List<Appoinment>();
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("GetAppoinmentList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    cmd.Parameters.AddWithValue("DId", DId);

                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Appoinment appoinment = new Appoinment();

                            appoinment.AId = Convert.ToInt32(reader["AId"]);

                            SqlCommand cmd2 = new SqlCommand("spAppoinmentDetails", con);
                            cmd2.CommandType = CommandType.StoredProcedure;

                            cmd2.Parameters.AddWithValue("AId", appoinment.AId);

                            var result2 = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                SqlDataReader reader2 = cmd2.ExecuteReader();

                                while (reader2.Read())
                                {
                                    appoinment.PId = Convert.ToInt32(reader["PId"]);
                                    appoinment.DId = Convert.ToInt32(reader["DId"]);
                                    appoinment.Pname = Convert.ToString(reader["Pname"]);
                                    appoinment.Dname = Convert.ToString(reader["Dname"]);
                                    appoinment.Email = Convert.ToString(reader["Email"]);
                                    appoinment.Photo = Convert.ToString(reader["ProfileImg"]);
                                    appoinment.Number = Convert.ToInt32(reader["Number"]);
                                    appoinment.Condition = Convert.ToString(reader["Condition"]);
                                    appoinment.Visit_Time = Convert.ToDateTime(reader["VisitStartTime"]);
                                    appoinment.Visit_End = Convert.ToDateTime(reader["VisiteEndTime"]);
                                    appoinment.Date = Convert.ToDateTime(reader["Date"]);

                                }
                            }
                            else
                            {
                                return null;
                            }

                            lstAppoinments.Add(appoinment);
                        }
                        con.Close();
                        return lstAppoinments;
                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
