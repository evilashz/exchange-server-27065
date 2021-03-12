using System;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009DF RID: 2527
	public sealed class FederationTrustCertificateStatus
	{
		// Token: 0x17001B01 RID: 6913
		// (get) Token: 0x06005A52 RID: 23122 RVA: 0x0017A0C2 File Offset: 0x001782C2
		public ADSite Site
		{
			get
			{
				return this.site;
			}
		}

		// Token: 0x17001B02 RID: 6914
		// (get) Token: 0x06005A53 RID: 23123 RVA: 0x0017A0CA File Offset: 0x001782CA
		internal TopologyServer Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17001B03 RID: 6915
		// (get) Token: 0x06005A54 RID: 23124 RVA: 0x0017A0D2 File Offset: 0x001782D2
		public FederationTrustCertificateState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17001B04 RID: 6916
		// (get) Token: 0x06005A55 RID: 23125 RVA: 0x0017A0DA File Offset: 0x001782DA
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x0017A0E2 File Offset: 0x001782E2
		internal FederationTrustCertificateStatus(ADSite site, TopologyServer server, FederationTrustCertificateState state, string thumbprint)
		{
			this.site = site;
			this.server = server;
			this.state = state;
			this.thumbprint = thumbprint;
		}

		// Token: 0x040033C1 RID: 13249
		private ADSite site;

		// Token: 0x040033C2 RID: 13250
		private TopologyServer server;

		// Token: 0x040033C3 RID: 13251
		private FederationTrustCertificateState state;

		// Token: 0x040033C4 RID: 13252
		private readonly string thumbprint;
	}
}
