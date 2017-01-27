using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iGotTheRuns.Data
{
  public class FamilyHistoryConsultant : HouseholdMember
  {
    //public int Id { get; set; }
    public DateTime Visit { get; set; }
  }
}