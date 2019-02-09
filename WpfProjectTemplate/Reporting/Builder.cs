using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace OutPatientApp.Reporting
{
    class Builder
    {
        public void Build()
        {
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MedicalRecordsDocTemplate.docx");

            using (var docx = DocX.Load(templatePath))
            {

            }
        }
    }
}
