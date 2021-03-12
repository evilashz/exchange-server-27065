using System;
using System.Net;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200022C RID: 556
	internal struct EnhancedDnsStatusResult
	{
		// Token: 0x060018CD RID: 6349 RVA: 0x00063F92 File Offset: 0x00062192
		public EnhancedDnsStatusResult(DnsStatus status, IPAddress server, EnhancedDnsRequestContext requestContext, string diagnosticInfo)
		{
			this.Status = status;
			this.Server = server;
			this.RequestContext = requestContext;
			this.DiagnosticInfo = diagnosticInfo;
		}

		// Token: 0x04000BE2 RID: 3042
		public readonly DnsStatus Status;

		// Token: 0x04000BE3 RID: 3043
		public readonly IPAddress Server;

		// Token: 0x04000BE4 RID: 3044
		public readonly EnhancedDnsRequestContext RequestContext;

		// Token: 0x04000BE5 RID: 3045
		public readonly string DiagnosticInfo;
	}
}
