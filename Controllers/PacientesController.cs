using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pacientesPessoas.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Uri = System.Uri;

namespace pacientesPessoas.Controllers
{

    //Integrantes do grupo:
    //                      Guilherme Braga 
    //                      Lucas Idalgo
    //                      Moises Resende
    //                      Willian Silva

    public class PacientesController : Controller
    {
        private readonly ILogger<PacientesController> logger;
        
        //variável para comunicação, requisição com API
        private readonly HttpClient client;

        public PacientesController(ILogger<PacientesController> logger){
            this.logger = logger;
            client = new HttpClient{
                BaseAddress = new Uri("http://localhost:5000/api/")
            };
        }

        public IActionResult Index(){
            //obtendo dados dos pacientes
            var stringPacientes=Pacientes().Result;

             //criar uma lista de pacientes:
            var listaPacientes = JsonConvert.DeserializeObject<IEnumerable<Pacientes>>(stringPacientes);
            return View(listaPacientes);
        }

        //função requerindo a API e obtendo dados
        async Task<string> Pacientes(){
            HttpResponseMessage response = await client.GetAsync("pacientes");

            string responseBody = await response.Content.ReadAsStringAsync();
            
            return responseBody;
        }

        public IActionResult Editar(){
            return View();
        }

        public IActionResult Excluir(){
            return View();
        }

        
         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}