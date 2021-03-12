using System;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Tasks;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000278 RID: 632
	internal class RunspaceServerSettingsInitModule : ITaskModule
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x000515A0 File Offset: 0x0004F7A0
		// (set) Token: 0x060015CD RID: 5581 RVA: 0x000515A8 File Offset: 0x0004F7A8
		private protected TaskContext CurrentTaskContext { protected get; private set; }

		// Token: 0x060015CE RID: 5582 RVA: 0x000515B1 File Offset: 0x0004F7B1
		public RunspaceServerSettingsInitModule(TaskContext context)
		{
			this.CurrentTaskContext = context;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000515C0 File Offset: 0x0004F7C0
		public void Init(ITaskEvent task)
		{
			task.PreInit += this.InitializeRunspaceServerSettings;
			task.PreIterate += this.SetScopeSet;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000515E6 File Offset: 0x0004F7E6
		public void Dispose()
		{
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000515E8 File Offset: 0x0004F7E8
		protected virtual ADServerSettings GetCmdletADServerSettings()
		{
			return null;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000515EC File Offset: 0x0004F7EC
		private void SetScopeSet(object sender, EventArgs e)
		{
			if (this.CurrentTaskContext.CanBypassRBACScope)
			{
				if (this.CurrentTaskContext.UserInfo != null)
				{
					if (this.CurrentTaskContext.UserInfo.CurrentOrganizationId != null)
					{
						this.CurrentTaskContext.ScopeSet = ScopeSet.GetOrgWideDefaultScopeSet(this.CurrentTaskContext.UserInfo.CurrentOrganizationId);
					}
					else
					{
						this.CurrentTaskContext.ScopeSet = ScopeSet.GetOrgWideDefaultScopeSet(this.CurrentTaskContext.UserInfo.ExecutingUserOrganizationId);
					}
				}
				if (this.CurrentTaskContext.InvocationInfo != null && this.CurrentTaskContext.InvocationInfo.IsVerboseOn && !TaskLogger.IsSetupLogging)
				{
					this.CurrentTaskContext.CommandShell.WriteVerbose(TaskVerboseStringHelper.GetScopeSetVerboseString(this.CurrentTaskContext.ScopeSet));
				}
			}
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000516B8 File Offset: 0x0004F8B8
		protected ADServerSettings CreateADServerSettingsForOrganization(bool useDCInAnySite = false)
		{
			ISessionState sessionState = this.CurrentTaskContext.SessionState;
			bool flag = false;
			if (sessionState != null)
			{
				flag = ExchangePropertyContainer.IsContainerInitialized(sessionState);
			}
			OrganizationId orgId = null;
			if (this.CurrentTaskContext.InvocationInfo != null && this.CurrentTaskContext.InvocationInfo.IsCmdletInvokedWithoutPSFramework && this.CurrentTaskContext.UserInfo != null)
			{
				orgId = this.CurrentTaskContext.UserInfo.CurrentOrganizationId;
			}
			else if (flag)
			{
				ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = ExchangePropertyContainer.GetExchangeRunspaceConfiguration(sessionState);
				if (exchangeRunspaceConfiguration != null)
				{
					orgId = exchangeRunspaceConfiguration.OrganizationId;
				}
			}
			return this.CreateServerSettings(orgId, useDCInAnySite);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0005173C File Offset: 0x0004F93C
		protected ADServerSettings CreateADServerSettingsForUserWithForestWideAffnity()
		{
			if (this.CurrentTaskContext.InvocationInfo != null && this.CurrentTaskContext.InvocationInfo.IsCmdletInvokedWithoutPSFramework && this.CurrentTaskContext.UserInfo != null)
			{
				return this.CreateServerSettingsForUserWithForestWideAffinity(this.CurrentTaskContext.UserInfo.ExecutingUserIdentityName, this.CurrentTaskContext.UserInfo.CurrentOrganizationId);
			}
			return null;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000517A0 File Offset: 0x0004F9A0
		private void InitializeRunspaceServerSettings(object sender, EventArgs e)
		{
			ISessionState sessionState = this.CurrentTaskContext.SessionState;
			ADServerSettings adserverSettings = null;
			bool flag = false;
			bool flag2 = false;
			string value = null;
			if (sessionState != null)
			{
				flag2 = ExchangePropertyContainer.IsContainerInitialized(sessionState);
				if (flag2)
				{
					adserverSettings = ExchangePropertyContainer.GetServerSettings(sessionState);
					if (adserverSettings != null)
					{
						value = "SessionState";
					}
				}
			}
			if (adserverSettings == null)
			{
				adserverSettings = ADSessionSettings.GetProcessServerSettings();
				if (adserverSettings != null)
				{
					value = "ProcessServerSettings";
				}
			}
			if (adserverSettings == null)
			{
				if (this.CurrentTaskContext.CommandShell != null)
				{
					this.CurrentTaskContext.CommandShell.TryGetVariableValue<ADServerSettings>(ExchangePropertyContainer.ADServerSettingsVarName, out adserverSettings);
				}
				flag = (adserverSettings != null);
				if (adserverSettings != null)
				{
					value = "CommandShell";
				}
			}
			if (TopologyProvider.CurrentTopologyMode == TopologyMode.ADTopologyService)
			{
				ADServerSettings cmdletADServerSettings = this.GetCmdletADServerSettings();
				if (cmdletADServerSettings != null)
				{
					this.CurrentTaskContext.Items["CmdletServerSettings"] = cmdletADServerSettings;
					value = "ADTopologyService";
					adserverSettings = cmdletADServerSettings;
				}
			}
			if (adserverSettings == null)
			{
				flag = true;
				if (TopologyProvider.CurrentTopologyMode == TopologyMode.Adam)
				{
					if (this.CurrentTaskContext.InvocationInfo != null && this.CurrentTaskContext.InvocationInfo.IsVerboseOn)
					{
						this.CurrentTaskContext.CommandShell.WriteVerbose(Strings.VerboseInitializeRunspaceServerSettingsAdam);
					}
					if (Globals.InstanceType == InstanceType.NotInitialized)
					{
						Globals.InitializeSinglePerfCounterInstance();
					}
					value = "Adam-SimpleServerSettings";
					adserverSettings = new SimpleServerSettings();
				}
				else if (TopologyProvider.CurrentTopologyMode == TopologyMode.Ldap)
				{
					if (this.CurrentTaskContext.InvocationInfo != null && this.CurrentTaskContext.CommandShell != null && this.CurrentTaskContext.InvocationInfo.IsVerboseOn)
					{
						this.CurrentTaskContext.CommandShell.WriteVerbose(Strings.VerboseInitializeRunspaceServerSettingsLocal);
					}
					Globals.InitializeMultiPerfCounterInstance("EMS");
					value = "Ldap-LocalCmdLineServerSettings";
					adserverSettings = LocalCmdLineServerSettings.CreateLocalCmdLineServerSettings();
				}
				else
				{
					if (this.CurrentTaskContext.InvocationInfo != null && this.CurrentTaskContext.InvocationInfo.IsVerboseOn)
					{
						this.CurrentTaskContext.CommandShell.WriteVerbose(Strings.VerboseInitializeRunspaceServerSettingsRemote);
					}
					adserverSettings = this.CreateADServerSettingsForOrganization(false);
					if (adserverSettings == null)
					{
						value = "GCRandomly";
						adserverSettings = RunspaceServerSettings.CreateRunspaceServerSettings(false);
					}
				}
			}
			if (flag)
			{
				if (flag2)
				{
					ExchangePropertyContainer.SetServerSettings(sessionState, adserverSettings);
				}
				else
				{
					sessionState.Variables[ExchangePropertyContainer.ADServerSettingsVarName] = adserverSettings;
				}
			}
			ADSessionSettings.SetThreadADContext(new ADDriverContext(adserverSettings, ContextMode.Cmdlet));
			LocalizedString localizedString = LocalizedString.Empty;
			if (this.CurrentTaskContext.InvocationInfo != null)
			{
				localizedString = TaskVerboseStringHelper.GetADServerSettings(this.CurrentTaskContext.InvocationInfo.CommandName, adserverSettings);
				if (this.CurrentTaskContext.InvocationInfo.IsVerboseOn && !string.Equals(this.CurrentTaskContext.InvocationInfo.CommandName, "Write-ExchangeSetupLog", StringComparison.OrdinalIgnoreCase) && this.CurrentTaskContext.CommandShell != null)
				{
					this.CurrentTaskContext.CommandShell.WriteVerbose(localizedString);
				}
			}
			CmdletLogger.SafeSetLogger(this.CurrentTaskContext.UniqueId, RpsCmdletMetadata.RunspaceSettingsCreationHint, value);
			CmdletLogHelper.LogADServerSettings(this.CurrentTaskContext.UniqueId, adserverSettings);
			if (ExTraceGlobals.LogTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.LogTracer.Information<LocalizedString>(0L, "Cmdlet ADServerSettings {0}", localizedString);
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00051A62 File Offset: 0x0004FC62
		private ADServerSettings CreateServerSettings(OrganizationId orgId, bool useDCInAnySite)
		{
			if (orgId == null || ADSessionSettings.IsForefrontObject(orgId.PartitionId) || orgId.Equals(OrganizationId.ForestWideOrgId))
			{
				return null;
			}
			return RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(RunspaceServerSettings.GetTokenForOrganization(orgId), orgId.PartitionId.ForestFQDN, useDCInAnySite);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00051AA0 File Offset: 0x0004FCA0
		private ADServerSettings CreateServerSettingsForUserWithForestWideAffinity(string userIdentity, OrganizationId orgId)
		{
			if (string.IsNullOrEmpty(userIdentity) || orgId == null || ADSessionSettings.IsForefrontObject(orgId.PartitionId) || orgId.Equals(OrganizationId.ForestWideOrgId))
			{
				return null;
			}
			return RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(RunspaceServerSettings.GetTokenForUser(userIdentity, orgId), orgId.PartitionId.ForestFQDN, true);
		}

		// Token: 0x040006AE RID: 1710
		public const string CmdletServerSettingsKey = "CmdletServerSettings";
	}
}
