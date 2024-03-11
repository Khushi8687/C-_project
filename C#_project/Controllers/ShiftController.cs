using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using C__project.Models;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using C__project.Models.viewmodel;

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


            string url = "shiftdata/listshift";
            HttpResponseMessage response = client.GetAsync(url).Result;

             

            IEnumerable<ShiftDto> shifts = response.Content.ReadAsAsync<IEnumerable<ShiftDto>>().Result;

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
        public ActionResult Edit(int id)
        {
            Updateshift ViewModel = new Updateshift();


            string url = "shiftdata/findshift/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ShiftDto SelectedShift = response.Content.ReadAsAsync<ShiftDto>().Result;
            //ViewModel.SelectedStaff = SelectedStaff;


            return View(SelectedShift);
        }

        //GET: Shift/Update/2
        //update
        [HttpPost]

        public ActionResult Update(int id, Shift shift)
        {
            // if (ModelState.IsValid)
            //curl https://localhost:44357/api/shiftdata/updateshifts


            string url = "shiftData/editshift/" + id;
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

            //return View(shift);
        

       public ActionResult Error()
       {
          return View();
      }

        //delete
        public ActionResult DeleteConfirm(int id)
        {
            string url = "shiftdata/findshift/" + id;
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