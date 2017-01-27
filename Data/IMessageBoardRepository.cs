using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGotTheRuns.Data
{
    public interface IMessageBoardRepository
    {
        //use interface to allow mock ups and testing
        IQueryable<Topic> GetTopics();  //IQueryable allows for paging, filtering, and other additional operations during DB execution
        IQueryable<Topic> GetTopicsIncludingReplies(); 
        IQueryable<Reply> GetRepliesByTopic(int topicId);

        bool Save();

        bool AddTopic(Topic newTopic);
        bool AddReply(Reply newReply);

    }
}
