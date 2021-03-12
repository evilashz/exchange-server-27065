using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000089 RID: 137
	public enum PredictedMessageAction : short
	{
		// Token: 0x040001CF RID: 463
		[Description("ReplyOrForward")]
		ReplyOrForward,
		// Token: 0x040001D0 RID: 464
		[Description("Flag")]
		Flag,
		// Token: 0x040001D1 RID: 465
		[Description("Delete")]
		Delete,
		// Token: 0x040001D2 RID: 466
		[Description("MarkUnread")]
		MarkUnread,
		// Token: 0x040001D3 RID: 467
		[Description("Ignore")]
		Ignore,
		// Token: 0x040001D4 RID: 468
		[Description("Clutter")]
		Clutter,
		// Token: 0x040001D5 RID: 469
		[Description("Max")]
		Max
	}
}
