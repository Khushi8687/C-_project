using C__project.Migrations;
using C__project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace C__project.Controllers
{
    public class ShiftController : Controller
    {
        // GET: Shift/List
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ShiftController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44357/api/");
        }

        // GET: Shift/List
        public ActionResult List()
        {

            //curl https://localhost:44357/api/shiftdata/listshifts


            string url = " https://localhost:44357/api/shiftdata/listshifts";
            HttpResponseMessage response = client.GetAsync(url).Result;



            List<ShiftDto> shifts = response.Content.ReadAsAsync<List<ShiftDto>>().Result;



            return View(shifts);
        }

        public ActionResult Details(int id)
        {


            //objective: communicate with our animal data api to retrieve one animal
            //curl https://localhost:44324/api/shiftdata/findshift/{id}

            string url = "shiftfdata/findshift/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            ShiftDto SelectedShift = response.Content.ReadAsAsync<ShiftDto>().Result;
            Debug.WriteLine("shift received : ");


            url = "staffdata/liststaffforshift/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<StaffDto> KeptStaff = response.Content.ReadAsAsync<IEnumerable<StaffDto>>().Result;

            //ViewModel.KeptStaff = KeptStaffs;



            return View(SelectedShift);
        }

        // GET: Shift/Add
        public ActionResult Add()
        {
            //curl https://localhost:44357/api/shiftdata/Addshifts
            return View();
        }
        //add
        [HttpPost]
        public ActionResult Create(Shift shift)
        {
            Debug.WriteLine("the json payload is :");

            string url = "shiftdata/addshift";


            string jsonpayload = jss.Serialize(shift);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }
      


        //update
        [HttpPost]
        public ActionResult Update(int id, Shift shift)
        {

            //curl https://localhost:44357/api/shiftdata/updateshifts

            string url = "shiftdata/updateshift/" + id;
            string jsonpayload = jss.Serialize(shift);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //delete
        public ActionResult DeleteConfirm(int id)
        {
            string url = "shiftdata/deleteshift/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ShiftDto selectedshift = response.Content.ReadAsAsync<ShiftDto>().Result;
            return View(selectedshift);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "shiftdata/deleteshift/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}