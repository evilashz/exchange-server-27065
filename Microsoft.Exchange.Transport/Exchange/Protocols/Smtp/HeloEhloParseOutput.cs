using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000479 RID: 1145
	internal sealed class HeloEhloParseOutput
	{
		// Token: 0x0600349C RID: 13468 RVA: 0x000D6A7F File Offset: 0x000D4C7F
		public HeloEhloParseOutput(string heloDomain, SmtpReceiveCapabilities tlsDomainCapabilities)
		{
			this.HeloDomain = heloDomain;
			this.TlsDomainCapabilities = tlsDomainCapabilities;
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x000D6A95 File Offset: 0x000D4C95
		// (set) Token: 0x0600349E RID: 13470 RVA: 0x000D6A9D File Offset: 0x000D4C9D
		public string HeloDomain { get; private set; }

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000D6AA6 File Offset: 0x000D4CA6
		// (set) Token: 0x060034A0 RID: 13472 RVA: 0x000D6AAE File Offset: 0x000D4CAE
		public SmtpReceiveCapabilities TlsDomainCapabilities { get; private set; }
	}
}
