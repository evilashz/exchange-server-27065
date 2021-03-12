using System;
using System.Net;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000277 RID: 631
	[Serializable]
	public class MessageInfo : PagedDataObject, IConfigurable
	{
		// Token: 0x06001508 RID: 5384 RVA: 0x000432F3 File Offset: 0x000414F3
		internal MessageInfo(long identity, QueueIdentity queueIdentity)
		{
			this.identity = new MessageIdentity(identity, queueIdentity);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00043308 File Offset: 0x00041508
		public MessageInfo(ExtensibleMessageInfo messageInfo)
		{
			this.identity = messageInfo.MessageIdentity;
			this.subject = messageInfo.Subject;
			this.internetMessageId = messageInfo.InternetMessageId;
			this.fromAddress = messageInfo.FromAddress;
			this.status = messageInfo.Status;
			this.size = messageInfo.Size;
			this.messageSourceName = messageInfo.MessageSourceName;
			this.sourceIP = messageInfo.SourceIP;
			this.scl = messageInfo.SCL;
			this.dateReceived = messageInfo.DateReceived;
			this.expirationTime = messageInfo.ExpirationTime;
			this.lastError = messageInfo.LastError;
			this.lastErrorCode = messageInfo.LastErrorCode;
			this.retryCount = messageInfo.RetryCount;
			this.recipients = messageInfo.Recipients;
			this.componentLatency = messageInfo.ComponentLatency;
			this.messageLatency = messageInfo.MessageLatency;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000433E8 File Offset: 0x000415E8
		public void Reset(long identity, QueueIdentity queueIdentity)
		{
			this.identity = new MessageIdentity(identity, queueIdentity);
			this.subject = null;
			this.internetMessageId = null;
			this.fromAddress = null;
			this.status = MessageStatus.None;
			this.size = default(ByteQuantifiedSize);
			this.messageSourceName = null;
			this.sourceIP = null;
			this.scl = 0;
			this.dateReceived = default(DateTime);
			this.expirationTime = null;
			this.lastError = null;
			this.lastErrorCode = 0;
			this.retryCount = 0;
			this.recipients = null;
			this.componentLatency = null;
			this.messageLatency = default(EnhancedTimeSpan);
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x00043486 File Offset: 0x00041686
		public ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0004348E File Offset: 0x0004168E
		// (set) Token: 0x0600150D RID: 5389 RVA: 0x00043496 File Offset: 0x00041696
		public string Subject
		{
			get
			{
				return this.subject;
			}
			internal set
			{
				this.subject = value;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x0004349F File Offset: 0x0004169F
		// (set) Token: 0x0600150F RID: 5391 RVA: 0x000434A7 File Offset: 0x000416A7
		public string InternetMessageId
		{
			get
			{
				return this.internetMessageId;
			}
			internal set
			{
				this.internetMessageId = value;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x000434B0 File Offset: 0x000416B0
		// (set) Token: 0x06001511 RID: 5393 RVA: 0x000434B8 File Offset: 0x000416B8
		public string FromAddress
		{
			get
			{
				return this.fromAddress;
			}
			internal set
			{
				this.fromAddress = value;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x000434C1 File Offset: 0x000416C1
		// (set) Token: 0x06001513 RID: 5395 RVA: 0x000434C9 File Offset: 0x000416C9
		public MessageStatus Status
		{
			get
			{
				return this.status;
			}
			internal set
			{
				this.status = value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x000434D2 File Offset: 0x000416D2
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x000434DA File Offset: 0x000416DA
		public ByteQuantifiedSize Size
		{
			get
			{
				return this.size;
			}
			internal set
			{
				this.size = value;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x000434E3 File Offset: 0x000416E3
		// (set) Token: 0x06001517 RID: 5399 RVA: 0x000434EB File Offset: 0x000416EB
		public string MessageSourceName
		{
			get
			{
				return this.messageSourceName;
			}
			internal set
			{
				this.messageSourceName = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x000434F4 File Offset: 0x000416F4
		// (set) Token: 0x06001519 RID: 5401 RVA: 0x000434FC File Offset: 0x000416FC
		public IPAddress SourceIP
		{
			get
			{
				return this.sourceIP;
			}
			internal set
			{
				this.sourceIP = value;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00043505 File Offset: 0x00041705
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x0004350D File Offset: 0x0004170D
		public int SCL
		{
			get
			{
				return this.scl;
			}
			internal set
			{
				this.scl = value;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x00043516 File Offset: 0x00041716
		// (set) Token: 0x0600151D RID: 5405 RVA: 0x0004351E File Offset: 0x0004171E
		public DateTime DateReceived
		{
			get
			{
				return this.dateReceived;
			}
			internal set
			{
				this.dateReceived = value;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x00043527 File Offset: 0x00041727
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x0004352F File Offset: 0x0004172F
		public DateTime? ExpirationTime
		{
			get
			{
				return this.expirationTime;
			}
			internal set
			{
				this.expirationTime = value;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x00043538 File Offset: 0x00041738
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x00043540 File Offset: 0x00041740
		internal int LastErrorCode
		{
			get
			{
				return this.lastErrorCode;
			}
			set
			{
				this.lastErrorCode = value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0004354C File Offset: 0x0004174C
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x000435A7 File Offset: 0x000417A7
		public string LastError
		{
			get
			{
				if (this.lastError != null)
				{
					return this.lastError;
				}
				if (this.identity.QueueIdentity.Type == QueueType.Unreachable)
				{
					return StatusCodeConverter.UnreachableReasonToString((UnreachableReason)this.lastErrorCode);
				}
				if (this.identity.QueueIdentity.Type == QueueType.Submission)
				{
					return StatusCodeConverter.DeferReasonToString((DeferReason)this.lastErrorCode);
				}
				return null;
			}
			internal set
			{
				this.lastError = value;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x000435B0 File Offset: 0x000417B0
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x000435B8 File Offset: 0x000417B8
		public int RetryCount
		{
			get
			{
				return this.retryCount;
			}
			internal set
			{
				this.retryCount = value;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x000435C1 File Offset: 0x000417C1
		public QueueIdentity Queue
		{
			get
			{
				return this.identity.QueueIdentity;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x000435CE File Offset: 0x000417CE
		// (set) Token: 0x06001528 RID: 5416 RVA: 0x000435D6 File Offset: 0x000417D6
		public RecipientInfo[] Recipients
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

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x000435DF File Offset: 0x000417DF
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x000435E7 File Offset: 0x000417E7
		public ComponentLatencyInfo[] ComponentLatency
		{
			get
			{
				return this.componentLatency;
			}
			internal set
			{
				this.componentLatency = value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x000435F0 File Offset: 0x000417F0
		// (set) Token: 0x0600152C RID: 5420 RVA: 0x000435F8 File Offset: 0x000417F8
		public EnhancedTimeSpan MessageLatency
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

		// Token: 0x0600152D RID: 5421 RVA: 0x00043601 File Offset: 0x00041801
		public void ConvertDatesToLocalTime()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00043608 File Offset: 0x00041808
		public void ConvertDatesToUniversalTime()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0004360F File Offset: 0x0004180F
		public ValidationError[] Validate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00043616 File Offset: 0x00041816
		public bool IsValid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0004361D File Offset: 0x0004181D
		public ObjectState ObjectState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00043624 File Offset: 0x00041824
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0004362B File Offset: 0x0004182B
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000C44 RID: 3140
		private MessageIdentity identity;

		// Token: 0x04000C45 RID: 3141
		private string subject;

		// Token: 0x04000C46 RID: 3142
		private string internetMessageId;

		// Token: 0x04000C47 RID: 3143
		private string fromAddress;

		// Token: 0x04000C48 RID: 3144
		private MessageStatus status;

		// Token: 0x04000C49 RID: 3145
		private ByteQuantifiedSize size;

		// Token: 0x04000C4A RID: 3146
		private string messageSourceName;

		// Token: 0x04000C4B RID: 3147
		private IPAddress sourceIP;

		// Token: 0x04000C4C RID: 3148
		private int scl;

		// Token: 0x04000C4D RID: 3149
		private DateTime dateReceived;

		// Token: 0x04000C4E RID: 3150
		private DateTime? expirationTime;

		// Token: 0x04000C4F RID: 3151
		private string lastError;

		// Token: 0x04000C50 RID: 3152
		private int lastErrorCode;

		// Token: 0x04000C51 RID: 3153
		private int retryCount;

		// Token: 0x04000C52 RID: 3154
		private RecipientInfo[] recipients;

		// Token: 0x04000C53 RID: 3155
		[OptionalField(VersionAdded = 2)]
		private ComponentLatencyInfo[] componentLatency;

		// Token: 0x04000C54 RID: 3156
		[OptionalField(VersionAdded = 2)]
		private EnhancedTimeSpan messageLatency;
	}
}
