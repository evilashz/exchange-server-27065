using System;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000034 RID: 52
	[Flags]
	internal enum RecurrenceFixupType
	{
		// Token: 0x0400012F RID: 303
		None = 0,
		// Token: 0x04000130 RID: 304
		RecurPattern = 1,
		// Token: 0x04000131 RID: 305
		BlobAndExceptions = 2,
		// Token: 0x04000132 RID: 306
		JustExceptions = 4
	}
}
