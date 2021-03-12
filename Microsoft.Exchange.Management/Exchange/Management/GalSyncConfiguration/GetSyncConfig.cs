using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.GalSyncConfiguration
{
	// Token: 0x0200040A RID: 1034
	[Cmdlet("Get", "SyncConfig")]
	public sealed class GetSyncConfig : GetMultitenancySingletonSystemConfigurationObjectTask<SyncOrganization>
	{
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x000908EB File Offset: 0x0008EAEB
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000908F0 File Offset: 0x0008EAF0
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			AcceptedDomain acceptedDomain = null;
			SyncOrganization syncOrganization = (SyncOrganization)dataObject;
			SmtpDomain federatedNamespace = null;
			SmtpDomainWithSubdomains provisioningDomain = null;
			if (syncOrganization.FederatedTenant)
			{
				acceptedDomain = this.ResolveDefaultAcceptedDomain();
				federatedNamespace = acceptedDomain.DomainName.SmtpDomain;
			}
			if (syncOrganization.ProvisioningDomain == null)
			{
				if (syncOrganization.FederatedTenant)
				{
					provisioningDomain = acceptedDomain.DomainName;
				}
				else
				{
					acceptedDomain = this.ResolveDefaultAcceptedDomain();
					provisioningDomain = acceptedDomain.DomainName;
				}
			}
			base.WriteResult(new SyncConfig((SyncOrganization)dataObject, federatedNamespace, provisioningDomain));
			TaskLogger.LogExit();
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00090984 File Offset: 0x0008EB84
		private AcceptedDomain ResolveDefaultAcceptedDomain()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			AcceptedDomain defaultAcceptedDomain = configurationSession.GetDefaultAcceptedDomain();
			if (defaultAcceptedDomain == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorNoDefaultAcceptedDomainFound(configurationSession.SessionSettings.CurrentOrganizationId.ConfigurationUnit.ToString())), (ErrorCategory)1001, null);
			}
			return defaultAcceptedDomain;
		}
	}
}
