using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.FrontendProxyRoutingAgent
{
	// Token: 0x02000006 RID: 6
	internal static class FrontendProxyAgentPerformanceCounters
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002318 File Offset: 0x00000518
		public static void GetPerfCounterInfo(XElement element)
		{
			if (FrontendProxyAgentPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in FrontendProxyAgentPerformanceCounters.AllCounters)
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

		// Token: 0x04000009 RID: 9
		public const string CategoryName = "MSExchangeFrontendTransport Proxy Routing Agent";

		// Token: 0x0400000A RID: 10
		public static readonly ExPerformanceCounter MessagesSuccessfullyRouted = new ExPerformanceCounter("MSExchangeFrontendTransport Proxy Routing Agent", "MessagesSuccessfullyRouted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400000B RID: 11
		public static readonly ExPerformanceCounter MessagesFailedToRoute = new ExPerformanceCounter("MSExchangeFrontendTransport Proxy Routing Agent", "MessagesFailedToRoute", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400000C RID: 12
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			FrontendProxyAgentPerformanceCounters.MessagesSuccessfullyRouted,
			FrontendProxyAgentPerformanceCounters.MessagesFailedToRoute
		};
	}
}
