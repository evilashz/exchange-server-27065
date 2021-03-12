using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000034 RID: 52
	internal static class SenderIdPerfCounters
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x000052F0 File Offset: 0x000034F0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SenderIdPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in SenderIdPerfCounters.AllCounters)
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

		// Token: 0x04000085 RID: 133
		public const string CategoryName = "MSExchange Sender Id Agent";

		// Token: 0x04000086 RID: 134
		private static readonly ExPerformanceCounter MessagesValidatedPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000087 RID: 135
		public static readonly ExPerformanceCounter TotalMessagesValidated = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesValidatedPerSecond
		});

		// Token: 0x04000088 RID: 136
		private static readonly ExPerformanceCounter MessagesThatBypassedValidationPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages That Bypassed Validation/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000089 RID: 137
		public static readonly ExPerformanceCounter TotalMessagesThatBypassedValidation = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages That Bypassed Validation", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesThatBypassedValidationPerSecond
		});

		// Token: 0x0400008A RID: 138
		private static readonly ExPerformanceCounter MessagesWithPassResultPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated/sec with a Pass Result", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008B RID: 139
		public static readonly ExPerformanceCounter TotalMessagesWithPassResult = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated with a Pass Result", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesWithPassResultPerSecond
		});

		// Token: 0x0400008C RID: 140
		private static readonly ExPerformanceCounter MessagesWithNeutralResultPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated/sec with a Neutral Result", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008D RID: 141
		public static readonly ExPerformanceCounter TotalMessagesWithNeutralResult = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated with a Neutral Result", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesWithNeutralResultPerSecond
		});

		// Token: 0x0400008E RID: 142
		private static readonly ExPerformanceCounter MessagesWithSoftFailResultPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated/sec with a SoftFail Result", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008F RID: 143
		public static readonly ExPerformanceCounter TotalMessagesWithSoftFailResult = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated with a SoftFail Result", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesWithSoftFailResultPerSecond
		});

		// Token: 0x04000090 RID: 144
		private static readonly ExPerformanceCounter MessagesWithFailNotPermittedResultPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated/sec with a Fail Not - Permitted Result", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000091 RID: 145
		public static readonly ExPerformanceCounter TotalMessagesWithFailNotPermittedResult = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated with a Fail - Not Permitted Result", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesWithFailNotPermittedResultPerSecond
		});

		// Token: 0x04000092 RID: 146
		private static readonly ExPerformanceCounter MessagesWithFailMalformedDomainResultPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated/sec with a Fail - Malformed Domain Result", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000093 RID: 147
		public static readonly ExPerformanceCounter TotalMessagesWithFailMalformedDomainResult = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated with a Fail - Malformed Domain Result", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesWithFailMalformedDomainResultPerSecond
		});

		// Token: 0x04000094 RID: 148
		private static readonly ExPerformanceCounter MessagesWithFailNonExistentDomainResultPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated/sec with a Fail - Non-Existent Domain Result", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000095 RID: 149
		public static readonly ExPerformanceCounter TotalMessagesWithFailNonExistentDomainResult = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated with a Fail - Non-Existent Domain Result", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesWithFailNonExistentDomainResultPerSecond
		});

		// Token: 0x04000096 RID: 150
		private static readonly ExPerformanceCounter MessagesWithNoneResultPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated/sec with a None Result", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000097 RID: 151
		public static readonly ExPerformanceCounter TotalMessagesWithNoneResult = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated with a None Result", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesWithNoneResultPerSecond
		});

		// Token: 0x04000098 RID: 152
		private static readonly ExPerformanceCounter MessagesWithTempErrorResultPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated/sec with a TempError Result", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000099 RID: 153
		public static readonly ExPerformanceCounter TotalMessagesWithTempErrorResult = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated with a TempError Result", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesWithTempErrorResultPerSecond
		});

		// Token: 0x0400009A RID: 154
		private static readonly ExPerformanceCounter MessagesWithPermErrorResultPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated/sec with a PermError Result", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009B RID: 155
		public static readonly ExPerformanceCounter TotalMessagesWithPermErrorResult = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Validated with a PermError Result", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesWithPermErrorResultPerSecond
		});

		// Token: 0x0400009C RID: 156
		private static readonly ExPerformanceCounter DnsQueriesPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "DNS Queries/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009D RID: 157
		public static readonly ExPerformanceCounter TotalDnsQueries = new ExPerformanceCounter("MSExchange Sender Id Agent", "DNS Queries", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.DnsQueriesPerSecond
		});

		// Token: 0x0400009E RID: 158
		private static readonly ExPerformanceCounter MessagesWithNoPRAPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages With No PRA/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009F RID: 159
		public static readonly ExPerformanceCounter TotalMessagesWithNoPRA = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages with No PRA", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesWithNoPRAPerSecond
		});

		// Token: 0x040000A0 RID: 160
		private static readonly ExPerformanceCounter MessagesMissingOriginatingIPPerSecond = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Missing Originating IP/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A1 RID: 161
		public static readonly ExPerformanceCounter TotalMessagesMissingOriginatingIP = new ExPerformanceCounter("MSExchange Sender Id Agent", "Messages Missing Originating IP", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.MessagesMissingOriginatingIPPerSecond
		});

		// Token: 0x040000A2 RID: 162
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			SenderIdPerfCounters.TotalMessagesValidated,
			SenderIdPerfCounters.TotalMessagesThatBypassedValidation,
			SenderIdPerfCounters.TotalMessagesWithPassResult,
			SenderIdPerfCounters.TotalMessagesWithNeutralResult,
			SenderIdPerfCounters.TotalMessagesWithSoftFailResult,
			SenderIdPerfCounters.TotalMessagesWithFailNotPermittedResult,
			SenderIdPerfCounters.TotalMessagesWithFailMalformedDomainResult,
			SenderIdPerfCounters.TotalMessagesWithFailNonExistentDomainResult,
			SenderIdPerfCounters.TotalMessagesWithNoneResult,
			SenderIdPerfCounters.TotalMessagesWithTempErrorResult,
			SenderIdPerfCounters.TotalMessagesWithPermErrorResult,
			SenderIdPerfCounters.TotalDnsQueries,
			SenderIdPerfCounters.TotalMessagesWithNoPRA,
			SenderIdPerfCounters.TotalMessagesMissingOriginatingIP
		};
	}
}
