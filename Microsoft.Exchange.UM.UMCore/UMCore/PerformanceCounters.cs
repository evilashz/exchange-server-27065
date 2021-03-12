using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000322 RID: 802
	internal static class PerformanceCounters
	{
		// Token: 0x06001BBD RID: 7101 RVA: 0x0006B118 File Offset: 0x00069318
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in PerformanceCounters.AllCounters)
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

		// Token: 0x04000E3B RID: 3643
		public const string CategoryName = "MSExchangeUMPerformance";

		// Token: 0x04000E3C RID: 3644
		public static readonly ExPerformanceCounter OperationsUnderTwoSeconds = new ExPerformanceCounter("MSExchangeUMPerformance", "Operations under Two Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E3D RID: 3645
		public static readonly ExPerformanceCounter OperationsBetweenTwoAndThreeSeconds = new ExPerformanceCounter("MSExchangeUMPerformance", "Operations Between Two and Three Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E3E RID: 3646
		public static readonly ExPerformanceCounter OperationsBetweenThreeAndFourSeconds = new ExPerformanceCounter("MSExchangeUMPerformance", "Operations Between Three and Four Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E3F RID: 3647
		public static readonly ExPerformanceCounter OperationsBetweenFourAndFiveSeconds = new ExPerformanceCounter("MSExchangeUMPerformance", "Operations Between Four and Five Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E40 RID: 3648
		public static readonly ExPerformanceCounter OperationsBetweenFiveAndSixSeconds = new ExPerformanceCounter("MSExchangeUMPerformance", "Operations Between Five and Six Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E41 RID: 3649
		public static readonly ExPerformanceCounter OperationsOverSixSeconds = new ExPerformanceCounter("MSExchangeUMPerformance", "Operations over Six Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E42 RID: 3650
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			PerformanceCounters.OperationsUnderTwoSeconds,
			PerformanceCounters.OperationsBetweenTwoAndThreeSeconds,
			PerformanceCounters.OperationsBetweenThreeAndFourSeconds,
			PerformanceCounters.OperationsBetweenFourAndFiveSeconds,
			PerformanceCounters.OperationsBetweenFiveAndSixSeconds,
			PerformanceCounters.OperationsOverSixSeconds
		};
	}
}
