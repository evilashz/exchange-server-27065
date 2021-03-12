using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000086 RID: 134
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class MapiEvent : IMapiEvent
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0000F61B File Offset: 0x0000D81B
		internal MapiEvent(ref MapiEventNative pEvent) : this(ref pEvent, true)
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000F628 File Offset: 0x0000D828
		internal MapiEvent(ref MapiEventNative pEvent, bool includeSid)
		{
			this.eventCounter = (long)pEvent.llEventCounter;
			this.eventMask = (MapiEventTypeFlags)pEvent.ulMask;
			this.mailboxGuid = pEvent.mailboxGuid;
			this.objectClass = pEvent.rgchObjectClass;
			this.createTime = DateTime.FromFileTimeUtc(pEvent.ftCreate);
			this.itemType = (ObjectType)pEvent.ulItemType;
			this.watermark = null;
			this.mdbWatermark = null;
			this.itemCount = (long)pEvent.lItemCount;
			this.unreadItemCount = (long)pEvent.lUnreadCount;
			this.eventFlags = (MapiEventFlags)pEvent.ulFlags;
			this.extendedEventFlags = (MapiExtendedEventFlags)pEvent.ullExtendedFlags;
			if (pEvent.binEidItem.count > 0)
			{
				this.itemEntryId = new byte[pEvent.binEidItem.count];
				Marshal.Copy(pEvent.binEidItem.intPtr, this.itemEntryId, 0, pEvent.binEidItem.count);
			}
			if (pEvent.binEidParent.count > 0)
			{
				this.parentEntryId = new byte[pEvent.binEidParent.count];
				Marshal.Copy(pEvent.binEidParent.intPtr, this.parentEntryId, 0, pEvent.binEidParent.count);
			}
			if (pEvent.binEidOldItem.count > 0)
			{
				this.oldItemEntryId = new byte[pEvent.binEidOldItem.count];
				Marshal.Copy(pEvent.binEidOldItem.intPtr, this.oldItemEntryId, 0, pEvent.binEidOldItem.count);
			}
			if (pEvent.binEidOldParent.count > 0)
			{
				this.oldParentEntryId = new byte[pEvent.binEidOldParent.count];
				Marshal.Copy(pEvent.binEidOldParent.intPtr, this.oldParentEntryId, 0, pEvent.binEidOldParent.count);
			}
			this.clientType = (MapiEventClientTypes)pEvent.ulClientType;
			if (includeSid && pEvent.binSid.count > 0)
			{
				byte[] array = new byte[pEvent.binSid.count];
				Marshal.Copy(pEvent.binSid.intPtr, array, 0, pEvent.binSid.count);
				this.sid = new SecurityIdentifier(array, 0);
			}
			this.docId = (int)pEvent.ulDocId;
			this.mailboxNumber = (int)pEvent.ulMailboxNumber;
			if (pEvent.binTenantHintBob.count > 0)
			{
				this.tenantHint = new byte[pEvent.binTenantHintBob.count];
				Marshal.Copy(pEvent.binTenantHintBob.intPtr, this.tenantHint, 0, pEvent.binTenantHintBob.count);
			}
			this.unifiedMailboxGuid = pEvent.unifiedMailboxGuid;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000F8A2 File Offset: 0x0000DAA2
		public long EventCounter
		{
			get
			{
				return this.eventCounter;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000F8AA File Offset: 0x0000DAAA
		public Watermark Watermark
		{
			get
			{
				if (this.watermark == null)
				{
					this.watermark = new Watermark(this.mailboxGuid, this.eventCounter);
				}
				return this.watermark;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000F8D1 File Offset: 0x0000DAD1
		public Watermark DatabaseWatermark
		{
			get
			{
				if (this.mdbWatermark == null)
				{
					this.mdbWatermark = new Watermark(Guid.Empty, this.eventCounter);
				}
				return this.mdbWatermark;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000F8F7 File Offset: 0x0000DAF7
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return this.eventMask;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000F8FF File Offset: 0x0000DAFF
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000F907 File Offset: 0x0000DB07
		public string ObjectClass
		{
			get
			{
				return this.objectClass;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000F90F File Offset: 0x0000DB0F
		public DateTime CreateTime
		{
			get
			{
				return this.createTime;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000F917 File Offset: 0x0000DB17
		public ObjectType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000F91F File Offset: 0x0000DB1F
		public byte[] ItemEntryId
		{
			get
			{
				return this.itemEntryId;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000F927 File Offset: 0x0000DB27
		public byte[] ParentEntryId
		{
			get
			{
				return this.parentEntryId;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000F92F File Offset: 0x0000DB2F
		public byte[] OldItemEntryId
		{
			get
			{
				return this.oldItemEntryId;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000F937 File Offset: 0x0000DB37
		public byte[] OldParentEntryId
		{
			get
			{
				return this.oldParentEntryId;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000F93F File Offset: 0x0000DB3F
		public string ItemEntryIdString
		{
			get
			{
				return this.EntryIdString(this.itemEntryId);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000F94D File Offset: 0x0000DB4D
		public string ParentEntryIdString
		{
			get
			{
				return this.EntryIdString(this.parentEntryId);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000F95B File Offset: 0x0000DB5B
		public string OldItemEntryIdString
		{
			get
			{
				return this.EntryIdString(this.oldItemEntryId);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000F969 File Offset: 0x0000DB69
		public string OldParentEntryIdString
		{
			get
			{
				return this.EntryIdString(this.oldParentEntryId);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000F977 File Offset: 0x0000DB77
		public long ItemCount
		{
			get
			{
				return this.itemCount;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000F97F File Offset: 0x0000DB7F
		public long UnreadItemCount
		{
			get
			{
				return this.unreadItemCount;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000F987 File Offset: 0x0000DB87
		public MapiEventFlags EventFlags
		{
			get
			{
				return this.eventFlags;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000F98F File Offset: 0x0000DB8F
		public MapiExtendedEventFlags ExtendedEventFlags
		{
			get
			{
				return this.extendedEventFlags;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000F997 File Offset: 0x0000DB97
		public MapiEventClientTypes ClientType
		{
			get
			{
				return this.clientType;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000F99F File Offset: 0x0000DB9F
		public SecurityIdentifier Sid
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000F9A7 File Offset: 0x0000DBA7
		public int DocumentId
		{
			get
			{
				return this.docId;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000F9AF File Offset: 0x0000DBAF
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000F9B7 File Offset: 0x0000DBB7
		public byte[] TenantHint
		{
			get
			{
				return this.tenantHint;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000F9BF File Offset: 0x0000DBBF
		public Guid UnifiedMailboxGuid
		{
			get
			{
				return this.unifiedMailboxGuid;
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
		public override string ToString()
		{
			string text = string.Format("Counter: 0x{0,0:X}; MailboxID: {1}; Mask: {2}; Flags: {10}; ExtendedFlags: {11};\nObject Class: {3}; Created Time: {4};\nItem Type: {5}\nItem EntryId: {6}\nParent entryId: {7}\nOld Item entryId: {8}\nOld parent entryId: {9}\nSID: {12}\nClient Type: {13}\nDocument ID: {14}\n", new object[]
			{
				this.eventCounter,
				this.mailboxGuid,
				this.eventMask.ToString(),
				this.objectClass,
				this.createTime,
				this.ItemType,
				this.ItemEntryIdString,
				this.ParentEntryIdString,
				this.OldItemEntryIdString,
				this.OldParentEntryIdString,
				this.eventFlags,
				this.extendedEventFlags,
				(null != this.sid) ? this.sid.ToString() : "<null>",
				this.clientType,
				this.docId
			});
			if (ObjectType.MAPI_FOLDER == this.itemType)
			{
				text += string.Format("Item Count: {0}\nUnread Item Count: {1}\n", this.itemCount, this.unreadItemCount);
			}
			return text;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
		private string EntryIdString(byte[] entryId)
		{
			string result = null;
			if (entryId != null)
			{
				StringBuilder stringBuilder = new StringBuilder(2 * entryId.Length);
				foreach (byte b in entryId)
				{
					stringBuilder.AppendFormat("{0:X} ", b);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x040004FE RID: 1278
		private Watermark watermark;

		// Token: 0x040004FF RID: 1279
		private Watermark mdbWatermark;

		// Token: 0x04000500 RID: 1280
		private readonly long eventCounter;

		// Token: 0x04000501 RID: 1281
		private readonly MapiEventTypeFlags eventMask;

		// Token: 0x04000502 RID: 1282
		private readonly Guid mailboxGuid;

		// Token: 0x04000503 RID: 1283
		private readonly string objectClass;

		// Token: 0x04000504 RID: 1284
		private readonly DateTime createTime;

		// Token: 0x04000505 RID: 1285
		private readonly ObjectType itemType;

		// Token: 0x04000506 RID: 1286
		private readonly byte[] itemEntryId;

		// Token: 0x04000507 RID: 1287
		private readonly byte[] parentEntryId;

		// Token: 0x04000508 RID: 1288
		private readonly byte[] oldItemEntryId;

		// Token: 0x04000509 RID: 1289
		private readonly byte[] oldParentEntryId;

		// Token: 0x0400050A RID: 1290
		private readonly long itemCount;

		// Token: 0x0400050B RID: 1291
		private readonly long unreadItemCount;

		// Token: 0x0400050C RID: 1292
		private readonly MapiEventFlags eventFlags;

		// Token: 0x0400050D RID: 1293
		private readonly MapiExtendedEventFlags extendedEventFlags;

		// Token: 0x0400050E RID: 1294
		private readonly MapiEventClientTypes clientType;

		// Token: 0x0400050F RID: 1295
		[NonSerialized]
		private readonly SecurityIdentifier sid;

		// Token: 0x04000510 RID: 1296
		private readonly int docId;

		// Token: 0x04000511 RID: 1297
		private readonly int mailboxNumber;

		// Token: 0x04000512 RID: 1298
		private readonly byte[] tenantHint;

		// Token: 0x04000513 RID: 1299
		private readonly Guid unifiedMailboxGuid;
	}
}
