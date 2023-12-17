using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SqlAdapter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = null;
        string strConn = "Server = DESKTOP-IKHI39U\\MONS;Database = CSDLTest; User Id = sa; pwd = Entercc12";
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        private void btnNhapDuLieu_Click(object sender, EventArgs e)
        {

            if (conn == null)
                conn = new SqlConnection(strConn);
            adapter = new SqlDataAdapter("Select * from SanPham", conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            adapter.Fill(ds, "SanPham");
            gvSanPham.DataSource = ds.Tables["SanPham"];
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["SanPham"].NewRow();
            row["Ma"] = txtMa.Text;
            row["Ten"] = txtTen.Text;
            row["DonGia"] = txtGia.Text;
            row["MaDanhMuc"] = txtMadm.Text;

            ds.Tables["SanPham"].Rows.Add(row);

            int ret = adapter.Update(ds.Tables["SanPham"]);
            if (ret > 0)
            {
                btnNhapDuLieu.PerformClick();
            }
            else
                MessageBox.Show("Them moi that bai");
        }
        int vt = -1;
        private void gvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            if (vt != -1)
            {
                DataRow row = ds.Tables["SanPham"].Rows[vt];
                txtMa.Text = row["Ma"] + "";
                txtTen.Text = row["Ten"] + "";
                txtGia.Text = row["DonGia"] + "";
                txtMadm.Text = row["MaDanhMuc"] + "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (vt != -1)
            {
                DataRow row = ds.Tables["SanPham"].Rows[vt];
                row.BeginEdit();
                row["Ma"] = txtMa.Text;
                row["Ten"] = txtTen.Text;
                row["DonGia"] = txtGia.Text;
                row["MaDanhMuc"] = txtMadm.Text;
                row.EndEdit();

                int ret = adapter.Update(ds.Tables["SanPham"]);
                if (ret > 0)
                {
                    btnNhapDuLieu.PerformClick();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Test thành công");
        }
    }
}
