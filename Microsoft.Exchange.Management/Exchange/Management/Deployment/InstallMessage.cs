using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020002A6 RID: 678
	internal enum InstallMessage
	{
		// Token: 0x04000A49 RID: 2633
		FatalExit,
		// Token: 0x04000A4A RID: 2634
		Error = 16777216,
		// Token: 0x04000A4B RID: 2635
		Warning = 33554432,
		// Token: 0x04000A4C RID: 2636
		User = 50331648,
		// Token: 0x04000A4D RID: 2637
		Info = 67108864,
		// Token: 0x04000A4E RID: 2638
		FilesInUse = 83886080,
		// Token: 0x04000A4F RID: 2639
		ResolveSource = 100663296,
		// Token: 0x04000A50 RID: 2640
		OutOfDiskSpace = 117440512,
		// Token: 0x04000A51 RID: 2641
		ActionStart = 134217728,
		// Token: 0x04000A52 RID: 2642
		ActionData = 150994944,
		// Token: 0x04000A53 RID: 2643
		Progress = 167772160,
		// Token: 0x04000A54 RID: 2644
		CommonData = 184549376,
		// Token: 0x04000A55 RID: 2645
		Initialize = 201326592,
		// Token: 0x04000A56 RID: 2646
		Terminate = 218103808,
		// Token: 0x04000A57 RID: 2647
		ShowDialog = 234881024
	}
}
