using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200008A RID: 138
	public enum MessageActionLabel
	{
		// Token: 0x040001D7 RID: 471
		[Description("ReplyForwardFlag")]
		ReplyForwardFlag,
		// Token: 0x040001D8 RID: 472
		[Description("DeleteFromClutter")]
		DeleteFromClutter,
		// Token: 0x040001D9 RID: 473
		[Description("DeleteFromInboxWithoutReading")]
		DeleteFromInboxWithoutReading,
		// Token: 0x040001DA RID: 474
		[Description("DeleteFromInboxAfterReading")]
		DeleteFromInboxAfterReading,
		// Token: 0x040001DB RID: 475
		[Description("MarkAsClutter")]
		MarkAsClutter,
		// Token: 0x040001DC RID: 476
		[Description("MarkAsNotClutter")]
		MarkAsNotClutter,
		// Token: 0x040001DD RID: 477
		[Description("ReadInClutter")]
		ReadInClutter,
		// Token: 0x040001DE RID: 478
		[Description("ReadInInbox")]
		ReadInInbox,
		// Token: 0x040001DF RID: 479
		[Description("IgnoreInClutter")]
		IgnoreInClutter,
		// Token: 0x040001E0 RID: 480
		[Description("IgnoreInInbox")]
		IgnoreInInbox,
		// Token: 0x040001E1 RID: 481
		[Description("MarkAsUnreadInClutter")]
		MarkAsUnreadInClutter,
		// Token: 0x040001E2 RID: 482
		[Description("MarkAsUnreadInInbox")]
		MarkAsUnreadInInbox,
		// Token: 0x040001E3 RID: 483
		[Description("ReadMultipleTimesInClutter")]
		ReadMultipleTimesInClutter,
		// Token: 0x040001E4 RID: 484
		[Description("ReadMultipleTimesInInbox")]
		ReadMultipleTimesInInbox,
		// Token: 0x040001E5 RID: 485
		[Description("Moved")]
		Moved,
		// Token: 0x040001E6 RID: 486
		[Description("CachedClutter")]
		CachedClutter,
		// Token: 0x040001E7 RID: 487
		[Description("CachedNotClutter")]
		CachedNotClutter,
		// Token: 0x040001E8 RID: 488
		[Description("OnVacation")]
		OnVacation
	}
}
