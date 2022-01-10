namespace CarPyramid
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.panel_Top = new System.Windows.Forms.Panel();
			this.Actvation = new System.Windows.Forms.Button();
			this.LicList = new System.Windows.Forms.ImageList(this.components);
			this.Spacer = new System.Windows.Forms.Button();
			this.labelFile = new System.Windows.Forms.Label();
			this.panel_Bottom = new System.Windows.Forms.Panel();
			this.labelInform = new System.Windows.Forms.Label();
			this.AllLineOpt = new System.Windows.Forms.Button();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.CarOptAllSave = new System.Windows.Forms.Button();
			this.CarOptAll = new System.Windows.Forms.Button();
			this.SaveFile = new System.Windows.Forms.Button();
			this.CarOpt = new System.Windows.Forms.Button();
			this.LoadFile = new System.Windows.Forms.Button();
			this.GridView_Task = new System.Windows.Forms.DataGridView();
			this.OFD = new System.Windows.Forms.OpenFileDialog();
			this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.panel_Top.SuspendLayout();
			this.panel_Bottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridView_Task)).BeginInit();
			this.SuspendLayout();
			// 
			// panel_Top
			// 
			this.panel_Top.Controls.Add(this.Actvation);
			this.panel_Top.Controls.Add(this.Spacer);
			this.panel_Top.Controls.Add(this.labelFile);
			this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel_Top.Location = new System.Drawing.Point(0, 0);
			this.panel_Top.Name = "panel_Top";
			this.panel_Top.Size = new System.Drawing.Size(1085, 22);
			this.panel_Top.TabIndex = 0;
			// 
			// Actvation
			// 
			this.Actvation.AutoSize = true;
			this.Actvation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Actvation.Dock = System.Windows.Forms.DockStyle.Right;
			this.Actvation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.Actvation.ForeColor = System.Drawing.SystemColors.Highlight;
			this.Actvation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Actvation.ImageKey = "key.png";
			this.Actvation.ImageList = this.LicList;
			this.Actvation.Location = new System.Drawing.Point(848, 0);
			this.Actvation.Margin = new System.Windows.Forms.Padding(0);
			this.Actvation.Name = "Actvation";
			this.Actvation.Size = new System.Drawing.Size(87, 22);
			this.Actvation.TabIndex = 3;
			this.Actvation.Text = "&Активация";
			this.Actvation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.Actvation.UseVisualStyleBackColor = true;
			this.Actvation.Click += new System.EventHandler(this.Actvation_Click);
			// 
			// LicList
			// 
			this.LicList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("LicList.ImageStream")));
			this.LicList.TransparentColor = System.Drawing.Color.Transparent;
			this.LicList.Images.SetKeyName(0, "key.png");
			this.LicList.Images.SetKeyName(1, "licence.png");
			// 
			// Spacer
			// 
			this.Spacer.AutoSize = true;
			this.Spacer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Spacer.Dock = System.Windows.Forms.DockStyle.Right;
			this.Spacer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.Spacer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Spacer.ImageKey = "licence.png";
			this.Spacer.ImageList = this.LicList;
			this.Spacer.Location = new System.Drawing.Point(935, 0);
			this.Spacer.MinimumSize = new System.Drawing.Size(150, 0);
			this.Spacer.Name = "Spacer";
			this.Spacer.Size = new System.Drawing.Size(150, 22);
			this.Spacer.TabIndex = 2;
			this.Spacer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Spacer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.Spacer.UseVisualStyleBackColor = true;
			this.Spacer.Click += new System.EventHandler(this.Spacer_Click);
			// 
			// labelFile
			// 
			this.labelFile.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelFile.Location = new System.Drawing.Point(0, 0);
			this.labelFile.Name = "labelFile";
			this.labelFile.Size = new System.Drawing.Size(185, 22);
			this.labelFile.TabIndex = 1;
			this.labelFile.Text = "Список Заданий:";
			this.labelFile.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// panel_Bottom
			// 
			this.panel_Bottom.Controls.Add(this.labelInform);
			this.panel_Bottom.Controls.Add(this.AllLineOpt);
			this.panel_Bottom.Controls.Add(this.CarOptAllSave);
			this.panel_Bottom.Controls.Add(this.CarOptAll);
			this.panel_Bottom.Controls.Add(this.SaveFile);
			this.panel_Bottom.Controls.Add(this.CarOpt);
			this.panel_Bottom.Controls.Add(this.LoadFile);
			this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel_Bottom.Location = new System.Drawing.Point(0, 460);
			this.panel_Bottom.Name = "panel_Bottom";
			this.panel_Bottom.Size = new System.Drawing.Size(1085, 30);
			this.panel_Bottom.TabIndex = 1;
			// 
			// labelInform
			// 
			this.labelInform.AutoSize = true;
			this.labelInform.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelInform.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelInform.Location = new System.Drawing.Point(674, 0);
			this.labelInform.Name = "labelInform";
			this.labelInform.Size = new System.Drawing.Size(90, 20);
			this.labelInform.TabIndex = 1;
			this.labelInform.Text = "OptResult";
			// 
			// AllLineOpt
			// 
			this.AllLineOpt.Dock = System.Windows.Forms.DockStyle.Left;
			this.AllLineOpt.Enabled = false;
			this.AllLineOpt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.AllLineOpt.ImageKey = "lineOpt.png";
			this.AllLineOpt.ImageList = this.imageList;
			this.AllLineOpt.Location = new System.Drawing.Point(517, 0);
			this.AllLineOpt.Name = "AllLineOpt";
			this.AllLineOpt.Size = new System.Drawing.Size(157, 30);
			this.AllLineOpt.TabIndex = 3;
			this.AllLineOpt.Text = "Линейная оптимизация";
			this.AllLineOpt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.AllLineOpt.UseVisualStyleBackColor = true;
			this.AllLineOpt.Click += new System.EventHandler(this.AllLineOpt_Click);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "open.png");
			this.imageList.Images.SetKeyName(1, "car.png");
			this.imageList.Images.SetKeyName(2, "carAll.png");
			this.imageList.Images.SetKeyName(3, "lineOpt.png");
			this.imageList.Images.SetKeyName(4, "save.png");
			this.imageList.Images.SetKeyName(5, "folder.png");
			// 
			// CarOptAllSave
			// 
			this.CarOptAllSave.Dock = System.Windows.Forms.DockStyle.Left;
			this.CarOptAllSave.Enabled = false;
			this.CarOptAllSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CarOptAllSave.ImageKey = "folder.png";
			this.CarOptAllSave.ImageList = this.imageList;
			this.CarOptAllSave.Location = new System.Drawing.Point(403, 0);
			this.CarOptAllSave.Name = "CarOptAllSave";
			this.CarOptAllSave.Size = new System.Drawing.Size(114, 30);
			this.CarOptAllSave.TabIndex = 6;
			this.CarOptAllSave.Text = "И сохранить";
			this.CarOptAllSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.CarOptAllSave.UseVisualStyleBackColor = true;
			this.CarOptAllSave.Click += new System.EventHandler(this.CarOptAll_Click);
			// 
			// CarOptAll
			// 
			this.CarOptAll.Dock = System.Windows.Forms.DockStyle.Left;
			this.CarOptAll.Enabled = false;
			this.CarOptAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CarOptAll.ImageKey = "carAll.png";
			this.CarOptAll.ImageList = this.imageList;
			this.CarOptAll.Location = new System.Drawing.Point(273, 0);
			this.CarOptAll.Name = "CarOptAll";
			this.CarOptAll.Size = new System.Drawing.Size(130, 30);
			this.CarOptAll.TabIndex = 5;
			this.CarOptAll.Text = "Подобрать Всем";
			this.CarOptAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.CarOptAll.UseVisualStyleBackColor = true;
			this.CarOptAll.Click += new System.EventHandler(this.CarOptAll_Click);
			// 
			// SaveFile
			// 
			this.SaveFile.Dock = System.Windows.Forms.DockStyle.Right;
			this.SaveFile.Enabled = false;
			this.SaveFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.SaveFile.ImageKey = "save.png";
			this.SaveFile.ImageList = this.imageList;
			this.SaveFile.Location = new System.Drawing.Point(929, 0);
			this.SaveFile.Name = "SaveFile";
			this.SaveFile.Size = new System.Drawing.Size(156, 30);
			this.SaveFile.TabIndex = 4;
			this.SaveFile.Text = "Сохранить Результат";
			this.SaveFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.SaveFile.UseVisualStyleBackColor = true;
			this.SaveFile.Click += new System.EventHandler(this.SaveFile_Click);
			// 
			// CarOpt
			// 
			this.CarOpt.Dock = System.Windows.Forms.DockStyle.Left;
			this.CarOpt.Enabled = false;
			this.CarOpt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CarOpt.ImageKey = "car.png";
			this.CarOpt.ImageList = this.imageList;
			this.CarOpt.Location = new System.Drawing.Point(137, 0);
			this.CarOpt.Name = "CarOpt";
			this.CarOpt.Size = new System.Drawing.Size(136, 30);
			this.CarOpt.TabIndex = 2;
			this.CarOpt.Text = "Подобрать Машину";
			this.CarOpt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.CarOpt.UseVisualStyleBackColor = true;
			this.CarOpt.Click += new System.EventHandler(this.CarOpt_Click);
			// 
			// LoadFile
			// 
			this.LoadFile.Dock = System.Windows.Forms.DockStyle.Left;
			this.LoadFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LoadFile.ImageKey = "open.png";
			this.LoadFile.ImageList = this.imageList;
			this.LoadFile.Location = new System.Drawing.Point(0, 0);
			this.LoadFile.Margin = new System.Windows.Forms.Padding(0);
			this.LoadFile.Name = "LoadFile";
			this.LoadFile.Size = new System.Drawing.Size(137, 30);
			this.LoadFile.TabIndex = 0;
			this.LoadFile.Text = "Загрузить Задания";
			this.LoadFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.LoadFile.UseVisualStyleBackColor = true;
			this.LoadFile.Click += new System.EventHandler(this.LoadFile_Click);
			// 
			// GridView_Task
			// 
			this.GridView_Task.AllowUserToAddRows = false;
			this.GridView_Task.AllowUserToDeleteRows = false;
			this.GridView_Task.AllowUserToResizeRows = false;
			this.GridView_Task.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.GridView_Task.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.GridView_Task.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GridView_Task.Location = new System.Drawing.Point(0, 22);
			this.GridView_Task.MultiSelect = false;
			this.GridView_Task.Name = "GridView_Task";
			this.GridView_Task.ReadOnly = true;
			this.GridView_Task.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.GridView_Task.Size = new System.Drawing.Size(1085, 438);
			this.GridView_Task.TabIndex = 2;
			this.GridView_Task.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridView_Task_CellDoubleClick);
			this.GridView_Task.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridView_Task_CellFormatting);
			this.GridView_Task.CurrentCellChanged += new System.EventHandler(this.GridView_Task_CurrentCellChanged);
			// 
			// OFD
			// 
			this.OFD.DefaultExt = "tsk";
			this.OFD.Filter = "Задания|*.xml;";
			this.OFD.Multiselect = true;
			this.OFD.SupportMultiDottedExtensions = true;
			this.OFD.Title = "Файлы Заданий";
			// 
			// NotifyIcon
			// 
			this.NotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.NotifyIcon.BalloonTipText = "BalloonTipText";
			this.NotifyIcon.BalloonTipTitle = "BalloonTipTitle";
			this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
			this.NotifyIcon.Text = "CarPyramid";
			this.NotifyIcon.Visible = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1085, 490);
			this.Controls.Add(this.GridView_Task);
			this.Controls.Add(this.panel_Bottom);
			this.Controls.Add(this.panel_Top);
			this.Name = "MainForm";
			this.Text = "CarPyramidApp";
			this.panel_Top.ResumeLayout(false);
			this.panel_Top.PerformLayout();
			this.panel_Bottom.ResumeLayout(false);
			this.panel_Bottom.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridView_Task)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel_Top;
		private System.Windows.Forms.Panel panel_Bottom;
		private System.Windows.Forms.DataGridView GridView_Task;
		private System.Windows.Forms.Button LoadFile;
		private System.Windows.Forms.OpenFileDialog OFD;
		private System.Windows.Forms.Label labelInform;
		private System.Windows.Forms.Button AllLineOpt;
		private System.Windows.Forms.Button CarOpt;
		private System.Windows.Forms.Button SaveFile;
		private System.Windows.Forms.Button CarOptAll;
		private System.Windows.Forms.NotifyIcon NotifyIcon;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Label labelFile;
		private System.Windows.Forms.Button Actvation;
		private System.Windows.Forms.ImageList LicList;
		private System.Windows.Forms.Button Spacer;
		private System.Windows.Forms.Button CarOptAllSave;
	}
}

