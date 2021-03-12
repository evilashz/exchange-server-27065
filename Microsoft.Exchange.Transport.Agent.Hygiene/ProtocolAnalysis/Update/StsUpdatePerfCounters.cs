using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Update
{
	// Token: 0x0200006B RID: 107
	internal static class StsUpdatePerfCounters
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x00012DE8 File Offset: 0x00010FE8
		public static void GetPerfCounterInfo(XElement element)
		{
			if (StsUpdatePerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in StsUpdatePerfCounters.AllCounters)
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

		// Token: 0x04000259 RID: 601
		public const string CategoryName = "MSExchange Update Agent";

		// Token: 0x0400025A RID: 602
		public static readonly ExPerformanceCounter TotalUpdate = new ExPerformanceCounter("MSExchange Update Agent", "Total Updates", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400025B RID: 603
		public static readonly ExPerformanceCounter TotalSrlUpdate = new ExPerformanceCounter("MSExchange Update Agent", "Total SRL Parameter Updates", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400025C RID: 604
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			StsUpdatePerfCounters.TotalUpdate,
			StsUpdatePerfCounters.TotalSrlUpdate
		};
	}
}
