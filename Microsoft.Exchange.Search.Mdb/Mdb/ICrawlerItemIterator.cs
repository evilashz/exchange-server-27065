using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000005 RID: 5
	internal interface ICrawlerItemIterator<TSort> where TSort : struct, IComparable<TSort>
	{
		// Token: 0x0600000E RID: 14
		IEnumerable<MdbItemIdentity> GetItems(StoreSession session, TSort intervalStart, TSort intervalStop);
	}
}
