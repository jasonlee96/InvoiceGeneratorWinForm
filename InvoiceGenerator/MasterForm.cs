using System.Windows.Forms;
using InvoiceGenerator.Helper;
namespace InvoiceGenerator
{
    public partial class MasterForm : Form
    {
        string FormID = string.Empty;
        public MasterForm()
        {
            InitializeComponent();
        }

        public MasterForm(string formId)
        {
            FormID = formId;
            InitializeComponent();
        }

        private void menuToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.SwitchForm(e.ClickedItem.Tag.ToString());
        }
    }
}
