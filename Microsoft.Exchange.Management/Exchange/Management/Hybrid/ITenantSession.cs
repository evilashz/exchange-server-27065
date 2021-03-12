using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x0200090F RID: 2319
	internal interface ITenantSession : ICommonSession, IDisposable
	{
		// Token: 0x0600524F RID: 21071
		void EnableOrganizationCustomization();

		// Token: 0x06005250 RID: 21072
		IEnumerable<IInboundConnector> GetInboundConnectors();

		// Token: 0x06005251 RID: 21073
		IInboundConnector GetInboundConnector(string identity);

		// Token: 0x06005252 RID: 21074
		IntraOrganizationConfiguration GetIntraOrganizationConfiguration();

		// Token: 0x06005253 RID: 21075
		IntraOrganizationConnector GetIntraOrganizationConnector(string identity);

		// Token: 0x06005254 RID: 21076
		IOrganizationalUnit GetOrganizationalUnit();

		// Token: 0x06005255 RID: 21077
		IOnPremisesOrganization GetOnPremisesOrganization(Guid organizationGuid);

		// Token: 0x06005256 RID: 21078
		IEnumerable<IOutboundConnector> GetOutboundConnectors();

		// Token: 0x06005257 RID: 21079
		IOutboundConnector GetOutboundConnector(string identity);

		// Token: 0x06005258 RID: 21080
		IInboundConnector NewInboundConnector(IInboundConnector configuration);

		// Token: 0x06005259 RID: 21081
		void NewIntraOrganizationConnector(string name, string discoveryEndpoint, MultiValuedProperty<SmtpDomain> targetAddressDomains, bool enabled);

		// Token: 0x0600525A RID: 21082
		IOnPremisesOrganization NewOnPremisesOrganization(IOrganizationConfig onPremisesOrgConfig, MultiValuedProperty<SmtpDomain> hybridDomains, IInboundConnector inboundConnector, IOutboundConnector outboundConnector, OrganizationRelationship tenantOrgRel);

		// Token: 0x0600525B RID: 21083
		IOutboundConnector NewOutboundConnector(IOutboundConnector configuration);

		// Token: 0x0600525C RID: 21084
		void RemoveInboundConnector(ADObjectId identity);

		// Token: 0x0600525D RID: 21085
		void RemoveIntraOrganizationConnector(string identity);

		// Token: 0x0600525E RID: 21086
		void RemoveOutboundConnector(ADObjectId identity);

		// Token: 0x0600525F RID: 21087
		void SetFederatedOrganizationIdentifier(string defaultDomain);

		// Token: 0x06005260 RID: 21088
		void SetInboundConnector(IInboundConnector configuration);

		// Token: 0x06005261 RID: 21089
		void SetIntraOrganizationConnector(string identity, string discoveryEndpoint, MultiValuedProperty<SmtpDomain> targetAddressDomains, bool enabled);

		// Token: 0x06005262 RID: 21090
		void SetOnPremisesOrganization(IOnPremisesOrganization configuration, IOrganizationConfig onPremisesOrgConfig, MultiValuedProperty<SmtpDomain> hybridDomains, IInboundConnector inboundConnector, IOutboundConnector outboundConnector, OrganizationRelationship tenantOrgRel);

		// Token: 0x06005263 RID: 21091
		void SetOutboundConnector(IOutboundConnector configuration);

		// Token: 0x06005264 RID: 21092
		void RenameInboundConnector(IInboundConnector configuration, string name);

		// Token: 0x06005265 RID: 21093
		void RenameOutboundConnector(IOutboundConnector configuration, string name);

		// Token: 0x06005266 RID: 21094
		IInboundConnector BuildExpectedInboundConnector(ADObjectId identity, string name, SmtpX509Identifier tlsCertificateName);

		// Token: 0x06005267 RID: 21095
		IOutboundConnector BuildExpectedOutboundConnector(ADObjectId identity, string name, string tlsCertificateSubjectDomainName, MultiValuedProperty<SmtpDomain> hybridDomains, string smartHost, bool centralizedTransportFeatureEnabled);
	}
}
