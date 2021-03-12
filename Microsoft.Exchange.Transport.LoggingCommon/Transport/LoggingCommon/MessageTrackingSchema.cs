using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x02000013 RID: 19
	public static class MessageTrackingSchema
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000027BF File Offset: 0x000009BF
		public static CsvTable MessageTrackingEvent
		{
			get
			{
				return MessageTrackingSchema.schema;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000027C6 File Offset: 0x000009C6
		public static List<Version> SupportedVersionsInAscendingOrder
		{
			get
			{
				return MessageTrackingSchema.supportedVersionsInAscendingOrder;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000027CD File Offset: 0x000009CD
		public static Version E15FirstVersion
		{
			get
			{
				return MessageTrackingSchema.E15Version;
			}
		}

		// Token: 0x040000A8 RID: 168
		public const string ClientHostName = "client-hostname";

		// Token: 0x040000A9 RID: 169
		public const string ServerHostName = "server-hostname";

		// Token: 0x040000AA RID: 170
		public const string SourceContext = "source-context";

		// Token: 0x040000AB RID: 171
		public const string ConnectorId = "connector-id";

		// Token: 0x040000AC RID: 172
		public const string Source = "source";

		// Token: 0x040000AD RID: 173
		public const string InternalMsgID = "internal-message-id";

		// Token: 0x040000AE RID: 174
		public const string NetworkMsgID = "network-message-id";

		// Token: 0x040000AF RID: 175
		public const string RecipientStatuses = "recipient-status";

		// Token: 0x040000B0 RID: 176
		public const string RelatedRecipientAddress = "related-recipient-address";

		// Token: 0x040000B1 RID: 177
		public const string Reference = "reference";

		// Token: 0x040000B2 RID: 178
		public const string ReturnPath = "return-path";

		// Token: 0x040000B3 RID: 179
		public const string MessageInfo = "message-info";

		// Token: 0x040000B4 RID: 180
		public const string Directionality = "directionality";

		// Token: 0x040000B5 RID: 181
		public const string CustomData = "custom-data";

		// Token: 0x040000B6 RID: 182
		private static readonly Version E12Version = new Version(12, 0, 0, 0);

		// Token: 0x040000B7 RID: 183
		private static readonly Version E14InterfaceUpdateVersion = new Version(14, 0, 533);

		// Token: 0x040000B8 RID: 184
		private static readonly Version CustomDataAddedVersion = new Version(14, 0, 552);

		// Token: 0x040000B9 RID: 185
		private static readonly Version E15Version = new Version(15, 0, 0, 0);

		// Token: 0x040000BA RID: 186
		private static readonly List<Version> supportedVersionsInAscendingOrder = new List<Version>
		{
			MessageTrackingSchema.CustomDataAddedVersion,
			MessageTrackingSchema.E15Version
		};

		// Token: 0x040000BB RID: 187
		private static readonly CsvTable schema = new CsvTable(new CsvField[]
		{
			new CsvField("date-time", typeof(DateTime), MessageTrackingSchema.E12Version),
			new CsvField("client-ip", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("client-hostname", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("server-ip", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("server-hostname", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("source-context", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("connector-id", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("source", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("event-id", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("internal-message-id", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("message-id", typeof(string), true, MessageTrackingSchema.E12Version, new NormalizeColumnDataMethod(CsvFieldCache.NormalizeMessageID)),
			new CsvField("network-message-id", typeof(string), MessageTrackingSchema.E15Version),
			new CsvField("recipient-address", typeof(string[]), MessageTrackingSchema.E12Version),
			new CsvField("recipient-status", typeof(string[]), MessageTrackingSchema.E12Version),
			new CsvField("total-bytes", typeof(int), MessageTrackingSchema.E12Version),
			new CsvField("recipient-count", typeof(int), MessageTrackingSchema.E12Version),
			new CsvField("related-recipient-address", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("reference", typeof(string[]), MessageTrackingSchema.E12Version),
			new CsvField("message-subject", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("sender-address", typeof(string), true, MessageTrackingSchema.E12Version, new NormalizeColumnDataMethod(CsvFieldCache.NormalizeEmailAddress)),
			new CsvField("return-path", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("message-info", typeof(string), MessageTrackingSchema.E12Version),
			new CsvField("directionality", typeof(string), MessageTrackingSchema.E14InterfaceUpdateVersion),
			new CsvField("tenant-id", typeof(string), MessageTrackingSchema.E14InterfaceUpdateVersion),
			new CsvField("original-client-ip", typeof(string), MessageTrackingSchema.E14InterfaceUpdateVersion),
			new CsvField("original-server-ip", typeof(string), MessageTrackingSchema.E14InterfaceUpdateVersion),
			new CsvField("custom-data", typeof(KeyValuePair<string, object>[]), MessageTrackingSchema.CustomDataAddedVersion)
		});
	}
}
