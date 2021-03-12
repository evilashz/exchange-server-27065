using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002F9 RID: 761
	internal class X400AuthoritativeDomainEntry
	{
		// Token: 0x06002187 RID: 8583 RVA: 0x0007F191 File Offset: 0x0007D391
		public X400AuthoritativeDomainEntry(X400AuthoritativeDomain authoritativeDomain)
		{
			this.externalRelay = authoritativeDomain.X400ExternalRelay;
			this.domain = authoritativeDomain.X400DomainName;
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x0007F1B1 File Offset: 0x0007D3B1
		public X400Domain Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x0007F1B9 File Offset: 0x0007D3B9
		public bool ExternalRelay
		{
			get
			{
				return this.externalRelay;
			}
		}

		// Token: 0x0400119D RID: 4509
		private X400Domain domain;

		// Token: 0x0400119E RID: 4510
		private bool externalRelay;
	}
}
