using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001D2 RID: 466
	internal class WLMResourceStatsLog : ObjectLog<WLMResourceStatsData>
	{
		// Token: 0x0600130A RID: 4874 RVA: 0x0002B695 File Offset: 0x00029895
		private WLMResourceStatsLog() : base(new WLMResourceStatsLog.WLMResourceStatsLogSchema(), new SimpleObjectLogConfiguration("WLMResourceStats", "WLMResourceStatsLogEnabled", "WLMResourceStatsLogMaxDirSize", "WLMResourceStatsLogMaxFileSize"))
		{
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0002B6BB File Offset: 0x000298BB
		public static void Write(WLMResourceStatsData loggingStatsData)
		{
			WLMResourceStatsLog.instance.LogObject(loggingStatsData);
		}

		// Token: 0x040009E3 RID: 2531
		public const int MaxDataContextLength = 1000;

		// Token: 0x040009E4 RID: 2532
		private static WLMResourceStatsLog instance = new WLMResourceStatsLog();

		// Token: 0x020001D3 RID: 467
		internal class WLMResourceStatsLogSchema : ObjectLogSchema
		{
			// Token: 0x1700065D RID: 1629
			// (get) Token: 0x0600130D RID: 4877 RVA: 0x0002B6D4 File Offset: 0x000298D4
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Mailbox Replication Service";
				}
			}

			// Token: 0x1700065E RID: 1630
			// (get) Token: 0x0600130E RID: 4878 RVA: 0x0002B6DB File Offset: 0x000298DB
			public override string LogType
			{
				get
				{
					return "WLMResourceStats Log";
				}
			}

			// Token: 0x040009E5 RID: 2533
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> OwnerResourceName = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("OwnerResourceName", (WLMResourceStatsData d) => d.OwnerResourceName);

			// Token: 0x040009E6 RID: 2534
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> OwnerResourceGuid = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("OwnerResourceGuid", (WLMResourceStatsData d) => d.OwnerResourceGuid);

			// Token: 0x040009E7 RID: 2535
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> OwnerResourceType = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("OwnerResourceType", (WLMResourceStatsData d) => d.OwnerResourceType);

			// Token: 0x040009E8 RID: 2536
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> ResourceKey = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("ResourceKey", (WLMResourceStatsData d) => d.WlmResourceKey);

			// Token: 0x040009E9 RID: 2537
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> LoadState = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("LoadState", (WLMResourceStatsData d) => d.LoadState);

			// Token: 0x040009EA RID: 2538
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> LoadRatio = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("LoadRatio", (WLMResourceStatsData d) => d.LoadRatio.ToString());

			// Token: 0x040009EB RID: 2539
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> Metric = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("Metric", (WLMResourceStatsData d) => d.Metric);

			// Token: 0x040009EC RID: 2540
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> DynamicCapacity = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("DynamicCapacity", (WLMResourceStatsData d) => d.DynamicCapacity.ToString());

			// Token: 0x040009ED RID: 2541
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> TimeInterval = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("TimeInterval", (WLMResourceStatsData d) => d.TimeInterval.ToString());

			// Token: 0x040009EE RID: 2542
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> UnderloadedCount = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("UnderloadedCount", (WLMResourceStatsData d) => d.UnderloadedCount.ToString());

			// Token: 0x040009EF RID: 2543
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> FullCount = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("FullCount", (WLMResourceStatsData d) => d.FullCount.ToString());

			// Token: 0x040009F0 RID: 2544
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> OverloadedCount = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("OverloadedCount", (WLMResourceStatsData d) => d.OverloadedCount.ToString());

			// Token: 0x040009F1 RID: 2545
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> CriticalCount = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("CriticalCount", (WLMResourceStatsData d) => d.CriticalCount.ToString());

			// Token: 0x040009F2 RID: 2546
			public static readonly ObjectLogSimplePropertyDefinition<WLMResourceStatsData> UnknownCount = new ObjectLogSimplePropertyDefinition<WLMResourceStatsData>("UnknownCount", (WLMResourceStatsData d) => d.UnknownCount.ToString());
		}
	}
}
