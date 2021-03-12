using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000195 RID: 405
	internal partial class BackgroundWorkerProgressDialog : ProgressDialog
	{
		// Token: 0x06001036 RID: 4150 RVA: 0x0003FBBC File Offset: 0x0003DDBC
		public BackgroundWorkerProgressDialog()
		{
			this.backgroundWorker = new BackgroundWorker();
			this.backgroundWorker.DoWork += this.backgroundWorker_DoWork;
			this.backgroundWorker.RunWorkerCompleted += this.backgroundWorker_RunWorkerCompleted;
			base.UseMarquee = true;
		}

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06001037 RID: 4151 RVA: 0x0003FC10 File Offset: 0x0003DE10
		// (remove) Token: 0x06001038 RID: 4152 RVA: 0x0003FC48 File Offset: 0x0003DE48
		public event DoWorkEventHandler DoWork;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06001039 RID: 4153 RVA: 0x0003FC80 File Offset: 0x0003DE80
		// (remove) Token: 0x0600103A RID: 4154 RVA: 0x0003FCB8 File Offset: 0x0003DEB8
		public event RunWorkerCompletedEventHandler RunWorkerCompleted;

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0003FCED File Offset: 0x0003DEED
		public bool IsBusy
		{
			get
			{
				return this.backgroundWorker.IsBusy;
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0003FCFC File Offset: 0x0003DEFC
		public void ReportProgress(int percentProgress, string statusText)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new BackgroundWorkerProgressDialog.ReportProgressDelegate(this.ReportProgress), new object[]
				{
					percentProgress,
					statusText
				});
				return;
			}
			base.UseMarquee = false;
			base.Value = percentProgress;
			base.StatusText = statusText;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0003FD4F File Offset: 0x0003DF4F
		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			base.UseMarquee = false;
			base.Maximum = 100;
			base.Value = 100;
			this.runWorkerCompletedEventArgs = e;
			if (this.RunWorkerCompleted != null)
			{
				this.RunWorkerCompleted(this, e);
			}
			base.Close();
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0003FD8A File Offset: 0x0003DF8A
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.DoWork != null)
			{
				this.DoWork(this, e);
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0003FDA4 File Offset: 0x0003DFA4
		public bool ShowErrors(string errorMessage, string warningMessage, WorkUnitCollection workUnits, IUIService uiService)
		{
			Exception error = this.runWorkerCompletedEventArgs.Error;
			if (error != null)
			{
				uiService.ShowError(error);
				return true;
			}
			IList<WorkUnit> errors = workUnits.FindByErrorOrWarning();
			return UIService.ShowError(errorMessage, warningMessage, errors, uiService);
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0003FDDB File Offset: 0x0003DFDB
		public object AsyncResults
		{
			get
			{
				if (this.runWorkerCompletedEventArgs != null && this.runWorkerCompletedEventArgs.Error == null)
				{
					return this.runWorkerCompletedEventArgs.Result;
				}
				return null;
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0003FE16 File Offset: 0x0003E016
		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel = this.backgroundWorker.IsBusy;
			base.OnClosing(e);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0003FE30 File Offset: 0x0003E030
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
			this.backgroundWorker.RunWorkerAsync();
		}

		// Token: 0x04000657 RID: 1623
		private RunWorkerCompletedEventArgs runWorkerCompletedEventArgs;

		// Token: 0x02000196 RID: 406
		// (Invoke) Token: 0x06001045 RID: 4165
		private delegate void ReportProgressDelegate(int percentProgress, string statusText);
	}
}
