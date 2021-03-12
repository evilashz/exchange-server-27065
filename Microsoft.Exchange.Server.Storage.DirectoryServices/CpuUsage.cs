using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x02000024 RID: 36
	public static class CpuUsage
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00003608 File Offset: 0x00001808
		public static uint GetCurrentUsagePercentage()
		{
			return ServerCPUUsage.GetCurrentUsagePercentage();
		}
	}
}
