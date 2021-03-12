using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharePointSignalStore
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SharePointSignalRestDataProvider
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000022BD File Offset: 0x000004BD
		public SharePointSignalRestDataProvider()
		{
			this.analyticsSignalSources = new List<IAnalyticsSignalSource>();
			this.anyDataProvided = false;
			this.report = new List<SharePointSignalRestDataProvider.AnalyticsSignalProviderReportItem>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000022E4 File Offset: 0x000004E4
		public void ProvideDataFor(ISharePointSender<string> sender)
		{
			IEnumerable<AnalyticsSignal> enumerable = new List<AnalyticsSignal>();
			foreach (IAnalyticsSignalSource analyticsSignalSource in this.analyticsSignalSources)
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				IEnumerable<AnalyticsSignal> signals = analyticsSignalSource.GetSignals();
				stopwatch.Stop();
				if (signals != null)
				{
					enumerable = enumerable.Concat(signals);
					this.report.Add(new SharePointSignalRestDataProvider.AnalyticsSignalProviderReportItem(analyticsSignalSource.GetSourceName(), signals.Count<AnalyticsSignal>(), stopwatch.Elapsed));
					if (signals.Any<AnalyticsSignal>())
					{
						this.anyDataProvided = true;
					}
				}
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["signals"] = enumerable;
			string data = new JavaScriptSerializer().Serialize(dictionary);
			sender.SetData(data);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023B8 File Offset: 0x000005B8
		public bool AnyDataProvided()
		{
			return this.anyDataProvided;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023C0 File Offset: 0x000005C0
		public void AddAnalyticsSignalSource(IAnalyticsSignalSource analyticsSignalSource)
		{
			this.analyticsSignalSources.Add(analyticsSignalSource);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000023D0 File Offset: 0x000005D0
		public void PrintProviderReport(ILogger logger)
		{
			foreach (SharePointSignalRestDataProvider.AnalyticsSignalProviderReportItem analyticsSignalProviderReportItem in this.report)
			{
				logger.LogInfo(analyticsSignalProviderReportItem.ToString(), new object[0]);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002430 File Offset: 0x00000630
		internal static Dictionary<string, object> CreateSignalProperties(Dictionary<string, string> properties)
		{
			List<object> list = new List<object>();
			if (properties != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in properties)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
					dictionary2["type"] = "SP.KeyValue";
					dictionary["__metadata"] = dictionary2;
					dictionary["ValueType"] = "Edm.String";
					dictionary["Key"] = keyValuePair.Key;
					dictionary["Value"] = keyValuePair.Value;
					list.Add(dictionary);
				}
			}
			Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
			dictionary3["results"] = list;
			return dictionary3;
		}

		// Token: 0x04000019 RID: 25
		private List<IAnalyticsSignalSource> analyticsSignalSources;

		// Token: 0x0400001A RID: 26
		private bool anyDataProvided;

		// Token: 0x0400001B RID: 27
		private List<SharePointSignalRestDataProvider.AnalyticsSignalProviderReportItem> report;

		// Token: 0x0200000C RID: 12
		private class AnalyticsSignalProviderReportItem
		{
			// Token: 0x06000030 RID: 48 RVA: 0x00002500 File Offset: 0x00000700
			public AnalyticsSignalProviderReportItem(string analyticsSignalSourceName, int numberOfsignals, TimeSpan timeUsed)
			{
				this.AnalyticsSignalSourceName = analyticsSignalSourceName;
				this.NumberOfSignals = numberOfsignals;
				this.TimeUsed = timeUsed;
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000031 RID: 49 RVA: 0x0000251D File Offset: 0x0000071D
			// (set) Token: 0x06000032 RID: 50 RVA: 0x00002525 File Offset: 0x00000725
			public string AnalyticsSignalSourceName { get; private set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000033 RID: 51 RVA: 0x0000252E File Offset: 0x0000072E
			// (set) Token: 0x06000034 RID: 52 RVA: 0x00002536 File Offset: 0x00000736
			public int NumberOfSignals { get; private set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000035 RID: 53 RVA: 0x0000253F File Offset: 0x0000073F
			// (set) Token: 0x06000036 RID: 54 RVA: 0x00002547 File Offset: 0x00000747
			public TimeSpan TimeUsed { get; set; }

			// Token: 0x06000037 RID: 55 RVA: 0x00002550 File Offset: 0x00000750
			public override string ToString()
			{
				return string.Format("Providing {0} {1} signals (used {2} seconds).", this.NumberOfSignals, this.AnalyticsSignalSourceName, this.TimeUsed.TotalSeconds);
			}
		}
	}
}
