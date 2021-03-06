﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iGotTheRuns.Data
{
    public class MessageBoardRepository : IMessageBoardRepository
    {
        MessageBoardContext _ctx;
        public MessageBoardRepository(MessageBoardContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Topic> GetTopics()
        {
            //var ctx = new MessageBoardContext();
            ////If did it this way, every call to GetTopics would generate a new instance of the repository. Is expensive.
            ////Using NinjectWebCommon to ensure a single instance of the repository.  

            return _ctx.Topics;
        }

        public IQueryable<Reply> GetRepliesByTopic(int topicId)
        {
            return _ctx.Replies.Where(r => r.TopicId == topicId);
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                //TODO log this error
                return false;
            }
        }

        public bool AddTopic(Topic newTopic)
        {
            try
            {
                _ctx.Topics.Add(newTopic);
                return true;

            }
            catch (Exception ex)
            {
                //TODO log this error
                return false;
            }
        }


        public IQueryable<Topic> GetTopicsIncludingReplies()
        {
            return _ctx.Topics.Include("Replies");
        }


        public bool AddReply(Reply newReply)
        {
            try
            {
                _ctx.Replies.Add(newReply);
                return true;

            }
            catch (Exception ex)
            {
                //TODO log this error
                return false;
            }
        }
    }
}