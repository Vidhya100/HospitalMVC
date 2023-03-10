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
                    SqlCommand cmd = new SqlCommand("spGqtAllAppointments", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                


                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Appoinment appoinment = new Appoinment();

                        appoinment.AId = Convert.ToInt32(reader["AId"]);
                        appoinment.Photo = reader["ProfileImg"].ToString();
                        appoinment.Name = reader["Pname"].ToString();
                        appoinment.Email = reader["Email"].ToString();
                        //appoinment.Date = Convert.ToInt32(reader["Date"]);
                        //appoinment.Visit_Time =reader["Time"]);
                        appoinment.Number = Convert.ToInt32(reader["Number"]);
                        appoinment.Doctor = reader["Dname"].ToString();
                        appoinment.Condition = reader["Condition"].ToString();

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

        public Appoinment GetApoointment(int Aid)
        {
            try
            {
                Appoinment appoinments = new Appoinment();
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("spGetById", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    con.Open();
                    cmd.Parameters.AddWithValue("AId", Aid);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Appoinment appoinment = new Appoinment();
                    while (reader.Read())
                    {
                        

                        appoinment.AId = Convert.ToInt32(reader["AId"]);
                        appoinment.Photo = reader["ProfileImg"].ToString();
                        appoinment.Name = reader["Pname"].ToString();
                        appoinment.Email = reader["Email"].ToString();
                        //appoinment.Date = reader["Date"].ToString();
                        //appoinment.Visit_Time =Convert.ToDateTime(reader["VisitTime"]);
                        //appoinment.Visit_Time = DateOnly.(Convert.ToDateTime(reader["VisitTime"]));
                        appoinment.Number = Convert.ToInt32(reader["Number"]);
                        appoinment.Doctor = reader["Dname"].ToString();
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


        public Appoinment Update(Appoinment appoinment)
        {
            try
            {
                using(SqlConnection con= new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("spUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AId", appoinment.AId);
                    cmd.Parameters.AddWithValue("@Pname", appoinment.Name);
                    cmd.Parameters.AddWithValue("@Dname", appoinment.Doctor);
                    cmd.Parameters.AddWithValue("@Date", appoinment.Date);
                    cmd.Parameters.AddWithValue("@VisitTime", appoinment.Visit_Time);
                    //cmd.Parameters.AddWithValue("@VisitEnd", appoinment.Visit_End);
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
                    SqlCommand cmd = new SqlCommand("spDelete", con);
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
