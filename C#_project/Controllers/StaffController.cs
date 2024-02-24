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


            string url = " https://localhost:44357/api/staffdata/liststaffs";
            HttpResponseMessage response = client.GetAsync(url).Result;

            

            IEnumerable<StaffDto> staffs = response.Content.ReadAsAsync<IEnumerable<StaffDto>>().Result;
           


            return View(staffs);
        }

        public ActionResult Details(int id)
        {
            DetailsStaff viewModel = new DetailsStaff();

            // Retrieve the details of the selected staff member
            string staffUrl = "staffdata/findstaff/" + id;
            HttpResponseMessage staffResponse = client.GetAsync(staffUrl).Result;
            if (staffResponse.IsSuccessStatusCode)
            {
                Debug.WriteLine("response from staff");
                StaffDto selectedStaff = staffResponse.Content.ReadAsAsync<StaffDto>().Result;
                viewModel.selectedstaff = selectedStaff;
            }
            else
            {
                // Handle error if staff details retrieval fails
                return RedirectToAction("Error");
            }

            // Retrieve the shifts associated with the selected staff member
            string shiftsUrl = "shiftdata/listshiftforstaff/" + id;
            HttpResponseMessage shiftsResponse = client.GetAsync(shiftsUrl).Result;
            if (shiftsResponse.IsSuccessStatusCode)
            {
                IEnumerable<ShiftDto> responsibleShifts = shiftsResponse.Content.ReadAsAsync<IEnumerable<ShiftDto>>().Result;
                viewModel.ResponsibleShift = responsibleShifts;
                Debug.WriteLine("response from shifts");

            }
            else
            {
                // Handle error if shift retrieval fails
                return RedirectToAction("Error");
            }

            return View(viewModel);
        }




        /// <summary>
        /// Displays a message when it catches an error 
        /// </summary>
        /// <returns>
        /// Returns a view of Error Page
        /// </returns>
        public ActionResult Error()
        {
            return View();
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

        public ActionResult Edit(int id)
        {
            Updatestaff ViewModel = new Updatestaff();

           
            string url = "staffdata/findstaff/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            StaffDto SelectedStaff = response.Content.ReadAsAsync<StaffDto>().Result;
            //ViewModel.SelectedStaff = SelectedStaff;


            return View(SelectedStaff);
        }

        //GET: Staff/Update/2
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