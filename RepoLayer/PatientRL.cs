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
        public IEnumerable<Doctor> GetDocList(string Role)
        {
            try
            {
                List<Doctor> lstDoctor = new List<Doctor>();
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
                            Doctor doctor = new Doctor();

                            doctor.DId = Convert.ToInt32(reader["UserId"]);
                            doctor.Photo = reader["ProfileIng"].ToString();
                            doctor.Dname = reader["Username"].ToString();
                            doctor.Degree = reader["Degree"].ToString();
                            doctor.Address = reader["Address"].ToString();

                            lstDoctor.Add(doctor);
                        }
                        con.Close();
                    }
                    return lstDoctor;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public CreateApModel CreateApoointment(int DId, int PId, CreateApModel appoinments)
        {
            try
            {
                //Appoinment appoinments = new Appoinment();
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {
                    SqlCommand cmd = new SqlCommand("spCreateAppointments", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    con.Open();
                    cmd.Parameters.AddWithValue("DId", DId);
                    cmd.Parameters.AddWithValue("PId", PId);

                    
                    cmd.Parameters.AddWithValue("ProfileImg", appoinments.Photo);
                    cmd.Parameters.AddWithValue("Pname", appoinments.Pname);
                    cmd.Parameters.AddWithValue("Dname", appoinments.Dname);
                    cmd.Parameters.AddWithValue("Date", appoinments.Date);
                    cmd.Parameters.AddWithValue("VisitStartTime", appoinments.Visit_Time);
                    cmd.Parameters.AddWithValue("VisiteEndTime", appoinments.Visit_End);
                    cmd.Parameters.AddWithValue("Condition", appoinments.Condition);
                    cmd.Parameters.AddWithValue("Number", appoinments.Number);
                    cmd.Parameters.AddWithValue("Email", appoinments.Email);

                    var result = cmd.ExecuteNonQuery();
                    //con.Close();
                    if (result != 0)
                    {
                         
                            SqlCommand cmd2 = new SqlCommand("spGetAppointmentID", con);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            
                            cmd2.Parameters.AddWithValue("DId", DId);
                            cmd2.Parameters.AddWithValue("PId", PId);

                        var result2 = cmd2.ExecuteScalar();
                        
                        if (result2 != null)
                        {
                            //to close reader automatically
                            using (SqlDataReader reader = cmd2.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    appoinments.AId = Convert.ToInt32(reader["AId"]);
                                }
                            }
                            SqlCommand cmd1 = new SqlCommand("spDPAppointment", con);
                            cmd1.CommandType = CommandType.StoredProcedure;

                            //con.Open();
                            cmd1.Parameters.AddWithValue("AId", appoinments.AId);
                            cmd1.Parameters.AddWithValue("DId", DId);
                            cmd1.Parameters.AddWithValue("PId", PId);
                            var result1 = cmd1.ExecuteNonQuery();
                            //con.Close();
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
        //public IEnumerable<CreateApModel> ViewAppoinmentList(int PId, CreateApModel appoinment)
        //{
        //    try
        //    {
        //        List<CreateApModel> lstAppoinments = new List<CreateApModel>();
        //        using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
        //        {
        //            SqlCommand cmd = new SqlCommand("GetPAppoinmentList", con);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            con.Open();
        //            cmd.Parameters.AddWithValue("PId", PId);

        //            var result = cmd.ExecuteScalar();
        //            if (result != null)
        //            {
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {

        //                    while (reader.Read())
        //                    {
        //                        appoinment.AId = Convert.ToInt32(reader["AId"]);

        //                    }
        //                }
        //                //for reading data from Appoinment table
        //                SqlCommand cmd1 = new SqlCommand("GetPatientAppoinments", con);
        //                cmd1.CommandType = CommandType.StoredProcedure;

        //                cmd1.Parameters.AddWithValue("AId", appoinment.AId);
        //                cmd1.Parameters.AddWithValue("PId", PId);

        //                var result1 = cmd1.ExecuteScalar();

        //                if (result1 != null)
        //                {
        //                    SqlDataReader reader1 = cmd1.ExecuteReader();

        //                    while (reader1.Read())
        //                    {
        //                        //CreateApModel appoinment = new CreateApModel();

        //                        appoinment.AId = Convert.ToInt32(reader1["AId"]);
        //                        appoinment.PId = PId;

        //                        appoinment.Photo = reader1["ProfileImg"].ToString();
        //                        appoinment.Pname = reader1["Pname"].ToString();
        //                        appoinment.Email = reader1["Email"].ToString();
        //                        appoinment.Date = Convert.ToDateTime(reader1["Date"]);
        //                        appoinment.Visit_Time = Convert.ToDateTime(reader1["VisitStartTime"]);
        //                        appoinment.Visit_End = Convert.ToDateTime(reader1["VisiteEndTime"]);
        //                        appoinment.Number = Convert.ToInt32(reader1["Number"]);
        //                        appoinment.Dname = reader1["Dname"].ToString();
        //                        appoinment.Condition = reader1["Condition"].ToString();

        //                        lstAppoinments.Add(appoinment);
        //                    }
        //                    //}

        //                }
        //                // con.Close();
        //            }
        //            return lstAppoinments;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


        public IEnumerable<CreateApModel> ViewAppoinmentList(int PId, CreateApModel appoinment)
        {
            try
            {
                List<int> lstAId = new List<int>();
                //Detail list of appoinments
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {

                    //1. fectch appointment id's for Doctor id from doctor table
                    SqlCommand cmd = new SqlCommand("GetPAppoinmentList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.Parameters.AddWithValue("PId", PId);

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lstAId.Add(Convert.ToInt32(reader["AId"]));
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("GetAppoinmentList failed.");
                        return null;
                    }

                    //2. fetching appoinment details from appoinment table
                    int i = 1;
                    List<CreateApModel> lstAppoinments = new List<CreateApModel>();

                    while (lstAId.Count > 0 && lstAId.Count >= i)
                    {
                        CreateApModel createApModel = new CreateApModel();
                        createApModel.AId = lstAId[i - 1];
                        createApModel.PId = PId;

                        SqlCommand command = new SqlCommand("GetPatientAppoinments", con);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("AId", createApModel.AId);
                        command.Parameters.AddWithValue("PId", PId);

                        var result1 = command.ExecuteScalar();
                        if (result1 != null)
                        {
                            using (SqlDataReader reader1 = command.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    createApModel.Photo = reader1["ProfileImg"].ToString();
                                    createApModel.Pname = reader1["Pname"].ToString();
                                    createApModel.Email = reader1["Email"].ToString();
                                    createApModel.Date = Convert.ToDateTime(reader1["Date"]);
                                    createApModel.Visit_Time = Convert.ToDateTime(reader1["VisitStartTime"]);
                                    createApModel.Visit_End = Convert.ToDateTime(reader1["VisiteEndTime"]);
                                    createApModel.Number = Convert.ToInt32(reader1["Number"]);
                                    createApModel.Dname = reader1["Dname"].ToString();
                                    createApModel.Condition = reader1["Condition"].ToString();
                                }
                                lstAppoinments.Add(createApModel);
                                i++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("GetDocAppoinments failed.");
                            return null;
                        }
                    }//while end  of lstAID 
                    return lstAppoinments;
                }//close of connection
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
