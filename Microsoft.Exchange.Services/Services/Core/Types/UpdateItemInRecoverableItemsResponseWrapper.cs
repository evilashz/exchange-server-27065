using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000578 RID: 1400
	internal class UpdateItemInRecoverableItemsResponseWrapper
	{
		// Token: 0x060026FF RID: 9983 RVA: 0x000A6CFF File Offset: 0x000A4EFF
		internal UpdateItemInRecoverableItemsResponseWrapper(ItemType item, AttachmentType[] attachments, ConflictResults conflictResults)
		{
			this.Item = item;
			this.Attachments = attachments;
			this.ConflictResults = conflictResults;
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x000A6D1C File Offset: 0x000A4F1C
		// (set) Token: 0x06002701 RID: 9985 RVA: 0x000A6D24 File Offset: 0x000A4F24
		internal ItemType Item { get; set; }

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x000A6D2D File Offset: 0x000A4F2D
		// (set) Token: 0x06002703 RID: 9987 RVA: 0x000A6D35 File Offset: 0x000A4F35
		internal AttachmentType[] Attachments { get; set; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x000A6D3E File Offset: 0x000A4F3E
		// (set) Token: 0x06002705 RID: 9989 RVA: 0x000A6D46 File Offset: 0x000A4F46
		internal ConflictResults ConflictResults { get; set; }
	}
}
