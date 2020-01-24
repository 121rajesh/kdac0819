using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExaminationPortal.Models;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class AdminController : ApiController
    {
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);
        Response response = null;
        OnlineExamDBEntities dalObj = null;
        public AdminController()
        {
            response = new Response();
            dalObj = new OnlineExamDBEntities();    
        }

        // GET: api/Admin
        public Response GetUsersList()
        {
            response.Data = dalObj.T_Users.ToList();
            response.Status = "Success";
            logger.Log("Displayed User list to admin");
            return response;   
        }

        //GET: api/Admin/5
        public Response Get(int id)
        {
            response.Data = dalObj.T_Users.Find(id);
            response.Status = "Success";
            response.Error = null;
            logger.Log("Displayed User data");
            return response;
        }

        // POST: api/Admin
        public Response Post([FromBody]T_Users admin)
        {
            dalObj.T_Users.Add(admin);
            dalObj.SaveChanges();
            response.Status = "Success";
            response.Error = null;
            logger.Log("New admin added");
            return response;
        }

        // PUT: api/Admin/5
        public Response Put(int id, [FromBody]T_Users admin)
        {
            T_Users adminObj = dalObj.T_Users.Find(id);
            adminObj.EmailId = admin.EmailId;
            adminObj.Name = admin.Name;
            adminObj.Password = admin.Password;
            adminObj.MobileNo = admin.MobileNo;
            dalObj.SaveChanges();
            response.Status = "Success";
            response.Error = null;
            logger.Log("Admin updated");
            return response;
        }

        // DELETE: api/Admin/5
        public Response Delete(int id)
        {
            T_Users admin = dalObj.T_Users.Find(id);
            dalObj.T_Users.Remove(admin);
            dalObj.SaveChanges();
            response.Status = "Success";
            response.Error = null;
            logger.Log("admin deleted");
            return response;
        }
    }
}
