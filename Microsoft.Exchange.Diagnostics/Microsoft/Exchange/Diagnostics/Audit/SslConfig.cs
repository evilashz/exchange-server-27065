using System;

namespace Microsoft.Exchange.Diagnostics.Audit
{
	// Token: 0x020001A0 RID: 416
	public sealed class SslConfig
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0002A320 File Offset: 0x00028520
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x0002A328 File Offset: 0x00028528
		public bool AllowInternalUntrustedCerts { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0002A331 File Offset: 0x00028531
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x0002A339 File Offset: 0x00028539
		public bool AllowExternalUntrustedCerts { get; set; }
	}
}
