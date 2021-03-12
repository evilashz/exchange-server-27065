using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x0200006B RID: 107
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncMail : DeltaSyncObject
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x00017374 File Offset: 0x00015574
		internal DeltaSyncMail(Guid serverId) : base(serverId)
		{
			this.Initialize();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00017383 File Offset: 0x00015583
		internal DeltaSyncMail(string clientId) : base(clientId)
		{
			this.Initialize();
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00017392 File Offset: 0x00015592
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0001739A File Offset: 0x0001559A
		internal string From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000173A3 File Offset: 0x000155A3
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x000173AB File Offset: 0x000155AB
		internal DeltaSyncMail.ImportanceLevel Importance
		{
			get
			{
				return this.importance;
			}
			set
			{
				this.importance = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x000173B4 File Offset: 0x000155B4
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x000173BC File Offset: 0x000155BC
		internal ExDateTime DateReceived
		{
			get
			{
				return this.dateReceived;
			}
			set
			{
				this.dateReceived = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000173C8 File Offset: 0x000155C8
		internal string DateReceivedUniversalTimeString
		{
			get
			{
				return this.dateReceived.UniversalTime.ToString(DeltaSyncCommon.DateTimeFormatString);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x000173ED File Offset: 0x000155ED
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x000173F5 File Offset: 0x000155F5
		internal string MessageClass
		{
			get
			{
				return this.messageClass;
			}
			set
			{
				this.messageClass = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x000173FE File Offset: 0x000155FE
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00017406 File Offset: 0x00015606
		internal string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001740F File Offset: 0x0001560F
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x00017417 File Offset: 0x00015617
		internal bool Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00017420 File Offset: 0x00015620
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x00017428 File Offset: 0x00015628
		internal string ConversationTopic
		{
			get
			{
				return this.conversationTopic;
			}
			set
			{
				this.conversationTopic = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00017431 File Offset: 0x00015631
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00017439 File Offset: 0x00015639
		internal string ConversationIndex
		{
			get
			{
				return this.conversationIndex;
			}
			set
			{
				this.conversationIndex = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00017442 File Offset: 0x00015642
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x0001744A File Offset: 0x0001564A
		internal DeltaSyncMail.SensitivityLevel Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
			set
			{
				this.sensitivity = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00017453 File Offset: 0x00015653
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x0001745B File Offset: 0x0001565B
		internal int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00017464 File Offset: 0x00015664
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0001746C File Offset: 0x0001566C
		internal bool HasAttachments
		{
			get
			{
				return this.hasAttachments;
			}
			set
			{
				this.hasAttachments = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00017475 File Offset: 0x00015675
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0001747D File Offset: 0x0001567D
		internal Stream EmailMessage
		{
			get
			{
				return this.emailMessage;
			}
			set
			{
				this.emailMessage = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00017486 File Offset: 0x00015686
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x0001748E File Offset: 0x0001568E
		internal DeltaSyncMail.ReplyToOrForwardState? ReplyToOrForward
		{
			get
			{
				return this.replyToOrForward;
			}
			set
			{
				this.replyToOrForward = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00017497 File Offset: 0x00015697
		internal bool IsDraft
		{
			get
			{
				return this.messageClass != null && this.messageClass.Equals(DeltaSyncCommon.DraftMessageClass, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000174B4 File Offset: 0x000156B4
		internal static bool IsSupportedMessageClass(string messageClass)
		{
			if (messageClass == null)
			{
				return false;
			}
			foreach (string value in DeltaSyncCommon.SupportedMessageClasses)
			{
				if (messageClass.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000174EF File Offset: 0x000156EF
		internal string MessageIncludeContentId
		{
			get
			{
				return this.messageIncludeContentId;
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000174F8 File Offset: 0x000156F8
		private void Initialize()
		{
			this.importance = DeltaSyncMail.ImportanceLevel.Normal;
			this.dateReceived = ExDateTime.UtcNow;
			this.messageClass = DeltaSyncCommon.NormalMessageClass;
			this.read = false;
			this.sensitivity = DeltaSyncMail.SensitivityLevel.Normal;
			this.hasAttachments = false;
			this.replyToOrForward = null;
			this.messageIncludeContentId = Guid.NewGuid().ToString();
		}

		// Token: 0x040002A5 RID: 677
		private string from;

		// Token: 0x040002A6 RID: 678
		private DeltaSyncMail.ImportanceLevel importance;

		// Token: 0x040002A7 RID: 679
		private ExDateTime dateReceived;

		// Token: 0x040002A8 RID: 680
		private string messageClass;

		// Token: 0x040002A9 RID: 681
		private string subject;

		// Token: 0x040002AA RID: 682
		private bool read;

		// Token: 0x040002AB RID: 683
		private string conversationTopic;

		// Token: 0x040002AC RID: 684
		private string conversationIndex;

		// Token: 0x040002AD RID: 685
		private DeltaSyncMail.SensitivityLevel sensitivity;

		// Token: 0x040002AE RID: 686
		private int size;

		// Token: 0x040002AF RID: 687
		private bool hasAttachments;

		// Token: 0x040002B0 RID: 688
		private Stream emailMessage;

		// Token: 0x040002B1 RID: 689
		private DeltaSyncMail.ReplyToOrForwardState? replyToOrForward;

		// Token: 0x040002B2 RID: 690
		private string messageIncludeContentId;

		// Token: 0x0200006C RID: 108
		internal enum ImportanceLevel : byte
		{
			// Token: 0x040002B4 RID: 692
			Low,
			// Token: 0x040002B5 RID: 693
			Normal,
			// Token: 0x040002B6 RID: 694
			High
		}

		// Token: 0x0200006D RID: 109
		internal enum ReplyToOrForwardState : byte
		{
			// Token: 0x040002B8 RID: 696
			None,
			// Token: 0x040002B9 RID: 697
			RepliedTo,
			// Token: 0x040002BA RID: 698
			Forwarded
		}

		// Token: 0x0200006E RID: 110
		internal enum SensitivityLevel : byte
		{
			// Token: 0x040002BC RID: 700
			Normal,
			// Token: 0x040002BD RID: 701
			Personal,
			// Token: 0x040002BE RID: 702
			Private,
			// Token: 0x040002BF RID: 703
			Confidential
		}
	}
}
