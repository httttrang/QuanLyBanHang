using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class QuanLyKhachSan : Form
    {
        SqlConnection con = new SqlConnection();
        public QuanLyKhachSan()
        {
            InitializeComponent();
        }

        private void QuanLyKhachSan_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
            con.ConnectionString = connectionString;
            con.Open();
            string sql = "Select * From tblPhong";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tabletblPhong = new DataTable();
            adp.Fill(tabletblPhong);
            DataGridView_tblPhong.DataSource = tabletblPhong;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            txtMaPhong.Enabled = true;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if(txtMaPhong.Text == "")
            {
                MessageBox.Show ("Bạn cần nhập mã phòng");
                txtMaPhong.Focus();
                return;
            }
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show ("Bạn cần nhập tên phòng");
                txtTenPhong.Focus();
            }
            else
            {
                // insert into tblPhong values ()
                string sql = "insert into tblPhong values ('" + txtMaPhong.Text + "', '" + txtTenPhong.Text + "'";
                if (txtDonGia.Text != "")
                    sql = sql + ", " + txtDonGia.Text.Trim();
                sql = sql + ")";
                ///MessageBox.Show(sql);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                loadDataToGridView();
            }    
        }

        private void loadDataToGridView()
        {
            throw new NotImplementedException();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string sql = @" UPDATE tbIPhong SET
                        Maphong=N'" + txtMaPhong.Text + @"',Tenphong =N'" + txtTenPhong.Text + @"',Dongia =N'" + txtDonGia.Text + @"'
                     Where (Maphong=N'" + txtMaPhong.Text + @"')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loadDataToGridView();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string sql = "Detete From tbIPhong Where Maphong = '" + txtMaPhong.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loadDataToGridView();
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            txtMaPhong.Clear();
            txtTenPhong.Clear();
            txtDonGia.Clear();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DataGridView_tblPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDonGia.Text = DataGridView_tblPhong.CurrentRow.Cells["DonGia"].Value.ToString();
            txtMaPhong.Text = DataGridView_tblPhong.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTenPhong.Text = DataGridView_tblPhong.CurrentRow.Cells["TenPhong"].Value.ToString();
            txtMaPhong.Enabled = false;
        }
    }
}
