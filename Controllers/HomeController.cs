using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using KundAdminsGranssnitt.Models;
using Newtonsoft.Json;

namespace KundAdminsGranssnitt.Controllers
{
    public class HomeController : Controller
    {              
        VisningsSchemaModel Visning = new VisningsSchemaModel();
        public ActionResult Index()
        {
            SalongLista();
            FilmLista();
                             
            return View(Visning);
        }
        [HttpPost]
        public ActionResult Index(VisningsSchemaModel visningsSchemaModel)
        {
            string titel = visningsSchemaModel.Titel;
            string namn = visningsSchemaModel.Namn;
            DateTime date = visningsSchemaModel.Datum;


            //using (var client = new HttpClient())
            //{
            //    VisningsSchemaModel visnings = new VisningsSchemaModel { Titel = titel, Namn = namn, Datum = date };
            //    client.BaseAddress = new Uri("http://193.10.202.72/BiljettService/visningsschema");
            //    var response = client.PostAsJsonAsync("visningsschema", visnings).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        Console.Write("Success");
            //    }
            //    else
            //        Console.Write("Error");
            //}


            return View();
        }




        public void SalongLista()
        {
            List<Salong> salongLista = new List<Salong>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://193.10.202.71/Filmservice/salong");
            // Add an Accept header for JSON format.    
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.    
            HttpResponseMessage response = client.GetAsync("salong").Result;  // Blocking call!    
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                salongLista = JsonConvert.DeserializeObject<List<Salong>>(products);
            }

            Visning.Salong = salongLista;
        }
        
        public void FilmLista()
        {
            List<Film> filmLista = new List<Film>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://193.10.202.71/Filmservice/film");
            // Add an Accept header for JSON format.    
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.    
            HttpResponseMessage response = client.GetAsync("film").Result;  // Blocking call!    
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                filmLista = JsonConvert.DeserializeObject<List<Film>>(products);
            }

            Visning.Film = filmLista;
        }
      


    }
}