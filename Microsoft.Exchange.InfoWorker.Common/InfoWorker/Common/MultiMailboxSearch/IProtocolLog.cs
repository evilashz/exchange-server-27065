using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001EC RID: 492
	internal interface IProtocolLog : IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06000CE1 RID: 3297
		void Add(string key, object value);

		// Token: 0x06000CE2 RID: 3298
		void Merge(IProtocolLog other);
	}
}
