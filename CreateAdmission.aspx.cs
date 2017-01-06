using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace HMS
{
    public partial class CreateAdmission : System.Web.UI.Page
    {
        static string visitationID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCalendar.Text = DateTime.Now.ToShortDateString(); 
            }                              
        }
        
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now)
            {
                e.Day.IsSelectable = false;
                e.Cell.Enabled = false;
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtPatientID.Text = "";
            txtPatientName.Text = "";
            txtPatientIC.Text = "";
            txtPatientContactNo.Text = "";
            txtMedicalCondition.Text = "";
            Calendar1.SelectedDates.Clear();
            txtCalendar.Text = DateTime.Now.ToShortDateString();
            ddlStaff.Items.Clear();
            ddlWardType.Items.Clear();
            txtWardNo.Text = "";
            ddlBedNo.Items.Clear();
            txtNoOfDaysStay.Text = "";
            btnFind.Enabled = false;
            btnCreateAdmission.Enabled = false;
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtPatientID.Text == "" || txtPatientID.Text == String.Empty)
            {
                MessageBox.Show("Patient ID cannot be null or empty!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {            
                SqlConnection conPatient;
                string connStr = ConfigurationManager.ConnectionStrings["HMS"].ConnectionString;
                conPatient = new SqlConnection(connStr);
                conPatient.Open();

                /////////////////////////get patient's data/////////////////////////
                string strRetrieve = "";
                SqlCommand cmdRetrieve;
                strRetrieve = "SELECT Patient.PatientID, PatientName, PatientIC, PatientContactNo, MedicalCondition,"+
                    " VisitationID FROM Patient, Visitation WHERE VisitationID NOT IN"+
                    " (SELECT VisitationID FROM Prescription) AND Visitation.PatientID =@PatientID AND VisitationID"+
                    " NOT IN (SELECT VisitationID FROM Admission) AND Visitation.PatientID =Patient.PatientID";
            
                cmdRetrieve = new SqlCommand(strRetrieve, conPatient);
                cmdRetrieve.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
                SqlDataReader dtr;
                dtr = cmdRetrieve.ExecuteReader();
                //----------------------------------------------------------------//
                if (dtr.HasRows)
                {
                    if (dtr.Read())
                    {   ///////////display data on the textboxes/////////////////////
                        txtPatientName.Text = "" + dtr["PatientName"];
                        txtPatientIC.Text = "" + dtr["PatientIC"];
                        txtPatientContactNo.Text = "" + dtr["PatientContactNo"];
                        txtMedicalCondition.Text = "" + dtr["MedicalCondition"];
                        //---------------------------------------------------------// 
                        visitationID = "" + dtr["VisitationID"];
                        dtr.Close();  
                    }
                    btnFind.Enabled = true;
                }
                else
                {/////////////////clear all input///////////////////////                
                    txtPatientID.Text = "";
                    txtPatientName.Text = "";
                    txtPatientIC.Text = "";
                    txtPatientContactNo.Text = "";
                    txtMedicalCondition.Text = "";
                    Calendar1.SelectedDates.Clear();
                    txtCalendar.Text = DateTime.Now.ToShortDateString();
                    ddlStaff.Items.Clear();
                    ddlWardType.Items.Clear();
                    txtWardNo.Text = "";
                    ddlBedNo.Items.Clear();
                    txtNoOfDaysStay.Text = "";
                 //----------------------------------------------------//

                    MessageBox.Show("Patient has not visited.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                conPatient.Close();
            }     
        }

        protected void ddlWardType_SelectedIndexChanged(object sender, EventArgs e)
        {           
            ////////////////display wardNo based on wardType and medical condition////////////////////
            if (ddlWardType.SelectedItem.Text.Equals("Standard") && txtMedicalCondition.Text.Equals("Dengue"))
            {
                txtWardNo.Text = "1000";
            }
            else if (ddlWardType.SelectedItem.Text.Equals("Standard") && txtMedicalCondition.Text.Equals("Fever"))
            {
                txtWardNo.Text = "1001";
            }
            else if (ddlWardType.SelectedItem.Text.Equals("Standard") && txtMedicalCondition.Text.Equals("Pregnant"))
            {
                txtWardNo.Text = "1002";
            }
            else if (ddlWardType.SelectedItem.Text.Equals("Semi Private") && txtMedicalCondition.Text.Equals("Dengue"))
            {
                txtWardNo.Text = "2000";
            }
            else if (ddlWardType.SelectedItem.Text.Equals("Semi Private") && txtMedicalCondition.Text.Equals("Fever"))
            {
                txtWardNo.Text = "2001";
            }
            else if (ddlWardType.SelectedItem.Text.Equals("Semi Private") && txtMedicalCondition.Text.Equals("Pregnant"))
            {
                txtWardNo.Text = "2002";
            }
            else if (ddlWardType.SelectedItem.Text.Equals("Private") && txtMedicalCondition.Text.Equals("Dengue"))
            {
                txtWardNo.Text = "3000";
            }
            else if (ddlWardType.SelectedItem.Text.Equals("Private") && txtMedicalCondition.Text.Equals("Fever"))
            {
                txtWardNo.Text = "3001";
            }
            else if (ddlWardType.SelectedItem.Text.Equals("Private") && txtMedicalCondition.Text.Equals("Pregnant"))
            {
                txtWardNo.Text = "3002";
            }
            else
            {
                txtWardNo.Text = "";
            }
            
            //-------------------------------------------------------------------------------------//  

            SqlConnection conBed;
            string connStr = ConfigurationManager.ConnectionStrings["HMS"].ConnectionString;
            conBed = new SqlConnection(connStr);
            conBed.Open();
            /////////////////////////////////////display available bedNo////////////////////////////////
            string strRetrieve = "";
            SqlCommand cmdRetrieve;
            strRetrieve = "SELECT BedNo FROM Bed, Ward WHERE Ward.WardNo = Bed.WardNo AND BedStatus ="+
                " 'Available' AND Bed.WardNo = @WardNo";

            cmdRetrieve = new SqlCommand(strRetrieve, conBed);
            cmdRetrieve.Parameters.AddWithValue("@WardNo", txtWardNo.Text);

            SqlDataReader dtr4;
            dtr4 = cmdRetrieve.ExecuteReader();
            ddlBedNo.DataTextField = "BedNo";
            ddlBedNo.DataSource = dtr4;
            ddlBedNo.DataBind();

            dtr4.Close();
            conBed.Close();
            btnCreateAdmission.Enabled = true;
            //----------------------------------------------------------------------------------------//
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtCalendar.Text = Calendar1.SelectedDate.ToShortDateString();
        }

        protected void btnCreateAdmission_Click(object sender, EventArgs e)
        {
            if (ddlWardType.SelectedItem.Text.Equals("--please select--"))
            {
                MessageBox.Show("You have not selected the ward type.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                SqlConnection conAdmission;
                string connStr = ConfigurationManager.ConnectionStrings["HMS"].ConnectionString;
                conAdmission = new SqlConnection(connStr);
                conAdmission.Open();

                //////////check whether patient has already admitted///////////
                string strRetrieve3 = "";
                SqlCommand cmdRetrieve3;
                strRetrieve3 = "SELECT VisitationID FROM Admission,Patient WHERE VisitationID = '"+visitationID+"' AND PatientStatus = 'Admitted' AND PatientID = '"+txtPatientID.Text+"'";
                cmdRetrieve3 = new SqlCommand(strRetrieve3, conAdmission);
                SqlDataReader dtr3;
                dtr3 = cmdRetrieve3.ExecuteReader();
                if (dtr3.Read())
                {                    
                    MessageBox.Show("Patient has already admitted.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                }
                else
                {
                    dtr3.Close();
                    //////////////get nurse id///////////////
                    string strRetrieve2 = "";
                    SqlCommand cmdRetrieve2;
                    strRetrieve2 = "SELECT StaffID FROM Staff WHERE StaffName = '" + ddlStaff.SelectedItem.Text + "'";

                    cmdRetrieve2 = new SqlCommand(strRetrieve2, conAdmission);
                    SqlDataReader dtr2;
                    dtr2 = cmdRetrieve2.ExecuteReader();
                    string staffID = "";
                    if (dtr2.Read())
                    {
                        staffID = dtr2["StaffID"].ToString();
                        dtr2.Close();
                    }

                    /////////get the last admission primay key////////
                    string strRetrieve = "";
                    SqlCommand cmdRetrieve;
                    strRetrieve = "SELECT TOP 1 AdmissionNo FROM Admission ORDER BY AdmissionNo DESC";
                    cmdRetrieve = new SqlCommand(strRetrieve, conAdmission);
                    SqlDataReader dtr;
                    dtr = cmdRetrieve.ExecuteReader();
                    if (dtr.Read())
                    {
                        int previousID = Convert.ToInt32(dtr["AdmissionNo"]);
                        int currentID = previousID + 1;
                        dtr.Close();

                        string strInsert = "";
                        SqlCommand cmdInsert;
                        if (txtCalendar.Text.Equals(DateTime.Now.ToShortDateString()))
                        {
                            ////////////////insert into admission table for today's date///////////////////
                            strInsert = "INSERT INTO Admission(AdmissionNo,AdmissionDate," +
                            "AdmissionStatus,NoOfDaysStay,VisitationID,WardNo,BedNo,StaffID) VALUES(@No," +
                            "@Date,@Status,@NoOfDays,@VisitationID,@WardNo,@BedNo,@StaffID)";

                            cmdInsert = new SqlCommand(strInsert, conAdmission);
                            cmdInsert.Parameters.AddWithValue("@Status", "Admitted");
                        }
                        else
                        {
                            ////////////////insert into admission table for future date///////////////////
                            strInsert = "INSERT INTO Admission(AdmissionNo,ReservationDate," +
                            "AdmissionStatus,NoOfDaysStay,VisitationID,WardNo,BedNo,StaffID) VALUES(@No," +
                            "@Date,@Status,@NoOfDays,@VisitationID,@WardNo,@BedNo,@StaffID)";

                            cmdInsert = new SqlCommand(strInsert, conAdmission);
                            cmdInsert.Parameters.AddWithValue("@Status", "Reserved");
                        }

                        cmdInsert.Parameters.AddWithValue("@No", currentID);
                        cmdInsert.Parameters.AddWithValue("@Date", txtCalendar.Text);
                        cmdInsert.Parameters.AddWithValue("@NoOfDays", txtNoOfDaysStay.Text);
                        cmdInsert.Parameters.AddWithValue("@VisitationID", visitationID);
                        cmdInsert.Parameters.AddWithValue("@WardNo", txtWardNo.Text);
                        cmdInsert.Parameters.AddWithValue("@BedNo", ddlBedNo.SelectedItem.Text);
                        cmdInsert.Parameters.AddWithValue("@StaffID", staffID);

                        int n = cmdInsert.ExecuteNonQuery();
                        if (n > 0)
                        {
                            MessageBox.Show("Admission successfully recorded.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Admission fail to be recorded.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }                        

                        //////////////////////////////update bed status/////////////////////////
                        string strUpdate = "";
                        SqlCommand cmdUpdate;
                        strUpdate = "UPDATE Bed SET BedStatus = 'Not available' WHERE BedNo = " +
                            " '" + ddlBedNo.SelectedItem.Text + "'";
                        cmdUpdate = new SqlCommand(strUpdate, conAdmission);
                        int n2 = cmdUpdate.ExecuteNonQuery();
                        if (n2 > 0)
                        {
                            MessageBox.Show("Bed status is updated.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Bed status update failed.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        ///////////////////////update patient status///////////////////////////
                        string strUpdate2 = "";
                        SqlCommand cmdUpdate2;
                        strUpdate2 = "UPDATE Patient SET PatientStatus = 'Admitted' WHERE PatientID =" +
                            " '" + txtPatientID.Text + "'";
                        cmdUpdate2 = new SqlCommand(strUpdate2, conAdmission);
                        int n3 = cmdUpdate2.ExecuteNonQuery();
                        if (n3 > 0)
                        {
                            MessageBox.Show("Patient status is updated.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Patient status update failed.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        txtPatientID.Text = "";
                        txtPatientName.Text = "";
                        txtPatientIC.Text = "";
                        txtPatientContactNo.Text = "";
                        txtMedicalCondition.Text = "";
                        Calendar1.SelectedDates.Clear();
                        txtCalendar.Text = DateTime.Now.ToShortDateString();
                        ddlStaff.Items.Clear();
                        ddlWardType.Items.Clear();
                        txtWardNo.Text = "";
                        ddlBedNo.Items.Clear();
                        txtNoOfDaysStay.Text = "";
                        btnFind.Enabled = false;
                        btnCreateAdmission.Enabled = false;
                    }                     
                }                                  
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            ddlStaff.Items.Clear();
            ddlWardType.Items.Clear();
            txtWardNo.Text = "";
            ddlBedNo.Items.Clear();
           
             int number;
                bool result = Int32.TryParse(txtNoOfDaysStay.Text,out number);
                if (!result)
                {
                    MessageBox.Show("No. of days stay is either empty or not numeric!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                  
                }
                else
                {
                    if (Convert.ToInt32(txtNoOfDaysStay.Text) < 1)
                    {
                        MessageBox.Show("No. of days stay cannot be less than 1!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        SqlConnection conAdmission;
                        string connStr = ConfigurationManager.ConnectionStrings["HMS"].ConnectionString;
                        conAdmission = new SqlConnection(connStr);
                        conAdmission.Open();

                        string strRetrieve2 = "";
                        SqlCommand cmdRetrieve2;
                        ///////////display nurse according to the patient's medical condition///////////
                        if (txtMedicalCondition.Text.Equals("Fever") || txtMedicalCondition.Text.Equals("Dengue"))
                        {
                            strRetrieve2 = "SELECT DISTINCT StaffName FROM Staff, Department WHERE" +
                                " Staff.DepartmentID = 'DP003' AND Position='Nurse' AND StaffStatus = 'Active'";
                        }
                        else
                        {
                            strRetrieve2 = "SELECT DISTINCT StaffName FROM Staff, Department WHERE" +
                                " Staff.DepartmentID='DP002' AND Position='Nurse' AND StaffStatus = 'Active'";
                        }
                        cmdRetrieve2 = new SqlCommand(strRetrieve2, conAdmission);
                        SqlDataReader dtr2;
                        dtr2 = cmdRetrieve2.ExecuteReader();
                        ddlStaff.DataSource = dtr2;
                        ddlStaff.DataTextField = "StaffName";
                        ddlStaff.DataBind();
                        dtr2.Close();

                        ddlWardType.AppendDataBoundItems = true;
                        ddlWardType.Items.Add(new ListItem("--please select--", "-1"));
                        //////////check whether all admissionstatus is 'discharged' / 'cancelled'////////
                        string strRetrieve4 = "";
                        SqlCommand cmdRetrieve4;
                        strRetrieve4 = "SELECT COUNT(AdmissionNo) as Total FROM Admission";
                        cmdRetrieve4 = new SqlCommand(strRetrieve4, conAdmission);
                        SqlDataReader dtr4;
                        dtr4 = cmdRetrieve4.ExecuteReader();
                        int countAll = 0;
                        if (dtr4.Read())
                        {
                            countAll = Convert.ToInt32(dtr4["Total"]);
                            dtr4.Close();
                        }

                        string strRetrieve5 = "";
                        SqlCommand cmdRetrieve5;
                        strRetrieve5 = "SELECT COUNT(AdmissionNo) as Total FROM Admission WHERE " +
                            " AdmissionStatus = 'Discharged' OR AdmissionStatus = 'Cancelled'";
                        cmdRetrieve5 = new SqlCommand(strRetrieve5, conAdmission);
                        SqlDataReader dtr5;
                        dtr5 = cmdRetrieve5.ExecuteReader();
                        int countStatus = 0;
                        if (dtr5.Read())
                        {
                            countStatus = Convert.ToInt32(dtr5["Total"]);
                            dtr5.Close();
                        }

                        if (countAll == countStatus)
                        {
                            string strRetrieve6 = "";
                            SqlCommand cmdRetrieve6;
                            strRetrieve6 = "SELECT DISTINCT WardType FROM Ward, Bed WHERE Ward.WardNo = Bed.WardNo";
                            cmdRetrieve6 = new SqlCommand(strRetrieve6, conAdmission);
                            SqlDataReader dtr6;
                            dtr6 = cmdRetrieve6.ExecuteReader();
                            ddlWardType.DataSource = dtr6;
                            ddlWardType.DataTextField = "WardType";
                            ddlWardType.DataBind();
                            dtr6.Close();
                        }
                        else
                        {
                            ////////convert string to date///////
                            DateTime firstDate = DateTime.Parse(txtCalendar.Text);
                            DateTime lastDate = firstDate.AddDays(Convert.ToInt32(txtNoOfDaysStay.Text));

                            ////////check whether admissiondate or reservedate is null/////////
                            string strRetrieve1 = "";
                            SqlCommand cmdRetrieve1;
                            strRetrieve1 = "SELECT AdmissionStatus FROM Admission WHERE AdmissionStatus = 'Admitted' AND" +
                                " AdmissionStatus <> 'Discharged' AND AdmissionStatus <> 'Cancelled'";
                            cmdRetrieve1 = new SqlCommand(strRetrieve1, conAdmission);
                            SqlDataReader dtr1;
                            dtr1 = cmdRetrieve1.ExecuteReader();

                            if (dtr1.HasRows)
                            {
                                dtr1.Close();
                                //////find from 'admitted'////////
                                string strRetrieve3 = "";
                                SqlCommand cmdRetrieve3;
                                /////display available wardtype/////
                                strRetrieve3 = "SELECT DISTINCT WardType FROM Ward, Bed WHERE Ward.WardNo = Bed.WardNo AND" +
                                    " Bed.BedNo NOT IN (SELECT Bed.BedNo FROM Bed, Admission WHERE (('" + firstDate.ToShortDateString() + "'" +
                                    " BETWEEN AdmissionDate AND convert(varchar(10),DATEADD(day,NoOfDaysStay-1," +
                                    " convert(datetime,AdmissionDate,103)),103)) OR ('" + lastDate.AddDays(-1).ToShortDateString() + "'" +
                                    " BETWEEN AdmissionDate AND convert(varchar(10),DATEADD(day,NoOfDaysStay-1," +
                                    " convert(datetime,AdmissionDate,103)),103)) OR ((AdmissionDate BETWEEN" +
                                    " '" + firstDate.ToShortDateString() + "' AND '" + lastDate.AddDays(-1).ToShortDateString() + "')" +
                                    " AND (convert(varchar(10),DATEADD(day,NoOfDaysStay-1,convert(datetime,AdmissionDate,103)),103)" +
                                    " BETWEEN '" + firstDate.ToShortDateString() + "' AND '" + lastDate.AddDays(-1).ToShortDateString() + "')))" +
                                    " AND (AdmissionStatus <> 'Disharged' OR AdmissionStatus <> 'Cancelled') AND Admission.BedNo = Bed.BedNo)";
                                cmdRetrieve3 = new SqlCommand(strRetrieve3, conAdmission);
                                SqlDataReader dtr3;
                                dtr3 = cmdRetrieve3.ExecuteReader();
                                ddlWardType.DataSource = dtr3;
                                ddlWardType.DataTextField = "WardType";
                                ddlWardType.DataBind();
                                dtr3.Close();
                            }
                            else
                            {
                                dtr1.Close();
                                //////find from 'reserved'////////
                                string strRetrieve3 = "";
                                SqlCommand cmdRetrieve3;
                                /////display available wardtype/////
                                strRetrieve3 = "SELECT DISTINCT WardType FROM Ward, Bed WHERE Ward.WardNo = Bed.WardNo AND" +
                                    " Bed.BedNo NOT IN (SELECT Bed.BedNo FROM Bed, Admission WHERE (('" + firstDate.ToShortDateString() + "'" +
                                    " BETWEEN ReservationDate AND convert(varchar(10),DATEADD(day,NoOfDaysStay-1," +
                                    " convert(datetime,ReservationDate,103)),103)) OR ('" + lastDate.AddDays(-1).ToShortDateString() + "'" +
                                    " BETWEEN ReservationDate AND convert(varchar(10),DATEADD(day,NoOfDaysStay-1," +
                                    " convert(datetime,ReservationDate,103)),103)) OR ((ReservationDate BETWEEN" +
                                    " '" + firstDate.ToShortDateString() + "' AND '" + lastDate.AddDays(-1).ToShortDateString() + "')" +
                                    " AND (convert(varchar(10),DATEADD(day,NoOfDaysStay-1,convert(datetime,ReservationDate,103)),103)" +
                                    " BETWEEN '" + firstDate.ToShortDateString() + "' AND '" + lastDate.AddDays(-1).ToShortDateString() + "')))" +
                                    " AND (AdmissionStatus <> 'Disharged' OR AdmissionStatus <> 'Cancelled') AND Admission.BedNo = Bed.BedNo)";
                                cmdRetrieve3 = new SqlCommand(strRetrieve3, conAdmission);
                                SqlDataReader dtr3;
                                dtr3 = cmdRetrieve3.ExecuteReader();
                                ddlWardType.DataSource = dtr3;
                                ddlWardType.DataTextField = "WardType";
                                ddlWardType.DataBind();
                                dtr3.Close();
                            }
                        }
                        conAdmission.Close();
                    }
                }
        }       
    }
}