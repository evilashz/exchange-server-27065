using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharedCache.DiagnosticHandlers
{
	// Token: 0x02000015 RID: 21
	internal sealed class StatsDiagHandler : ExchangeDiagnosableWrapper<List<CacheStatsResult>>
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000039C8 File Offset: 0x00001BC8
		protected override string ComponentName
		{
			get
			{
				return "Stats";
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000039CF File Offset: 0x00001BCF
		protected override string UsageSample
		{
			get
			{
				return "This handler returns live information and statistics on the caches currently loaded into the shared cache process.";
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000039D6 File Offset: 0x00001BD6
		protected override string UsageText
		{
			get
			{
				return "Example: Returns statistics on all caches\r\n                        Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.SharedCache -Component Stats";
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000039E0 File Offset: 0x00001BE0
		public static StatsDiagHandler GetInstance()
		{
			if (StatsDiagHandler.instance == null)
			{
				lock (StatsDiagHandler.lockObject)
				{
					if (StatsDiagHandler.instance == null)
					{
						StatsDiagHandler.instance = new StatsDiagHandler();
					}
				}
			}
			return StatsDiagHandler.instance;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003A38 File Offset: 0x00001C38
		internal override List<CacheStatsResult> GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			List<CacheStatsResult> list = new List<CacheStatsResult>();
			ReadOnlyDictionary<Guid, ISharedCache> registeredCaches = SharedCacheServer.RegisteredCaches;
			foreach (Guid guid in registeredCaches.Keys)
			{
				ISharedCache sharedCache = registeredCaches[guid];
				list.Add(new CacheStatsResult
				{
					Guid = guid,
					Name = sharedCache.Name,
					NumberOfEntries = sharedCache.Count,
					AverageLatency = sharedCache.PerformanceCounters.AverageLatencyValue + "ms",
					Type = sharedCache.GetType().ToString(),
					DiskUsage = ByteQuantifiedSize.FromBytes((ulong)sharedCache.PerformanceCounters.DiskSpace).ToString()
				});
			}
			return list;
		}

		// Token: 0x04000047 RID: 71
		private static StatsDiagHandler instance;

		// Token: 0x04000048 RID: 72
		private static object lockObject = new object();
	}
}
