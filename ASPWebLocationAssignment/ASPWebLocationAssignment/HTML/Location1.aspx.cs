using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPWebLocationAssignment.HTML
{
    public partial class Location1 : System.Web.UI.Page
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        public void CountryDatas()
        {
            cn.Open();
            using(SqlCommand cmd= new SqlCommand("Sp_Country",cn))
            {
                SqlDataAdapter dr = new SqlDataAdapter(cmd);
                dr.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet dataSet = new DataSet();
                dr.Fill(dataSet);
                DataTable dt = dataSet.Tables[0];
                ddlCountry.DataSource = dt;
                ddlCountry.DataValueField = "countryId";
                ddlCountry.DataTextField = "countryName";
                ddlCountry.DataBind();
            }
            cn.Close();
        }
        public void StateData()
        {
            cn.Open();
            using (SqlCommand cmd1 = new SqlCommand("Sp_State", cn))
            {
                
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@countryId",ddlCountry.SelectedValue);
                //dr.GetFillParameters();
                SqlDataAdapter dr = new SqlDataAdapter(cmd1);

                DataSet ds = new DataSet();
                dr.Fill(ds);
                DataTable dt = ds.Tables[0];

                DropDownList2.DataSource = dt;
                DropDownList2.DataValueField = "stateId";
                DropDownList2.DataTextField = "stateName";
                DropDownList2.DataBind();
            }
            cn.Close();
        }

        public void CityData()
        {
            cn.Open();
            using (SqlCommand cmd1 = new SqlCommand("Sp_City", cn))
            {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@stateId", DropDownList2.SelectedValue);
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    DropDownList3.DataSource = dt;
                    DropDownList3.DataValueField = "cityId";
                    DropDownList3.DataTextField = "cityName";
                    DropDownList3.DataBind();
            }
            cn.Close();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!Page.IsPostBack)
            {
                CountryDatas();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StateData();
            lblCountry.Text = "Country : "+ddlCountry.SelectedItem.Text;
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCity.Text = "City : "+DropDownList3.SelectedItem.Text;
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CityData();
            lblState.Text = "State : "+DropDownList2.SelectedItem.Text;
        }
    }
}