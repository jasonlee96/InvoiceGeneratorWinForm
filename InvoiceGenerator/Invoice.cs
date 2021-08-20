using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using InvoiceGenerator.Helper;
using InvoiceGenerator.Model;
using static InvoiceGenerator.Helper.ExtensionMethod;

namespace InvoiceGenerator
{
    public partial class Invoice : MasterForm
    {
        public Invoice() : base("invoice")
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            GenerateButton.Enabled = false;
            GenerateButton.Text = "Generating...";
            // Data Validation
            bool valid = ValidateData();
            if (valid)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var filePath = config.AppSettings.Settings["htmlfile"].Value;
                var invoiceId = config.AppSettings.Settings["invoiceid"].Value;
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
                
                string html = File.ReadAllText(filePath);

                // template parsing
                string output = PlugDataIntoTemplate(html, invoiceId);
                PdfRender renderer = new PdfRender();
                string path = await renderer.PdfSharpConvert(output, invoiceId);

                int nextInvoice = int.Parse(invoiceId) + 1;
                config.AppSettings.Settings["invoiceid"].Value = nextInvoice.ToString();
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                MessageBox.Show("Invoice Generated.");
                if (File.Exists(path))
                {
                    Process.Start("explorer.exe", path);
                }
            }
            GenerateButton.Enabled = true;
            GenerateButton.Text = "Generate Invoice";
        }

        public string PlugDataIntoTemplate(string html, string invoiceId)
        {
            AppSettingManager appReader = new AppSettingManager();
            var app = appReader.Load<AppSettings>();
            var tp = appReader.Load<TemplateSetting>();
            decimal subtotal = 0m;
            decimal tax = 0m;
            StringBuilder breakdown = new StringBuilder();
            int index = 1;
            if (!Description1.IsEmpty() || !Price1.IsEmpty() || !Quantity1.IsEmpty())
            {
                StringBuild(breakdown, Description1, Price1, Quantity1, index);

                subtotal += decimal.Parse(Price1.Text);
                index++;
            }
            if (!Description2.IsEmpty() || !Price2.IsEmpty() || !Quantity2.IsEmpty())
            {
                StringBuild(breakdown, Description2, Price2, Quantity2, index);

                subtotal += decimal.Parse(Price2.Text);
                index++;
            }
            if (!Description3.IsEmpty() || !Price3.IsEmpty() || !Quantity3.IsEmpty())
            {
                StringBuild(breakdown, Description3, Price3, Quantity3, index);

                subtotal += decimal.Parse(Price3.Text);
                index++;
            }
            if (!Description4.IsEmpty() || !Price4.IsEmpty() || !Quantity4.IsEmpty())
            {
                StringBuild(breakdown, Description4, Price4, Quantity4, index);

                subtotal += decimal.Parse(Price4.Text);
                index++;
            }
            if (!Description5.IsEmpty() || !Price5.IsEmpty() || !Quantity5.IsEmpty())
            {
                StringBuild(breakdown, Description5, Price5, Quantity5, index);

                subtotal += decimal.Parse(Price5.Text);
                index++;
            }

            html = html.Replace(tp.Recipient, Recipient.Text).Replace(tp.Contact, Contact.Text)
                .Replace(tp.Address1, Address1.Text).Replace(tp.Address2, Address2.Text).Replace(tp.Address3, Address3.Text)
                .Replace(tp.Bank, app.Bank).Replace(tp.Breakdown, breakdown.ToString()).Replace(tp.Currency, Currency.Text)
                .Replace(tp.Date, DateTime.Now.ToString()).Replace(tp.FooterContact, app.Contact).Replace(tp.Instagram, app.Instagram)
                .Replace(tp.InvoiceID, invoiceId).Replace(tp.Method, app.PaymentMethod).Replace(tp.Subtotal, subtotal.ToString("0.00"))
                .Replace(tp.Tax, tax.ToString("0.00")).Replace(tp.Total, (subtotal + tax).ToString("0.00")).Replace(tp.AccountName, app.AccountHolderName)
                .Replace(tp.AccountNo, app.AccountNumber);

            return html;
        }

        public bool ValidateData()
        {
            bool breakdown = false;
            if (Recipient.IsEmpty())
            {
                MessageBox.Show("Recipient can't be empty");
                return false;
            }
            if (!Description1.IsEmpty() || !Price1.IsEmpty() || !Quantity1.IsEmpty())
            {
                breakdown = true;
                if (Description1.IsEmpty() || Price1.IsEmpty() || Quantity1.IsEmpty())
                {
                    MessageBox.Show("Item Breakdown data is missing, please double check");
                    return false;
                }
                if(!Price1.IsNumber() || !Quantity1.IsNumber())
                {
                    MessageBox.Show("Price or Quantity should be number");
                    return false;
                }
            }
            if (!Description2.IsEmpty() || !Price2.IsEmpty() || !Quantity2.IsEmpty())
            {
                breakdown = true;
                if (Description2.IsEmpty() || Price2.IsEmpty() || Quantity2.IsEmpty())
                {
                    MessageBox.Show("Item Breakdown data is missing, please double check");
                    return false;
                }
                if (!Price2.IsNumber() || !Quantity2.IsNumber())
                {
                    MessageBox.Show("Price or Quantity should be number");
                    return false;
                }
            }
            if (!Description3.IsEmpty() || !Price3.IsEmpty() || !Quantity3.IsEmpty())
            {
                breakdown = true;
                if (Description1.IsEmpty() || Price1.IsEmpty() || Quantity1.IsEmpty())
                {
                    MessageBox.Show("Item Breakdown data is missing, please double check");
                    return false;
                }
                if (!Price3.IsNumber() || !Quantity3.IsNumber())
                {
                    MessageBox.Show("Price or Quantity should be number");
                    return false;
                }
            }
            if (!Description4.IsEmpty() || !Price4.IsEmpty() || !Quantity4.IsEmpty())
            {
                breakdown = true;
                if (Description4.IsEmpty() || Price4.IsEmpty() || Quantity4.IsEmpty())
                {
                    MessageBox.Show("Item Breakdown data is missing, please double check");
                    return false;
                }
                if (!Price4.IsNumber() || !Quantity4.IsNumber())
                {
                    MessageBox.Show("Price or Quantity should be number");
                    return false;
                }
            }
            if (!Description5.IsEmpty() || !Price5.IsEmpty() || !Quantity5.IsEmpty())
            {
                breakdown = true;
                if (Description5.IsEmpty() || Price5.IsEmpty() || Quantity5.IsEmpty())
                {
                    MessageBox.Show("Item Breakdown data is missing, please double check");
                    return false;
                }
                if (!Price5.IsNumber() || !Quantity5.IsNumber())
                {
                    MessageBox.Show("Price or Quantity should be number");
                    return false;
                }
            }

            

            if (!breakdown)
            {
                MessageBox.Show("Item Breakdown is required");
                return false;
            }
            return true;
        }

        public void StringBuild(StringBuilder sb, TextBox desc, TextBox price, TextBox quantity, int index)
        {
            sb.Append("<tr><td>");
            sb.Append(index.ToString());
            sb.Append(".</td><td>");
            sb.Append(desc.Text);
            sb.Append("</td><td>");
            sb.Append(quantity.Text);
            sb.Append("</td><td>");
            sb.Append(decimal.Parse(price.Text).ToString("0.00"));
            sb.Append("</td></tr>");
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            Currency.Text = "MYR";
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var invoiceId = config.AppSettings.Settings["invoiceid"].Value;

            ID.Text = invoiceId;
        }
    }
}
