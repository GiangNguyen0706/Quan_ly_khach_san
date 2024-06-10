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
    public partial class QuanLyHeThong : Form
    {
        SqlConnection con;
        public QuanLyHeThong()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=DESKTOP-49A7EKC;Initial Catalog=QLKS;Integrated Security=True");
            con.Open();
        }
        public void loadNhanVien()
        {
            SqlCommand cmd = new SqlCommand("select * from NhanVien", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvNhanVien.DataSource = dt;
        }
        private void QuanLyHeThong_Load(object sender, EventArgs e)
        {
            loadNhanVien();
        }

        private void btThemNV_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into NhanVien(MaNV,HoTen,MatKhau) values (@manv,@tennv,@mk)", con);
                cmd.Parameters.AddWithValue("@manv", txtMaNV.Text);
                cmd.Parameters.AddWithValue("@tennv", txtTenNV.Text);
                cmd.Parameters.AddWithValue("@mk", txtMatKhau.Text);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Thêm thành công");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Thêm thất bại");
            }
            loadNhanVien();
        }

        private void bbtSuaNV_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvNhanVien.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand(
                    "update NhanVien set HoTen=@tennv,MatKhau=@mk where MaNV = @macu;", con);

                cmd.Parameters.AddWithValue("@tennv", txtTenNV.Text);
                cmd.Parameters.AddWithValue("@mk", txtMatKhau.Text);
                cmd.Parameters.AddWithValue("@macu", dgvNhanVien.Rows[chon].Cells["MaNV"].Value.ToString());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công");
                }
                else MessageBox.Show("Sửa thất bại");
                loadNhanVien();
            }
        }

        private void btXoaNV_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvNhanVien.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand("delete from NhanVien where MaNV=@manv", con);
                cmd.Parameters.AddWithValue("@manv", dgvNhanVien.Rows[chon].Cells["MaNV"].Value.ToString());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xoá thành công");
                }
                else MessageBox.Show("Xoá thất bại");
                loadNhanVien();
            }
        }

        private void btTimNV_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from NhanVien where MaNV like N'%" + txtTimNV.Text + "%'or HoTen like N'%" + txtTimNV.Text + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvNhanVien.DataSource = dt;
        }

        private void label28_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                DangNhap f = new DangNhap();
                f.ShowDialog();
                this.Close();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            QuanLyDanhMuc Obj = new QuanLyDanhMuc();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            QuanLyThuePhong Obj = new QuanLyThuePhong();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            BaoCaoThongKe Obj = new BaoCaoThongKe();
            Obj.Show();
            this.Hide();
        }
    }
}
