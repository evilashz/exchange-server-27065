using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.ExSetupUI;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x0200000D RID: 13
	internal class PreCheckPage : ProgressPageBase
	{
		// Token: 0x06000068 RID: 104 RVA: 0x0000640C File Offset: 0x0000460C
		public PreCheckPage(RootDataHandler rootDataHandler)
		{
			this.rootDataHandler = rootDataHandler;
			this.TaskState = TaskState.NotStarted;
			this.InitializeComponent();
			base.PageTitle = Strings.PreCheckPageTitle;
			this.preChecksLabel.Text = Strings.PreCheckDescriptionText;
			this.customProgressBarWithTitle.Title = string.Empty;
			this.reportTextBox.Text = string.Empty;
			this.reportTextBox.Enabled = false;
			this.reportTextBox.Visible = false;
			base.WizardRetry += this.PreCheckPage_Retry;
			base.WizardCancel += this.PreCheckPage_WizardCancel;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000069 RID: 105 RVA: 0x000064B4 File Offset: 0x000046B4
		// (remove) Token: 0x0600006A RID: 106 RVA: 0x000064EC File Offset: 0x000046EC
		public event EventHandler TaskStateChanged;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600006B RID: 107 RVA: 0x00006524 File Offset: 0x00004724
		// (remove) Token: 0x0600006C RID: 108 RVA: 0x0000655C File Offset: 0x0000475C
		public event EventHandler TaskComplete;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600006D RID: 109 RVA: 0x00006594 File Offset: 0x00004794
		// (remove) Token: 0x0600006E RID: 110 RVA: 0x000065CC File Offset: 0x000047CC
		public event PropertyChangedEventHandler WorkUnitPropertyChanged;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00006601 File Offset: 0x00004801
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00006609 File Offset: 0x00004809
		internal TaskState TaskState
		{
			get
			{
				return this.taskState;
			}
			set
			{
				if (this.taskState != value)
				{
					this.taskState = value;
					this.OnTaskStateChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00006626 File Offset: 0x00004826
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00006645 File Offset: 0x00004845
		protected virtual void OnTaskStateChanged(EventArgs e)
		{
			if (this.TaskStateChanged != null)
			{
				this.TaskStateChanged(this, e);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000665C File Offset: 0x0000485C
		protected virtual void OnTaskCompleted(EventArgs e)
		{
			if (this.TaskComplete != null)
			{
				this.TaskComplete(this, e);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00006673 File Offset: 0x00004873
		protected virtual void OnWorkUnitPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this.WorkUnitPropertyChanged != null)
			{
				this.WorkUnitPropertyChanged(this, e);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000668A File Offset: 0x0000488A
		private void PreCheckPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			this.StartPrecheck();
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000066AC File Offset: 0x000048AC
		private void StartPrecheck()
		{
			if (this.rootDataHandler.Mode == InstallationModes.Uninstall)
			{
				base.SetBtnNextText(Strings.Uninstall);
			}
			else
			{
				base.SetBtnNextText(Strings.Install);
			}
			base.SetWizardButtons(WizardButtons.None);
			base.SetVisibleWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			this.reportTextBox.Text = string.Empty;
			this.reportTextBox.Enabled = false;
			this.reportTextBox.Visible = false;
			base.SetRetryFlag(false);
			this.reportTextBox.LinkClicked = new LinkClickedEventHandler(this.ReportTextBox_LinkClicked);
			if (this.rootDataHandler.IsUmLanguagePackOperation)
			{
				if (this.rootDataHandler.Mode == InstallationModes.Install)
				{
					AddUmLanguagePackModeDataHandler addUmLanguagePackModeDataHandler = this.rootDataHandler.ModeDatahandler as AddUmLanguagePackModeDataHandler;
					this.preCheckDataHandler = addUmLanguagePackModeDataHandler.PreCheckDataHandler;
				}
			}
			else
			{
				this.preCheckDataHandler = this.rootDataHandler.ModeDatahandler.PreCheckDataHandler;
			}
			this.preCheckDataHandler.WorkUnits.RaiseListChangedEvents = false;
			this.preCheckDataHandler.UpdateWorkUnits();
			this.workUnits = this.preCheckDataHandler.WorkUnits;
			this.preCheckDataHandler.WorkUnits.RaiseListChangedEvents = true;
			foreach (WorkUnit workUnit in this.workUnits)
			{
				workUnit.Status = WorkUnitStatus.NotStarted;
				workUnit.PropertyChanged += this.WorkUnit_PropertyChanged;
			}
			this.TaskState = TaskState.InProgress;
			this.customProgressBarWithTitle.Title = this.workUnits[0].Text;
			this.customProgressBarWithTitle.Value = 0;
			SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
			this.backgroundWorker.RunWorkerAsync();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006864 File Offset: 0x00004A64
		private void ReportTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			SetupFormBase.ShowHelpFromUrl(e.LinkText);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006874 File Offset: 0x00004A74
		private void WorkUnit_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new PropertyChangedEventHandler(this.WorkUnit_PropertyChanged), new object[]
				{
					sender,
					e
				});
				return;
			}
			this.UpdateProgress();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000068B4 File Offset: 0x00004AB4
		private void UpdateProgress()
		{
			if (this.TaskState != TaskState.InProgress)
			{
				return;
			}
			foreach (WorkUnit workUnit in this.workUnits)
			{
				StringBuilder stringBuilder = new StringBuilder(2048);
				switch (workUnit.Status)
				{
				case WorkUnitStatus.InProgress:
					this.UpdateProgressBar(workUnit);
					break;
				case WorkUnitStatus.Completed:
					if (!string.IsNullOrEmpty(workUnit.WarningsDescription))
					{
						stringBuilder.AppendLine(workUnit.WarningsDescription);
					}
					break;
				case WorkUnitStatus.Failed:
					if (!string.IsNullOrEmpty(workUnit.ErrorsDescription))
					{
						stringBuilder.AppendLine(workUnit.ErrorsDescription);
					}
					else
					{
						stringBuilder.AppendLine(Strings.FatalError);
					}
					if (!string.IsNullOrEmpty(workUnit.WarningsDescription))
					{
						stringBuilder.AppendLine(workUnit.WarningsDescription);
					}
					break;
				}
				string value = stringBuilder.ToString().Trim();
				if (!string.IsNullOrEmpty(value))
				{
					this.reportTextBox.Enabled = true;
					this.reportTextBox.InitializeCustomScrollbar();
					this.reportTextBox.Visible = true;
					this.reportTextBox.Text = stringBuilder.ToString();
				}
				if (workUnit.Status == WorkUnitStatus.Failed)
				{
					this.TaskState = TaskState.Completed;
					this.backgroundWorker.CancelAsync();
					break;
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00006A14 File Offset: 0x00004C14
		private void UpdateProgressBar(WorkUnit workUnit)
		{
			if (!this.customProgressBarWithTitle.Visible)
			{
				this.customProgressBarWithTitle.Visible = true;
				this.reportTextBox.Enabled = false;
				this.reportTextBox.Visible = false;
			}
			this.customProgressBarWithTitle.Title = workUnit.Text;
			this.customProgressBarWithTitle.Value = workUnit.PercentComplete;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00006A74 File Offset: 0x00004C74
		private void PreCheckPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.preChecksLabel.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00006ABA File Offset: 0x00004CBA
		private void PreCheckPage_WizardCancel(object sender, CancelEventArgs e)
		{
			this.StopPreCheckDataHandler();
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00006AC8 File Offset: 0x00004CC8
		private void StopPreCheckDataHandler()
		{
			if (this.backgroundWorker.IsBusy && this.preCheckDataHandler != null)
			{
				this.preCheckDataHandler.Cancel();
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00006AEC File Offset: 0x00004CEC
		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			if (backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
				this.StopPreCheckDataHandler();
				return;
			}
			if (this.preCheckDataHandler != null)
			{
				this.preCheckDataHandler.Save(null);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00006B2C File Offset: 0x00004D2C
		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.TaskState = TaskState.Completed;
			this.OnTaskCompleted(e);
			this.backgroundWorker.CancelAsync();
			if (this.preCheckDataHandler.IsSucceeded)
			{
				base.SetRetryFlag(false);
			}
			else
			{
				base.SetRetryFlag(true);
				base.SetBtnNextText(Strings.btnRetry);
				RestoreServer restoreServer = new RestoreServer();
				restoreServer.RestoreServerOnPrereqFailure();
			}
			base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			base.SetVisibleWizardButtons(WizardButtons.Next);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00006B99 File Offset: 0x00004D99
		private void PreCheckPage_Retry(object sender, CancelEventArgs e)
		{
			base.Focus();
			this.StartPrecheck();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00006BA8 File Offset: 0x00004DA8
		private void InitializeComponent()
		{
			this.preChecksLabel = new Label();
			this.backgroundWorker = new BackgroundWorker();
			this.reportTextBox = new CustomRichTextBox();
			base.SuspendLayout();
			this.customProgressBarWithTitle.Location = new Point(0, 60);
			this.customProgressBarWithTitle.Size = new Size(721, 76);
			this.preChecksLabel.AutoSize = true;
			this.preChecksLabel.BackColor = Color.Transparent;
			this.preChecksLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.preChecksLabel.Location = new Point(0, 0);
			this.preChecksLabel.MaximumSize = new Size(740, 0);
			this.preChecksLabel.Name = "preChecksLabel";
			this.preChecksLabel.Size = new Size(159, 17);
			this.preChecksLabel.TabIndex = 26;
			this.preChecksLabel.Text = "[PreCheckDescriptionText]";
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += this.BackgroundWorker_DoWork;
			this.backgroundWorker.RunWorkerCompleted += this.BackgroundWorker_RunWorkerCompleted;
			this.reportTextBox.BackColor = SystemColors.Window;
			this.reportTextBox.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.reportTextBox.ForeColor = Color.FromArgb(51, 51, 51);
			this.reportTextBox.Location = new Point(0, 150);
			this.reportTextBox.Margin = new Padding(0);
			this.reportTextBox.Name = "reportTextBox";
			this.reportTextBox.Size = new Size(721, 285);
			this.reportTextBox.TabIndex = 19;
			this.reportTextBox.Text = "[ReportText]";
			this.BackColor = Color.Transparent;
			base.Controls.Add(this.preChecksLabel);
			base.Controls.Add(this.reportTextBox);
			base.Name = "PreCheckPage";
			base.SetActive += this.PreCheckPage_SetActive;
			base.CheckLoaded += this.PreCheckPage_CheckLoaded;
			base.Controls.SetChildIndex(this.customProgressBarWithTitle, 0);
			base.Controls.SetChildIndex(this.reportTextBox, 0);
			base.Controls.SetChildIndex(this.preChecksLabel, 0);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000049 RID: 73
		private Label preChecksLabel;

		// Token: 0x0400004A RID: 74
		private BackgroundWorker backgroundWorker;

		// Token: 0x0400004B RID: 75
		private PreCheckDataHandler preCheckDataHandler;

		// Token: 0x0400004C RID: 76
		private IList<WorkUnit> workUnits;

		// Token: 0x0400004D RID: 77
		private TaskState taskState;

		// Token: 0x0400004E RID: 78
		private CustomRichTextBox reportTextBox;

		// Token: 0x0400004F RID: 79
		private RootDataHandler rootDataHandler;

		// Token: 0x04000050 RID: 80
		private IContainer components;
	}
}
