using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.CmdletInfra;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000289 RID: 649
	internal class RbacModule : TaskIOPipelineBase, ITaskModule
	{
		// Token: 0x0600165C RID: 5724 RVA: 0x00054751 File Offset: 0x00052951
		public RbacModule(TaskContext context)
		{
			this.context = context;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00054760 File Offset: 0x00052960
		public void Init(ITaskEvent task)
		{
			this.CheckVerboseDebugParameter();
			if (this.context.CommandShell != null && (this.verboseDisabled || this.debugDisabled))
			{
				this.context.CommandShell.PrependTaskIOPipelineHandler(this);
			}
			task.PreInit += this.ExtractRbacDataFromRunspace;
			task.PreIterate += this.SetScopeSet;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x000547C5 File Offset: 0x000529C5
		public void Dispose()
		{
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000547C7 File Offset: 0x000529C7
		public override bool WriteVerbose(LocalizedString input, out LocalizedString output)
		{
			output = input;
			return !this.verboseDisabled;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000547D9 File Offset: 0x000529D9
		public override bool WriteDebug(LocalizedString input, out LocalizedString output)
		{
			output = input;
			return !this.debugDisabled;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000547EC File Offset: 0x000529EC
		private void ExtractRbacDataFromRunspace(object sender, EventArgs e)
		{
			ADObjectId adobjectId = null;
			OrganizationId organizationId = null;
			ExchangePropertyContainer.InitializeExchangePropertyContainerIfNeeded(this.context.SessionState, out adobjectId, out organizationId);
			ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = null;
			if (this.context.SessionState != null)
			{
				exchangeRunspaceConfiguration = ExchangePropertyContainer.GetExchangeRunspaceConfiguration(this.context.SessionState);
			}
			OrganizationId organizationId2;
			string executingUserIdentityName;
			SmtpAddress executingWindowsLiveId;
			if (exchangeRunspaceConfiguration == null)
			{
				if (this.context.SessionState != null)
				{
					organizationId = (organizationId ?? ExchangePropertyContainer.GetExecutingUserOrganizationId(this.context.SessionState));
					adobjectId = (adobjectId ?? ExchangePropertyContainer.GetExecutingUserId(this.context.SessionState));
				}
				organizationId2 = organizationId;
				executingUserIdentityName = ((adobjectId == null) ? string.Empty : adobjectId.Name);
			}
			else
			{
				exchangeRunspaceConfiguration.TryGetExecutingUserId(out adobjectId);
				executingUserIdentityName = exchangeRunspaceConfiguration.IdentityName;
				exchangeRunspaceConfiguration.TryGetExecutingWindowsLiveId(out executingWindowsLiveId);
				organizationId = (exchangeRunspaceConfiguration.PartnerMode ? OrganizationId.ForestWideOrgId : exchangeRunspaceConfiguration.OrganizationId);
				organizationId2 = exchangeRunspaceConfiguration.OrganizationId;
				SecurityIdentifier value;
				exchangeRunspaceConfiguration.TryGetExecutingUserSid(out value);
				CmdletLogger.SafeSetLogger(this.context.UniqueId, RpsCmdletMetadata.ExecutingUserSid, value);
			}
			if (this.context.InvocationInfo != null && this.context.InvocationInfo.IsVerboseOn && !TaskLogger.IsSetupLogging)
			{
				string executingUserId = (adobjectId != null) ? adobjectId.ToCanonicalName() : string.Empty;
				string executingUserOrganizationId = (organizationId != null) ? organizationId.ToString() : string.Empty;
				string currentOrganizationId = (organizationId2 != null) ? organizationId2.ToString() : string.Empty;
				if (this.context.CommandShell != null)
				{
					this.context.CommandShell.WriteVerbose(Strings.VerboseExecutingUserContext(executingUserId, executingUserOrganizationId, currentOrganizationId, (exchangeRunspaceConfiguration == null) ? Strings.DisabledString : Strings.EnabledString));
				}
			}
			this.context.ExchangeRunspaceConfig = exchangeRunspaceConfiguration;
			this.context.UserInfo = new TaskUserInfo(organizationId, organizationId2, adobjectId, executingUserIdentityName, executingWindowsLiveId);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000549C8 File Offset: 0x00052BC8
		private void SetScopeSet(object sender, EventArgs e)
		{
			if (!this.context.CanBypassRBACScope)
			{
				string exchangeCmdletName = "";
				string[] array = null;
				if (this.context.InvocationInfo != null)
				{
					exchangeCmdletName = this.context.InvocationInfo.CommandName;
					array = this.FilterVerboseDebugParameter(this.context.InvocationInfo.UserSpecifiedParameters.Keys);
				}
				if (this.context.ExchangeRunspaceConfig != null && this.context.ExchangeRunspaceConfig.ConfigurationSettings.IsProxy)
				{
					array = (from val in array
					where !ClientRoleEntries.ParametersForProxy.Contains(val)
					select val).ToArray<string>();
				}
				if (this.context.ExchangeRunspaceConfig != null && this.context.UserInfo != null && this.context.CommandShell != null)
				{
					this.context.ScopeSet = this.context.ExchangeRunspaceConfig.CalculateScopeSetForExchangeCmdlet(exchangeCmdletName, array, this.context.UserInfo.CurrentOrganizationId, new Task.ErrorLoggerDelegate(this.context.CommandShell.WriteError));
				}
				if (this.context.InvocationInfo != null && this.context.InvocationInfo.IsVerboseOn && !TaskLogger.IsSetupLogging)
				{
					this.context.CommandShell.WriteVerbose(TaskVerboseStringHelper.GetScopeSetVerboseString(this.context.ScopeSet));
				}
			}
			this.CheckForExpiredSession();
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00054B2C File Offset: 0x00052D2C
		private void CheckForExpiredSession()
		{
			IExpiringRunspaceConfiguration expiringRunspaceConfiguration = this.context.ExchangeRunspaceConfig as IExpiringRunspaceConfiguration;
			if (expiringRunspaceConfiguration != null && expiringRunspaceConfiguration.ShouldCloseDueToExpiration())
			{
				TaskLogger.LogEvent(TaskEventLogConstants.Tuple_PowershellSessionExpired, this.context.InvocationInfo, this.context.ExchangeRunspaceConfig.IdentityName, new object[0]);
				if (this.context.CommandShell != null)
				{
					this.context.CommandShell.WriteError(new SessionExpiredException(), ExchangeErrorCategory.ServerTransient, null, false);
					this.context.CommandShell.SetShouldExit(-1);
				}
			}
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00054BBC File Offset: 0x00052DBC
		private string[] FilterVerboseDebugParameter(ICollection parameterNames)
		{
			List<string> list = new List<string>(parameterNames.Count);
			foreach (object obj in parameterNames)
			{
				string text = (string)obj;
				if ((!this.verboseDisabled || !(text == "Verbose")) && (!this.debugDisabled || !(text == "Debug")))
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00054C4C File Offset: 0x00052E4C
		private void CheckVerboseDebugParameter()
		{
			if (this.context.SessionState != null && ExchangePropertyContainer.IsContainerInitialized(this.context.SessionState))
			{
				ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = ExchangePropertyContainer.GetExchangeRunspaceConfiguration(this.context.SessionState);
				if (exchangeRunspaceConfiguration != null && this.context.InvocationInfo != null)
				{
					if (!exchangeRunspaceConfiguration.IsVerboseEnabled(this.context.InvocationInfo.CommandName))
					{
						this.verboseDisabled = true;
						TaskLogger.Trace("The cmdlet does not have Verbose parameter by RBAC check. WriteVerbose is disabled.", new object[0]);
					}
					if (!exchangeRunspaceConfiguration.IsDebugEnabled(this.context.InvocationInfo.CommandName))
					{
						this.debugDisabled = true;
						TaskLogger.Trace("The cmdlet does not have Debug parameter by RBAC check. WriteDebug is disabled.", new object[0]);
					}
				}
			}
		}

		// Token: 0x040006D6 RID: 1750
		private readonly TaskContext context;

		// Token: 0x040006D7 RID: 1751
		private bool verboseDisabled;

		// Token: 0x040006D8 RID: 1752
		private bool debugDisabled;
	}
}
