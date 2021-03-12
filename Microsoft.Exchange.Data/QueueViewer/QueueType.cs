using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000288 RID: 648
	[Serializable]
	public enum QueueType
	{
		// Token: 0x04000D5B RID: 3419
		Undefined,
		// Token: 0x04000D5C RID: 3420
		Delivery,
		// Token: 0x04000D5D RID: 3421
		Poison,
		// Token: 0x04000D5E RID: 3422
		Submission,
		// Token: 0x04000D5F RID: 3423
		Unreachable,
		// Token: 0x04000D60 RID: 3424
		Shadow
	}
}
