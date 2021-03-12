using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis
{
	// Token: 0x02000046 RID: 70
	internal static class ProtocolAnalysisPerfCounters
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ProtocolAnalysisPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ProtocolAnalysisPerfCounters.AllCounters)
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

		// Token: 0x04000182 RID: 386
		public const string CategoryName = "MSExchange Protocol Analysis Agent";

		// Token: 0x04000183 RID: 387
		public static readonly ExPerformanceCounter SenderSRL0 = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Calculations at SRL 0", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000184 RID: 388
		public static readonly ExPerformanceCounter SenderSRL1 = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Calculations at SRL 1", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000185 RID: 389
		public static readonly ExPerformanceCounter SenderSRL2 = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Calculations at SRL 2", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000186 RID: 390
		public static readonly ExPerformanceCounter SenderSRL3 = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Calculations at SRL 3", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000187 RID: 391
		public static readonly ExPerformanceCounter SenderSRL4 = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Calculations at SRL 4", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000188 RID: 392
		public static readonly ExPerformanceCounter SenderSRL5 = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Calculations at SRL 5", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000189 RID: 393
		public static readonly ExPerformanceCounter SenderSRL6 = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Calculations at SRL 6", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018A RID: 394
		public static readonly ExPerformanceCounter SenderSRL7 = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Calculations at SRL 7", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018B RID: 395
		public static readonly ExPerformanceCounter SenderSRL8 = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Calculations at SRL 8", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018C RID: 396
		public static readonly ExPerformanceCounter SenderSRL9 = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Calculations at SRL 9", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018D RID: 397
		public static readonly ExPerformanceCounter BlockSenderLocalSrl = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Senders Blocked Because of Local SRL", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018E RID: 398
		public static readonly ExPerformanceCounter BlockSenderRemoteSrl = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Senders Blocked Because of Remote SRL", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018F RID: 399
		public static readonly ExPerformanceCounter BlockSenderLocalOpenProxy = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Senders Blocked Because of Local Open Proxy", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000190 RID: 400
		public static readonly ExPerformanceCounter BlockSenderRemoteOpenProxy = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Senders Blocked Because of Remote Open Proxy", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000191 RID: 401
		public static readonly ExPerformanceCounter BypassSrlCalculation = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Senders Bypass Local SRL Calculation", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000192 RID: 402
		public static readonly ExPerformanceCounter SenderProcessed = new ExPerformanceCounter("MSExchange Protocol Analysis Agent", "Senders Processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000193 RID: 403
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ProtocolAnalysisPerfCounters.SenderSRL0,
			ProtocolAnalysisPerfCounters.SenderSRL1,
			ProtocolAnalysisPerfCounters.SenderSRL2,
			ProtocolAnalysisPerfCounters.SenderSRL3,
			ProtocolAnalysisPerfCounters.SenderSRL4,
			ProtocolAnalysisPerfCounters.SenderSRL5,
			ProtocolAnalysisPerfCounters.SenderSRL6,
			ProtocolAnalysisPerfCounters.SenderSRL7,
			ProtocolAnalysisPerfCounters.SenderSRL8,
			ProtocolAnalysisPerfCounters.SenderSRL9,
			ProtocolAnalysisPerfCounters.BlockSenderLocalSrl,
			ProtocolAnalysisPerfCounters.BlockSenderRemoteSrl,
			ProtocolAnalysisPerfCounters.BlockSenderLocalOpenProxy,
			ProtocolAnalysisPerfCounters.BlockSenderRemoteOpenProxy,
			ProtocolAnalysisPerfCounters.BypassSrlCalculation,
			ProtocolAnalysisPerfCounters.SenderProcessed
		};
	}
}
