using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.CmdletInfra;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200028B RID: 651
	internal class RunspaceServerSettingsFinalizeModule : ITaskModule, ICriticalFeature
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x00054DEA File Offset: 0x00052FEA
		public RunspaceServerSettingsFinalizeModule(TaskContext context)
		{
			this.context = context;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00054DF9 File Offset: 0x00052FF9
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00054DFC File Offset: 0x00052FFC
		public void Init(ITaskEvent task)
		{
			task.Error += this.OnError;
			task.PreRelease += this.PreRelease;
			task.Release += this.Finalize;
			task.Stop += this.Finalize;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00054E51 File Offset: 0x00053051
		public void Dispose()
		{
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00054E53 File Offset: 0x00053053
		private void OnError(object sender, GenericEventArg<TaskErrorEventArg> e)
		{
			if (e.Data.ExceptionHandled)
			{
				return;
			}
			this.MarkDcDownIfNecessary(e);
			this.TryFailOver();
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00054E70 File Offset: 0x00053070
		private void Finalize(object sender, EventArgs e)
		{
			this.TryFailOver();
			ADSessionSettings.ClearThreadADContext();
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00054E80 File Offset: 0x00053080
		private void PreRelease(object sender, EventArgs e)
		{
			ADDriverContext threadADContext = ADSessionSettings.GetThreadADContext();
			ADServerSettings serverSettings = (threadADContext != null) ? threadADContext.ServerSettings : null;
			CmdletLogHelper.LogADServerSettingsAfterCmdExecuted(this.context.UniqueId, serverSettings);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00054EB4 File Offset: 0x000530B4
		private void MarkDcDownIfNecessary(GenericEventArg<TaskErrorEventArg> e)
		{
			if (e.Data.ExceptionHandled)
			{
				return;
			}
			ADDriverContext threadADContext = ADSessionSettings.GetThreadADContext();
			ADServerSettings adserverSettings = (threadADContext != null) ? threadADContext.ServerSettings : null;
			if (this.context == null || adserverSettings == null)
			{
				return;
			}
			string text = null;
			for (Exception ex = e.Data.Exception; ex != null; ex = ex.InnerException)
			{
				if (ex is SuitabilityDirectoryException)
				{
					text = ((SuitabilityDirectoryException)ex).Fqdn;
					break;
				}
				if (ex is ServerInMMException)
				{
					text = ((ServerInMMException)ex).Fqdn;
					break;
				}
				if (ex is ADServerSettingsChangedException)
				{
					ADServerSettings serverSettings = ((ADServerSettingsChangedException)ex).ServerSettings;
					this.PersistNewServerSettings(serverSettings);
					break;
				}
				if (ex == ex.InnerException)
				{
					break;
				}
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				return;
			}
			Fqdn fqdn;
			if (Fqdn.TryParse(text, out fqdn))
			{
				try
				{
					adserverSettings.MarkDcDown(fqdn);
					CmdletLogger.SafeAppendGenericInfo(this.context.UniqueId, "MarkDcDown", fqdn);
					return;
				}
				catch (NotSupportedException)
				{
					CmdletLogger.SafeAppendGenericInfo(this.context.UniqueId, "MarkDcDown-NotSupportedException", fqdn);
					return;
				}
			}
			CmdletLogger.SafeAppendGenericInfo(this.context.UniqueId, "MarkDcDown-InvalidFqdn", fqdn);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00054FE0 File Offset: 0x000531E0
		private void TryFailOver()
		{
			ADDriverContext threadADContext = ADSessionSettings.GetThreadADContext();
			ADServerSettings adserverSettings = (threadADContext != null) ? threadADContext.ServerSettings : null;
			if (adserverSettings != null && adserverSettings.IsFailoverRequired())
			{
				ADServerSettings newServerSettings;
				string str;
				if (adserverSettings.TryFailover(out newServerSettings, out str, false))
				{
					this.PersistNewServerSettings(newServerSettings);
					return;
				}
				CmdletLogger.SafeAppendGenericInfo(this.context.UniqueId, "ADServerSettings-TryFailOver", "Failed - " + str);
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00055040 File Offset: 0x00053240
		private void PersistNewServerSettings(ADServerSettings newServerSettings)
		{
			if (newServerSettings != null)
			{
				ADSessionSettings.ClearThreadADContext();
				LocalizedString adserverSettings;
				if (CmdletLogHelper.NeedConvertLogMessageToUS)
				{
					adserverSettings = TaskVerboseStringHelper.GetADServerSettings(this.context.InvocationInfo.CommandName, newServerSettings, CmdletLogHelper.DefaultLoggingCulture);
				}
				else
				{
					adserverSettings = TaskVerboseStringHelper.GetADServerSettings(this.context.InvocationInfo.CommandName, newServerSettings);
				}
				if (this.context.SessionState != null)
				{
					CmdletLogger.SafeAppendGenericInfo(this.context.UniqueId, "ADServerSettings-FailOver", adserverSettings);
					ExchangePropertyContainer.SetServerSettings(this.context.SessionState, newServerSettings);
				}
				else
				{
					CmdletLogger.SafeAppendGenericInfo(this.context.UniqueId, "ADServerSettings-NOTFailOver-SessionStateNull", adserverSettings);
				}
				ADSessionSettings.SetThreadADContext(new ADDriverContext(newServerSettings, ContextMode.Cmdlet));
				this.context.ServerSettingsAfterFailOver = newServerSettings;
				this.context.CommandShell.WriteWarning(DirectoryStrings.RunspaceServerSettingsChanged);
				return;
			}
			CmdletLogger.SafeAppendGenericInfo(this.context.UniqueId, "ADServerSettings-NOTFailOver-ServerSettingsNull", "NULL");
		}

		// Token: 0x040006DB RID: 1755
		private readonly TaskContext context;
	}
}
