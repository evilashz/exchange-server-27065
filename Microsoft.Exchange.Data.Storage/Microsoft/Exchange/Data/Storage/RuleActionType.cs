using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BD3 RID: 3027
	internal enum RuleActionType : byte
	{
		// Token: 0x04003D90 RID: 15760
		Move = 1,
		// Token: 0x04003D91 RID: 15761
		Copy,
		// Token: 0x04003D92 RID: 15762
		Reply,
		// Token: 0x04003D93 RID: 15763
		OutOfOfficeReply,
		// Token: 0x04003D94 RID: 15764
		DeferAction,
		// Token: 0x04003D95 RID: 15765
		Bounce,
		// Token: 0x04003D96 RID: 15766
		Forward,
		// Token: 0x04003D97 RID: 15767
		Delegate,
		// Token: 0x04003D98 RID: 15768
		Tag,
		// Token: 0x04003D99 RID: 15769
		Delete,
		// Token: 0x04003D9A RID: 15770
		MarkAsRead
	}
}
