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
    public partial class QuanLyDanhMuc : Form
    {
        SqlConnection con;
        public QuanLyDanhMuc()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=DESKTOP-49A7EKC;Initial Catalog=QLKS;Integrated Security=True");
            con.Open();
        }

        public void loadLoaiPhong()
        {
            SqlCommand cmd = new SqlCommand("select * from LoaiPhong", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvLoaiPhong.DataSource = dt;
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
        public void loadPhong()
        {
            SqlCommand cmd = new SqlCommand("select * from Phong", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvPhong.DataSource = dt;
        }
        
        public void loadKhachHang()
        {
            SqlCommand cmd = new SqlCommand("select * from KhachHang", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvKhachHang.DataSource = dt;
        }
        public void loadDichVu()
        {
            SqlCommand cmd = new SqlCommand("select * from DichVu", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvDichVu.DataSource = dt;
        }
        private void QuanLyDanhMuc_Load(object sender, EventArgs e)
        {
            loadLoaiPhong();
            loadPhong();
            loadCBLoaiPhong();
            loadKhachHang();
            loadDichVu();
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into LoaiPhong(MaLoaiPhong,TenLoaiPhong,Gia) values (@maloaiphong,@tenloaiphong,@gia)", con);
                cmd.Parameters.AddWithValue("@maloaiphong", txtMaLoaiPhong.Text);
                cmd.Parameters.AddWithValue("@tenloaiphong", txtLoaiPhong.Text);
                cmd.Parameters.AddWithValue("@gia", decimal.Parse(txtGia.Text));
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
            loadLoaiPhong();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvLoaiPhong.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand(
                    "update LoaiPhong set TenLoaiPhong=@tenloaiphong,Gia=@gia where MaLoaiPhong = @maloaiphongcu;", con);

                cmd.Parameters.AddWithValue("@tenloaiphong", txtLoaiPhong.Text);
                cmd.Parameters.AddWithValue("@gia", decimal.Parse(txtGia.Text));
                cmd.Parameters.AddWithValue("@maloaiphongcu", dgvLoaiPhong.Rows[chon].Cells["MaLoaiPhong"].Value.ToString());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công");
                }
                else MessageBox.Show("Sửa thất bại");
                loadLoaiPhong();
            }
        }

        private void dgvLoaiPhong_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvLoaiPhong.CurrentRow.Index;
            if (chon >= 0)
            {
                txtMaLoaiPhong.Text = dgvLoaiPhong.Rows[chon].Cells["MaLoaiPhong"].Value.ToString().Trim();
                txtLoaiPhong.Text = dgvLoaiPhong.Rows[chon].Cells["TenLoaiPhong"].Value.ToString().Trim();
                txtGia.Text = dgvLoaiPhong.Rows[chon].Cells["Gia"].Value.ToString().Trim();
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvLoaiPhong.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand("delete from LoaiPhong where MaLoaiPhong=@maloaiphong", con);
                cmd.Parameters.AddWithValue("@maloaiphong", dgvLoaiPhong.Rows[chon].Cells["MaLoaiPhong"].Value.ToString());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xoá thành công");
                }
                else MessageBox.Show("Xoá thất bại");
                loadLoaiPhong();
            }
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from LoaiPhong where MaLoaiPhong like N'%" + txtTimKiem.Text + "%'or TenLoaiPhong like N'%" + txtTimKiem.Text + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvLoaiPhong.DataSource = dt;
        }

        private void btThemPhong_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into Phong(MaPhong,TenPhong,TenLoaiPhong,TrangThai) values (@maphong,@tenphong,@tenloaiphong,@trangthai)", con);
                cmd.Parameters.AddWithValue("@maphong", txtMaPhong.Text);
                cmd.Parameters.AddWithValue("@tenphong", txtTenPhong.Text);
                cmd.Parameters.AddWithValue("@tenloaiphong", cbLoaiPhong.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@trangthai", cbTrangThai.Text);
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
            loadPhong();
        }

        private void dgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvPhong.CurrentRow.Index;
            if (chon >= 0)
            {
                txtMaPhong.Text = dgvPhong.Rows[chon].Cells["MaPhong"].Value.ToString().Trim();
                txtTenPhong.Text = dgvPhong.Rows[chon].Cells["TenPhong"].Value.ToString().Trim();
                cbLoaiPhong.SelectedValue = dgvPhong.Rows[chon].Cells["LoaiPhong"].Value;
                cbTrangThai.Text = dgvPhong.Rows[chon].Cells["TrangThai"].Value.ToString().Trim();
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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Phong where MaPhong like N'%" + txtTim.Text + "%'or TenPhong like N'%" + txtTim.Text +"%'or TenLoaiPhong like N'%" + txtTim.Text + "%'or TrangThai like N'%" + txtTim.Text + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvPhong.DataSource = dt;
        }

        private void btSuaPhong_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvPhong.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand(
                    "update Phong set TenPhong=@tenphong,TenLoaiPhong=@tenloaiphong,TrangThai=@trangthai where MaPhong = @maphongcu", con);

                cmd.Parameters.AddWithValue("@tenphong", txtTenPhong.Text);
                cmd.Parameters.AddWithValue("@tenloaiphong", dgvPhong.Rows[chon].Cells["TenLoaiPhong"].Value.ToString());
                cmd.Parameters.AddWithValue("@trangthai", dgvPhong.Rows[chon].Cells["TrangThai"].Value.ToString());
                cmd.Parameters.AddWithValue("@maphongcu", dgvPhong.Rows[chon].Cells["MaPhong"].Value.ToString());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công");
                }
                else MessageBox.Show("Sửa thất bại");
                loadPhong();
            }
        }

        private void btXoaPhong_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvPhong.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand("delete from Phong where MaPhong=@maphong", con);
                cmd.Parameters.AddWithValue("@maphong", dgvPhong.Rows[chon].Cells["MaPhong"].Value.ToString());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xoá thành công");
                }
                else MessageBox.Show("Xoá thất bại");
                loadPhong();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into KhachHang(MaKH,HoTenKH,SdtKH,Cccd,GioiTinhKH) values (@makh,@tenkh,@sdt,@cccd,@gioitinh)", con);
                cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);
                cmd.Parameters.AddWithValue("@tenkh", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@sdt", txtSdt.Text);
                if (rdNam.Checked)
                    cmd.Parameters.AddWithValue("@gioitinh", "Nam");
                else
                    cmd.Parameters.AddWithValue("@gioitinh", "Nữ");
                cmd.Parameters.AddWithValue("@cccd", txtCccd.Text);

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
            loadKhachHang();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvKhachHang.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand(
                    "update KhachHang set HoTenKH=@tenkh,SdtKH=@sdt,Cccd=@cccd,GioiTinhKH=@gioitinh where MaKH = @makhcu", con);

                cmd.Parameters.AddWithValue("@tenkh", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@sdt", txtSdt.Text);
                cmd.Parameters.AddWithValue("@cccd", txtCccd.Text);
                if (rdNam.Checked)
                    cmd.Parameters.AddWithValue("@gioitinh", "Nam");
                else
                    cmd.Parameters.AddWithValue("@gioitinh", "Nữ");
                cmd.Parameters.AddWithValue("@makhcu", dgvKhachHang.Rows[chon].Cells["MaKH"].Value.ToString());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công");
                }
                else MessageBox.Show("Sửa thất bại");
                loadKhachHang();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvKhachHang.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand("delete from KhachHang where MaKH=@makh", con);
                cmd.Parameters.AddWithValue("@makh", dgvKhachHang.Rows[chon].Cells["MaKH"].Value.ToString());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xoá thành công");
                }
                else MessageBox.Show("Xoá thất bại");
                loadKhachHang();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from KhachHang where MaKH like N'%" + txtTimkiemm.Text + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvKhachHang.DataSource = dt;
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

        private void btThemDV_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into DichVu(MaDV,TenDV,GiaDV) values (@madv,@tendv,@giadv)", con);
                cmd.Parameters.AddWithValue("@madv", txtMaDV.Text);
                cmd.Parameters.AddWithValue("@tendv", txtTenDV.Text);
                cmd.Parameters.AddWithValue("@giadv", txtGiaDV.Text);
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
            loadDichVu();
        }

        private void bbtSuaDV_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvDichVu.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand(
                    "update DichVu set TenDV=@tendv,GiaDV=@giadv where MaDV = @madvcu", con);

                cmd.Parameters.AddWithValue("@tendv", txtTenDV.Text);
                cmd.Parameters.AddWithValue("@giadv", txtGiaDV.Text);
                cmd.Parameters.AddWithValue("@madvcu", dgvDichVu.Rows[chon].Cells["MaDV"].Value.ToString());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công");
                }
                else MessageBox.Show("Sửa thất bại");
                loadDichVu();
            }
        }

        private void btXoaDV_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvDichVu.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand("delete from DichVu where MaDV=@madv", con);
                cmd.Parameters.AddWithValue("@madv", dgvDichVu.Rows[chon].Cells["MaDV"].Value.ToString());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xoá thành công");
                }
                else MessageBox.Show("Xoá thất bại");
                loadDichVu();
            }
        }

        private void btTimDV_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from DichVu where MaDV like N'%" + txtTimDV.Text + "%' or TenDV like N'" + txtTimDV.Text + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvDichVu.DataSource = dt;
        }

        private void label31_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                DangNhap f = new DangNhap();
                f.ShowDialog();
                this.Close();
            }
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

        private void label5_Click(object sender, EventArgs e)
        {
            QuanLyHeThong Obj = new QuanLyHeThong();
            Obj.Show();
            this.Hide();
        }
    }
}
