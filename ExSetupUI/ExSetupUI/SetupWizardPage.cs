using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000002 RID: 2
	[DefaultEvent("SetActive")]
	internal class SetupWizardPage : UserControl
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected SetupWizardPage()
		{
			this.InitializeComponent();
			this.SetLoaded += this.WizardPage_SetLoaded;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000002 RID: 2 RVA: 0x000020F0 File Offset: 0x000002F0
		// (remove) Token: 0x06000003 RID: 3 RVA: 0x00002128 File Offset: 0x00000328
		[Category("Wizard")]
		public event CancelEventHandler SetActive;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000004 RID: 4 RVA: 0x00002160 File Offset: 0x00000360
		// (remove) Token: 0x06000005 RID: 5 RVA: 0x00002198 File Offset: 0x00000398
		[Category("Wizard")]
		public event CancelEventHandler SetLoaded;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000006 RID: 6 RVA: 0x000021D0 File Offset: 0x000003D0
		// (remove) Token: 0x06000007 RID: 7 RVA: 0x00002208 File Offset: 0x00000408
		[Category("Wizard")]
		public event CancelEventHandler CheckLoaded;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000008 RID: 8 RVA: 0x00002240 File Offset: 0x00000440
		// (remove) Token: 0x06000009 RID: 9 RVA: 0x00002278 File Offset: 0x00000478
		[Category("Wizard")]
		public event WizardPageEventHandler WizardNext;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600000A RID: 10 RVA: 0x000022B0 File Offset: 0x000004B0
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x000022E8 File Offset: 0x000004E8
		[Category("Wizard")]
		public event WizardPageEventHandler WizardPrevious;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600000C RID: 12 RVA: 0x00002320 File Offset: 0x00000520
		// (remove) Token: 0x0600000D RID: 13 RVA: 0x00002358 File Offset: 0x00000558
		[Category("Wizard")]
		public event CancelEventHandler WizardFinish;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600000E RID: 14 RVA: 0x00002390 File Offset: 0x00000590
		// (remove) Token: 0x0600000F RID: 15 RVA: 0x000023C8 File Offset: 0x000005C8
		[Category("Wizard")]
		public event CancelEventHandler WizardCancel;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000010 RID: 16 RVA: 0x00002400 File Offset: 0x00000600
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x00002438 File Offset: 0x00000638
		[Category("Wizard")]
		public event CancelEventHandler WizardRetry;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000012 RID: 18 RVA: 0x00002470 File Offset: 0x00000670
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x000024A8 File Offset: 0x000006A8
		[Category("Wizard")]
		public event CancelEventHandler WizardFailed;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000024DD File Offset: 0x000006DD
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000024E5 File Offset: 0x000006E5
		public string Caption { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000024EE File Offset: 0x000006EE
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000024F6 File Offset: 0x000006F6
		public string PageTitle { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000024FF File Offset: 0x000006FF
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002507 File Offset: 0x00000707
		public bool PageVisible { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002510 File Offset: 0x00000710
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002518 File Offset: 0x00000718
		public string ParentPageName { get; set; }

		// Token: 0x0600001C RID: 28 RVA: 0x00002521 File Offset: 0x00000721
		public virtual void OnSetActive(CancelEventArgs e)
		{
			base.Focus();
			if (this.SetActive != null)
			{
				this.SetActive(this, e);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000253F File Offset: 0x0000073F
		public virtual void OnSetLoaded(CancelEventArgs e)
		{
			if (this.SetLoaded != null)
			{
				this.SetLoaded(this, e);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002556 File Offset: 0x00000756
		public virtual void OnCheckLoaded(CancelEventArgs e)
		{
			if (this.CheckLoaded != null)
			{
				this.CheckLoaded(this, e);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000256D File Offset: 0x0000076D
		public virtual void OnWizardNext(WizardPageEventArgs e)
		{
			if (this.WizardNext != null)
			{
				this.WizardNext(this, e);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002584 File Offset: 0x00000784
		public virtual void OnWizardPrevious(WizardPageEventArgs e)
		{
			if (this.WizardPrevious != null)
			{
				this.WizardPrevious(this, e);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000259B File Offset: 0x0000079B
		public virtual void OnWizardFinish(CancelEventArgs e)
		{
			if (this.WizardFinish != null)
			{
				this.WizardFinish(this, e);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025B2 File Offset: 0x000007B2
		public virtual void OnWizardCancel(CancelEventArgs e)
		{
			if (this.WizardCancel != null)
			{
				this.WizardCancel(this, e);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025C9 File Offset: 0x000007C9
		public virtual void OnWizardRetry(CancelEventArgs e)
		{
			if (this.WizardRetry != null)
			{
				this.WizardRetry(this, e);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025E0 File Offset: 0x000007E0
		public virtual void OnWizardFailed(CancelEventArgs e)
		{
			if (this.WizardFailed != null)
			{
				this.WizardFailed(this, e);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025F8 File Offset: 0x000007F8
		internal void SetCancelMessageBoxMessage(string text)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.SetCancelMessageBoxMessage(text);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002614 File Offset: 0x00000814
		internal void SetPageVisibleControl(string parentPageName, string pageName, bool visible)
		{
			SetupFormBase wizard = this.GetWizard();
			SetupWizardPage setupWizardPage = wizard.FindPage(pageName);
			setupWizardPage.PageVisible = visible;
			setupWizardPage.ParentPageName = parentPageName;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002640 File Offset: 0x00000840
		internal SetupWizardPage FindPage(string pageName)
		{
			SetupFormBase wizard = this.GetWizard();
			return wizard.FindPage(pageName);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000265C File Offset: 0x0000085C
		internal void InsertPage(SetupWizardPage pageToInsert, SetupWizardPage pageInsertBefore)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.InsertPage(pageToInsert, pageInsertBefore);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002678 File Offset: 0x00000878
		internal void RemovePage(SetupWizardPage pageToRemove)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.RemovePage(pageToRemove);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002694 File Offset: 0x00000894
		protected SetupFormBase GetWizard()
		{
			return (SetupFormBase)base.ParentForm;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026B0 File Offset: 0x000008B0
		protected void SetWizardButtons(WizardButtons buttons)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.SetWizardButtons(buttons);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026CC File Offset: 0x000008CC
		protected void SetBtnNextText(string btnNextText)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.SetBtnNextText(btnNextText);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026E8 File Offset: 0x000008E8
		protected void SetVisibleWizardButtons(WizardButtons buttons)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.SetVisibleWizardButtons(buttons);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002704 File Offset: 0x00000904
		protected void SetPageTitle(string title)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.SetTitle(title);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002720 File Offset: 0x00000920
		protected void SetPrintDocument(string fullFileName)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.DocumentToPrint = fullFileName;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000273C File Offset: 0x0000093C
		protected void EnableCancelButton(bool enableCancelButton)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.EnableCancelButton(enableCancelButton);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002757 File Offset: 0x00000957
		protected void DisableCustomCheckbox(CustomCheckbox checkbox, bool visible)
		{
			if (checkbox != null)
			{
				checkbox.Visible = visible;
				checkbox.Enabled = false;
				checkbox.Checked = false;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002774 File Offset: 0x00000974
		protected void SetRetryFlag(bool retryFlag)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.Retry = retryFlag;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002790 File Offset: 0x00000990
		protected void SetExitFlag(bool exitFlag)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.Exit = exitFlag;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027AC File Offset: 0x000009AC
		protected void SetTopMost(bool topMost)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.TopMost = topMost;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027C8 File Offset: 0x000009C8
		protected void EnablePrintButton(bool enablePrintButton)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.EnablePrintButton(enablePrintButton);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027E4 File Offset: 0x000009E4
		protected void PressButton(WizardButtons buttons)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.PressButton(buttons);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002800 File Offset: 0x00000A00
		protected void DoBtnNextClick()
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.DoBtnNextClick(false);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000281C File Offset: 0x00000A1C
		protected void EnableNextButtonTimer(int interval)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.EnableNextButtonTimer(interval);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002838 File Offset: 0x00000A38
		protected void DisableNextButtonTimer()
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.DisableNextButtonTimer();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002854 File Offset: 0x00000A54
		protected void EnableCheckLoadedTimer(int interval)
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.EnableCheckLoadedTimer(interval);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002870 File Offset: 0x00000A70
		protected void DisableCheckLoadedTimer()
		{
			SetupFormBase wizard = this.GetWizard();
			wizard.DisableCheckLoadedTimer();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000288A File Offset: 0x00000A8A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028A9 File Offset: 0x00000AA9
		private void WizardPage_SetLoaded(object sender, CancelEventArgs e)
		{
			this.DisableCheckLoadedTimer();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028B4 File Offset: 0x00000AB4
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleMode = AutoScaleMode.Inherit;
			this.BackColor = Color.Transparent;
			this.DoubleBuffered = true;
			this.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.ForeColor = Color.FromArgb(51, 51, 51);
			base.Name = "SetupWizardPage";
			base.Size = new Size(721, 435);
			base.ResumeLayout(false);
		}

		// Token: 0x04000001 RID: 1
		internal static Color DefaultNormalColor = Color.FromArgb(80, 80, 80);

		// Token: 0x04000002 RID: 2
		internal static Color DefaultHighlightColor = Color.FromArgb(51, 51, 51);

		// Token: 0x04000003 RID: 3
		internal static Color DefaultDisabledColor = Color.FromArgb(221, 221, 221);

		// Token: 0x04000004 RID: 4
		private IContainer components;
	}
}
