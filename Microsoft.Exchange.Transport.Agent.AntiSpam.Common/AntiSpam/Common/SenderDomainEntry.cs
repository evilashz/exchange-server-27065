using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x02000017 RID: 23
	internal sealed class SenderDomainEntry : DomainMatchMap<SenderDomainEntry>.IDomainEntry
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000048E5 File Offset: 0x00002AE5
		public SenderDomainEntry(SmtpDomainWithSubdomains domain)
		{
			this.domain = domain;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000048F4 File Offset: 0x00002AF4
		public SenderDomainEntry(SmtpDomain domain, bool includeSubdomains)
		{
			this.domain = new SmtpDomainWithSubdomains(domain, includeSubdomains);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004909 File Offset: 0x00002B09
		public SmtpDomainWithSubdomains DomainName
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x04000062 RID: 98
		private readonly SmtpDomainWithSubdomains domain;
	}
}
