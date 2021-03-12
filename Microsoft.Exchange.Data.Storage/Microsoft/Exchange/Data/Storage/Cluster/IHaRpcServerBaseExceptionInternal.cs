using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x020000C0 RID: 192
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IHaRpcServerBaseExceptionInternal
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001234 RID: 4660
		string MessageInternal { get; }

		// Token: 0x17000627 RID: 1575
		// (set) Token: 0x06001235 RID: 4661
		string OriginatingServer { set; }

		// Token: 0x17000628 RID: 1576
		// (set) Token: 0x06001236 RID: 4662
		string OriginatingStackTrace { set; }

		// Token: 0x17000629 RID: 1577
		// (set) Token: 0x06001237 RID: 4663
		string DatabaseName { set; }
	}
}
