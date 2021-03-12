using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Approval
{
	// Token: 0x0200012F RID: 303
	internal static class ApprovalAssistantPerformanceCounters
	{
		// Token: 0x06000C2A RID: 3114 RVA: 0x0004F2B8 File Offset: 0x0004D4B8
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ApprovalAssistantPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ApprovalAssistantPerformanceCounters.AllCounters)
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

		// Token: 0x04000787 RID: 1927
		public const string CategoryName = "MSExchange Approval Assistant";

		// Token: 0x04000788 RID: 1928
		public static readonly ExPerformanceCounter ApprovalRequestsProcessed = new ExPerformanceCounter("MSExchange Approval Assistant", "Approval Requests Processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000789 RID: 1929
		public static readonly ExPerformanceCounter ApprovalRequestsApproved = new ExPerformanceCounter("MSExchange Approval Assistant", "Approval Requests Approved", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400078A RID: 1930
		public static readonly ExPerformanceCounter ApprovalRequestsRejected = new ExPerformanceCounter("MSExchange Approval Assistant", "Approval Requests Rejected", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400078B RID: 1931
		public static readonly ExPerformanceCounter ApprovalRequestsExpired = new ExPerformanceCounter("MSExchange Approval Assistant", "Approval Requests Expired", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400078C RID: 1932
		public static readonly ExPerformanceCounter LastApprovalAssistantProcessingTime = new ExPerformanceCounter("MSExchange Approval Assistant", "Last Approval Assistant Processing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400078D RID: 1933
		public static readonly ExPerformanceCounter AverageApprovalAssistantProcessingTime = new ExPerformanceCounter("MSExchange Approval Assistant", "Average Approval Assistant Processing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400078E RID: 1934
		public static readonly ExPerformanceCounter AverageApprovalAssistantProcessingTimeBase = new ExPerformanceCounter("MSExchange Approval Assistant", "Average Approval Assistant Processing Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400078F RID: 1935
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ApprovalAssistantPerformanceCounters.ApprovalRequestsProcessed,
			ApprovalAssistantPerformanceCounters.ApprovalRequestsApproved,
			ApprovalAssistantPerformanceCounters.ApprovalRequestsRejected,
			ApprovalAssistantPerformanceCounters.ApprovalRequestsExpired,
			ApprovalAssistantPerformanceCounters.LastApprovalAssistantProcessingTime,
			ApprovalAssistantPerformanceCounters.AverageApprovalAssistantProcessingTime,
			ApprovalAssistantPerformanceCounters.AverageApprovalAssistantProcessingTimeBase
		};
	}
}
