using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace CarPyramid
{
	public class Integrator
	{
		public Integrator()
		{ }
		public static string DoCarPyramidWork(string XMLtask)
		{
			string XMLres = string.Empty;
            NotifyIcon NotifyIcon = new NotifyIcon();
			string Dt = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
			try
			{

				NotifyIcon.Icon = Properties.Resources.truck;
				NotifyIcon.Text = Application.ProductName + " Integrated";
				NotifyIcon.Visible = true;

				List<Classes.Task> TaskList = Classes.XML.LoadTasksOuterXML(XMLtask, "Integrated");
				if (TaskList.Count == 0)
				{
					NotifyIcon.Icon = Properties.Resources.truckColor;
					NotifyIcon.BalloonTipIcon = ToolTipIcon.Warning;
					NotifyIcon.BalloonTipTitle = "Файл: "+ Dt;
					NotifyIcon.BalloonTipText = "Задания не загружены.";
					NotifyIcon.ShowBalloonTip(5000);
					return string.Empty;
				}

				NotifyIcon.Icon = Properties.Resources.task;
				NotifyIcon.Text = Application.ProductName + " Integrated" + Environment.NewLine + "Выполнение Заданий";
				List<Task> Works = new List<Task>();
				List<Classes.Task> Results = new List<Classes.Task>();
                foreach (Classes.Task t in TaskList)
					Works.Add(Task.Factory.StartNew(delegate
					{ t.Optimize(); Results.Add(t); }));
				Task.WaitAll(Works.ToArray());
				XMLres = Classes.XML.GetResultXML(Results, "Integrated").OuterXml;

				NotifyIcon.Icon = Properties.Resources.result;
				NotifyIcon.Text = Application.ProductName + " Integrated" + Environment.NewLine + ": Задания Выполнены";
				NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
				NotifyIcon.BalloonTipTitle = "Файл: " + Dt;
				NotifyIcon.BalloonTipText = "Результаты успешно сохранены";
			}
			catch (Exception es)
			{
				NotifyIcon.Icon = Properties.Resources.truckColor;
				NotifyIcon.Text = Application.ProductName + Environment.NewLine + ": Ошибка Выполнения Заданий";
				NotifyIcon.BalloonTipIcon = ToolTipIcon.Error;
				NotifyIcon.BalloonTipTitle = "Файл: " + Dt;
				NotifyIcon.BalloonTipText = es.Message;
			}
			finally
			{
				NotifyIcon.ShowBalloonTip((NotifyIcon.BalloonTipIcon == ToolTipIcon.Error) ? 5000 : 1000);
			}
			NotifyIcon.Visible = false;
			return XMLres;
        }
    }
}
