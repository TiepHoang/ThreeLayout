using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreeLayoutVer2._0
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();
        }

        void setData(Core.Setting setting)
        {
            Core.Setting ob = new Core.Setting();
            if (setting == null)
            {
                ob.Username = ob.username;
                ob.Password = ob.password;

                ob.ExtentionBus = ob.extentionBus;
                ob.ExtentionDal = ob.extentionDal;
                ob.ExtentionDto = ob.extentionDto;

                ob.NamespaceBus = ob.namespaceBus;
                ob.NamespaceDal = ob.namespaceDal;
                ob.NamespaceDto = ob.namespaceDto;
            }
            else
            {
                ob = setting;
            }
            txtNameServer.Text = ob.ServerName;
            txtUsername.Text = ob.Username;

            txtPassword.Text = ob.Password;
            txtNameBus.Text = ob.ExtentionBus;
            txtNameDal.Text = ob.ExtentionDal;
            txtNameDto.Text = ob.ExtentionDto;

            txtNamespaceBus.Text = ob.NamespaceBus;
            txtNamespaceDal.Text = ob.NamespaceDal;
            txtNamespaceDto.Text = ob.NamespaceDto;
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            try
            {
                var ob = new Core.XmlSetting().Read();
                setData(ob);
            }
            catch (Exception)
            {
                setData(null);
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            setData(null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Core.Setting ob = new Core.Setting();

            ob.ServerName = txtNameServer.Text;
            ob.Username = txtUsername.Text;
            ob.Password = txtPassword.Text;

            ob.ExtentionBus = txtNameBus.Text;
            ob.ExtentionDal = txtNameDal.Text;
            ob.ExtentionDto = txtNameDto.Text;

            ob.NamespaceBus = txtNamespaceBus.Text;
            ob.NamespaceDal = txtNamespaceDal.Text;
            ob.NamespaceDto = txtNamespaceDto.Text;

            try
            {
                new Core.XmlSetting().Write(ob);
                MessageBox.Show("Thành công");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR " + ex.Message);
            }
        }

        private void txtNameBus_TextChanged(object sender, EventArgs e)
        {
            lblBus.Text = "Account" + txtNamespaceBus.Text;
        }

        private void txtNameDal_TextChanged(object sender, EventArgs e)
        {
            lblDal.Text = "Account" + txtNamespaceDal.Text;
        }

        private void txtNameDto_TextChanged(object sender, EventArgs e)
        {
            lblDto.Text = "Account" + txtNamespaceDto.Text;
        }

        private void txtNamespaceBus_TextChanged(object sender, EventArgs e)
        {
            lblNamespaceBus.Text = "namespace " + txtNamespaceBus.Text;
        }

        private void txtNamespaceDal_TextChanged(object sender, EventArgs e)
        {
            lblNamespaceDal.Text = "namespace " + txtNamespaceDal.Text;
        }

        private void txtNamespaceDto_TextChanged(object sender, EventArgs e)
        {
            lblNamespaceDto.Text = "namespace " + txtNamespaceDto.Text;
        }
    }
}
