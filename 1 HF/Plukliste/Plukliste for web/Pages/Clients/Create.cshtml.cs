using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


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
            clientInfo.Antal = Request.Form["antal"];
            clientInfo.Type = Request.Form["type"];
            clientInfo.Produktnr = Request.Form["produktnr"];
            clientInfo.Navn = Request.Form["navn"];
            clientInfo.Name = Request.Form["name"];
            clientInfo.Forsendelse = Request.Form["Forsendelse"];

            if (clientInfo.Antal.Length == 0 || clientInfo.Type.Length == 0 ||
                clientInfo.Produktnr.Length == 0 || clientInfo.Navn.Length == 0 ||
                clientInfo.Name.Length == 0 || clientInfo.Forsendelse.Length == 0)
            {
                errorMessage = "Udfyld venligst alle felter";
                return;
            }

            string json = JsonConvert.SerializeObject(clientInfo);
            string webRootPath = _webHostEnvironment.WebRootPath;
            string filePath = Path.Combine(webRootPath, "clientInfo.json");

            System.IO.File.WriteAllText(filePath, json);

            clientInfo.Antal = "";
            clientInfo.Type = "";
            clientInfo.Produktnr = "";
            clientInfo.Navn = "";
            clientInfo.Name = "";
            clientInfo.Forsendelse = "";
            successMessage = "Ny plukliste tilføjet";
        }
    }
}
