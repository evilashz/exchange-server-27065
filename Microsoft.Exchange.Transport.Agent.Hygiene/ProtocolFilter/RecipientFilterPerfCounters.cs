using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.ProtocolFilter
{
	// Token: 0x0200002B RID: 43
	internal static class RecipientFilterPerfCounters
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00008D64 File Offset: 0x00006F64
		public static void GetPerfCounterInfo(XElement element)
		{
			if (RecipientFilterPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in RecipientFilterPerfCounters.AllCounters)
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

		// Token: 0x040000F7 RID: 247
		public const string CategoryName = "MSExchange Recipient Filter Agent";

		// Token: 0x040000F8 RID: 248
		private static readonly ExPerformanceCounter RecipientsRejectedByRecipientValidationPerSecond = new ExPerformanceCounter("MSExchange Recipient Filter Agent", "Recipients Rejected by Recipient Validation/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000F9 RID: 249
		public static readonly ExPerformanceCounter TotalRecipientsRejectedByRecipientValidation = new ExPerformanceCounter("MSExchange Recipient Filter Agent", "Recipients Rejected by Recipient Validation", string.Empty, null, new ExPerformanceCounter[]
		{
			RecipientFilterPerfCounters.RecipientsRejectedByRecipientValidationPerSecond
		});

		// Token: 0x040000FA RID: 250
		private static readonly ExPerformanceCounter RecipientsRejectedByBlockListPerSecond = new ExPerformanceCounter("MSExchange Recipient Filter Agent", "Recipients Rejected by Block List/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000FB RID: 251
		public static readonly ExPerformanceCounter TotalRecipientsRejectedByBlockList = new ExPerformanceCounter("MSExchange Recipient Filter Agent", "Recipients Rejected by Block List", string.Empty, null, new ExPerformanceCounter[]
		{
			RecipientFilterPerfCounters.RecipientsRejectedByBlockListPerSecond
		});

		// Token: 0x040000FC RID: 252
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			RecipientFilterPerfCounters.TotalRecipientsRejectedByRecipientValidation,
			RecipientFilterPerfCounters.TotalRecipientsRejectedByBlockList
		};
	}
}
