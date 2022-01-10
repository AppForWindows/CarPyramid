namespace CarPyramid
{
	partial class TaskForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskForm));
			this.TC = new System.Windows.Forms.TabControl();
			this.TPord = new System.Windows.Forms.TabPage();
			this.SCord = new System.Windows.Forms.SplitContainer();
			this.TPcar = new System.Windows.Forms.TabPage();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.DGVord = new System.Windows.Forms.DataGridView();
			this.DGVit = new System.Windows.Forms.DataGridView();
			this.SCcar = new System.Windows.Forms.SplitContainer();
			this.Li = new System.Windows.Forms.Label();
			this.DGVitem = new System.Windows.Forms.DataGridView();
			this.TC.SuspendLayout();
			this.TPord.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SCord)).BeginInit();
			this.SCord.Panel1.SuspendLayout();
			this.SCord.Panel2.SuspendLayout();
			this.SCord.SuspendLayout();
			this.TPcar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DGVord)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DGVit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SCcar)).BeginInit();
			this.SCcar.Panel2.SuspendLayout();
			this.SCcar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DGVitem)).BeginInit();
			this.SuspendLayout();
			// 
			// TC
			// 
			this.TC.Controls.Add(this.TPord);
			this.TC.Controls.Add(this.TPcar);
			this.TC.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TC.ImageList = this.imageList;
			this.TC.Location = new System.Drawing.Point(0, 0);
			this.TC.Name = "TC";
			this.TC.SelectedIndex = 0;
			this.TC.Size = new System.Drawing.Size(1213, 648);
			this.TC.TabIndex = 0;
			// 
			// TPord
			// 
			this.TPord.Controls.Add(this.SCord);
			this.TPord.ImageKey = "order.png";
			this.TPord.Location = new System.Drawing.Point(4, 23);
			this.TPord.Name = "TPord";
			this.TPord.Padding = new System.Windows.Forms.Padding(3);
			this.TPord.Size = new System.Drawing.Size(1205, 621);
			this.TPord.TabIndex = 0;
			this.TPord.Text = "Заказы и Изделия";
			this.TPord.UseVisualStyleBackColor = true;
			// 
			// SCord
			// 
			this.SCord.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SCord.Location = new System.Drawing.Point(3, 3);
			this.SCord.Name = "SCord";
			// 
			// SCord.Panel1
			// 
			this.SCord.Panel1.Controls.Add(this.DGVord);
			// 
			// SCord.Panel2
			// 
			this.SCord.Panel2.Controls.Add(this.DGVit);
			this.SCord.Size = new System.Drawing.Size(1199, 615);
			this.SCord.SplitterDistance = 399;
			this.SCord.TabIndex = 0;
			// 
			// TPcar
			// 
			this.TPcar.Controls.Add(this.SCcar);
			this.TPcar.ImageKey = "car.png";
			this.TPcar.Location = new System.Drawing.Point(4, 23);
			this.TPcar.Name = "TPcar";
			this.TPcar.Padding = new System.Windows.Forms.Padding(3);
			this.TPcar.Size = new System.Drawing.Size(1205, 621);
			this.TPcar.TabIndex = 1;
			this.TPcar.Text = "TPcar";
			this.TPcar.UseVisualStyleBackColor = true;
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "car.png");
			this.imageList.Images.SetKeyName(1, "items.png");
			this.imageList.Images.SetKeyName(2, "order.png");
			this.imageList.Images.SetKeyName(3, "pyramid.png");
			// 
			// DGVord
			// 
			this.DGVord.AllowUserToAddRows = false;
			this.DGVord.AllowUserToDeleteRows = false;
			this.DGVord.AllowUserToResizeRows = false;
			this.DGVord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DGVord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DGVord.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DGVord.Location = new System.Drawing.Point(0, 0);
			this.DGVord.MultiSelect = false;
			this.DGVord.Name = "DGVord";
			this.DGVord.ReadOnly = true;
			this.DGVord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DGVord.Size = new System.Drawing.Size(399, 615);
			this.DGVord.TabIndex = 0;
			this.DGVord.CurrentCellChanged += new System.EventHandler(this.DGVord_CurrentCellChanged);
			// 
			// DGVit
			// 
			this.DGVit.AllowUserToAddRows = false;
			this.DGVit.AllowUserToDeleteRows = false;
			this.DGVit.AllowUserToResizeRows = false;
			this.DGVit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DGVit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DGVit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DGVit.Location = new System.Drawing.Point(0, 0);
			this.DGVit.MultiSelect = false;
			this.DGVit.Name = "DGVit";
			this.DGVit.ReadOnly = true;
			this.DGVit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DGVit.Size = new System.Drawing.Size(796, 615);
			this.DGVit.TabIndex = 0;
			// 
			// SCcar
			// 
			this.SCcar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SCcar.Location = new System.Drawing.Point(3, 3);
			this.SCcar.Name = "SCcar";
			this.SCcar.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// SCcar.Panel1
			// 
			this.SCcar.Panel1.AutoScroll = true;
			// 
			// SCcar.Panel2
			// 
			this.SCcar.Panel2.Controls.Add(this.DGVitem);
			this.SCcar.Panel2.Controls.Add(this.Li);
			this.SCcar.Size = new System.Drawing.Size(1199, 615);
			this.SCcar.SplitterDistance = 407;
			this.SCcar.TabIndex = 0;
			// 
			// Li
			// 
			this.Li.AutoSize = true;
			this.Li.Dock = System.Windows.Forms.DockStyle.Top;
			this.Li.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Li.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Li.ImageKey = "items.png";
			this.Li.ImageList = this.imageList;
			this.Li.Location = new System.Drawing.Point(0, 0);
			this.Li.Name = "Li";
			this.Li.Size = new System.Drawing.Size(274, 16);
			this.Li.TabIndex = 0;
			this.Li.Text = "            СОСТАВ ВЫБРАННОГО РЯДА:";
			this.Li.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DGVitem
			// 
			this.DGVitem.AllowUserToAddRows = false;
			this.DGVitem.AllowUserToDeleteRows = false;
			this.DGVitem.AllowUserToResizeRows = false;
			this.DGVitem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DGVitem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DGVitem.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DGVitem.Location = new System.Drawing.Point(0, 16);
			this.DGVitem.MultiSelect = false;
			this.DGVitem.Name = "DGVitem";
			this.DGVitem.ReadOnly = true;
			this.DGVitem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DGVitem.Size = new System.Drawing.Size(1199, 188);
			this.DGVitem.TabIndex = 1;
			// 
			// TaskForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1213, 648);
			this.Controls.Add(this.TC);
			this.Name = "TaskForm";
			this.Text = "TaskForm";
			this.TC.ResumeLayout(false);
			this.TPord.ResumeLayout(false);
			this.SCord.Panel1.ResumeLayout(false);
			this.SCord.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SCord)).EndInit();
			this.SCord.ResumeLayout(false);
			this.TPcar.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DGVord)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DGVit)).EndInit();
			this.SCcar.Panel2.ResumeLayout(false);
			this.SCcar.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.SCcar)).EndInit();
			this.SCcar.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DGVitem)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl TC;
		private System.Windows.Forms.TabPage TPord;
		private System.Windows.Forms.TabPage TPcar;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.SplitContainer SCord;
		private System.Windows.Forms.DataGridView DGVord;
		private System.Windows.Forms.DataGridView DGVit;
		private System.Windows.Forms.SplitContainer SCcar;
		private System.Windows.Forms.Label Li;
		private System.Windows.Forms.DataGridView DGVitem;
	}
}