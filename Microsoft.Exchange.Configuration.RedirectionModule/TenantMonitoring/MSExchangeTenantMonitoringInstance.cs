using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.TenantMonitoring
{
	// Token: 0x02000016 RID: 22
	internal sealed class MSExchangeTenantMonitoringInstance : PerformanceCounterInstance
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00004200 File Offset: 0x00002400
		internal MSExchangeTenantMonitoringInstance(string instanceName, MSExchangeTenantMonitoringInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTenantMonitoring")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.MSExchangeHomeSiteLocationAttempts = new ExPerformanceCounter(base.CategoryName, "Datacenter and Site Location Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeHomeSiteLocationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeHomeSiteLocationAttempts);
				this.MSExchangeHomeSiteLocationSuccesses = new ExPerformanceCounter(base.CategoryName, "Datacenter and Site Location Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeHomeSiteLocationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeHomeSiteLocationSuccesses);
				this.MSExchangePartnerHomeSiteLocationAttempts = new ExPerformanceCounter(base.CategoryName, "Partner Datacenter and Site Location Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangePartnerHomeSiteLocationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangePartnerHomeSiteLocationAttempts);
				this.MSExchangePartnerHomeSiteLocationSuccesses = new ExPerformanceCounter(base.CategoryName, "Partner Datacenter and Site Location Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangePartnerHomeSiteLocationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangePartnerHomeSiteLocationSuccesses);
				this.MSExchangeRemotePoweshellUserAuthorizationAttempts = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell Tenant User Authorization Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemotePoweshellUserAuthorizationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellUserAuthorizationAttempts);
				this.MSExchangeRemotePoweshellUserAuthorizationSuccesses = new ExPerformanceCounter(base.CategoryName, "Remote PoweSshell Tenant User Authorization Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemotePoweshellUserAuthorizationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellUserAuthorizationSuccesses);
				this.MSExchangeRemotePoweshellSessionCreationAttempts = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell Tenant Session Creation Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemotePoweshellSessionCreationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellSessionCreationAttempts);
				this.MSExchangeRemotePoweshellSessionCreationSuccesses = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell Tenant Session Creation Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemotePoweshellSessionCreationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellSessionCreationSuccesses);
				this.MSExchangeRemotePoweshellPartnerAuthorizationAttempts = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell Partner Authorization Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemotePoweshellPartnerAuthorizationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellPartnerAuthorizationAttempts);
				this.MSExchangeRemotePoweshellPartnerAuthorizationSuccesses = new ExPerformanceCounter(base.CategoryName, "Remote Powershell Partner Authorization Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemotePoweshellPartnerAuthorizationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellPartnerAuthorizationSuccesses);
				this.MSExchangeRemotePoweshellPartnerSessionCreationAttempts = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell Partner Session Creation Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemotePoweshellPartnerSessionCreationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellPartnerSessionCreationAttempts);
				this.MSExchangeRemotePoweshellPartnerSessionCreationSuccesses = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell partner session creation successes per period.", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemotePoweshellPartnerSessionCreationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellPartnerSessionCreationSuccesses);
				this.MSExchangeECPSessionCreationAttempts = new ExPerformanceCounter(base.CategoryName, "Exchange Control Panel Session Creation Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeECPSessionCreationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeECPSessionCreationAttempts);
				this.MSExchangeECPSessionCreationSuccesses = new ExPerformanceCounter(base.CategoryName, "Exchange Control Panel Session Creation Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeECPSessionCreationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeECPSessionCreationSuccesses);
				this.MSExchangeECPRedirectionSuccesses = new ExPerformanceCounter(base.CategoryName, "ECP session redirection successes per period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeECPRedirectionSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeECPRedirectionSuccesses);
				this.MSExchangeNewMailboxAttempts = new ExPerformanceCounter(base.CategoryName, "NewMailbox Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeNewMailboxAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewMailboxAttempts);
				this.MSExchangeNewMailboxSuccesses = new ExPerformanceCounter(base.CategoryName, "NewMailbox Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeNewMailboxSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewMailboxSuccesses);
				this.MSExchangeNewMailboxIterationAttempts = new ExPerformanceCounter(base.CategoryName, "NewMailbox Iteration Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeNewMailboxIterationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewMailboxIterationAttempts);
				this.MSExchangeNewMailboxIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "NewMailbox Iteration Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeNewMailboxIterationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewMailboxIterationSuccesses);
				this.MSExchangeNewOrganizationAttempts = new ExPerformanceCounter(base.CategoryName, "NewOrganization Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeNewOrganizationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewOrganizationAttempts);
				this.MSExchangeNewOrganizationSuccesses = new ExPerformanceCounter(base.CategoryName, "NewOrganization Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeNewOrganizationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewOrganizationSuccesses);
				this.MSExchangeNewOrganizationIterationAttempts = new ExPerformanceCounter(base.CategoryName, "NewOrganization Iteration Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeNewOrganizationIterationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewOrganizationIterationAttempts);
				this.MSExchangeNewOrganizationIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "NewOrganization Iteration Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeNewOrganizationIterationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewOrganizationIterationSuccesses);
				this.MSExchangeRemoveOrganizationAttempts = new ExPerformanceCounter(base.CategoryName, "RemoveOrganization Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemoveOrganizationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveOrganizationAttempts);
				this.MSExchangeRemoveOrganizationSuccesses = new ExPerformanceCounter(base.CategoryName, "RemoveOrganization successes per period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemoveOrganizationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveOrganizationSuccesses);
				this.MSExchangeRemoveOrganizationIterationAttempts = new ExPerformanceCounter(base.CategoryName, "RemoveOrganization Iteration Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemoveOrganizationIterationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveOrganizationIterationAttempts);
				this.MSExchangeRemoveOrganizationIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "RemoveOrganization Iteration Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemoveOrganizationIterationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveOrganizationIterationSuccesses);
				this.MSExchangeAddSecondaryDomainAttempts = new ExPerformanceCounter(base.CategoryName, "AddSecondaryDomain Attempts Per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeAddSecondaryDomainAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeAddSecondaryDomainAttempts);
				this.MSExchangeAddSecondaryDomainSuccesses = new ExPerformanceCounter(base.CategoryName, "AddSecondaryDomain Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeAddSecondaryDomainSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeAddSecondaryDomainSuccesses);
				this.MSExchangeAddSecondaryDomainIterationAttempts = new ExPerformanceCounter(base.CategoryName, "AddSecondaryDomainIteration Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeAddSecondaryDomainIterationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeAddSecondaryDomainIterationAttempts);
				this.MSExchangeAddSecondaryDomainIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "AddSecondaryDomain Iteration Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeAddSecondaryDomainIterationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeAddSecondaryDomainIterationSuccesses);
				this.MSExchangeRemoveSecondaryDomainAttempts = new ExPerformanceCounter(base.CategoryName, "RemoveSecondaryDomain Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemoveSecondaryDomainAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveSecondaryDomainAttempts);
				this.MSExchangeRemoveSecondaryDomainSuccesses = new ExPerformanceCounter(base.CategoryName, "RemoveSecondaryDomain Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemoveSecondaryDomainSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveSecondaryDomainSuccesses);
				this.MSExchangeRemoveSecondaryDomainIterationAttempts = new ExPerformanceCounter(base.CategoryName, "RemoveSecondaryDomain Iteration Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemoveSecondaryDomainIterationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveSecondaryDomainIterationAttempts);
				this.MSExchangeRemoveSecondaryDomainIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "RemoveSecondaryDomain Iteration Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeRemoveSecondaryDomainIterationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveSecondaryDomainIterationSuccesses);
				this.MSExchangeStartOrganizationPilotAttempts = new ExPerformanceCounter(base.CategoryName, "StartOrganizationPilot Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeStartOrganizationPilotAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationPilotAttempts);
				this.MSExchangeStartOrganizationPilotSuccesses = new ExPerformanceCounter(base.CategoryName, "StartOrganizationPilot Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeStartOrganizationPilotSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationPilotSuccesses);
				this.MSExchangeStartOrganizationPilotIterationAttempts = new ExPerformanceCounter(base.CategoryName, "StartOrganizationPilot Iteration Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeStartOrganizationPilotIterationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationPilotIterationAttempts);
				this.MSExchangeStartOrganizationPilotIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "StartOrganizationPilot Iteration Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeStartOrganizationPilotIterationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationPilotIterationSuccesses);
				this.MSExchangeStartOrganizationUpgradeAttempts = new ExPerformanceCounter(base.CategoryName, "StartOrganizationUpgrade Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeStartOrganizationUpgradeAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationUpgradeAttempts);
				this.MSExchangeStartOrganizationUpgradeSuccesses = new ExPerformanceCounter(base.CategoryName, "StartOrganizationUpgrade Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeStartOrganizationUpgradeSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationUpgradeSuccesses);
				this.MSExchangeStartOrganizationUpgradeIterationAttempts = new ExPerformanceCounter(base.CategoryName, "StartOrganizationUpgrade Iteration Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeStartOrganizationUpgradeIterationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationUpgradeIterationAttempts);
				this.MSExchangeStartOrganizationUpgradeIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "StartOrganizationUpgrade Iteration Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeStartOrganizationUpgradeIterationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationUpgradeIterationSuccesses);
				this.MSExchangeCompleteOrganizationUpgradeAttempts = new ExPerformanceCounter(base.CategoryName, "CompleteOrganizationUpgrade Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeCompleteOrganizationUpgradeAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCompleteOrganizationUpgradeAttempts);
				this.MSExchangeCompleteOrganizationUpgradeSuccesses = new ExPerformanceCounter(base.CategoryName, "CompleteOrganizationUpgrade Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeCompleteOrganizationUpgradeSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCompleteOrganizationUpgradeSuccesses);
				this.MSExchangeCompleteOrganizationUpgradeIterationAttempts = new ExPerformanceCounter(base.CategoryName, "CompleteOrganizationUpgrade Iteration Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeCompleteOrganizationUpgradeIterationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCompleteOrganizationUpgradeIterationAttempts);
				this.MSExchangeCompleteOrganizationUpgradeIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "CompleteOrganizationUpgrade Iteration Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeCompleteOrganizationUpgradeIterationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCompleteOrganizationUpgradeIterationSuccesses);
				this.MSExchangeGetManagementEndpointAttempts = new ExPerformanceCounter(base.CategoryName, "GetManagementEndpoint Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeGetManagementEndpointAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeGetManagementEndpointAttempts);
				this.MSExchangeGetManagementEndpointSuccesses = new ExPerformanceCounter(base.CategoryName, "GetManagementEndpoint Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeGetManagementEndpointSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeGetManagementEndpointSuccesses);
				this.MSExchangeGetManagementEndpointIterationAttempts = new ExPerformanceCounter(base.CategoryName, "GetManagementEndpoint Iteration Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeGetManagementEndpointIterationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeGetManagementEndpointIterationAttempts);
				this.MSExchangeGetManagementEndpointIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "GetManagementEndpoint Iteration Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeGetManagementEndpointIterationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeGetManagementEndpointIterationSuccesses);
				this.MSExchangeCmdletAttempts = new ExPerformanceCounter(base.CategoryName, "Cmdlet attempts per period. This is only for cmdlets which are to be monitored.", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeCmdletAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCmdletAttempts);
				this.MSExchangeCmdletSuccesses = new ExPerformanceCounter(base.CategoryName, "Cmdlet Successes per Period. This is only for cmdlets which are to be monitored.", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeCmdletSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCmdletSuccesses);
				this.MSExchangeCmdletIterationAttempts = new ExPerformanceCounter(base.CategoryName, "Cmdlet Iteration Attempts per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeCmdletIterationAttempts, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCmdletIterationAttempts);
				this.MSExchangeCmdletIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "Cmdlet Iteration Successes per Period", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSExchangeCmdletIterationSuccesses, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCmdletIterationSuccesses);
				long num = this.MSExchangeHomeSiteLocationAttempts.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004E10 File Offset: 0x00003010
		internal MSExchangeTenantMonitoringInstance(string instanceName) : base(instanceName, "MSExchangeTenantMonitoring")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.MSExchangeHomeSiteLocationAttempts = new ExPerformanceCounter(base.CategoryName, "Datacenter and Site Location Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeHomeSiteLocationAttempts);
				this.MSExchangeHomeSiteLocationSuccesses = new ExPerformanceCounter(base.CategoryName, "Datacenter and Site Location Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeHomeSiteLocationSuccesses);
				this.MSExchangePartnerHomeSiteLocationAttempts = new ExPerformanceCounter(base.CategoryName, "Partner Datacenter and Site Location Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangePartnerHomeSiteLocationAttempts);
				this.MSExchangePartnerHomeSiteLocationSuccesses = new ExPerformanceCounter(base.CategoryName, "Partner Datacenter and Site Location Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangePartnerHomeSiteLocationSuccesses);
				this.MSExchangeRemotePoweshellUserAuthorizationAttempts = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell Tenant User Authorization Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellUserAuthorizationAttempts);
				this.MSExchangeRemotePoweshellUserAuthorizationSuccesses = new ExPerformanceCounter(base.CategoryName, "Remote PoweSshell Tenant User Authorization Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellUserAuthorizationSuccesses);
				this.MSExchangeRemotePoweshellSessionCreationAttempts = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell Tenant Session Creation Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellSessionCreationAttempts);
				this.MSExchangeRemotePoweshellSessionCreationSuccesses = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell Tenant Session Creation Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellSessionCreationSuccesses);
				this.MSExchangeRemotePoweshellPartnerAuthorizationAttempts = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell Partner Authorization Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellPartnerAuthorizationAttempts);
				this.MSExchangeRemotePoweshellPartnerAuthorizationSuccesses = new ExPerformanceCounter(base.CategoryName, "Remote Powershell Partner Authorization Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellPartnerAuthorizationSuccesses);
				this.MSExchangeRemotePoweshellPartnerSessionCreationAttempts = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell Partner Session Creation Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellPartnerSessionCreationAttempts);
				this.MSExchangeRemotePoweshellPartnerSessionCreationSuccesses = new ExPerformanceCounter(base.CategoryName, "Remote PowerShell partner session creation successes per period.", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemotePoweshellPartnerSessionCreationSuccesses);
				this.MSExchangeECPSessionCreationAttempts = new ExPerformanceCounter(base.CategoryName, "Exchange Control Panel Session Creation Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeECPSessionCreationAttempts);
				this.MSExchangeECPSessionCreationSuccesses = new ExPerformanceCounter(base.CategoryName, "Exchange Control Panel Session Creation Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeECPSessionCreationSuccesses);
				this.MSExchangeECPRedirectionSuccesses = new ExPerformanceCounter(base.CategoryName, "ECP session redirection successes per period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeECPRedirectionSuccesses);
				this.MSExchangeNewMailboxAttempts = new ExPerformanceCounter(base.CategoryName, "NewMailbox Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewMailboxAttempts);
				this.MSExchangeNewMailboxSuccesses = new ExPerformanceCounter(base.CategoryName, "NewMailbox Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewMailboxSuccesses);
				this.MSExchangeNewMailboxIterationAttempts = new ExPerformanceCounter(base.CategoryName, "NewMailbox Iteration Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewMailboxIterationAttempts);
				this.MSExchangeNewMailboxIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "NewMailbox Iteration Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewMailboxIterationSuccesses);
				this.MSExchangeNewOrganizationAttempts = new ExPerformanceCounter(base.CategoryName, "NewOrganization Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewOrganizationAttempts);
				this.MSExchangeNewOrganizationSuccesses = new ExPerformanceCounter(base.CategoryName, "NewOrganization Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewOrganizationSuccesses);
				this.MSExchangeNewOrganizationIterationAttempts = new ExPerformanceCounter(base.CategoryName, "NewOrganization Iteration Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewOrganizationIterationAttempts);
				this.MSExchangeNewOrganizationIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "NewOrganization Iteration Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeNewOrganizationIterationSuccesses);
				this.MSExchangeRemoveOrganizationAttempts = new ExPerformanceCounter(base.CategoryName, "RemoveOrganization Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveOrganizationAttempts);
				this.MSExchangeRemoveOrganizationSuccesses = new ExPerformanceCounter(base.CategoryName, "RemoveOrganization successes per period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveOrganizationSuccesses);
				this.MSExchangeRemoveOrganizationIterationAttempts = new ExPerformanceCounter(base.CategoryName, "RemoveOrganization Iteration Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveOrganizationIterationAttempts);
				this.MSExchangeRemoveOrganizationIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "RemoveOrganization Iteration Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveOrganizationIterationSuccesses);
				this.MSExchangeAddSecondaryDomainAttempts = new ExPerformanceCounter(base.CategoryName, "AddSecondaryDomain Attempts Per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeAddSecondaryDomainAttempts);
				this.MSExchangeAddSecondaryDomainSuccesses = new ExPerformanceCounter(base.CategoryName, "AddSecondaryDomain Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeAddSecondaryDomainSuccesses);
				this.MSExchangeAddSecondaryDomainIterationAttempts = new ExPerformanceCounter(base.CategoryName, "AddSecondaryDomainIteration Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeAddSecondaryDomainIterationAttempts);
				this.MSExchangeAddSecondaryDomainIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "AddSecondaryDomain Iteration Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeAddSecondaryDomainIterationSuccesses);
				this.MSExchangeRemoveSecondaryDomainAttempts = new ExPerformanceCounter(base.CategoryName, "RemoveSecondaryDomain Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveSecondaryDomainAttempts);
				this.MSExchangeRemoveSecondaryDomainSuccesses = new ExPerformanceCounter(base.CategoryName, "RemoveSecondaryDomain Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveSecondaryDomainSuccesses);
				this.MSExchangeRemoveSecondaryDomainIterationAttempts = new ExPerformanceCounter(base.CategoryName, "RemoveSecondaryDomain Iteration Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveSecondaryDomainIterationAttempts);
				this.MSExchangeRemoveSecondaryDomainIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "RemoveSecondaryDomain Iteration Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeRemoveSecondaryDomainIterationSuccesses);
				this.MSExchangeStartOrganizationPilotAttempts = new ExPerformanceCounter(base.CategoryName, "StartOrganizationPilot Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationPilotAttempts);
				this.MSExchangeStartOrganizationPilotSuccesses = new ExPerformanceCounter(base.CategoryName, "StartOrganizationPilot Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationPilotSuccesses);
				this.MSExchangeStartOrganizationPilotIterationAttempts = new ExPerformanceCounter(base.CategoryName, "StartOrganizationPilot Iteration Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationPilotIterationAttempts);
				this.MSExchangeStartOrganizationPilotIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "StartOrganizationPilot Iteration Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationPilotIterationSuccesses);
				this.MSExchangeStartOrganizationUpgradeAttempts = new ExPerformanceCounter(base.CategoryName, "StartOrganizationUpgrade Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationUpgradeAttempts);
				this.MSExchangeStartOrganizationUpgradeSuccesses = new ExPerformanceCounter(base.CategoryName, "StartOrganizationUpgrade Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationUpgradeSuccesses);
				this.MSExchangeStartOrganizationUpgradeIterationAttempts = new ExPerformanceCounter(base.CategoryName, "StartOrganizationUpgrade Iteration Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationUpgradeIterationAttempts);
				this.MSExchangeStartOrganizationUpgradeIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "StartOrganizationUpgrade Iteration Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeStartOrganizationUpgradeIterationSuccesses);
				this.MSExchangeCompleteOrganizationUpgradeAttempts = new ExPerformanceCounter(base.CategoryName, "CompleteOrganizationUpgrade Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCompleteOrganizationUpgradeAttempts);
				this.MSExchangeCompleteOrganizationUpgradeSuccesses = new ExPerformanceCounter(base.CategoryName, "CompleteOrganizationUpgrade Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCompleteOrganizationUpgradeSuccesses);
				this.MSExchangeCompleteOrganizationUpgradeIterationAttempts = new ExPerformanceCounter(base.CategoryName, "CompleteOrganizationUpgrade Iteration Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCompleteOrganizationUpgradeIterationAttempts);
				this.MSExchangeCompleteOrganizationUpgradeIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "CompleteOrganizationUpgrade Iteration Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCompleteOrganizationUpgradeIterationSuccesses);
				this.MSExchangeGetManagementEndpointAttempts = new ExPerformanceCounter(base.CategoryName, "GetManagementEndpoint Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeGetManagementEndpointAttempts);
				this.MSExchangeGetManagementEndpointSuccesses = new ExPerformanceCounter(base.CategoryName, "GetManagementEndpoint Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeGetManagementEndpointSuccesses);
				this.MSExchangeGetManagementEndpointIterationAttempts = new ExPerformanceCounter(base.CategoryName, "GetManagementEndpoint Iteration Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeGetManagementEndpointIterationAttempts);
				this.MSExchangeGetManagementEndpointIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "GetManagementEndpoint Iteration Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeGetManagementEndpointIterationSuccesses);
				this.MSExchangeCmdletAttempts = new ExPerformanceCounter(base.CategoryName, "Cmdlet attempts per period. This is only for cmdlets which are to be monitored.", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCmdletAttempts);
				this.MSExchangeCmdletSuccesses = new ExPerformanceCounter(base.CategoryName, "Cmdlet Successes per Period. This is only for cmdlets which are to be monitored.", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCmdletSuccesses);
				this.MSExchangeCmdletIterationAttempts = new ExPerformanceCounter(base.CategoryName, "Cmdlet Iteration Attempts per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCmdletIterationAttempts);
				this.MSExchangeCmdletIterationSuccesses = new ExPerformanceCounter(base.CategoryName, "Cmdlet Iteration Successes per Period", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MSExchangeCmdletIterationSuccesses);
				long num = this.MSExchangeHomeSiteLocationAttempts.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000057C4 File Offset: 0x000039C4
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04000061 RID: 97
		public readonly ExPerformanceCounter MSExchangeHomeSiteLocationAttempts;

		// Token: 0x04000062 RID: 98
		public readonly ExPerformanceCounter MSExchangeHomeSiteLocationSuccesses;

		// Token: 0x04000063 RID: 99
		public readonly ExPerformanceCounter MSExchangePartnerHomeSiteLocationAttempts;

		// Token: 0x04000064 RID: 100
		public readonly ExPerformanceCounter MSExchangePartnerHomeSiteLocationSuccesses;

		// Token: 0x04000065 RID: 101
		public readonly ExPerformanceCounter MSExchangeRemotePoweshellUserAuthorizationAttempts;

		// Token: 0x04000066 RID: 102
		public readonly ExPerformanceCounter MSExchangeRemotePoweshellUserAuthorizationSuccesses;

		// Token: 0x04000067 RID: 103
		public readonly ExPerformanceCounter MSExchangeRemotePoweshellSessionCreationAttempts;

		// Token: 0x04000068 RID: 104
		public readonly ExPerformanceCounter MSExchangeRemotePoweshellSessionCreationSuccesses;

		// Token: 0x04000069 RID: 105
		public readonly ExPerformanceCounter MSExchangeRemotePoweshellPartnerAuthorizationAttempts;

		// Token: 0x0400006A RID: 106
		public readonly ExPerformanceCounter MSExchangeRemotePoweshellPartnerAuthorizationSuccesses;

		// Token: 0x0400006B RID: 107
		public readonly ExPerformanceCounter MSExchangeRemotePoweshellPartnerSessionCreationAttempts;

		// Token: 0x0400006C RID: 108
		public readonly ExPerformanceCounter MSExchangeRemotePoweshellPartnerSessionCreationSuccesses;

		// Token: 0x0400006D RID: 109
		public readonly ExPerformanceCounter MSExchangeECPSessionCreationAttempts;

		// Token: 0x0400006E RID: 110
		public readonly ExPerformanceCounter MSExchangeECPSessionCreationSuccesses;

		// Token: 0x0400006F RID: 111
		public readonly ExPerformanceCounter MSExchangeECPRedirectionSuccesses;

		// Token: 0x04000070 RID: 112
		public readonly ExPerformanceCounter MSExchangeNewMailboxAttempts;

		// Token: 0x04000071 RID: 113
		public readonly ExPerformanceCounter MSExchangeNewMailboxSuccesses;

		// Token: 0x04000072 RID: 114
		public readonly ExPerformanceCounter MSExchangeNewMailboxIterationAttempts;

		// Token: 0x04000073 RID: 115
		public readonly ExPerformanceCounter MSExchangeNewMailboxIterationSuccesses;

		// Token: 0x04000074 RID: 116
		public readonly ExPerformanceCounter MSExchangeNewOrganizationAttempts;

		// Token: 0x04000075 RID: 117
		public readonly ExPerformanceCounter MSExchangeNewOrganizationSuccesses;

		// Token: 0x04000076 RID: 118
		public readonly ExPerformanceCounter MSExchangeNewOrganizationIterationAttempts;

		// Token: 0x04000077 RID: 119
		public readonly ExPerformanceCounter MSExchangeNewOrganizationIterationSuccesses;

		// Token: 0x04000078 RID: 120
		public readonly ExPerformanceCounter MSExchangeRemoveOrganizationAttempts;

		// Token: 0x04000079 RID: 121
		public readonly ExPerformanceCounter MSExchangeRemoveOrganizationSuccesses;

		// Token: 0x0400007A RID: 122
		public readonly ExPerformanceCounter MSExchangeRemoveOrganizationIterationAttempts;

		// Token: 0x0400007B RID: 123
		public readonly ExPerformanceCounter MSExchangeRemoveOrganizationIterationSuccesses;

		// Token: 0x0400007C RID: 124
		public readonly ExPerformanceCounter MSExchangeAddSecondaryDomainAttempts;

		// Token: 0x0400007D RID: 125
		public readonly ExPerformanceCounter MSExchangeAddSecondaryDomainSuccesses;

		// Token: 0x0400007E RID: 126
		public readonly ExPerformanceCounter MSExchangeAddSecondaryDomainIterationAttempts;

		// Token: 0x0400007F RID: 127
		public readonly ExPerformanceCounter MSExchangeAddSecondaryDomainIterationSuccesses;

		// Token: 0x04000080 RID: 128
		public readonly ExPerformanceCounter MSExchangeRemoveSecondaryDomainAttempts;

		// Token: 0x04000081 RID: 129
		public readonly ExPerformanceCounter MSExchangeRemoveSecondaryDomainSuccesses;

		// Token: 0x04000082 RID: 130
		public readonly ExPerformanceCounter MSExchangeRemoveSecondaryDomainIterationAttempts;

		// Token: 0x04000083 RID: 131
		public readonly ExPerformanceCounter MSExchangeRemoveSecondaryDomainIterationSuccesses;

		// Token: 0x04000084 RID: 132
		public readonly ExPerformanceCounter MSExchangeStartOrganizationPilotAttempts;

		// Token: 0x04000085 RID: 133
		public readonly ExPerformanceCounter MSExchangeStartOrganizationPilotSuccesses;

		// Token: 0x04000086 RID: 134
		public readonly ExPerformanceCounter MSExchangeStartOrganizationPilotIterationAttempts;

		// Token: 0x04000087 RID: 135
		public readonly ExPerformanceCounter MSExchangeStartOrganizationPilotIterationSuccesses;

		// Token: 0x04000088 RID: 136
		public readonly ExPerformanceCounter MSExchangeStartOrganizationUpgradeAttempts;

		// Token: 0x04000089 RID: 137
		public readonly ExPerformanceCounter MSExchangeStartOrganizationUpgradeSuccesses;

		// Token: 0x0400008A RID: 138
		public readonly ExPerformanceCounter MSExchangeStartOrganizationUpgradeIterationAttempts;

		// Token: 0x0400008B RID: 139
		public readonly ExPerformanceCounter MSExchangeStartOrganizationUpgradeIterationSuccesses;

		// Token: 0x0400008C RID: 140
		public readonly ExPerformanceCounter MSExchangeCompleteOrganizationUpgradeAttempts;

		// Token: 0x0400008D RID: 141
		public readonly ExPerformanceCounter MSExchangeCompleteOrganizationUpgradeSuccesses;

		// Token: 0x0400008E RID: 142
		public readonly ExPerformanceCounter MSExchangeCompleteOrganizationUpgradeIterationAttempts;

		// Token: 0x0400008F RID: 143
		public readonly ExPerformanceCounter MSExchangeCompleteOrganizationUpgradeIterationSuccesses;

		// Token: 0x04000090 RID: 144
		public readonly ExPerformanceCounter MSExchangeGetManagementEndpointAttempts;

		// Token: 0x04000091 RID: 145
		public readonly ExPerformanceCounter MSExchangeGetManagementEndpointSuccesses;

		// Token: 0x04000092 RID: 146
		public readonly ExPerformanceCounter MSExchangeGetManagementEndpointIterationAttempts;

		// Token: 0x04000093 RID: 147
		public readonly ExPerformanceCounter MSExchangeGetManagementEndpointIterationSuccesses;

		// Token: 0x04000094 RID: 148
		public readonly ExPerformanceCounter MSExchangeCmdletAttempts;

		// Token: 0x04000095 RID: 149
		public readonly ExPerformanceCounter MSExchangeCmdletSuccesses;

		// Token: 0x04000096 RID: 150
		public readonly ExPerformanceCounter MSExchangeCmdletIterationAttempts;

		// Token: 0x04000097 RID: 151
		public readonly ExPerformanceCounter MSExchangeCmdletIterationSuccesses;
	}
}
