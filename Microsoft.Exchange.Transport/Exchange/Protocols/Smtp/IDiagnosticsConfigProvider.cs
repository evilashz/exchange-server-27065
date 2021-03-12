using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004CB RID: 1227
	internal interface IDiagnosticsConfigProvider
	{
		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x06003887 RID: 14471
		TimeSpan SmtpRecvLogAsyncInterval { get; }

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x06003888 RID: 14472
		int SmtpRecvLogBufferSize { get; }

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06003889 RID: 14473
		TimeSpan SmtpRecvLogFlushInterval { get; }
	}
}
