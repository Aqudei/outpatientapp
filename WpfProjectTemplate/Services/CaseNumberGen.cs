using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutPatientApp.Models;
using OutPatientApp.Persistence;

namespace OutPatientApp.Services
{
    class CaseNumberGen
    {
      

        public CaseNumber Get()
        {
            using (var db = new OPContext())
            {
                var now = DateTime.Now.Date;
                var caseNumber = db.CaseNumbers.Where(cn => DbFunctions.TruncateTime(cn.ForDate) == now)
                    .OrderByDescending(cn => cn.Number).FirstOrDefault();

                if (caseNumber == null)
                {
                    caseNumber = new CaseNumber();
                    db.CaseNumbers.Add(caseNumber);
                    db.SaveChanges();
                    return caseNumber;
                }

                caseNumber = caseNumber.Increment();
                db.CaseNumbers.Add(caseNumber);
                db.SaveChanges();
                return caseNumber;
            }
        }
    }
}
