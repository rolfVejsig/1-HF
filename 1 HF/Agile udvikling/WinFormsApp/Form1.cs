using MedarbejderIDLibrary;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fornavn = textBox1.Text;
            string efternavn = textBox2.Text;

            string medarbejderID = MedarbejderIDGenerator.GenerateMedarbejderID(fornavn, efternavn);
            string firmaEmail = $"{fornavn.Substring(0, 1).ToLower()}{efternavn.ToLower()}@fiktivefirma.dk";

            label1.Visible = true;
            label1.Text = $"Dit medarbejder ID er: {medarbejderID}";

            label2.Visible = true;
            label2.Text = $"Din firmaemail er: {firmaEmail}";


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}