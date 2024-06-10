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
    public partial class BaoCaoThongKe : Form
    {
        SqlConnection con;
        public BaoCaoThongKe()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=DESKTOP-49A7EKC;Initial Catalog=QLKS;Integrated Security=True");
            con.Open();
        }
        public void loadLoaiPhong()
        {
            SqlCommand cmd = new SqlCommand("select LoaiPhong.*,Phong.MaPhong,Phong.TenPhong,Phong.TrangThai from LoaiPhong,Phong where LoaiPhong.TenLoaiPhong = Phong.TenLoaiPhong", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvTKLoaiPhong.DataSource = dt;
        }
        public void loadCBLoaiPhong()
        {
            SqlCommand cmd = new SqlCommand("select * from LoaiPhong", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbLoaiPhong.DataSource = dt;
            cbLoaiPhong.DisplayMember = "TenLoaiPhong";
            cbLoaiPhong.ValueMember = "TenLoaiPhong";
        }
        private void demPhong()
        {
            SqlCommand demPhongTrong = new SqlCommand("select count(*) from Phong where TrangThai=N'Trống'", con);
            lbPhongTrong.Text = demPhongTrong.ExecuteScalar() + "  phòng";
            SqlCommand demPhongDaDat = new SqlCommand("select count(*) from Phong where TrangThai=N'Đã đặt'", con);
            lbPhongDaDat.Text = demPhongDaDat.ExecuteScalar() + "  phòng";
        }
        public void loadKhachHang()
        {
            SqlCommand cmd = new SqlCommand("select * from KhachHang", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvKhachHang.DataSource = dt;
        }
        private void demKhachHang()
        {
            SqlCommand demKH = new SqlCommand("select count(*) from KhachHang", con);
            lbKH.Text = demKH.ExecuteScalar() + "  khách hàng";
        }
        public void loadDichVu()
        {
            SqlCommand cmd = new SqlCommand("select * from DichVu", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvDichVu.DataSource = dt;
        }
        private void demDV()
        {
            SqlCommand demDV= new SqlCommand("select count(*) from DichVu", con);
            lbDV.Text = demDV.ExecuteScalar() + "  dịch vụ";
        }
        private void BaoCaoThongKe_Load(object sender, EventArgs e)
        {
            loadLoaiPhong();
            loadCBLoaiPhong();
            loadKhachHang();
            loadDichVu();
            demPhong();
            demKhachHang();
            demDV();
        }

        private void dgvTKLoaiPhong_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvTKLoaiPhong.CurrentRow.Index;
            if (chon >= 0)
            {
                txtMaPhong.Text = dgvTKLoaiPhong.Rows[chon].Cells["MaPhong"].Value.ToString();
                txtMaLoaiPhong.Text = dgvTKLoaiPhong.Rows[chon].Cells["MaLoaiPhong"].Value.ToString();
                txtTenPhong.Text = dgvTKLoaiPhong.Rows[chon].Cells["TenPhong"].Value.ToString();
                cbLoaiPhong.SelectedValue = dgvTKLoaiPhong.Rows[chon].Cells["TenLoaiPhong"].Value;
                cbTrangThai.Text = dgvTKLoaiPhong.Rows[chon].Cells["TrangThai"].Value.ToString();
                txtGia.Text = dgvTKLoaiPhong.Rows[chon].Cells["Gia"].Value.ToString();
            }
        }

        private void dgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvKhachHang.CurrentRow.Index;
            if (chon >= 0)
            {
                txtMaKH.Text = dgvKhachHang.Rows[chon].Cells["MaKH"].Value.ToString().Trim();
                txtHoTen.Text = dgvKhachHang.Rows[chon].Cells["HoTenKH"].Value.ToString().Trim();
                txtSdt.Text = dgvKhachHang.Rows[chon].Cells["SdtKH"].Value.ToString().Trim();
                string gt = "Nam";
                if (dgvKhachHang.Rows[chon].Cells["GioiTinhKH"].Value.ToString().Trim() == gt)
                {
                    rdNam.Checked = true;
                    rdNu.Checked = false;
                }
                else
                {
                    rdNam.Checked = false;
                    rdNu.Checked = true;
                }
                txtCccd.Text = dgvKhachHang.Rows[chon].Cells["Cccd"].Value.ToString().Trim();
            }
        }

        private void dgvDichVu_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvDichVu.CurrentRow.Index;
            if (chon >= 0)
            {
                txtMaDV.Text = dgvDichVu.Rows[chon].Cells["MaDV"].Value.ToString().Trim();
                txtTenDV.Text = dgvDichVu.Rows[chon].Cells["TenDV"].Value.ToString().Trim();
                txtGiaDV.Text = dgvDichVu.Rows[chon].Cells["GiaDV"].Value.ToString().Trim();
            }
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

        private void label5_Click(object sender, EventArgs e)
        {
            QuanLyHeThong Obj = new QuanLyHeThong();
            Obj.Show();
            this.Hide();
        }
    }
}
