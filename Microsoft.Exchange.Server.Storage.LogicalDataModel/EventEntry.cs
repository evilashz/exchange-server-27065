using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200003E RID: 62
	public class EventEntry
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x00038A64 File Offset: 0x00036C64
		internal EventEntry(long eventCounter, DateTime createTime) : this(eventCounter, createTime, 0, (EventType)0, null, new Guid?(Guid.Empty), new Guid?(Guid.Empty), null, null, null, null, null, null, null, null, null, EventFlags.None, null, (ClientType)0, null, null, TenantHint.Empty, null)
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00038AD8 File Offset: 0x00036CD8
		public EventEntry(long eventCounter, DateTime createTime, int transactionId, EventType eventType, int? mailboxNumber, Guid? mailboxGuid, Guid? mapiEntryIdGuid, string objectClass, byte[] fid24, byte[] mid24, byte[] parentFid24, byte[] oldFid24, byte[] oldMid24, byte[] oldParentFid24, int? itemCount, int? unreadCount, EventFlags flags, ExtendedEventFlags? extendedFlags, ClientType clientType, byte[] sid, int? documentId, TenantHint tenantHint, Guid? unifiedMailboxGuid)
		{
			this.eventCounter = eventCounter;
			this.createTime = createTime;
			this.transactionId = transactionId;
			this.eventType = eventType;
			this.mailboxNumber = mailboxNumber;
			this.mailboxGuid = mailboxGuid;
			this.mapiEntryIdGuid = mapiEntryIdGuid;
			this.objectClass = objectClass;
			this.fid24 = fid24;
			this.mid24 = mid24;
			this.parentFid24 = parentFid24;
			this.oldFid24 = oldFid24;
			this.oldMid24 = oldMid24;
			this.oldParentFid24 = oldParentFid24;
			this.itemCount = itemCount;
			this.unreadCount = unreadCount;
			this.flags = flags;
			this.extendedFlags = extendedFlags;
			this.clientType = clientType;
			this.sid = sid;
			this.documentId = documentId;
			this.tenantHint = tenantHint;
			this.unifiedMailboxGuid = unifiedMailboxGuid;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00038BA0 File Offset: 0x00036DA0
		public long EventCounter
		{
			get
			{
				return this.eventCounter;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00038BA8 File Offset: 0x00036DA8
		public DateTime CreateTime
		{
			get
			{
				return this.createTime;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00038BB0 File Offset: 0x00036DB0
		public int TransactionId
		{
			get
			{
				return this.transactionId;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00038BB8 File Offset: 0x00036DB8
		public EventType EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00038BC0 File Offset: 0x00036DC0
		public int? MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00038BC8 File Offset: 0x00036DC8
		public Guid? MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00038BD0 File Offset: 0x00036DD0
		public Guid? MapiEntryIdGuid
		{
			get
			{
				return this.mapiEntryIdGuid;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00038BD8 File Offset: 0x00036DD8
		public string ObjectClass
		{
			get
			{
				return this.objectClass;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00038BE0 File Offset: 0x00036DE0
		public byte[] Fid24
		{
			get
			{
				return this.fid24;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00038BE8 File Offset: 0x00036DE8
		public byte[] Mid24
		{
			get
			{
				return this.mid24;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00038BF0 File Offset: 0x00036DF0
		public byte[] ParentFid24
		{
			get
			{
				return this.parentFid24;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00038BF8 File Offset: 0x00036DF8
		public byte[] OldFid24
		{
			get
			{
				return this.oldFid24;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00038C00 File Offset: 0x00036E00
		public byte[] OldMid24
		{
			get
			{
				return this.oldMid24;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00038C08 File Offset: 0x00036E08
		public byte[] OldParentFid24
		{
			get
			{
				return this.oldParentFid24;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00038C10 File Offset: 0x00036E10
		public int? ItemCount
		{
			get
			{
				return this.itemCount;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00038C18 File Offset: 0x00036E18
		public int? UnreadCount
		{
			get
			{
				return this.unreadCount;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00038C20 File Offset: 0x00036E20
		public EventFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00038C28 File Offset: 0x00036E28
		public ExtendedEventFlags? ExtendedFlags
		{
			get
			{
				return this.extendedFlags;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00038C30 File Offset: 0x00036E30
		public ClientType ClientType
		{
			get
			{
				return this.clientType;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00038C38 File Offset: 0x00036E38
		public byte[] Sid
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00038C40 File Offset: 0x00036E40
		public int? DocumentId
		{
			get
			{
				return this.documentId;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00038C48 File Offset: 0x00036E48
		public TenantHint TenantHint
		{
			get
			{
				return this.tenantHint;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00038C50 File Offset: 0x00036E50
		public Guid? UnifiedMailboxGuid
		{
			get
			{
				return this.unifiedMailboxGuid;
			}
		}

		// Token: 0x04000335 RID: 821
		private long eventCounter;

		// Token: 0x04000336 RID: 822
		private DateTime createTime;

		// Token: 0x04000337 RID: 823
		private int transactionId;

		// Token: 0x04000338 RID: 824
		private EventType eventType;

		// Token: 0x04000339 RID: 825
		private int? mailboxNumber;

		// Token: 0x0400033A RID: 826
		private Guid? mailboxGuid;

		// Token: 0x0400033B RID: 827
		private Guid? mapiEntryIdGuid;

		// Token: 0x0400033C RID: 828
		private string objectClass;

		// Token: 0x0400033D RID: 829
		private byte[] fid24;

		// Token: 0x0400033E RID: 830
		private byte[] mid24;

		// Token: 0x0400033F RID: 831
		private byte[] parentFid24;

		// Token: 0x04000340 RID: 832
		private byte[] oldFid24;

		// Token: 0x04000341 RID: 833
		private byte[] oldMid24;

		// Token: 0x04000342 RID: 834
		private byte[] oldParentFid24;

		// Token: 0x04000343 RID: 835
		private int? itemCount;

		// Token: 0x04000344 RID: 836
		private int? unreadCount;

		// Token: 0x04000345 RID: 837
		private EventFlags flags;

		// Token: 0x04000346 RID: 838
		private ExtendedEventFlags? extendedFlags;

		// Token: 0x04000347 RID: 839
		private ClientType clientType;

		// Token: 0x04000348 RID: 840
		private byte[] sid;

		// Token: 0x04000349 RID: 841
		private int? documentId;

		// Token: 0x0400034A RID: 842
		private TenantHint tenantHint;

		// Token: 0x0400034B RID: 843
		private Guid? unifiedMailboxGuid;
	}
}
