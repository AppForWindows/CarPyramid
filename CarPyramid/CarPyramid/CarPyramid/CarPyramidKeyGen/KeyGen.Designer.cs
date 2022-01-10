namespace CarPyramidKeyGen
{
	partial class KeyGen
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
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.KeyGenGroupBox = new System.Windows.Forms.GroupBox();
			this.Generate = new System.Windows.Forms.Button();
			this.gbProductKey = new System.Windows.Forms.GroupBox();
			this.txtProductKey = new System.Windows.Forms.TextBox();
			this.gbHardwareId = new System.Windows.Forms.GroupBox();
			this.txtHardwareId = new System.Windows.Forms.MaskedTextBox();
			this.gbCompanyName = new System.Windows.Forms.GroupBox();
			this.txtCompanyName = new System.Windows.Forms.TextBox();
			this.gbEmail = new System.Windows.Forms.GroupBox();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.KeyGenerator = new System.Windows.Forms.TabPage();
			this.Integration = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.TaskText = new System.Windows.Forms.TextBox();
			this.panelBot = new System.Windows.Forms.Panel();
			this.LoadFile = new System.Windows.Forms.Button();
			this.DoWork2 = new System.Windows.Forms.Button();
			this.DoWork1 = new System.Windows.Forms.Button();
			this.ResultText = new System.Windows.Forms.TextBox();
			this.panelBot2 = new System.Windows.Forms.Panel();
			this.btnSave = new System.Windows.Forms.Button();
			this.OFD = new System.Windows.Forms.OpenFileDialog();
			this.SFD = new System.Windows.Forms.SaveFileDialog();
			this.panelBotEmail = new System.Windows.Forms.Panel();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.btnSendEmail = new System.Windows.Forms.Button();
			this.txtSubject = new System.Windows.Forms.TextBox();
			this.panelTopEmail = new System.Windows.Forms.Panel();
			this.lSubject = new System.Windows.Forms.Label();
			this.txtBody = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.KeyGenGroupBox.SuspendLayout();
			this.gbProductKey.SuspendLayout();
			this.gbHardwareId.SuspendLayout();
			this.gbCompanyName.SuspendLayout();
			this.gbEmail.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.KeyGenerator.SuspendLayout();
			this.Integration.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panelBot.SuspendLayout();
			this.panelBot2.SuspendLayout();
			this.panelBotEmail.SuspendLayout();
			this.panelTopEmail.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.KeyGenGroupBox);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.gbEmail);
			this.splitContainer2.Size = new System.Drawing.Size(1223, 340);
			this.splitContainer2.SplitterDistance = 475;
			this.splitContainer2.TabIndex = 3;
			// 
			// KeyGenGroupBox
			// 
			this.KeyGenGroupBox.Controls.Add(this.Generate);
			this.KeyGenGroupBox.Controls.Add(this.gbProductKey);
			this.KeyGenGroupBox.Controls.Add(this.gbHardwareId);
			this.KeyGenGroupBox.Controls.Add(this.gbCompanyName);
			this.KeyGenGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.KeyGenGroupBox.Location = new System.Drawing.Point(0, 0);
			this.KeyGenGroupBox.Name = "KeyGenGroupBox";
			this.KeyGenGroupBox.Size = new System.Drawing.Size(475, 340);
			this.KeyGenGroupBox.TabIndex = 0;
			this.KeyGenGroupBox.TabStop = false;
			this.KeyGenGroupBox.Text = "KeyGenerator";
			// 
			// Generate
			// 
			this.Generate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Generate.Enabled = false;
			this.Generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Generate.Location = new System.Drawing.Point(68, 180);
			this.Generate.Name = "Generate";
			this.Generate.Size = new System.Drawing.Size(310, 47);
			this.Generate.TabIndex = 3;
			this.Generate.Text = "&Создать Ключ Продукта";
			this.Generate.UseVisualStyleBackColor = true;
			this.Generate.Click += new System.EventHandler(this.Generate_Click);
			// 
			// gbProductKey
			// 
			this.gbProductKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbProductKey.Controls.Add(this.txtProductKey);
			this.gbProductKey.Location = new System.Drawing.Point(18, 253);
			this.gbProductKey.Name = "gbProductKey";
			this.gbProductKey.Size = new System.Drawing.Size(441, 54);
			this.gbProductKey.TabIndex = 2;
			this.gbProductKey.TabStop = false;
			this.gbProductKey.Text = "Ключ Продукта:";
			// 
			// txtProductKey
			// 
			this.txtProductKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtProductKey.Enabled = false;
			this.txtProductKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtProductKey.Location = new System.Drawing.Point(6, 19);
			this.txtProductKey.Name = "txtProductKey";
			this.txtProductKey.ReadOnly = true;
			this.txtProductKey.Size = new System.Drawing.Size(428, 29);
			this.txtProductKey.TabIndex = 0;
			this.txtProductKey.Click += new System.EventHandler(this.txt_Click);
			// 
			// gbHardwareId
			// 
			this.gbHardwareId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbHardwareId.Controls.Add(this.txtHardwareId);
			this.gbHardwareId.Location = new System.Drawing.Point(12, 104);
			this.gbHardwareId.Name = "gbHardwareId";
			this.gbHardwareId.Size = new System.Drawing.Size(447, 54);
			this.gbHardwareId.TabIndex = 1;
			this.gbHardwareId.TabStop = false;
			this.gbHardwareId.Text = "Код Активации Клиента:";
			// 
			// txtHardwareId
			// 
			this.txtHardwareId.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtHardwareId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtHardwareId.Location = new System.Drawing.Point(6, 19);
			this.txtHardwareId.Mask = "&&&&&&&&-&&&&&&&&-&&&&&&&&-&&&&&&&&";
			this.txtHardwareId.Name = "txtHardwareId";
			this.txtHardwareId.Size = new System.Drawing.Size(435, 29);
			this.txtHardwareId.TabIndex = 0;
			this.txtHardwareId.Click += new System.EventHandler(this.txt_Click);
			this.txtHardwareId.TextChanged += new System.EventHandler(this.txt_TextChanged);
			this.txtHardwareId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHardwareId_KeyDown);
			// 
			// gbCompanyName
			// 
			this.gbCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbCompanyName.Controls.Add(this.txtCompanyName);
			this.gbCompanyName.Location = new System.Drawing.Point(12, 29);
			this.gbCompanyName.Name = "gbCompanyName";
			this.gbCompanyName.Size = new System.Drawing.Size(447, 54);
			this.gbCompanyName.TabIndex = 0;
			this.gbCompanyName.TabStop = false;
			this.gbCompanyName.Text = "Организация:";
			// 
			// txtCompanyName
			// 
			this.txtCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtCompanyName.Location = new System.Drawing.Point(6, 19);
			this.txtCompanyName.Name = "txtCompanyName";
			this.txtCompanyName.Size = new System.Drawing.Size(434, 29);
			this.txtCompanyName.TabIndex = 0;
			this.txtCompanyName.Click += new System.EventHandler(this.txt_Click);
			this.txtCompanyName.TextChanged += new System.EventHandler(this.txt_TextChanged);
			// 
			// gbEmail
			// 
			this.gbEmail.Controls.Add(this.txtBody);
			this.gbEmail.Controls.Add(this.panelTopEmail);
			this.gbEmail.Controls.Add(this.panelBotEmail);
			this.gbEmail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbEmail.Location = new System.Drawing.Point(0, 0);
			this.gbEmail.Name = "gbEmail";
			this.gbEmail.Size = new System.Drawing.Size(744, 340);
			this.gbEmail.TabIndex = 0;
			this.gbEmail.TabStop = false;
			this.gbEmail.Text = "Email сообщение Клиенту.";
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.KeyGenerator);
			this.tabControl.Controls.Add(this.Integration);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(1237, 372);
			this.tabControl.TabIndex = 4;
			// 
			// KeyGenerator
			// 
			this.KeyGenerator.Controls.Add(this.splitContainer2);
			this.KeyGenerator.Location = new System.Drawing.Point(4, 22);
			this.KeyGenerator.Name = "KeyGenerator";
			this.KeyGenerator.Padding = new System.Windows.Forms.Padding(3);
			this.KeyGenerator.Size = new System.Drawing.Size(1229, 346);
			this.KeyGenerator.TabIndex = 0;
			this.KeyGenerator.Text = "ГЕНЕРАЦИЯ ProductKey Клиентов";
			this.KeyGenerator.UseVisualStyleBackColor = true;
			// 
			// Integration
			// 
			this.Integration.Controls.Add(this.splitContainer1);
			this.Integration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Integration.Location = new System.Drawing.Point(4, 22);
			this.Integration.Name = "Integration";
			this.Integration.Padding = new System.Windows.Forms.Padding(3);
			this.Integration.Size = new System.Drawing.Size(1229, 346);
			this.Integration.TabIndex = 1;
			this.Integration.Text = "Integrator - Прямой Вызов DLL";
			this.Integration.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.TaskText);
			this.splitContainer1.Panel1.Controls.Add(this.panelBot);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.ResultText);
			this.splitContainer1.Panel2.Controls.Add(this.panelBot2);
			this.splitContainer1.Size = new System.Drawing.Size(1223, 340);
			this.splitContainer1.SplitterDistance = 518;
			this.splitContainer1.TabIndex = 0;
			// 
			// TaskText
			// 
			this.TaskText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TaskText.Location = new System.Drawing.Point(0, 0);
			this.TaskText.Multiline = true;
			this.TaskText.Name = "TaskText";
			this.TaskText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TaskText.Size = new System.Drawing.Size(518, 316);
			this.TaskText.TabIndex = 2;
			this.TaskText.WordWrap = false;
			// 
			// panelBot
			// 
			this.panelBot.Controls.Add(this.LoadFile);
			this.panelBot.Controls.Add(this.DoWork2);
			this.panelBot.Controls.Add(this.DoWork1);
			this.panelBot.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBot.Location = new System.Drawing.Point(0, 316);
			this.panelBot.Name = "panelBot";
			this.panelBot.Size = new System.Drawing.Size(518, 24);
			this.panelBot.TabIndex = 1;
			// 
			// LoadFile
			// 
			this.LoadFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LoadFile.Location = new System.Drawing.Point(0, 0);
			this.LoadFile.Name = "LoadFile";
			this.LoadFile.Size = new System.Drawing.Size(258, 24);
			this.LoadFile.TabIndex = 1;
			this.LoadFile.Text = "LoadFile";
			this.LoadFile.UseVisualStyleBackColor = true;
			this.LoadFile.Click += new System.EventHandler(this.LoadFile_Click);
			// 
			// DoWork2
			// 
			this.DoWork2.Dock = System.Windows.Forms.DockStyle.Right;
			this.DoWork2.Location = new System.Drawing.Point(258, 0);
			this.DoWork2.Name = "DoWork2";
			this.DoWork2.Size = new System.Drawing.Size(130, 24);
			this.DoWork2.TabIndex = 2;
			this.DoWork2.Text = "DoWork2";
			this.DoWork2.UseVisualStyleBackColor = true;
			this.DoWork2.Click += new System.EventHandler(this.DoWork2_Click);
			// 
			// DoWork1
			// 
			this.DoWork1.Dock = System.Windows.Forms.DockStyle.Right;
			this.DoWork1.Location = new System.Drawing.Point(388, 0);
			this.DoWork1.Name = "DoWork1";
			this.DoWork1.Size = new System.Drawing.Size(130, 24);
			this.DoWork1.TabIndex = 0;
			this.DoWork1.Text = "DoWork1";
			this.DoWork1.UseVisualStyleBackColor = true;
			this.DoWork1.Click += new System.EventHandler(this.DoWork1_Click);
			// 
			// ResultText
			// 
			this.ResultText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ResultText.Location = new System.Drawing.Point(0, 0);
			this.ResultText.Multiline = true;
			this.ResultText.Name = "ResultText";
			this.ResultText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.ResultText.Size = new System.Drawing.Size(701, 316);
			this.ResultText.TabIndex = 3;
			this.ResultText.WordWrap = false;
			// 
			// panelBot2
			// 
			this.panelBot2.Controls.Add(this.btnSave);
			this.panelBot2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBot2.Location = new System.Drawing.Point(0, 316);
			this.panelBot2.Name = "panelBot2";
			this.panelBot2.Size = new System.Drawing.Size(701, 24);
			this.panelBot2.TabIndex = 4;
			// 
			// btnSave
			// 
			this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnSave.Location = new System.Drawing.Point(0, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(701, 24);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "Сохранить";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// OFD
			// 
			this.OFD.DefaultExt = "cptsk";
			this.OFD.Filter = "Задания|*.xml;*.cptsk|All Files|*.*";
			this.OFD.SupportMultiDottedExtensions = true;
			this.OFD.Title = "Файлы Заданий";
			// 
			// SFD
			// 
			this.SFD.DefaultExt = "cpres";
			this.SFD.FileName = "TaskResult";
			this.SFD.Filter = "Задания|*.xml;*.cptsk|All Files|*.*";
			this.SFD.SupportMultiDottedExtensions = true;
			this.SFD.Title = "Сохранить Результат";
			// 
			// panelBotEmail
			// 
			this.panelBotEmail.Controls.Add(this.txtAddress);
			this.panelBotEmail.Controls.Add(this.btnSendEmail);
			this.panelBotEmail.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBotEmail.Location = new System.Drawing.Point(3, 310);
			this.panelBotEmail.Name = "panelBotEmail";
			this.panelBotEmail.Size = new System.Drawing.Size(738, 27);
			this.panelBotEmail.TabIndex = 0;
			// 
			// txtAddress
			// 
			this.txtAddress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtAddress.Location = new System.Drawing.Point(0, 0);
			this.txtAddress.Multiline = true;
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(541, 27);
			this.txtAddress.TabIndex = 0;
			this.txtAddress.Click += new System.EventHandler(this.txt_Click);
			// 
			// btnSendEmail
			// 
			this.btnSendEmail.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnSendEmail.Location = new System.Drawing.Point(541, 0);
			this.btnSendEmail.Name = "btnSendEmail";
			this.btnSendEmail.Size = new System.Drawing.Size(197, 27);
			this.btnSendEmail.TabIndex = 1;
			this.btnSendEmail.Text = "Создать Письмо";
			this.btnSendEmail.UseVisualStyleBackColor = true;
			this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
			// 
			// txtSubject
			// 
			this.txtSubject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtSubject.Location = new System.Drawing.Point(59, 0);
			this.txtSubject.Multiline = true;
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new System.Drawing.Size(679, 18);
			this.txtSubject.TabIndex = 1;
			this.txtSubject.Text = "Ключ Продукта CarPyramid";
			// 
			// panelTopEmail
			// 
			this.panelTopEmail.Controls.Add(this.txtSubject);
			this.panelTopEmail.Controls.Add(this.lSubject);
			this.panelTopEmail.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTopEmail.Location = new System.Drawing.Point(3, 16);
			this.panelTopEmail.Name = "panelTopEmail";
			this.panelTopEmail.Size = new System.Drawing.Size(738, 18);
			this.panelTopEmail.TabIndex = 2;
			// 
			// lSubject
			// 
			this.lSubject.Dock = System.Windows.Forms.DockStyle.Left;
			this.lSubject.Location = new System.Drawing.Point(0, 0);
			this.lSubject.Name = "lSubject";
			this.lSubject.Size = new System.Drawing.Size(59, 18);
			this.lSubject.TabIndex = 2;
			this.lSubject.Text = "Тема:";
			this.lSubject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtBody
			// 
			this.txtBody.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtBody.Location = new System.Drawing.Point(3, 34);
			this.txtBody.Multiline = true;
			this.txtBody.Name = "txtBody";
			this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtBody.Size = new System.Drawing.Size(738, 276);
			this.txtBody.TabIndex = 3;
			this.txtBody.WordWrap = false;
			// 
			// KeyGen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1237, 372);
			this.Controls.Add(this.tabControl);
			this.Name = "KeyGen";
			this.Text = "CarPyramidKeyGen";
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.KeyGenGroupBox.ResumeLayout(false);
			this.gbProductKey.ResumeLayout(false);
			this.gbProductKey.PerformLayout();
			this.gbHardwareId.ResumeLayout(false);
			this.gbHardwareId.PerformLayout();
			this.gbCompanyName.ResumeLayout(false);
			this.gbCompanyName.PerformLayout();
			this.gbEmail.ResumeLayout(false);
			this.gbEmail.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.KeyGenerator.ResumeLayout(false);
			this.Integration.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panelBot.ResumeLayout(false);
			this.panelBot2.ResumeLayout(false);
			this.panelBotEmail.ResumeLayout(false);
			this.panelBotEmail.PerformLayout();
			this.panelTopEmail.ResumeLayout(false);
			this.panelTopEmail.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.GroupBox KeyGenGroupBox;
		private System.Windows.Forms.GroupBox gbEmail;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage KeyGenerator;
		private System.Windows.Forms.TabPage Integration;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panelBot;
		private System.Windows.Forms.Button DoWork1;
		private System.Windows.Forms.Button LoadFile;
		private System.Windows.Forms.OpenFileDialog OFD;
		private System.Windows.Forms.Button DoWork2;
		private System.Windows.Forms.TextBox TaskText;
		private System.Windows.Forms.TextBox ResultText;
		private System.Windows.Forms.GroupBox gbCompanyName;
		private System.Windows.Forms.GroupBox gbProductKey;
		private System.Windows.Forms.TextBox txtProductKey;
		private System.Windows.Forms.GroupBox gbHardwareId;
		private System.Windows.Forms.TextBox txtCompanyName;
		private System.Windows.Forms.Panel panelBot2;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.SaveFileDialog SFD;
		private System.Windows.Forms.MaskedTextBox txtHardwareId;
		private System.Windows.Forms.Button Generate;
		private System.Windows.Forms.Panel panelBotEmail;
		private System.Windows.Forms.TextBox txtBody;
		private System.Windows.Forms.Panel panelTopEmail;
		private System.Windows.Forms.TextBox txtSubject;
		private System.Windows.Forms.Label lSubject;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.Button btnSendEmail;
	}
}

