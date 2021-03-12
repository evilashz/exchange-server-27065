using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Common.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x0200048B RID: 1163
	public class SearchWordBreakerLoadingProbe : GenericEventLogProbe
	{
		// Token: 0x06001D68 RID: 7528 RVA: 0x000AFF08 File Offset: 0x000AE108
		static SearchWordBreakerLoadingProbe()
		{
			CentralEventLogWatcher.Instance.BeforeEnqueueEvent += delegate(EventRecord eventRecord, CentralEventLogWatcher.EventRecordMini eventRecordMini)
			{
				if (eventRecordMini.EventId == 151 && string.Equals(eventRecordMini.LogName, "Microsoft-Office Server-Search/Operational", StringComparison.OrdinalIgnoreCase) && string.Equals(eventRecordMini.Source, "Microsoft-Office Server-Search", StringComparison.OrdinalIgnoreCase) && eventRecord.Properties != null && eventRecord.Properties.Count >= 1 && eventRecord.Properties[0] != null)
				{
					eventRecordMini.ExtendedPropertyField1 = eventRecord.Properties[0].Value.ToString();
				}
			};
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000AFF34 File Offset: 0x000AE134
		public static ProbeDefinition CreateDefinition(string name, int recurrenceIntervalSeconds, bool enabled)
		{
			ProbeDefinition probeDefinition = GenericEventLogProbe.CreateDefinition(name, ExchangeComponent.Search.Name, "Microsoft-Office Server-Search/Operational", "Microsoft-Office Server-Search", new int[]
			{
				152
			}, new int[]
			{
				151
			}, recurrenceIntervalSeconds, recurrenceIntervalSeconds, 3);
			probeDefinition.TypeName = typeof(SearchWordBreakerLoadingProbe).FullName;
			probeDefinition.Enabled = enabled;
			return probeDefinition;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x000AFF9C File Offset: 0x000AE19C
		protected override void OnRedEvent(CentralEventLogWatcher.EventRecordMini redEvent)
		{
			string text = null;
			if (!string.IsNullOrEmpty(redEvent.ExtendedPropertyField1))
			{
				string[] array = redEvent.ExtendedPropertyField1.Split(new char[]
				{
					'-'
				});
				if (array.Length == 2)
				{
					text = array[0];
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				base.Result.StateAttribute15 = text;
			}
			else
			{
				text = "<Unknown>";
			}
			throw new SearchProbeFailureException(Strings.SearchWordBreakerLoadingFailure(text, redEvent.TimeCreated.ToString(), 151.ToString(), "Microsoft-Office Server-Search/Operational", 152.ToString()));
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x000B0034 File Offset: 0x000AE234
		protected override void OnNoEvent(CancellationToken cancellationToken)
		{
			ProbeResult lastProbeResult = SearchMonitoringHelper.GetLastProbeResult(this, base.Broker, cancellationToken);
			if (lastProbeResult != null && lastProbeResult.ResultType == ResultType.Failed && lastProbeResult.Exception.Contains("SearchProbeFailureException"))
			{
				base.Result.StateAttribute15 = lastProbeResult.StateAttribute15;
				base.Result.StateAttribute14 = lastProbeResult.ExecutionStartTime.ToString();
				throw new SearchProbeFailureException(new LocalizedString(lastProbeResult.Error));
			}
		}

		// Token: 0x04001461 RID: 5217
		public const string Source = "Microsoft-Office Server-Search";

		// Token: 0x04001462 RID: 5218
		public const string Channel = "Microsoft-Office Server-Search/Operational";

		// Token: 0x04001463 RID: 5219
		public const int RedEventId = 151;

		// Token: 0x04001464 RID: 5220
		public const int GreenEventId = 152;
	}
}
