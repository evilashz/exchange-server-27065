using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200004A RID: 74
	public abstract class AcceptedDomainCollection : IEnumerable<AcceptedDomain>, IEnumerable
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x000063B1 File Offset: 0x000045B1
		public AcceptedDomain Find(RoutingAddress smtpAddress)
		{
			return this.Find(smtpAddress.DomainPart);
		}

		// Token: 0x060001B5 RID: 437
		public abstract AcceptedDomain Find(string domainName);

		// Token: 0x060001B6 RID: 438
		public abstract IEnumerator<AcceptedDomain> GetEnumerator();

		// Token: 0x060001B7 RID: 439 RVA: 0x000063C0 File Offset: 0x000045C0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
