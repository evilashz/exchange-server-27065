using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000D6 RID: 214
	public class EntryIdMap<TValue> : Dictionary<byte[], TValue>
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x0000D726 File Offset: 0x0000B926
		public EntryIdMap() : base(ArrayComparer<byte>.EqualityComparer)
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0000D733 File Offset: 0x0000B933
		public EntryIdMap(int capacity) : base(capacity, ArrayComparer<byte>.EqualityComparer)
		{
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0000D744 File Offset: 0x0000B944
		public byte[][] EntryIds
		{
			get
			{
				List<byte[]> list = new List<byte[]>(base.Count);
				list.AddRange(base.Keys);
				return list.ToArray();
			}
		}
	}
}
