using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iGotTheRuns.Data
{
  public class Household
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<HouseholdMember> HouseholdMembers { get; set; }
  }
}