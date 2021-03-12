using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x02000483 RID: 1155
	public class SearchQueryFailureProbe : SearchProbeBase
	{
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x000AB1D4 File Offset: 0x000A93D4
		protected override bool SkipOnNonHealthyCatalog
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x000AB1D7 File Offset: 0x000A93D7
		protected override bool SkipOnNonActiveDatabase
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x000AB1DA File Offset: 0x000A93DA
		protected override bool SkipOnAutoDagExcludeFromMonitoring
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000AB2E0 File Offset: 0x000A94E0
		internal static string GetFullTextIndexExceptionEventsCached(int seconds)
		{
			if (DateTime.UtcNow < SearchQueryFailureProbe.recentFailureEventsCacheTimeoutTime)
			{
				return SearchQueryFailureProbe.recentFailureEventsCached;
			}
			bool flag = false;
			string result;
			try
			{
				object obj;
				Monitor.Enter(obj = SearchQueryFailureProbe.recentFailureEventsCacheLock, ref flag);
				if (DateTime.UtcNow < SearchQueryFailureProbe.recentFailureEventsCacheTimeoutTime)
				{
					result = SearchQueryFailureProbe.recentFailureEventsCached;
				}
				else
				{
					StringBuilder sb = new StringBuilder();
					Action delegateGetEvents = delegate()
					{
						try
						{
							List<EventRecord> events = SearchMonitoringHelper.GetEvents("Application", 1012, "MSExchangeIS", seconds, 2, null);
							foreach (EventRecord eventRecord in events)
							{
								sb.AppendLine("=====================================");
								sb.AppendLine(eventRecord.TimeCreated.ToString());
								sb.AppendLine(eventRecord.FormatDescription());
								eventRecord.Dispose();
							}
						}
						catch (Exception ex)
						{
							SearchMonitoringHelper.LogInfo("Exception caught reading query failure event logs:\n{0}.", new object[]
							{
								ex
							});
						}
					};
					IAsyncResult asyncResult = delegateGetEvents.BeginInvoke(delegate(IAsyncResult r)
					{
						delegateGetEvents.EndInvoke(r);
					}, null);
					if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromMinutes(2.0)))
					{
						SearchMonitoringHelper.LogInfo("Timeout reading query failure event logs.", new object[0]);
						result = null;
					}
					else
					{
						string text = sb.ToString();
						if (text.Length > 0)
						{
							SearchQueryFailureProbe.recentFailureEventsCached = text;
							SearchQueryFailureProbe.recentFailureEventsCacheTimeoutTime = DateTime.UtcNow.AddMinutes(20.0);
						}
						result = text;
					}
				}
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
			return result;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000AB410 File Offset: 0x000A9610
		protected override void InternalDoWork(CancellationToken cancellationToken)
		{
			string targetResource = base.Definition.TargetResource;
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			double num4 = (double)SearchMonitoringHelper.GetPerformanceCounterValue("MSExchangeIS Store", "Total searches", targetResource);
			double num5 = (double)SearchMonitoringHelper.GetPerformanceCounterValue("MSExchangeIS Store", "Total number of successful search queries", targetResource);
			double num6 = (double)SearchMonitoringHelper.GetPerformanceCounterValue("MSExchangeIS Store", "Total search queries completed in > 60 sec", targetResource);
			base.Result.StateAttribute6 = num4;
			base.Result.StateAttribute7 = num5;
			base.Result.StateAttribute8 = num6;
			ProbeResult lastProbeResult = SearchMonitoringHelper.GetLastProbeResult(this, base.Broker, cancellationToken);
			if (lastProbeResult != null && lastProbeResult.StateAttribute6 <= num4 && lastProbeResult.StateAttribute7 <= num5 && lastProbeResult.StateAttribute8 <= num6)
			{
				num = lastProbeResult.StateAttribute6;
				num2 = lastProbeResult.StateAttribute7;
				num3 = lastProbeResult.StateAttribute8;
			}
			double num7 = num4 - num;
			if (num7 == 0.0)
			{
				return;
			}
			double num8 = (num7 - (num5 - num2)) / num7;
			double @double = base.AttributeHelper.GetDouble("FailureRateThreshold", true, 0.0, null, null);
			if (num8 > @double)
			{
				string text = SearchQueryFailureProbe.GetFullTextIndexExceptionEventsCached(base.Definition.RecurrenceIntervalSeconds);
				if (string.IsNullOrWhiteSpace(text))
				{
					text = Strings.SearchInformationNotAvailable;
				}
				throw new SearchProbeFailureException(Strings.SearchQueryFailure(targetResource, num8.ToString("P"), @double.ToString("P"), num7.ToString(), (num5 - num2).ToString(), text));
			}
			double double2 = base.AttributeHelper.GetDouble("SlowRateThreshold", true, 0.0, null, null);
			double num9 = (num6 - num3) / (num5 - num2);
			if (num9 > double2)
			{
				throw new SearchProbeFailureException(Strings.SearchQuerySlow(targetResource, num8.ToString("P"), @double.ToString("P")));
			}
		}

		// Token: 0x040013ED RID: 5101
		internal const string ApplicationEventLogName = "Application";

		// Token: 0x040013EE RID: 5102
		internal const int FullTextIndexExceptionEventId = 1012;

		// Token: 0x040013EF RID: 5103
		internal const string MSExchangeISProviderName = "MSExchangeIS";

		// Token: 0x040013F0 RID: 5104
		private const int RecentFailureEventsCachedTimeoutMinutes = 20;

		// Token: 0x040013F1 RID: 5105
		private const int GetEventsTimeoutMinutes = 2;

		// Token: 0x040013F2 RID: 5106
		private static string recentFailureEventsCached;

		// Token: 0x040013F3 RID: 5107
		private static DateTime recentFailureEventsCacheTimeoutTime = DateTime.MinValue;

		// Token: 0x040013F4 RID: 5108
		private static readonly object recentFailureEventsCacheLock = new object();
	}
}
