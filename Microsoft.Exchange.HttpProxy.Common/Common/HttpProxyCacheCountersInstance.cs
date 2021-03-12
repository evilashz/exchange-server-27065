using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000023 RID: 35
	internal sealed class HttpProxyCacheCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00006780 File Offset: 0x00004980
		internal HttpProxyCacheCountersInstance(string instanceName, HttpProxyCacheCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange HttpProxy Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AnchorMailboxCacheSize = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to Database Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AnchorMailboxCacheSize);
				this.AnchorMailboxLocalCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to Database Local Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AnchorMailboxLocalCacheHitsRate);
				this.AnchorMailboxLocalCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to Database Local Cache Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AnchorMailboxLocalCacheHitsRateBase);
				this.AnchorMailboxOverallCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to Database Overall Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AnchorMailboxOverallCacheHitsRate);
				this.AnchorMailboxOverallCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to Database Overall Cache Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AnchorMailboxOverallCacheHitsRateBase);
				this.NegativeAnchorMailboxCacheSize = new ExPerformanceCounter(base.CategoryName, "NegativeAnchorMailbox Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NegativeAnchorMailboxCacheSize);
				this.NegativeAnchorMailboxLocalCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "NegativeAnchorMailbox Local Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NegativeAnchorMailboxLocalCacheHitsRate);
				this.NegativeAnchorMailboxLocalCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "NegativeAnchorMailbox Local Cache Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NegativeAnchorMailboxLocalCacheHitsRateBase);
				this.BackEndServerCacheSize = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerCacheSize);
				this.BackEndServerLocalCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Local Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerLocalCacheHitsRate);
				this.BackEndServerLocalCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Local Cache Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerLocalCacheHitsRateBase);
				this.BackEndServerOverallCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Overall Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerOverallCacheHitsRate);
				this.BackEndServerOverallCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Overall Cache Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerOverallCacheHitsRateBase);
				this.BackEndServerCacheLocalServerListCount = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Cache Local Site MailboxServers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerCacheLocalServerListCount);
				this.BackEndServerCacheRefreshingQueueLength = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Cache Refreshing Queue Length", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerCacheRefreshingQueueLength);
				this.BackEndServerCacheRefreshingStatus = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Cache Refreshing Status", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerCacheRefreshingStatus);
				this.FbaModuleKeyCacheSize = new ExPerformanceCounter(base.CategoryName, "FBAModule Key Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FbaModuleKeyCacheSize);
				this.FbaModuleKeyCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "FBAModule Key Cache Hits Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FbaModuleKeyCacheHitsRate);
				this.FbaModuleKeyCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "FBAModule Key Cache Hits Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FbaModuleKeyCacheHitsRateBase);
				this.CookieUseRate = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to MailboxServer Cookie Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CookieUseRate);
				this.CookieUseRateBase = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to MailboxServer Cookie Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CookieUseRateBase);
				this.OverallCacheEffectivenessRate = new ExPerformanceCounter(base.CategoryName, "Overall Cache Effectiveness (% of requests)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OverallCacheEffectivenessRate);
				this.OverallCacheEffectivenessRateBase = new ExPerformanceCounter(base.CategoryName, "Overall Cache Effectiveness (% of requests) Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OverallCacheEffectivenessRateBase);
				this.RouteRefresherSuccessfulMailboxServerCacheUpdates = new ExPerformanceCounter(base.CategoryName, "Route Refresher Mailbox Server Cache Updates", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RouteRefresherSuccessfulMailboxServerCacheUpdates);
				this.RouteRefresherTotalMailboxServerCacheUpdateAttempts = new ExPerformanceCounter(base.CategoryName, "Route Refresher Mailbox Server Cache Update Attempts", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RouteRefresherTotalMailboxServerCacheUpdateAttempts);
				this.RouteRefresherSuccessfulAnchorMailboxCacheUpdates = new ExPerformanceCounter(base.CategoryName, "Route Refresher Anchor Mailbox Cache Updates", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RouteRefresherSuccessfulAnchorMailboxCacheUpdates);
				this.RouteRefresherTotalAnchorMailboxCacheUpdateAttempts = new ExPerformanceCounter(base.CategoryName, "Route Refresher Anchor Mailbox Cache Update Attempts", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RouteRefresherTotalAnchorMailboxCacheUpdateAttempts);
				this.MovingPercentageBackEndServerLocalCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Local Cache Hit Rate (Moving Average)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageBackEndServerLocalCacheHitsRate);
				this.MovingPercentageBackEndServerOverallCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Overall Cache Hit Rate (Moving Average)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageBackEndServerOverallCacheHitsRate);
				this.MovingPercentageCookieUseRate = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to MailboxServer Cookie Hit Rate (Moving Average)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageCookieUseRate);
				long num = this.AnchorMailboxCacheSize.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006D18 File Offset: 0x00004F18
		internal HttpProxyCacheCountersInstance(string instanceName) : base(instanceName, "MSExchange HttpProxy Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AnchorMailboxCacheSize = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to Database Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AnchorMailboxCacheSize);
				this.AnchorMailboxLocalCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to Database Local Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AnchorMailboxLocalCacheHitsRate);
				this.AnchorMailboxLocalCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to Database Local Cache Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AnchorMailboxLocalCacheHitsRateBase);
				this.AnchorMailboxOverallCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to Database Overall Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AnchorMailboxOverallCacheHitsRate);
				this.AnchorMailboxOverallCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to Database Overall Cache Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AnchorMailboxOverallCacheHitsRateBase);
				this.NegativeAnchorMailboxCacheSize = new ExPerformanceCounter(base.CategoryName, "NegativeAnchorMailbox Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NegativeAnchorMailboxCacheSize);
				this.NegativeAnchorMailboxLocalCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "NegativeAnchorMailbox Local Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NegativeAnchorMailboxLocalCacheHitsRate);
				this.NegativeAnchorMailboxLocalCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "NegativeAnchorMailbox Local Cache Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NegativeAnchorMailboxLocalCacheHitsRateBase);
				this.BackEndServerCacheSize = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerCacheSize);
				this.BackEndServerLocalCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Local Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerLocalCacheHitsRate);
				this.BackEndServerLocalCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Local Cache Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerLocalCacheHitsRateBase);
				this.BackEndServerOverallCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Overall Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerOverallCacheHitsRate);
				this.BackEndServerOverallCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Overall Cache Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerOverallCacheHitsRateBase);
				this.BackEndServerCacheLocalServerListCount = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Cache Local Site MailboxServers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerCacheLocalServerListCount);
				this.BackEndServerCacheRefreshingQueueLength = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Cache Refreshing Queue Length", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerCacheRefreshingQueueLength);
				this.BackEndServerCacheRefreshingStatus = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Cache Refreshing Status", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BackEndServerCacheRefreshingStatus);
				this.FbaModuleKeyCacheSize = new ExPerformanceCounter(base.CategoryName, "FBAModule Key Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FbaModuleKeyCacheSize);
				this.FbaModuleKeyCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "FBAModule Key Cache Hits Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FbaModuleKeyCacheHitsRate);
				this.FbaModuleKeyCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "FBAModule Key Cache Hits Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FbaModuleKeyCacheHitsRateBase);
				this.CookieUseRate = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to MailboxServer Cookie Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CookieUseRate);
				this.CookieUseRateBase = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to MailboxServer Cookie Hit Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CookieUseRateBase);
				this.OverallCacheEffectivenessRate = new ExPerformanceCounter(base.CategoryName, "Overall Cache Effectiveness (% of requests)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OverallCacheEffectivenessRate);
				this.OverallCacheEffectivenessRateBase = new ExPerformanceCounter(base.CategoryName, "Overall Cache Effectiveness (% of requests) Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OverallCacheEffectivenessRateBase);
				this.RouteRefresherSuccessfulMailboxServerCacheUpdates = new ExPerformanceCounter(base.CategoryName, "Route Refresher Mailbox Server Cache Updates", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RouteRefresherSuccessfulMailboxServerCacheUpdates);
				this.RouteRefresherTotalMailboxServerCacheUpdateAttempts = new ExPerformanceCounter(base.CategoryName, "Route Refresher Mailbox Server Cache Update Attempts", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RouteRefresherTotalMailboxServerCacheUpdateAttempts);
				this.RouteRefresherSuccessfulAnchorMailboxCacheUpdates = new ExPerformanceCounter(base.CategoryName, "Route Refresher Anchor Mailbox Cache Updates", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RouteRefresherSuccessfulAnchorMailboxCacheUpdates);
				this.RouteRefresherTotalAnchorMailboxCacheUpdateAttempts = new ExPerformanceCounter(base.CategoryName, "Route Refresher Anchor Mailbox Cache Update Attempts", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RouteRefresherTotalAnchorMailboxCacheUpdateAttempts);
				this.MovingPercentageBackEndServerLocalCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Local Cache Hit Rate (Moving Average)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageBackEndServerLocalCacheHitsRate);
				this.MovingPercentageBackEndServerOverallCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "DatabaseGuid to MailboxServer Overall Cache Hit Rate (Moving Average)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageBackEndServerOverallCacheHitsRate);
				this.MovingPercentageCookieUseRate = new ExPerformanceCounter(base.CategoryName, "AnchorMailbox to MailboxServer Cookie Hit Rate (Moving Average)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageCookieUseRate);
				long num = this.AnchorMailboxCacheSize.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000072B0 File Offset: 0x000054B0
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04000119 RID: 281
		public readonly ExPerformanceCounter AnchorMailboxCacheSize;

		// Token: 0x0400011A RID: 282
		public readonly ExPerformanceCounter AnchorMailboxLocalCacheHitsRate;

		// Token: 0x0400011B RID: 283
		public readonly ExPerformanceCounter AnchorMailboxLocalCacheHitsRateBase;

		// Token: 0x0400011C RID: 284
		public readonly ExPerformanceCounter AnchorMailboxOverallCacheHitsRate;

		// Token: 0x0400011D RID: 285
		public readonly ExPerformanceCounter AnchorMailboxOverallCacheHitsRateBase;

		// Token: 0x0400011E RID: 286
		public readonly ExPerformanceCounter NegativeAnchorMailboxCacheSize;

		// Token: 0x0400011F RID: 287
		public readonly ExPerformanceCounter NegativeAnchorMailboxLocalCacheHitsRate;

		// Token: 0x04000120 RID: 288
		public readonly ExPerformanceCounter NegativeAnchorMailboxLocalCacheHitsRateBase;

		// Token: 0x04000121 RID: 289
		public readonly ExPerformanceCounter BackEndServerCacheSize;

		// Token: 0x04000122 RID: 290
		public readonly ExPerformanceCounter BackEndServerLocalCacheHitsRate;

		// Token: 0x04000123 RID: 291
		public readonly ExPerformanceCounter BackEndServerLocalCacheHitsRateBase;

		// Token: 0x04000124 RID: 292
		public readonly ExPerformanceCounter BackEndServerOverallCacheHitsRate;

		// Token: 0x04000125 RID: 293
		public readonly ExPerformanceCounter BackEndServerOverallCacheHitsRateBase;

		// Token: 0x04000126 RID: 294
		public readonly ExPerformanceCounter BackEndServerCacheLocalServerListCount;

		// Token: 0x04000127 RID: 295
		public readonly ExPerformanceCounter BackEndServerCacheRefreshingQueueLength;

		// Token: 0x04000128 RID: 296
		public readonly ExPerformanceCounter BackEndServerCacheRefreshingStatus;

		// Token: 0x04000129 RID: 297
		public readonly ExPerformanceCounter FbaModuleKeyCacheSize;

		// Token: 0x0400012A RID: 298
		public readonly ExPerformanceCounter FbaModuleKeyCacheHitsRate;

		// Token: 0x0400012B RID: 299
		public readonly ExPerformanceCounter FbaModuleKeyCacheHitsRateBase;

		// Token: 0x0400012C RID: 300
		public readonly ExPerformanceCounter CookieUseRate;

		// Token: 0x0400012D RID: 301
		public readonly ExPerformanceCounter CookieUseRateBase;

		// Token: 0x0400012E RID: 302
		public readonly ExPerformanceCounter OverallCacheEffectivenessRate;

		// Token: 0x0400012F RID: 303
		public readonly ExPerformanceCounter OverallCacheEffectivenessRateBase;

		// Token: 0x04000130 RID: 304
		public readonly ExPerformanceCounter RouteRefresherSuccessfulMailboxServerCacheUpdates;

		// Token: 0x04000131 RID: 305
		public readonly ExPerformanceCounter RouteRefresherTotalMailboxServerCacheUpdateAttempts;

		// Token: 0x04000132 RID: 306
		public readonly ExPerformanceCounter RouteRefresherSuccessfulAnchorMailboxCacheUpdates;

		// Token: 0x04000133 RID: 307
		public readonly ExPerformanceCounter RouteRefresherTotalAnchorMailboxCacheUpdateAttempts;

		// Token: 0x04000134 RID: 308
		public readonly ExPerformanceCounter MovingPercentageBackEndServerLocalCacheHitsRate;

		// Token: 0x04000135 RID: 309
		public readonly ExPerformanceCounter MovingPercentageBackEndServerOverallCacheHitsRate;

		// Token: 0x04000136 RID: 310
		public readonly ExPerformanceCounter MovingPercentageCookieUseRate;
	}
}
