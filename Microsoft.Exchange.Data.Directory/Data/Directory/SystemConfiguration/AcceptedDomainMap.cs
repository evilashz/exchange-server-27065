using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002FA RID: 762
	internal class AcceptedDomainMap : AcceptedDomainCollection
	{
		// Token: 0x06002380 RID: 9088 RVA: 0x00099FD4 File Offset: 0x000981D4
		public AcceptedDomainMap(IList<AcceptedDomainEntry> acceptedDomainEntryList)
		{
			this.map = new DomainMatchMap<AcceptedDomainEntry>(acceptedDomainEntryList ?? ((IList<AcceptedDomainEntry>)AcceptedDomainMap.EmptyAcceptedDomainEntryList));
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x00099FF8 File Offset: 0x000981F8
		public bool CheckInternal(SmtpDomain domain)
		{
			AcceptedDomainEntry domainEntry = this.GetDomainEntry(domain);
			return domainEntry != null && domainEntry.IsInternal;
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x0009A018 File Offset: 0x00098218
		public AcceptedDomainEntry GetDomainEntry(SmtpDomain domain)
		{
			return this.map.GetBestMatch(domain);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x0009A026 File Offset: 0x00098226
		public bool CheckAccepted(SmtpDomain domain)
		{
			return this.GetDomainEntry(domain) != null;
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x0009A038 File Offset: 0x00098238
		public bool CheckAuthoritative(SmtpDomain domain)
		{
			AcceptedDomainEntry domainEntry = this.GetDomainEntry(domain);
			return domainEntry != null && domainEntry.IsAuthoritative;
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x0009A058 File Offset: 0x00098258
		public override AcceptedDomain Find(string domainName)
		{
			return this.map.GetBestMatch(domainName);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0009A066 File Offset: 0x00098266
		public override IEnumerator<AcceptedDomain> GetEnumerator()
		{
			return this.map.GetAllDomains<AcceptedDomain>();
		}

		// Token: 0x04001619 RID: 5657
		private static readonly AcceptedDomainEntry[] EmptyAcceptedDomainEntryList = new AcceptedDomainEntry[0];

		// Token: 0x0400161A RID: 5658
		private readonly DomainMatchMap<AcceptedDomainEntry> map;
	}
}
