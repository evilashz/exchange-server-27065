using System;
using System.Net.Security;

namespace Microsoft.Exchange.Diagnostics.Audit
{
	// Token: 0x0200019F RID: 415
	public sealed class SslError
	{
		// Token: 0x06000B8B RID: 2955 RVA: 0x0002A294 File Offset: 0x00028494
		public SslError()
		{
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002A29C File Offset: 0x0002849C
		public SslError(SslPolicyErrors sslPolicyErrors, bool allowInternalUntrustedCerts, bool allowExternalUntrustedCerts)
		{
			this.Timestamp = DateTime.UtcNow;
			this.SslPolicyErrors = sslPolicyErrors;
			this.SslConfig = new SslConfig
			{
				AllowInternalUntrustedCerts = allowInternalUntrustedCerts,
				AllowExternalUntrustedCerts = allowExternalUntrustedCerts
			};
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0002A2DC File Offset: 0x000284DC
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x0002A2E4 File Offset: 0x000284E4
		public long Index { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002A2ED File Offset: 0x000284ED
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x0002A2F5 File Offset: 0x000284F5
		public DateTime Timestamp { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0002A2FE File Offset: 0x000284FE
		// (set) Token: 0x06000B92 RID: 2962 RVA: 0x0002A306 File Offset: 0x00028506
		public SslPolicyErrors SslPolicyErrors { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0002A30F File Offset: 0x0002850F
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x0002A317 File Offset: 0x00028517
		public SslConfig SslConfig { get; set; }
	}
}
