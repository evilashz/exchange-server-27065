using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200008B RID: 139
	public enum PredictedAction : short
	{
		// Token: 0x040001EA RID: 490
		Respond = 10,
		// Token: 0x040001EB RID: 491
		FollowUp = 20,
		// Token: 0x040001EC RID: 492
		MoveToFolder = 30,
		// Token: 0x040001ED RID: 493
		Delete = 40,
		// Token: 0x040001EE RID: 494
		ActionCompleted = 50,
		// Token: 0x040001EF RID: 495
		Read = 60,
		// Token: 0x040001F0 RID: 496
		Ignore = 70,
		// Token: 0x040001F1 RID: 497
		None = 10000
	}
}
