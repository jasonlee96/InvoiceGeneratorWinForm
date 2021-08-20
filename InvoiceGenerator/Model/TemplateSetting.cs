using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGenerator.Model
{
    class TemplateSetting
    { 
        [SettingName("htmlfile")]
        public string Path { get; set; }

        [SettingName("recipientplaceholder")]
        public string Recipient { get; set; }
        [SettingName("address1placeholder")]
        public string Address1 { get; set; }
        [SettingName("address2placeholder")]
        public string Address2 { get; set; }
        [SettingName("address3placeholder")]
        public string Address3 { get; set; }
        [SettingName("contactplaceholder")]
        public string Contact { get; set; }
        [SettingName("invoiceplaceholder")]
        public string InvoiceID { get; set; }
        [SettingName("dateplaceholder")]
        public string Date { get; set; }
        [SettingName("currencyplaceholder")]
        public string Currency { get; set; }
        [SettingName("subtotalplaceholder")]
        public string Subtotal { get; set; }
        [SettingName("totalplaceholder")]
        public string Total { get; set; }
        [SettingName("taxplaceholder")]
        public string Tax { get; set; }

        [SettingName("methodplaceholder")]
        public string Method { get; set; }
        [SettingName("breakdownplaceholder")]
        public string Breakdown { get; set; }
        [SettingName("accountnoplaceholder")]
        public string AccountNo { get; set; }
        [SettingName("accountnameplaceholder")]
        public string AccountName { get; set; }
        [SettingName("bankplaceholder")]
        public string Bank { get; set; }
        [SettingName("instagramplaceholder")]
        public string Instagram { get; set; }
        [SettingName("footercontactplaceholder")]
        public string FooterContact { get; set; }
    }
}
