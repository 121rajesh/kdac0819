using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using ExaminationPortal.Models;
using MySecurityLib;
using System.Net.Mail;
using ExaminationPortal.Controllers;

namespace FinalProject.Controllers
{
    public class ForgetPasswordController : BaseController
    {
        OnlineExamDBEntities dalObj;
        Response response;
        public ForgetPasswordController()
        {
            response = new Response();
            dalObj = new OnlineExamDBEntities();
        }

        // GET: api/ForgetPassword
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ForgetPassword/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ForgetPassword
        [HttpPost]
        [Route("api/User/OTP")]
        public Response OTP([FromBody]dynamic otpDetails)
        {
            string email = otpDetails.EmailId.ToString();


            var userlist = dalObj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == email
                        select u).SingleOrDefault();
            string o = otpDetails.OTP.ToString();

            var otpd = dalObj.T_OTP_Details.ToList();
            var vOTP = (from v in otpd
                        where v.UserId == User.UserId && v.OTP == o
                        select v).SingleOrDefault();

            if (vOTP != null)
            {
                Response RC = new Response();
                RC.Status = "success";
                RC.Error = null;
                RC.Data = vOTP;
                return RC;
            }
            else
            {
                Response RC = new Response();
                RC.Status = "fail";
                RC.Error = null;
                RC.Data = false;
                return RC;
            }
        }




        [HttpPost]
        [Route("api/User/IsExist")]
        public Response IsExist([FromBody]T_Users value)
        {

            var userlist = dalObj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();
            if (User != null)
            {
                Response RC = new Response();
                string mails = Security.GetOTP("1", 5);

                T_OTP_Details otp = new T_OTP_Details();
                otp.UserId = User.UserId;
                otp.ValidTill = (DateTime.Now).AddMinutes(5);
                otp.GeneratedOn = DateTime.Now;
                otp.OTP = mails;
                dalObj.T_OTP_Details.Add(otp);
                dalObj.SaveChanges();
                SendEmail(User.EmailId, "Registered successfully ", "your OTP is: "+mails);

                RC.Status = "success";
                RC.Error = null;
                RC.Data = mails;
                return RC;
            }
            else
            {
                Response RC = new Response();
                RC.Status = "fail";
                RC.Error = null;
                RC.Data = false;
                return RC;
            }

        }


        [HttpPut]
        [Route("api/User/UpdatePassword")]
        public Response updatepassword([FromBody]T_Users value)
        {

            var userlist = dalObj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();

            if (User != null)
            {
                User.Password = value.Password;
                dalObj.SaveChanges();
                Response rc = new Response();
                rc.Status = "success";
                rc.Error = null;
                rc.Data = User;
                return rc;
            }
            else
            {
                Response rc = new Response();
                rc.Status = "fail";
                rc.Error = null;
                rc.Data = null;
                return rc;
            }
        }

        // PUT: api/ForgetPassword/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ForgetPassword/5
        public void Delete(int id)
        {
        }

        //--------------------------------------------------------------------
        //Email and OTP

        [HttpPost]
        [Route("api/User/SendMail")]
        public string SendEmail(string toAddress, string subject, string body)
        {

            string result = "Message Sent Successfully..!!";
            string senderID = "kdaconlineexaminationportal@gmail.com";// use sender’s email id here..
            const string senderPassword = "admin@onlineportal"; // sender password here…
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // smtp server address here…
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(senderID, toAddress, subject, body);
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                //result = "Error sending email.!!!";
                result = ex.ToString();

            }
            return result;

        }
    }
}
