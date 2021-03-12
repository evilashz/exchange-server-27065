using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200004A RID: 74
	internal static class MSExchangeStoreDriverSubmission
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeStoreDriverSubmission.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MSExchangeStoreDriverSubmission.AllCounters)
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

		// Token: 0x0400019D RID: 413
		public const string CategoryName = "MSExchange Submission Store Driver";

		// Token: 0x0400019E RID: 414
		private static readonly ExPerformanceCounter SubmittedMailItemsPerSecond = new ExPerformanceCounter("MSExchange Submission Store Driver", "Outbound: Submitted Mail Items Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400019F RID: 415
		public static readonly ExPerformanceCounter SubmittedMailItems = new ExPerformanceCounter("MSExchange Submission Store Driver", "Outbound: Submitted Mail Items", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeStoreDriverSubmission.SubmittedMailItemsPerSecond
		});

		// Token: 0x040001A0 RID: 416
		public static readonly ExPerformanceCounter TotalRecipients = new ExPerformanceCounter("MSExchange Submission Store Driver", "Outbound: TotalRecipients", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001A1 RID: 417
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MSExchangeStoreDriverSubmission.SubmittedMailItems,
			MSExchangeStoreDriverSubmission.TotalRecipients
		};
	}
}
