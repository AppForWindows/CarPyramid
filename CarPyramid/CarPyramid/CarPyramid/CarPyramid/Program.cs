using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace CarPyramid
{
	public static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			AppDomain.CurrentDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs ueea)
			{
				MessageBox.Show(ueea.ExceptionObject.ToString(), "UnhandledException");
			};
			Application.ThreadException += delegate (Object sender, ThreadExceptionEventArgs teea)
			{
				MessageBox.Show(teea.Exception.Message, "ThreadException");
			};

			//args = new string[] { "D:\\РАБОТА\\CarPyramid.cptsk" };
			if (args != null && args.Length > 0 && System.IO.File.Exists(args[0]) && System.IO.Path.GetExtension(args[0]) == Classes.XML.TASK_EXTENSION)
			{
				string FileName = args[0];

				NotifyIcon NotifyIcon = new NotifyIcon();
				NotifyIcon.Icon = Properties.Resources.truck;
				NotifyIcon.Text = Application.ProductName + Environment.NewLine + FileName;
				NotifyIcon.Visible = true;

				List<Classes.Task> TaskList = Classes.XML.LoadTasksFile(FileName);
				System.IO.File.Delete(FileName);
				if (TaskList.Count == 0)
				{
					NotifyIcon.Icon = Properties.Resources.truckColor;
					NotifyIcon.BalloonTipIcon = ToolTipIcon.Warning;
					NotifyIcon.BalloonTipTitle = "Файл: " + System.IO.Path.GetFileNameWithoutExtension(FileName);
					NotifyIcon.BalloonTipText = "Задания не загружены.";
					NotifyIcon.ShowBalloonTip(5000);
					return;
				}

				NotifyIcon.Icon = Properties.Resources.task;
				NotifyIcon.Text = Application.ProductName + Environment.NewLine + "Выполнение Заданий";
				List<Task> Works = new List<Task>();
				List<Classes.Task> Results = new List<Classes.Task>();
				foreach (Classes.Task t in TaskList)
					Works.Add(Task.Factory.StartNew(delegate
					{ t.Optimize(); Results.Add(t); }));
				Task.WaitAll(Works.ToArray());
				try
				{
					System.IO.File.Delete(FileName);
					Classes.XML.SaveTasks(Results, FileName);
					NotifyIcon.Icon = Properties.Resources.result;
					NotifyIcon.Text = Application.ProductName + Environment.NewLine + ": Задания Выполнены";
					NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
					NotifyIcon.BalloonTipTitle = "Файл: " + System.IO.Path.GetFileNameWithoutExtension(FileName);
					NotifyIcon.BalloonTipText = "Результаты успешно сохранены";
				}
				catch (Exception es)
				{
					NotifyIcon.Icon = Properties.Resources.truckColor;
					NotifyIcon.Text = Application.ProductName + Environment.NewLine + ": Ошибка Сохранения Результатов";
					NotifyIcon.BalloonTipIcon = ToolTipIcon.Error;
					NotifyIcon.BalloonTipTitle = "Файл: " + System.IO.Path.GetFileNameWithoutExtension(FileName);
					NotifyIcon.BalloonTipText = es.Message;
				}
				finally
				{
					NotifyIcon.ShowBalloonTip((NotifyIcon.BalloonTipIcon == ToolTipIcon.Error) ? 5000 : 2000);
				}
				NotifyIcon.Visible = false;
			}
			else
			{
				//if (Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + Classes.XML.TASK_EXTENSION, false) == null)
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Classes", true))
				{
					string IcoFile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Classes.XML.TASK_EXTENSION.Replace(".", "") + ".ico");
					try
					{
						if (System.IO.File.Exists(IcoFile))
							System.IO.File.Delete(IcoFile);
						using (System.IO.FileStream fs = new System.IO.FileStream(IcoFile, System.IO.FileMode.CreateNew))
							Properties.Resources.task.Save(fs);
					}
					catch { }
					RegistryKey skey = key.CreateSubKey(Classes.XML.TASK_EXTENSION);
					skey.SetValue(string.Empty, Application.ProductName);
					skey.CreateSubKey("DefaultIcon").SetValue(string.Empty, IcoFile);
					skey = skey.CreateSubKey("Shell");
					skey.SetValue(string.Empty, "Open");
					skey = skey.CreateSubKey("Open");
					skey.CreateSubKey("Command").SetValue(string.Empty, "\"" + Application.ExecutablePath + "\" \"%1\"");

					SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
				}
				//if (Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + Classes.XML.RESULT_EXTENSION, false) == null)
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Classes", true))
				{
					string IcoFile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Classes.XML.RESULT_EXTENSION.Replace(".", "") + ".ico");
					try
					{
						if (System.IO.File.Exists(IcoFile))
							System.IO.File.Delete(IcoFile);
						using (System.IO.FileStream fs = new System.IO.FileStream(IcoFile, System.IO.FileMode.CreateNew))
							Properties.Resources.result.Save(fs);
					}
					catch { }
					RegistryKey skey = key.CreateSubKey(Classes.XML.RESULT_EXTENSION);
					skey.SetValue(string.Empty, Application.ProductName);
					skey.CreateSubKey("DefaultIcon").SetValue(string.Empty, IcoFile);

					SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
				}

				Application.Run(new MainForm());
			}
		}

		[DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
		public static bool DetectClockManipulation(DateTime holdTime)
		{
			DateTime adjustedTime = new DateTime(holdTime.Year, holdTime.Month, holdTime.Day, 23, 59, 59);
			foreach (System.Diagnostics.EventLogEntry entry in new System.Diagnostics.EventLog("system").Entries)
				if (entry.TimeWritten > adjustedTime)
					return true;
			return false;
		}
		public static string CompanyName
		{
			get
			{
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\" + Application.ProductName, false))
					return (key != null) ? key.GetValue("CompanyName", string.Empty).ToString() : string.Empty;
			}
			set
			{
				if (Registry.CurrentUser.OpenSubKey("Software\\" + Application.ProductName, false) == null)
					using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true))
						key.CreateSubKey(Application.ProductName);
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\" + Application.ProductName, true))
					key.SetValue("CompanyName", value);
			}
		}
		public static string HardwareId
		{
			get
			{
				string HDD = "";
				foreach (System.Management.ManagementObject OS in new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get())
					foreach (System.Management.ManagementObject hdd in new System.Management.ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE Index=" + System.Text.RegularExpressions.Regex.Matches(OS["Name"].ToString().Substring(OS["Name"].ToString().IndexOf("Harddisk")), @"[0-9]{1,}")[0].Value).Get())
						foreach (System.Management.ManagementObject pm in hdd.GetRelated("Win32_PhysicalMedia"))
							HDD = pm["SerialNumber"].ToString();
				System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
				using (System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
				{
					byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(HDD));
					for (int i = 0; i < data.Length; i++)
						sBuilder.Append(data[i].ToString("x2"));
				}
				return System.Text.RegularExpressions.Regex.Matches(sBuilder.ToString(), @".{1,8}").Cast<System.Text.RegularExpressions.Match>().Select(m => m.Value).Aggregate((prev, next) => prev + "-" + next).ToUpper();
            }
		}
		public static string ProductKey
		{
			get
			{
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\" + Application.ProductName, false))
					return (key != null) ? key.GetValue(string.Empty, string.Empty).ToString() : string.Empty;
			}
			set
			{
				if (Registry.CurrentUser.OpenSubKey("Software\\" + Application.ProductName, false) == null)
					using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true))
						key.CreateSubKey(Application.ProductName);
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\" + Application.ProductName, true))
					key.SetValue(string.Empty, value);
			}
		}
		public static string GetProductKey(string CompanyName)
		{
			CompanyName = CompanyName.Trim();
			string HDD = HardwareId.Replace("-", "");
			string key = string.Empty;
			for (int s = 0; s < Math.Max(CompanyName.Length, HDD.Length); s++)
			{
				if (s < HDD.Length)
					key += HDD[s];
				if (s < CompanyName.Length)
					key += CompanyName[s];
			}
			System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
			using (System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
			{
				byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(key));
				for (int i = 0; i < data.Length; i++)
					sBuilder.Append(data[i].ToString("x2"));
			}
			return System.Text.RegularExpressions.Regex.Matches(sBuilder.ToString(), @".{1,8}").Cast<System.Text.RegularExpressions.Match>().Select(m => m.Value).Aggregate((prev, next) => prev + "-" + next).ToUpper();
        }
    }
}
