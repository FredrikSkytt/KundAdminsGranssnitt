using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using KundAdminsGranssnitt.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace KundAdminsGranssnitt.Controllers
{
    public class HomeController : Controller
    {
        VisningsSchema Visning = new VisningsSchema();
        List<Film> filmLista = new List<Film>();
        List<Salong> salongLista = new List<Salong>();
        
        
        public ActionResult Index()
        {
            SalongLista();
            FilmLista();
            return View(Visning);
        }

        public ActionResult Create()
        {
            SalongLista();
            FilmLista();
            var stringList = filmLista.ConvertAll(obj => obj.ToString());

            ViewBag.list = stringList;
            
            return View(Visning);
        }

        [HttpPost]
        public ActionResult Create(VisningsSchema nyVisning) /*Källa till create-metoden. https://www.tutorialsteacher.com/webapi/consume-web-api-post-method-in-aspnet-mvc*/
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://193.10.202.72/BiljettService/VisningsSchema");
                var postJob = client.PostAsJsonAsync<VisningsSchema>("VisningsSchema", nyVisning);
                postJob.Wait();

                var postReslut = postJob.Result;
                if (postReslut.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            }

            return View(nyVisning);
        }

   
        public void SalongLista()
        {
           
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

            Visning.SalongLista = salongLista;
           
        }
        
        public void FilmLista()
        {
            
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

            Visning.TitelLista = filmLista;
            
        }
      


    }
}