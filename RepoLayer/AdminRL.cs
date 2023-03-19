using Microsoft.Extensions.Configuration;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepoLayer
{
    public class AdminRL: IAdminRL
    {
        private readonly IConfiguration iconfiguration;

        public AdminRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

        public IEnumerable<Appoinment> GetAllApoointments()
        {
            try
            {
                List<Appoinment> lstAppoinments = new List<Appoinment>();
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("GetAllAppoinments", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                


                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Appoinment appoinment = new Appoinment();

                        appoinment.AId = Convert.ToInt32(reader["AId"]);
                        appoinment.PId = Convert.ToInt32(reader["PId"]);
                        appoinment.DId = Convert.ToInt32(reader["DId"]);
                        appoinment.Photo = reader["ProfileImg"].ToString();
                        appoinment.Pname = reader["Pname"].ToString();
                        appoinment.Email = reader["Email"].ToString();
                        appoinment.Date = Convert.ToDateTime(reader["Date"]);
                        appoinment.Visit_Time =Convert.ToDateTime(reader["VisitStartTime"]);
                        appoinment.Visit_End = Convert.ToDateTime(reader["VisiteEndTime"]);
                        appoinment.Number = Convert.ToInt32(reader["Number"]);
                        appoinment.Dname = reader["Dname"].ToString();
                        appoinment.Condition = reader["Condition"].ToString();
                        //appoinment.isHide = Convert.ToInt32(reader["isHide"]);

                        lstAppoinments.Add(appoinment);
                    }
                    con.Close();
                }
                return lstAppoinments;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CreateApModel GetApoointment(int Aid)
        {
            try
            {
                CreateApModel appoinments = new CreateApModel();
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("spAppoinmentDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    con.Open();
                    cmd.Parameters.AddWithValue("AId", Aid);
                    SqlDataReader reader = cmd.ExecuteReader();
                    CreateApModel appoinment = new CreateApModel();
                    while (reader.Read())
                    {


                        appoinment.AId = Convert.ToInt32(reader["AId"]);
                        appoinment.PId = Convert.ToInt32(reader["PId"]);
                        appoinment.DId = Convert.ToInt32(reader["DId"]);
                        appoinment.Photo = reader["ProfileImg"].ToString();
                        appoinment.Pname = reader["Pname"].ToString();
                        appoinment.Email = reader["Email"].ToString();
                        appoinment.Date = Convert.ToDateTime(reader["Date"]);
                        appoinment.Visit_Time = Convert.ToDateTime(reader["VisitStartTime"]);
                        appoinment.Visit_End = Convert.ToDateTime(reader["VisiteEndTime"]);
                        appoinment.Number = Convert.ToInt32(reader["Number"]);
                        appoinment.Dname = reader["Dname"].ToString();
                        appoinment.Condition = reader["Condition"].ToString();
                    }
                    return appoinment;
                    con.Close();
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public CreateApModel Update(CreateApModel appoinment)
        {
            try
            {
                using(SqlConnection con= new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("spUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AId", appoinment.AId);
                    //cmd.Parameters.AddWithValue("@PId", appoinment.PId);
                    //cmd.Parameters.AddWithValue("@DId", appoinment.DId);
                    cmd.Parameters.AddWithValue("@ProfileImg", appoinment.Photo);
                    cmd.Parameters.AddWithValue("@Pname", appoinment.Pname);
                    cmd.Parameters.AddWithValue("@Email", appoinment.Email);
                    cmd.Parameters.AddWithValue("@Dname", appoinment.Dname);
                    cmd.Parameters.AddWithValue("@Date", appoinment.Date);
                    cmd.Parameters.AddWithValue("@VisitStartTime", appoinment.Visit_Time);
                    cmd.Parameters.AddWithValue("@VisiteEndTime", appoinment.Visit_End);
                    cmd.Parameters.AddWithValue("@Number", appoinment.Number);
                    cmd.Parameters.AddWithValue("@Condition", appoinment.Condition);

                    con.Open();
                    int result=cmd.ExecuteNonQuery();
                    con.Close();

                    if(result != 0)
                    {
                        return appoinment;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        //To Delete the record on a particular employee    
        public bool Delete(int Aid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("spRemove", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AId", Aid);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (result != 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
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
