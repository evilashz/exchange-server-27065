using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.HttpProxy.Routing.Providers
{
	// Token: 0x0200000C RID: 12
	internal interface IUserProvider
	{
		// Token: 0x06000024 RID: 36
		User FindByExchangeGuidIncludingAlternate(Guid exchangeGuid, string tenantDomain, IRoutingDiagnostics diagnostics);

		// Token: 0x06000025 RID: 37
		User FindBySmtpAddress(SmtpAddress smtpAddress, IRoutingDiagnostics diagnostics);

		// Token: 0x06000026 RID: 38
		User FindByExternalDirectoryObjectId(Guid userGuid, Guid tenantGuid, IRoutingDiagnostics diagnostics);

		// Token: 0x06000027 RID: 39
		User FindByLiveIdMemberName(SmtpAddress liveIdMemberName, string organizationContext, IRoutingDiagnostics diagnostics);

		// Token: 0x06000028 RID: 40
		string FindResourceForestFqdnByAcceptedDomainName(string acceptedDomain, IRoutingDiagnostics diagnostics);

		// Token: 0x06000029 RID: 41
		string FindResourceForestFqdnByExternalDirectoryOrganizationId(Guid externalDirectoryOrganizationId, IRoutingDiagnostics diagnostics);
	}
}
