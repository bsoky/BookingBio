using BookingBio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Managers
{
    public class LoungeManager
    {     
        public int FetchLoungeId(int id) // fetch lounge
        {
            using (var db = new BookingDBEntities())
            {
                var lounge = (from s in db.Lounges
                               where s.loungeId == id
                               select s.loungeId).FirstOrDefault();

                

                return lounge;
            }
        }
    }
}