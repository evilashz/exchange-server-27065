using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.TransportLogSearchTasks
{
	// Token: 0x02000047 RID: 71
	[Serializable]
	public class MessageTrackingEvent
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A088 File Offset: 0x00008288
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000A090 File Offset: 0x00008290
		public DateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
			internal set
			{
				this.timestamp = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000A099 File Offset: 0x00008299
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000A0A1 File Offset: 0x000082A1
		public string ClientIp
		{
			get
			{
				return this.clientIp;
			}
			internal set
			{
				this.clientIp = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000A0AA File Offset: 0x000082AA
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000A0B2 File Offset: 0x000082B2
		public string ClientHostname
		{
			get
			{
				return this.clientHostname;
			}
			internal set
			{
				this.clientHostname = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A0BB File Offset: 0x000082BB
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000A0C3 File Offset: 0x000082C3
		public string ServerIp
		{
			get
			{
				return this.serverIp;
			}
			internal set
			{
				this.serverIp = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000A0CC File Offset: 0x000082CC
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000A0D4 File Offset: 0x000082D4
		public string ServerHostname
		{
			get
			{
				return this.serverHostname;
			}
			internal set
			{
				this.serverHostname = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000A0DD File Offset: 0x000082DD
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000A0E5 File Offset: 0x000082E5
		public string SourceContext
		{
			get
			{
				return this.sourceContext;
			}
			internal set
			{
				this.sourceContext = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A0EE File Offset: 0x000082EE
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000A0F6 File Offset: 0x000082F6
		public string ConnectorId
		{
			get
			{
				return this.connectorId;
			}
			internal set
			{
				this.connectorId = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A0FF File Offset: 0x000082FF
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000A107 File Offset: 0x00008307
		public string Source
		{
			get
			{
				return this.source;
			}
			internal set
			{
				this.source = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A110 File Offset: 0x00008310
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000A118 File Offset: 0x00008318
		public string EventId
		{
			get
			{
				return this.eventId;
			}
			internal set
			{
				this.eventId = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000A121 File Offset: 0x00008321
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000A129 File Offset: 0x00008329
		public string InternalMessageId
		{
			get
			{
				return this.internalMessageId;
			}
			internal set
			{
				this.internalMessageId = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A132 File Offset: 0x00008332
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000A13A File Offset: 0x0000833A
		public string MessageId
		{
			get
			{
				return this.messageId;
			}
			internal set
			{
				this.messageId = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A143 File Offset: 0x00008343
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000A14B File Offset: 0x0000834B
		public string[] Recipients
		{
			get
			{
				return this.recipients;
			}
			internal set
			{
				this.recipients = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000A154 File Offset: 0x00008354
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000A15C File Offset: 0x0000835C
		public string[] RecipientStatus
		{
			get
			{
				return this.recipientStatus;
			}
			internal set
			{
				this.recipientStatus = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A165 File Offset: 0x00008365
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000A16D File Offset: 0x0000836D
		public int? TotalBytes
		{
			get
			{
				return this.totalBytes;
			}
			internal set
			{
				this.totalBytes = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000A176 File Offset: 0x00008376
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000A17E File Offset: 0x0000837E
		public int? RecipientCount
		{
			get
			{
				return this.recipientCount;
			}
			internal set
			{
				this.recipientCount = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000A187 File Offset: 0x00008387
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000A18F File Offset: 0x0000838F
		public string RelatedRecipientAddress
		{
			get
			{
				return this.relatedRecipientAddress;
			}
			internal set
			{
				this.relatedRecipientAddress = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000A198 File Offset: 0x00008398
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public string[] Reference
		{
			get
			{
				return this.reference;
			}
			internal set
			{
				this.reference = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A1A9 File Offset: 0x000083A9
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000A1B1 File Offset: 0x000083B1
		public string MessageSubject
		{
			get
			{
				return this.messageSubject;
			}
			internal set
			{
				this.messageSubject = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A1BA File Offset: 0x000083BA
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000A1C2 File Offset: 0x000083C2
		public string Sender
		{
			get
			{
				return this.sender;
			}
			internal set
			{
				this.sender = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000A1CB File Offset: 0x000083CB
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000A1D3 File Offset: 0x000083D3
		public string ReturnPath
		{
			get
			{
				return this.returnPath;
			}
			internal set
			{
				this.returnPath = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000A1DC File Offset: 0x000083DC
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000A1E4 File Offset: 0x000083E4
		public string Directionality
		{
			get
			{
				return this.directionality;
			}
			internal set
			{
				this.directionality = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A1ED File Offset: 0x000083ED
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000A1F5 File Offset: 0x000083F5
		public string TenantId
		{
			get
			{
				return this.tenantId;
			}
			internal set
			{
				this.tenantId = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000A1FE File Offset: 0x000083FE
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000A206 File Offset: 0x00008406
		public string OriginalClientIp
		{
			get
			{
				return this.originalClientIp;
			}
			internal set
			{
				this.originalClientIp = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000A20F File Offset: 0x0000840F
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000A222 File Offset: 0x00008422
		public string MessageInfo
		{
			get
			{
				return MessageTrackingEvent.FormatMessageInfo(this.messageInfo, this.eventId);
			}
			internal set
			{
				this.messageInfo = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000A22B File Offset: 0x0000842B
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000A233 File Offset: 0x00008433
		public EnhancedTimeSpan? MessageLatency
		{
			get
			{
				return this.messageLatency;
			}
			internal set
			{
				this.messageLatency = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000A23C File Offset: 0x0000843C
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000A244 File Offset: 0x00008444
		public MessageLatencyType MessageLatencyType
		{
			get
			{
				return this.messageLatencyType;
			}
			internal set
			{
				this.messageLatencyType = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000A24D File Offset: 0x0000844D
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000A255 File Offset: 0x00008455
		public KeyValuePair<string, object>[] EventData
		{
			get
			{
				return this.eventData;
			}
			internal set
			{
				this.eventData = value;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A260 File Offset: 0x00008460
		private static string FormatMessageInfo(string messageInfo, string eventId)
		{
			if (eventId == "SEND" || eventId == "DELIVER" || eventId == "DEFER")
			{
				DateTime dateTime;
				bool flag = DateTime.TryParseExact(messageInfo, "yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AssumeUniversal, out dateTime);
				if (flag)
				{
					return dateTime.ToLocalTime().ToString();
				}
			}
			return messageInfo;
		}

		// Token: 0x040000CD RID: 205
		private DateTime timestamp;

		// Token: 0x040000CE RID: 206
		private string clientIp;

		// Token: 0x040000CF RID: 207
		private string clientHostname;

		// Token: 0x040000D0 RID: 208
		private string serverIp;

		// Token: 0x040000D1 RID: 209
		private string serverHostname;

		// Token: 0x040000D2 RID: 210
		private string sourceContext;

		// Token: 0x040000D3 RID: 211
		private string connectorId;

		// Token: 0x040000D4 RID: 212
		private string source;

		// Token: 0x040000D5 RID: 213
		private string eventId;

		// Token: 0x040000D6 RID: 214
		private string internalMessageId;

		// Token: 0x040000D7 RID: 215
		private string messageId;

		// Token: 0x040000D8 RID: 216
		private string[] recipients;

		// Token: 0x040000D9 RID: 217
		private string[] recipientStatus;

		// Token: 0x040000DA RID: 218
		private int? totalBytes;

		// Token: 0x040000DB RID: 219
		private int? recipientCount;

		// Token: 0x040000DC RID: 220
		private string relatedRecipientAddress;

		// Token: 0x040000DD RID: 221
		private string[] reference;

		// Token: 0x040000DE RID: 222
		private string messageSubject;

		// Token: 0x040000DF RID: 223
		private string sender;

		// Token: 0x040000E0 RID: 224
		private string returnPath;

		// Token: 0x040000E1 RID: 225
		private string directionality;

		// Token: 0x040000E2 RID: 226
		private string tenantId;

		// Token: 0x040000E3 RID: 227
		private string originalClientIp;

		// Token: 0x040000E4 RID: 228
		private string messageInfo;

		// Token: 0x040000E5 RID: 229
		private EnhancedTimeSpan? messageLatency;

		// Token: 0x040000E6 RID: 230
		private MessageLatencyType messageLatencyType;

		// Token: 0x040000E7 RID: 231
		private KeyValuePair<string, object>[] eventData;
	}
}
