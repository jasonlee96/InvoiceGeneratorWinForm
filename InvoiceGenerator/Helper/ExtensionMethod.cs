using System.Windows.Forms;

namespace InvoiceGenerator.Helper
{
    public static class ExtensionMethod
    {
        public static bool IsEmpty(this TextBox obj)
        {
            return string.IsNullOrEmpty(obj.Text);
        }

        public static bool IsNumber(this TextBox obj)
        {
            return decimal.TryParse(obj.Text, out decimal number);
        }
    }
}
