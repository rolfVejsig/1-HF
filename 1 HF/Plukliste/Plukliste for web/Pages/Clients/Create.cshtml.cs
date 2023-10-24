using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data.SqlClient;

namespace Plukliste_for_web.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnPost()
        {
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
                String  connectionString = "Data Source=DESKTOP-3T5FOIJ;Initial Catalog=Pluklist;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Insert INTO Orders" +
                                "(Antal, Type, Produktnr, Navn, Name, Forsendelse) VALUES" +
                                "(@Antal,@Type,@Produktnr,@Navn,@Name,@Forsendelse)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Antal",clientInfo.Antal);
                        command.Parameters.AddWithValue("@Type", clientInfo.Type);
                        command.Parameters.AddWithValue("@Produktnr", clientInfo.Produktnr);
                        command.Parameters.AddWithValue("@Navn", clientInfo.Navn);
                        command.Parameters.AddWithValue("@Name", clientInfo.Name);
                        command.Parameters.AddWithValue("@Forsendelse", clientInfo.Forsendelse);

                        command.ExecuteNonQuery();
                    }
                }
			}
			catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            clientInfo.Antal = "";
            clientInfo.Type = "";
            clientInfo.Produktnr = "";
            clientInfo.Navn = "";
            clientInfo.Name = "";
            clientInfo.Forsendelse = "";
            successMessage = "Ny plukliste tilføjet";
            Response.Redirect("/Clients/Index");
        }
    }
}
