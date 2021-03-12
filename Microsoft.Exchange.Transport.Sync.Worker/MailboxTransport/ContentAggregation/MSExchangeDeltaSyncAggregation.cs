﻿using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200023D RID: 573
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MSExchangeDeltaSyncAggregation
	{
		// Token: 0x060014E5 RID: 5349 RVA: 0x0004B7A0 File Offset: 0x000499A0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeDeltaSyncAggregation.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MSExchangeDeltaSyncAggregation.AllCounters)
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

		// Token: 0x04000AE8 RID: 2792
		public const string CategoryName = "MSExchange Transport Sync - Hotmail";

		// Token: 0x04000AE9 RID: 2793
		private static readonly ExPerformanceCounter RateOfBytesDownloaded = new ExPerformanceCounter("MSExchange Transport Sync - Hotmail", "Bytes Downloaded/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000AEA RID: 2794
		public static readonly ExPerformanceCounter TotalBytesDownloaded = new ExPerformanceCounter("MSExchange Transport Sync - Hotmail", "Total bytes downloaded", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeDeltaSyncAggregation.RateOfBytesDownloaded
		});

		// Token: 0x04000AEB RID: 2795
		private static readonly ExPerformanceCounter RateOfBytesUploaded = new ExPerformanceCounter("MSExchange Transport Sync - Hotmail", "Bytes Uploaded/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000AEC RID: 2796
		public static readonly ExPerformanceCounter TotalBytesUploaded = new ExPerformanceCounter("MSExchange Transport Sync - Hotmail", "Total bytes uploaded", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeDeltaSyncAggregation.RateOfBytesUploaded
		});

		// Token: 0x04000AED RID: 2797
		private static readonly ExPerformanceCounter RateOfMessagesDownloaded = new ExPerformanceCounter("MSExchange Transport Sync - Hotmail", "Messages Downloaded/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000AEE RID: 2798
		public static readonly ExPerformanceCounter TotalMessagesDownloaded = new ExPerformanceCounter("MSExchange Transport Sync - Hotmail", "Total Messages Downloaded", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeDeltaSyncAggregation.RateOfMessagesDownloaded
		});

		// Token: 0x04000AEF RID: 2799
		private static readonly ExPerformanceCounter RateOfMessagesUploaded = new ExPerformanceCounter("MSExchange Transport Sync - Hotmail", "Messages Uploaded/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000AF0 RID: 2800
		public static readonly ExPerformanceCounter TotalMessagesUploaded = new ExPerformanceCounter("MSExchange Transport Sync - Hotmail", "Total Messages Uploaded", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeDeltaSyncAggregation.RateOfMessagesUploaded
		});

		// Token: 0x04000AF1 RID: 2801
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MSExchangeDeltaSyncAggregation.TotalBytesDownloaded,
			MSExchangeDeltaSyncAggregation.TotalBytesUploaded,
			MSExchangeDeltaSyncAggregation.TotalMessagesDownloaded,
			MSExchangeDeltaSyncAggregation.TotalMessagesUploaded
		};
	}
}
