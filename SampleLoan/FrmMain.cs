using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace SampleLoan
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is FrmImport)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            FrmImport frmImport = new FrmImport();
            frmImport.MdiParent = this;
            frmImport.Show();
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiSmVmZnJleSBUYXZlcmEgVGF2ZXJhIiwiZW1haWwiOiJqZWZmcmV5dGF2ZXJhQGNmaWMucGgiLCJuYW1laWQiOiIyIiwibmJmIjoxNjk2ODQxMTg2LCJleHAiOjE2OTY4NDQ3ODYsImlhdCI6MTY5Njg0MTE4Nn0.5i1_RSxQ04dc78m07Uo875iCqHa_oOqgRgfimcpbAIc");
            //HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:44350/api/Sample/SampleRetrieveData");

            //var sample = await responseMessage.Content.ReadAsStringAsync();
        }


        

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            //System.Environment.Exit(1);
        }
    }
}
