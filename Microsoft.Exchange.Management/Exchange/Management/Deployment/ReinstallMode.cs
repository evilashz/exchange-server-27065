using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020002A5 RID: 677
	[Flags]
	internal enum ReinstallMode
	{
		// Token: 0x04000A3D RID: 2621
		Repair = 1,
		// Token: 0x04000A3E RID: 2622
		FileMissing = 2,
		// Token: 0x04000A3F RID: 2623
		FileOlderVersion = 4,
		// Token: 0x04000A40 RID: 2624
		FileEqualVersion = 8,
		// Token: 0x04000A41 RID: 2625
		FileExact = 16,
		// Token: 0x04000A42 RID: 2626
		FileVerify = 32,
		// Token: 0x04000A43 RID: 2627
		FileReplace = 64,
		// Token: 0x04000A44 RID: 2628
		MachineData = 128,
		// Token: 0x04000A45 RID: 2629
		UserData = 256,
		// Token: 0x04000A46 RID: 2630
		ShortCut = 512,
		// Token: 0x04000A47 RID: 2631
		Package = 1024
	}
}
