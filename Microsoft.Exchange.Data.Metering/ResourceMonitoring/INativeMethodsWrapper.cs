using System;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000019 RID: 25
	internal interface INativeMethodsWrapper
	{
		// Token: 0x0600012B RID: 299
		bool GetDiskFreeSpaceEx(string directoryName, out ulong freeBytesAvailable, out ulong totalNumberOfBytes, out ulong totalNumberOfFreeBytes);

		// Token: 0x0600012C RID: 300
		bool GetSystemMemoryUsePercentage(out uint systemMemoryUsage);

		// Token: 0x0600012D RID: 301
		bool GetTotalSystemMemory(out ulong systemMemory);

		// Token: 0x0600012E RID: 302
		bool GetTotalVirtualMemory(out ulong virtualMemory);

		// Token: 0x0600012F RID: 303
		bool GetProcessPrivateBytes(out ulong privateBytes);
	}
}
