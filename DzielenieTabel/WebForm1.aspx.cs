using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DzielenieTabel
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private DataTable convertEmployeeForDisplay(List<Employees2> employees2s)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Gender");
            dt.Columns.Add("AnuualSalary");
            dt.Columns.Add("HourlyPay");
            dt.Columns.Add("HoursWorked");
            dt.Columns.Add("Type");

            foreach(Employees2 person in employees2s)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = person.ID;
                dr["Name"] = person.Name;
                dr["Gender"] = person.Gender;
                if (person is Permament)
                {
                    dr["AnuualSalary"] = ((Permament)person).AnuualSalary;
                    dr["Type"] = "Permament";
                }
                else
                {
                    dr["HourlyPay"] = ((Contract)person).HourlyPay;
                    dr["HoursWorked"] = ((Contract)person).HoursWorked;
                    dr["Type"] = "Contract";
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SampleDataContext sdc = new SampleDataContext();
            sdc.Log = Response.Output;
            switch (RadioButtonList1.SelectedValue)
            {
                case "Pemanent":
                    GridView1.DataSource = sdc.Employees2.OfType<Permament>().ToList();
                    GridView1.DataBind();
                    break;
                case "Contract":
                    GridView1.DataSource = sdc.Employees2.OfType<Contract>().ToList();
                    GridView1.DataBind();
                    break;
                default:
                    GridView1.DataSource = convertEmployeeForDisplay(sdc.Employees2.ToList());
                    GridView1.DataBind();
                    break;
            }
        }

        protected void btnDodajPracownika_Click(object sender, EventArgs e)
        {
            using(SampleDataContext sdc = new SampleDataContext())
            {
                Permament pr = new Permament();
                pr.Name = "Robert";
                pr.Gender = "Male";
                pr.AnuualSalary = 96000;

                Contract ct = new Contract();
                ct.Name = "Marta";
                ct.HourlyPay = 50;
                ct.HoursWorked = 160;

                sdc.Employees2.InsertOnSubmit(pr);
                sdc.Employees2.InsertOnSubmit(ct);
                sdc.SubmitChanges();
            }

        }
    }
}