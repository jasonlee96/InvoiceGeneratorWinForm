using InvoiceGenerator.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceGenerator.Helper
{
    public static class FormRenderer
    {
        public static void SwitchForm(this MasterForm form, string formName)
        {
            Form newForm;
            switch (formName)
            {
                case "invoice":
                    newForm = new Invoice();
                    newForm.StartPosition = FormStartPosition.Manual;
                    newForm.Show();
                    form.Hide();
                    break;
                case "setting":
                    newForm = new Setting();
                    newForm.StartPosition = FormStartPosition.Manual;
                    newForm.Show();
                    form.Hide();
                    break;
                case "template":
                    newForm = new Template();
                    newForm.StartPosition = FormStartPosition.Manual;
                    newForm.Show();
                    form.Hide();
                    break;
            }
        }
    }
}
