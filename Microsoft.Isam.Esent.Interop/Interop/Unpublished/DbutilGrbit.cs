using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000075 RID: 117
	[Flags]
	public enum DbutilGrbit
	{
		// Token: 0x0400026E RID: 622
		None = 0,
		// Token: 0x0400026F RID: 623
		OptionAllNodes = 1,
		// Token: 0x04000270 RID: 624
		OptionKeyStats = 2,
		// Token: 0x04000271 RID: 625
		OptionPageDump = 4,
		// Token: 0x04000272 RID: 626
		OptionStats = 8,
		// Token: 0x04000273 RID: 627
		OptionSuppressConsoleOutput = 16,
		// Token: 0x04000274 RID: 628
		OptionIgnoreErrors = 32,
		// Token: 0x04000275 RID: 629
		OptionVerify = 64,
		// Token: 0x04000276 RID: 630
		OptionReportErrors = 128,
		// Token: 0x04000277 RID: 631
		OptionDontRepair = 256,
		// Token: 0x04000278 RID: 632
		OptionRepairAll = 512,
		// Token: 0x04000279 RID: 633
		OptionRepairIndexes = 1024,
		// Token: 0x0400027A RID: 634
		OptionDontBuildIndexes = 2048,
		// Token: 0x0400027B RID: 635
		OptionSuppressLogo = 32768,
		// Token: 0x0400027C RID: 636
		OptionRepairCheckOnly = 65536,
		// Token: 0x0400027D RID: 637
		OptionDumpLVPageUsage = 131072,
		// Token: 0x0400027E RID: 638
		OptionDumpLogInfoCSV = 262144,
		// Token: 0x0400027F RID: 639
		OptionDumpLogPermitPatching = 524288,
		// Token: 0x04000280 RID: 640
		OptionDumpVerbose = 268435456,
		// Token: 0x04000281 RID: 641
		OptionCheckBTree = 536870912
	}
}
