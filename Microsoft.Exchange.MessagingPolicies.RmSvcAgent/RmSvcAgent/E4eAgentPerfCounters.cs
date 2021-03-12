using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200000F RID: 15
	internal static class E4eAgentPerfCounters
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00003CFC File Offset: 0x00001EFC
		public static void GetPerfCounterInfo(XElement element)
		{
			if (E4eAgentPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in E4eAgentPerfCounters.AllCounters)
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

		// Token: 0x0400006A RID: 106
		public const string CategoryName = "MSExchange E4E Agent";

		// Token: 0x0400006B RID: 107
		private static readonly ExPerformanceCounter RateOfEncryptionSuccess = new ExPerformanceCounter("MSExchange E4E Agent", "Encryption Success Count Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006C RID: 108
		public static readonly ExPerformanceCounter EncryptionSuccessCount = new ExPerformanceCounter("MSExchange E4E Agent", "Encryption Success Count", string.Empty, null, new ExPerformanceCounter[]
		{
			E4eAgentPerfCounters.RateOfEncryptionSuccess
		});

		// Token: 0x0400006D RID: 109
		private static readonly ExPerformanceCounter RateOfAfterEncryptionSuccess = new ExPerformanceCounter("MSExchange E4E Agent", "After Encryption Success Count Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006E RID: 110
		public static readonly ExPerformanceCounter AfterEncryptionSuccessCount = new ExPerformanceCounter("MSExchange E4E Agent", "After Encryption Success Count", string.Empty, null, new ExPerformanceCounter[]
		{
			E4eAgentPerfCounters.RateOfAfterEncryptionSuccess
		});

		// Token: 0x0400006F RID: 111
		private static readonly ExPerformanceCounter RateOfReEncryptionSuccess = new ExPerformanceCounter("MSExchange E4E Agent", "Re-Encryption Success Count Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000070 RID: 112
		public static readonly ExPerformanceCounter ReEncryptionSuccessCount = new ExPerformanceCounter("MSExchange E4E Agent", "Re-Encryption Success Count", string.Empty, null, new ExPerformanceCounter[]
		{
			E4eAgentPerfCounters.RateOfReEncryptionSuccess
		});

		// Token: 0x04000071 RID: 113
		private static readonly ExPerformanceCounter RateOfEncryptionFailure = new ExPerformanceCounter("MSExchange E4E Agent", "Encryption Failure Count Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000072 RID: 114
		public static readonly ExPerformanceCounter EncryptionFailureCount = new ExPerformanceCounter("MSExchange E4E Agent", "Encryption Failure Count", string.Empty, null, new ExPerformanceCounter[]
		{
			E4eAgentPerfCounters.RateOfEncryptionFailure
		});

		// Token: 0x04000073 RID: 115
		private static readonly ExPerformanceCounter RateOfAfterEncryptionFailure = new ExPerformanceCounter("MSExchange E4E Agent", "After Encryption Failure Count Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000074 RID: 116
		public static readonly ExPerformanceCounter AfterEncryptionFailureCount = new ExPerformanceCounter("MSExchange E4E Agent", "After Encryption Failure Count", string.Empty, null, new ExPerformanceCounter[]
		{
			E4eAgentPerfCounters.RateOfAfterEncryptionFailure
		});

		// Token: 0x04000075 RID: 117
		private static readonly ExPerformanceCounter RateOfReEncryptionFailure = new ExPerformanceCounter("MSExchange E4E Agent", "Re-Encryption Failure Count Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000076 RID: 118
		public static readonly ExPerformanceCounter ReEncryptionFailureCount = new ExPerformanceCounter("MSExchange E4E Agent", "Re-Encryption Failure Count", string.Empty, null, new ExPerformanceCounter[]
		{
			E4eAgentPerfCounters.RateOfReEncryptionFailure
		});

		// Token: 0x04000077 RID: 119
		private static readonly ExPerformanceCounter RateOfDecryptionSuccess = new ExPerformanceCounter("MSExchange E4E Agent", "Decryption Success Count Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000078 RID: 120
		public static readonly ExPerformanceCounter DecryptionSuccessCount = new ExPerformanceCounter("MSExchange E4E Agent", "Decryption Success Count", string.Empty, null, new ExPerformanceCounter[]
		{
			E4eAgentPerfCounters.RateOfDecryptionSuccess
		});

		// Token: 0x04000079 RID: 121
		private static readonly ExPerformanceCounter RateOfDecryptionFailure = new ExPerformanceCounter("MSExchange E4E Agent", "Decryption Failure Count Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400007A RID: 122
		public static readonly ExPerformanceCounter DecryptionFailureCount = new ExPerformanceCounter("MSExchange E4E Agent", "Decryption Failure Count", string.Empty, null, new ExPerformanceCounter[]
		{
			E4eAgentPerfCounters.RateOfDecryptionFailure
		});

		// Token: 0x0400007B RID: 123
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			E4eAgentPerfCounters.EncryptionSuccessCount,
			E4eAgentPerfCounters.AfterEncryptionSuccessCount,
			E4eAgentPerfCounters.ReEncryptionSuccessCount,
			E4eAgentPerfCounters.EncryptionFailureCount,
			E4eAgentPerfCounters.AfterEncryptionFailureCount,
			E4eAgentPerfCounters.ReEncryptionFailureCount,
			E4eAgentPerfCounters.DecryptionSuccessCount,
			E4eAgentPerfCounters.DecryptionFailureCount
		};
	}
}
