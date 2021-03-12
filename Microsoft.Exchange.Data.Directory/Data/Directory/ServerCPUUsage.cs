using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009C8 RID: 2504
	internal static class ServerCPUUsage
	{
		// Token: 0x06007412 RID: 29714 RVA: 0x0017F062 File Offset: 0x0017D262
		internal static uint GetCurrentUsagePercentage()
		{
			ServerCPUUsage.Refresh();
			return ServerCPUUsage.lastServerCPUUsagePercentage;
		}

		// Token: 0x06007413 RID: 29715 RVA: 0x0017F070 File Offset: 0x0017D270
		static ServerCPUUsage()
		{
			if (CPUUsage.GetCurrentCPU(ref ServerCPUUsage.lastServerCPUUsage))
			{
				ServerCPUUsage.lastUpdatedTime = DateTime.UtcNow;
				return;
			}
			ServerCPUUsage.lastServerCPUUsage = 0L;
			ServerCPUUsage.lastUpdatedTime = DateTime.MinValue;
		}

		// Token: 0x06007414 RID: 29716 RVA: 0x0017F0C8 File Offset: 0x0017D2C8
		private static void Refresh()
		{
			if (DateTime.UtcNow - ServerCPUUsage.lastUpdatedTime <= ServerCPUUsage.refreshInterval)
			{
				return;
			}
			lock (ServerCPUUsage.lockObject)
			{
				if (!(DateTime.UtcNow - ServerCPUUsage.lastUpdatedTime <= ServerCPUUsage.refreshInterval))
				{
					float num;
					if (CPUUsage.CalculateCPUUsagePercentage(ref ServerCPUUsage.lastUpdatedTime, ref ServerCPUUsage.lastServerCPUUsage, out num))
					{
						ServerCPUUsage.lastServerCPUUsagePercentage = (uint)Math.Round((double)num);
					}
				}
			}
		}

		// Token: 0x04004AF3 RID: 19187
		private static readonly TimeSpan refreshInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x04004AF4 RID: 19188
		private static object lockObject = new object();

		// Token: 0x04004AF5 RID: 19189
		private static DateTime lastUpdatedTime;

		// Token: 0x04004AF6 RID: 19190
		private static long lastServerCPUUsage;

		// Token: 0x04004AF7 RID: 19191
		private static uint lastServerCPUUsagePercentage = 0U;
	}
}
