using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000022 RID: 34
	internal static class MailboxLoadBalancePerformanceCounters
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00003270 File Offset: 0x00001470
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MailboxLoadBalancePerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MailboxLoadBalancePerformanceCounters.AllCounters)
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

		// Token: 0x04000039 RID: 57
		public const string CategoryName = "MSExchange Mailbox Load Balancing";

		// Token: 0x0400003A RID: 58
		public static readonly ExPerformanceCounter InjectionRequests = new ExPerformanceCounter("MSExchange Mailbox Load Balancing", "Injection requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003B RID: 59
		public static readonly ExPerformanceCounter CacheEntries = new ExPerformanceCounter("MSExchange Mailbox Load Balancing", "Cache entries", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003C RID: 60
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MailboxLoadBalancePerformanceCounters.InjectionRequests,
			MailboxLoadBalancePerformanceCounters.CacheEntries
		};
	}
}
