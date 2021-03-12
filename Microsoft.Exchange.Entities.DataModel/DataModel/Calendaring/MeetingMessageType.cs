using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000065 RID: 101
	public enum MeetingMessageType
	{
		// Token: 0x0400016A RID: 362
		Unknown,
		// Token: 0x0400016B RID: 363
		SingleInstanceRequest,
		// Token: 0x0400016C RID: 364
		SingleInstanceResponse,
		// Token: 0x0400016D RID: 365
		SingleInstanceCancel,
		// Token: 0x0400016E RID: 366
		SingleInstanceForwardNotification,
		// Token: 0x0400016F RID: 367
		SeriesRequest,
		// Token: 0x04000170 RID: 368
		SeriesResponse,
		// Token: 0x04000171 RID: 369
		SeriesCancel,
		// Token: 0x04000172 RID: 370
		SeriesForwardNotification
	}
}
