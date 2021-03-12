using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200000E RID: 14
	internal interface IItemIdList
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004D RID: 77
		string SourceId { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004E RID: 78
		IList<ItemId> MemoryCache { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004F RID: 79
		bool Exists { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000050 RID: 80
		bool IsUnsearchable { get; }

		// Token: 0x06000051 RID: 81
		void WriteItemId(ItemId itemId);

		// Token: 0x06000052 RID: 82
		void Flush();

		// Token: 0x06000053 RID: 83
		IEnumerable<ItemId> ReadItemIds();
	}
}
