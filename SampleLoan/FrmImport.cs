using System.Data;
using System;
using System.IO;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.OleDb;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.Generic;

namespace SampleLoan
{
    public partial class FrmImport : Form
    {
        public FrmImport()
        {
            InitializeComponent();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {

            List<HappyPathForPNRequest> happyPathForPNRequests = new List<HappyPathForPNRequest>();
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Excel Files (*.xls)|*.xls";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream inputStream;

                //Get the path of specified file
                filePath = openFileDialog.FileName;
                try
                {
                    inputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _ = MessageBox.Show("Please close the file first.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var connectionString = $@"
                Provider=Microsoft.ACE.OLEDB.12.0;
                Data Source={filePath};
                Extended Properties=""Excel 12.0 Xml;HDR=YES""
                ";

                using (var conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM [Sheet1$]";

                    using (var rdr = cmd.ExecuteReader())
                    {

                        //LINQ query - when executed will create anonymous objects for each row
                        var query = rdr.Cast<DbDataRecord>().Select(row => new
                        {
                            num = row[0],
                            Acctnumber = row[1],
                            Loannumber = row[2],
                            DateSetup = row[3],
                            DateAvailed = row[4],
                            DateMaturity = row[5],
                            LoanAmount = row[6],
                            TotalInterest = row[7],
                            TotalPrincipal = row[8],
                            TotalBalance = row[9],
                            InterestRate = row[10],
                            Term = row[11],
                            Rateperperiod = row[12],
                            Typeofpayment = row[13],
                            StatusAD = row[14],
                            Computation = row[15],
                            DocStamps = row[16],
                            NotStamp = row[17],
                            Service = row[18],
                            ChattelMortgage = row[19],
                            RealMortgage = row[20],
                            Appraisal = row[21],
                            OtherCharges = row[22],
                            BrokenDayInterest = row[23],
                            Setoff = row[24],
                            CvNo = row[25],
                            Instructions = row[26],
                            ClassStatus = row[27],
                            BrokenDay = row[28],
                            Penalty = row[29],
                            InterestUnpaid = row[30],
                            Status = row[31],
                            MNCTransferDate = row[32],
                            Released = row[33],
                            BrokenDayStart = row[34]
                        }).ToList();



                        happyPathForPNRequests = (from row in query
                                                  select new HappyPathForPNRequest
                                                  {
                                                      num = row.num == DBNull.Value ? string.Empty : (string)row.num,
                                                      Acctnumber = row.Acctnumber == DBNull.Value ? string.Empty : (string)row.Acctnumber,
                                                      Loannumber = row.Loannumber == DBNull.Value ? string.Empty : (string)row.Loannumber,
                                                      DateSetup = row.DateSetup == DBNull.Value ? string.Empty : (string)row.DateSetup,
                                                      DateAvailed = row.DateAvailed == DBNull.Value ? string.Empty : (string)row.DateAvailed,
                                                      DateMaturity = row.DateMaturity == DBNull.Value ? string.Empty : (string)row.DateMaturity,
                                                      LoanAmount = row.LoanAmount == DBNull.Value ? string.Empty : (string)row.LoanAmount,
                                                      TotalInterest = row.TotalInterest == DBNull.Value ? string.Empty : (string)row.TotalInterest,
                                                      TotalPrincipal = row.TotalPrincipal == DBNull.Value ? string.Empty : (string)row.TotalPrincipal,
                                                      TotalBalance = row.TotalBalance == DBNull.Value ? string.Empty : (string)row.TotalBalance,
                                                      InterestRate = row.InterestRate == DBNull.Value ? string.Empty : (string)row.InterestRate,
                                                      Term = row.Term == DBNull.Value ? string.Empty : (string)row.Term,
                                                      Rateperperiod = row.Rateperperiod == DBNull.Value ? string.Empty : (string)row.Rateperperiod,
                                                      Typeofpayment = row.Typeofpayment == DBNull.Value ? string.Empty : (string)row.Typeofpayment,
                                                      StatusAD = row.StatusAD == DBNull.Value ? string.Empty : (string)row.StatusAD,
                                                      Computation = row.Computation == DBNull.Value ? string.Empty : (string)row.Computation,
                                                      DocStamps = row.DocStamps == DBNull.Value ? string.Empty : (string)row.DocStamps,
                                                      NotStamp = row.NotStamp == DBNull.Value ? string.Empty : (string)row.NotStamp,
                                                      Service = row.Service == DBNull.Value ? string.Empty : (string)row.Service,
                                                      ChattelMortgage = row.ChattelMortgage == DBNull.Value ? string.Empty : (string)row.ChattelMortgage,
                                                      RealMortgage = row.RealMortgage == DBNull.Value ? string.Empty : (string)row.RealMortgage,
                                                      Appraisal = row.Appraisal == DBNull.Value ? string.Empty : (string)row.Appraisal,
                                                      OtherCharges = row.OtherCharges == DBNull.Value ? string.Empty : (string)row.OtherCharges,
                                                      BrokenDayInterest = row.BrokenDayInterest == DBNull.Value ? string.Empty : (string)row.BrokenDayInterest,
                                                      Setoff = row.Setoff == DBNull.Value ? string.Empty : (string)row.Setoff,
                                                      CvNo = row.CvNo == DBNull.Value ? string.Empty : (string)row.CvNo,
                                                      Instructions = row.Instructions == DBNull.Value ? string.Empty : (string)row.Instructions,
                                                      ClassStatus = row.ClassStatus == DBNull.Value ? string.Empty : (string)row.ClassStatus,
                                                      BrokenDay = row.BrokenDay == DBNull.Value ? string.Empty : (string)row.BrokenDay,
                                                      Penalty = row.Penalty == DBNull.Value ? string.Empty : (string)row.Penalty,
                                                      InterestUnpaid = row.InterestUnpaid == DBNull.Value ? string.Empty : (string)row.InterestUnpaid,
                                                      Status = row.Status == DBNull.Value ? string.Empty : (string)row.Status,
                                                      MNCTransferDate = row.MNCTransferDate == DBNull.Value ? string.Empty : (string)row.MNCTransferDate,
                                                      Released = row.Released == DBNull.Value ? string.Empty : (string)row.Released,
                                                      BrokenDayStart = row.BrokenDayStart == DBNull.Value ? string.Empty : (string)row.BrokenDayStart
                                                  }).ToList();

                        //Generates JSON from the LINQ query
                        var json = JsonConvert.SerializeObject(happyPathForPNRequests);

                        // var destinationPath = @"C:\db\PN.json";
                        // File.WriteAllText(destinationPath, json);
                    }
                }
                _ = MessageBox.Show("Done importing", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btOpenCIF_Click(object sender, EventArgs e)
        {
            List<HappyPathRequest> happyPathRequest = new List<HappyPathRequest>();
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Excel Files (*.xls)|*.xls";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream inputStream;

                //Get the path of specified file
                filePath = openFileDialog.FileName;
                try
                {
                    inputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _ = MessageBox.Show("Please close the file first.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var connectionString = $@"
                Provider=Microsoft.ACE.OLEDB.12.0;
                Data Source={filePath};
                Extended Properties=""Excel 12.0 Xml;HDR=YES""
                ";



                using (var conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM [Sheet1$]";

                    using (var rdr = cmd.ExecuteReader())
                    {

                        //LINQ query - when executed will create anonymous objects for each row
                        var querydatafromexcel = rdr.Cast<DbDataRecord>().Select(row => new
                        {
                            num = row[0],
                            LoanType = row[1],
                            Acctnumber = row[2],
                            CompanyCode = row[3],
                            BorrowerName = row[4],
                            BorrowerFirstName = row[5],
                            BorrowerMiddleName = row[6],
                            DatePrepared = row[7],
                            LoanAmount = row[8],
                            InterestRate = row[9],
                            EffectiveYield = row[10],
                            Term = row[11],
                            Purpose = row[12],
                            PurposeSpecify = row[13],
                            TermDesired = row[14],
                            Months = row[15],
                            Vesting = row[16],
                            EmploymentEmployer = row[17],
                            EmployeeNumber = row[18],
                            EmploymentPrePosition = row[19],
                            EmploymentDept = row[20],
                            EmploymentPlaceBranch = row[21],
                            DateHired = row[22],
                            Nationality = row[23],
                            AppLengthService = row[24],
                            AppMonthlySalary = row[25],
                            EmploymentImmediateHead = row[26],
                            EmploymentPositionHead = row[27],
                            EmploymentTelnum = row[28],
                            Sex = row[29],
                            BirthDate = row[30],
                            Status = row[31],
                            BorrowerTelno = row[32],
                            AppCellphone = row[33],
                            BorrowerProvincialAdd = row[34],
                            LengthStayPer = row[35],
                            BorrowerAddress = row[36],
                            LengthStapHome = row[37],
                            ResidenceType = row[38],
                            HowMuch = row[39],
                            SpouseName = row[40],
                            SpouseBirthDate = row[41],
                            SpousePreEmp = row[42],
                            SpouseAddress = row[43],
                            SpouseLengthService = row[44],
                            SpousePhone = row[45],
                            SpousePrePos = row[46],
                            SpouseSalary = row[47],
                            NoChildren = row[48],
                            YoungestChild = row[49],
                            School = row[50],
                            Course = row[51],
                            SchoolYear = row[52],
                            NearestRelative = row[53],
                            RelativeAddress = row[54],
                            Relationship = row[55],
                            RelHomePhone = row[56],
                            RelCellphone = row[57],
                            BorrowerTin = row[58],
                            BorrowerSSS = row[59],
                            ResCertNo = row[60],
                            PlaceIssue = row[61],
                            DateIssue = row[62],
                            Remarks = row[63],
                            DocSubmitted = row[64],
                            Tag = row[65],
                            SourceApplication = row[66],
                            Agent = row[67],
                            Type = row[68],
                            Agency = row[69],
                            YearsOFW = row[70],
                            Email = row[71],
                            SpouseEmail = row[72],
                            SourceIncome = row[73],
                            SpouseSourceIncome = row[74],
                            SpouseCitizenship = row[75],
                            EmployeeStatus = row[76],
                            EmployeeStatusOthers = row[77]
                        }).ToList();

                        happyPathRequest = (from query in querydatafromexcel
                                            select new HappyPathRequest
                                            {

                                                num = query.num == DBNull.Value ? string.Empty : (string)query.num,
                                                LoanType = query.LoanType == DBNull.Value ? string.Empty : (string)query.LoanType,
                                                Acctnumber = query.Acctnumber == DBNull.Value ? string.Empty : (string)query.Acctnumber,
                                                CompanyCode = query.CompanyCode == DBNull.Value ? string.Empty : (string)query.CompanyCode,
                                                BorrowerName = query.BorrowerName == DBNull.Value ? string.Empty : (string)query.BorrowerName,
                                                BorrowerFirstName = query.BorrowerFirstName == DBNull.Value ? string.Empty : (string)query.BorrowerFirstName,
                                                BorrowerMiddleName = query.BorrowerMiddleName == DBNull.Value ? string.Empty : (string)query.BorrowerMiddleName,
                                                DatePrepared = query.DatePrepared == DBNull.Value ? string.Empty : (string)query.DatePrepared,
                                                LoanAmount = query.LoanAmount == DBNull.Value ? string.Empty : (string)query.LoanAmount,
                                                InterestRate = query.InterestRate == DBNull.Value ? string.Empty : (string)query.InterestRate,
                                                EffectiveYield = query.EffectiveYield == DBNull.Value ? string.Empty : (string)query.EffectiveYield,
                                                Term = query.Term == DBNull.Value ? string.Empty : (string)query.Term,
                                                Purpose = query.Purpose == DBNull.Value ? string.Empty : (string)query.Purpose,
                                                PurposeSpecify = query.PurposeSpecify == DBNull.Value ? string.Empty : (string)query.PurposeSpecify,
                                                TermDesired = query.TermDesired == DBNull.Value ? string.Empty : (string)query.TermDesired,
                                                Months = query.Months == DBNull.Value ? string.Empty : (string)query.Months,
                                                Vesting = query.Vesting == DBNull.Value ? string.Empty : (string)query.Vesting,
                                                EmploymentEmployer = query.EmploymentEmployer == DBNull.Value ? string.Empty : (string)query.EmploymentEmployer,
                                                EmployeeNumber = query.EmployeeNumber == DBNull.Value ? string.Empty : (string)query.EmployeeNumber,
                                                EmploymentPrePosition = query.EmploymentPrePosition == DBNull.Value ? string.Empty : (string)query.EmploymentPrePosition,
                                                EmploymentDept = query.EmploymentDept == DBNull.Value ? string.Empty : (string)query.EmploymentDept,
                                                EmploymentPlaceBranch = query.EmploymentPlaceBranch == DBNull.Value ? string.Empty : (string)query.EmploymentPlaceBranch,
                                                DateHired = query.DateHired == DBNull.Value ? string.Empty : (string)query.DateHired,
                                                Nationality = query.Nationality == DBNull.Value ? string.Empty : (string)query.Nationality,
                                                AppLengthService = query.AppLengthService == DBNull.Value ? string.Empty : (string)query.AppLengthService,
                                                AppMonthlySalary = query.AppMonthlySalary == DBNull.Value ? string.Empty : (string)query.AppMonthlySalary,
                                                EmploymentImmediateHead = query.EmploymentImmediateHead == DBNull.Value ? string.Empty : (string)query.EmploymentImmediateHead,
                                                EmploymentPositionHead = query.EmploymentPositionHead == DBNull.Value ? string.Empty : (string)query.EmploymentPositionHead,
                                                EmploymentTelnum = query.EmploymentTelnum == DBNull.Value ? string.Empty : (string)query.EmploymentTelnum,
                                                Sex = query.Sex == DBNull.Value ? string.Empty : (string)query.Sex,
                                                BirthDate = query.BirthDate == DBNull.Value ? string.Empty : (string)query.BirthDate,
                                                Status = query.Status == DBNull.Value ? string.Empty : (string)query.Status,
                                                BorrowerTelno = query.BorrowerTelno == DBNull.Value ? string.Empty : (string)query.BorrowerTelno,
                                                AppCellphone = query.AppCellphone == DBNull.Value ? string.Empty : (string)query.AppCellphone,
                                                BorrowerProvincialAdd = query.BorrowerProvincialAdd == DBNull.Value ? string.Empty : (string)query.BorrowerProvincialAdd,
                                                LengthStayPer = query.LengthStayPer == DBNull.Value ? string.Empty : (string)query.LengthStayPer,
                                                BorrowerAddress = query.BorrowerAddress == DBNull.Value ? string.Empty : (string)query.BorrowerAddress,
                                                LengthStapHome = query.LengthStapHome == DBNull.Value ? string.Empty : (string)query.LengthStapHome,
                                                ResidenceType = query.ResidenceType == DBNull.Value ? string.Empty : (string)query.ResidenceType,
                                                HowMuch = query.HowMuch == DBNull.Value ? string.Empty : (string)query.HowMuch,
                                                SpouseName = query.SpouseName == DBNull.Value ? string.Empty : (string)query.SpouseName,
                                                SpouseBirthDate = query.SpouseBirthDate == DBNull.Value ? string.Empty : (string)query.SpouseBirthDate,
                                                SpousePreEmp = query.SpousePreEmp == DBNull.Value ? string.Empty : (string)query.SpousePreEmp,
                                                SpouseAddress = query.SpouseAddress == DBNull.Value ? string.Empty : (string)query.SpouseAddress,
                                                SpouseLengthService = query.SpouseLengthService == DBNull.Value ? string.Empty : (string)query.SpouseLengthService,
                                                SpousePhone = query.SpousePhone == DBNull.Value ? string.Empty : (string)query.SpousePhone,
                                                SpousePrePos = query.SpousePrePos == DBNull.Value ? string.Empty : (string)query.SpousePrePos,
                                                SpouseSalary = query.SpouseSalary == DBNull.Value ? string.Empty : (string)query.SpouseSalary,
                                                NoChildren = query.NoChildren == DBNull.Value ? string.Empty : (string)query.NoChildren,
                                                YoungestChild = query.YoungestChild == DBNull.Value ? string.Empty : (string)query.YoungestChild,
                                                School = query.School == DBNull.Value ? string.Empty : (string)query.School,
                                                Course = query.Course == DBNull.Value ? string.Empty : (string)query.Course,
                                                SchoolYear = query.SchoolYear == DBNull.Value ? string.Empty : (string)query.SchoolYear,
                                                NearestRelative = query.NearestRelative == DBNull.Value ? string.Empty : (string)query.NearestRelative,
                                                RelativeAddress = query.RelativeAddress == DBNull.Value ? string.Empty : (string)query.RelativeAddress,
                                                Relationship = query.Relationship == DBNull.Value ? string.Empty : (string)query.Relationship,
                                                RelHomePhone = query.RelHomePhone == DBNull.Value ? string.Empty : (string)query.RelHomePhone,
                                                RelCellphone = query.RelCellphone == DBNull.Value ? string.Empty : (string)query.RelCellphone,
                                                BorrowerTin = query.BorrowerTin == DBNull.Value ? string.Empty : (string)query.BorrowerTin,
                                                BorrowerSSS = query.BorrowerSSS == DBNull.Value ? string.Empty : (string)query.BorrowerSSS,
                                                ResCertNo = query.ResCertNo == DBNull.Value ? string.Empty : (string)query.ResCertNo,
                                                PlaceIssue = query.PlaceIssue == DBNull.Value ? string.Empty : (string)query.PlaceIssue,
                                                DateIssue = query.DateIssue == DBNull.Value ? string.Empty : (string)query.DateIssue,
                                                Remarks = query.Remarks == DBNull.Value ? string.Empty : (string)query.Remarks,
                                                DocSubmitted = query.DocSubmitted == DBNull.Value ? string.Empty : (string)query.DocSubmitted,
                                                Tag = query.Tag == DBNull.Value ? string.Empty : (string)query.Tag,
                                                SourceApplication = query.SourceApplication == DBNull.Value ? string.Empty : (string)query.SourceApplication,
                                                Agent = query.Agent == DBNull.Value ? string.Empty : (string)query.Agent,
                                                Type = query.Type == DBNull.Value ? string.Empty : (string)query.Type,
                                                Agency = query.Agency == DBNull.Value ? string.Empty : (string)query.Agency,
                                                YearsOFW = query.YearsOFW == DBNull.Value ? string.Empty : (string)query.YearsOFW,
                                                Email = query.Email == DBNull.Value ? string.Empty : (string)query.Email,
                                                SpouseEmail = query.SpouseEmail == DBNull.Value ? string.Empty : (string)query.SpouseEmail,
                                                SourceIncome = query.SourceIncome == DBNull.Value ? string.Empty : (string)query.SourceIncome,
                                                SpouseSourceIncome = query.SpouseSourceIncome == DBNull.Value ? string.Empty : (string)query.SpouseSourceIncome,
                                                SpouseCitizenship = query.SpouseCitizenship == DBNull.Value ? string.Empty : (string)query.SpouseCitizenship,
                                                EmployeeStatus = query.EmployeeStatus == DBNull.Value ? string.Empty : (string)query.EmployeeStatus,
                                                EmployeeStatusOthers = query.EmployeeStatusOthers == DBNull.Value ? string.Empty : (string)query.EmployeeStatusOthers

                                            }).ToList();


                        var json = JsonConvert.SerializeObject(happyPathRequest);


                        HttpClient client = new HttpClient();

                        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                        HttpResponseMessage httpResponseMessage = await client.PostAsync("https://localhost:44350/api/HappyPath/InsertingHappyPathProcess", httpContent);




                        //Generates JSON from the LINQ query

                        //var destinationPath = @"C:\db\CIF.json";
                        //File.WriteAllText(destinationPath, json);

                        //API Integration Save Imported file columns to database



                        if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        {
                            MessageBox.Show("Error", "Error", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Done importing", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //var responseMessageIfSuccess = await httpResponseMessage.Content.ReadAsStringAsync();
                        }

                        //End of API Integration Save Imported file columns to database
                    }
                }
            }

        }

        private void btOpenSched_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream inputStream;

                //Get the path of specified file
                filePath = openFileDialog.FileName;
                try
                {
                    inputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _ = MessageBox.Show("Please close the file first.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var connectionString = $@"
                Provider=Microsoft.ACE.OLEDB.12.0;
                Data Source={filePath};
                Extended Properties=""Excel 12.0 Xml;HDR=YES""
                ";

                using (var conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM [Sheet1$]";

                    using (var rdr = cmd.ExecuteReader())
                    {

                        //LINQ query - when executed will create anonymous objects for each row
                        var query = rdr.Cast<DbDataRecord>().Select(row => new
                        {
                            num = row[0],
                            Loan_num = row[1],
                            Acctnumber = row[2],
                            DateAvailed = row[3],
                            Date = row[4],
                            Principal = row[5],
                            Interest = row[6],
                            Payment = row[7],
                            OutstandingPrincipal = row[8],
                            OutstandingBalance = row[9],
                            Status = row[10],
                            DateofPayment = row[11],
                            MonthlyPayment = row[12],
                            currPrinPaid = row[13],
                            currIntePaid = row[14],
                            prevPrinPaid = row[15],
                            prevIntePaid = row[16],
                            Latepenalty = row[17],
                            ORnumber = row[18],
                            Transno = row[19],
                            BillingStatus = row[20],
                            unpaidPrin = row[21],
                            unpaidInte = row[22],
                            unpaidPenalty = row[23],
                            PostingDate = row[24],
                            ReferenceNo = row[25],
                            Remarks = row[26],
                            HoldingFee = row[27]
                        });

                        //Generates JSON from the LINQ query
                        var json = JsonConvert.SerializeObject(query);

                        var destinationPath = @"C:\db\Sched.json";
                        File.WriteAllText(destinationPath, json);
                    }
                }
                _ = MessageBox.Show("Done importing", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void FrmImport_Load(object sender, EventArgs e)
        {

        }
    }

    public class HappyPathRequest
    {

        public string num { get; set; } = string.Empty;
        public string LoanType { get; set; } = string.Empty;
        public string Acctnumber { get; set; } = string.Empty;
        public string CompanyCode { get; set; } = string.Empty;
        public string BorrowerName { get; set; } = string.Empty;
        public string BorrowerFirstName { get; set; } = string.Empty;
        public string BorrowerMiddleName { get; set; } = string.Empty;
        public string DatePrepared { get; set; } = string.Empty;
        public string LoanAmount { get; set; } = string.Empty;
        public string InterestRate { get; set; } = string.Empty;
        public string EffectiveYield { get; set; } = string.Empty;
        public string Term { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public string PurposeSpecify { get; set; } = string.Empty;
        public string TermDesired { get; set; } = string.Empty;
        public string Months { get; set; } = string.Empty;
        public string Vesting { get; set; } = string.Empty;
        public string EmploymentEmployer { get; set; } = string.Empty;

        public string EmployeeNumber { get; set; } = string.Empty;

        public string EmploymentPrePosition { get; set; } = string.Empty;

        public string EmploymentDept { get; set; } = string.Empty;

        public string EmploymentPlaceBranch { get; set; } = string.Empty;

        public string DateHired { get; set; } = string.Empty;

        public string Nationality { get; set; } = string.Empty;

        public string AppLengthService { get; set; } = string.Empty;

        public string AppMonthlySalary { get; set; } = string.Empty;

        public string EmploymentImmediateHead { get; set; } = string.Empty;

        public string EmploymentPositionHead { get; set; } = string.Empty;

        public string EmploymentTelnum { get; set; } = string.Empty;

        public string Sex { get; set; } = string.Empty;

        public string BirthDate { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string BorrowerTelno { get; set; } = string.Empty;

        public string AppCellphone { get; set; } = string.Empty;

        public string BorrowerProvincialAdd { get; set; } = string.Empty;

        public string LengthStayPer { get; set; } = string.Empty;

        public string BorrowerAddress { get; set; } = string.Empty;

        public string LengthStapHome { get; set; } = string.Empty;

        public string ResidenceType { get; set; } = string.Empty;

        public string HowMuch { get; set; } = string.Empty;

        public string SpouseName { get; set; } = string.Empty;

        public string SpouseBirthDate { get; set; } = string.Empty;


        public string SpousePreEmp { get; set; } = string.Empty;

        public string SpouseAddress { get; set; } = string.Empty;

        public string SpouseLengthService { get; set; } = string.Empty;

        public string SpousePhone { get; set; } = string.Empty;

        public string SpousePrePos { get; set; } = string.Empty;

        public string SpouseSalary { get; set; } = string.Empty;

        public string NoChildren { get; set; } = string.Empty;

        public string YoungestChild { get; set; } = string.Empty;

        public string School { get; set; } = string.Empty;

        public string Course { get; set; } = string.Empty;

        public string SchoolYear { get; set; } = string.Empty;

        public string NearestRelative { get; set; } = string.Empty;

        public string RelativeAddress { get; set; } = string.Empty;

        public string Relationship { get; set; } = string.Empty;


        public string RelHomePhone { get; set; } = string.Empty;

        public string RelCellphone { get; set; } = string.Empty;

        public string BorrowerTin { get; set; } = string.Empty;

        public string BorrowerSSS { get; set; } = string.Empty;

        public string ResCertNo { get; set; } = string.Empty;

        public string PlaceIssue { get; set; } = string.Empty;

        public string DateIssue { get; set; } = string.Empty;

        public string Remarks { get; set; } = string.Empty;

        public string DocSubmitted { get; set; } = string.Empty;

        public string Tag { get; set; } = string.Empty;

        public string SourceApplication { get; set; } = string.Empty;

        public string Agent { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Agency { get; set; } = string.Empty;

        public string YearsOFW { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string SpouseEmail { get; set; } = string.Empty;

        public string SourceIncome { get; set; } = string.Empty;

        public string SpouseSourceIncome { get; set; } = string.Empty;

        public string SpouseCitizenship { get; set; } = string.Empty;

        public string EmployeeStatus { get; set; } = string.Empty;

        public string EmployeeStatusOthers { get; set; } = string.Empty;






    }

    public class HappyPathForPNRequest
    {
        public string num { get; set; } = string.Empty;
        public string Acctnumber { get; set; } = string.Empty;
        public string Loannumber { get; set; } = string.Empty;
        public string DateSetup { get; set; } = string.Empty;
        public string DateAvailed { get; set; } = string.Empty;
        public string DateMaturity { get; set; } = string.Empty;
        public string LoanAmount { get; set; } = string.Empty;
        public string TotalInterest { get; set; } = string.Empty;
        public string TotalPrincipal { get; set; } = string.Empty;
        public string TotalBalance { get; set; } = string.Empty;
        public string InterestRate { get; set; } = string.Empty;
        public string Term { get; set; } = string.Empty;
        public string Rateperperiod { get; set; } = string.Empty;
        public string Typeofpayment { get; set; } = string.Empty;
        public string StatusAD { get; set; } = string.Empty;
        public string Computation { get; set; } = string.Empty;
        public string DocStamps { get; set; } = string.Empty;
        public string NotStamp { get; set; } = string.Empty;
        public string Service { get; set; } = string.Empty;
        public string ChattelMortgage { get; set; } = string.Empty;
        public string RealMortgage { get; set; } = string.Empty;
        public string Appraisal { get; set; } = string.Empty;
        public string OtherCharges { get; set; } = string.Empty;
        public string BrokenDayInterest { get; set; } = string.Empty;
        public string Setoff { get; set; } = string.Empty;
        public string CvNo { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public string ClassStatus { get; set; } = string.Empty;
        public string BrokenDay { get; set; } = string.Empty;
        public string Penalty { get; set; } = string.Empty;
        public string InterestUnpaid { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string MNCTransferDate { get; set; } = string.Empty;
        public string Released { get; set; } = string.Empty;
        public string BrokenDayStart { get; set; } = string.Empty;
    }
}
