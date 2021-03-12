using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000004 RID: 4
	public sealed class MSysObjectsTable
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002944 File Offset: 0x00000B44
		internal MSysObjectsTable()
		{
			this.objidTable = Factory.CreatePhysicalColumn("ObjidTable", "ObjidTable", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.type = Factory.CreatePhysicalColumn("Type", "Type", typeof(short), false, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.id = Factory.CreatePhysicalColumn("Id", "Id", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.name = Factory.CreatePhysicalColumn("Name", "Name", typeof(byte[]), false, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.coltypOrPgnoFDP = Factory.CreatePhysicalColumn("ColtypOrPgnoFDP", "ColtypOrPgnoFDP", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.spaceUsage = Factory.CreatePhysicalColumn("SpaceUsage", "SpaceUsage", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.flags = Factory.CreatePhysicalColumn("Flags", "Flags", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.pagesOrLocale = Factory.CreatePhysicalColumn("PagesOrLocale", "PagesOrLocale", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.recordOffset = Factory.CreatePhysicalColumn("RecordOffset", "RecordOffset", typeof(short), true, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.keyMost = Factory.CreatePhysicalColumn("KeyMost", "KeyMost", typeof(short), true, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.stats = Factory.CreatePhysicalColumn("Stats", "Stats", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1, 0, 1);
			this.rootFlag = Factory.CreatePhysicalColumn("RootFlag", "RootFlag", typeof(bool), true, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.templateTable = Factory.CreatePhysicalColumn("TemplateTable", "TemplateTable", typeof(byte[]), true, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.defaultValue = Factory.CreatePhysicalColumn("DefaultValue", "DefaultValue", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1, 0, 1);
			this.keyFldIDs = Factory.CreatePhysicalColumn("KeyFldIDs", "KeyFldIDs", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1, 0, 1);
			this.varSegMac = Factory.CreatePhysicalColumn("VarSegMac", "VarSegMac", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1, 0, 1);
			this.conditionalColumns = Factory.CreatePhysicalColumn("ConditionalColumns", "ConditionalColumns", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1, 0, 1);
			this.lCMapFlags = Factory.CreatePhysicalColumn("LCMapFlags", "LCMapFlags", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.tupleLimits = Factory.CreatePhysicalColumn("TupleLimits", "TupleLimits", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1, 0, 1);
			this.version = Factory.CreatePhysicalColumn("Version", "Version", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1, 0, 1);
			this.sortID = Factory.CreatePhysicalColumn("SortID", "SortID", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1, 0, 1);
			this.callbackData = Factory.CreatePhysicalColumn("CallbackData", "CallbackData", typeof(byte[]), true, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.callbackDependencies = Factory.CreatePhysicalColumn("CallbackDependencies", "CallbackDependencies", typeof(byte[]), true, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.separateLV = Factory.CreatePhysicalColumn("SeparateLV", "SeparateLV", typeof(byte[]), true, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.spaceHints = Factory.CreatePhysicalColumn("SpaceHints", "SpaceHints", typeof(byte[]), true, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.spaceDeferredLVHints = Factory.CreatePhysicalColumn("SpaceDeferredLVHints", "SpaceDeferredLVHints", typeof(byte[]), true, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.localeName = Factory.CreatePhysicalColumn("LocaleName", "LocaleName", typeof(string), true, false, false, false, false, Visibility.Public, 1, 0, 1);
			string text = "Id";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[3];
			this.idIndex = new Index(text, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.ObjidTable,
				this.Type,
				this.Id
			});
			string text2 = "Name";
			bool primaryKey2 = false;
			bool unique2 = true;
			bool schemaExtension2 = false;
			bool[] conditional2 = new bool[3];
			this.nameIndex = new Index(text2, primaryKey2, unique2, schemaExtension2, conditional2, new bool[]
			{
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.ObjidTable,
				this.Type,
				this.Name
			});
			string text3 = "RootObjects";
			bool primaryKey3 = false;
			bool unique3 = true;
			bool schemaExtension3 = false;
			bool[] conditional3 = new bool[2];
			this.rootObjectsIndex = new Index(text3, primaryKey3, unique3, schemaExtension3, conditional3, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.RootFlag,
				this.Name
			});
			Index[] indexes = new Index[]
			{
				this.IdIndex,
				this.NameIndex,
				this.RootObjectsIndex
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.ObjidTable,
				this.Type,
				this.Id,
				this.Name,
				this.ColtypOrPgnoFDP,
				this.SpaceUsage,
				this.Flags,
				this.PagesOrLocale,
				this.RecordOffset,
				this.KeyMost,
				this.Stats,
				this.RootFlag,
				this.TemplateTable,
				this.DefaultValue,
				this.KeyFldIDs,
				this.VarSegMac,
				this.ConditionalColumns,
				this.LCMapFlags,
				this.TupleLimits,
				this.Version,
				this.SortID,
				this.CallbackData,
				this.CallbackDependencies,
				this.SeparateLV,
				this.SpaceHints,
				this.SpaceDeferredLVHints,
				this.LocaleName
			};
			this.table = Factory.CreateTable("MSysObjects", TableClass.Unknown, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, true, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00003048 File Offset: 0x00001248
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00003050 File Offset: 0x00001250
		public PhysicalColumn ObjidTable
		{
			get
			{
				return this.objidTable;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00003058 File Offset: 0x00001258
		public PhysicalColumn Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00003060 File Offset: 0x00001260
		public PhysicalColumn Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00003068 File Offset: 0x00001268
		public PhysicalColumn Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00003070 File Offset: 0x00001270
		public PhysicalColumn ColtypOrPgnoFDP
		{
			get
			{
				return this.coltypOrPgnoFDP;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00003078 File Offset: 0x00001278
		public PhysicalColumn SpaceUsage
		{
			get
			{
				return this.spaceUsage;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00003080 File Offset: 0x00001280
		public PhysicalColumn Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00003088 File Offset: 0x00001288
		public PhysicalColumn PagesOrLocale
		{
			get
			{
				return this.pagesOrLocale;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00003090 File Offset: 0x00001290
		public PhysicalColumn RecordOffset
		{
			get
			{
				return this.recordOffset;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00003098 File Offset: 0x00001298
		public PhysicalColumn KeyMost
		{
			get
			{
				return this.keyMost;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000030A0 File Offset: 0x000012A0
		public PhysicalColumn Stats
		{
			get
			{
				return this.stats;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000030A8 File Offset: 0x000012A8
		public PhysicalColumn RootFlag
		{
			get
			{
				return this.rootFlag;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000030B0 File Offset: 0x000012B0
		public PhysicalColumn TemplateTable
		{
			get
			{
				return this.templateTable;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000030B8 File Offset: 0x000012B8
		public PhysicalColumn DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000030C0 File Offset: 0x000012C0
		public PhysicalColumn KeyFldIDs
		{
			get
			{
				return this.keyFldIDs;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000030C8 File Offset: 0x000012C8
		public PhysicalColumn VarSegMac
		{
			get
			{
				return this.varSegMac;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000030D0 File Offset: 0x000012D0
		public PhysicalColumn ConditionalColumns
		{
			get
			{
				return this.conditionalColumns;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000030D8 File Offset: 0x000012D8
		public PhysicalColumn LCMapFlags
		{
			get
			{
				return this.lCMapFlags;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000030E0 File Offset: 0x000012E0
		public PhysicalColumn TupleLimits
		{
			get
			{
				return this.tupleLimits;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000030E8 File Offset: 0x000012E8
		public PhysicalColumn Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000030F0 File Offset: 0x000012F0
		public PhysicalColumn SortID
		{
			get
			{
				return this.sortID;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000030F8 File Offset: 0x000012F8
		public PhysicalColumn CallbackData
		{
			get
			{
				return this.callbackData;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003100 File Offset: 0x00001300
		public PhysicalColumn CallbackDependencies
		{
			get
			{
				return this.callbackDependencies;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003108 File Offset: 0x00001308
		public PhysicalColumn SeparateLV
		{
			get
			{
				return this.separateLV;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003110 File Offset: 0x00001310
		public PhysicalColumn SpaceHints
		{
			get
			{
				return this.spaceHints;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003118 File Offset: 0x00001318
		public PhysicalColumn SpaceDeferredLVHints
		{
			get
			{
				return this.spaceDeferredLVHints;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003120 File Offset: 0x00001320
		public PhysicalColumn LocaleName
		{
			get
			{
				return this.localeName;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003128 File Offset: 0x00001328
		public Index IdIndex
		{
			get
			{
				return this.idIndex;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003130 File Offset: 0x00001330
		public Index NameIndex
		{
			get
			{
				return this.nameIndex;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003138 File Offset: 0x00001338
		public Index RootObjectsIndex
		{
			get
			{
				return this.rootObjectsIndex;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003140 File Offset: 0x00001340
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.objidTable;
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
			physicalColumn = this.id;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.id = null;
			}
			physicalColumn = this.name;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.name = null;
			}
			physicalColumn = this.coltypOrPgnoFDP;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.coltypOrPgnoFDP = null;
			}
			physicalColumn = this.spaceUsage;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.spaceUsage = null;
			}
			physicalColumn = this.flags;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.flags = null;
			}
			physicalColumn = this.pagesOrLocale;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.pagesOrLocale = null;
			}
			physicalColumn = this.recordOffset;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.recordOffset = null;
			}
			physicalColumn = this.keyMost;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.keyMost = null;
			}
			physicalColumn = this.stats;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.stats = null;
			}
			physicalColumn = this.rootFlag;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.rootFlag = null;
			}
			physicalColumn = this.templateTable;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.templateTable = null;
			}
			physicalColumn = this.defaultValue;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.defaultValue = null;
			}
			physicalColumn = this.keyFldIDs;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.keyFldIDs = null;
			}
			physicalColumn = this.varSegMac;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.varSegMac = null;
			}
			physicalColumn = this.conditionalColumns;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.conditionalColumns = null;
			}
			physicalColumn = this.lCMapFlags;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lCMapFlags = null;
			}
			physicalColumn = this.tupleLimits;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.tupleLimits = null;
			}
			physicalColumn = this.version;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.version = null;
			}
			physicalColumn = this.sortID;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.sortID = null;
			}
			physicalColumn = this.callbackData;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.callbackData = null;
			}
			physicalColumn = this.callbackDependencies;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.callbackDependencies = null;
			}
			physicalColumn = this.separateLV;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.separateLV = null;
			}
			physicalColumn = this.spaceHints;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.spaceHints = null;
			}
			physicalColumn = this.spaceDeferredLVHints;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.spaceDeferredLVHints = null;
			}
			physicalColumn = this.localeName;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.localeName = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.idIndex;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.idIndex = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.nameIndex;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.nameIndex = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.rootObjectsIndex;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.rootObjectsIndex = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x04000015 RID: 21
		public const string ObjidTableName = "ObjidTable";

		// Token: 0x04000016 RID: 22
		public const string TypeName = "Type";

		// Token: 0x04000017 RID: 23
		public const string IdName = "Id";

		// Token: 0x04000018 RID: 24
		public const string NameName = "Name";

		// Token: 0x04000019 RID: 25
		public const string ColtypOrPgnoFDPName = "ColtypOrPgnoFDP";

		// Token: 0x0400001A RID: 26
		public const string SpaceUsageName = "SpaceUsage";

		// Token: 0x0400001B RID: 27
		public const string FlagsName = "Flags";

		// Token: 0x0400001C RID: 28
		public const string PagesOrLocaleName = "PagesOrLocale";

		// Token: 0x0400001D RID: 29
		public const string RecordOffsetName = "RecordOffset";

		// Token: 0x0400001E RID: 30
		public const string KeyMostName = "KeyMost";

		// Token: 0x0400001F RID: 31
		public const string StatsName = "Stats";

		// Token: 0x04000020 RID: 32
		public const string RootFlagName = "RootFlag";

		// Token: 0x04000021 RID: 33
		public const string TemplateTableName = "TemplateTable";

		// Token: 0x04000022 RID: 34
		public const string DefaultValueName = "DefaultValue";

		// Token: 0x04000023 RID: 35
		public const string KeyFldIDsName = "KeyFldIDs";

		// Token: 0x04000024 RID: 36
		public const string VarSegMacName = "VarSegMac";

		// Token: 0x04000025 RID: 37
		public const string ConditionalColumnsName = "ConditionalColumns";

		// Token: 0x04000026 RID: 38
		public const string LCMapFlagsName = "LCMapFlags";

		// Token: 0x04000027 RID: 39
		public const string TupleLimitsName = "TupleLimits";

		// Token: 0x04000028 RID: 40
		public const string VersionName = "Version";

		// Token: 0x04000029 RID: 41
		public const string SortIDName = "SortID";

		// Token: 0x0400002A RID: 42
		public const string CallbackDataName = "CallbackData";

		// Token: 0x0400002B RID: 43
		public const string CallbackDependenciesName = "CallbackDependencies";

		// Token: 0x0400002C RID: 44
		public const string SeparateLVName = "SeparateLV";

		// Token: 0x0400002D RID: 45
		public const string SpaceHintsName = "SpaceHints";

		// Token: 0x0400002E RID: 46
		public const string SpaceDeferredLVHintsName = "SpaceDeferredLVHints";

		// Token: 0x0400002F RID: 47
		public const string LocaleNameName = "LocaleName";

		// Token: 0x04000030 RID: 48
		public const string PhysicalTableName = "MSysObjects";

		// Token: 0x04000031 RID: 49
		private PhysicalColumn objidTable;

		// Token: 0x04000032 RID: 50
		private PhysicalColumn type;

		// Token: 0x04000033 RID: 51
		private PhysicalColumn id;

		// Token: 0x04000034 RID: 52
		private PhysicalColumn name;

		// Token: 0x04000035 RID: 53
		private PhysicalColumn coltypOrPgnoFDP;

		// Token: 0x04000036 RID: 54
		private PhysicalColumn spaceUsage;

		// Token: 0x04000037 RID: 55
		private PhysicalColumn flags;

		// Token: 0x04000038 RID: 56
		private PhysicalColumn pagesOrLocale;

		// Token: 0x04000039 RID: 57
		private PhysicalColumn recordOffset;

		// Token: 0x0400003A RID: 58
		private PhysicalColumn keyMost;

		// Token: 0x0400003B RID: 59
		private PhysicalColumn stats;

		// Token: 0x0400003C RID: 60
		private PhysicalColumn rootFlag;

		// Token: 0x0400003D RID: 61
		private PhysicalColumn templateTable;

		// Token: 0x0400003E RID: 62
		private PhysicalColumn defaultValue;

		// Token: 0x0400003F RID: 63
		private PhysicalColumn keyFldIDs;

		// Token: 0x04000040 RID: 64
		private PhysicalColumn varSegMac;

		// Token: 0x04000041 RID: 65
		private PhysicalColumn conditionalColumns;

		// Token: 0x04000042 RID: 66
		private PhysicalColumn lCMapFlags;

		// Token: 0x04000043 RID: 67
		private PhysicalColumn tupleLimits;

		// Token: 0x04000044 RID: 68
		private PhysicalColumn version;

		// Token: 0x04000045 RID: 69
		private PhysicalColumn sortID;

		// Token: 0x04000046 RID: 70
		private PhysicalColumn callbackData;

		// Token: 0x04000047 RID: 71
		private PhysicalColumn callbackDependencies;

		// Token: 0x04000048 RID: 72
		private PhysicalColumn separateLV;

		// Token: 0x04000049 RID: 73
		private PhysicalColumn spaceHints;

		// Token: 0x0400004A RID: 74
		private PhysicalColumn spaceDeferredLVHints;

		// Token: 0x0400004B RID: 75
		private PhysicalColumn localeName;

		// Token: 0x0400004C RID: 76
		private Index idIndex;

		// Token: 0x0400004D RID: 77
		private Index nameIndex;

		// Token: 0x0400004E RID: 78
		private Index rootObjectsIndex;

		// Token: 0x0400004F RID: 79
		private Table table;
	}
}
