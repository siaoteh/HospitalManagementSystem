using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Collections;

using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI.HtmlControls;

namespace HMS
{
    public partial class SendEmail : System.Web.UI.Page
    {
        static string UserName = null;
        static int totalCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginID"] == null)
            {
                Response.Redirect("~/TanAngie/LoginPage.aspx");
            }

            UserName = Session["LoginID"].ToString();
            lblDoctorName.Text = UserName;


            //Step 1: Create and Open Connection
            SqlConnection conHMS;
            String connStr = ConfigurationManager.ConnectionStrings["HMS"].ConnectionString;
            conHMS = new SqlConnection(connStr);
            conHMS.Open();

            String strAppointment;
            SqlCommand cmdAppointment;
            SqlDataReader dtr;

            //getAppointment Details
            strAppointment = "Select P.PatientName, P.PatientIC, A.AppointmentDate, A.AppointmentTime, A.AppointmentStatus, A.AppointmentID, P.PatientEmail, S.EmailID, S.EmailPW " +
                             "FROM Patient P, Appointment A, Staff S " +
                             "WHERE P.PatientID = A.PatientID AND " +
                             "A.StaffID = S.StaffID AND " +
                             "S.LoginID = '" + UserName + "' AND " +
                             "A.StaffID = " + " (SELECT StaffID FROM Staff WHERE LoginID ='" + UserName + "') " +
                             "ORDER BY SUBSTRING(A.AppointmentID,3,4) DESC";

            cmdAppointment = new SqlCommand(strAppointment, conHMS);
            //cmdAppointment.Parameters.AddWithValue("@LoginID", UserName);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdAppointment);
            da.Fill(ds);
            totalCount = ds.Tables[0].Rows.Count;

            //lblPage.Text = Convert.ToString(totalCount.ToString());

            //rptPaging.DataSource = ds;
            //rptPaging.DataBind();

            bindData();

            //dtr = cmdAppointment.ExecuteReader();
            //if (dtr.HasRows)
            //{
            //    if (dtr.Read())
            //    {
            //        PatientName = dtr["PatientName"].ToString();
            //        PatientIC = dtr["PatientIC"].ToString();
            //        AppointmentDate = dtr["AppointmentDate"].ToString();
            //        AppointmentTime = dtr["AppointmentTime"].ToString();
            //    }
            //}
            //dtr.Close();
            if (lblAppointmentStatus.Text.Equals("Approved"))
            {
                
            }
        }

        protected void Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rpt = sender as Repeater; // Get the Repeater control object.

            // If the Repeater contains no data.
            if (rpt != null && rpt.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    // Show the Error Label (if no data is present).
                    System.Web.UI.WebControls.Label ErrorMessage = e.Item.FindControl("ErrorMessage") as System.Web.UI.WebControls.Label;
                    if (ErrorMessage != null)
                    {
                        ErrorMessage.Visible = true;
                    }
                }
            }
            //foreach (RepeaterItem item2 in rptPaging.Items)
            //{
                //LinkButton btn = (LinkButton)sender;
                //var item = (RepeaterItem)btn.NamingContainer;
               // HiddenField appointmentStatus = (HiddenField)item2.FindControl("hiddenAppointmentStatus");

               // if (appointmentStatus.Value.Equals("Pending"))
                //{
                    //LinkButton test = (LinkButton)item2.FindControl("sendEmail");
                    //test.Attributes.Add("style", "background-color:White;");
                //}
                //else if (appointmentStatus.Value.Equals("Rejected"))
                //{
                //    LinkButton test = (LinkButton)item2.FindControl("sendEmail");
                //    test.Attributes.Add("style", "background-color:Red;");
                //}
                //else if (appointmentStatus.Value.Equals("Approved"))
                //{
                //    LinkButton test = (LinkButton)item2.FindControl("sendEmail");
                //    test.Attributes.Add("style", "background-color:Lightgrey;");
                //}
            //}
        }

        public void bindData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HMS"].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            DataSet ds = new DataSet();
            String sql = "Select P.PatientName, P.PatientIC, A.AppointmentDate, A.AppointmentTime, A.AppointmentStatus, A.AppointmentID, P.PatientEmail, S.EmailID, S.EmailPW " +
                         "FROM Patient P, Appointment A, Staff S " +
                         "WHERE P.PatientID = A.PatientID AND " +
                         "A.StaffID = S.StaffID AND " +
                         "S.LoginID = '" + UserName + "' AND " +
                         "A.StaffID = " + " (SELECT StaffID FROM Staff WHERE LoginID ='" + UserName + "') " +
                         "ORDER BY SUBSTRING(A.AppointmentID,3,4) DESC";
            int val = Convert.ToInt16(txtHidden.Value);
            if (val <= 0)
                val = 0;
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            adapter.Fill(ds, val, 4, "authors");
            connection.Close();
            rptPaging.DataSource = ds;
            rptPaging.DataBind();

            int totalPages = totalCount / 4;

            //lblPage.Text = "You are at page number '" + CurrentPageNumber + "' from total of '" + totalPages + "' pages";

            if (val <= 0)
            {
                lnkPrevious.Visible = false;
                lnkNext.Visible = true;
            }

            if (val >= 4)
            {
                lnkPrevious.Visible = true;
                lnkNext.Visible = true;
            }

            if ((val + 4) >= totalCount)
            {
                lnkNext.Visible = false;
            }
        }

        public int CurrentPageNumber
        {
            set{
                ViewState["PageNumber"] = value;
            }
            get{
                if (ViewState["PageNumber"] != null){
                    return Convert.ToInt32(ViewState["PageNumber"]);
                }
                else{
                    return 1;
                }
            }
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            txtHidden.Value = Convert.ToString(Convert.ToInt16(txtHidden.Value) - 4);
            bindData();
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            txtHidden.Value = Convert.ToString(Convert.ToInt16(txtHidden.Value) + 4);
            bindData();
        }

        protected void clickEmail(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField email = (HiddenField)item.FindControl("findEmail");
            HiddenField emailID = (HiddenField)item.FindControl("hiddenEmail");
            HiddenField emailPW = (HiddenField)item.FindControl("hiddenPW");
            HiddenField appointmentID = (HiddenField)item.FindControl("hiddenAppointmentID");
            HiddenField appointmentStatus = (HiddenField)item.FindControl("hiddenAppointmentStatus");
            txtTo.Text = email.Value;
            txtEmail.Text = emailID.Value;
            txtPassword.Text = emailPW.Value;

            lblAppointmentID.Text = appointmentID.Value;
            lblAppointmentStatus.Text = appointmentStatus.Value;

            txtSubject.Text = "";
            txtBody.Text = "";

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            String myMail = "jinnyboy0628@gmail.com";
            String myPass = "chash jin";
            string emailTo = null;
            emailTo = txtTo.Text;
            DialogResult dialogResult = MessageBox.Show("Are you sure want send this email?", "New Email", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!emailTo.Equals(""))
                {
                    MailMessage mm = new MailMessage(myMail, txtTo.Text);                                //sender email, receiver
                    mm.Subject = txtSubject.Text;           //email subject
                    mm.Body = txtBody.Text;                 //body content
                    if (fuAttachment.HasFile)
                    {
                        string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);                   //attachment file
                        mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));      //attachment file
                    }

                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(myMail, myPass);     //doctor email n password
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    ClientScript.RegisterStartupScript(GetType(), "(alert);", "alert('Email sent.');", true);

                    txtSubject.Text = "";
                    txtBody.Text = "";

                    
                }
                else
                {
                    MessageBox.Show("Please select an email to send.");
                }
            }

            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("No email sent.");
                Response.Redirect("~/TanDingKang/AppointmentHomePage.aspx");
            }

            string appStatus = lblAppointmentStatus.Text;
            if (appStatus.Equals("Pending") || appStatus.Equals("Approved"))
            {
                string status = ddlStatus.SelectedItem.Text;
                //Step 1: Create and Open Connection
                SqlConnection conHMS;
                String connStr = ConfigurationManager.ConnectionStrings["HMS"].ConnectionString;
                conHMS = new SqlConnection(connStr);
                conHMS.Open();

                String strUpdate;
                SqlCommand cmdUpdate;
                SqlDataReader dtr;

                //getPatientID
                strUpdate = "UPDATE Appointment SET AppointmentStatus = @AppointmentStatus " +
                           "WHERE AppointmentID = '" + lblAppointmentID.Text + "'";

                cmdUpdate = new SqlCommand(strUpdate, conHMS);
                cmdUpdate.Parameters.AddWithValue("@AppointmentStatus", status);

                //int n = cmdCancel.ExecuteNonQuery();
                //if (n > 0)
                //    MessageBox.Show("Appointment details cancelled successfully");
                //else
                //    MessageBox.Show("Sorry, cancel failed.");

                dtr = cmdUpdate.ExecuteReader();
                dtr.Close();
                conHMS.Close();
            }
        }
    }
}