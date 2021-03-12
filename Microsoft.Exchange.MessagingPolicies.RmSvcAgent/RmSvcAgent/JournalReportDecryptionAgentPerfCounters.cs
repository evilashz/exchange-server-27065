using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200000C RID: 12
	internal static class JournalReportDecryptionAgentPerfCounters
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000038CC File Offset: 0x00001ACC
		public static void GetPerfCounterInfo(XElement element)
		{
			if (JournalReportDecryptionAgentPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in JournalReportDecryptionAgentPerfCounters.AllCounters)
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

		// Token: 0x04000056 RID: 86
		public const string CategoryName = "MSExchange Journal Report Decryption Agent";

		// Token: 0x04000057 RID: 87
		private static readonly ExPerformanceCounter RateOfJRDecrypted = new ExPerformanceCounter("MSExchange Journal Report Decryption Agent", "Journal Reports decrypted/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000058 RID: 88
		public static readonly ExPerformanceCounter TotalJRDecrypted = new ExPerformanceCounter("MSExchange Journal Report Decryption Agent", "Total Journal Reports decrypted", string.Empty, null, new ExPerformanceCounter[]
		{
			JournalReportDecryptionAgentPerfCounters.RateOfJRDecrypted
		});

		// Token: 0x04000059 RID: 89
		private static readonly ExPerformanceCounter RateOfJRFailed = new ExPerformanceCounter("MSExchange Journal Report Decryption Agent", "Journal Reports failed to decrypt/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400005A RID: 90
		public static readonly ExPerformanceCounter TotalJRFailed = new ExPerformanceCounter("MSExchange Journal Report Decryption Agent", "Total Journal Reports failed to decrypt", string.Empty, null, new ExPerformanceCounter[]
		{
			JournalReportDecryptionAgentPerfCounters.RateOfJRFailed
		});

		// Token: 0x0400005B RID: 91
		private static readonly ExPerformanceCounter RateOfDeferrals = new ExPerformanceCounter("MSExchange Journal Report Decryption Agent", "Deferrals of Journal Reports/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400005C RID: 92
		public static readonly ExPerformanceCounter TotalDeferrals = new ExPerformanceCounter("MSExchange Journal Report Decryption Agent", "Total deferrals of Journal Reports", string.Empty, null, new ExPerformanceCounter[]
		{
			JournalReportDecryptionAgentPerfCounters.RateOfDeferrals
		});

		// Token: 0x0400005D RID: 93
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			JournalReportDecryptionAgentPerfCounters.TotalJRDecrypted,
			JournalReportDecryptionAgentPerfCounters.TotalJRFailed,
			JournalReportDecryptionAgentPerfCounters.TotalDeferrals
		};
	}
}
