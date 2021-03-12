using System;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006EA RID: 1770
	internal enum SharepointFieldType
	{
		// Token: 0x04002666 RID: 9830
		Invalid,
		// Token: 0x04002667 RID: 9831
		Integer,
		// Token: 0x04002668 RID: 9832
		Text,
		// Token: 0x04002669 RID: 9833
		Note,
		// Token: 0x0400266A RID: 9834
		DateTime,
		// Token: 0x0400266B RID: 9835
		Counter,
		// Token: 0x0400266C RID: 9836
		Choice,
		// Token: 0x0400266D RID: 9837
		Lookup,
		// Token: 0x0400266E RID: 9838
		Boolean,
		// Token: 0x0400266F RID: 9839
		Number,
		// Token: 0x04002670 RID: 9840
		Currency,
		// Token: 0x04002671 RID: 9841
		URL,
		// Token: 0x04002672 RID: 9842
		Computed,
		// Token: 0x04002673 RID: 9843
		Threading,
		// Token: 0x04002674 RID: 9844
		Guid,
		// Token: 0x04002675 RID: 9845
		MultiChoice,
		// Token: 0x04002676 RID: 9846
		GridChoice,
		// Token: 0x04002677 RID: 9847
		Calculated,
		// Token: 0x04002678 RID: 9848
		File,
		// Token: 0x04002679 RID: 9849
		Attachments,
		// Token: 0x0400267A RID: 9850
		User,
		// Token: 0x0400267B RID: 9851
		Recurrence,
		// Token: 0x0400267C RID: 9852
		CrossProjectLink,
		// Token: 0x0400267D RID: 9853
		ModStat,
		// Token: 0x0400267E RID: 9854
		Error,
		// Token: 0x0400267F RID: 9855
		MaxItems
	}
}
