using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGenerator.Model
{
    class AppSettings
    {
        [SettingName("method")]
        public string PaymentMethod { get; set; }

        [SettingName("accountnumber")]
        public string AccountNumber { get; set; }
        [SettingName("accountholdername")]
        public string AccountHolderName { get; set; }
        [SettingName("bank")]
        public string Bank { get; set; }
        [SettingName("instagram")]
        public string Instagram { get; set; }
        [SettingName("contact")]
        public string Contact { get; set; }
    }
}
