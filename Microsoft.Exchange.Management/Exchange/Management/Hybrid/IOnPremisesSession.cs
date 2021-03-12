using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x0200090D RID: 2317
	internal interface IOnPremisesSession : ICommonSession, IDisposable
	{
		// Token: 0x0600522A RID: 21034
		void AddAvailabilityAddressSpace(string forestName, AvailabilityAccessMethod accessMethod, bool useServiceAccount, Uri proxyUrl);

		// Token: 0x0600522B RID: 21035
		void AddFederatedDomain(string domainName);

		// Token: 0x0600522C RID: 21036
		IEnumerable<AvailabilityAddressSpace> GetAvailabilityAddressSpace();

		// Token: 0x0600522D RID: 21037
		IEnumerable<EmailAddressPolicy> GetEmailAddressPolicy();

		// Token: 0x0600522E RID: 21038
		IEnumerable<IExchangeCertificate> GetExchangeCertificate(string server);

		// Token: 0x0600522F RID: 21039
		IExchangeCertificate GetExchangeCertificate(string server, SmtpX509Identifier certificateName);

		// Token: 0x06005230 RID: 21040
		IExchangeServer GetExchangeServer(string identity);

		// Token: 0x06005231 RID: 21041
		IEnumerable<IExchangeServer> GetExchangeServer();

		// Token: 0x06005232 RID: 21042
		IEnumerable<IFederationTrust> GetFederationTrust();

		// Token: 0x06005233 RID: 21043
		IntraOrganizationConfiguration GetIntraOrganizationConfiguration();

		// Token: 0x06005234 RID: 21044
		IntraOrganizationConnector GetIntraOrganizationConnector(string identity);

		// Token: 0x06005235 RID: 21045
		IReceiveConnector GetReceiveConnector(ADObjectId server);

		// Token: 0x06005236 RID: 21046
		IEnumerable<ISendConnector> GetSendConnector();

		// Token: 0x06005237 RID: 21047
		IEnumerable<ADWebServicesVirtualDirectory> GetWebServicesVirtualDirectory(string server);

		// Token: 0x06005238 RID: 21048
		void NewAcceptedDomain(string domainName, string name);

		// Token: 0x06005239 RID: 21049
		void NewIntraOrganizationConnector(string name, string discoveryEndpoint, string onlineTargetAddress, bool enabled);

		// Token: 0x0600523A RID: 21050
		DomainContentConfig NewRemoteDomain(string domainName, string name);

		// Token: 0x0600523B RID: 21051
		ISendConnector NewSendConnector(ISendConnector configuration);

		// Token: 0x0600523C RID: 21052
		void RemoveAvailabilityAddressSpace(string identity);

		// Token: 0x0600523D RID: 21053
		void RemoveIntraOrganizationConnector(string identity);

		// Token: 0x0600523E RID: 21054
		void SetEmailAddressPolicy(string identity, string includedRecipients, ProxyAddressTemplateCollection EnabledEmailAddressTemplates);

		// Token: 0x0600523F RID: 21055
		void SetFederatedOrganizationIdentifier(string accountNamespace, string delegationTrustLink, string defaultDomain);

		// Token: 0x06005240 RID: 21056
		void SetFederationTrustRefreshMetadata(string identity);

		// Token: 0x06005241 RID: 21057
		void SetIntraOrganizationConnector(string identity, string discoveryEndpoint, string onlineTargetAddress, bool enabled);

		// Token: 0x06005242 RID: 21058
		void SetReceiveConnector(IReceiveConnector configuration);

		// Token: 0x06005243 RID: 21059
		void SetRemoteDomain(string identity, SessionParameters parameters);

		// Token: 0x06005244 RID: 21060
		void SetSendConnector(ISendConnector configuration);

		// Token: 0x06005245 RID: 21061
		void SetWebServicesVirtualDirectory(string distinguishedName, bool proxyEnabled);

		// Token: 0x06005246 RID: 21062
		void UpdateEmailAddressPolicy(string identity);

		// Token: 0x06005247 RID: 21063
		ISendConnector BuildExpectedSendConnector(string name, string tenantCoexistenceDomain, MultiValuedProperty<ADObjectId> servers, string fqdn, string fopeCertificateSubjectDomainName, SmtpX509Identifier tlsCertificateName, bool enableSecureMail);

		// Token: 0x06005248 RID: 21064
		IReceiveConnector BuildExpectedReceiveConnector(ADObjectId server, SmtpX509Identifier tlsCertificateName, SmtpReceiveDomainCapabilities tlsDomainCapabilities);
	}
}
