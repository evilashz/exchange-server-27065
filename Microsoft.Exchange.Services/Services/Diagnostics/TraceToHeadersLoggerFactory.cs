using System;
using System.Collections.Specialized;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200005C RID: 92
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TraceToHeadersLoggerFactory
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000B9EF File Offset: 0x00009BEF
		public TraceToHeadersLoggerFactory(bool enabled)
		{
			this.enabled = enabled;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000B9FE File Offset: 0x00009BFE
		public ITraceLogger Create(NameValueCollection headers)
		{
			if (!this.enabled)
			{
				return NullTraceLogger.Instance;
			}
			return new TraceToHeadersLogger(headers);
		}

		// Token: 0x04000517 RID: 1303
		private readonly bool enabled;
	}
}
