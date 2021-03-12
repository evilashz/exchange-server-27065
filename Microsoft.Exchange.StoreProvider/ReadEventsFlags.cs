using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200008A RID: 138
	[Flags]
	internal enum ReadEventsFlags
	{
		// Token: 0x04000561 RID: 1377
		None = 0,
		// Token: 0x04000562 RID: 1378
		FailIfEventsDeleted = 1,
		// Token: 0x04000563 RID: 1379
		IncludeMoveDestinationEvents = 2
	}
}
