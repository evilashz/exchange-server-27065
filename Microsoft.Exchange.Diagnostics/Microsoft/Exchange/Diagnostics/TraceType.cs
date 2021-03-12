using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000079 RID: 121
	public enum TraceType
	{
		// Token: 0x0400026D RID: 621
		None,
		// Token: 0x0400026E RID: 622
		DebugTrace,
		// Token: 0x0400026F RID: 623
		Debug = 1,
		// Token: 0x04000270 RID: 624
		WarningTrace,
		// Token: 0x04000271 RID: 625
		Warning = 2,
		// Token: 0x04000272 RID: 626
		ErrorTrace,
		// Token: 0x04000273 RID: 627
		Error = 3,
		// Token: 0x04000274 RID: 628
		FatalTrace,
		// Token: 0x04000275 RID: 629
		Fatal = 4,
		// Token: 0x04000276 RID: 630
		InfoTrace,
		// Token: 0x04000277 RID: 631
		Info = 5,
		// Token: 0x04000278 RID: 632
		PerformanceTrace,
		// Token: 0x04000279 RID: 633
		Performance = 6,
		// Token: 0x0400027A RID: 634
		FunctionTrace,
		// Token: 0x0400027B RID: 635
		Function = 7,
		// Token: 0x0400027C RID: 636
		PfdTrace,
		// Token: 0x0400027D RID: 637
		Pfd = 8,
		// Token: 0x0400027E RID: 638
		FaultInjection,
		// Token: 0x0400027F RID: 639
		FaultI = 9
	}
}
