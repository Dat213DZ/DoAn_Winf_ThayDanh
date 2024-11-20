using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
namespace GUI
{
    public partial class DanhMuc_form : Form
    {
        DanhMucBLL  dmBLL = new DanhMucBLL();
        public DanhMuc_form()
        {
            InitializeComponent();
            LoadDM();
        }
        public void LoadDM()
        {
            bunifuDataGridView1.ReadOnly = true;
            bunifuDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            bunifuDataGridView1.DataSource = dmBLL.LoadDM();
        }
        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {
            SanPham_form sp = new SanPham_form();
            sp.Show();
            this.Close();
        }
    }
}
