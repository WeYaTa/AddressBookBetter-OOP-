using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AddressBookBetter
{
    public partial class FrmAddressBook : Form
    {
        AddressBookController address;
        int[] selectedRows = new int[25];
        int selectedRowsAfterFilter = 0;

        public FrmAddressBook()
        {
            InitializeComponent();
        }

        private void FrmAddressBook_Load(object sender, EventArgs e)
        {
            selectedRowsAfterFilter = 0;
            selectedRows = new int[25];
            try
            {
                address = new AddressBookController();
                dgvData.DataSource = address.ListData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                lblBanyakRecordData.Text = $"{dgvData.Rows.Count.ToString("n0")} Record Data(s)";
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            FrmTambahData frm = new FrmTambahData(true, null, 0);
            frm.Run(frm);
            FrmAddressBook_Load(null, null);
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count == 0)
            {
                MessageBox.Show("There are no rows to be selected ! Click the refresh button !", "Error", MessageBoxButtons.OK);
            }
            else
            {
                address = new AddressBookController();
                address.DeleteItem(selectedRowsAfterFilter, selectedRows, dgvData);
                FrmAddressBook_Load(null, null);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count == 0)
            {
                MessageBox.Show("There are no rows to be selected ! Click the refresh button !", "Error", MessageBoxButtons.OK);
            }
            else
            {
                address = new AddressBookController();
                address.EditItem(selectedRowsAfterFilter, selectedRows,dgvData);
                FrmAddressBook_Load(null, null);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string [] data = {txtNama.Text, txtAlamat.Text, txtKota.Text,txtNoHp.Text,txtTglLahir.Text,txtEmail.Text};
            address = new AddressBookController();
            address.FilterItem(data, selectedRowsAfterFilter, selectedRows,dgvData);

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
                txtAlamat.Clear();
                txtEmail.Clear();
                txtKota.Clear();
                txtNama.Clear();
                txtNoHp.Clear();
                txtTglLahir.Clear();
            FrmAddressBook_Load(null, null);
        }

        private void txtNoHp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)) {
                e.Handled = true;
            }
        }
    }
}
