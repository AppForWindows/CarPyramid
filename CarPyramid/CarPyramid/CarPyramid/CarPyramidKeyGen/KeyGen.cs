using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarPyramidKeyGen
{
	public partial class KeyGen : Form
	{
		public KeyGen()
		{
			InitializeComponent();

			txtHardwareId.ValidatingType = typeof(string);
        }

		private void LoadFile_Click(object sender, EventArgs e)
		{
			if (OFD.ShowDialog() == DialogResult.OK)
			{
				List<Task> TempList = new List<Task>();
				foreach (string FileName in OFD.FileNames)
				{
					if (!System.IO.File.Exists(FileName))
						continue;
					try
					{
						System.Xml.XmlDocument File = new System.Xml.XmlDocument();
						File.Load(FileName);
						TaskText.Text = System.Xml.Linq.XDocument.Parse(File.OuterXml).ToString();
                    }
					catch (Exception el)
					{
						TaskText.Text = el.Source + Environment.NewLine + el.Message + Environment.NewLine + el.StackTrace;
					}
					finally
					{
					}
				}

			}
		}
		private void DoWork1_Click(object sender, EventArgs e)
		{
			string FileName = System.IO.Path.Combine(Application.StartupPath, "CarPyramid.exe");    //   C:\\CarPyramid.exe
			using (System.IO.FileStream FS = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
			{
				byte[] dll = new byte[FS.Length];
				FS.Read(dll, 0, (int)FS.Length);
				FS.Close();
				System.Reflection.Assembly ass = System.Reflection.Assembly.Load(dll);
				object assclass = ass.CreateInstance("CarPyramid.Integrator");
				System.Reflection.MethodInfo mi = assclass.GetType().GetMethod("DoCarPyramidWork");
				ResultText.Text = System.Xml.Linq.XDocument.Parse((string)mi.Invoke(assclass, new object[] { TaskText.Text })).ToString();
			}
		}
		private void DoWork2_Click(object sender, EventArgs e)
		{
			ResultText.Text = System.Xml.Linq.XDocument.Parse(CarPyramid.Integrator.DoCarPyramidWork(TaskText.Text)).ToString();
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			if (SFD.ShowDialog() == DialogResult.OK)
			{
				System.Xml.XmlDocument Doc = new System.Xml.XmlDocument();
				Doc.LoadXml(ResultText.Text);
				if (System.IO.File.Exists(SFD.FileName))
					System.IO.File.Delete(SFD.FileName);
				Doc.Save(SFD.FileName);
			}
		}

		private void txt_TextChanged(object sender, EventArgs e)
		{
			Generate.Enabled = !string.IsNullOrWhiteSpace(txtCompanyName.Text) && System.Text.RegularExpressions.Regex.IsMatch(txtHardwareId.Text, "^[A-Z0-9]{8}(-[A-Z0-9]{8}){2}(-[A-Z0-9]{8})$");
			txtProductKey.Enabled = Generate.Enabled;
			if (!txtProductKey.Enabled)
				txtProductKey.Text = string.Empty;
        }
		private void txtHardwareId_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				txt_TextChanged(txtCompanyName, new EventArgs());
				Generate.Focus();
			}
		}
		private void Generate_Click(object sender, EventArgs e)
		{
			string CompanyName = txtCompanyName.Text.Trim();
			string HDD = txtHardwareId.Text.Replace("-", "");
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
			txtProductKey.Text = System.Text.RegularExpressions.Regex.Matches(sBuilder.ToString(), @".{1,8}").Cast<System.Text.RegularExpressions.Match>().Select(m => m.Value).Aggregate((prev, next) => prev + "-" + next).ToUpper();

			txtBody.Text = string.Format(@"
Для Организации:   {0}
и Кода Активации:  {1}
создан
Ключ Продукта:     {2}

Для активации программы введите
    Организация: {0}
    Ключ Продукта: {2}
Ключ Продукта {2} на форме активации.

Для Интеграции в сторонние приложения добавляйте в создаваемые файлы заданий Корневой Элемент

<LoadDataForCarPyramidApplication CompanyName=""{0}"" ProductKey=""{2}"">
  <Task ...
  </Task >
</LoadDataForCarPyramidApplication>

Указанный Ключ Продукта действителен только на ЭВМ с указанным Кодом Активации.

С Уважением, Команда разработчиков.", txtCompanyName.Text, txtHardwareId.Text, txtProductKey.Text);

			txtProductKey.Focus();
			txtProductKey.SelectAll();
        }
		private void txt_Click(object sender, EventArgs e)
		{
			if (sender is TextBox)
				((TextBox)sender).SelectAll();
			else if (sender is MaskedTextBox)
				((MaskedTextBox)sender).SelectAll();
		}

		private void btnSendEmail_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Uri.EscapeUriString(string.Format(@"mailto:{0}?Subject={1}&Body={2}", txtAddress.Text, txtSubject.Text, txtBody.Text)));
			//System.Diagnostics.Process.Start(string.Format(@"mailto:{0}?Subject={1}&Body={2}&attach=D:\CP\CarPyramidShort.cpres", "valery_us@mail.ru", "Test", "body"));
			

		}
	}
}
