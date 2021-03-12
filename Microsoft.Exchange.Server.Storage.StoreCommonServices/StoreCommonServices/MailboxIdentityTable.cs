using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000006 RID: 6
	public sealed class MailboxIdentityTable
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00003234 File Offset: 0x00001434
		internal MailboxIdentityTable()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.localIdGuid = Factory.CreatePhysicalColumn("LocalIdGuid", "LocalIdGuid", typeof(Guid), false, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.idCounter = Factory.CreatePhysicalColumn("IdCounter", "IdCounter", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.cnCounter = Factory.CreatePhysicalColumn("CnCounter", "CnCounter", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.lastCounterPatchingTime = Factory.CreatePhysicalColumn("LastCounterPatchingTime", "LastCounterPatchingTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.nextMessageDocumentId = Factory.CreatePhysicalColumn("NextMessageDocumentId", "NextMessageDocumentId", typeof(int), true, false, false, false, true, Visibility.Public, 0, 4, 4);
			string name = "MailboxIdentityPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[1];
			this.mailboxIdentityPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber
			});
			Index[] indexes = new Index[]
			{
				this.MailboxIdentityPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.LocalIdGuid,
				this.IdCounter,
				this.CnCounter,
				this.LastCounterPatchingTime,
				this.ExtensionBlob,
				this.NextMessageDocumentId
			};
			this.table = Factory.CreateTable("MailboxIdentity", TableClass.MailboxIdentity, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003441 File Offset: 0x00001641
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00003449 File Offset: 0x00001649
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003451 File Offset: 0x00001651
		public PhysicalColumn LocalIdGuid
		{
			get
			{
				return this.localIdGuid;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003459 File Offset: 0x00001659
		public PhysicalColumn IdCounter
		{
			get
			{
				return this.idCounter;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003461 File Offset: 0x00001661
		public PhysicalColumn CnCounter
		{
			get
			{
				return this.cnCounter;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003469 File Offset: 0x00001669
		public PhysicalColumn LastCounterPatchingTime
		{
			get
			{
				return this.lastCounterPatchingTime;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003471 File Offset: 0x00001671
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003479 File Offset: 0x00001679
		public PhysicalColumn NextMessageDocumentId
		{
			get
			{
				return this.nextMessageDocumentId;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003481 File Offset: 0x00001681
		public Index MailboxIdentityPK
		{
			get
			{
				return this.mailboxIdentityPK;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000348C File Offset: 0x0000168C
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxPartitionNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxPartitionNumber = null;
			}
			physicalColumn = this.localIdGuid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.localIdGuid = null;
			}
			physicalColumn = this.idCounter;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.idCounter = null;
			}
			physicalColumn = this.cnCounter;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.cnCounter = null;
			}
			physicalColumn = this.lastCounterPatchingTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastCounterPatchingTime = null;
			}
			physicalColumn = this.extensionBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extensionBlob = null;
			}
			physicalColumn = this.nextMessageDocumentId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.nextMessageDocumentId = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.mailboxIdentityPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.mailboxIdentityPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x04000033 RID: 51
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x04000034 RID: 52
		public const string LocalIdGuidName = "LocalIdGuid";

		// Token: 0x04000035 RID: 53
		public const string IdCounterName = "IdCounter";

		// Token: 0x04000036 RID: 54
		public const string CnCounterName = "CnCounter";

		// Token: 0x04000037 RID: 55
		public const string LastCounterPatchingTimeName = "LastCounterPatchingTime";

		// Token: 0x04000038 RID: 56
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x04000039 RID: 57
		public const string NextMessageDocumentIdName = "NextMessageDocumentId";

		// Token: 0x0400003A RID: 58
		public const string PhysicalTableName = "MailboxIdentity";

		// Token: 0x0400003B RID: 59
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x0400003C RID: 60
		private PhysicalColumn localIdGuid;

		// Token: 0x0400003D RID: 61
		private PhysicalColumn idCounter;

		// Token: 0x0400003E RID: 62
		private PhysicalColumn cnCounter;

		// Token: 0x0400003F RID: 63
		private PhysicalColumn lastCounterPatchingTime;

		// Token: 0x04000040 RID: 64
		private PhysicalColumn extensionBlob;

		// Token: 0x04000041 RID: 65
		private PhysicalColumn nextMessageDocumentId;

		// Token: 0x04000042 RID: 66
		private Index mailboxIdentityPK;

		// Token: 0x04000043 RID: 67
		private Table table;
	}
}
