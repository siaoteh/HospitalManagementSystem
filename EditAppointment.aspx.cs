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

namespace HMS
{
    public partial class EditAppointment : System.Web.UI.Page
    {
        //getPatientID
        static string patientID = null;
        static string currentDate = null;
        //static string untilDate = null;
        static string month = null;
        static string day = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginID"] == null)
            {
                Response.Redirect("~/TanAngie/LoginPage.aspx");
            }
            String UserName = Session["LoginID"].ToString();
            lblUserName.Text = UserName;
            Session["LoginID"] = Session["LoginID"].ToString();

            currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            month = currentDate.Substring(3, 2).ToString();
            day = currentDate.Substring(0, 2).ToString();

            
                //Step 1: Create and Open Connection
                SqlConnection conHMS;
                String connStr = ConfigurationManager.ConnectionStrings["HMS"].ConnectionString;
                conHMS = new SqlConnection(connStr);
                conHMS.Open();

                String strPatientID, strAppointment;
                SqlCommand cmdPatientID, cmdAppointment;
                SqlDataReader dtr, dtr2;

                //getPatientID
                strPatientID = "Select PatientID From Patient Where LoginID = @LoginID";
                cmdPatientID = new SqlCommand(strPatientID, conHMS);
                cmdPatientID.Parameters.AddWithValue("@LoginID", UserName);
                dtr = cmdPatientID.ExecuteReader();
                if (dtr.HasRows)
                {
                    if (dtr.Read())
                    {
                        patientID = dtr["PatientID"].ToString();
                    }
                }
                dtr.Close();
                //endOfPatientID

                strAppointment = "SELECT A.AppointmentID as 'Appointment ID', A.AppointmentType as 'Appointment Type', A.AppointmentDate as 'Appointment Date', " +
                     "A.AppointmentTime as 'Appointment Time', A.AppointmentStatus as 'Status', " +
                     "(SELECT S.StaffName FROM STAFF S WHERE S.StaffID = A.StaffID ) as 'Doctor Name' " +
                     "FROM Appointment A " +
                     "WHERE A.PatientID = @PatientID AND " +
                     " SUBSTRING(A.AppointmentDate,4,2) >= '" + month + "' AND " +
                    //" SUBSTRING(A.AppointmentDate,1,2) >= '" + day + "'"+
                    //" AND " +
                     "A.AppointmentStatus = 'Pending'";

                //new
                //strAppointment = "SELECT A.AppointmentID as 'Appointment ID', A.AppointmentType as 'Appointment Type', A.AppointmentDate as 'Appointmen Date', " +
                //     "A.AppointmentTime as 'Appointment Time', A.AppointmentStatus as 'Status', " +
                //     "(SELECT S.StaffName FROM STAFF S WHERE S.StaffID = A.StaffID ) as 'Doctor Name' " +
                //     "FROM Appointment A " +
                //     "WHERE A.PatientID = @PatientID AND " +
                //     " ( SUBSTRING(A.AppointmentDate,4,2) = '" + month + "'" + " AND " + " SUBSTRING(A.AppointmentDate,1,2) > '" + day + "'" + ")" + " OR " +
                //     " (  SUBSTRING(A.AppointmentDate,4,2) >= '" + month + "'" + " AND " + " SUBSTRING(A.AppointmentDate,1,2) <= '31'" + " AND " + " SUBSTRING(A.AppointmentDate,1,2) >= '1' )" +
                //    //" AND SUBSTRING(A.AppointmentDate,1,2) >= '" + day + "'" +
                //     " AND " +
                //     "A.AppointmentStatus = 'Pending'";

                //old
                //strAppointment = "SELECT A.AppointmentID as 'Appointment ID', A.AppointmentType as 'Appointment Type', A.AppointmentDate as 'Appointmen Date', " +
                //     "A.AppointmentTime as 'Appointment Time', A.AppointmentStatus as 'Status', " +
                //     "(SELECT S.StaffName FROM STAFF S WHERE S.StaffID = A.StaffID ) as 'Doctor Name' " +
                //     "FROM Appointment A " +
                //     "WHERE A.PatientID = @PatientID AND " +
                //     " SUBSTRING(A.AppointmentDate,4,2) >= '" + month + "' OR " + " (  SUBSTRING(A.AppointmentDate,4,2) >= '" + month + "'" + " AND " + " SUBSTRING(A.AppointmentDate,1,2) >= '" + day + "' )" +
                //     " AND SUBSTRING(A.AppointmentDate,1,2) >= '" + day + "'" +
                //     " AND " +
                //     "A.AppointmentStatus = 'Pending'";
                //substring 1,2 = day     SUBSTRING(A.AppointmentDate, 1, 2)
                //substring 4,2 = month   SUBSTRING(A.AppointmentDate, 4, 2)

                cmdAppointment = new SqlCommand(strAppointment, conHMS);
                cmdAppointment.Parameters.AddWithValue("@PatientID", patientID);
                dtr2 = cmdAppointment.ExecuteReader();

                //string appDate = null;
                //int subDay;
                //int subMonth;
                //int currentDay;
                //int currentMonth;
                //currentDay = int.Parse(currentDate.Substring(0, 2));
                //currentMonth = int.Parse(currentDate.Substring(3, 2));

                //if (dtr2.HasRows)
                //{
                //    if (dtr2.Read())
                //    {
                //        appDate = dtr2["AppointmentDate"].ToString();
                //        subDay = int.Parse(appDate.Substring(0, 2));
                //        subMonth = int.Parse(appDate.Substring(3, 2));

                //        if (subMonth >= currentMonth && subDay >= currentDay)
                //        {

                //        }
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("No Records Found.");
                //}

                gridAppointment.DataSource = dtr2;
                gridAppointment.DataBind();
                dtr2.Close();
            
        }

        static string checkjor;
        protected void gridAppointment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblAppointmentID.Text = gridAppointment.SelectedRow.Cells[2].Text;
            //lblAppointmentType.Text = gridAppointment.SelectedRow.Cells[3].Text;
            //lblDoctorName.Text = gridAppointment.SelectedRow.Cells[7].Text;
            //lblDate.Text = gridAppointment.SelectedRow.Cells[4].Text;
            //lblTime.Text = gridAppointment.SelectedRow.Cells[5].Text;
            //lblStatus.Text = gridAppointment.SelectedRow.Cells[6].Text;

            Session["DoctorName"] = gridAppointment.SelectedRow.Cells[7].Text;
            Session["AppointmentID"] = gridAppointment.SelectedRow.Cells[2].Text;
            Session["AppointmentType"] = gridAppointment.SelectedRow.Cells[3].Text;
            Session["AppointmentDate"] = gridAppointment.SelectedRow.Cells[4].Text;
            Session["AppointmentTime"] = gridAppointment.SelectedRow.Cells[5].Text;
            Session["AppointmentStatus"] = gridAppointment.SelectedRow.Cells[6].Text;

            string testing = checkjor;
            if (testing == "cancel")
            {
                Response.Redirect("~/TanDingKang/CancelAppointment.aspx");
            }
            else if (testing == "update")
            {
                Response.Redirect("~/TanDingKang/UpdateAppointment.aspx");
            }
        }

        protected void linkButtonUpdate(object sender, EventArgs e)
        {
            checkjor = "update";
        }

        protected void linkButtonCancel(object sender, EventArgs e)
        {
            checkjor = "cancel";
        }
    }
}