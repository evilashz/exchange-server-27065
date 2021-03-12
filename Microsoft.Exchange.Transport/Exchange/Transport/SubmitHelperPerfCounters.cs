using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000553 RID: 1363
	internal static class SubmitHelperPerfCounters
	{
		// Token: 0x06003EDB RID: 16091 RVA: 0x0010D674 File Offset: 0x0010B874
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SubmitHelperPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in SubmitHelperPerfCounters.AllCounters)
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

		// Token: 0x040022FA RID: 8954
		public const string CategoryName = "MSExchangeTransport Submit Helper";

		// Token: 0x040022FB RID: 8955
		public static readonly ExPerformanceCounter AgentSubmitted = new ExPerformanceCounter("MSExchangeTransport Submit Helper", "Agent Messages Submitted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040022FC RID: 8956
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			SubmitHelperPerfCounters.AgentSubmitted
		};
	}
}
