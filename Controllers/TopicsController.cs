using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using iGotTheRuns.Data;

namespace iGotTheRuns.Controllers
{
    public class TopicsController : ApiController
    {
        private IMessageBoardRepository _repo;
        public TopicsController(IMessageBoardRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Topic> Get(bool includeReplies = false)
        {
            IQueryable<Topic> results;

            if (includeReplies == true)
            {
                //  /api/v1/topics/?includeReplies=true
                results = _repo.GetTopicsIncludingReplies();
            }
            else
            {
                //  /api/v1/topics/
                results = _repo.GetTopics();
            }

            var topics = results.OrderByDescending(t => t.Created)
                                .Take(25)
                                .ToList();
            return topics;
        }

        //notice WebApi doesn't require the HttpPost attribute bcz the method name is Post
        public HttpResponseMessage Post([FromBody]Topic newTopic)  //FromBody is not required but is a good practice to ensure body is injected
        {
            if (newTopic.Created == default(DateTime))
            {
                newTopic.Created = DateTime.UtcNow;
            }
            if(_repo.AddTopic(newTopic) &&
                _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, newTopic);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
