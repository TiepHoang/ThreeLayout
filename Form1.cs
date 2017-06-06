using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ThreeLayoutVer2._0
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Function
        void Load_cmbDatabase()
        {
            try
            {
                Core.Provider.StringConnect = new Core.Provider().NewStringConnect(txtNameServer.Text);
                new Core.Provider().NewConnecttion();
                SetStringConnect();

                this.cmbNameDatabase.DataSource = new Core.iCore().GetNameDatabase(txtNameServer.Text).OrderBy(q => q).ToList();
                this.cmbNameDatabase.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Load_cmbTable()
        {
            Core.Provider.StringConnect = new Core.Provider().NewStringConnect(txtNameServer.Text, cmbNameDatabase.Text, cbAccount.Checked ? txtUsername.Text : null, cbAccount.Checked ? txtPassword.Text : null);
            SetStringConnect();
            new Core.Provider().NewConnecttion();
            this.cmbNameTable.DataSource = new Core.iCore().GetTableName(txtNameServer.Text, cmbNameDatabase.Text).OrderBy(q => q).ToList();
            this.cmbNameTable.Update();
        }

        void Load_dgv()
        {
            this.dgvData.DataSource = new Core.Provider().GetDataFromQuery(string.Format("select * from [{0}]", cmbNameTable.Text));
        }

        void SetResult(string result, bool message = false)
        {
            this.txtResult.Text = result;
            this.txtResult.Focus();
            this.txtResult.SelectAll();

            if (!string.IsNullOrWhiteSpace(result))
            {
                Clipboard.SetText(result);
                if (message) MessageBox.Show(result, "Đã copy vào bộ nhớ");
            }
        }

        void ShowProcess(string message, bool error = false)
        {
            message = "\r\n" + DateTime.Now.ToShortTimeString() + ": " + message;

            txtProcess.AppendText(message);
            int length = txtProcess.TextLength;  // at end of text
            txtProcess.SelectionStart = length;
            txtProcess.SelectionLength = message.Length;
            txtProcess.SelectionColor = error ? Color.Red : Color.Green;

            txtProcess.ScrollToCaret();
        }

        void SetStringConnect()
        {
            SetResult(Core.Provider.StringConnect);
        }

        void CheckServer()
        {
            this.Enabled = false;

            Core.Setting setting = new Core.Setting();
            string serverName = setting.nameServer;
            try
            {
                setting = new Core.XmlSetting().Read();
            }
            catch (Exception)
            {
                setting = null;
            }
            this.txtNameServer.Text = setting != null ? setting.ServerName : serverName;

            try
            {
                Load_cmbDatabase();
                Core.Setting ob = new Core.Setting();
                try
                {
                    ob = new Core.XmlSetting().Read();
                }
                catch (Exception)
                { }
                ob.ServerName = txtNameServer.Text;
                new Core.XmlSetting().Write(ob);
                this.btnCheck.BackgroundImage = Properties.Resources.checkok;
            }
            catch (Exception ex)
            {
                this.btnCheck.BackgroundImage = Properties.Resources._1494858178_Error;
                MessageBox.Show(ex.Message);
            }

            this.Enabled = true;
        }

        void runProc(string queryProc)
        {
            SetResult(queryProc);
            try
            {
                new Core.Provider().ExecQuery(queryProc);
                MessageBox.Show(queryProc, "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        List<string> InsertAllProcOfTable(string nameTable)
        {
            ShowProcess(" \r\n===========\r\n>> TABLE " + nameTable + " :");
            List<string> lstError = new List<string>();
            Dictionary<string, string> dic;
            if (cbGetBy.Checked) dic = new Dictionary<string, string>(new Core.iCore().GetProcGetBy(nameTable));
            else dic = new Dictionary<string, string>();
            if (cbGetAll.Checked) dic.Add("GetAll", new Core.iCore().GetProcGetAll(nameTable));
            if (cbInsert.Checked) dic.Add("INSERT", new Core.iCore().GetProcInsert(nameTable));
            if (cbDelete.Checked) dic.Add("DELETE", new Core.iCore().GetProcDelete(nameTable));
            if (cbUpdate.Checked) dic.Add("UPDATE", new Core.iCore().GetProcUpdate(nameTable));

            foreach (var item in dic)
            {
                try
                {
                    new Core.Provider().ExecQuery(item.Value);
                    ShowProcess(item.Key + ": OK ");
                }
                catch (Exception ex)
                {
                    string t = "[ERROR] " + item.Key + " : " + ex.Message;
                    lstError.Add(t);
                    ShowProcess(t, true);
                }
            }
            return lstError;
        }
        #endregion

        private void txtNameServer_KeyUp(object sender, KeyEventArgs e)
        {
            this.btnCheck.BackgroundImage = Properties.Resources.question;
            if (e.KeyCode == Keys.Enter)
            {
                CheckServer();
            }
        }

        private void cmbNameDatabase_SelectedValueChanged(object sender, EventArgs e)
        {
            Load_cmbTable();
            load_dgvAuto();
        }

        private void cmbNameTable_SelectedValueChanged(object sender, EventArgs e)
        {
            Load_dgv();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckServer();
            Load_cmbDatabase();
            Core.Setting setting = new Core.Setting();
            string user = setting.username, pass = setting.password;
            try
            {
                setting = new Core.XmlSetting().Read();
                user = setting.Username;
                pass = setting.Password;
            }
            catch (Exception)
            { }
            this.txtUsername.Text = user;
            this.txtPassword.Text = pass;
        }

        private void cbAccount_CheckedChanged(object sender, EventArgs e)
        {
            this.grbAccount.Enabled = cbAccount.Checked;
        }

        private void txtUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Nhập mật khẩu");
                this.txtPassword.Focus();
            }
            else
            {
                Load_cmbDatabase();
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Nhập tên đăng nhập");
                this.txtUsername.Focus();
            }
            else
            {
                Load_cmbDatabase();
            }
        }

        private void btnStringConnect_Click(object sender, EventArgs e)
        {
            SetStringConnect();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            CheckServer();
        }

        private void dgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btnInsertAllProc_Click(object sender, EventArgs e)
        {
            var lstError = InsertAllProcOfTable(cmbNameTable.Text);
            MessageBox.Show("Done! Có " + lstError.Count.ToString() + " lỗi");
        }

        private void btnRunTSQL_Click(object sender, EventArgs e)
        {
            if (cbData.Checked)
            {
                this.dgvData.DataSource = new Core.Provider().GetDataFromQuery(txtResult.Text);
            }
            else
            {
                MessageBox.Show(new Core.Provider().ExecQuery(txtResult.Text) > 0 ? "Thành công" : "Thất bại");
            }
        }

        private void btnAuthor_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/100007134302427");
        }

        private void btnDto_Click(object sender, EventArgs e)
        {
            SetResult(new Core.iCore().GetDTO(cmbNameTable.Text), true);
        }

        private void btnDal_Click(object sender, EventArgs e)
        {
            SetResult(new Core.iCore().GetDAL(cmbNameTable.Text, cmbNameDatabase.Text), true);
        }

        private void bt_Click(object sender, EventArgs e)
        {
            SetResult(new Core.iCore().GetBUS(cmbNameTable.Text), true);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (new frmSetting().ShowDialog() == DialogResult.OK)
            {
                CheckServer();
            }
        }

        private void btnAllDatabase_Click(object sender, EventArgs e)
        {
            var lst = new Core.iCore().GetTableName(txtNameServer.Text, cmbNameDatabase.Text);
            if (lst != null && lst.Count > 0)
            {
                string s = "";
                foreach (var item in lst)
                {
                    if (item.Contains("sys"))
                    {
                        s += "\r\n\n>> Bỏ qua bảng " + item;
                        continue;
                    }
                    s += InsertAllProcOfTable(item);
                }
                SetResult(s, true);
            }
            else
            {
                MessageBox.Show("Database không có bảng!");
            }
        }

        private void Replace_Click(object sender, EventArgs e)
        {
            SetResult(txtResult.Text.Replace(txtOldChar.Text, txtNewChar.Text));
        }

        private void txtNewChar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetResult(txtResult.Text.Replace(txtOldChar.Text, txtNewChar.Text));
            }
        }

        string newPathFolder()
        {
            return folderBrowserDialog1.ShowDialog() == DialogResult.OK ? folderBrowserDialog1.SelectedPath : null;
        }

        private void btnChaneFoderBus_Click(object sender, EventArgs e)
        {
            string s = newPathFolder();
            if (s != null) txtFolderBus.Text = s;
        }

        private void btnChaneFoderDal_Click(object sender, EventArgs e)
        {
            string s = newPathFolder();
            if (s != null) txtFolderDal.Text = s;
        }

        private void btnChaneFoderDto_Click(object sender, EventArgs e)
        {
            string s = newPathFolder();
            if (s != null) txtFolderDto.Text = s;
        }

        private void load_dgvAuto()
        {
            dgvAuto.DataSource = new Core.iCore().GetTableName(txtNameServer.Text, cmbNameDatabase.Text).Where(q => q.Contains("sys") == false).OrderBy(q => q).Select(q => new { NameTable = q }).ToList();
            foreach (DataGridViewRow item in dgvAuto.Rows)
            {
                for (int i = 0; i < 5; i++)
                {
                    (item.Cells[i] as DataGridViewCheckBoxCell).Value = true;
                }

            }
            Core.Setting setting = new Core.Setting();
            txtNamespaceBus.Text = setting.namespaceBus;
            txtNamespaceDal.Text = setting.namespaceDal;
            txtNamespaceDto.Text = setting.namespaceDto;
            try
            {
                setting = new Core.XmlSetting().Read();
                txtNamespaceBus.Text = setting.NamespaceBus;
                txtNamespaceDal.Text = setting.NamespaceDal;
                txtNamespaceDto.Text = setting.NamespaceDto;
            }
            catch (Exception)
            { }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            List<string> lstError = new List<string>();
            if (!Directory.Exists(txtFolderBus.Text)) btnChaneFoderBus_Click(sender, e);
            else if (!Directory.Exists(txtFolderDal.Text)) btnChaneFoderDal_Click(sender, e);
            else if (!Directory.Exists(txtFolderDto.Text)) btnChaneFoderDto_Click(sender, e);
            else
            {
                string nameProcess = "=========[INSERT PROCIDURE]=========";

                ShowProcess(nameProcess);
                var lst1 = AutoRunProc();
                lstError.AddRange(lst1);
                ShowProcess("________________[END PROCIDURE]________________");
                string detailError = "\r\n" + nameProcess + " : " + lst1.Count + " Error";

                nameProcess = "=========[INSERT DTO]=========";
                ShowProcess(nameProcess);
                lst1 = AutoRunDto();
                lstError.AddRange(lst1);
                ShowProcess("________________[END DTO]________________");
                detailError += "\r\n" + nameProcess + " : " + lst1.Count + " Error";

                nameProcess = "=========[INSERT DAL]=========";
                ShowProcess(nameProcess);
                lst1 = AutoRunDal();
                lstError.AddRange(lst1);
                ShowProcess("________________[END DAL]________________");
                detailError += "\r\n" + nameProcess + " : " + lst1.Count + " Error";

                nameProcess = "=========[INSERT BUS]=========";
                ShowProcess(nameProcess);
                lst1 = AutoRunBus();
                lstError.AddRange(lst1);
                ShowProcess("________________[END BUS]________________");
                detailError += "\r\n" + nameProcess + " : " + lst1.Count + " Error";

                string error = "Done " + cmbNameDatabase.Text + "/r/n " + lstError.Count + " Error\r\n" + detailError;

                if (lstError.Count > 0)
                {
                    foreach (var item in lstError)
                    {
                        error += "\r\n" + item;
                    }
                }

                ShowProcess(error, true);
                MessageBox.Show(error);
            }
        }

        private void saveFile(string pathFolder, string nameFile, string v)
        {
            try
            {
                File.WriteAllText(Path.Combine(pathFolder, nameFile), v);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<string> AutoRunBus()
        {
            List<string> lstError = new List<string>();
            var bus = new Core.iCore();
            foreach (DataGridViewRow item in dgvAuto.Rows)
            {
                try
                {
                    if (bool.Parse(item.Cells[1].Value.ToString()))
                    {
                        string extentionBus = "";
                        Core.Setting setting;
                        try
                        {
                            setting = new Core.XmlSetting().Read();
                            extentionBus = setting.ExtentionBus;
                        }
                        catch (Exception)
                        {
                            setting = new Core.Setting();
                            extentionBus = setting.extentionBus;
                        }
                        string nameTable = item.Cells[5].Value.ToString();
                        string nameFile = nameTable + extentionBus + ".cs";
                        try
                        {
                            saveFile(txtFolderBus.Text, nameFile, bus.GetBUS(nameTable));
                            ShowProcess(nameFile + " : OK");
                        }
                        catch (Exception ex)
                        {

                            ShowProcess(nameFile + " : " + ex.Message);
                            throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowProcess(ex.Message, true);
                    throw;
                }
            }
            return lstError;
        }

        private List<string> AutoRunDal()
        {
            List<string> lstError = new List<string>();
            var bus = new Core.iCore();
            foreach (DataGridViewRow item in dgvAuto.Rows)
            {
                try
                {
                    if (bool.Parse(item.Cells[2].Value.ToString()))
                    {
                        string extentionDal = "";
                        Core.Setting setting;
                        try
                        {
                            setting = new Core.XmlSetting().Read();
                            extentionDal = setting.ExtentionDal;
                        }
                        catch (Exception)
                        {
                            setting = new Core.Setting();
                            extentionDal = setting.extentionDal;
                        }
                        string nameTable = item.Cells[5].Value.ToString();
                        string nameFile = nameTable + extentionDal + ".cs";
                        try
                        {
                            saveFile(txtFolderDal.Text, nameFile, bus.GetDAL(nameTable, cmbNameDatabase.Text));
                            ShowProcess(nameFile + " : OK");
                        }
                        catch (Exception ex)
                        {
                            string t = nameFile + " : " + ex.Message;
                            ShowProcess(t);
                            lstError.Add(t);
                            throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowProcess(ex.Message, true);
                    lstError.Add(ex.Message);
                    throw;
                }
            }
            return lstError;
        }

        private List<string> AutoRunDto()
        {
            List<string> lstError = new List<string>();
            var bus = new Core.iCore();
            foreach (DataGridViewRow item in dgvAuto.Rows)
            {
                try
                {
                    if (bool.Parse(item.Cells[3].Value.ToString()))
                    {
                        string extentionDto = "";
                        Core.Setting setting;
                        try
                        {
                            setting = new Core.XmlSetting().Read();
                            extentionDto = setting.ExtentionDto;
                        }
                        catch (Exception)
                        {
                            setting = new Core.Setting();
                            extentionDto = setting.extentionDto;
                        }
                        string nameTable = item.Cells[5].Value.ToString();
                        string nameFile = nameTable + extentionDto + ".cs";
                        try
                        {
                            saveFile(txtFolderDto.Text, nameFile, bus.GetDTO(nameTable));
                            ShowProcess(nameFile + " : OK");
                        }
                        catch (Exception ex)
                        {
                            string t = nameFile + " : " + ex.Message;
                            ShowProcess(t);
                            lstError.Add(t);
                            throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowProcess(ex.Message, true);
                    lstError.Add(ex.Message);
                    throw;
                }
            }
            return lstError;
        }

        private List<string> AutoRunProc()
        {
            List<string> lstError = new List<string>();
            foreach (DataGridViewRow item in dgvAuto.Rows)
            {
                try
                {
                    if (bool.Parse(item.Cells[4].Value.ToString()))
                    {
                        lstError.AddRange(InsertAllProcOfTable(item.Cells[5].Value.ToString()));
                    }
                }
                catch (Exception)
                { throw; }
            }
            return lstError;
        }

        private void dgvAuto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool valueAfterSelect = !(bool)((dgvAuto.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell)).Value;
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        (dgvAuto.Rows[e.RowIndex].Cells[i] as DataGridViewCheckBoxCell).Value = valueAfterSelect;
                    }
                }
                else if (e.ColumnIndex > 0 && e.ColumnIndex < 5)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        if ((bool)(dgvAuto.Rows[e.RowIndex].Cells[i].Value) == true)
                        {
                            (dgvAuto.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell).Value = false;
                            dgvAuto.Update();
                            return;
                        }
                    }
                    (dgvAuto.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell).Value = true;
                }
                dgvAuto.Update();
            }
            catch (Exception)
            {
            }
        }
    }
}
