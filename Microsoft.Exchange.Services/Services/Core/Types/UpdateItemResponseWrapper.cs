using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200057B RID: 1403
	internal class UpdateItemResponseWrapper
	{
		// Token: 0x0600270F RID: 9999 RVA: 0x000A6E00 File Offset: 0x000A5000
		internal UpdateItemResponseWrapper(ItemType item, ConflictResults conflictResults)
		{
			this.Item = item;
			this.ConflictResults = conflictResults;
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002710 RID: 10000 RVA: 0x000A6E16 File Offset: 0x000A5016
		// (set) Token: 0x06002711 RID: 10001 RVA: 0x000A6E1E File Offset: 0x000A501E
		internal ItemType Item { get; set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002712 RID: 10002 RVA: 0x000A6E27 File Offset: 0x000A5027
		// (set) Token: 0x06002713 RID: 10003 RVA: 0x000A6E2F File Offset: 0x000A502F
		internal ConflictResults ConflictResults { get; set; }
	}
}
