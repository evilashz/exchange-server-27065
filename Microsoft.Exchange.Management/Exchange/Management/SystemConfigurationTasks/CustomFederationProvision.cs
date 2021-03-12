using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009D9 RID: 2521
	internal sealed class CustomFederationProvision : FederationProvision
	{
		// Token: 0x06005A2F RID: 23087 RVA: 0x00179E07 File Offset: 0x00178007
		public override void OnNewFederationTrust(FederationTrust federationTrust)
		{
		}

		// Token: 0x06005A30 RID: 23088 RVA: 0x00179E09 File Offset: 0x00178009
		public override void OnSetFederatedOrganizationIdentifier(FederationTrust federationTrust, SmtpDomain accountNamespace)
		{
		}

		// Token: 0x06005A31 RID: 23089 RVA: 0x00179E0B File Offset: 0x0017800B
		public override void OnAddFederatedDomain(SmtpDomain smtpDomain)
		{
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x00179E0D File Offset: 0x0017800D
		public override void OnRemoveAccountNamespace(SmtpDomain smtpDomain, bool force)
		{
		}

		// Token: 0x06005A33 RID: 23091 RVA: 0x00179E0F File Offset: 0x0017800F
		public override void OnRemoveFederatedDomain(SmtpDomain smtpDomain, bool force)
		{
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x00179E11 File Offset: 0x00178011
		public override void OnPublishFederationCertificate(FederationTrust federationTrust)
		{
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x00179E13 File Offset: 0x00178013
		public override DomainState GetDomainState(string domain)
		{
			return DomainState.CustomProvision;
		}
	}
}
