using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001FD RID: 509
	internal static class OABGeneratorPerfmon
	{
		// Token: 0x0600138F RID: 5007 RVA: 0x000721A0 File Offset: 0x000703A0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (OABGeneratorPerfmon.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in OABGeneratorPerfmon.AllCounters)
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

		// Token: 0x04000BF3 RID: 3059
		public const string CategoryName = "MSExchange OAB Generator Assistant";

		// Token: 0x04000BF4 RID: 3060
		public static readonly ExPerformanceCounter TotalOABRecords = new ExPerformanceCounter("MSExchange OAB Generator Assistant", "Total number of OAB records", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000BF5 RID: 3061
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			OABGeneratorPerfmon.TotalOABRecords
		};
	}
}
