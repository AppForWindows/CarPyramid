using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Classes;
using CarPyramid.LineOpt;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CarPyramid
{
	public partial class MainForm : Form
	{
		Task[] TaskArray = new Task[] { };
		public MainForm()
		{
			InitializeComponent();

			this.Icon = Properties.Resources.truck;
			OFD.DefaultExt = XML.TASK_EXTENSION;
            OFD.Filter += "*" + XML.TASK_EXTENSION + ";";
			labelInform.Text = "";

			SetLic();
		}
		void SetLic()
		{
			Spacer.Text = Program.CompanyName;
			if (Program.GetProductKey(Program.CompanyName) == Program.ProductKey)
			{
				Actvation.Text = string.Empty;
				Spacer.ImageIndex = 1;
			}
			else
			{
				Actvation.Text = "&Активация";
				Spacer.ImageIndex = -1;
			}
		}
		private void LoadFile_Click(object sender, EventArgs e)
		{
			if (OFD.ShowDialog() == DialogResult.OK)
			{
				List<Task> TempList = new List<Task>();
				foreach (string FileName in OFD.FileNames)
					TempList.AddRange(XML.LoadTasksFile(FileName));
				TaskArray = TempList.ToArray();
            }
            GridView_Task.DataSource = TaskArray;
        }

		private void GridView_Task_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;
			using (TaskForm F = new TaskForm(TaskArray[e.RowIndex]))
				F.ShowDialog(this);
		}
		private void GridView_Task_CurrentCellChanged(object sender, EventArgs e)
		{
			DataGridView sDGV = (DataGridView)sender;
			CarOpt.Enabled = (sDGV.CurrentRow != null);
			CarOptAll.Enabled = (sDGV.CurrentRow != null);
			AllLineOpt.Enabled = (sDGV.CurrentRow != null);
			CarOptAllSave.Enabled = (sDGV.CurrentRow != null);
			SaveFile.Enabled = (sDGV.CurrentRow != null);
		}
		private void GridView_Task_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			try
			{
				if (e.Value is CarOpt.Car && e.Value != null)
				{
					CarOpt.Car Car = (CarOpt.Car)e.Value;
					if (Car.OptimSeries.Length == 0)
						e.CellStyle.BackColor = Color.LightGreen;
					else if (TaskArray[e.RowIndex].Items.Count != Car.OptimItem.Count())
						e.CellStyle.BackColor = Color.Red;
					else if (Car.PyramidCount<Car.PyramidCountFact)
						e.CellStyle.BackColor = Color.LightPink;
				}
			} catch { }
		}

		private void AllLineOpt_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				NotifyIcon.Icon = Properties.Resources.truckColor;
				labelInform.Text = "LineOpt for All Items";
				List<Item> OptimList = new List<Item>();
				foreach (Task t in TaskArray)
					foreach (Order o in t.Orders)
						OptimList.AddRange(o.Items);
				OptimLine Opt = new OptimLine(OptimList, 6000, new List<OptimSource> { new OptimSource(2500), new OptimSource(1400), new OptimSource(3000) });
				OptimResult OR = Opt.Optimize();
				GridView_Task.DataSource = OR.OptimSeries.ToArray();
				labelInform.Text = "Generation: " + Opt.Generation.ToString() + " " + OR.ToString();
			}
			finally
			{
				this.Cursor = Cursors.Default;
				NotifyIcon.Icon = Properties.Resources.truck;
				NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
				NotifyIcon.BalloonTipTitle = "Линейная Оптимизация";
				NotifyIcon.BalloonTipText = "Линейная Оптимизация Завершена";
				NotifyIcon.ShowBalloonTip(1500);
            }
		}
		private void CarOpt_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				NotifyIcon.Icon = Properties.Resources.truckColor;
				foreach (DataGridViewRow dgvr in GridView_Task.SelectedRows)
				{
					Task t = TaskArray[GridView_Task.Rows.IndexOf(dgvr)];
					labelInform.Text = "CAR for " + t.NameTask;
					t.Status = "Optimize...";
					TaskArray[GridView_Task.Rows.IndexOf(dgvr)] = t;
					GridView_Task.Enabled = false;
					GridView_Task.Enabled = true;
					t.Optimize();
					//dgvr.Cells["Car"].Value = t.Car;
					TaskArray[GridView_Task.Rows.IndexOf(dgvr)] = t;
					GridView_Task.DataSource = TaskArray;
					GridView_Task.Enabled = false;
					GridView_Task.Enabled = true;
					labelInform.Text = t.Car.ToString();

					NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
					NotifyIcon.BalloonTipTitle = "Подбор Машины";
					NotifyIcon.BalloonTipText = t.NameTask + " - " + t.Car.ToString();
					NotifyIcon.ShowBalloonTip(1500);
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
				NotifyIcon.Icon = Properties.Resources.truck;
			}
		}
		List<BackgroundWorker> Works = new List<BackgroundWorker>();
		private void CarOptAll_Click(object sender, EventArgs e)
		{
			bool AutoSave = sender is Button && (Button)sender == CarOptAllSave;
            if (Works.Any(bgw => bgw.IsBusy))
			{
				if (MessageBox.Show("Некоторые оптимизации не завершены.\nПробуем остановить процессы?", "Запущенные оптимизации", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
					foreach (BackgroundWorker BGW in Works.Where(bgw => bgw.IsBusy))
						BGW.CancelAsync();
				else
					return;
			}
			this.Cursor = Cursors.WaitCursor;
			NotifyIcon.Icon = Properties.Resources.truckColor;
			Works.Clear();
			foreach (Task t in TaskArray)
			{
				BackgroundWorker BGW = new BackgroundWorker();
				BGW.WorkerReportsProgress = true;
                Works.Add(BGW);
				BGW.DoWork += delegate(object bgwo, DoWorkEventArgs dwea)
				{
					TaskArray[Array.IndexOf(TaskArray, t)].Status = "Optimize...";
					((BackgroundWorker)bgwo).ReportProgress(1);
                    t.Optimize();
					((BackgroundWorker)bgwo).ReportProgress(100);
					dwea.Result = t;
				};
				BGW.ProgressChanged += delegate (object bgwo, ProgressChangedEventArgs pcea)
				{
					GridView_Task.Enabled = false;
					GridView_Task.Enabled = true;
				};
                BGW.RunWorkerCompleted += delegate(object bgwo, RunWorkerCompletedEventArgs rwcea)
				{
					if (!rwcea.Cancelled && rwcea.Error == null && rwcea.Result != null && rwcea.Result is Task)
					{
						Task task = (Task)rwcea.Result;
						for (int i = 0; i < TaskArray.Length; i++)
							if (TaskArray[i].Guid == task.Guid)
							{
								//DataGridViewRow dgvr = GridView_Task.Rows[i];
								//dgvr.Cells["Car"].Value = task.Car;
								TaskArray[i] = task;
							}
						GridView_Task.DataSource = TaskArray;
						GridView_Task.Enabled = false;
						GridView_Task.Enabled = true;

						NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
						NotifyIcon.BalloonTipTitle = "Подбор Машины";
						NotifyIcon.BalloonTipText = task.NameTask + " - " + task.Car.ToString();
						NotifyIcon.ShowBalloonTip(1500);
					}
                    if (Works.Any(bgw => bgw.IsBusy))
						return;
					this.Cursor = Cursors.Default;
					labelInform.Text = "ВСЕ Задания ВЫПОЛНЕНЫ";
					NotifyIcon.Icon = Properties.Resources.truck;

					NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
					NotifyIcon.BalloonTipTitle = "Подбор Машин";
					NotifyIcon.BalloonTipText = "ВСЕ Задания ВЫПОЛНЕНЫ";
					NotifyIcon.ShowBalloonTip(1500);

					if (AutoSave)
						SaveFile_Click(SaveFile, new EventArgs());
                };
				BGW.RunWorkerAsync();
            }
		}
		private void SaveFile_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				for (int t = 0; t < TaskArray.Length; t++)
					TaskArray[t].Status = "Saving...";
				GridView_Task.Enabled = false;
				GridView_Task.Enabled = true;
				if (TaskArray.Length > 0)
					foreach (IGrouping<string, Task> F in TaskArray.GroupBy(t => t.FilePath))
						XML.SaveTasks(F, F.Key);
				NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
				NotifyIcon.BalloonTipTitle = "Сохранение";
				NotifyIcon.BalloonTipText = "ВСЕ Задания СОХРАНЕНЫ";
				this.Cursor = Cursors.WaitCursor;
				for (int t = 0; t < TaskArray.Length; t++)
					TaskArray[t].Status = "Saved";
				GridView_Task.Enabled = false;
				GridView_Task.Enabled = true;
			}
			catch (Exception es)
			{
				NotifyIcon.Icon = Properties.Resources.truckColor;
				NotifyIcon.Text = Application.ProductName + Environment.NewLine + ": Ошибка Сохранения Результатов";
				NotifyIcon.BalloonTipIcon = ToolTipIcon.Error;
				NotifyIcon.BalloonTipTitle = "Ошибка Сохранения Результатов";
                NotifyIcon.BalloonTipText = es.Message;
			}
			finally
			{
				this.Cursor = Cursors.Default;
				NotifyIcon.ShowBalloonTip((NotifyIcon.BalloonTipIcon == ToolTipIcon.Error) ? 5000 : 1500);
			}
			GridView_Task.DataSource = TaskArray;
			GridView_Task.Enabled = false;
			GridView_Task.Enabled = true;
		}

		private void Spacer_Click(object sender, EventArgs e)
		{
			using (AboutLic AL = new AboutLic())
				AL.ShowDialog();
		}
		private void Actvation_Click(object sender, EventArgs e)
		{
			using (ActvationForm AF = new ActvationForm())
				AF.ShowDialog();
			SetLic();
        }
	}
}
