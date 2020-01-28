using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExaminationPortal.Models;
using FinalProject.Models;
using System.IO;
using System.Net.Http.Headers;

namespace ExaminationPortal.Controllers
{
    public class SubjectController : BaseController
    {
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);
        Response response = new Response();
        OnlineExamDBEntities dalObj = new OnlineExamDBEntities();
        public SubjectController()
        {
            dalObj.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Subject
        public Response Get()
        {
            try
            {
                List<T_Subject> subjects = dalObj.T_Subject.ToList();
                response.Data = subjects;
                response.Status = "success";
                response.Error = null;
                logger.Log("List displayed for subjects");
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

        // GET: api/Subject/5
        public Response Get(int id)
        {
            try
            {
                T_Subject subject = dalObj.T_Subject.Find(id);
                response.Data = subject;
                response.Status = "success";
                response.Error = null;
                logger.Log("Get subject by id   ");
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

        // POST: api/Subject
        public Response Post([FromBody]T_Subject sub)
        {
            //dalObj.T_Subject.Add(sub);
            //dalObj.SaveChanges();

            try
            {
                dalObj.Proc_AddSubject(sub.SubName);
                response.Data = null;
                response.Status = "Subject added successfully";
                response.Error = null;
                logger.Log("Added new Subject");
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
        
        // PUT: api/Subject/5
        public Response Put(int id, [FromBody]T_Subject sub)
        {
            //T_Subject subToBeModified = dalObj.T_Subject.Find(id);
            //subToBeModified.SubName = sub.SubName;

            try
            {
                dalObj.Proc_ModifSubject(id, sub.SubName);
                response.Data = null;
                response.Status = "Success";
                response.Error = null;
                logger.Log("Subject modified");
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

        // DELETE: api/Subject/5
        public Response Delete(int id)
        {
            //T_Subject sub = dalObj.T_Subject.Find(id);
            //dalObj.T_Subject.Remove(sub);
            //dalObj.SaveChanges();

            try
            {
                dalObj.Proc_RemoveSubject(id);
                response.Data = null;
                response.Status = "Successfully deleted ";
                response.Error = null;
                logger.Log("Subject Deleted");
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

        [HttpGet]
        [Route("api/Subject/pdf")]
        public HttpResponseMessage RetrieveFile()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            FileStream fileStream = File.OpenRead(@"D:\ASDM\MEAN.pdf");
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }
    }
}
