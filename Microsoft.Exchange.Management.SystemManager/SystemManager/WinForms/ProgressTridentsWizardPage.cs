using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001FC RID: 508
	public class ProgressTridentsWizardPage : TridentsWizardPage
	{
		// Token: 0x0600170D RID: 5901 RVA: 0x00060E7B File Offset: 0x0005F07B
		public ProgressTridentsWizardPage()
		{
			this.InitializeComponent();
			this.Text = Strings.WizardCompletionTitleText;
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00060EA8 File Offset: 0x0005F0A8
		private void InitializeComponent()
		{
			this.worker = new BackgroundWorker();
			((ISupportInitialize)base.BindingSource).BeginInit();
			base.SuspendLayout();
			base.InputValidationProvider.SetEnabled(base.BindingSource, true);
			this.worker.WorkerSupportsCancellation = true;
			this.worker.DoWork += this.worker_DoWork;
			this.worker.RunWorkerCompleted += this.worker_RunWorkerCompleted;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "ProgressTridentsWizardPage";
			((ISupportInitialize)base.BindingSource).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00060F5C File Offset: 0x0005F15C
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x00060F64 File Offset: 0x0005F164
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00060F6D File Offset: 0x0005F16D
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x00060F75 File Offset: 0x0005F175
		[DefaultValue(false)]
		public bool NeverRunTaskAgain
		{
			get
			{
				return this.neverRunTaskAgain;
			}
			set
			{
				this.neverRunTaskAgain = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x00060F7E File Offset: 0x0005F17E
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x00060F86 File Offset: 0x0005F186
		[DefaultValue(false)]
		public bool RunTaskOnSetActive
		{
			get
			{
				return this.runTaskOnSetActive;
			}
			set
			{
				this.runTaskOnSetActive = value;
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00060F8F File Offset: 0x0005F18F
		protected override void OnContextChanged(EventArgs e)
		{
			base.OnContextChanged(e);
			if (base.DataHandler != null)
			{
				base.WorkUnits = base.DataHandler.WorkUnits;
				return;
			}
			base.WorkUnits = null;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00060FBC File Offset: 0x0005F1BC
		protected override void OnSetActive(EventArgs e)
		{
			base.ElapsedTimeText = string.Empty;
			string text = base.ShortDescription.Trim();
			if (string.IsNullOrEmpty(text))
			{
				if (base.Wizard != null && base.Wizard.ParentForm is WizardForm)
				{
					string action = ExchangeUserControl.RemoveAccelerator(base.NextButtonText);
					text = Strings.WizardWillUseConfigurationBelow(action);
				}
				else
				{
					text = base.DataHandler.InProgressDescription.Trim();
				}
				base.ShortDescription = text;
			}
			base.WorkUnitsPanel.TaskState = 0;
			this.scenario = this.GetPageScenario();
			if (base.CheckReadOnlyAndDisablePage())
			{
				base.CanGoForward = false;
				this.allowTaskToRun = false;
				base.CanCancel = true;
				base.InformationDescription = string.Empty;
				base.Status = string.Empty;
				if (base.Context != null)
				{
					base.ShortDescription = Strings.WizardCannotEditObject(base.DataHandler.ObjectReadOnlyReason);
				}
				base.WorkUnitsPanel.Visible = false;
			}
			else if (base.DataHandler != null && this.NeedToRunTask)
			{
				CollapsiblePanel.Animate = false;
				base.WorkUnitsPanel.SuspendLayout();
				base.DataHandler.WorkUnits.RaiseListChangedEvents = false;
				try
				{
					base.DataHandler.UpdateWorkUnits();
					WizardForm wizardForm = (WizardForm)base.FindForm();
					if (wizardForm != null && wizardForm.Icon != null)
					{
						foreach (WorkUnit workUnit in base.WorkUnits)
						{
							if (workUnit.Icon == null)
							{
								workUnit.Icon = wizardForm.Icon;
							}
							workUnit.Status = WorkUnitStatus.NotStarted;
						}
					}
				}
				finally
				{
					base.Status = base.DataHandler.WorkUnits.Description;
					base.WorkUnitsPanel.ResumeLayout(false);
					base.DataHandler.WorkUnits.RaiseListChangedEvents = true;
					base.DataHandler.WorkUnits.ResetBindings();
					CollapsiblePanel.Animate = true;
				}
			}
			base.OnSetActive(e);
			if ((this.RunTaskOnSetActive || this.scenario.RunTaskOnSetActive) && this.NeedToRunTask)
			{
				this.StartTheTask();
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000611F4 File Offset: 0x0005F3F4
		protected override void OnGoBack(CancelEventArgs e)
		{
			base.OnGoBack(e);
			if (!e.Cancel)
			{
				this.NeedToRetryTask = false;
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0006120C File Offset: 0x0005F40C
		protected override void OnGoForward(CancelEventArgs e)
		{
			base.OnGoForward(e);
			if (e.Cancel)
			{
				return;
			}
			if ((this.NeedToRunTask && !this.RunTaskOnSetActive && !this.scenario.RunTaskOnSetActive) || this.NeedToRetryTask)
			{
				this.NeedToRetryTask = false;
				this.StartTheTask();
				e.Cancel = true;
			}
			if (!e.Cancel)
			{
				this.allowTaskToRun = false;
				if (base.Wizard != null)
				{
					for (int i = 0; i <= this.PageIndex - 1; i++)
					{
						base.Wizard.WizardPages[i].Enabled = false;
					}
				}
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000612A4 File Offset: 0x0005F4A4
		private void StartTheTask()
		{
			base.SuspendLayout();
			try
			{
				foreach (WorkUnit workUnit in base.DataHandler.WorkUnits)
				{
					workUnit.Status = WorkUnitStatus.NotStarted;
				}
				base.WorkUnitsPanel.TaskState = 1;
				base.ElapsedTimeText = base.WorkUnits.ElapsedTimeText;
				base.IsDirty = true;
				base.CanGoForward = false;
				base.CanGoBack = false;
				base.CanFinish = false;
				base.CanCancel = (this.CanCancelDataHandler && !this.scenario.DisableCancelDuringTask);
				if ((this.scenario.AutomaticallyMoveNext || this.scenario.ShowCompletionText) && base.DataHandler != null)
				{
					base.ShortDescription = base.DataHandler.InProgressDescription.Trim();
				}
				SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
				this.worker.RunWorkerAsync();
			}
			finally
			{
				base.ResumeLayout();
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x000613B8 File Offset: 0x0005F5B8
		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			if (base.DataHandler != null)
			{
				base.DataHandler.Save(new WinFormsCommandInteractionHandler(base.ShellUI));
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000613D8 File Offset: 0x0005F5D8
		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			base.WorkUnitsPanel.TaskState = 2;
			base.ElapsedTimeText = base.WorkUnits.ElapsedTimeText;
			if (e.Error != null && !ExceptionHelper.IsWellknownCommandExecutionException(e.Error))
			{
				base.ShellUI.ShowError(e.Error);
			}
			if (base.DataHandler != null && base.DataHandler.WorkUnits.Cancelled)
			{
				base.Status = Strings.TheWizardWasCancelled;
				base.ShortDescription = (this.scenario.ShowCompletionText ? (Strings.WizardCancelledNotAllActionsCompleted + " " + Strings.FinishWizardDescription) : Strings.WizardCancelledNotAllActionsCompleted);
			}
			else if (this.scenario.ShowCompletionText && base.DataHandler != null)
			{
				base.Status = base.DataHandler.CompletionStatus;
				string text = base.DataHandler.CompletionDescription.Trim();
				if (string.IsNullOrEmpty(text))
				{
					if (base.Wizard != null && base.Wizard.ParentForm is WizardForm)
					{
						if (base.DataHandler.IsSucceeded)
						{
							text = (base.WorkUnits.AllCompleted ? Strings.WizardCompletionSucceededDescription : Strings.WizardCompletionPartialSucceededDescription);
						}
						else
						{
							text = Strings.WizardCompletionFailedDescription;
						}
					}
					text = text + " " + Strings.FinishWizardDescription;
				}
				base.ShortDescription = text;
			}
			else if (base.DataHandler != null && this.scenario.AutomaticallyMoveNext)
			{
				base.Status = base.DataHandler.CompletionStatus;
				base.ShortDescription = base.DataHandler.CompletionDescription;
			}
			bool flag = e.Error != null || (base.DataHandler != null && base.DataHandler.WorkUnits.HasFailures);
			this.runTaskIfAllowed = (flag ? this.scenario.CanRunTaskAgainIfFail : this.scenario.CanRunTaskAgainIfOK);
			if (this.scenario.CanDoRetryIfFail && flag)
			{
				this.NeedToRetryTask = true;
			}
			base.CanGoForward = (!flag || this.scenario.CanGoForwardIfFail || this.NeedToRetryTask);
			base.CanGoBack = ((flag ? this.scenario.CanRunTaskAgainIfFail : this.scenario.CanRunTaskAgainIfOK) && !base.DataHandler.Cancelled && !this.NeverRunTaskAgain);
			base.CanFinish = true;
			if (this.scenario.DisableCancelDuringTask || !this.CanCancelDataHandler)
			{
				base.CanCancel = true;
			}
			if (this.scenario.DisableCancelAfterTask)
			{
				base.CanCancel = false;
			}
			this.OnTaskCompleted(e);
			bool flag2 = flag ? this.scenario.DisableTaskIfFail : this.scenario.DisableTaskIfOK;
			if (flag2)
			{
				this.allowTaskToRun = false;
				base.NextButtonText = Strings.Next;
			}
			if (base.Wizard != null)
			{
				base.Wizard.IsDataChanged = base.DataHandler.WorkUnits.IsDataChanged;
				bool flag3 = flag ? this.scenario.DisablePreviousPagesIfFail : this.scenario.DisablePreviousPagesIfOK;
				if (flag3)
				{
					for (int i = 0; i <= this.PageIndex - 1; i++)
					{
						base.Wizard.WizardPages[i].Enabled = false;
					}
				}
				bool flag4 = this.scenario.MoveNextIfNoTask && base.DataHandler != null && !base.DataHandler.HasWorkUnits;
				if (this.scenario.AutomaticallyMoveNext || flag4)
				{
					base.Wizard.CurrentPageIndex++;
				}
			}
			if (this.closeOnFinish)
			{
				Form form = base.FindForm();
				if (form != null)
				{
					form.Close();
				}
			}
		}

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x0600171C RID: 5916 RVA: 0x0006178C File Offset: 0x0005F98C
		// (remove) Token: 0x0600171D RID: 5917 RVA: 0x000617C4 File Offset: 0x0005F9C4
		public event EventHandler TaskComplete;

		// Token: 0x0600171E RID: 5918 RVA: 0x000617F9 File Offset: 0x0005F9F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnTaskCompleted(RunWorkerCompletedEventArgs e)
		{
			if (this.TaskComplete != null)
			{
				this.TaskComplete(this, e);
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00061820 File Offset: 0x0005FA20
		protected override void OnCancel(CancelEventArgs e)
		{
			base.OnCancel(e);
			if (!e.Cancel && this.worker.IsBusy)
			{
				this.closeOnFinish = this.scenario.CloseWizardIfCancelled;
				if (base.CanCancel)
				{
					base.CanCancel = false;
					if (base.DataHandler != null)
					{
						WinformsHelper.InvokeAsync(delegate
						{
							base.DataHandler.Cancel();
						}, this);
					}
				}
				e.Cancel = (this.worker.IsBusy || !this.closeOnFinish);
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x000618A9 File Offset: 0x0005FAA9
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x000618B4 File Offset: 0x0005FAB4
		private bool NeedToRetryTask
		{
			get
			{
				return this.needToRetryTask;
			}
			set
			{
				if (value != this.NeedToRetryTask)
				{
					this.needToRetryTask = value;
					if (this.NeedToRetryTask)
					{
						this.previousNextButtonText = base.NextButtonText;
						base.NextButtonText = Strings.Retry;
						return;
					}
					if (!string.IsNullOrEmpty(this.previousNextButtonText))
					{
						base.NextButtonText = this.previousNextButtonText;
					}
				}
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x0006190F File Offset: 0x0005FB0F
		protected int PageIndex
		{
			get
			{
				if (base.Wizard == null)
				{
					return -1;
				}
				return base.Wizard.WizardPages.IndexOf(this);
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0006192C File Offset: 0x0005FB2C
		private bool CanCancelDataHandler
		{
			get
			{
				return base.DataHandler != null && base.DataHandler.CanCancel;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x00061943 File Offset: 0x0005FB43
		protected bool NeedToRunTask
		{
			get
			{
				return this.allowTaskToRun && this.runTaskIfAllowed;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00061955 File Offset: 0x0005FB55
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x0006195D File Offset: 0x0005FB5D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00061968 File Offset: 0x0005FB68
		private ProgressTridentsWizardPage.Scenario GetPageScenario()
		{
			if (base.Wizard == null)
			{
				return ProgressTridentsWizardPage.Scenario.LightPage;
			}
			int pageIndex = this.PageIndex;
			if (pageIndex == base.Wizard.WizardPages.Count - 1)
			{
				return ProgressTridentsWizardPage.Scenario.LightPage;
			}
			if (base.Wizard.WizardPages[pageIndex + 1] is ProgressTridentsWizardPage)
			{
				return ProgressTridentsWizardPage.Scenario.PreReqPage;
			}
			if (pageIndex > 0 && base.Wizard.WizardPages[pageIndex - 1] is ProgressTridentsWizardPage)
			{
				return ProgressTridentsWizardPage.Scenario.InstallPage;
			}
			return ProgressTridentsWizardPage.Scenario.NormalPage;
		}

		// Token: 0x0400088C RID: 2188
		private BackgroundWorker worker;

		// Token: 0x0400088D RID: 2189
		private bool closeOnFinish;

		// Token: 0x0400088E RID: 2190
		private bool allowTaskToRun = true;

		// Token: 0x0400088F RID: 2191
		private bool runTaskIfAllowed = true;

		// Token: 0x04000890 RID: 2192
		private bool needToRetryTask;

		// Token: 0x04000891 RID: 2193
		private LocalizedString previousNextButtonText;

		// Token: 0x04000892 RID: 2194
		private ProgressTridentsWizardPage.Scenario scenario;

		// Token: 0x04000893 RID: 2195
		private bool neverRunTaskAgain;

		// Token: 0x04000894 RID: 2196
		private bool runTaskOnSetActive;

		// Token: 0x020001FD RID: 509
		private class Scenario
		{
			// Token: 0x06001729 RID: 5929 RVA: 0x000619F0 File Offset: 0x0005FBF0
			public Scenario(bool runTaskOnSetActive, bool automaticallyMoveNext, bool disablePreviousPagesIfOK, bool disablePreviousPagesIfFail, bool disableTaskIfOK, bool disableTaskIfFail, bool disableCancelDuringTask, bool disableCancelAfterTask, bool canGoForwardIfFail, bool canRunTaskAgainIfOK, bool canRunTaskAgainIfFail, bool showCompletionText, bool closeWizardIfCancelled, bool canDoRetryIfFail, bool moveNextIfNoTask)
			{
				this.RunTaskOnSetActive = runTaskOnSetActive;
				this.AutomaticallyMoveNext = automaticallyMoveNext;
				this.DisablePreviousPagesIfOK = disablePreviousPagesIfOK;
				this.DisablePreviousPagesIfFail = disablePreviousPagesIfFail;
				this.DisableTaskIfOK = disableTaskIfOK;
				this.DisableTaskIfFail = disableTaskIfFail;
				this.DisableCancelDuringTask = disableCancelDuringTask;
				this.DisableCancelAfterTask = disableCancelAfterTask;
				this.CanGoForwardIfFail = canGoForwardIfFail;
				this.CanRunTaskAgainIfOK = canRunTaskAgainIfOK;
				this.CanRunTaskAgainIfFail = canRunTaskAgainIfFail;
				this.ShowCompletionText = showCompletionText;
				this.CloseWizardIfCancelled = closeWizardIfCancelled;
				this.CanDoRetryIfFail = canDoRetryIfFail;
				this.MoveNextIfNoTask = moveNextIfNoTask;
			}

			// Token: 0x04000896 RID: 2198
			public static readonly ProgressTridentsWizardPage.Scenario LightPage = new ProgressTridentsWizardPage.Scenario(true, false, true, false, true, false, false, true, false, false, true, true, false, false, false);

			// Token: 0x04000897 RID: 2199
			public static readonly ProgressTridentsWizardPage.Scenario NormalPage = new ProgressTridentsWizardPage.Scenario(false, true, true, false, true, false, false, false, true, false, true, false, false, false, false);

			// Token: 0x04000898 RID: 2200
			public static readonly ProgressTridentsWizardPage.Scenario PreReqPage = new ProgressTridentsWizardPage.Scenario(true, false, false, false, false, false, false, false, false, true, true, false, true, true, true);

			// Token: 0x04000899 RID: 2201
			public static readonly ProgressTridentsWizardPage.Scenario InstallPage = new ProgressTridentsWizardPage.Scenario(true, true, true, true, true, true, true, true, true, false, false, false, false, false, false);

			// Token: 0x0400089A RID: 2202
			public readonly bool RunTaskOnSetActive;

			// Token: 0x0400089B RID: 2203
			public readonly bool AutomaticallyMoveNext;

			// Token: 0x0400089C RID: 2204
			public readonly bool DisablePreviousPagesIfOK;

			// Token: 0x0400089D RID: 2205
			public readonly bool DisablePreviousPagesIfFail;

			// Token: 0x0400089E RID: 2206
			public readonly bool DisableTaskIfOK;

			// Token: 0x0400089F RID: 2207
			public readonly bool DisableTaskIfFail;

			// Token: 0x040008A0 RID: 2208
			public readonly bool DisableCancelDuringTask;

			// Token: 0x040008A1 RID: 2209
			public readonly bool DisableCancelAfterTask;

			// Token: 0x040008A2 RID: 2210
			public readonly bool CanGoForwardIfFail;

			// Token: 0x040008A3 RID: 2211
			public readonly bool CanRunTaskAgainIfOK;

			// Token: 0x040008A4 RID: 2212
			public readonly bool CanRunTaskAgainIfFail;

			// Token: 0x040008A5 RID: 2213
			public readonly bool ShowCompletionText;

			// Token: 0x040008A6 RID: 2214
			public readonly bool CloseWizardIfCancelled;

			// Token: 0x040008A7 RID: 2215
			public readonly bool CanDoRetryIfFail;

			// Token: 0x040008A8 RID: 2216
			public readonly bool MoveNextIfNoTask;
		}
	}
}
