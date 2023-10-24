using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Plukliste_for_web.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-3T5FOIJ;Initial Catalog=Pluklist;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Orders";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.ID = "" + reader.GetInt32(0);
                                clientInfo.Antal = "" + reader.GetInt32(1);
                                clientInfo.Type = reader.GetString(2);
                                clientInfo.Produktnr = reader.GetString(3);
                                clientInfo.Navn = reader.GetString(4);
                                clientInfo.Name = reader.GetString(5);
                                clientInfo.Forsendelse = reader.GetString(6);

                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());

            }
        }
    }

    public class ClientInfo
    {
        public string ID { get; set; }
        public string Antal { get; set; }
        public string Type { get; set; }
        public string Produktnr { get; set; }
        public string Navn { get; set; }
        public string Name { get; set; }
        public string Forsendelse { get; set; }
    }
}
