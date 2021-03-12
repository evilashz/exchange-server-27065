using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200023E RID: 574
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MSExchangeImapAggregation
	{
		// Token: 0x060014E7 RID: 5351 RVA: 0x0004B994 File Offset: 0x00049B94
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeImapAggregation.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MSExchangeImapAggregation.AllCounters)
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

		// Token: 0x04000AF2 RID: 2802
		public const string CategoryName = "MSExchange Transport Sync - IMAP";

		// Token: 0x04000AF3 RID: 2803
		private static readonly ExPerformanceCounter RateOfBytesDownloaded = new ExPerformanceCounter("MSExchange Transport Sync - IMAP", "Bytes Downloaded/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000AF4 RID: 2804
		public static readonly ExPerformanceCounter TotalBytesDownloaded = new ExPerformanceCounter("MSExchange Transport Sync - IMAP", "Total bytes downloaded", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeImapAggregation.RateOfBytesDownloaded
		});

		// Token: 0x04000AF5 RID: 2805
		private static readonly ExPerformanceCounter RateOfBytesUploaded = new ExPerformanceCounter("MSExchange Transport Sync - IMAP", "Bytes Uploaded/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000AF6 RID: 2806
		public static readonly ExPerformanceCounter TotalBytesUploaded = new ExPerformanceCounter("MSExchange Transport Sync - IMAP", "Total bytes uploaded", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeImapAggregation.RateOfBytesUploaded
		});

		// Token: 0x04000AF7 RID: 2807
		private static readonly ExPerformanceCounter RateOfMessagesDownloaded = new ExPerformanceCounter("MSExchange Transport Sync - IMAP", "Messages Downloaded/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000AF8 RID: 2808
		public static readonly ExPerformanceCounter TotalMessagesDownloaded = new ExPerformanceCounter("MSExchange Transport Sync - IMAP", "Total Messages Downloaded", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeImapAggregation.RateOfMessagesDownloaded
		});

		// Token: 0x04000AF9 RID: 2809
		private static readonly ExPerformanceCounter RateOfMessagesUploaded = new ExPerformanceCounter("MSExchange Transport Sync - IMAP", "Messages Uploaded/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000AFA RID: 2810
		public static readonly ExPerformanceCounter TotalMessagesUploaded = new ExPerformanceCounter("MSExchange Transport Sync - IMAP", "Total Messages Uploaded", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeImapAggregation.RateOfMessagesUploaded
		});

		// Token: 0x04000AFB RID: 2811
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MSExchangeImapAggregation.TotalBytesDownloaded,
			MSExchangeImapAggregation.TotalBytesUploaded,
			MSExchangeImapAggregation.TotalMessagesDownloaded,
			MSExchangeImapAggregation.TotalMessagesUploaded
		};
	}
}
