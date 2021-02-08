using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace ASPWebLocationAssignment.HTML
{
    public partial class Location : System.Web.UI.Page
    {
        DataTable dt = null;
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        
        public void CountryDatas()
        {
            cn.Open();
            using (SqlDataAdapter cmd = new SqlDataAdapter(@"select countryId,countryName from Country", cn))
            {

                        DataSet ds = new DataSet();
                        cmd.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        DropDownList1.DataSource = dt;
                        DropDownList1.DataValueField = "countryId";
                        DropDownList1.DataTextField = "countryName";
                        DropDownList1.DataBind();
                    
            }
            cn.Close();
        }
        public void StateData()
        {
            cn.Open();
            using (SqlDataAdapter cmd1 = new SqlDataAdapter(@"select stateId,stateName from States where countryId ='"+DropDownList1.SelectedValue+"'", cn))
            {
                    DataSet ds = new DataSet();
                    cmd1.Fill(ds);
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
            using (SqlCommand cmd1 = new SqlCommand(@"select cityId,cityName from City where stateId='"+DropDownList2.SelectedValue+"'", cn))
            {
                
                using (SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    
                        DropDownList3.DataSource = dr;
                        DropDownList3.DataValueField = "cityId";
                        DropDownList3.DataTextField = "cityName";
                        DropDownList3.DataBind();
                    
                }
            }
            cn.Close();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //DropDownList1.Items.Insert(0, "Select Country");
            if (!Page.IsPostBack)
            {
                CountryDatas();
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            StateData();
            lblCountry.Text = "Country : "+DropDownList1.SelectedItem.Text;

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            CityData();
            lblState.Text = "State : "+DropDownList2.SelectedItem.Text;
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCity.Text = "City : "+DropDownList3.SelectedItem.Text;
            
        }
    }
}