namespace CarPyramid
{
	partial class ActvationForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActvationForm));
			this.btnActivation = new System.Windows.Forms.Button();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.txtCompanyName = new System.Windows.Forms.TextBox();
			this.lCompanyName = new System.Windows.Forms.Label();
			this.txtHarwareId = new System.Windows.Forms.TextBox();
			this.lHarwareId = new System.Windows.Forms.Label();
			this.QueryBox = new System.Windows.Forms.GroupBox();
			this.ResultBox = new System.Windows.Forms.GroupBox();
			this.txtProducKey = new System.Windows.Forms.MaskedTextBox();
			this.lProductKey = new System.Windows.Forms.Label();
			this.btnEmail = new System.Windows.Forms.Button();
			this.QueryBox.SuspendLayout();
			this.ResultBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnActivation
			// 
			this.btnActivation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnActivation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnActivation.ImageKey = "(none)";
			this.btnActivation.ImageList = this.imageList;
			this.btnActivation.Location = new System.Drawing.Point(53, 191);
			this.btnActivation.Name = "btnActivation";
			this.btnActivation.Size = new System.Drawing.Size(271, 36);
			this.btnActivation.TabIndex = 15;
			this.btnActivation.Text = "&Активировать";
			this.btnActivation.UseVisualStyleBackColor = true;
			this.btnActivation.Click += new System.EventHandler(this.btnActivation_Click);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "dialogErr.png");
			this.imageList.Images.SetKeyName(1, "dialogOk.png");
			// 
			// txtCompanyName
			// 
			this.txtCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCompanyName.Location = new System.Drawing.Point(102, 19);
			this.txtCompanyName.Name = "txtCompanyName";
			this.txtCompanyName.Size = new System.Drawing.Size(241, 20);
			this.txtCompanyName.TabIndex = 18;
			this.txtCompanyName.Click += new System.EventHandler(this.txt_Click);
			this.txtCompanyName.TextChanged += new System.EventHandler(this.txtCompanyName_TextChanged);
			this.txtCompanyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompanyName_KeyDown);
			this.txtCompanyName.Leave += new System.EventHandler(this.txtCompanyName_Leave);
			// 
			// lCompanyName
			// 
			this.lCompanyName.AutoSize = true;
			this.lCompanyName.Location = new System.Drawing.Point(19, 22);
			this.lCompanyName.Name = "lCompanyName";
			this.lCompanyName.Size = new System.Drawing.Size(77, 13);
			this.lCompanyName.TabIndex = 17;
			this.lCompanyName.Text = "Организация:";
			// 
			// txtHarwareId
			// 
			this.txtHarwareId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtHarwareId.Location = new System.Drawing.Point(102, 45);
			this.txtHarwareId.Name = "txtHarwareId";
			this.txtHarwareId.ReadOnly = true;
			this.txtHarwareId.Size = new System.Drawing.Size(241, 20);
			this.txtHarwareId.TabIndex = 20;
			this.txtHarwareId.Click += new System.EventHandler(this.txt_Click);
			// 
			// lHarwareId
			// 
			this.lHarwareId.AutoSize = true;
			this.lHarwareId.Location = new System.Drawing.Point(3, 48);
			this.lHarwareId.Name = "lHarwareId";
			this.lHarwareId.Size = new System.Drawing.Size(86, 13);
			this.lHarwareId.TabIndex = 21;
			this.lHarwareId.Text = "Код Активации:";
			// 
			// QueryBox
			// 
			this.QueryBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.QueryBox.Controls.Add(this.txtHarwareId);
			this.QueryBox.Controls.Add(this.txtCompanyName);
			this.QueryBox.Controls.Add(this.lHarwareId);
			this.QueryBox.Controls.Add(this.lCompanyName);
			this.QueryBox.Location = new System.Drawing.Point(12, 12);
			this.QueryBox.Name = "QueryBox";
			this.QueryBox.Size = new System.Drawing.Size(364, 81);
			this.QueryBox.TabIndex = 22;
			this.QueryBox.TabStop = false;
			this.QueryBox.Text = "1. Обратитесь к постащику. Укажите";
			// 
			// ResultBox
			// 
			this.ResultBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ResultBox.Controls.Add(this.txtProducKey);
			this.ResultBox.Controls.Add(this.lProductKey);
			this.ResultBox.Location = new System.Drawing.Point(12, 136);
			this.ResultBox.Name = "ResultBox";
			this.ResultBox.Size = new System.Drawing.Size(364, 49);
			this.ResultBox.TabIndex = 23;
			this.ResultBox.TabStop = false;
			this.ResultBox.Text = "2. Укажите, полученный от поставщика, Код";
			// 
			// txtProducKey
			// 
			this.txtProducKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtProducKey.Location = new System.Drawing.Point(102, 19);
			this.txtProducKey.Mask = "&&&&&&&&-&&&&&&&&-&&&&&&&&-&&&&&&&&";
			this.txtProducKey.Name = "txtProducKey";
			this.txtProducKey.Size = new System.Drawing.Size(241, 20);
			this.txtProducKey.TabIndex = 16;
			this.txtProducKey.Click += new System.EventHandler(this.txt_Click);
			this.txtProducKey.TextChanged += new System.EventHandler(this.txtProducKey_TextChanged);
			// 
			// lProductKey
			// 
			this.lProductKey.AutoSize = true;
			this.lProductKey.Location = new System.Drawing.Point(9, 22);
			this.lProductKey.Name = "lProductKey";
			this.lProductKey.Size = new System.Drawing.Size(87, 13);
			this.lProductKey.TabIndex = 15;
			this.lProductKey.Text = "Ключ Продукта:";
			// 
			// btnEmail
			// 
			this.btnEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEmail.Location = new System.Drawing.Point(53, 99);
			this.btnEmail.Name = "btnEmail";
			this.btnEmail.Size = new System.Drawing.Size(271, 30);
			this.btnEmail.TabIndex = 24;
			this.btnEmail.Text = "&Запросить Ключ Продукта";
			this.btnEmail.UseVisualStyleBackColor = true;
			this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
			this.btnEmail.Enter += new System.EventHandler(this.btnEmail_Enter);
			// 
			// ActvationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(387, 239);
			this.Controls.Add(this.btnEmail);
			this.Controls.Add(this.ResultBox);
			this.Controls.Add(this.QueryBox);
			this.Controls.Add(this.btnActivation);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ActvationForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Активация";
			this.QueryBox.ResumeLayout(false);
			this.QueryBox.PerformLayout();
			this.ResultBox.ResumeLayout(false);
			this.ResultBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnActivation;
		private System.Windows.Forms.TextBox txtCompanyName;
		private System.Windows.Forms.Label lCompanyName;
		private System.Windows.Forms.TextBox txtHarwareId;
		private System.Windows.Forms.Label lHarwareId;
		private System.Windows.Forms.GroupBox QueryBox;
		private System.Windows.Forms.GroupBox ResultBox;
		private System.Windows.Forms.MaskedTextBox txtProducKey;
		private System.Windows.Forms.Label lProductKey;
		private System.Windows.Forms.Button btnEmail;
		private System.Windows.Forms.ImageList imageList;
	}
}