using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutPatientApp.Persistence;

namespace OutPatientApp.Models
{
    class CaseNumber : EntityBase
    {
        public DateTime ForDate { get; set; } = DateTime.Now.Date;
        public int Number { get; set; } = 1;

        public CaseNumber Increment()
        {
            return new CaseNumber
            {
                Number = Number++,
                ForDate = ForDate
            };
        }

        public override string ToString()
        {
            return $"{ForDate.ToString("d").Replace("/", "")}-{Number:D4}";
        }

        public string Display => ToString();
    }
}
