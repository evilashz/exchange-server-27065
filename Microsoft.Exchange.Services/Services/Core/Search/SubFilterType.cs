using System;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000284 RID: 644
	internal enum SubFilterType
	{
		// Token: 0x04000C3E RID: 3134
		None,
		// Token: 0x04000C3F RID: 3135
		RecipientTo,
		// Token: 0x04000C40 RID: 3136
		RecipientCc,
		// Token: 0x04000C41 RID: 3137
		RecipientBcc,
		// Token: 0x04000C42 RID: 3138
		AttendeeRequired,
		// Token: 0x04000C43 RID: 3139
		AttendeeOptional,
		// Token: 0x04000C44 RID: 3140
		AttendeeResource,
		// Token: 0x04000C45 RID: 3141
		Attachment
	}
}
