using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace SampleLoan
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private async  void btOK_Click(object sender, EventArgs e)
        {

            HttpClient client = new HttpClient();

            EmailPasswordDTO email = new EmailPasswordDTO();    

           string username = txUsername.Text;
            string password = txPassword.Text;
            email.email = username;
            email.password = password;

            string json = JsonConvert.SerializeObject(email);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await client.PostAsync("https://localhost:44350/Credential/Login/LoginUser", httpContent);


                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                 {
                MessageBox.Show("Email or Password is incorrect", "Error", MessageBoxButtons.OK);
            }
            else 
                 {

                var responseMessageIfSuccess = await httpResponseMessage.Content.ReadAsStringAsync();

                //LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseMessageIfSuccess);
                this.Hide();
                FrmMain frMain = new FrmMain();
                frMain.Show();
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }

    public class EmailPasswordDTO
    {

        public string email { get; set; }
        public string password { get; set; }
    }
    public class LoginResponse
    {
        public HttpStatusCode statusCode { get; set; }

        public string message { get; set; }
        public bool isSuccess { get; set; }



    }
}
