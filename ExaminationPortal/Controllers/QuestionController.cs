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

    public class QuestionController : BaseController
    {
        Response response = new Response();
        OnlineExamDBEntities dalobj = new OnlineExamDBEntities();
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);

        QuestionController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Question
        public Response Get()
        {
            try
            {
                List<T_Question> list = dalobj.T_Question.ToList();
                if (list != null)
                {
                    response.Data = list;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("List of Questions displayed");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Error = null;
                    logger.Log("List of Questions is Empty");
                    return response;
                }
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Error = cause;
                logger.Log("Exception occured returned Erroror msg");
                return response;
            }
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Questions/{id}")]
        public Response GetQuestionBySubjects(int id)
        {
            try
            {
                List<T_Question> quesList = dalobj.T_Question.ToList();
                List<T_Subject> subList = dalobj.T_Subject.ToList();

                var list = (from ques in quesList
                            where ques.SubId == id
                            select new
                            {
                            ques.Question,
                            ques.Opt1,
                            ques.Opt2,
                            ques.Opt3,
                            ques.Opt4,
                            ques.CorrectAns
                            }).ToList();
                if (list != null)
                {
                    response.Data = list;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("List of Questions displayed");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Error = null;
                    logger.Log("List of Questions is Empty");
                    return response;
                }
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Error = cause;
                logger.Log("Exception occured returned Erroror msg");
                return response;
            }
        }
        // GET: api/Question/5
        public Response Get(int id)
        {
            try
            {
                T_Question queToBeFind = dalobj.T_Question.Find(id);
                if (queToBeFind != null)
                {
                    response.Data = queToBeFind;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("Specific Question displayed using id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Error = null;
                    logger.Log("Specific Question Not found using id");
                    return response;

                }
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Error = cause;
                logger.Log("Exception occured returned Erroror msg");
                return response;
            }
        }

        //// GET: api/Question/5
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/Question/Subject/5")]
        //public Response Get1(int SubId)
        //{
        //    try
        //    {
        //        T_Question queToBeFind = dalobj.T_Question.Find(SubId);
        //        if (queToBeFind != null)
        //        {
        //            response.Data = queToBeFind;
        //            response.Status = "success";
        //            response.Error = null;
        //            logger.Log("Specific Question displayed using id");
        //            return response;
        //        }
        //        else
        //        {
        //            response.Data = null;
        //            response.Status = "failed";
        //            response.Error = null;
        //            logger.Log("Specific Question Not found using id");
        //            return response;

        //        }
        //    }
        //    catch (Exception cause)
        //    {
        //        response.Data = cause.Message;
        //        response.Status = "Failed";
        //        response.Error = cause;
        //        logger.Log("Exception occured returned Erroror msg");
        //        return response;
        //    }
        //}


        // POST: api/Question
        public Response Post([FromBody]T_Question que)
        {
            try
            {
                if (que != null)
                {
                    dalobj.T_Question.Add(que);
                    dalobj.SaveChanges();

                    response.Data = null;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("Question inserted in db");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Error = null;
                    logger.Log("Invalid Credentials");
                    return response;

                }
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Error = cause;
                logger.Log("Exception occured returned Erroror msg");
                return response;
            }
        }

        // PUT: api/Question/5
        public Response Put(int id, [FromBody]T_Question que)
        {
            try
            {
                T_Question queToBeFind = dalobj.T_Question.Find(id);
                if (queToBeFind != null)
                {
                    queToBeFind.SubId = que.SubId;
                    queToBeFind.Question = que.Question;
                    queToBeFind.Opt1 = que.Opt1;
                    queToBeFind.Opt2 = que.Opt2;
                    queToBeFind.Opt3 = que.Opt3;
                    queToBeFind.Opt4 = que.Opt4;
                    queToBeFind.CorrectAns = que.CorrectAns;

                    dalobj.SaveChanges();

                    response.Data = null;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("Specific Question updated using id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Error = null;
                    logger.Log("Specific Question Not found using id");
                    return response;

                }
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Error = cause;
                logger.Log("Exception occured returned Erroror msg");
                return response;
            }
        }

        // DELETE: api/Question/5
        public Response Delete(int id)
        {
            try
            {
                T_Question queToBeFind = dalobj.T_Question.Find(id);
                if (queToBeFind != null)
                {
                    dalobj.T_Question.Remove(queToBeFind);
                    dalobj.SaveChanges();

                    response.Data = null;
                    response.Status = "success";
                    response.Error = null;
                    logger.Log("Specific Question deleted using id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Error = null;
                    logger.Log("Specific Question Not found using id");
                    return response;

                }
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Error = cause;
                logger.Log("Exception occured returned Erroror msg");
                return response;
            }
        }
    }
}
