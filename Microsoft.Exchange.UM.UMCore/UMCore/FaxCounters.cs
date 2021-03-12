using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000308 RID: 776
	internal static class FaxCounters
	{
		// Token: 0x06001786 RID: 6022 RVA: 0x00065334 File Offset: 0x00063534
		public static void GetPerfCounterInfo(XElement element)
		{
			if (FaxCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in FaxCounters.AllCounters)
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

		// Token: 0x04000E17 RID: 3607
		public const string CategoryName = "MSExchangeUMFax";

		// Token: 0x04000E18 RID: 3608
		public static readonly ExPerformanceCounter TotalNumberOfInvalidFaxCalls = new ExPerformanceCounter("MSExchangeUMFax", "Total Invalid Fax Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E19 RID: 3609
		public static readonly ExPerformanceCounter TotalNumberOfValidFaxCalls = new ExPerformanceCounter("MSExchangeUMFax", "Total Valid Fax Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E1A RID: 3610
		public static readonly ExPerformanceCounter TotalNumberOfSuccessfulValidFaxCalls = new ExPerformanceCounter("MSExchangeUMFax", "Total Succesful Valid Fax Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E1B RID: 3611
		public static readonly ExPerformanceCounter PercentageSuccessfulValidFaxCalls = new ExPerformanceCounter("MSExchangeUMFax", "Percentage of Successful Valid Fax Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E1C RID: 3612
		public static readonly ExPerformanceCounter PercentageSuccessfulValidFaxCalls_Base = new ExPerformanceCounter("MSExchangeUMFax", "Base for Percentage Successful Valid Fax Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E1D RID: 3613
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			FaxCounters.TotalNumberOfInvalidFaxCalls,
			FaxCounters.TotalNumberOfValidFaxCalls,
			FaxCounters.TotalNumberOfSuccessfulValidFaxCalls,
			FaxCounters.PercentageSuccessfulValidFaxCalls,
			FaxCounters.PercentageSuccessfulValidFaxCalls_Base
		};
	}
}
