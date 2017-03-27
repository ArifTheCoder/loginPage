using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            con.Open();
            string checkUser = "Select count(*) from UserData where UserName = '"+TextBoxUN.Text+"'";
            SqlCommand cmd = new SqlCommand(checkUser, con);

            int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            if (temp == 1)
            {
                Response.Write("User already exists!");

            }

            con.Close();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            con.Open();
            string insertQuerry = "insert into UserData (UserName, Email, Password, Country) values (@uName, @email, @password, @country)";
            SqlCommand cmd = new SqlCommand(insertQuerry, con);

            cmd.Parameters.AddWithValue("@uName", TextBoxUN.Text);
            cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text);
            cmd.Parameters.AddWithValue("@password", TextBoxPass.Text);
            cmd.Parameters.AddWithValue("@country", DropDownListCountry.SelectedItem.ToString());

            cmd.ExecuteNonQuery();
            Response.Redirect("Manager.aspx");
            Response.Write("Registration is successful");

            con.Close();
        }
        catch (Exception ex)
        {
            Response.Write("Error:" + ex.ToString());
        }

    }
}