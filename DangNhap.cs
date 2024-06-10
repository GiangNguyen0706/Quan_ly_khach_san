using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Đồ_án_1___QLKS
{
    public partial class DangNhap : Form
    {
        SqlConnection con;
        public DangNhap()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=DESKTOP-49A7EKC;Initial Catalog=QLKS;Integrated Security=True");
        }

        private void btDangNhap_Click(object sender, EventArgs e)
        {
            if(txtTaiKhoan.Text == "" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !!!");
            }    
            else
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("select * from NhanVien where MaNV='" + txtTaiKhoan.Text + "' or HoTen='" + txtTaiKhoan.Text + "' and MatKhau='" + txtMatKhau.Text + "'" ,con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    QuanLyDanhMuc f = new QuanLyDanhMuc();
                    f.Show();
                    this.Hide();
                }
                catch
                {
                    MessageBox.Show("Lỗi đăng nhập");
                }
                finally
                {
                    con.Close();
                }
            }    
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
