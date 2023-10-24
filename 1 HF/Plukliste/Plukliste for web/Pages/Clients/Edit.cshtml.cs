using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Plukliste_for_web.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            string ID = Request.Query["ID"];

            try
            {
                String connectionString = "Data Source=DESKTOP-3T5FOIJ;Initial Catalog=Pluklist;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Orders WHERE ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.Antal = "" + reader.GetInt32(1);
                                clientInfo.Type = reader.GetString(2);
                                clientInfo.Produktnr = reader.GetString(3);
                                clientInfo.Navn = reader.GetString(4);
                                clientInfo.Name = reader.GetString(5);
                                clientInfo.Forsendelse = reader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            clientInfo.ID = Request.Form["ID"];
            clientInfo.Antal = Request.Form["Antal"];
            clientInfo.Type = Request.Form["Type"];
            clientInfo.Produktnr = Request.Form["Produktnr"];
            clientInfo.Navn = Request.Form["Navn"];
            clientInfo.Name = Request.Form["Name"];
            clientInfo.Forsendelse = Request.Form["Forsendelse"];

            if (clientInfo.Antal.Length == 0 || clientInfo.Type.Length == 0 ||
    clientInfo.Produktnr.Length == 0 || clientInfo.Navn.Length == 0 ||
    clientInfo.Name.Length == 0 || clientInfo.Forsendelse.Length == 0)
            {
                errorMessage = "Udfyld venligst alle felter";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-3T5FOIJ;Initial Catalog=Pluklist;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Orders " +
                                "SET Antal=@Antal, Type=@Type,Produktnr=@Produktnr,Navn=@Navn,name=@Name " +
                                "WHERE ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", clientInfo.Antal);
                        command.Parameters.AddWithValue("@Antal", clientInfo.Antal);
                        command.Parameters.AddWithValue("@Type", clientInfo.Type);
                        command.Parameters.AddWithValue("@Produktnr", clientInfo.Produktnr);
                        command.Parameters.AddWithValue("@Navn", clientInfo.Navn);
                        command.Parameters.AddWithValue("@Name", clientInfo.Name);
                        command.Parameters.AddWithValue("@Forsendelse", clientInfo.Forsendelse);

                        command.ExecuteNonQuery();
                    }
                }


            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clients/Index");
        }
    }
}
