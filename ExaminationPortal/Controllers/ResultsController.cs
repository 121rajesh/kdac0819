using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using ExaminationPortal.Controllers;
using ExaminationPortal.Models;

namespace ExaminationPortal.Controllers
{
    public class ResultsController : BaseController
    {
        Response response = new Response();
        OnlineExamDBEntities dalobj = new OnlineExamDBEntities();
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);

        ResultsController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Results
        public Response Get()
        {
            try
            {
                List<T_Result> resList = dalobj.T_Result.ToList();
                List<T_Subject> subList = dalobj.T_Subject.ToList();
                List<T_Users> uList = dalobj.T_Users.ToList();
                var list = (from res in resList
                            join u in uList on res.UserId equals u.UserId
                            join sub in subList on res.SubId equals sub.SubId
                            select new
                            {
                                u.Name,
                                u.EmailId,
                                sub.SubName,
                                u.IsLocked,
                                res.CntCorrectAns
                            }).ToList();

                response.Data = list;
                response.Status = "success";
                response.Error = null;
                logger.Log("Result list displayed");
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
        //        List<T_Result> resList = dalobj.T_Result.ToList();

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
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/Results/5")]
        // GET: api/Results/5
        public Response Get(int id)
        {
            try
            {
                T_Result resToFind = dalobj.T_Result.Find(id);
                if (resToFind != null)
                {
                    response.Data = resToFind;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("specific result displayed using id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Error = null;
                    logger.Log("specific result not found using id");
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
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/Results")]
        // POST: api/Results
        public Response Post([FromBody]T_Result res)
        {
            try
            {
                if (res != null)                    // afterwards check valid subject or not so if/else loop ...
                {
                    dalobj.T_Result.Add(res);
                    dalobj.SaveChanges();
                    response.Data = null;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("result added in db");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "Empty fields";
                    response.Error = null;
                    logger.Log("result insertion failed due to empty fields");
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
        //[System.Web.Http.HttpPut]
        //[System.Web.Http.Route("api/Results/5")]
        // PUT: api/Results/5
        public Response Put(int id, [FromBody] T_Result res)
        {
            try
            {
                T_Result resToFind = dalobj.T_Result.Find(id);
                if (resToFind != null)
                {
                    resToFind.UserId = res.UserId;
                    //resToFind.Message = res.Message;
                    dalobj.SaveChanges();

                    response.Data = resToFind;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("Specific Result updated using Id");
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
        //[System.Web.Http.HttpDelete]
        //[System.Web.Http.Route("api/Results/5")]
        // DELETE: api/Results/5
        public Response Delete(int id)
        {
            try
            {
                T_Result resToFind = dalobj.T_Result.Find(id);

                if (resToFind != null)
                {
                    dalobj.T_Result.Remove(resToFind);
                    dalobj.SaveChanges();

                    response.Data = null;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("Specific Result deleted using Id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Error = null;
                    logger.Log("Something went wrong!");
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
