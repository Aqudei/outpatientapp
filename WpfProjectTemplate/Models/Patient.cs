﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutPatientApp.Models
{
    class Patient : EntityBase
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? Birthday { get; set; }
        public string BirthPlace { get; set; }
        public string CivilStatus { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }

        public string NextKin { get; set; }
        public string KinRelationship { get; set; }

        public DateTime? LastUpdated { get; set; }

        public Patient()
        {
            LastUpdated = DateTime.Now;
        }
    }
}
