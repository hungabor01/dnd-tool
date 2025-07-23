namespace DndTool.Views
{
    public partial class InputDialog : Form
    {
        public string Value => textBox1.Text;

        public InputDialog(string message)
        {
            InitializeComponent();

            label1.Text = message;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show(label1.Text);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void InputDialog_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
