using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace iGotTheRuns.Data
{
    public class MessageBoardMigrationsConfiguration : DbMigrationsConfiguration<MessageBoardContext>
    {
        public MessageBoardMigrationsConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;

        }

        //allows insert or seeding data
        protected override void Seed(MessageBoardContext context)
        {
            base.Seed(context);

#if DEBUG
            if (context.Topics.Count() == 0)
            {
                var topic = new Topic()
                {
                    Title = "AngularJS User Group Mtg",
                    Created = DateTime.Now,
                    Body = "The next meeting will be Monday, May 18, 2015 at 3:00 PM in Room 213.",
                    Replies = new List<Reply>()
                    {
                        new Reply() {
                            Body = "Oh joy!",
                            Created = DateTime.Now
                        },
                        new Reply() {
                            Body = "I'm so excited!",
                            Created = DateTime.Now
                        },
                        new Reply() {
                            Body = "Oh Plop Plops. I can't make it this time.",
                            Created = DateTime.Now
                        }
                    }
                };

                context.Topics.Add(topic);

                var anotherTopic = new Topic()
                {
                    Title = "Fruit Loops User Group Mtg",
                    Created = DateTime.Now,
                    Body = "The next meeting will be Tuesday, May 19, 2015 at 3:00 PM in Room 213.",
                    Replies = new List<Reply>()
                    {
                        new Reply() {
                            Body = "Oh joy! I will bring the milk.",
                            Created = DateTime.Now
                        },
                        new Reply() {
                            Body = "I'm so excited! Now I can use my new spoon.",
                            Created = DateTime.Now
                        },
                        new Reply() {
                            Body = "Oh crap! I forgot to order my multi-colored Tucan Sam beak. It's the one from the haunted house commercial -> www.youtube.com/watch?v=f5O0EHwfPno",
                            Created = DateTime.Now
                        },
                        new Reply() {
                            Body = "That's my favorite Tucan Sam vintage! Have you ever seen the Tucan Sam evolution site? -> http://tinyurl.com/mcfgdma",
                            Created = DateTime.Now
                        }
                    }
                };

                context.Topics.Add(anotherTopic);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
#endif
        }
    }
}
