using System;
using System.Configuration;
using InvoiceGenerator.Model;
using InvoiceGenerator.Helper;
using System.Windows.Forms;

namespace InvoiceGenerator
{
    public partial class Setting : MasterForm
    {
        public AppSettingManager appWriter { get; set; }
        public Setting() : base("setting")
        {
            InitializeComponent();
            appWriter = new AppSettingManager();
        }

        //save button
        private void button1_Click(object sender, EventArgs e)
        {

            // data read
            AppSettings setting = new AppSettings()
            {
                PaymentMethod = paymentMethod.Text,
                AccountHolderName = accountHolderName.Text,
                AccountNumber = accountNo.Text,
                Bank = bank.Text,
                Contact = contact.Text,
                Instagram = instagram.Text
            };

            // data write
            appWriter.Write(setting);
            MessageBox.Show("Saved Completed");
        }

        // on load
        private void Setting_Load(object sender, EventArgs e)
        {
            // data load
            AppSettings setting = appWriter.Load<AppSettings>();

            // data display
            paymentMethod.Text = setting.PaymentMethod;
            accountHolderName.Text = setting.AccountHolderName;
            accountNo.Text = setting.AccountNumber;
            bank.Text = setting.Bank;
            contact.Text = setting.Contact;
            instagram.Text = setting.Instagram;
        }
    }
}
