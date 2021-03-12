using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.ContentFilter
{
	// Token: 0x0200001F RID: 31
	internal static class ContentFilterPerfCounters
	{
		// Token: 0x060000BC RID: 188 RVA: 0x0000725C File Offset: 0x0000545C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ContentFilterPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ContentFilterPerfCounters.AllCounters)
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

		// Token: 0x040000BE RID: 190
		public const string CategoryName = "MSExchange Content Filter Agent";

		// Token: 0x040000BF RID: 191
		public static readonly ExPerformanceCounter TotalMessagesNotScanned = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages that Bypassed Scanning", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C0 RID: 192
		public static readonly ExPerformanceCounter TotalMessagesWithPreExistingSCL = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with a Preexisting SCL", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C1 RID: 193
		public static readonly ExPerformanceCounter TotalMessagesThatCauseFilterFailure = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with SCL Unknown", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C2 RID: 194
		private static readonly ExPerformanceCounter MessagesScannedPerSecond = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages Scanned Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C3 RID: 195
		public static readonly ExPerformanceCounter TotalMessagesScanned = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages Scanned", string.Empty, null, new ExPerformanceCounter[]
		{
			ContentFilterPerfCounters.MessagesScannedPerSecond
		});

		// Token: 0x040000C4 RID: 196
		public static readonly ExPerformanceCounter MessagesAtSCL0 = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with SCL 0", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C5 RID: 197
		public static readonly ExPerformanceCounter MessagesAtSCL1 = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with SCL 1", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C6 RID: 198
		public static readonly ExPerformanceCounter MessagesAtSCL2 = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with SCL 2", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C7 RID: 199
		public static readonly ExPerformanceCounter MessagesAtSCL3 = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with SCL 3", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C8 RID: 200
		public static readonly ExPerformanceCounter MessagesAtSCL4 = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with SCL 4", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C9 RID: 201
		public static readonly ExPerformanceCounter MessagesAtSCL5 = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with SCL 5", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CA RID: 202
		public static readonly ExPerformanceCounter MessagesAtSCL6 = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with SCL 6", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CB RID: 203
		public static readonly ExPerformanceCounter MessagesAtSCL7 = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with SCL 7", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CC RID: 204
		public static readonly ExPerformanceCounter MessagesAtSCL8 = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with SCL 8", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CD RID: 205
		public static readonly ExPerformanceCounter MessagesAtSCL9 = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages with SCL 9", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CE RID: 206
		public static readonly ExPerformanceCounter TotalMessagesDeleted = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages Deleted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CF RID: 207
		public static readonly ExPerformanceCounter TotalMessagesRejected = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages Rejected", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D0 RID: 208
		public static readonly ExPerformanceCounter TotalMessagesQuarantined = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages Quarantined", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D1 RID: 209
		public static readonly ExPerformanceCounter TotalMessagesNotScannedDueToOrgSafeSender = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages that Bypassed Scanning due to an organization-wide Safe Sender", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D2 RID: 210
		public static readonly ExPerformanceCounter TotalBypassedRecipientsDueToPerRecipientSafeSender = new ExPerformanceCounter("MSExchange Content Filter Agent", "Bypassed recipients due to per-recipient Safe Senders", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D3 RID: 211
		public static readonly ExPerformanceCounter TotalBypassedRecipientsDueToPerRecipientSafeRecipient = new ExPerformanceCounter("MSExchange Content Filter Agent", "Bypassed recipients due to per-recipient Safe Recipients", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D4 RID: 212
		public static readonly ExPerformanceCounter TotalMessagesWithValidPostmarks = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages that include an Outlook E-mail Postmark that validated successfully", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D5 RID: 213
		public static readonly ExPerformanceCounter TotalMessagesWithInvalidPostmarks = new ExPerformanceCounter("MSExchange Content Filter Agent", "Messages that include an Outlook E-mail Postmark that did not validate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D6 RID: 214
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ContentFilterPerfCounters.TotalMessagesNotScanned,
			ContentFilterPerfCounters.TotalMessagesWithPreExistingSCL,
			ContentFilterPerfCounters.TotalMessagesScanned,
			ContentFilterPerfCounters.TotalMessagesThatCauseFilterFailure,
			ContentFilterPerfCounters.MessagesAtSCL0,
			ContentFilterPerfCounters.MessagesAtSCL1,
			ContentFilterPerfCounters.MessagesAtSCL2,
			ContentFilterPerfCounters.MessagesAtSCL3,
			ContentFilterPerfCounters.MessagesAtSCL4,
			ContentFilterPerfCounters.MessagesAtSCL5,
			ContentFilterPerfCounters.MessagesAtSCL6,
			ContentFilterPerfCounters.MessagesAtSCL7,
			ContentFilterPerfCounters.MessagesAtSCL8,
			ContentFilterPerfCounters.MessagesAtSCL9,
			ContentFilterPerfCounters.TotalMessagesDeleted,
			ContentFilterPerfCounters.TotalMessagesRejected,
			ContentFilterPerfCounters.TotalMessagesQuarantined,
			ContentFilterPerfCounters.TotalMessagesNotScannedDueToOrgSafeSender,
			ContentFilterPerfCounters.TotalBypassedRecipientsDueToPerRecipientSafeSender,
			ContentFilterPerfCounters.TotalBypassedRecipientsDueToPerRecipientSafeRecipient,
			ContentFilterPerfCounters.TotalMessagesWithValidPostmarks,
			ContentFilterPerfCounters.TotalMessagesWithInvalidPostmarks
		};
	}
}
