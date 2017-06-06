namespace ThreeLayoutVer2._0
{
    partial class frmSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameBus = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNameDal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNameDto = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNameServer = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.lblBus = new System.Windows.Forms.Label();
            this.lblDal = new System.Windows.Forms.Label();
            this.lblDto = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNamespaceBus = new System.Windows.Forms.TextBox();
            this.lblNamespaceBus = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblNamespaceDto = new System.Windows.Forms.Label();
            this.lblNamespaceDal = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtNamespaceDto = new System.Windows.Forms.TextBox();
            this.txtNamespaceDal = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Extention BUS";
            // 
            // txtNameBus
            // 
            this.txtNameBus.Location = new System.Drawing.Point(88, 23);
            this.txtNameBus.Name = "txtNameBus";
            this.txtNameBus.Size = new System.Drawing.Size(176, 20);
            this.txtNameBus.TabIndex = 1;
            this.txtNameBus.TextChanged += new System.EventHandler(this.txtNameBus_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(163, 371);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Extention DAL";
            // 
            // txtNameDal
            // 
            this.txtNameDal.Location = new System.Drawing.Point(88, 87);
            this.txtNameDal.Name = "txtNameDal";
            this.txtNameDal.Size = new System.Drawing.Size(176, 20);
            this.txtNameDal.TabIndex = 1;
            this.txtNameDal.TextChanged += new System.EventHandler(this.txtNameDal_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Extention DTO";
            // 
            // txtNameDto
            // 
            this.txtNameDto.Location = new System.Drawing.Point(88, 150);
            this.txtNameDto.Name = "txtNameDto";
            this.txtNameDto.Size = new System.Drawing.Size(176, 20);
            this.txtNameDto.TabIndex = 1;
            this.txtNameDto.TextChanged += new System.EventHandler(this.txtNameDto_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Default Name server";
            // 
            // txtNameServer
            // 
            this.txtNameServer.Location = new System.Drawing.Point(120, 19);
            this.txtNameServer.Name = "txtNameServer";
            this.txtNameServer.Size = new System.Drawing.Size(166, 20);
            this.txtNameServer.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNameServer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Location = new System.Drawing.Point(174, 223);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 131);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Tên đăng nhập";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(120, 89);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(166, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Mật khẩu";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(120, 55);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(166, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtNameBus);
            this.groupBox2.Controls.Add(this.lblBus);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblDto);
            this.groupBox2.Controls.Add(this.lblDal);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtNameDto);
            this.groupBox2.Controls.Add(this.txtNameDal);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(318, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 205);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Extention";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(277, 371);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(385, 371);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 2;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // lblBus
            // 
            this.lblBus.AutoSize = true;
            this.lblBus.Location = new System.Drawing.Point(117, 46);
            this.lblBus.Name = "lblBus";
            this.lblBus.Size = new System.Drawing.Size(76, 13);
            this.lblBus.TabIndex = 0;
            this.lblBus.Text = "Extention BUS";
            // 
            // lblDal
            // 
            this.lblDal.AutoSize = true;
            this.lblDal.Location = new System.Drawing.Point(117, 110);
            this.lblDal.Name = "lblDal";
            this.lblDal.Size = new System.Drawing.Size(75, 13);
            this.lblDal.TabIndex = 0;
            this.lblDal.Text = "Extention DAL";
            // 
            // lblDto
            // 
            this.lblDto.AutoSize = true;
            this.lblDto.Location = new System.Drawing.Point(118, 173);
            this.lblDto.Name = "lblDto";
            this.lblDto.Size = new System.Drawing.Size(77, 13);
            this.lblDto.TabIndex = 0;
            this.lblDto.Text = "Extention DTO";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(90, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "ex:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(93, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "ex:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(91, 173);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "ex:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtNamespaceBus);
            this.groupBox3.Controls.Add(this.lblNamespaceBus);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.lblNamespaceDto);
            this.groupBox3.Controls.Add(this.lblNamespaceDal);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.txtNamespaceDto);
            this.groupBox3.Controls.Add(this.txtNamespaceDal);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Location = new System.Drawing.Point(18, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(294, 205);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Namespace";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(104, 173);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "ex:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(106, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "ex:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(103, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "ex:";
            // 
            // txtNamespaceBus
            // 
            this.txtNamespaceBus.Location = new System.Drawing.Point(101, 23);
            this.txtNamespaceBus.Name = "txtNamespaceBus";
            this.txtNamespaceBus.Size = new System.Drawing.Size(176, 20);
            this.txtNamespaceBus.TabIndex = 1;
            this.txtNamespaceBus.TextChanged += new System.EventHandler(this.txtNamespaceBus_TextChanged);
            // 
            // lblNamespaceBus
            // 
            this.lblNamespaceBus.AutoSize = true;
            this.lblNamespaceBus.Location = new System.Drawing.Point(130, 46);
            this.lblNamespaceBus.Name = "lblNamespaceBus";
            this.lblNamespaceBus.Size = new System.Drawing.Size(76, 13);
            this.lblNamespaceBus.TabIndex = 0;
            this.lblNamespaceBus.Text = "Extention BUS";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Namespace BUS";
            // 
            // lblNamespaceDto
            // 
            this.lblNamespaceDto.AutoSize = true;
            this.lblNamespaceDto.Location = new System.Drawing.Point(131, 173);
            this.lblNamespaceDto.Name = "lblNamespaceDto";
            this.lblNamespaceDto.Size = new System.Drawing.Size(75, 13);
            this.lblNamespaceDto.TabIndex = 0;
            this.lblNamespaceDto.Text = "Extention DAL";
            // 
            // lblNamespaceDal
            // 
            this.lblNamespaceDal.AutoSize = true;
            this.lblNamespaceDal.Location = new System.Drawing.Point(130, 110);
            this.lblNamespaceDal.Name = "lblNamespaceDal";
            this.lblNamespaceDal.Size = new System.Drawing.Size(75, 13);
            this.lblNamespaceDal.TabIndex = 0;
            this.lblNamespaceDal.Text = "Extention DAL";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 89);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(88, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Namespace DAL";
            // 
            // txtNamespaceDto
            // 
            this.txtNamespaceDto.Location = new System.Drawing.Point(101, 150);
            this.txtNamespaceDto.Name = "txtNamespaceDto";
            this.txtNamespaceDto.Size = new System.Drawing.Size(176, 20);
            this.txtNamespaceDto.TabIndex = 1;
            this.txtNamespaceDto.TextChanged += new System.EventHandler(this.txtNamespaceDto_TextChanged);
            // 
            // txtNamespaceDal
            // 
            this.txtNamespaceDal.Location = new System.Drawing.Point(101, 87);
            this.txtNamespaceDal.Name = "txtNamespaceDal";
            this.txtNamespaceDal.Size = new System.Drawing.Size(176, 20);
            this.txtNamespaceDal.TabIndex = 1;
            this.txtNamespaceDal.TextChanged += new System.EventHandler(this.txtNamespaceDal_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(5, 151);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(90, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Namespace DTO";
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 409);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "frmSetting";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameBus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNameDal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNameDto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNameServer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Label lblBus;
        private System.Windows.Forms.Label lblDto;
        private System.Windows.Forms.Label lblDal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNamespaceBus;
        private System.Windows.Forms.Label lblNamespaceBus;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblNamespaceDto;
        private System.Windows.Forms.Label lblNamespaceDal;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtNamespaceDto;
        private System.Windows.Forms.TextBox txtNamespaceDal;
        private System.Windows.Forms.Label label18;
    }
}