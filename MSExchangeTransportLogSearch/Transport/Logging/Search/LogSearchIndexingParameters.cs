using System;
using System.Collections.Generic;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000031 RID: 49
	internal static class LogSearchIndexingParameters
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00007808 File Offset: 0x00005A08
		private static ulong GetIndexMemoryLimit()
		{
			NativeMethods.MemoryStatusEx memoryStatusEx;
			if (NativeMethods.GlobalMemoryStatusEx(out memoryStatusEx))
			{
				return Convert.ToUInt64(Convert.ToDouble(LogSearchAppConfig.Instance.LogSearchIndexing.IndexLimitMemoryPercentage) / 100.0 * memoryStatusEx.TotalPhys);
			}
			return 0UL;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007850 File Offset: 0x00005A50
		internal static ulong GetIndexLimit(string prefix)
		{
			if (LogSearchIndexingParameters.MessageTrackingIndexPercentageByPrefix.ContainsKey(prefix))
			{
				if (0UL == LogSearchIndexingParameters.messageTrackingIndexLimit)
				{
					ulong indexMemoryLimit = LogSearchIndexingParameters.GetIndexMemoryLimit();
					LogSearchIndexingParameters.messageTrackingIndexLimit = Math.Max(10485760UL, indexMemoryLimit);
				}
				return LogSearchIndexingParameters.messageTrackingIndexLimit;
			}
			return ulong.MaxValue;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007892 File Offset: 0x00005A92
		internal static double GetDefaultIndexPercentage(string prefix)
		{
			if (LogSearchIndexingParameters.MessageTrackingIndexPercentageByPrefix.ContainsKey(prefix))
			{
				return 0.1;
			}
			return 0.0;
		}

		// Token: 0x040000A6 RID: 166
		private const ulong MinimumIndexLimit = 18446744073709551615UL;

		// Token: 0x040000A7 RID: 167
		private const ulong MessageTrackingMinimumIndexLimit = 10485760UL;

		// Token: 0x040000A8 RID: 168
		private const double DefaultIndexPercentage = 0.0;

		// Token: 0x040000A9 RID: 169
		private const double MessageTrackingDefaultIndexPercentage = 0.1;

		// Token: 0x040000AA RID: 170
		private static ulong messageTrackingIndexLimit = 0UL;

		// Token: 0x040000AB RID: 171
		internal static Dictionary<string, double> MessageTrackingIndexPercentageByPrefix = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"MSGTRK",
				0.0006
			},
			{
				"MSGTRKM",
				0.1
			},
			{
				"MSGTRKMA",
				0.1
			},
			{
				"MSGTRKMD",
				0.1
			},
			{
				"MSGTRKMS",
				0.1
			}
		};
	}
}
