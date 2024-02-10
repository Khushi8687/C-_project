using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using C__project.Models;
using System.Web.Script.Serialization;

namespace C__project.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff/List
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static StaffController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44357/api/");
        }

        // GET: Staff/List
        public ActionResult List()
        {
            
            //curl https://localhost:44357/api/staffdata/liststaffs


            string url = "staffdata/liststaffs";
            HttpResponseMessage response = client.GetAsync(url).Result;

            

            IEnumerable<StaffDto> staffs = response.Content.ReadAsAsync<IEnumerable<StaffDto>>().Result;
           


            return View(staffs);
        }

        // GET: Staff/Add
        public ActionResult Add()
        {
            //curl https://localhost:44357/api/staffdata/Addstaffs
            return View();
        }
        //add
        [HttpPost]
        public ActionResult Create(Staff staff)
        {
            Debug.WriteLine("the json payload is :");

            string url = "staffdata/addstaff";


            string jsonpayload = jss.Serialize(staff);
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
            public ActionResult Update(int id, Staff staff)
            {

            //curl https://localhost:44357/api/staffdata/updatestaffs

                string url = "staffdata/updatestaff/" + id;
                string jsonpayload = jss.Serialize(staff);
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
            string url = "staffdata/deletestaff/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            StaffDto selectedstaff = response.Content.ReadAsAsync<StaffDto>().Result;
            return View(selectedstaff);
        }

        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "staffdata/deletestaff/" + id;
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