using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007BD RID: 1981
	internal enum OwaViewFilter
	{
		// Token: 0x04002847 RID: 10311
		All,
		// Token: 0x04002848 RID: 10312
		Flagged,
		// Token: 0x04002849 RID: 10313
		HasAttachment,
		// Token: 0x0400284A RID: 10314
		ToOrCcMe,
		// Token: 0x0400284B RID: 10315
		Unread,
		// Token: 0x0400284C RID: 10316
		TaskActive,
		// Token: 0x0400284D RID: 10317
		TaskOverdue,
		// Token: 0x0400284E RID: 10318
		TaskCompleted,
		// Token: 0x0400284F RID: 10319
		DeprecatedSuggestions,
		// Token: 0x04002850 RID: 10320
		DeprecatedSuggestionsRespond,
		// Token: 0x04002851 RID: 10321
		DeprecatedSuggestionsDelete,
		// Token: 0x04002852 RID: 10322
		NoClutter,
		// Token: 0x04002853 RID: 10323
		Clutter
	}
}
