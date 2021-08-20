using InvoiceGenerator.Helper;
using InvoiceGenerator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace InvoiceGenerator
{
    public partial class Template : InvoiceGenerator.MasterForm
    {
        public AppSettingManager appWriter { get; set; }
        public Template():base("template")
        {
            InitializeComponent();
            appWriter = new AppSettingManager();
        }

        private void Template_Load(object sender, EventArgs e)
        {
            // data load
            TemplateSetting setting = appWriter.Load<TemplateSetting>();

            // data display
            Recipient.Text = setting.Recipient;
            Address1.Text = setting.Address1;
            Address2.Text = setting.Address2;
            Address3.Text = setting.Address3;
            Contact.Text = setting.Contact;
            Currency.Text = setting.Currency;
            Invoice.Text = setting.InvoiceID;
            Date.Text = setting.Date;
            Breakdown.Text = setting.Breakdown;
            Subtotal.Text = setting.Subtotal;
            Tax.Text = setting.Tax;
            Total.Text = setting.Total;
            PaymentMethod.Text = setting.Method;
            AccountNo.Text = setting.AccountNo;
            AccountHolder.Text = setting.AccountName;
            Bank.Text = setting.Bank;
            Instagram.Text = setting.Instagram;
            FooterContact.Text = setting.FooterContact;
            FilePath.Text = setting.Path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // data read
            TemplateSetting setting = new TemplateSetting()
            {
                Path = FilePath.Text,
                Recipient = Recipient.Text,
                Address1 = Address1.Text,
                Address2 = Address2.Text,
                Address3 = Address3.Text,
                Contact = Contact.Text,
                Currency = Currency.Text,
                Breakdown = Breakdown.Text,
                Subtotal = Subtotal.Text,
                Tax = Tax.Text,
                Total = Total.Text,
                InvoiceID = Invoice.Text,
                Date = Date.Text,
                AccountName = AccountHolder.Text,
                AccountNo = AccountNo.Text,
                Method = PaymentMethod.Text,
                Bank = Bank.Text,
                Instagram = Instagram.Text,
                FooterContact = FooterContact.Text
            };

            // data write
            appWriter.Write(setting);
            MessageBox.Show("Saved Completed");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;

                string extension = Path.GetExtension(file);
                
                if(extension != null && extension == ".html")
                {
                    //
                    FilePath.Text = file;
                }
                else
                {
                    MessageBox.Show("Only html file is supported");
                }
            }
        }
    }
}
