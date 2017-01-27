using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iGotTheRuns.Data
{
  public class HouseholdMember
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Household Household { get; set; }

    //public virtual int HouseholdId { get; set; }
  }
}
