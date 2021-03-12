using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200007C RID: 124
	public abstract class RemoteDomainCollection : IEnumerable<RemoteDomain>, IEnumerable
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x00007207 File Offset: 0x00005407
		public RemoteDomain Find(RoutingAddress smtpAddress)
		{
			return this.Find(smtpAddress.DomainPart);
		}

		// Token: 0x060002B9 RID: 697
		public abstract RemoteDomain Find(string domainName);

		// Token: 0x060002BA RID: 698
		public abstract IEnumerator<RemoteDomain> GetEnumerator();

		// Token: 0x060002BB RID: 699 RVA: 0x00007216 File Offset: 0x00005416
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
