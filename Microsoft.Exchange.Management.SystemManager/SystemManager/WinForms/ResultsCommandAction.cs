using System;
using System.Threading;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000202 RID: 514
	public abstract class ResultsCommandAction : CommandAction
	{
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x00062BB3 File Offset: 0x00060DB3
		public ResultsCommandProfile Profile
		{
			get
			{
				return base.Profile as ResultsCommandProfile;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x00062BC0 File Offset: 0x00060DC0
		public ResultPane ResultPane
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.ResultPane;
				}
				return null;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00062BD7 File Offset: 0x00060DD7
		public ResultsCommandSetting Setting
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.Setting;
				}
				return null;
			}
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00062BF0 File Offset: 0x00060DF0
		protected IRefreshable GetDefaultRefreshObject()
		{
			if (this.Setting == null || this.ResultPane == null)
			{
				return null;
			}
			if (this.Setting.IsSelectionCommand && this.ResultPane.HasSelection && this.Setting.UseSingleRowRefresh)
			{
				return this.ResultPane.GetSelectionRefreshObjects();
			}
			if (this.Setting.UseFullRefresh)
			{
				return this.ResultPane.RefreshableDataSource;
			}
			return null;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00062C5C File Offset: 0x00060E5C
		protected void RefreshResultsThreadSafely(DataContext context)
		{
			this.InvokeRefreshResults(context);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00062C68 File Offset: 0x00060E68
		private void InvokeRefreshResults(object obj)
		{
			if (this.ResultPane.IsHandleCreated)
			{
				if (this.ResultPane.InvokeRequired)
				{
					this.ResultPane.BeginInvoke(new SendOrPostCallback(this.InvokeRefreshResults), new object[]
					{
						obj
					});
					return;
				}
				this.RefreshResults((DataContext)obj);
			}
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00062CC0 File Offset: 0x00060EC0
		protected virtual void RefreshResults(DataContext context)
		{
			if (context == null || context.RefreshOnSave == null)
			{
				this.ResultPane.SetRefreshWhenActivated();
				return;
			}
			context.RefreshOnSave.Refresh(this.ResultPane.CreateProgress(this.ResultPane.RefreshCommand.Text));
		}
	}
}
