using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarPyramid
{
	public partial class ActvationForm : Form
	{
		public ActvationForm()
		{
			InitializeComponent();

			this.Icon = Properties.Resources.truck;
			SetControls();
		}
		void SetControls()
		{
			txtCompanyName.Text = Program.CompanyName;
			txtHarwareId.Text = Program.HardwareId;
			txtProducKey.Text = Program.ProductKey;

			txtCompanyName.ReadOnly = !string.IsNullOrEmpty(Program.CompanyName) || !string.IsNullOrEmpty(Program.ProductKey);
			txtProducKey.ReadOnly = !string.IsNullOrEmpty(Program.ProductKey);

			btnEmail.Enabled = string.IsNullOrEmpty(Program.ProductKey) && !string.IsNullOrEmpty(txtCompanyName.Text);
			btnEmail.Text = (txtCompanyName.ReadOnly) ? "&Удалить Активацию" : "&Запросить Ключ Продукта";
			btnActivation.Text = (txtProducKey.ReadOnly) ? "&Удалить Ключ Продукта" : "&Активировать";
		}
		private void txtCompanyName_TextChanged(object sender, EventArgs e)
		{
			btnEmail.Enabled = string.IsNullOrEmpty(Program.ProductKey) && !string.IsNullOrEmpty(txtCompanyName.Text);
		}
		private void txtCompanyName_Leave(object sender, EventArgs e)
		{
			btnEmail.Enabled = string.IsNullOrEmpty(Program.ProductKey) && !string.IsNullOrEmpty(txtCompanyName.Text);
		}
		private void txtCompanyName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				btnEmail.Enabled = string.IsNullOrEmpty(Program.ProductKey) && !string.IsNullOrEmpty(txtCompanyName.Text);
				btnEmail.Focus();
            }
		}
		private void btnEmail_Enter(object sender, EventArgs e)
		{
			btnEmail.Enabled = string.IsNullOrEmpty(Program.ProductKey) && !string.IsNullOrEmpty(txtCompanyName.Text);
		}
		private void txt_Click(object sender, EventArgs e)
		{
			if (sender is TextBox)
				((TextBox)sender).SelectAll();
			else if (sender is MaskedTextBox)
				((MaskedTextBox)sender).SelectAll();
		}
		private void btnEmail_Click(object sender, EventArgs e)
		{
			if (txtCompanyName.ReadOnly)
			{
				Program.CompanyName = string.Empty;
				Program.ProductKey = string.Empty;
			}
			else if (string.IsNullOrEmpty(txtCompanyName.Text))
				btnEmail.Enabled = false;
			else if (((Button)sender).Enabled)
			{
				Program.CompanyName = txtCompanyName.Text;
				Program.ProductKey = string.Empty;
				string Subject = "CarPyramid Activation Request";
				string Body = "Прошу сообщить Ключ Продукта" + Environment.NewLine + Environment.NewLine;
				Body += "CompanyName: " + txtCompanyName.Text + Environment.NewLine;
				Body += "HardwareId: " + Program.HardwareId + Environment.NewLine + Environment.NewLine;
				try
				{
					Body += "OS: " + Environment.OSVersion + " [U: " + Environment.UserName + "] " + Environment.NewLine;
				}
				catch { }
				try
				{
					Body += "APP: " + Application.ProductName + " ";
					if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed && System.Deployment.Application.ApplicationDeployment.CurrentDeployment != null)
						Body += "Deploy: " + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() + Environment.NewLine;
					else
						Body += "Local: " + Application.ProductVersion + Environment.NewLine;
				}
				catch { }
				Body += DateTime.Now.ToString("d MMMM yyг. HH:mm");
				string Temp = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), ".cpreq");
				System.IO.File.WriteAllText(Temp, Body);

				System.Diagnostics.Process.Start(Uri.EscapeUriString(string.Format(@"mailto:{0}?Subject={1}&Body={2}&attach={3}&attach=""{3}""&attachment={3}&attachment=""{3}""", "valery_us@mail.ru", Subject, Body, Temp)));
            }
			SetControls();
		}
		private void txtProducKey_TextChanged(object sender, EventArgs e)
		{
			btnActivation.Enabled = !string.IsNullOrWhiteSpace(txtCompanyName.Text) && System.Text.RegularExpressions.Regex.IsMatch(txtProducKey.Text, "^[A-Z0-9]{8}(-[A-Z0-9]{8}){2}(-[A-Z0-9]{8})$");
		}
		private void btnActivation_Click(object sender, EventArgs e)
		{
			if (Program.GetProductKey(Program.CompanyName) == txtProducKey.Text)
			{
				if (txtProducKey.ReadOnly == false)
				{
					Program.ProductKey = txtProducKey.Text;
					txtProducKey.ReadOnly = true;
					btnActivation.ImageIndex = 1;
					btnActivation.Enabled = false;
				}
				else
				{
					Program.ProductKey = string.Empty;
					txtProducKey.Text = string.Empty;
                    txtProducKey.ReadOnly = false;
					btnActivation.ImageIndex = -1;
					btnEmail.Enabled = true;
					SetControls();
				}
			}
			else
			{
				btnActivation.ImageIndex = 0;
				txtProducKey.Focus();
				txtProducKey.SelectAll();
			}
        }
	}
}
