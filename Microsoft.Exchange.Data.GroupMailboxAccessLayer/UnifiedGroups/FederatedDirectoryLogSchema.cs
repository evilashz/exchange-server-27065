using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class FederatedDirectoryLogSchema
	{
		// Token: 0x02000006 RID: 6
		internal enum AssertTag
		{
			// Token: 0x0400000A RID: 10
			ActivityId,
			// Token: 0x0400000B RID: 11
			Message
		}

		// Token: 0x02000007 RID: 7
		internal enum ExceptionTag
		{
			// Token: 0x0400000D RID: 13
			TaskName,
			// Token: 0x0400000E RID: 14
			ActivityId,
			// Token: 0x0400000F RID: 15
			ExceptionType,
			// Token: 0x04000010 RID: 16
			ExceptionDetail,
			// Token: 0x04000011 RID: 17
			CurrentAction,
			// Token: 0x04000012 RID: 18
			Message
		}

		// Token: 0x02000008 RID: 8
		internal enum ShipAssertTag
		{
			// Token: 0x04000014 RID: 20
			ActivityId,
			// Token: 0x04000015 RID: 21
			Message
		}

		// Token: 0x02000009 RID: 9
		internal enum TraceTag
		{
			// Token: 0x04000017 RID: 23
			TaskName,
			// Token: 0x04000018 RID: 24
			ActivityId,
			// Token: 0x04000019 RID: 25
			CurrentAction,
			// Token: 0x0400001A RID: 26
			Message
		}
	}
}
