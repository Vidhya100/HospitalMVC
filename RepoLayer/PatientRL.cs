using Microsoft.Extensions.Configuration;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepoLayer
{
    public class PatientRL: IPatientsRL
    {
        private readonly IConfiguration iconfiguration;

        public PatientRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

        //For getting doctors deatils
        public IEnumerable<Appoinment> GetDocList(string Role)
        {
            try
            {
                List<Appoinment> lstAppoinments = new List<Appoinment>();
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("spGetDocList", con);
                    cmd.CommandType = CommandType.StoredProcedure;



                    con.Open();
                    cmd.Parameters.AddWithValue("Role", Role);

                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Appoinment appoinment = new Appoinment();

                            appoinment.DId = Convert.ToInt32(reader["UserId"]);
                            appoinment.Photo = reader["ProfileIng"].ToString();
                            appoinment.Dname = reader["Username"].ToString();
                            appoinment.Degree = reader["Degree"].ToString();
                            appoinment.Address = reader["Address"].ToString();

                            lstAppoinments.Add(appoinment);
                        }
                        con.Close();
                    }
                    return lstAppoinments;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Appoinment GetApoointment(int DId, int PId)
        {
            try
            {
                Appoinment appoinments = new Appoinment();
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("spCreateAppointments", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    con.Open();
                    cmd.Parameters.AddWithValue("DId", DId);
                    cmd.Parameters.AddWithValue("PId", PId);

                    //cmd.Parameters.AddWithValue("AId", appoinments.AId);
                    cmd.Parameters.AddWithValue("ProfileImg", appoinments.Photo);
                    cmd.Parameters.AddWithValue("Pname", appoinments.Pname);
                    cmd.Parameters.AddWithValue("Dname", appoinments.Dname);
                    cmd.Parameters.AddWithValue("Date", appoinments.Date);
                    cmd.Parameters.AddWithValue("VisitStartTime", appoinments.Visit_Time);
                    cmd.Parameters.AddWithValue("VisitEndTime", appoinments.Visit_End);
                    cmd.Parameters.AddWithValue("Condition", appoinments.Condition);
                    cmd.Parameters.AddWithValue("Number", appoinments.Number);
                    cmd.Parameters.AddWithValue("Email", appoinments.Email);

                    var result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                         result = (int)cmd.ExecuteScalar();
                        if (result != null)
                        {
                            SqlCommand cmd2 = new SqlCommand("spGetAppointmentID", con);
                            cmd2.CommandType = CommandType.StoredProcedure;

                          
                            cmd2.Parameters.AddWithValue("DId", DId);
                            cmd2.Parameters.AddWithValue("PId", PId);

                            SqlDataReader reader = cmd2.ExecuteReader();

                            while (reader.Read())
                            {
                                appoinments.AId = Convert.ToInt32(reader["AId"]);
                            }
                            
                            SqlCommand cmd1 = new SqlCommand("spDoctorAppointment", con);
                            cmd1.CommandType = CommandType.StoredProcedure;


                            cmd1.Parameters.AddWithValue("AId", appoinments.AId);
                            cmd1.Parameters.AddWithValue("DId", DId);
                            var result1 = cmd.ExecuteNonQuery();
                           // con.Close();
                            if (result1 != 0)
                            {
                                return appoinments;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        //con.Close();
                        return appoinments;
                    }
                    
                    else
                    {
                        //con.Close();
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
