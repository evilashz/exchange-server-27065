using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.Provisioning;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000057 RID: 87
	internal sealed class ExchangePropertyContainer : IRunspaceObserver
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0000D343 File Offset: 0x0000B543
		private ExchangePropertyContainer()
		{
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000D34C File Offset: 0x0000B54C
		public void Activate()
		{
			if (this.budget != null)
			{
				OverBudgetException ex;
				if (this.budget.TryCheckOverBudget(CostType.ActiveRunspace, out ex) && (ex.PolicyPart == "MaxConcurrency" || ex.PolicyPart == "LocalTime"))
				{
					throw ex;
				}
				if (this.activeRunSpaceCostHandle != null)
				{
					this.activeRunSpaceCostHandle.Dispose();
				}
				this.activeRunSpaceCostHandle = this.budget.StartActiveRunspace();
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		public void Deactivate()
		{
			if (this.activeRunSpaceCostHandle != null)
			{
				this.activeRunSpaceCostHandle.Dispose();
				this.activeRunSpaceCostHandle = null;
			}
			if (this.exchangeRunspaceConfiguration.EnablePiiMap && !string.IsNullOrEmpty(this.exchangeRunspaceConfiguration.PiiMapId))
			{
				PiiMapManager.Instance.Remove(this.exchangeRunspaceConfiguration.PiiMapId);
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000D418 File Offset: 0x0000B618
		internal static void InitExchangePropertyContainer(ISessionState sessionState, ExchangeRunspaceConfiguration configuration)
		{
			ExchangePropertyContainer exchangePropertyContainer = new ExchangePropertyContainer();
			exchangePropertyContainer.exchangeRunspaceConfiguration = configuration;
			exchangePropertyContainer.budget = ExchangePropertyContainer.AcquirePowerShellBudget(configuration);
			if (sessionState.Variables.ContainsName(ExchangePropertyContainer.ADServerSettingsVarName))
			{
				exchangePropertyContainer.serverSettings = (sessionState.Variables[ExchangePropertyContainer.ADServerSettingsVarName] as ADServerSettings);
			}
			ExchangePropertyContainer.SetExchangePropertyContainer(sessionState, exchangePropertyContainer);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000D474 File Offset: 0x0000B674
		internal static void InitExchangePropertyContainer(InitialSessionState initialSessionState, ExchangeRunspaceConfiguration configuration)
		{
			ExchangePropertyContainer exchangePropertyContainer = new ExchangePropertyContainer();
			exchangePropertyContainer.logEntries = new CmdletLogEntries();
			exchangePropertyContainer.exchangeRunspaceConfiguration = configuration;
			exchangePropertyContainer.budget = ExchangePropertyContainer.AcquirePowerShellBudget(configuration);
			SessionStateVariableEntry item = new SessionStateVariableEntry(ExchangePropertyContainer.ExchangePropertyContainerName, exchangePropertyContainer, ExchangePropertyContainer.ExchangePropertyContainerName, ScopedItemOptions.ReadOnly | ScopedItemOptions.Constant | ScopedItemOptions.Private | ScopedItemOptions.AllScope);
			initialSessionState.Variables.Add(item);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		internal static void InitExchangePropertyContainer(ISessionState sessionState, OrganizationId currentExecutingOrgId, ADObjectId currentExecutingUserId)
		{
			ExchangePropertyContainer exchangePropertyContainer = new ExchangePropertyContainer();
			exchangePropertyContainer.executingUserOrganizationId = currentExecutingOrgId;
			exchangePropertyContainer.executingUserId = currentExecutingUserId;
			if (sessionState.Variables.ContainsName(ExchangePropertyContainer.ADServerSettingsVarName))
			{
				exchangePropertyContainer.serverSettings = (sessionState.Variables[ExchangePropertyContainer.ADServerSettingsVarName] as ADServerSettings);
			}
			ExchangePropertyContainer.SetExchangePropertyContainer(sessionState, exchangePropertyContainer);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000D51C File Offset: 0x0000B71C
		internal static void PropagateExchangePropertyContainer(ISessionState sessionState, RunspaceProxy runspace, bool propagateRBAC, bool propagateBudget, ADServerSettings adServerSettingsOverride, ExchangeRunspaceConfigurationSettings.ExchangeApplication application)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			ExchangePropertyContainer exchangePropertyContainer = new ExchangePropertyContainer();
			if (propertyContainer.exchangeRunspaceConfiguration != null)
			{
				propertyContainer.exchangeRunspaceConfiguration.TryGetExecutingUserId(out exchangePropertyContainer.executingUserId);
				exchangePropertyContainer.executingUserOrganizationId = propertyContainer.exchangeRunspaceConfiguration.OrganizationId;
				if (propagateRBAC)
				{
					exchangePropertyContainer.exchangeRunspaceConfiguration = propertyContainer.exchangeRunspaceConfiguration;
				}
				exchangePropertyContainer.propagatedClientAppId = application;
				if (propertyContainer.budget != null && propagateBudget)
				{
					exchangePropertyContainer.budget = ExchangePropertyContainer.AcquirePowerShellBudget(propertyContainer.exchangeRunspaceConfiguration);
				}
			}
			else
			{
				exchangePropertyContainer.executingUserId = propertyContainer.executingUserId;
				exchangePropertyContainer.executingUserOrganizationId = propertyContainer.executingUserOrganizationId;
			}
			exchangePropertyContainer.logEntries = propertyContainer.logEntries;
			exchangePropertyContainer.logEnabled = propertyContainer.logEnabled;
			if (adServerSettingsOverride == null)
			{
				exchangePropertyContainer.serverSettings = propertyContainer.serverSettings;
			}
			else
			{
				exchangePropertyContainer.serverSettings = adServerSettingsOverride;
			}
			runspace.SetVariable(ExchangePropertyContainer.ExchangePropertyContainerName, exchangePropertyContainer);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D5EC File Offset: 0x0000B7EC
		internal static bool InitializeExchangePropertyContainerIfNeeded(ISessionState sessionState, out ADObjectId executingUserId, out OrganizationId executingUserOrganizationId)
		{
			executingUserId = null;
			executingUserOrganizationId = null;
			if (sessionState != null && !ExchangePropertyContainer.IsContainerInitialized(sessionState))
			{
				ExTraceGlobals.LogTracer.Information(0L, "ExchangePropertyContainer is not initialized. Case of Service, Setup or Powershell with manually added snapin");
				executingUserOrganizationId = TaskHelper.ResolveCurrentUserOrganization(out executingUserId);
				if (executingUserOrganizationId == null)
				{
					executingUserOrganizationId = OrganizationId.ForestWideOrgId;
				}
				ExchangePropertyContainer.InitExchangePropertyContainer(sessionState, executingUserOrganizationId, executingUserId);
				return true;
			}
			return false;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D644 File Offset: 0x0000B844
		internal static ADObjectId GetExecutingUserId(ISessionState sessionState)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			if (propertyContainer == null)
			{
				return null;
			}
			return propertyContainer.executingUserId;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D664 File Offset: 0x0000B864
		internal static string GetSiteRedirectionTemplate(ISessionState sessionState)
		{
			ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = ExchangePropertyContainer.GetExchangeRunspaceConfiguration(sessionState);
			if (exchangeRunspaceConfiguration == null || exchangeRunspaceConfiguration.ConfigurationSettings == null)
			{
				return null;
			}
			return exchangeRunspaceConfiguration.ConfigurationSettings.SiteRedirectionTemplate;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D690 File Offset: 0x0000B890
		internal static string GetPodRedirectionTemplate(ISessionState sessionState)
		{
			ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = ExchangePropertyContainer.GetExchangeRunspaceConfiguration(sessionState);
			if (exchangeRunspaceConfiguration == null || exchangeRunspaceConfiguration.ConfigurationSettings == null)
			{
				return null;
			}
			return exchangeRunspaceConfiguration.ConfigurationSettings.PodRedirectionTemplate;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D6BC File Offset: 0x0000B8BC
		internal static Uri GetOriginalConnectionUri(ISessionState sessionState)
		{
			ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = ExchangePropertyContainer.GetExchangeRunspaceConfiguration(sessionState);
			if (exchangeRunspaceConfiguration == null || exchangeRunspaceConfiguration.ConfigurationSettings == null)
			{
				return null;
			}
			return exchangeRunspaceConfiguration.ConfigurationSettings.OriginalConnectionUri;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D6E8 File Offset: 0x0000B8E8
		internal static ExchangeRunspaceConfiguration GetExchangeRunspaceConfiguration(ISessionState sessionState)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			if (propertyContainer == null)
			{
				return null;
			}
			return propertyContainer.exchangeRunspaceConfiguration;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D708 File Offset: 0x0000B908
		internal static ExchangeRunspaceConfiguration GetExchangeRunspaceConfiguration(InitialSessionState initialSessionState)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(initialSessionState);
			return propertyContainer.exchangeRunspaceConfiguration;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D724 File Offset: 0x0000B924
		internal static IPowerShellBudget GetPowerShellBudget(ISessionState sessionState)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			if (propertyContainer == null)
			{
				return null;
			}
			return propertyContainer.budget;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D744 File Offset: 0x0000B944
		internal static ExchangeRunspaceConfigurationSettings.ExchangeApplication GetPropagatedClientAppId(ISessionState sessionState)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			if (propertyContainer == null)
			{
				return ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown;
			}
			return propertyContainer.propagatedClientAppId;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D764 File Offset: 0x0000B964
		internal static OrganizationId GetExecutingUserOrganizationId(ISessionState sessionState)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			if (propertyContainer == null)
			{
				return null;
			}
			return propertyContainer.executingUserOrganizationId;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D784 File Offset: 0x0000B984
		internal static bool IsContainerInitialized(ISessionState sessionState)
		{
			object obj;
			return sessionState.Variables.TryGetValue(ExchangePropertyContainer.ExchangePropertyContainerName, out obj) && obj is ExchangePropertyContainer;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		internal static ExchangeRunspaceConfiguration UpdateExchangeRunspaceConfiguration(ISessionState sessionState)
		{
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = new ExchangeRunspaceConfiguration(current);
			if (!ExchangePropertyContainer.IsContainerInitialized(sessionState))
			{
				ExchangePropertyContainer.InitExchangePropertyContainer(sessionState, exchangeRunspaceConfiguration);
			}
			else
			{
				ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
				propertyContainer.exchangeRunspaceConfiguration = exchangeRunspaceConfiguration;
				if (propertyContainer.budget != null)
				{
					propertyContainer.budget.Dispose();
				}
				propertyContainer.budget = ExchangePropertyContainer.AcquirePowerShellBudget(exchangeRunspaceConfiguration);
			}
			return exchangeRunspaceConfiguration;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000D80C File Offset: 0x0000BA0C
		internal static void SetServerSettings(ISessionState sessionState, ADServerSettings serverSettings)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			if (propertyContainer == null)
			{
				throw new ArgumentException("sessionState");
			}
			propertyContainer.serverSettings = serverSettings;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000D838 File Offset: 0x0000BA38
		internal static ADServerSettings GetServerSettings(ISessionState sessionState)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			if (propertyContainer == null)
			{
				throw new ArgumentException("sessionState");
			}
			return propertyContainer.serverSettings;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000D860 File Offset: 0x0000BA60
		internal static CmdletLogEntries GetCmdletLogEntries(ISessionState sessionState)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			if (propertyContainer == null)
			{
				return null;
			}
			return propertyContainer.logEntries;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D880 File Offset: 0x0000BA80
		internal static bool IsCmdletLogEnabled(ISessionState sessionState)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			return propertyContainer != null && propertyContainer.logEnabled;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
		internal static void EnableCmdletLog(ISessionState sessionState)
		{
			ADObjectId adobjectId = null;
			OrganizationId organizationId = null;
			ExchangePropertyContainer.InitializeExchangePropertyContainerIfNeeded(sessionState, out adobjectId, out organizationId);
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			propertyContainer.logEnabled = true;
			propertyContainer.logEntries.IncreaseIndentation();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		internal static void DisableCmdletLog(ISessionState sessionState)
		{
			ADObjectId adobjectId = null;
			OrganizationId organizationId = null;
			ExchangePropertyContainer.InitializeExchangePropertyContainerIfNeeded(sessionState, out adobjectId, out organizationId);
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			propertyContainer.logEnabled = false;
			propertyContainer.logEntries.DecreaseIndentation();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D910 File Offset: 0x0000BB10
		internal static ProvisioningBroker GetProvisioningBroker(ISessionState sessionState)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			if (propertyContainer == null)
			{
				throw new ArgumentException("sessionState");
			}
			ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = propertyContainer.exchangeRunspaceConfiguration;
			if (exchangeRunspaceConfiguration != null)
			{
				return exchangeRunspaceConfiguration.GetProvisioningBroker();
			}
			if (propertyContainer.provisioningBroker == null)
			{
				propertyContainer.provisioningBroker = new ProvisioningBroker();
			}
			return propertyContainer.provisioningBroker;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000D95C File Offset: 0x0000BB5C
		internal static void RefreshProvisioningBroker(ISessionState sessionState)
		{
			ExchangePropertyContainer propertyContainer = ExchangePropertyContainer.GetPropertyContainer(sessionState);
			if (propertyContainer == null)
			{
				throw new ArgumentException("sessionState");
			}
			ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = propertyContainer.exchangeRunspaceConfiguration;
			if (exchangeRunspaceConfiguration != null)
			{
				exchangeRunspaceConfiguration.RefreshProvisioningBroker();
			}
			if (propertyContainer.provisioningBroker != null)
			{
				propertyContainer.provisioningBroker = null;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000D99D File Offset: 0x0000BB9D
		internal static void ResetPerOrganizationData(ISessionState sessionState)
		{
			ExchangePropertyContainer.SetServerSettings(sessionState, null);
			ExchangePropertyContainer.RefreshProvisioningBroker(sessionState);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		private static IPowerShellBudget AcquirePowerShellBudget(ExchangeRunspaceConfiguration configuration)
		{
			IPowerShellBudget powerShellBudget;
			SecurityIdentifier callerSid;
			if (configuration.DelegatedPrincipal != null)
			{
				powerShellBudget = PowerShellBudget.Acquire(new DelegatedPrincipalBudgetKey(configuration.DelegatedPrincipal, BudgetType.PowerShell));
			}
			else if (!configuration.TryGetExecutingUserSid(out callerSid))
			{
				ADObjectId adobjectId;
				if (!configuration.TryGetExecutingUserId(out adobjectId))
				{
					throw new ExecutingUserPropertyNotFoundException("ExecutingUserSid");
				}
				powerShellBudget = PowerShellBudget.AcquireFallback(adobjectId.ObjectGuid.ToString(), BudgetType.PowerShell);
			}
			else
			{
				ADObjectId rootOrgId;
				if (configuration.ExecutingUserOrganizationId == null || configuration.ExecutingUserOrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					rootOrgId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
				}
				else
				{
					rootOrgId = ADSystemConfigurationSession.GetRootOrgContainerId(configuration.ExecutingUserOrganizationId.PartitionId.ForestFQDN, null, null);
				}
				powerShellBudget = PowerShellBudget.Acquire(callerSid, BudgetType.PowerShell, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgId, configuration.ExecutingUserOrganizationId, configuration.ExecutingUserOrganizationId, true));
			}
			PowerShellThrottlingPolicyUpdater.RevertExpiredThrottlingPolicyIfNeeded(powerShellBudget);
			if (configuration.IsPowerShellWebService)
			{
				IPowerShellBudget result = new PswsBudgetWrapper(((BudgetWrapper<PowerShellBudget>)powerShellBudget).GetInnerBudget());
				if (powerShellBudget != null)
				{
					powerShellBudget.Dispose();
				}
				return result;
			}
			return powerShellBudget;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
		internal static void SetExchangePropertyContainer(ISessionState sessionState, ExchangePropertyContainer container)
		{
			container.logEntries = new CmdletLogEntries();
			object obj;
			if (!sessionState.Variables.TryGetValue(ExchangePropertyContainer.ExchangePropertyContainerName, out obj) || !(obj is ExchangePropertyContainer))
			{
				VariableScopedOptions scope = VariableScopedOptions.AllScope | VariableScopedOptions.Constant | VariableScopedOptions.Private | VariableScopedOptions.ReadOnly;
				sessionState.Variables.Set(ExchangePropertyContainer.ExchangePropertyContainerName, container, scope);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000DAF0 File Offset: 0x0000BCF0
		private static ExchangePropertyContainer GetPropertyContainer(ISessionState sessionState)
		{
			object obj;
			sessionState.Variables.TryGetValue(ExchangePropertyContainer.ExchangePropertyContainerName, out obj);
			return obj as ExchangePropertyContainer;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000DB18 File Offset: 0x0000BD18
		private static ExchangePropertyContainer GetPropertyContainer(InitialSessionState initialSessionState)
		{
			IList<SessionStateVariableEntry> list = initialSessionState.Variables[ExchangePropertyContainer.ExchangePropertyContainerName];
			ExchangePropertyContainer exchangePropertyContainer = null;
			if (list.Count > 0)
			{
				exchangePropertyContainer = (ExchangePropertyContainer)list[0].Value;
			}
			if (exchangePropertyContainer == null)
			{
				throw new ArgumentException("initialSessionState");
			}
			return exchangePropertyContainer;
		}

		// Token: 0x040000E2 RID: 226
		internal static readonly string ADServerSettingsVarName = "ADServerSettings";

		// Token: 0x040000E3 RID: 227
		private static string ExchangePropertyContainerName = "ExchangePropertyContainer";

		// Token: 0x040000E4 RID: 228
		private CmdletLogEntries logEntries;

		// Token: 0x040000E5 RID: 229
		private bool logEnabled;

		// Token: 0x040000E6 RID: 230
		private ExchangeRunspaceConfiguration exchangeRunspaceConfiguration;

		// Token: 0x040000E7 RID: 231
		private ADServerSettings serverSettings;

		// Token: 0x040000E8 RID: 232
		private ProvisioningBroker provisioningBroker;

		// Token: 0x040000E9 RID: 233
		private OrganizationId executingUserOrganizationId;

		// Token: 0x040000EA RID: 234
		private ADObjectId executingUserId;

		// Token: 0x040000EB RID: 235
		private IPowerShellBudget budget;

		// Token: 0x040000EC RID: 236
		private CostHandle activeRunSpaceCostHandle;

		// Token: 0x040000ED RID: 237
		private ExchangeRunspaceConfigurationSettings.ExchangeApplication propagatedClientAppId;
	}
}
