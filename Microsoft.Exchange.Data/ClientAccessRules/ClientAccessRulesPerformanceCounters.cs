using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x020000F5 RID: 245
	internal static class ClientAccessRulesPerformanceCounters
	{
		// Token: 0x0600086C RID: 2156 RVA: 0x0001B94C File Offset: 0x00019B4C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ClientAccessRulesPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ClientAccessRulesPerformanceCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x0400059E RID: 1438
		public const string CategoryName = "MSExchangeCAR";

		// Token: 0x0400059F RID: 1439
		public static readonly ExPerformanceCounter TotalClientAccessRulesEvaluationCalls = new ExPerformanceCounter("MSExchangeCAR", "ClientAccessRules Evaluation Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005A0 RID: 1440
		public static readonly ExPerformanceCounter TotalConnectionsBlockedByClientAccessRules = new ExPerformanceCounter("MSExchangeCAR", "ClientAccessRules Evaluation Blocks", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005A1 RID: 1441
		public static readonly ExPerformanceCounter TotalClientAccessRulesEvaluationCallsOver10ms = new ExPerformanceCounter("MSExchangeCAR", "ClientAccessRules Evaluation Calls that took over 10ms", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005A2 RID: 1442
		public static readonly ExPerformanceCounter TotalClientAccessRulesEvaluationCallsOver50ms = new ExPerformanceCounter("MSExchangeCAR", "ClientAccessRules Evaluation Calls that took over 50ms", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005A3 RID: 1443
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ClientAccessRulesPerformanceCounters.TotalClientAccessRulesEvaluationCalls,
			ClientAccessRulesPerformanceCounters.TotalConnectionsBlockedByClientAccessRules,
			ClientAccessRulesPerformanceCounters.TotalClientAccessRulesEvaluationCallsOver10ms,
			ClientAccessRulesPerformanceCounters.TotalClientAccessRulesEvaluationCallsOver50ms
		};
	}
}
