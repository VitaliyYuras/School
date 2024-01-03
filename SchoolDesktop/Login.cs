using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace SchoolDesktop
{
    public partial class Login : Form
    {
        private readonly HttpClient _httpClient;
        public Login()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var token = Authenticate(txtUsername.Text, txtPassword.Text);

            if (!string.IsNullOrEmpty(token))
            {
                var studentForm = new MainFrom(token);
                studentForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid credentials. Please try again.");
            }
        }
        private string Authenticate(string username, string password)
        {
            try
            {
                var response = _httpClient.PostAsync("https://localhost:7253/api/Token/token", new StringContent($"{{\"Login\":\"{username}\",\"Password\":\"{password}\"}}", Encoding.UTF8, "application/json")).Result;
                var token = response.Content.ReadAsStringAsync().Result;
                return response.IsSuccessStatusCode ? token : null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during authentication: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
