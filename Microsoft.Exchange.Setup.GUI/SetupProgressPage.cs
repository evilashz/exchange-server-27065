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
	// Token: 0x02000012 RID: 18
	internal class SetupProgressPage : ProgressPageBase
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00009994 File Offset: 0x00007B94
		public SetupProgressPage(RootDataHandler rootDataHandler)
		{
			this.rootDataHandler = rootDataHandler;
			this.TaskState = TaskState.NotStarted;
			this.InitializeComponent();
			base.PageTitle = Strings.SetupProgressPageTitle;
			this.setupProgressLabel.Text = string.Empty;
			this.customProgressBarWithTitle.Title = string.Empty;
			this.reportTextBox.Text = string.Empty;
			this.reportTextBox.Enabled = false;
			this.reportTextBox.Visible = false;
			this.currentStep = 1;
			base.WizardCancel += this.SetupProgressPage_WizardCancel;
			this.TaskComplete += this.SetupProgressPage_TaskComplete;
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060000C4 RID: 196 RVA: 0x00009A48 File Offset: 0x00007C48
		// (remove) Token: 0x060000C5 RID: 197 RVA: 0x00009A80 File Offset: 0x00007C80
		public event EventHandler TaskStateChanged;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060000C6 RID: 198 RVA: 0x00009AB8 File Offset: 0x00007CB8
		// (remove) Token: 0x060000C7 RID: 199 RVA: 0x00009AF0 File Offset: 0x00007CF0
		public event EventHandler TaskComplete;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060000C8 RID: 200 RVA: 0x00009B28 File Offset: 0x00007D28
		// (remove) Token: 0x060000C9 RID: 201 RVA: 0x00009B60 File Offset: 0x00007D60
		public event PropertyChangedEventHandler WorkUnitPropertyChanged;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00009B95 File Offset: 0x00007D95
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00009B9D File Offset: 0x00007D9D
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

		// Token: 0x060000CC RID: 204 RVA: 0x00009BBA File Offset: 0x00007DBA
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00009BD9 File Offset: 0x00007DD9
		protected virtual void OnTaskStateChanged(EventArgs e)
		{
			if (this.TaskStateChanged != null)
			{
				this.TaskStateChanged(this, e);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00009BF0 File Offset: 0x00007DF0
		protected virtual void OnTaskCompleted(EventArgs e)
		{
			if (this.TaskComplete != null)
			{
				this.TaskComplete(this, e);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00009C07 File Offset: 0x00007E07
		protected virtual void OnWorkUnitPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this.WorkUnitPropertyChanged != null)
			{
				this.WorkUnitPropertyChanged(this, e);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00009C20 File Offset: 0x00007E20
		private void SetupProgressPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			this.setupProgressLabel.Enabled = false;
			this.setupProgressLabel.Visible = false;
			base.SetWizardButtons(WizardButtons.None);
			base.SetVisibleWizardButtons(WizardButtons.None);
			this.rootDataHandler.WorkUnits.RaiseListChangedEvents = false;
			this.rootDataHandler.UpdateWorkUnits();
			this.workUnits = this.rootDataHandler.WorkUnits;
			this.totalSteps = this.workUnits.Count.ToString();
			this.rootDataHandler.WorkUnits.RaiseListChangedEvents = true;
			foreach (WorkUnit workUnit in this.workUnits)
			{
				workUnit.Status = WorkUnitStatus.NotStarted;
				workUnit.PropertyChanged += this.WorkUnit_PropertyChanged;
			}
			this.TaskState = TaskState.InProgress;
			this.customProgressBarWithTitle.Title = this.workUnits[0].Text;
			this.customProgressBarWithTitle.Value = 0;
			base.EnableCancelButton(false);
			SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
			this.backgroundWorker.RunWorkerAsync();
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00009D60 File Offset: 0x00007F60
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

		// Token: 0x060000D2 RID: 210 RVA: 0x00009DA0 File Offset: 0x00007FA0
		private void UpdateProgress()
		{
			foreach (WorkUnit workUnit in this.workUnits)
			{
				StringBuilder stringBuilder = new StringBuilder(2048);
				switch (workUnit.Status)
				{
				case WorkUnitStatus.InProgress:
				{
					int num = this.workUnits.IndexOf(workUnit);
					if (num >= this.currentStep)
					{
						this.currentStep = num;
					}
					this.UpdateProgressBar(workUnit);
					break;
				}
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
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00009EE8 File Offset: 0x000080E8
		private void UpdateProgressBar(WorkUnit workUnit)
		{
			if (!this.customProgressBarWithTitle.Visible)
			{
				this.customProgressBarWithTitle.Visible = true;
				this.reportTextBox.Enabled = false;
				this.reportTextBox.Visible = false;
			}
			this.customProgressBarWithTitle.Title = Strings.CurrentStep(this.currentStep.ToString(), this.totalSteps, workUnit.Text);
			this.customProgressBarWithTitle.Value = workUnit.PercentComplete;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00009F63 File Offset: 0x00008163
		private void SetupProgressPage_TaskComplete(object sender, EventArgs e)
		{
			if (this.rootDataHandler.IsSucceeded)
			{
				this.customProgressBarWithTitle.Title = Strings.SetupCompleted;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00009F88 File Offset: 0x00008188
		private void SetupProgressPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.setupProgressLabel.Name, true);
			if (array != null && array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00009FD1 File Offset: 0x000081D1
		private void SetupProgressPage_WizardCancel(object sender, CancelEventArgs e)
		{
			if (this.backgroundWorker.IsBusy && this.rootDataHandler != null)
			{
				this.rootDataHandler.Cancel();
			}
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00009FF9 File Offset: 0x000081F9
		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.rootDataHandler != null)
			{
				this.rootDataHandler.Save(null);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000A010 File Offset: 0x00008210
		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.TaskState = TaskState.Completed;
			this.OnTaskCompleted(e);
			if (!this.rootDataHandler.IsSucceeded)
			{
				this.RemoveSetupCompletedPage();
				base.SetWizardButtons(WizardButtons.Next);
				base.SetBtnNextText(Strings.btnExit);
				base.SetVisibleWizardButtons(WizardButtons.Next);
				return;
			}
			base.SetWizardButtons(WizardButtons.Next);
			if ((this.rootDataHandler.Mode == InstallationModes.Install || this.rootDataHandler.Mode == InstallationModes.BuildToBuildUpgrade) && !this.rootDataHandler.IsUmLanguagePackOperation && !this.rootDataHandler.IsLanguagePackOperation)
			{
				base.SetVisibleWizardButtons(WizardButtons.Next);
				base.PressButton(WizardButtons.Next);
				return;
			}
			base.SetBtnNextText(Strings.btnFinish);
			base.SetVisibleWizardButtons(WizardButtons.Next);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000A0C0 File Offset: 0x000082C0
		private void RemoveSetupCompletedPage()
		{
			this.setupCompletedPage = (SetupCompletedPage)base.FindPage("SetupCompletedPage");
			if (this.setupCompletedPage != null)
			{
				base.RemovePage(this.setupCompletedPage);
				this.setupCompletedPage = null;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000A0F4 File Offset: 0x000082F4
		private void InitializeComponent()
		{
			this.setupProgressLabel = new Label();
			this.backgroundWorker = new BackgroundWorker();
			this.reportTextBox = new CustomRichTextBox();
			base.SuspendLayout();
			this.customProgressBarWithTitle.Location = new Point(0, 60);
			this.customProgressBarWithTitle.Size = new Size(721, 76);
			this.setupProgressLabel.AutoSize = true;
			this.setupProgressLabel.BackColor = Color.Transparent;
			this.setupProgressLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.setupProgressLabel.Location = new Point(0, 0);
			this.setupProgressLabel.MaximumSize = new Size(720, 0);
			this.setupProgressLabel.Name = "setupProgressLabel";
			this.setupProgressLabel.Size = new Size(191, 17);
			this.setupProgressLabel.TabIndex = 26;
			this.setupProgressLabel.Text = "[SetupProgressDescriptionText]";
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += this.BackgroundWorker_DoWork;
			this.backgroundWorker.RunWorkerCompleted += this.BackgroundWorker_RunWorkerCompleted;
			this.reportTextBox.BackColor = SystemColors.Window;
			this.reportTextBox.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.reportTextBox.Location = new Point(0, 150);
			this.reportTextBox.Margin = new Padding(0);
			this.reportTextBox.Name = "reportTextBox";
			this.reportTextBox.Size = new Size(721, 285);
			this.reportTextBox.TabIndex = 19;
			this.reportTextBox.Text = "[ReportText]";
			this.BackColor = Color.Transparent;
			base.Controls.Add(this.setupProgressLabel);
			base.Controls.Add(this.reportTextBox);
			base.Name = "SetupProgressPage";
			base.SetActive += this.SetupProgressPage_SetActive;
			base.CheckLoaded += this.SetupProgressPage_CheckLoaded;
			base.Controls.SetChildIndex(this.customProgressBarWithTitle, 0);
			base.Controls.SetChildIndex(this.reportTextBox, 0);
			base.Controls.SetChildIndex(this.setupProgressLabel, 0);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000087 RID: 135
		private Label setupProgressLabel;

		// Token: 0x04000088 RID: 136
		private BackgroundWorker backgroundWorker;

		// Token: 0x04000089 RID: 137
		private RootDataHandler rootDataHandler;

		// Token: 0x0400008A RID: 138
		private IList<WorkUnit> workUnits;

		// Token: 0x0400008B RID: 139
		private TaskState taskState;

		// Token: 0x0400008C RID: 140
		private CustomRichTextBox reportTextBox;

		// Token: 0x0400008D RID: 141
		private SetupCompletedPage setupCompletedPage;

		// Token: 0x0400008E RID: 142
		private string totalSteps;

		// Token: 0x0400008F RID: 143
		private int currentStep = 1;

		// Token: 0x04000090 RID: 144
		private IContainer components;
	}
}
