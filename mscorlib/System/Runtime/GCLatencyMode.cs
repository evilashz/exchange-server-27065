using System;

namespace System.Runtime
{
	// Token: 0x020006E9 RID: 1769
	[__DynamicallyInvokable]
	[Serializable]
	public enum GCLatencyMode
	{
		// Token: 0x0400233B RID: 9019
		[__DynamicallyInvokable]
		Batch,
		// Token: 0x0400233C RID: 9020
		[__DynamicallyInvokable]
		Interactive,
		// Token: 0x0400233D RID: 9021
		[__DynamicallyInvokable]
		LowLatency,
		// Token: 0x0400233E RID: 9022
		[__DynamicallyInvokable]
		SustainedLowLatency,
		// Token: 0x0400233F RID: 9023
		NoGCRegion
	}
}
