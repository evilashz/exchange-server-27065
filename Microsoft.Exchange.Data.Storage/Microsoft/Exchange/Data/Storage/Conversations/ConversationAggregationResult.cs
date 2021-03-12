using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008A0 RID: 2208
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationAggregationResult
	{
		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x00159774 File Offset: 0x00157974
		// (set) Token: 0x060052A7 RID: 21159 RVA: 0x0015977C File Offset: 0x0015797C
		public bool SupportsSideConversation { get; set; }

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x060052A8 RID: 21160 RVA: 0x00159785 File Offset: 0x00157985
		// (set) Token: 0x060052A9 RID: 21161 RVA: 0x0015978D File Offset: 0x0015798D
		public ConversationIndex.FixupStage Stage { get; set; }

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x060052AA RID: 21162 RVA: 0x00159796 File Offset: 0x00157996
		// (set) Token: 0x060052AB RID: 21163 RVA: 0x0015979E File Offset: 0x0015799E
		public ConversationId ConversationFamilyId { get; set; }

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x001597A7 File Offset: 0x001579A7
		// (set) Token: 0x060052AD RID: 21165 RVA: 0x001597AF File Offset: 0x001579AF
		public ConversationIndex ConversationIndex { get; set; }
	}
}
