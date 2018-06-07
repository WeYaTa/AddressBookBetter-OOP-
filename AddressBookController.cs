using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AddressBookBetter
{
    class AddressBookController
    {
        public List<Address> ListData { get; set; } //ListData adalah objek
        public AddressBookController()
        {
            ListData = new List<Address>();
            try
            {
                if (File.Exists(Properties.Settings.Default.NamaFile)) //set dari Properties, ada di App.config
                {
                    string[] fileContent = File.ReadAllLines(Properties.Settings.Default.NamaFile);
                    foreach (string item in fileContent)
                    {
                        string[] arrItem = item.Split(';');
                        ListData.Add(new Address
                        {
                            Nama = arrItem[0].Trim(),
                            Alamat = arrItem[1].Trim(),
                            Kota = arrItem[2].Trim(),
                            NoTelp = arrItem[3].Trim(),
                            TglLahir = Convert.ToDateTime(arrItem[4].Trim()),
                            Email = arrItem[5].Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //methods
        public void AddItem(string[] data, bool _addMode, int _row)
        {
            try
            {
                //simpan data ke file
                if (_addMode)
                { // add item
                    using (var fs = new FileStream("addressbook.csv", FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            writer.WriteLine($"{data[0].Trim()};{data[1].Trim()};{data[2].Trim()};{data[3].Trim()};{data[4].Trim()/*dtpTglLahir.Value.ToShortDateString()*/};{data[5].Trim()}");
                        }
                    }

                    MessageBox.Show("Your data has been added successfuly", "Notification", MessageBoxButtons.OK);
                }
                else
                { // edit data
                    try
                    {
                        if (File.Exists("addressbook.csv"))
                        {
                            string[] arrLine = File.ReadAllLines("addressbook.csv");
                            string editedData = data[0].Trim() + ';' + data[1].Trim() + ';' + data[2].Trim() + ';' + data[3].Trim() + ';' + data[4].Trim() + ';' + data[5].Trim();
                            arrLine[_row] = editedData;
                            File.WriteAllLines("addressbook.csv", arrLine);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "AddressBook", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    finally
                    {
                        MessageBox.Show("Your data has been edited successfuly", "Notification", MessageBoxButtons.OK);
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AddressBook", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void DeleteItem(int selectedRowsAfterFilter, int[] selectedRows, DataGridView dgvData)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to delete the selected row/data ?", "Warning !", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (selectedRowsAfterFilter == 0)
                {
                    try
                    {
                        if (File.Exists("addressbook.csv"))
                        {
                            string[] arrLine = File.ReadAllLines("addressbook.csv");
                            string[] newArrLine = new string[arrLine.Length - 1];
                            for (int i = 0; i < arrLine.Length; i++)
                            {
                                if (dgvData.Rows[i].Selected == true)
                                {
                                    for (int j = i; j < arrLine.Length - 1; j++)
                                    {
                                        arrLine[j] = arrLine[j + 1];
                                    }
                                    break;
                                }
                            }
                            //newArrLine = arrLine; <-- fail
                            for (int i = 0; i < newArrLine.Length; i++)
                            {
                                newArrLine[i] = arrLine[i];
                            }
                            File.WriteAllLines("addressbook.csv", newArrLine);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "AddressBook", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    try
                    {
                        if (File.Exists("addressbook.csv"))
                        {
                            string[] arrLine = File.ReadAllLines("addressbook.csv");
                            string[] newArrLine = new string[arrLine.Length - 1];
                            for (int i = 0; i < arrLine.Length; i++)
                            {
                                if (dgvData.Rows[i].Selected == true)
                                {
                                    for (int j = selectedRows[i]; j < arrLine.Length - 1; j++)
                                    {
                                        arrLine[j] = arrLine[j + 1];
                                    }
                                    break;
                                }
                            }
                            //newArrLine = arrLine; <-- fail
                            for (int i = 0; i < newArrLine.Length; i++)
                            {
                                newArrLine[i] = arrLine[i];
                            }
                            File.WriteAllLines("addressbook.csv", newArrLine);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "AddressBook", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void EditItem(int selectedRowsAfterFilter, int[] selectedRows, DataGridView dgvData)
        {
            int row = 0;
            string newArrLine = "";
            DialogResult dialogResult = MessageBox.Show("Do you want to edit the selected row/data ?", "Warning !", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (selectedRowsAfterFilter == 0)
                {
                    try
                    {
                        if (File.Exists("addressbook.csv"))
                        {
                            string[] arrLine = File.ReadAllLines("addressbook.csv");
                            for (int i = 0; i < arrLine.Length; i++)
                            {
                                if (dgvData.Rows[i].Selected == true)
                                {
                                    newArrLine = arrLine[i];
                                    row = i;
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "AddressBook", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    try
                    {
                        if (File.Exists("addressbook.csv"))
                        {
                            string[] arrLine = File.ReadAllLines("addressbook.csv");
                            for (int i = 0; i < arrLine.Length; i++)
                            {
                                if (dgvData.Rows[i].Selected == true)
                                {
                                    newArrLine = arrLine[selectedRows[i]];
                                    row = selectedRows[i];
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "AddressBook", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                FrmTambahData frm = new FrmTambahData(false, newArrLine, row);
                frm.Run(frm);
            }
        }

        public void FilterItem(string[] data, int selectedRowsAfterFilter, int[] selectedRows, DataGridView dgvData)
        {
            if (File.Exists("addressbook.csv"))
            {
                string[] arrLine = File.ReadAllLines("addressbook.csv");
                List<Address> FilterList = new List<Address>();
                for (int i = 0; i < arrLine.Length; i++)
                {
                    if ((data[1] != "" && arrLine[i].ToLower().Contains(data[1].ToLower().Trim()))
                        || (data[5] != "" && arrLine[i].ToLower().Contains(data[5].ToLower().Trim()))
                        || (data[2] != "" && arrLine[i].ToLower().Contains(data[2].ToLower().Trim()))
                        || (data[0] != "" && arrLine[i].ToLower().Contains(data[0].ToLower().Trim()))
                        || (data[3] != "" && arrLine[i].ToLower().Contains(data[3].ToLower().Trim()))
                        || (data[4] != "" && arrLine[i].ToLower().Contains(data[4].ToLower().Trim())))
                    {
                        string[] arrItem = arrLine[i].Split(';');
                        FilterList.Add(new Address
                        {
                            Nama = arrItem[0].Trim(),
                            Alamat = arrItem[1].Trim(),
                            Kota = arrItem[2].Trim(),
                            NoTelp = arrItem[3].Trim(),
                            TglLahir = Convert.ToDateTime(arrItem[4].Trim()),
                            Email = arrItem[5].Trim()
                        });
                        selectedRows[selectedRowsAfterFilter++] = i;
                    }
                }

                dgvData.DataSource = FilterList;

            }
        }
    }
    
}

