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

        public IEnumerable<CreateApModel> ViewAppoinmentList(int DId, CreateApModel appoinment)
        {
            try
            {
                List<int> lstAId = new List<int>();
                //Detail list of appoinments
                using (SqlConnection con = new SqlConnection(this.iconfiguration.GetConnectionString("Hospital")))
                {

                    //1. fectch appointment id's for Doctor id from doctor table
                    SqlCommand cmd = new SqlCommand("GetAppoinmentList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.Parameters.AddWithValue("DId", DId);

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
                        createApModel.DId = DId;

                        SqlCommand command = new SqlCommand("GetDocAppoinments", con);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("AId", createApModel.AId);
                        command.Parameters.AddWithValue("DId", DId);

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



