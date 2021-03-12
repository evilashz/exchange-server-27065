using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000003 RID: 3
	public sealed class ExtendedPropertyNameMappingTable
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000025E8 File Offset: 0x000007E8
		internal ExtendedPropertyNameMappingTable()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxPartitionNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.propNumber = Factory.CreatePhysicalColumn("PropNumber", "PropNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.propGuid = Factory.CreatePhysicalColumn("PropGuid", "PropGuid", typeof(Guid), false, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.propName = Factory.CreatePhysicalColumn("PropName", "PropName", typeof(string), true, false, false, false, false, Visibility.Public, 256, 0, 256);
			this.propDispId = Factory.CreatePhysicalColumn("PropDispId", "PropDispId", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "ExtendedPropertyNameMappingPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.extendedPropertyNameMappingPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.PropNumber
			});
			Index[] indexes = new Index[]
			{
				this.ExtendedPropertyNameMappingPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 1);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.PropNumber,
				this.PropGuid,
				this.PropName,
				this.PropDispId,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("ExtendedPropertyNameMapping", TableClass.ExtendedPropertyNameMapping, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000027DA File Offset: 0x000009DA
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000027E2 File Offset: 0x000009E2
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000027EA File Offset: 0x000009EA
		public PhysicalColumn PropNumber
		{
			get
			{
				return this.propNumber;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000027F2 File Offset: 0x000009F2
		public PhysicalColumn PropGuid
		{
			get
			{
				return this.propGuid;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000027FA File Offset: 0x000009FA
		public PhysicalColumn PropName
		{
			get
			{
				return this.propName;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002802 File Offset: 0x00000A02
		public PhysicalColumn PropDispId
		{
			get
			{
				return this.propDispId;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000280A File Offset: 0x00000A0A
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002812 File Offset: 0x00000A12
		public Index ExtendedPropertyNameMappingPK
		{
			get
			{
				return this.extendedPropertyNameMappingPK;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000281C File Offset: 0x00000A1C
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxPartitionNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxPartitionNumber = null;
			}
			physicalColumn = this.propNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propNumber = null;
			}
			physicalColumn = this.propGuid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propGuid = null;
			}
			physicalColumn = this.propName;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propName = null;
			}
			physicalColumn = this.propDispId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propDispId = null;
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
			Index index = this.extendedPropertyNameMappingPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.extendedPropertyNameMappingPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x0400000A RID: 10
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x0400000B RID: 11
		public const string PropNumberName = "PropNumber";

		// Token: 0x0400000C RID: 12
		public const string PropGuidName = "PropGuid";

		// Token: 0x0400000D RID: 13
		public const string PropNameName = "PropName";

		// Token: 0x0400000E RID: 14
		public const string PropDispIdName = "PropDispId";

		// Token: 0x0400000F RID: 15
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x04000010 RID: 16
		public const string PhysicalTableName = "ExtendedPropertyNameMapping";

		// Token: 0x04000011 RID: 17
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x04000012 RID: 18
		private PhysicalColumn propNumber;

		// Token: 0x04000013 RID: 19
		private PhysicalColumn propGuid;

		// Token: 0x04000014 RID: 20
		private PhysicalColumn propName;

		// Token: 0x04000015 RID: 21
		private PhysicalColumn propDispId;

		// Token: 0x04000016 RID: 22
		private PhysicalColumn extensionBlob;

		// Token: 0x04000017 RID: 23
		private Index extendedPropertyNameMappingPK;

		// Token: 0x04000018 RID: 24
		private Table table;
	}
}
