using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008F7 RID: 2295
	internal interface IOutboundConnector : IEntity<IOutboundConnector>
	{
		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x0600515B RID: 20827
		string Name { get; }

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x0600515C RID: 20828
		string TlsDomain { get; }

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x0600515D RID: 20829
		TenantConnectorType ConnectorType { get; }

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x0600515E RID: 20830
		// (set) Token: 0x0600515F RID: 20831
		TenantConnectorSource ConnectorSource { get; set; }

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x06005160 RID: 20832
		MultiValuedProperty<SmtpDomainWithSubdomains> RecipientDomains { get; }

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x06005161 RID: 20833
		MultiValuedProperty<SmartHost> SmartHosts { get; }

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x06005162 RID: 20834
		TlsAuthLevel? TlsSettings { get; }

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x06005163 RID: 20835
		// (set) Token: 0x06005164 RID: 20836
		bool CloudServicesMailEnabled { get; set; }

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x06005165 RID: 20837
		// (set) Token: 0x06005166 RID: 20838
		bool RouteAllMessagesViaOnPremises { get; set; }
	}
}
