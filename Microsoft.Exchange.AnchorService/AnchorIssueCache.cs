using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x0200000C RID: 12
	internal class AnchorIssueCache : ServiceIssueCache
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003408 File Offset: 0x00001608
		public AnchorIssueCache(AnchorContext context, JobCache cache)
		{
			this.Context = context;
			this.Cache = cache;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000341E File Offset: 0x0000161E
		public override bool ScanningIsEnabled
		{
			get
			{
				return this.Context.Config.GetConfig<bool>("IssueCacheIsEnabled");
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003435 File Offset: 0x00001635
		protected override TimeSpan FullScanFrequency
		{
			get
			{
				return this.Context.Config.GetConfig<TimeSpan>("IssueCacheScanFrequency");
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000344C File Offset: 0x0000164C
		protected override int IssueLimit
		{
			get
			{
				return this.Context.Config.GetConfig<int>("IssueCacheItemLimit");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003463 File Offset: 0x00001663
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000346B File Offset: 0x0000166B
		private AnchorContext Context { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003474 File Offset: 0x00001674
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000347C File Offset: 0x0000167C
		private JobCache Cache { get; set; }

		// Token: 0x060000A1 RID: 161 RVA: 0x00003488 File Offset: 0x00001688
		internal static bool TrySendEventNotification(AnchorContext context, string notificationReason, string message, ResultSeverityLevel severity = ResultSeverityLevel.Error)
		{
			if (!context.Config.GetConfig<bool>("IssueCacheIsEnabled"))
			{
				return false;
			}
			string config = context.Config.GetConfig<string>("MonitoringComponentName");
			if (string.IsNullOrEmpty(config))
			{
				return false;
			}
			Component a = Component.FindWellKnownComponent(config);
			if (a == null)
			{
				return false;
			}
			new EventNotificationItem(config, config, notificationReason, message, severity).Publish(false);
			return true;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000034E8 File Offset: 0x000016E8
		protected override ICollection<ServiceIssue> RunFullIssueScan()
		{
			ICollection<ServiceIssue> collection = new List<ServiceIssue>();
			foreach (CacheEntryBase cacheEntryBase in this.Cache.Get())
			{
				if (cacheEntryBase.ServiceException != null)
				{
					collection.Add(new DiagnosableServiceIssue(cacheEntryBase, cacheEntryBase.ServiceException.ToString()));
				}
			}
			return collection;
		}

		// Token: 0x0400002E RID: 46
		public const string CacheEntryIsPoisonedNotification = "CacheEntryIsPoisoned";
	}
}
