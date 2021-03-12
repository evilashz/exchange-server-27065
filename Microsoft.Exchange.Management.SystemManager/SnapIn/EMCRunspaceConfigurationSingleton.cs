using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000275 RID: 629
	internal class EMCRunspaceConfigurationSingleton : RefreshableComponent
	{
		// Token: 0x06001ADA RID: 6874 RVA: 0x0007678C File Offset: 0x0007498C
		internal void PerformAction(EMCRunspaceConfigurationSingleton.DoActionWhenSucceeded doAction)
		{
			if (this.finished)
			{
				doAction();
			}
			base.RefreshCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
			{
				if (this.finished)
				{
					doAction();
				}
			};
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00076818 File Offset: 0x00074A18
		internal void PerformActionWhenFailed(EMCRunspaceConfigurationSingleton.DoActionWhenFailed doAction)
		{
			if (this.finished && this.exception != null)
			{
				doAction(this.ErrorMessage, this.exception);
			}
			base.RefreshCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
			{
				if (this.finished && this.exception != null)
				{
					doAction(this.ErrorMessage, this.exception);
				}
			};
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000768A4 File Offset: 0x00074AA4
		internal void PerformActionWhenSucceeded(EMCRunspaceConfigurationSingleton.DoActionWhenSucceeded doAction)
		{
			if (this.finished && this.exception == null)
			{
				doAction();
			}
			base.RefreshCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
			{
				if (this.finished && this.exception == null)
				{
					doAction();
				}
			};
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x000768F2 File Offset: 0x00074AF2
		private void ResetStatus()
		{
			this.finished = false;
			this.exception = null;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00076904 File Offset: 0x00074B04
		protected override void OnDoRefreshWork(RefreshRequestEventArgs e)
		{
			this.progress = new RefreshRequestEventArgsToIProgressAdapter(e);
			try
			{
				this.ResetStatus();
				PSConnectionInfoSingleton.GetInstance().ReportProgress = this.progress;
				PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo();
				this.progress.ReportProgress(50, 100, Strings.ProgressReportInitializeHelpService, Strings.ProgressReportInitializeHelpServiceErrorText);
				ExchangeHelpService.Initialize();
				this.erc = CmdletBasedRunspaceConfiguration.Create(PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo(), PSConnectionInfoSingleton.GetInstance().UserAccount, this.progress);
				this.TenantDomain = ((this.erc.LogonUserLiveID == SmtpAddress.Empty) ? null : this.erc.LogonUserLiveID.Domain);
			}
			finally
			{
				this.progress.ReportProgress(100, 100, Strings.ProgressReportEnd, string.Empty);
				PSConnectionInfoSingleton.GetInstance().ReportProgress = null;
			}
			base.OnDoRefreshWork(e);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00076A04 File Offset: 0x00074C04
		protected override void OnRefreshCompleted(RunWorkerCompletedEventArgs e)
		{
			this.finished = true;
			if (e.Error != null)
			{
				this.exception = e.Error;
			}
			base.OnRefreshCompleted(e);
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00076A28 File Offset: 0x00074C28
		// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x00076A30 File Offset: 0x00074C30
		internal string TenantDomain { get; private set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00076A39 File Offset: 0x00074C39
		internal string StepDescription
		{
			get
			{
				if (this.progress == null)
				{
					return string.Empty;
				}
				return this.progress.StepDescription;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00076A54 File Offset: 0x00074C54
		internal string ErrorHeader
		{
			get
			{
				if (this.progress == null)
				{
					return string.Empty;
				}
				return this.progress.ErrorHeader;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x00076A6F File Offset: 0x00074C6F
		[DefaultValue(0)]
		internal int ProgressValue
		{
			get
			{
				if (this.progress == null)
				{
					return 0;
				}
				return this.progress.Value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x00076A86 File Offset: 0x00074C86
		private string ErrorMessage
		{
			get
			{
				if (ExceptionHelper.IsWellknownCommandExecutionException(this.exception))
				{
					return this.exception.InnerException.Message;
				}
				if (this.exception != null)
				{
					return this.exception.Message;
				}
				return null;
			}
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00076ABC File Offset: 0x00074CBC
		public bool IsCmdletAllowedInScope(string cmdletName, string[] paramNames)
		{
			if (this.erc != null)
			{
				ScopeSet scopeSet = new ScopeSet(new ADScope(null, null), new ADScopeCollection[0], null, null);
				return this.erc.IsCmdletAllowedInScope("Microsoft.Exchange.Management.PowerShell.E2010\\" + cmdletName.Trim(), EMCRunspaceConfigurationSingleton.TrimArray(paramNames), scopeSet);
			}
			return true;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00076B12 File Offset: 0x00074D12
		private static string[] TrimArray(string[] paramNames)
		{
			if (paramNames != null)
			{
				return (from c in paramNames
				select c.Trim()).ToArray<string>();
			}
			return null;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00076B41 File Offset: 0x00074D41
		public ICollection<string> GetAllowedParamsForSetCmdlet(string cmdletName, ADRawEntry adObject)
		{
			if (this.erc != null)
			{
				return this.erc.GetAllowedParamsForSetCmdlet("Microsoft.Exchange.Management.PowerShell.E2010\\" + cmdletName.Trim(), null, ScopeLocation.RecipientWrite);
			}
			return null;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00076B6C File Offset: 0x00074D6C
		public bool IsCmdletAllowedInScope(MonadCommand command)
		{
			if (this.erc != null)
			{
				List<string> list = new List<string>(command.Parameters.Count);
				foreach (object obj in command.Parameters)
				{
					MonadParameter monadParameter = (MonadParameter)obj;
					list.Add(monadParameter.ParameterName);
				}
				return this.IsCmdletAllowedInScope(command.CommandText, list.ToArray());
			}
			return true;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00076BF8 File Offset: 0x00074DF8
		public static EMCRunspaceConfigurationSingleton GetInstance()
		{
			return EMCRunspaceConfigurationSingleton.instance;
		}

		// Token: 0x04000A00 RID: 2560
		private const string PSSnapInName = "Microsoft.Exchange.Management.PowerShell.E2010";

		// Token: 0x04000A01 RID: 2561
		private CmdletBasedRunspaceConfiguration erc;

		// Token: 0x04000A02 RID: 2562
		private bool finished;

		// Token: 0x04000A03 RID: 2563
		private Exception exception;

		// Token: 0x04000A04 RID: 2564
		private RefreshRequestEventArgsToIProgressAdapter progress;

		// Token: 0x04000A05 RID: 2565
		private static EMCRunspaceConfigurationSingleton instance = new EMCRunspaceConfigurationSingleton();

		// Token: 0x02000276 RID: 630
		// (Invoke) Token: 0x06001AEF RID: 6895
		internal delegate void DoActionWhenSucceeded();

		// Token: 0x02000277 RID: 631
		// (Invoke) Token: 0x06001AF3 RID: 6899
		internal delegate void DoActionWhenFailed(string errorMessage, Exception exception);
	}
}
