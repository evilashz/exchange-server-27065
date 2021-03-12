using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x02000015 RID: 21
	internal static class SpamDigestLogSchema
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002E4B File Offset: 0x0000104B
		public static CsvTable Schema
		{
			get
			{
				return SpamDigestLogSchema.schema;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002E52 File Offset: 0x00001052
		public static CsvTable DefaultSchema
		{
			get
			{
				return SpamDigestLogSchema.schema;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002E59 File Offset: 0x00001059
		public static Version DefaultVersion
		{
			get
			{
				return SpamDigestLogSchema.E15Version;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002E60 File Offset: 0x00001060
		public static List<Version> SupportedVersionsInAscendingOrder
		{
			get
			{
				return SpamDigestLogSchema.supportedVersionsInAscendingOrder;
			}
		}

		// Token: 0x040000D5 RID: 213
		public const string Timestamp = "Timestamp";

		// Token: 0x040000D6 RID: 214
		public const string EventId = "EventId";

		// Token: 0x040000D7 RID: 215
		public const string Source = "Source";

		// Token: 0x040000D8 RID: 216
		public const string Status = "Status";

		// Token: 0x040000D9 RID: 217
		public const string SourceContext = "Source-Context";

		// Token: 0x040000DA RID: 218
		public const string TenantId = "TenantId";

		// Token: 0x040000DB RID: 219
		public const string ExMessageId = "ExMessageId";

		// Token: 0x040000DC RID: 220
		public const string MessageId = "MessageId";

		// Token: 0x040000DD RID: 221
		public const string Sender = "Sender";

		// Token: 0x040000DE RID: 222
		public const string Recipient = "Recipient";

		// Token: 0x040000DF RID: 223
		public const string Subject = "Subject";

		// Token: 0x040000E0 RID: 224
		public const string Error = "Error";

		// Token: 0x040000E1 RID: 225
		public const string Data = "Data";

		// Token: 0x040000E2 RID: 226
		public const string CustomData = "CustomData";

		// Token: 0x040000E3 RID: 227
		public static readonly Version E15Version = new Version(15, 0, 0, 0);

		// Token: 0x040000E4 RID: 228
		private static readonly List<Version> supportedVersionsInAscendingOrder = new List<Version>
		{
			SpamDigestLogSchema.E15Version
		};

		// Token: 0x040000E5 RID: 229
		private static readonly CsvTable schema = new CsvTable(new CsvField[]
		{
			new CsvField("Timestamp", typeof(DateTime), SpamDigestLogSchema.E15Version),
			new CsvField("EventId", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("Source", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("Status", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("Source-Context", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("TenantId", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("ExMessageId", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("MessageId", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("Sender", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("Recipient", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("Subject", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("Error", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("Data", typeof(string), SpamDigestLogSchema.E15Version),
			new CsvField("CustomData", typeof(KeyValuePair<string, object>[]), SpamDigestLogSchema.E15Version)
		});
	}
}
