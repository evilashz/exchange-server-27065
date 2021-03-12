using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x0200001F RID: 31
	internal class InitializingSetupPage : SetupWizardPage
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000802C File Offset: 0x0000622C
		public InitializingSetupPage(IList<SetupWizardPage> pages, SetupBase theApp)
		{
			this.pages = pages;
			this.theApp = theApp;
			this.InitializeComponent();
			base.PageTitle = Strings.InitializingSetupPageTitle;
			base.WizardCancel += this.InitializingSetupPage_WizardCancel;
			this.progressTimer.Tick += this.ProgressTimer_Tick;
			this.progressTimer.Interval = 100;
			this.progress = 0;
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += this.BackgroundWorker_DoWork;
			this.backgroundWorker.RunWorkerCompleted += this.BackgroundWorker_RunWorkerCompleted;
			this.EnableNextAutoClick = true;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000080ED File Offset: 0x000062ED
		// (set) Token: 0x06000164 RID: 356 RVA: 0x000080F5 File Offset: 0x000062F5
		internal bool EnableNextAutoClick { get; set; }

		// Token: 0x06000165 RID: 357 RVA: 0x000080FE File Offset: 0x000062FE
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008120 File Offset: 0x00006320
		private void InitializingSetupPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			base.SetWizardButtons(WizardButtons.None);
			base.SetVisibleWizardButtons(WizardButtons.None);
			base.EnableCancelButton(true);
			this.setupInitializingLabel.Text = Strings.InitializingSetupText;
			base.EnableCheckLoadedTimer(200);
			SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
			if (!this.backgroundWorker.IsBusy)
			{
				this.progressTimer.Start();
				this.indeterminiteProgressBar.Visible = true;
				this.backgroundWorker.RunWorkerAsync();
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000081A8 File Offset: 0x000063A8
		private void ProgressTimer_Tick(object sender, EventArgs e)
		{
			if (++this.progress > 100)
			{
				this.progress = 0;
			}
			this.indeterminiteProgressBar.Value = this.progress;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000081E4 File Offset: 0x000063E4
		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			if (backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
				return;
			}
			SetupWizard.PopulateWizard(this.pages, this.theApp);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000821C File Offset: 0x0000641C
		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.progressTimer.Stop();
			this.indeterminiteProgressBar.Visible = false;
			if (e.Error != null)
			{
				MessageBoxHelper.ShowError(e.Error.Message);
				ExSetupUI.ExitApplication(ExitCode.Error);
				return;
			}
			if (e.Cancelled)
			{
				ExSetupUI.ExitApplication(ExitCode.Success);
				return;
			}
			base.SetWizardButtons(WizardButtons.Next);
			if (this.EnableNextAutoClick)
			{
				base.DoBtnNextClick();
				return;
			}
			base.SetVisibleWizardButtons(WizardButtons.Next);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000828C File Offset: 0x0000648C
		private void InitializingSetupPage_WizardCancel(object sender, CancelEventArgs e)
		{
			if (this.backgroundWorker.WorkerSupportsCancellation && this.backgroundWorker.IsBusy)
			{
				this.backgroundWorker.CancelAsync();
			}
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000082BC File Offset: 0x000064BC
		private void InitializingSetupPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.setupInitializingLabel.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008304 File Offset: 0x00006504
		private void InitializeComponent()
		{
			this.components = new Container();
			this.setupInitializingLabel = new Label();
			this.backgroundWorker = new BackgroundWorker();
			this.progressTimer = new System.Windows.Forms.Timer(this.components);
			this.indeterminiteProgressBar = new CustomProgressBar();
			base.SuspendLayout();
			this.setupInitializingLabel.AutoSize = true;
			this.setupInitializingLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.setupInitializingLabel.ForeColor = Color.FromArgb(51, 51, 51);
			this.setupInitializingLabel.Location = new Point(0, 0);
			this.setupInitializingLabel.Margin = new Padding(0);
			this.setupInitializingLabel.MaximumSize = new Size(720, 0);
			this.setupInitializingLabel.Name = "setupInitializingLabel";
			this.setupInitializingLabel.Size = new Size(121, 15);
			this.setupInitializingLabel.TabIndex = 21;
			this.setupInitializingLabel.Text = "[SetupInitializingText]";
			this.indeterminiteProgressBar.BackColor = Color.FromArgb(198, 198, 198);
			this.indeterminiteProgressBar.ForeColor = Color.FromArgb(0, 114, 198);
			this.indeterminiteProgressBar.Location = new Point(3, 49);
			this.indeterminiteProgressBar.Margin = new Padding(0);
			this.indeterminiteProgressBar.Name = "indeterminiteProgressBar";
			this.indeterminiteProgressBar.Size = new Size(708, 20);
			this.indeterminiteProgressBar.TabIndex = 22;
			this.indeterminiteProgressBar.Value = 0;
			base.Controls.Add(this.indeterminiteProgressBar);
			base.Controls.Add(this.setupInitializingLabel);
			base.Name = "InitializingSetupPage";
			base.SetActive += this.InitializingSetupPage_SetActive;
			base.CheckLoaded += this.InitializingSetupPage_CheckLoaded;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000C7 RID: 199
		private IContainer components;

		// Token: 0x040000C8 RID: 200
		private BackgroundWorker backgroundWorker;

		// Token: 0x040000C9 RID: 201
		private Label setupInitializingLabel;

		// Token: 0x040000CA RID: 202
		private readonly SetupBase theApp;

		// Token: 0x040000CB RID: 203
		private System.Windows.Forms.Timer progressTimer;

		// Token: 0x040000CC RID: 204
		private CustomProgressBar indeterminiteProgressBar;

		// Token: 0x040000CD RID: 205
		private int progress;

		// Token: 0x040000CE RID: 206
		private readonly IList<SetupWizardPage> pages;
	}
}
