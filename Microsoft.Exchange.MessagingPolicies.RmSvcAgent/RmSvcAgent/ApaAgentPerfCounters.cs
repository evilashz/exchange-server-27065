using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200000B RID: 11
	internal static class ApaAgentPerfCounters
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00003658 File Offset: 0x00001858
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ApaAgentPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ApaAgentPerfCounters.AllCounters)
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

		// Token: 0x04000049 RID: 73
		public const string CategoryName = "MSExchange Encryption Agent";

		// Token: 0x0400004A RID: 74
		private static readonly ExPerformanceCounter RateOfMessagesEncrypted = new ExPerformanceCounter("MSExchange Encryption Agent", "Messages encrypted for policy/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400004B RID: 75
		public static readonly ExPerformanceCounter TotalMessagesEncrypted = new ExPerformanceCounter("MSExchange Encryption Agent", "Total messages encrypted for policy", string.Empty, null, new ExPerformanceCounter[]
		{
			ApaAgentPerfCounters.RateOfMessagesEncrypted
		});

		// Token: 0x0400004C RID: 76
		private static readonly ExPerformanceCounter RateOfMessagesFailed = new ExPerformanceCounter("MSExchange Encryption Agent", "Messages failed to encrypt for policy/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400004D RID: 77
		public static readonly ExPerformanceCounter TotalMessagesFailed = new ExPerformanceCounter("MSExchange Encryption Agent", "Total messages failed to encrypt for policy", string.Empty, null, new ExPerformanceCounter[]
		{
			ApaAgentPerfCounters.RateOfMessagesFailed
		});

		// Token: 0x0400004E RID: 78
		private static readonly ExPerformanceCounter RateOfDeferrals = new ExPerformanceCounter("MSExchange Encryption Agent", "Deferrals of messages for policy/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400004F RID: 79
		public static readonly ExPerformanceCounter TotalDeferrals = new ExPerformanceCounter("MSExchange Encryption Agent", "Total deferrals of messages for policy", string.Empty, null, new ExPerformanceCounter[]
		{
			ApaAgentPerfCounters.RateOfDeferrals
		});

		// Token: 0x04000050 RID: 80
		public static readonly ExPerformanceCounter Percentile95FailedToEncrypt = new ExPerformanceCounter("MSExchange Encryption Agent", "Over 5% of messages failing Transport Policy Encryption in the last 30 minutes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000051 RID: 81
		private static readonly ExPerformanceCounter RateOfMessagesReencrypted = new ExPerformanceCounter("MSExchange Encryption Agent", "Messages reencrypted/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000052 RID: 82
		public static readonly ExPerformanceCounter TotalMessagesReencrypted = new ExPerformanceCounter("MSExchange Encryption Agent", "Total messages reencrypted", string.Empty, null, new ExPerformanceCounter[]
		{
			ApaAgentPerfCounters.RateOfMessagesReencrypted
		});

		// Token: 0x04000053 RID: 83
		private static readonly ExPerformanceCounter RateOfMessagesFailedToReencrypt = new ExPerformanceCounter("MSExchange Encryption Agent", "Messages failed to reencrypt/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000054 RID: 84
		public static readonly ExPerformanceCounter TotalMessagesFailedToReencrypt = new ExPerformanceCounter("MSExchange Encryption Agent", "Total messages failed to reencrypt", string.Empty, null, new ExPerformanceCounter[]
		{
			ApaAgentPerfCounters.RateOfMessagesFailedToReencrypt
		});

		// Token: 0x04000055 RID: 85
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ApaAgentPerfCounters.TotalMessagesEncrypted,
			ApaAgentPerfCounters.TotalMessagesFailed,
			ApaAgentPerfCounters.TotalDeferrals,
			ApaAgentPerfCounters.Percentile95FailedToEncrypt,
			ApaAgentPerfCounters.TotalMessagesReencrypted,
			ApaAgentPerfCounters.TotalMessagesFailedToReencrypt
		};
	}
}
