using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000005 RID: 5
	public sealed class MSysObjidsTable
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00003AAC File Offset: 0x00001CAC
		internal MSysObjidsTable()
		{
			this.objid = Factory.CreatePhysicalColumn("objid", "objid", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.objidTable = Factory.CreatePhysicalColumn("objidTable", "objidTable", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.type = Factory.CreatePhysicalColumn("type", "type", typeof(short), false, false, false, false, false, Visibility.Public, 0, 2, 2);
			string name = "primary";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[1];
			this.primaryIndex = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.objid
			});
			Index[] indexes = new Index[]
			{
				this.primaryIndex
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.objid,
				this.objidTable,
				this.type
			};
			this.table = Factory.CreateTable("MSysObjids", TableClass.Unknown, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, true, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003BE6 File Offset: 0x00001DE6
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003BEE File Offset: 0x00001DEE
		public PhysicalColumn Objid
		{
			get
			{
				return this.objid;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public PhysicalColumn ObjidTable
		{
			get
			{
				return this.objidTable;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003BFE File Offset: 0x00001DFE
		public PhysicalColumn Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003C06 File Offset: 0x00001E06
		public Index PrimaryIndex
		{
			get
			{
				return this.primaryIndex;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003C10 File Offset: 0x00001E10
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.objid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.objid = null;
			}
			physicalColumn = this.objidTable;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.objidTable = null;
			}
			physicalColumn = this.type;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.type = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.primaryIndex;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.primaryIndex = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x04000050 RID: 80
		public const string objidName = "objid";

		// Token: 0x04000051 RID: 81
		public const string objidTableName = "objidTable";

		// Token: 0x04000052 RID: 82
		public const string typeName = "type";

		// Token: 0x04000053 RID: 83
		public const string PhysicalTableName = "MSysObjids";

		// Token: 0x04000054 RID: 84
		private PhysicalColumn objid;

		// Token: 0x04000055 RID: 85
		private PhysicalColumn objidTable;

		// Token: 0x04000056 RID: 86
		private PhysicalColumn type;

		// Token: 0x04000057 RID: 87
		private Index primaryIndex;

		// Token: 0x04000058 RID: 88
		private Table table;
	}
}
