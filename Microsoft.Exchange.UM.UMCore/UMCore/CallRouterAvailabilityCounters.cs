using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000306 RID: 774
	internal static class CallRouterAvailabilityCounters
	{
		// Token: 0x06001782 RID: 6018 RVA: 0x00064E40 File Offset: 0x00063040
		public static void GetPerfCounterInfo(XElement element)
		{
			if (CallRouterAvailabilityCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in CallRouterAvailabilityCounters.AllCounters)
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

		// Token: 0x04000DFC RID: 3580
		public const string CategoryName = "MSExchangeUMCallRouterAvailability";

		// Token: 0x04000DFD RID: 3581
		public static readonly ExPerformanceCounter RecentMissedCallNotificationProxyFailed = new ExPerformanceCounter("MSExchangeUMCallRouterAvailability", "% of Missed Call Notification Proxy Failed at UM Call Router Over the Last Hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000DFE RID: 3582
		public static readonly ExPerformanceCounter UMCallRouterCallsRejected = new ExPerformanceCounter("MSExchangeUMCallRouterAvailability", "Total Inbound Calls Rejected by the UM Call Router", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000DFF RID: 3583
		public static readonly ExPerformanceCounter UMCallRouterCallsReceived = new ExPerformanceCounter("MSExchangeUMCallRouterAvailability", "Total Inbound Calls Received by the UM Call Router", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E00 RID: 3584
		public static readonly ExPerformanceCounter RecentUMCallRouterCallsRejected = new ExPerformanceCounter("MSExchangeUMCallRouterAvailability", "% of Inbound Calls Rejected by the UM Call Router Over the Last Hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E01 RID: 3585
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			CallRouterAvailabilityCounters.RecentMissedCallNotificationProxyFailed,
			CallRouterAvailabilityCounters.UMCallRouterCallsRejected,
			CallRouterAvailabilityCounters.UMCallRouterCallsReceived,
			CallRouterAvailabilityCounters.RecentUMCallRouterCallsRejected
		};
	}
}
