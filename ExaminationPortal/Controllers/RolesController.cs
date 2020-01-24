using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using ExaminationPortal.Models;
using ExaminationPortal.Controllers;
namespace FinalProject.Controllers
{
    public class RolesController : BaseController
    {
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);
        Response response = new Response();
        OnlineExamDBEntities dalObj = new OnlineExamDBEntities();
        // GET: api/Roles

        public RolesController()
        {
            
            dalObj.Configuration.ProxyCreationEnabled = false;
        }


        public Response Get()
        {
            List<T_Roles> roles = dalObj.T_Roles.ToList();
            response.Data = roles;
            response.Status = "Success";
            response.Error = null;
            logger.Log("List displayed for roles");
            return response;
        }

        // GET: api/Roles/5
        public Response Get(int id)
        {
            response.Data = dalObj.T_Roles.Find(id);
            response.Status = "Success";
            response.Error = null;
            logger.Log("Displayed Roles");
            return response;
        }

        // POST: api/Roles
        public Response Post([FromBody]T_Roles role)
        {
            //dalObj.T_Roles.Add(role);
            //dalObj.SaveChanges();
            dalObj.Proc_AddRole(role.RoleName);
            response.Data = null;    
            response.Status = "Success";
            response.Error = null;
            logger.Log("Added new roles");
            return response;
        }

        // PUT: api/Roles/5
        public Response Put(int id, [FromBody]T_Roles roles)
        {
            //T_Roles roleobj = dalObj.T_Roles.Find(id);
            //roleobj.RoleName = roles.RoleName;
            //dalObj.SaveChanges();
            
            dalObj.Proc_ModifyRole(id, roles.RoleName);
            response.Data = null;
            response.Status = "Success";
            response.Error = null;
            logger.Log("Roles Edited");
            return response;
        }

        // DELETE: api/Roles/5
        public Response Delete(int id)
        {
            //T_Roles role = dalObj.T_Roles.Find(id);
            //dalObj.T_Roles.Remove(role);
            //dalObj.SaveChanges();

            dalObj.Proc_RemoveRole(id);
            response.Data = null;
            response.Status = "Success";
            response.Error = null;
            logger.Log("Role Deleted");
            return response;
        }
    }
}
