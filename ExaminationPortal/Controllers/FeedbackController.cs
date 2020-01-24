using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using ExaminationPortal.Controllers;
using ExaminationPortal.Models;
namespace FinalProject.Controllers
{
    public class AFeedbackController : BaseController
    {
        Response response = new Response();
        OnlineExamDBEntities dalobj = new OnlineExamDBEntities();
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);

        AFeedbackController()
        {
           dalobj.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/AFeedback
        public Response Get()
        {
            try
            {
                List<T_Feedback> fdbkList = dalobj.T_Feedback.ToList();
                List<T_Users> uList = dalobj.T_Users.ToList();
                var list = (from u in uList
                            join fdbk in fdbkList on u.UserId equals fdbk.UserId
                            select fdbk).ToList();
                 
                response.Data = list;
                response.Status = "success";
                response.Error = null;
                logger.Log("Feedback list displayed");
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
        //To get list of users who have filled feedbacks
        //public Response GetUser()
        //{
        //    try
        //    {
        //        List<T_Feedback> fdbkList = dalobj.T_Feedback.ToList();

        //        return response;
        //    }
        //    catch (Exception cause)
        //    {
        //        response.Data = cause.Message;
        //        response.Status = "Failed";
        //        response.Error = cause;
        //        logger.Log("Exception occured returned Error msg");
        //        return response;
        //    }
        //}

        // GET: api/AFeedback/5
        public Response Get(int id)
        {
            try
            {
                T_Feedback fdbkToFind = dalobj.T_Feedback.Find(id);
                if (fdbkToFind != null)
                {
                    response.Data = fdbkToFind;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("specific feedback displayed using id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Error = null;
                    logger.Log("specific feedback not found using id");
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

        // POST: api/AFeedback
        public Response Post([FromBody]T_Feedback fdbk)
        {
            try
            {
                if (fdbk != null)                    // afterwards check valid subject or not so if/else loop ...
                {
                    dalobj.T_Feedback.Add(fdbk);
                    dalobj.SaveChanges();
                    response.Data = null;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("feedback added in db");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "Empty fields";
                    response.Error = null;
                    logger.Log("feedback insertion failed due to empty fields");
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

        // PUT: api/AFeedback/5
        public Response Put(int id, [FromBody] T_Feedback fdbk)
        {
            try
            {
                T_Feedback fdbkToFind = dalobj.T_Feedback.Find(id);
                if (fdbkToFind != null)
                {
                    fdbkToFind.UserId = fdbk.UserId;
                    fdbkToFind.Message = fdbk.Message;
                    dalobj.SaveChanges();

                    response.Data = fdbkToFind;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("Specific Feedback updated using Id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Error = null;
                    logger.Log("Something wentr wrong!");
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

        // DELETE: api/AFeedback/5
        public Response Delete(int id)
        {
            try
            {
                T_Feedback fdbkToFind = dalobj.T_Feedback.Find(id);

                if (fdbkToFind != null)
                {
                    dalobj.T_Feedback.Remove(fdbkToFind);
                    dalobj.SaveChanges();

                    response.Data = null;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("Specific Feedback deleted using Id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Error = null;
                    logger.Log("Something wentr wrong!");
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
