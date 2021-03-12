using System;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000286 RID: 646
	public enum ViewFilter
	{
		// Token: 0x04000C47 RID: 3143
		All,
		// Token: 0x04000C48 RID: 3144
		Flagged,
		// Token: 0x04000C49 RID: 3145
		HasAttachment,
		// Token: 0x04000C4A RID: 3146
		ToOrCcMe,
		// Token: 0x04000C4B RID: 3147
		Unread,
		// Token: 0x04000C4C RID: 3148
		TaskActive,
		// Token: 0x04000C4D RID: 3149
		TaskOverdue,
		// Token: 0x04000C4E RID: 3150
		TaskCompleted,
		// Token: 0x04000C4F RID: 3151
		DeprecatedSuggestions,
		// Token: 0x04000C50 RID: 3152
		DeprecatedSuggestionsRespond,
		// Token: 0x04000C51 RID: 3153
		DeprecatedSuggestionsDelete,
		// Token: 0x04000C52 RID: 3154
		NoClutter,
		// Token: 0x04000C53 RID: 3155
		Clutter
	}
}
