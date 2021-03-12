using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000900 RID: 2304
	internal interface ISendConnector : IEntity<ISendConnector>
	{
		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x060051B3 RID: 20915
		string Name { get; }

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x060051B4 RID: 20916
		MultiValuedProperty<AddressSpace> AddressSpaces { get; }

		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x060051B5 RID: 20917
		MultiValuedProperty<ADObjectId> SourceTransportServers { get; }

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x060051B6 RID: 20918
		bool DNSRoutingEnabled { get; }

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x060051B7 RID: 20919
		MultiValuedProperty<SmartHost> SmartHosts { get; }

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x060051B8 RID: 20920
		bool RequireTLS { get; }

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x060051B9 RID: 20921
		TlsAuthLevel? TlsAuthLevel { get; }

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x060051BA RID: 20922
		string TlsDomain { get; }

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x060051BB RID: 20923
		ErrorPolicies ErrorPolicies { get; }

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x060051BC RID: 20924
		SmtpX509Identifier TlsCertificateName { get; }

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x060051BD RID: 20925
		// (set) Token: 0x060051BE RID: 20926
		bool CloudServicesMailEnabled { get; set; }

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x060051BF RID: 20927
		string Fqdn { get; }
	}
}
