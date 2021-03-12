using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200013A RID: 314
	internal partial class InvisibleForm : ExchangeForm
	{
		// Token: 0x06000C61 RID: 3169 RVA: 0x0002C844 File Offset: 0x0002AA44
		public InvisibleForm()
		{
			this.AutoSize = true;
			base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.ShowInTaskbar = false;
			base.ShowIcon = false;
			base.MinimizeBox = false;
			base.MinimizeBox = false;
			base.ControlBox = false;
			base.Size = new Size(0, 0);
			base.Opacity = 0.0;
			this.backgroundWorker = new BackgroundWorker();
			this.backgroundWorker.RunWorkerCompleted += this.backgroundWorker_RunWorkerCompleted;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002C8C6 File Offset: 0x0002AAC6
		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.runWorkerCompletedEventArgs = e;
			base.Close();
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002C8D8 File Offset: 0x0002AAD8
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

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0002C90F File Offset: 0x0002AB0F
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

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0002C94A File Offset: 0x0002AB4A
		public BackgroundWorker BackgroundWorker
		{
			get
			{
				return this.backgroundWorker;
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002C952 File Offset: 0x0002AB52
		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel = this.backgroundWorker.IsBusy;
			base.OnClosing(e);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002C96C File Offset: 0x0002AB6C
		protected override void OnLoad(EventArgs e)
		{
			SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
			this.backgroundWorker.RunWorkerAsync();
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0002C983 File Offset: 0x0002AB83
		protected override string DefaultHelpTopic
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x04000509 RID: 1289
		private RunWorkerCompletedEventArgs runWorkerCompletedEventArgs;
	}
}
