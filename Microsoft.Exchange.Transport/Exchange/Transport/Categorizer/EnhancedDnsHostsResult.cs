using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200022D RID: 557
	internal struct EnhancedDnsHostsResult
	{
		// Token: 0x060018CE RID: 6350 RVA: 0x00063FB1 File Offset: 0x000621B1
		public EnhancedDnsHostsResult(EnhancedDnsTargetHost[] hosts, EnhancedDnsRequestContext requestContext)
		{
			this.Hosts = hosts;
			this.RequestContext = requestContext;
		}

		// Token: 0x04000BE6 RID: 3046
		public readonly EnhancedDnsTargetHost[] Hosts;

		// Token: 0x04000BE7 RID: 3047
		public readonly EnhancedDnsRequestContext RequestContext;
	}
}
