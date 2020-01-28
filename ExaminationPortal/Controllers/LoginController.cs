using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using ExaminationPortal.Models;
//using System.Web.Security;
using ExaminationPortal.Controllers;
namespace FinalProject.Controllers
{
    public class LoginController : BaseController
    {
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);
        Response response = new Response();
        OnlineExamDBEntities dalObj = new OnlineExamDBEntities();
        public LoginController()
        {
            dalObj.Configuration.ProxyCreationEnabled = false;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Login")]
        public Response Login([FromBody]T_Users user)
        {
            try
            {
                
                string encData = null;
                List<T_Users> users = dalObj.T_Users.ToList();
                MySecurityLib.Security.Encrypt(user.Password, out encData);
                user.Password = encData;
                if (user.EmailId != null && user.Password != null)
                {
                    var validate = (from u in users
                                    where u.EmailId == user.EmailId && u.Password ==
                                    user.Password
                                    select u).SingleOrDefault();
                    if (validate != null)
                    {
                        response.Data = validate;
                        response.Error = null;
                        response.Status = "Success";
                        return response;
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = null;
                        response.Status = "Incorrect email or password";
                        return response;
                    }
                }
                else
                {
                    response.Data = null;
                    response.Error = null;
                    response.Status = "Fields are empty";
                    return response;
                }
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
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Login/Register")]
        public Response Register([FromBody] T_Users data)
        {
            try
            {
                if (data != null)
                {
                    string encdata = null;
                    MySecurityLib.Security.Encrypt(data.Password, out encdata);

                    data.Password = encdata;
                    dalObj.T_Users.Add(data);
                    dalObj.SaveChanges();
                    response.Data = data;
                    response.Error = null;
                    response.Status = "Registered successfully, login here";
                    ForgetPasswordController fpc = new ForgetPasswordController();
                    fpc.SendEmail(data.EmailId, "Registered successfully", "Thanks for registering with us!");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Error = null;
                    response.Status = "Fields are empty";
                    return response;
                }
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
