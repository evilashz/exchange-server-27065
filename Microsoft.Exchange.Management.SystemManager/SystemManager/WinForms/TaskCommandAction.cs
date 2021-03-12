using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200016A RID: 362
	public class TaskCommandAction : CommandAction
	{
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0003849E File Offset: 0x0003669E
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x000384A6 File Offset: 0x000366A6
		[DefaultValue("")]
		public string CommandText
		{
			get
			{
				return this.commandText;
			}
			set
			{
				if (this.CommandText != value)
				{
					this.commandText = (value ?? string.Empty);
				}
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x000384C6 File Offset: 0x000366C6
		// (set) Token: 0x06000EB7 RID: 3767 RVA: 0x000384CE File Offset: 0x000366CE
		internal MonadParameterCollection Parameters
		{
			get
			{
				return this.parameters;
			}
			set
			{
				this.parameters = (value ?? new MonadParameterCollection());
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x000384E0 File Offset: 0x000366E0
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x000384E8 File Offset: 0x000366E8
		[DefaultValue(null)]
		public SingleSelectionMessageDelegate SingleSelectionConfirmation
		{
			get
			{
				return this.singleSelectionConfirmationDelegate;
			}
			set
			{
				this.singleSelectionConfirmationDelegate = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x000384F1 File Offset: 0x000366F1
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x000384F9 File Offset: 0x000366F9
		[DefaultValue(null)]
		public MultipleSelectionMessageDelegate MultipleSelectionConfirmation
		{
			get
			{
				return this.multipleSelectionConfirmationDelegate;
			}
			set
			{
				this.multipleSelectionConfirmationDelegate = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00038502 File Offset: 0x00036702
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x0003850A File Offset: 0x0003670A
		[DefaultValue(null)]
		public SingleSelectionMessageDelegate SingleSelectionError
		{
			get
			{
				return this.singleSelectionErrorDelegate;
			}
			set
			{
				this.singleSelectionErrorDelegate = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00038513 File Offset: 0x00036713
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x0003851B File Offset: 0x0003671B
		[DefaultValue(null)]
		public MultipleSelectionMessageDelegate MultipleSelectionError
		{
			get
			{
				return this.multipleSelectionErrorDelegate;
			}
			set
			{
				this.multipleSelectionErrorDelegate = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00038524 File Offset: 0x00036724
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x0003852C File Offset: 0x0003672C
		[DefaultValue(null)]
		public SingleSelectionMessageDelegate SingleSelectionWarning
		{
			get
			{
				return this.singleSelectionWarningDelegate;
			}
			set
			{
				this.singleSelectionWarningDelegate = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00038535 File Offset: 0x00036735
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x0003853D File Offset: 0x0003673D
		[DefaultValue(null)]
		public MultipleSelectionMessageDelegate MultipleSelectionWarning
		{
			get
			{
				return this.multipleSelectionWarningDelegate;
			}
			set
			{
				this.multipleSelectionWarningDelegate = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00038546 File Offset: 0x00036746
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x0003854E File Offset: 0x0003674E
		[DefaultValue(null)]
		public IRefreshable RefreshOnFinish
		{
			get
			{
				return this.refreshOnFinish;
			}
			set
			{
				this.refreshOnFinish = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00038557 File Offset: 0x00036757
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x0003855F File Offset: 0x0003675F
		[DefaultValue(null)]
		public IRefreshable[] MultiRefreshOnFinish
		{
			get
			{
				return this.multiRefreshOnFinish;
			}
			set
			{
				this.multiRefreshOnFinish = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00038568 File Offset: 0x00036768
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x00038570 File Offset: 0x00036770
		private IUIService TestUIService { get; set; }

		// Token: 0x06000ECA RID: 3786 RVA: 0x0003857C File Offset: 0x0003677C
		public IProgress CreateProgress(string operationName)
		{
			IProgressProvider progressProvider = (IProgressProvider)this.GetService(typeof(IProgressProvider));
			if (progressProvider != null)
			{
				return progressProvider.CreateProgress(operationName);
			}
			return NullProgress.Value;
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x000385AF File Offset: 0x000367AF
		protected string CommandDisplayName
		{
			get
			{
				return ExchangeUserControl.RemoveAccelerator(base.Command.Text);
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x000385C4 File Offset: 0x000367C4
		protected virtual bool ConfirmOperation(WorkUnitCollectionEventArgs inputArgs)
		{
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			string text = null;
			if (inputArgs.WorkUnits.Count == 1 && this.SingleSelectionConfirmation != null)
			{
				text = this.SingleSelectionConfirmation(inputArgs.WorkUnits[0].Text);
			}
			if (inputArgs.WorkUnits.Count > 1 && this.MultipleSelectionConfirmation != null)
			{
				text = this.MultipleSelectionConfirmation(inputArgs.WorkUnits.Count);
			}
			return string.IsNullOrEmpty(text) || DialogResult.No != iuiservice.ShowMessage(text, this.CommandDisplayName, MessageBoxButtons.YesNo);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00038B98 File Offset: 0x00036D98
		protected override void OnExecute()
		{
			string commandDisplayName = this.CommandDisplayName;
			WorkUnitCollectionEventArgs inputArgs = new WorkUnitCollectionEventArgs(new WorkUnitCollection());
			this.OnInputRequested(inputArgs);
			IUIService uiService = (IUIService)this.GetService(typeof(IUIService));
			if (uiService == null)
			{
				throw new InvalidOperationException("TaskCommand must be sited and needs to be able to find an IUIService.");
			}
			Control controlToRestoreFocus = uiService.GetDialogOwnerWindow() as Control;
			IRefreshable singleRefreshOnFinish = this.RefreshOnFinish;
			IRefreshable[] multiRefreshOnFinish = (this.MultiRefreshOnFinish == null) ? null : ((IRefreshable[])this.MultiRefreshOnFinish.Clone());
			if (this.ConfirmOperation(inputArgs))
			{
				WorkUnitCollection workUnits = inputArgs.WorkUnits;
				if (workUnits.Count == 0)
				{
					WorkUnit workUnit = new WorkUnit();
					workUnit.Text = commandDisplayName;
					workUnit.Target = null;
					workUnits.Add(workUnit);
				}
				IProgress progress = this.CreateProgress(new LocalizedString(commandDisplayName));
				MonadCommand command = new LoggableMonadCommand();
				command.CommandText = this.CommandText;
				foreach (object obj in this.Parameters)
				{
					MonadParameter value = (MonadParameter)obj;
					command.Parameters.Add(value);
				}
				command.ProgressReport += delegate(object sender, ProgressReportEventArgs progressReportEventArgs)
				{
					progress.ReportProgress(workUnits.ProgressValue, workUnits.MaxProgressValue, progressReportEventArgs.ProgressRecord.StatusDescription);
				};
				BackgroundWorker worker = new BackgroundWorker();
				worker.DoWork += delegate(object param0, DoWorkEventArgs param1)
				{
					MonadConnection connection = new MonadConnection("timeout=30", new WinFormsCommandInteractionHandler(this.TestUIService ?? uiService), ADServerSettingsSingleton.GetInstance().CreateRunspaceServerSettingsObject(), PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo());
					command.Connection = connection;
					using (new OpenConnection(connection))
					{
						command.Execute(workUnits.ToArray());
					}
				};
				worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
				{
					command.Connection.Close();
					if (runWorkerCompletedEventArgs.Error != null)
					{
						progress.ReportProgress(0, 0, "");
						uiService.ShowError(runWorkerCompletedEventArgs.Error);
					}
					else
					{
						int num = workUnits.HasFailures ? 0 : 100;
						progress.ReportProgress(num, num, "");
						List<WorkUnit> list = new List<WorkUnit>(workUnits.FindByErrorOrWarning());
						if (workUnits.Cancelled)
						{
							WorkUnit workUnit2 = list[list.Count - 1];
							for (int i = 0; i < workUnit2.Errors.Count; i++)
							{
								if (workUnit2.Errors[i].Exception is PipelineStoppedException)
								{
									workUnit2.Errors.Remove(workUnit2.Errors[i]);
									break;
								}
							}
							if (workUnit2.Errors.Count == 0)
							{
								list.Remove(workUnit2);
							}
						}
						if (list.Count > 0)
						{
							string errorMessage = null;
							string warningMessage = null;
							if (list.Count == 1)
							{
								if (this.SingleSelectionError != null)
								{
									errorMessage = this.SingleSelectionError(list[0].Text);
								}
								else
								{
									errorMessage = Strings.SingleSelectionError(commandDisplayName, list[0].Text);
								}
								if (this.SingleSelectionWarning != null)
								{
									warningMessage = this.SingleSelectionWarning(list[0].Text);
								}
								else
								{
									warningMessage = Strings.SingleSelectionWarning(commandDisplayName, list[0].Text);
								}
							}
							else if (list.Count > 1)
							{
								if (this.MultipleSelectionError != null)
								{
									errorMessage = this.MultipleSelectionError(list.Count);
								}
								else
								{
									errorMessage = Strings.MultipleSelectionError(commandDisplayName, list.Count);
								}
								if (this.MultipleSelectionWarning != null)
								{
									warningMessage = this.MultipleSelectionWarning(list.Count);
								}
								else
								{
									warningMessage = Strings.MultipleSelectionWarning(commandDisplayName, list.Count);
								}
							}
							UIService.ShowError(errorMessage, warningMessage, list, uiService);
						}
					}
					this.PerformRefreshOnFinish(workUnits, singleRefreshOnFinish, multiRefreshOnFinish);
					this.OnCompleted(inputArgs);
				};
				bool flag = workUnits.Count > 1;
				ProgressDialog pd = null;
				if (flag)
				{
					pd = new ProgressDialog();
					pd.OkEnabled = false;
					pd.Text = Strings.TaskProgressDialogTitle(commandDisplayName);
					pd.UseMarquee = true;
					pd.StatusText = workUnits.Description;
					pd.FormClosing += delegate(object sender, FormClosingEventArgs formClosingEventArgs)
					{
						if (worker.IsBusy)
						{
							if (pd.CancelEnabled)
							{
								pd.CancelEnabled = false;
								WinformsHelper.InvokeAsync(delegate
								{
									command.Cancel();
								}, pd);
							}
							formClosingEventArgs.Cancel = worker.IsBusy;
						}
					};
					pd.FormClosed += delegate(object param0, FormClosedEventArgs param1)
					{
						if (controlToRestoreFocus != null)
						{
							controlToRestoreFocus.Focus();
						}
					};
					worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
					{
						pd.UseMarquee = false;
						pd.Maximum = 100;
						pd.Value = 100;
						pd.Close();
					};
					command.ProgressReport += delegate(object sender, ProgressReportEventArgs progressReportEventArgs)
					{
						if ((progressReportEventArgs.ProgressRecord.RecordType == ProgressRecordType.Processing && progressReportEventArgs.ProgressRecord.PercentComplete > 0 && progressReportEventArgs.ProgressRecord.PercentComplete < 100) || workUnits[0].Status == WorkUnitStatus.Completed)
						{
							pd.UseMarquee = false;
						}
						pd.Maximum = workUnits.MaxProgressValue;
						pd.Value = workUnits.ProgressValue;
						pd.StatusText = workUnits.Description;
					};
					pd.ShowModeless(uiService.GetDialogOwnerWindow() as IServiceProvider);
					uiService = pd.ShellUI;
				}
				SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
				worker.RunWorkerAsync();
				base.OnExecute();
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00038F08 File Offset: 0x00037108
		private void PerformRefreshOnFinish(WorkUnitCollection workUnits, IRefreshable singleRefresh, IRefreshable[] multiRefresh)
		{
			if (workUnits.IsDataChanged)
			{
				if (singleRefresh != null)
				{
					singleRefresh.Refresh(this.CreateProgress(base.Command.Text));
				}
				if (multiRefresh != null)
				{
					MultiRefreshableSource multiRefreshableSource = new MultiRefreshableSource();
					for (int i = 0; i < workUnits.Count; i++)
					{
						if (workUnits[i].Status != WorkUnitStatus.NotStarted)
						{
							multiRefreshableSource.RefreshableSources.Add(multiRefresh[i]);
						}
					}
					multiRefreshableSource.Refresh(this.CreateProgress(base.Command.Text));
				}
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000ECF RID: 3791 RVA: 0x00038F84 File Offset: 0x00037184
		// (remove) Token: 0x06000ED0 RID: 3792 RVA: 0x00038FBC File Offset: 0x000371BC
		public event EventHandler<WorkUnitCollectionEventArgs> InputRequested;

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00038FF1 File Offset: 0x000371F1
		protected virtual void OnInputRequested(WorkUnitCollectionEventArgs e)
		{
			if (this.InputRequested != null)
			{
				this.InputRequested(this, e);
			}
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000ED2 RID: 3794 RVA: 0x00039008 File Offset: 0x00037208
		// (remove) Token: 0x06000ED3 RID: 3795 RVA: 0x00039040 File Offset: 0x00037240
		public event EventHandler<WorkUnitCollectionEventArgs> Completed;

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00039075 File Offset: 0x00037275
		protected virtual void OnCompleted(WorkUnitCollectionEventArgs e)
		{
			if (this.Completed != null)
			{
				this.Completed(this, e);
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00039094 File Offset: 0x00037294
		public override bool HasPermission()
		{
			return EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope(this.CommandText, (from MonadParameter c in this.Parameters
			select c.ParameterName).ToArray<string>());
		}

		// Token: 0x040005E8 RID: 1512
		private SingleSelectionMessageDelegate singleSelectionConfirmationDelegate;

		// Token: 0x040005E9 RID: 1513
		private MultipleSelectionMessageDelegate multipleSelectionConfirmationDelegate;

		// Token: 0x040005EA RID: 1514
		private SingleSelectionMessageDelegate singleSelectionErrorDelegate;

		// Token: 0x040005EB RID: 1515
		private MultipleSelectionMessageDelegate multipleSelectionErrorDelegate;

		// Token: 0x040005EC RID: 1516
		private SingleSelectionMessageDelegate singleSelectionWarningDelegate;

		// Token: 0x040005ED RID: 1517
		private MultipleSelectionMessageDelegate multipleSelectionWarningDelegate;

		// Token: 0x040005EE RID: 1518
		private string commandText = string.Empty;

		// Token: 0x040005EF RID: 1519
		private MonadParameterCollection parameters = new MonadParameterCollection();

		// Token: 0x040005F0 RID: 1520
		private IRefreshable refreshOnFinish;

		// Token: 0x040005F1 RID: 1521
		private IRefreshable[] multiRefreshOnFinish;
	}
}
