using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008EE RID: 2286
	internal interface IInboundConnector : IEntity<IInboundConnector>
	{
		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x060050F9 RID: 20729
		string Name { get; }

		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x060050FA RID: 20730
		string TLSSenderCertificateName { get; }

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x060050FB RID: 20731
		TenantConnectorType ConnectorType { get; }

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x060050FC RID: 20732
		// (set) Token: 0x060050FD RID: 20733
		TenantConnectorSource ConnectorSource { get; set; }

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x060050FE RID: 20734
		MultiValuedProperty<AddressSpace> SenderDomains { get; }

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x060050FF RID: 20735
		bool RequireTls { get; }

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x06005100 RID: 20736
		// (set) Token: 0x06005101 RID: 20737
		bool CloudServicesMailEnabled { get; set; }
	}
}
