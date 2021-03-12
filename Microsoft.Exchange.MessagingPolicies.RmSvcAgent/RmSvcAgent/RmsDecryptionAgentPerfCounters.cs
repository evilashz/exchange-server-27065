using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200000D RID: 13
	internal static class RmsDecryptionAgentPerfCounters
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00003A68 File Offset: 0x00001C68
		public static void GetPerfCounterInfo(XElement element)
		{
			if (RmsDecryptionAgentPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in RmsDecryptionAgentPerfCounters.AllCounters)
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

		// Token: 0x0400005E RID: 94
		public const string CategoryName = "MSExchange RMS Decryption Agent";

		// Token: 0x0400005F RID: 95
		public static readonly ExPerformanceCounter MessageDecrypted = new ExPerformanceCounter("MSExchange RMS Decryption Agent", "Message successfully decrypted by RMS Decryption Agent", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000060 RID: 96
		public static readonly ExPerformanceCounter MessageFailedToDecrypt = new ExPerformanceCounter("MSExchange RMS Decryption Agent", "Message failed to be decrypted by RMS Decryption Agent", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000061 RID: 97
		public static readonly ExPerformanceCounter Percentile95FailedToDecrypt = new ExPerformanceCounter("MSExchange RMS Decryption Agent", "Over 5% of messages failed to decrypt in the last 30 minutes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000062 RID: 98
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			RmsDecryptionAgentPerfCounters.MessageDecrypted,
			RmsDecryptionAgentPerfCounters.MessageFailedToDecrypt,
			RmsDecryptionAgentPerfCounters.Percentile95FailedToDecrypt
		};
	}
}
