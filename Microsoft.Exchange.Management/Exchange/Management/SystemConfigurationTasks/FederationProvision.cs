using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009D8 RID: 2520
	internal abstract class FederationProvision
	{
		// Token: 0x06005A25 RID: 23077 RVA: 0x00179DA8 File Offset: 0x00177FA8
		public static FederationProvision Create(FederationTrust federationTrust, Task task)
		{
			switch (federationTrust.NamespaceProvisioner)
			{
			case FederationTrust.NamespaceProvisionerType.LiveDomainServices:
				return new LiveFederationProvision1(federationTrust.OrgPrivCertificate, federationTrust.ApplicationIdentifier, task);
			case FederationTrust.NamespaceProvisionerType.LiveDomainServices2:
				return new LiveFederationProvision2(federationTrust.OrgPrivCertificate, federationTrust.ApplicationIdentifier, task);
			default:
				return new CustomFederationProvision();
			}
		}

		// Token: 0x06005A27 RID: 23079
		public abstract void OnNewFederationTrust(FederationTrust federationTrust);

		// Token: 0x06005A28 RID: 23080
		public abstract void OnSetFederatedOrganizationIdentifier(FederationTrust federationTrust, SmtpDomain accountNamespace);

		// Token: 0x06005A29 RID: 23081
		public abstract void OnAddFederatedDomain(SmtpDomain smtpDomain);

		// Token: 0x06005A2A RID: 23082
		public abstract void OnRemoveAccountNamespace(SmtpDomain smtpDomain, bool force);

		// Token: 0x06005A2B RID: 23083
		public abstract void OnRemoveFederatedDomain(SmtpDomain smtpDomain, bool force);

		// Token: 0x06005A2C RID: 23084
		public abstract void OnPublishFederationCertificate(FederationTrust federationTrust);

		// Token: 0x06005A2D RID: 23085
		public abstract DomainState GetDomainState(string domain);
	}
}
