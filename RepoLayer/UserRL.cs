using Microsoft.Extensions.Configuration;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepoLayer
{
    public class UserRL: IUserRL
    {
        private readonly IConfiguration iconfiguration;

        public UserRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

        public RegiModel Registration(RegiModel regiModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("spRegister", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.Parameters.AddWithValue("Role", regiModel.Role);
                    cmd.Parameters.AddWithValue("Username", regiModel.Username);
                    cmd.Parameters.AddWithValue("Email", regiModel.Email);
                    cmd.Parameters.AddWithValue("Password", regiModel.Password);
                    cmd.Parameters.AddWithValue("Fullname", regiModel.Fullname);
                    cmd.Parameters.AddWithValue("ProfileIng", regiModel.Photo);
                    //cmd.Parameters.AddWithValue("Degree", regiModel.Degree);
                    //cmd.Parameters.AddWithValue("Address", regiModel.Address);

                    if (regiModel.Role == "Patient" || regiModel.Role == "Admin")
                    {

                        
                        cmd.Parameters.AddWithValue("Degree", "-");
                        cmd.Parameters.AddWithValue("Address", "-");

                    }
                    else if (regiModel.Role == "Doctor")
                    {
                       
                        cmd.Parameters.AddWithValue("Degree", regiModel.Degree);
                        cmd.Parameters.AddWithValue("Address", regiModel.Address);
                    }
                    var result = cmd.ExecuteNonQuery();
                    if(result != 0)
                    {
                        return regiModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
        public RegiModel Login(LoginModel loginModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("spLogin", con);
                    cmd.CommandType= CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("Email", loginModel.Email);
                    cmd.Parameters.AddWithValue("Password", loginModel.Password);


                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        
                        SqlDataReader reader = cmd.ExecuteReader();
                        RegiModel regiModel = new RegiModel();
   
                            
                            while (reader.Read())
                            {
                                regiModel.UserId = Convert.ToInt32(reader["UserId"]);
                                regiModel.Username = reader["Username"].ToString();
                                regiModel.Email = reader["Email"].ToString();
                                regiModel.Password = reader["Password"].ToString();
                                regiModel.Role = reader["Role"].ToString();
                            }
                        //con.Close();
                        return regiModel;
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


    }
}
