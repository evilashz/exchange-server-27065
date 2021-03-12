using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.ExSetupUI;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000011 RID: 17
	internal class SetupCompletedPage : SetupWizardPage
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x0000925C File Offset: 0x0000745C
		public SetupCompletedPage(RootDataHandler rootDataHandler)
		{
			this.modeDataHandler = rootDataHandler.ModeDatahandler;
			this.isCASInstalled = (this.modeDataHandler.IsCafeChecked && this.modeDataHandler.IsFrontendTransportChecked);
			this.InitializeComponent();
			base.PageTitle = Strings.SetupCompletedPageTitle;
			string text = Strings.SetupCompletedPageText;
			if (rootDataHandler.Mode == InstallationModes.Install && rootDataHandler.IsCleanMachine)
			{
				text += Strings.SetupCompletedPageLinkText;
			}
			this.setupCompletedTextBox.Text = text;
			this.openExchangeAdminCenterCheckBox.Text = Strings.OpenExchangeAdminCenterCheckBoxText;
			this.openExchangeAdminCenterCheckBox.Checked = false;
			this.setupCompletedTextBox.LinkClicked += this.SetupCompletedTextBox_LinkClicked;
			this.openExchangeAdminCenterCheckBox.CheckedChanged += delegate(object param0, EventArgs param1)
			{
				this.OnIsOpenExchangeAdminCenterCheckedChanged(EventArgs.Empty);
			};
			this.IsOpenExchangeAdminCenterCheckedChanged += this.OpenExchangeAdminCenter_Checked;
			base.WizardCancel += this.SetupCompletedPage_WizardCancel;
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060000B7 RID: 183 RVA: 0x00009368 File Offset: 0x00007568
		// (remove) Token: 0x060000B8 RID: 184 RVA: 0x000093A0 File Offset: 0x000075A0
		internal event EventHandler IsOpenExchangeAdminCenterCheckedChanged;

		// Token: 0x060000B9 RID: 185 RVA: 0x000093D5 File Offset: 0x000075D5
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000093F4 File Offset: 0x000075F4
		private void SetupCompletedPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			base.SetWizardButtons(WizardButtons.Next);
			base.SetVisibleWizardButtons(WizardButtons.Next);
			base.SetBtnNextText(Strings.btnFinish);
			SetupFormBase.LaunchECPUrl = null;
			this.openExchangeAdminCenterCheckBox.Visible = ((this.modeDataHandler.IsCafeChecked && this.modeDataHandler.IsFrontendTransportChecked) || this.isCASInstalled);
			if (this.openExchangeAdminCenterCheckBox.Visible)
			{
				RichTextBox richTextBox = this.setupCompletedTextBox;
				richTextBox.Text += Strings.SetupCompletedPageEACText;
				this.openExchangeAdminCenterCheckBox.Checked = false;
			}
			this.ChangeRichTextBoxHeight();
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000094A9 File Offset: 0x000076A9
		private void OnIsOpenExchangeAdminCenterCheckedChanged(EventArgs e)
		{
			if (this.IsOpenExchangeAdminCenterCheckedChanged != null)
			{
				this.IsOpenExchangeAdminCenterCheckedChanged(this, e);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000094C0 File Offset: 0x000076C0
		private void ChangeRichTextBoxHeight()
		{
			Graphics graphics = this.setupCompletedTextBox.CreateGraphics();
			SizeF sizeF = graphics.MeasureString(this.setupCompletedTextBox.Text, this.setupCompletedTextBox.Font, this.setupCompletedTextBox.ClientSize.Width);
			this.setupCompletedTextBox.Height = Convert.ToInt32(sizeF.Height + 1f);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00009526 File Offset: 0x00007726
		private void SetupCompletedTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			SetupFormBase.ShowHelpFromUrl(e.LinkText);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00009533 File Offset: 0x00007733
		private void SetupCompletedPage_WizardCancel(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000953C File Offset: 0x0000773C
		private void SetupCompletedPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.setupCompletedTextBox.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00009584 File Offset: 0x00007784
		private void OpenExchangeAdminCenter_Checked(object sender, EventArgs e)
		{
			if (this.openExchangeAdminCenterCheckBox.Checked)
			{
				string machineName = Environment.MachineName;
				SetupFormBase.LaunchECPUrl = string.Format("https://{0}/ecp/?ExchClientVer=15", machineName);
				return;
			}
			SetupFormBase.LaunchECPUrl = null;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000095BC File Offset: 0x000077BC
		private void InitializeComponent()
		{
			this.spacerPanel = new Panel();
			this.openExchangeAdminCenterCheckBox = new CustomCheckbox();
			this.headerPanel = new Panel();
			this.bodyPanel = new Panel();
			this.setupCompletedTextBox = new RichTextBox();
			this.headerPanel.SuspendLayout();
			this.bodyPanel.SuspendLayout();
			base.SuspendLayout();
			this.spacerPanel.Dock = DockStyle.Top;
			this.spacerPanel.Location = new Point(0, 17);
			this.spacerPanel.Name = "spacerPanel";
			this.spacerPanel.Size = new Size(721, 35);
			this.spacerPanel.TabIndex = 29;
			this.openExchangeAdminCenterCheckBox.BackColor = Color.Transparent;
			this.openExchangeAdminCenterCheckBox.Checked = false;
			this.openExchangeAdminCenterCheckBox.DisabledColor = Color.FromArgb(221, 221, 221);
			this.openExchangeAdminCenterCheckBox.ForeColor = Color.FromArgb(152, 163, 166);
			this.openExchangeAdminCenterCheckBox.HighlightedColor = Color.FromArgb(125, 125, 125);
			this.openExchangeAdminCenterCheckBox.Highligted = false;
			this.openExchangeAdminCenterCheckBox.Location = new Point(0, 0);
			this.openExchangeAdminCenterCheckBox.Margin = new Padding(4, 4, 4, 4);
			this.openExchangeAdminCenterCheckBox.Name = "openExchangeAdminCenterCheckBox";
			this.openExchangeAdminCenterCheckBox.NormalColor = Color.FromArgb(152, 163, 166);
			this.openExchangeAdminCenterCheckBox.Size = new Size(721, 19);
			this.openExchangeAdminCenterCheckBox.TabIndex = 28;
			this.openExchangeAdminCenterCheckBox.Text = "[OpenExchangeAdminCenterCheckBox]";
			this.openExchangeAdminCenterCheckBox.TextGap = 10;
			this.headerPanel.AutoSize = true;
			this.headerPanel.Controls.Add(this.setupCompletedTextBox);
			this.headerPanel.Dock = DockStyle.Top;
			this.headerPanel.Location = new Point(0, 0);
			this.headerPanel.Name = "headerPanel";
			this.headerPanel.Size = new Size(721, 17);
			this.headerPanel.TabIndex = 27;
			this.bodyPanel.Controls.Add(this.openExchangeAdminCenterCheckBox);
			this.bodyPanel.Dock = DockStyle.Fill;
			this.bodyPanel.Location = new Point(0, 52);
			this.bodyPanel.Name = "bodyPanel";
			this.bodyPanel.Size = new Size(721, 383);
			this.bodyPanel.TabIndex = 30;
			this.setupCompletedTextBox.BackColor = SystemColors.Window;
			this.setupCompletedTextBox.BorderStyle = BorderStyle.None;
			this.setupCompletedTextBox.ForeColor = Color.FromArgb(51, 51, 51);
			this.setupCompletedTextBox.Location = new Point(0, 0);
			this.setupCompletedTextBox.Name = "setupCompletedTextBox";
			this.setupCompletedTextBox.ReadOnly = true;
			this.setupCompletedTextBox.ScrollBars = RichTextBoxScrollBars.None;
			this.setupCompletedTextBox.Size = new Size(720, 20);
			this.setupCompletedTextBox.TabIndex = 29;
			this.setupCompletedTextBox.Text = "[SetupCompletedPageText]";
			base.Controls.Add(this.bodyPanel);
			base.Controls.Add(this.spacerPanel);
			base.Controls.Add(this.headerPanel);
			base.Name = "SetupCompletedPage";
			base.SetActive += this.SetupCompletedPage_SetActive;
			base.CheckLoaded += this.SetupCompletedPage_CheckLoaded;
			this.headerPanel.ResumeLayout(false);
			this.headerPanel.PerformLayout();
			this.bodyPanel.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400007D RID: 125
		private const string EcpPage = "https://{0}/ecp/?ExchClientVer=15";

		// Token: 0x0400007E RID: 126
		private readonly ModeDataHandler modeDataHandler;

		// Token: 0x0400007F RID: 127
		private readonly bool isCASInstalled;

		// Token: 0x04000080 RID: 128
		private IContainer components;

		// Token: 0x04000081 RID: 129
		private Panel spacerPanel;

		// Token: 0x04000082 RID: 130
		private CustomCheckbox openExchangeAdminCenterCheckBox;

		// Token: 0x04000083 RID: 131
		private Panel headerPanel;

		// Token: 0x04000084 RID: 132
		private Panel bodyPanel;

		// Token: 0x04000085 RID: 133
		private RichTextBox setupCompletedTextBox;
	}
}
