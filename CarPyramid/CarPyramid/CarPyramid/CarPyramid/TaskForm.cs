using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Classes;
using CarPyramid.LineOpt;

namespace CarPyramid
{
	public partial class TaskForm : Form
	{
		Task Task;
		public TaskForm(Task T)
		{
			InitializeComponent();

			this.Icon = Properties.Resources.result;
			this.Text = T.NameTask;
			this.Task = T;

			DGVord.DataSource = this.Task.Orders;

			if (this.Task.Car == null)
			{
				TC.TabPages.Remove(TPcar);
				this.Icon = Properties.Resources.task;
			}
			else
			{
				TPcar.Text = this.Task.Car.ToString();
				foreach (IGrouping<int, OptimSeries> Pyr in T.Car.OptimSeries.GroupBy(s => s.PyramidNum).OrderBy(g => g.Key))
				{
					Label L = new Label();
					SCcar.Panel1.Controls.Add(L);
					L.AutoSize = true;
					L.Dock = DockStyle.Top;
					L.Text = "            Пирамида " + Pyr.Key;
					L.TextAlign = ContentAlignment.MiddleLeft;
					L.Font = new Font(TPcar.Font.FontFamily, 10, FontStyle.Bold);
					L.ImageList = imageList;
					L.ImageIndex = 3;
					L.ImageAlign = ContentAlignment.MiddleLeft;
					L.BringToFront();

					DataGridView DGV = new DataGridView();
					SetProprties(DGV);
					SCcar.Panel1.Controls.Add(DGV);
					DGV.Dock = DockStyle.Top;
					DGV.DataSource = Pyr.OrderByDescending(s => s.RowNum).ToArray();
					DGV.CurrentCellChanged += delegate (object sender, EventArgs e)
					{
						DataGridView sDGV = (DataGridView)sender;
						DGVitem.DataSource = (sDGV.CurrentRow != null) ? ((OptimSeries[])((DataGridView)sender).DataSource)[sDGV.CurrentRow.Index].OptimItem.OrderBy(i => i.CellNum).ToArray() : null;
					};
					int H = DGV.ColumnHeadersHeight;
					foreach (DataGridViewRow drwr in DGV.Rows)
						H += drwr.Height;
					DGV.Height = H;
                    DGV.BringToFront();
				}
            }
		}
		void SetProprties(DataGridView DGV)
		{
			DGV.AllowUserToAddRows = false;
			DGV.AllowUserToDeleteRows = false;
			DGV.AllowUserToOrderColumns = true;
			DGV.AllowUserToResizeRows = false;
			DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DGV.MultiSelect = false;
			DGV.ReadOnly = true;
			DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			DGV.AllowUserToResizeRows = false;
		}
		private void DGVord_CurrentCellChanged(object sender, EventArgs e)
		{
			DataGridView sDGV = (DataGridView)sender;
			DGVit.DataSource = (sDGV.CurrentRow != null) ? Task.Orders[sDGV.CurrentRow.Index].Items : null;
		}
	}
}
