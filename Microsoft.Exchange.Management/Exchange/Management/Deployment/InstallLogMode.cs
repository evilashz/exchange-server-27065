using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020002A7 RID: 679
	public enum InstallLogMode
	{
		// Token: 0x04000A59 RID: 2649
		None,
		// Token: 0x04000A5A RID: 2650
		FatalExit,
		// Token: 0x04000A5B RID: 2651
		Error,
		// Token: 0x04000A5C RID: 2652
		Warning = 4,
		// Token: 0x04000A5D RID: 2653
		User = 8,
		// Token: 0x04000A5E RID: 2654
		Info = 16,
		// Token: 0x04000A5F RID: 2655
		ResolveSource = 64,
		// Token: 0x04000A60 RID: 2656
		OutOfDiskSpace = 128,
		// Token: 0x04000A61 RID: 2657
		ActionStart = 256,
		// Token: 0x04000A62 RID: 2658
		ActionData = 512,
		// Token: 0x04000A63 RID: 2659
		CommonData = 2048,
		// Token: 0x04000A64 RID: 2660
		PropertyDump = 1024,
		// Token: 0x04000A65 RID: 2661
		Verbose = 4096,
		// Token: 0x04000A66 RID: 2662
		ExtraDebug = 8192,
		// Token: 0x04000A67 RID: 2663
		Progress = 1024,
		// Token: 0x04000A68 RID: 2664
		Initialize = 4096,
		// Token: 0x04000A69 RID: 2665
		Terminate = 8192,
		// Token: 0x04000A6A RID: 2666
		ShowDialog = 16384,
		// Token: 0x04000A6B RID: 2667
		All = 16383
	}
}
