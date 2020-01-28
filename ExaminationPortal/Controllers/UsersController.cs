using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using ExaminationPortal.Models;
using ExaminationPortal.Controllers;

namespace FinalProject.App_Start
{
    public class UsersController : BaseController
    {
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);
        OnlineExamDBEntities dalObj = new OnlineExamDBEntities();
        Response response = new Response();
        public UsersController()
        {
            dalObj.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Users
        public Response Get()
        {
            try
            {
                List<T_Users> users = dalObj.T_Users.ToList();
                response.Data = users;
                response.Status = "Success";
                response.Error = null;
                logger.Log("List displayed for Users");
                return response;
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Error = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }
        }

        // GET: api/Users/5
        public Response Get(int id)
        {
            try
            {
                response.Data = dalObj.T_Users.Find(id);
                response.Status = "Success";
                response.Error = null;
                logger.Log("Displayed User data");
                return response;
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Error = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }
        }

        // POST: api/Users
        public Response Post([FromBody]T_Users user)
        {
            try
            {
                dalObj.T_Users.Add(user);
                dalObj.SaveChanges();
                response.Status = "Success";
                response.Error = null;
                logger.Log("New user added");
                return response;
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Error = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }
        }

        // PUT: api/Users/5
        public Response Put(int id, [FromBody]T_Users user)
        {
            try
            {
                T_Users userObj = dalObj.T_Users.Find(id);
                userObj.EmailId = user.EmailId;
                userObj.Name = user.Name;
                //string encData = null;
                //MySecurityLib.Security.Encrypt(user.Password, out encData);
                //user.Password = encData;
                userObj.Password = user.Password;
                userObj.MobileNo = user.MobileNo;
                if (user.IsLocked == true)
                {
                    userObj.IsLocked = user.IsLocked;
                }
                else { userObj.IsLocked = user.IsLocked; }
                dalObj.SaveChanges();
                response.Status = "Success";
                response.Error = null;
                logger.Log("User updated");
                return response;
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Error = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }
        }

        // DELETE: api/Users/5
        public Response Delete(int id)
        {
            try
            {
                T_Users user = dalObj.T_Users.Find(id);
                dalObj.T_Users.Remove(user);
                dalObj.SaveChanges();
                response.Status = "Success";
                response.Error = null;
                logger.Log("User deleted");
                return response;
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Error = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }
        }
    }
}
