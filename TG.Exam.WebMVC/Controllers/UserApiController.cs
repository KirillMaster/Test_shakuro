using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TG.Exam.WebMVC.Models;
using Models = TG.Exam.WebMVC.Models;

namespace Salestech.Exam.WebMVC.Controllers
{
    public class UserApiController : ApiController
    {
        public List<Models.User> GetUsers()
        {
            return Models.User.GetAll();
        }
    }
}