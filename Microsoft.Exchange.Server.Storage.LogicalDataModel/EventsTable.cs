using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000007 RID: 7
	public sealed class EventsTable
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000034D0 File Offset: 0x000016D0
		internal EventsTable()
		{
			this.eventCounter = Factory.CreatePhysicalColumn("EventCounter", "EventCounter", typeof(long), false, true, false, false, false, Visibility.Public, 0, 8, 8);
			this.createTime = Factory.CreatePhysicalColumn("CreateTime", "CreateTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.transactionId = Factory.CreatePhysicalColumn("TransactionId", "TransactionId", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.eventType = Factory.CreatePhysicalColumn("EventType", "EventType", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.clientType = Factory.CreatePhysicalColumn("ClientType", "ClientType", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.flags = Factory.CreatePhysicalColumn("Flags", "Flags", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.objectClass = Factory.CreatePhysicalColumn("ObjectClass", "ObjectClass", typeof(string), true, false, false, false, false, Visibility.Public, 59, 0, 59);
			this.fid = Factory.CreatePhysicalColumn("Fid", "Fid", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 24, 24);
			this.mid = Factory.CreatePhysicalColumn("Mid", "Mid", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 24, 24);
			this.oldFid = Factory.CreatePhysicalColumn("OldFid", "OldFid", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 24, 24);
			this.oldMid = Factory.CreatePhysicalColumn("OldMid", "OldMid", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 24, 24);
			this.oldParentFid = Factory.CreatePhysicalColumn("OldParentFid", "OldParentFid", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 24, 24);
			this.itemCount = Factory.CreatePhysicalColumn("ItemCount", "ItemCount", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.unreadCount = Factory.CreatePhysicalColumn("UnreadCount", "UnreadCount", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.sid = Factory.CreatePhysicalColumn("Sid", "Sid", typeof(byte[]), true, false, false, false, false, Visibility.Public, 68, 0, 68);
			this.documentId = Factory.CreatePhysicalColumn("DocumentId", "DocumentId", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.extendedFlags = Factory.CreatePhysicalColumn("ExtendedFlags", "ExtendedFlags", typeof(long), true, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.parentFid = Factory.CreatePhysicalColumn("ParentFid", "ParentFid", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 24, 24);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "EventsPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[1];
			this.eventsPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.EventCounter
			});
			Index[] indexes = new Index[]
			{
				this.EventsPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.EventCounter,
				this.CreateTime,
				this.TransactionId,
				this.EventType,
				this.MailboxNumber,
				this.ClientType,
				this.Flags,
				this.ObjectClass,
				this.Fid,
				this.Mid,
				this.OldFid,
				this.OldMid,
				this.OldParentFid,
				this.ItemCount,
				this.UnreadCount,
				this.Sid,
				this.DocumentId,
				this.ExtendedFlags,
				this.ParentFid,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("Events", TableClass.Events, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003980 File Offset: 0x00001B80
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003988 File Offset: 0x00001B88
		public PhysicalColumn EventCounter
		{
			get
			{
				return this.eventCounter;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003990 File Offset: 0x00001B90
		public PhysicalColumn CreateTime
		{
			get
			{
				return this.createTime;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003998 File Offset: 0x00001B98
		public PhysicalColumn TransactionId
		{
			get
			{
				return this.transactionId;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000039A0 File Offset: 0x00001BA0
		public PhysicalColumn EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000039A8 File Offset: 0x00001BA8
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000039B0 File Offset: 0x00001BB0
		public PhysicalColumn ClientType
		{
			get
			{
				return this.clientType;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000039B8 File Offset: 0x00001BB8
		public PhysicalColumn Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000039C0 File Offset: 0x00001BC0
		public PhysicalColumn ObjectClass
		{
			get
			{
				return this.objectClass;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000039C8 File Offset: 0x00001BC8
		public PhysicalColumn Fid
		{
			get
			{
				return this.fid;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000039D0 File Offset: 0x00001BD0
		public PhysicalColumn Mid
		{
			get
			{
				return this.mid;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000039D8 File Offset: 0x00001BD8
		public PhysicalColumn OldFid
		{
			get
			{
				return this.oldFid;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000039E0 File Offset: 0x00001BE0
		public PhysicalColumn OldMid
		{
			get
			{
				return this.oldMid;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000039E8 File Offset: 0x00001BE8
		public PhysicalColumn OldParentFid
		{
			get
			{
				return this.oldParentFid;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000039F0 File Offset: 0x00001BF0
		public PhysicalColumn ItemCount
		{
			get
			{
				return this.itemCount;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000039F8 File Offset: 0x00001BF8
		public PhysicalColumn UnreadCount
		{
			get
			{
				return this.unreadCount;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003A00 File Offset: 0x00001C00
		public PhysicalColumn Sid
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003A08 File Offset: 0x00001C08
		public PhysicalColumn DocumentId
		{
			get
			{
				return this.documentId;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003A10 File Offset: 0x00001C10
		public PhysicalColumn ExtendedFlags
		{
			get
			{
				return this.extendedFlags;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003A18 File Offset: 0x00001C18
		public PhysicalColumn ParentFid
		{
			get
			{
				return this.parentFid;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003A20 File Offset: 0x00001C20
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003A28 File Offset: 0x00001C28
		public Index EventsPK
		{
			get
			{
				return this.eventsPK;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003A30 File Offset: 0x00001C30
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.eventCounter;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.eventCounter = null;
			}
			physicalColumn = this.createTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.createTime = null;
			}
			physicalColumn = this.transactionId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.transactionId = null;
			}
			physicalColumn = this.eventType;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.eventType = null;
			}
			physicalColumn = this.mailboxNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxNumber = null;
			}
			physicalColumn = this.clientType;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.clientType = null;
			}
			physicalColumn = this.flags;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.flags = null;
			}
			physicalColumn = this.objectClass;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.objectClass = null;
			}
			physicalColumn = this.fid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.fid = null;
			}
			physicalColumn = this.mid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mid = null;
			}
			physicalColumn = this.oldFid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.oldFid = null;
			}
			physicalColumn = this.oldMid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.oldMid = null;
			}
			physicalColumn = this.oldParentFid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.oldParentFid = null;
			}
			physicalColumn = this.itemCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.itemCount = null;
			}
			physicalColumn = this.unreadCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.unreadCount = null;
			}
			physicalColumn = this.sid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.sid = null;
			}
			physicalColumn = this.documentId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.documentId = null;
			}
			physicalColumn = this.extendedFlags;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extendedFlags = null;
			}
			physicalColumn = this.parentFid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.parentFid = null;
			}
			physicalColumn = this.extensionBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extensionBlob = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.eventsPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.eventsPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x04000031 RID: 49
		public const string EventCounterName = "EventCounter";

		// Token: 0x04000032 RID: 50
		public const string CreateTimeName = "CreateTime";

		// Token: 0x04000033 RID: 51
		public const string TransactionIdName = "TransactionId";

		// Token: 0x04000034 RID: 52
		public const string EventTypeName = "EventType";

		// Token: 0x04000035 RID: 53
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x04000036 RID: 54
		public const string ClientTypeName = "ClientType";

		// Token: 0x04000037 RID: 55
		public const string FlagsName = "Flags";

		// Token: 0x04000038 RID: 56
		public const string ObjectClassName = "ObjectClass";

		// Token: 0x04000039 RID: 57
		public const string FidName = "Fid";

		// Token: 0x0400003A RID: 58
		public const string MidName = "Mid";

		// Token: 0x0400003B RID: 59
		public const string OldFidName = "OldFid";

		// Token: 0x0400003C RID: 60
		public const string OldMidName = "OldMid";

		// Token: 0x0400003D RID: 61
		public const string OldParentFidName = "OldParentFid";

		// Token: 0x0400003E RID: 62
		public const string ItemCountName = "ItemCount";

		// Token: 0x0400003F RID: 63
		public const string UnreadCountName = "UnreadCount";

		// Token: 0x04000040 RID: 64
		public const string SidName = "Sid";

		// Token: 0x04000041 RID: 65
		public const string DocumentIdName = "DocumentId";

		// Token: 0x04000042 RID: 66
		public const string ExtendedFlagsName = "ExtendedFlags";

		// Token: 0x04000043 RID: 67
		public const string ParentFidName = "ParentFid";

		// Token: 0x04000044 RID: 68
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x04000045 RID: 69
		public const string PhysicalTableName = "Events";

		// Token: 0x04000046 RID: 70
		private PhysicalColumn eventCounter;

		// Token: 0x04000047 RID: 71
		private PhysicalColumn createTime;

		// Token: 0x04000048 RID: 72
		private PhysicalColumn transactionId;

		// Token: 0x04000049 RID: 73
		private PhysicalColumn eventType;

		// Token: 0x0400004A RID: 74
		private PhysicalColumn mailboxNumber;

		// Token: 0x0400004B RID: 75
		private PhysicalColumn clientType;

		// Token: 0x0400004C RID: 76
		private PhysicalColumn flags;

		// Token: 0x0400004D RID: 77
		private PhysicalColumn objectClass;

		// Token: 0x0400004E RID: 78
		private PhysicalColumn fid;

		// Token: 0x0400004F RID: 79
		private PhysicalColumn mid;

		// Token: 0x04000050 RID: 80
		private PhysicalColumn oldFid;

		// Token: 0x04000051 RID: 81
		private PhysicalColumn oldMid;

		// Token: 0x04000052 RID: 82
		private PhysicalColumn oldParentFid;

		// Token: 0x04000053 RID: 83
		private PhysicalColumn itemCount;

		// Token: 0x04000054 RID: 84
		private PhysicalColumn unreadCount;

		// Token: 0x04000055 RID: 85
		private PhysicalColumn sid;

		// Token: 0x04000056 RID: 86
		private PhysicalColumn documentId;

		// Token: 0x04000057 RID: 87
		private PhysicalColumn extendedFlags;

		// Token: 0x04000058 RID: 88
		private PhysicalColumn parentFid;

		// Token: 0x04000059 RID: 89
		private PhysicalColumn extensionBlob;

		// Token: 0x0400005A RID: 90
		private Index eventsPK;

		// Token: 0x0400005B RID: 91
		private Table table;
	}
}
