using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.QueueViewer
{
	// Token: 0x02000014 RID: 20
	internal static class QueueLogSchema
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002B78 File Offset: 0x00000D78
		public static CsvTable Schema
		{
			get
			{
				return QueueLogSchema.schema;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002B7F File Offset: 0x00000D7F
		public static string LogPrefix
		{
			get
			{
				return "TRANSPORTQUEUE";
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002B86 File Offset: 0x00000D86
		public static List<Version> SupportedVersionsInAscendingOrder
		{
			get
			{
				return QueueLogSchema.supportedVersionsInAscendingOrder;
			}
		}

		// Token: 0x040000BC RID: 188
		public const string Timestamp = "Timestamp";

		// Token: 0x040000BD RID: 189
		public const string SequenceNumber = "SequenceNumber";

		// Token: 0x040000BE RID: 190
		public const string EventId = "EventId";

		// Token: 0x040000BF RID: 191
		public const string QueueIdentity = "QueueIdentity";

		// Token: 0x040000C0 RID: 192
		public const string QueueStatus = "QueueStatus";

		// Token: 0x040000C1 RID: 193
		public const string DeliveryType = "DeliveryType";

		// Token: 0x040000C2 RID: 194
		public const string NextHopDomain = "NextHopDomain";

		// Token: 0x040000C3 RID: 195
		public const string NextHopKey = "NextHopKey";

		// Token: 0x040000C4 RID: 196
		public const string ActiveMessageCount = "ActiveMessageCount";

		// Token: 0x040000C5 RID: 197
		public const string LockedMessageCount = "LockedMessageCount";

		// Token: 0x040000C6 RID: 198
		public const string DeferredMessageCount = "DeferredMessageCount";

		// Token: 0x040000C7 RID: 199
		public const string IncomingRate = "IncomingRate";

		// Token: 0x040000C8 RID: 200
		public const string NextHopCategory = "NextHopCategory";

		// Token: 0x040000C9 RID: 201
		public const string OutgoingRate = "OutgoingRate";

		// Token: 0x040000CA RID: 202
		public const string RiskLevel = "RiskLevel";

		// Token: 0x040000CB RID: 203
		public const string OutboundIPPool = "OutboundIPPool";

		// Token: 0x040000CC RID: 204
		public const string LastError = "LastError";

		// Token: 0x040000CD RID: 205
		public const string Velocity = "Velocity";

		// Token: 0x040000CE RID: 206
		public const string NextHopConnector = "NextHopConnector";

		// Token: 0x040000CF RID: 207
		public const string TlsDomain = "TlsDomain";

		// Token: 0x040000D0 RID: 208
		public const string Data = "Data";

		// Token: 0x040000D1 RID: 209
		public const string CustomData = "CustomData";

		// Token: 0x040000D2 RID: 210
		public static readonly Version E15Version = new Version(15, 0, 0, 0);

		// Token: 0x040000D3 RID: 211
		private static readonly List<Version> supportedVersionsInAscendingOrder = new List<Version>
		{
			QueueLogSchema.E15Version
		};

		// Token: 0x040000D4 RID: 212
		private static readonly CsvTable schema = new CsvTable(new CsvField[]
		{
			new CsvField("Timestamp", typeof(string), QueueLogSchema.E15Version),
			new CsvField("SequenceNumber", typeof(string), QueueLogSchema.E15Version),
			new CsvField("EventId", typeof(string), QueueLogSchema.E15Version),
			new CsvField("QueueIdentity", typeof(string), QueueLogSchema.E15Version),
			new CsvField("QueueStatus", typeof(string), QueueLogSchema.E15Version),
			new CsvField("DeliveryType", typeof(string), QueueLogSchema.E15Version),
			new CsvField("NextHopDomain", typeof(string), QueueLogSchema.E15Version),
			new CsvField("NextHopKey", typeof(string), QueueLogSchema.E15Version),
			new CsvField("ActiveMessageCount", typeof(string), QueueLogSchema.E15Version),
			new CsvField("DeferredMessageCount", typeof(string), QueueLogSchema.E15Version),
			new CsvField("LockedMessageCount", typeof(string), QueueLogSchema.E15Version),
			new CsvField("IncomingRate", typeof(string), QueueLogSchema.E15Version),
			new CsvField("OutgoingRate", typeof(string), QueueLogSchema.E15Version),
			new CsvField("Velocity", typeof(string), QueueLogSchema.E15Version),
			new CsvField("NextHopCategory", typeof(string), QueueLogSchema.E15Version),
			new CsvField("RiskLevel", typeof(string), QueueLogSchema.E15Version),
			new CsvField("OutboundIPPool", typeof(string), QueueLogSchema.E15Version),
			new CsvField("NextHopConnector", typeof(string), QueueLogSchema.E15Version),
			new CsvField("TlsDomain", typeof(string), QueueLogSchema.E15Version),
			new CsvField("LastError", typeof(string), QueueLogSchema.E15Version),
			new CsvField("Data", typeof(string), QueueLogSchema.E15Version),
			new CsvField("CustomData", typeof(KeyValuePair<string, object>[]), QueueLogSchema.E15Version)
		});
	}
}
