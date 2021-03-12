using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200008C RID: 140
	public abstract class Table
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001C383 File Offset: 0x0001A583
		public static PhysicalColumn[] NoColumns
		{
			get
			{
				return Array<PhysicalColumn>.Empty;
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001C38C File Offset: 0x0001A58C
		protected Table(string name, TableClass tableClass, CultureInfo culture, bool trackDirtyObjects, TableAccessHints tableAccessHints, bool readOnly, Visibility visibility, bool schemaExtension, SpecialColumns specialCols, Index[] indexes, PhysicalColumn[] computedColumns, PhysicalColumn[] columns)
		{
			if (schemaExtension)
			{
				this.minVersion = int.MaxValue;
			}
			else
			{
				this.minVersion = 0;
			}
			this.maxVersion = int.MaxValue;
			this.specialCols = specialCols;
			this.indexes = new List<Index>(indexes);
			this.name = name;
			this.tableClass = tableClass;
			this.culture = culture;
			this.columns = new List<PhysicalColumn>(columns);
			for (int i = 0; i < this.Columns.Count; i++)
			{
				this.Columns[i].Index = i;
				this.Columns[i].Table = this;
			}
			for (int j = 0; j < computedColumns.Length; j++)
			{
				computedColumns[j].Index = -1;
				computedColumns[j].Table = this;
			}
			for (int k = 0; k < this.Indexes.Count; k++)
			{
				if (this.Indexes[k].PrimaryKey)
				{
					this.primaryKeyIndex = this.Indexes[k];
					break;
				}
			}
			this.trackDirtyObjects = trackDirtyObjects;
			this.tableAccessHints = tableAccessHints;
			this.readOnly = readOnly;
			this.visibility = visibility;
			this.commonColumns = new List<PhysicalColumn>(this.Columns.Count);
			for (int l = 0; l < this.Columns.Count; l++)
			{
				if (!this.Columns[l].NotFetchedByDefault && this.PrimaryKeyIndex.PositionInIndex(this.Columns[l]) < 0)
				{
					this.commonColumns.Add(this.Columns[l]);
				}
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001C523 File Offset: 0x0001A723
		public TableClass TableClass
		{
			[DebuggerStepThrough]
			get
			{
				return this.tableClass;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001C52B File Offset: 0x0001A72B
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001C533 File Offset: 0x0001A733
		public SpecialColumns SpecialCols
		{
			get
			{
				return this.specialCols;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001C53B File Offset: 0x0001A73B
		public IList<PhysicalColumn> Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001C543 File Offset: 0x0001A743
		public IList<Column> ColumnsForTestOnly
		{
			get
			{
				return new List<Column>(this.columns);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0001C550 File Offset: 0x0001A750
		public IList<Index> Indexes
		{
			get
			{
				return this.indexes;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001C558 File Offset: 0x0001A758
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0001C560 File Offset: 0x0001A760
		public IIndex FullTextIndex
		{
			get
			{
				return this.fullTextIndex;
			}
			set
			{
				this.fullTextIndex = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001C569 File Offset: 0x0001A769
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0001C571 File Offset: 0x0001A771
		public int MinVersion
		{
			get
			{
				return this.minVersion;
			}
			set
			{
				this.minVersion = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001C57A File Offset: 0x0001A77A
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0001C582 File Offset: 0x0001A782
		public int MaxVersion
		{
			get
			{
				return this.maxVersion;
			}
			set
			{
				this.maxVersion = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001C58B File Offset: 0x0001A78B
		public Index PrimaryKeyIndex
		{
			get
			{
				return this.primaryKeyIndex;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001C593 File Offset: 0x0001A793
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001C59B File Offset: 0x0001A79B
		public IList<PhysicalColumn> CommonColumns
		{
			get
			{
				return this.commonColumns;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0001C5A3 File Offset: 0x0001A7A3
		public bool TrackDirtyObjects
		{
			get
			{
				return this.trackDirtyObjects;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0001C5AB File Offset: 0x0001A7AB
		public TableAccessHints TableAccessHints
		{
			get
			{
				return this.tableAccessHints;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001C5B3 File Offset: 0x0001A7B3
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001C5BB File Offset: 0x0001A7BB
		public int NumberOfPartitioningColumns
		{
			get
			{
				return this.specialCols.NumberOfPartioningColumns;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001C5C8 File Offset: 0x0001A7C8
		public bool IsPartitioned
		{
			get
			{
				return this.NumberOfPartitioningColumns > 0;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001C5D3 File Offset: 0x0001A7D3
		public Visibility Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001C5DB File Offset: 0x0001A7DB
		public static bool operator ==(Table table1, Table table2)
		{
			return object.ReferenceEquals(table1, table2) || (table1 != null && table2 != null && table1.Equals(table2));
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001C5F7 File Offset: 0x0001A7F7
		public static bool operator !=(Table table1, Table table2)
		{
			return !(table1 == table2);
		}

		// Token: 0x06000615 RID: 1557
		public abstract void CreateTable(IConnectionProvider connectionProvider, int version);

		// Token: 0x06000616 RID: 1558
		public abstract void AddColumn(IConnectionProvider connectionProvider, PhysicalColumn column);

		// Token: 0x06000617 RID: 1559
		public abstract void RemoveColumn(IConnectionProvider connectionProvider, PhysicalColumn column);

		// Token: 0x06000618 RID: 1560
		public abstract void CreateIndex(IConnectionProvider connectionProvider, Index index, IList<object> partitionValues);

		// Token: 0x06000619 RID: 1561
		public abstract void DeleteIndex(IConnectionProvider connectionProvider, Index index, IList<object> partitionValues);

		// Token: 0x0600061A RID: 1562
		public abstract bool IsIndexCreated(IConnectionProvider connectionProvider, Index index, IList<object> partitionValues);

		// Token: 0x0600061B RID: 1563
		public abstract bool ValidateLocaleVersion(IConnectionProvider connectionProvider, IList<object> partitionValues);

		// Token: 0x0600061C RID: 1564
		public abstract void GetTableSize(IConnectionProvider connectionProvider, IList<object> partitionValues, out int totalPages, out int availablePages);

		// Token: 0x0600061D RID: 1565 RVA: 0x0001C603 File Offset: 0x0001A803
		public virtual VirtualColumn VirtualColumn(string column)
		{
			return null;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001C608 File Offset: 0x0001A808
		public override bool Equals(object other)
		{
			Table table = other as Table;
			return table != null && this.name == table.name;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001C63B File Offset: 0x0001A83B
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001C648 File Offset: 0x0001A848
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001C650 File Offset: 0x0001A850
		public PhysicalColumn Column(string name)
		{
			PhysicalColumn result = null;
			for (int i = 0; i < this.Columns.Count; i++)
			{
				if (this.Columns[i].Name == name)
				{
					result = this.Columns[i];
					break;
				}
			}
			return result;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001C69E File Offset: 0x0001A89E
		internal void SetPrimaryKeyIndexForUpgraders(Index primaryKeyIndex)
		{
			this.primaryKeyIndex.SetIsPrimaryKeyForUpgraders(false);
			this.primaryKeyIndex = primaryKeyIndex;
			this.primaryKeyIndex.SetIsPrimaryKeyForUpgraders(true);
		}

		// Token: 0x040001F1 RID: 497
		private readonly string name;

		// Token: 0x040001F2 RID: 498
		private readonly TableClass tableClass;

		// Token: 0x040001F3 RID: 499
		private readonly CultureInfo culture;

		// Token: 0x040001F4 RID: 500
		private readonly bool readOnly;

		// Token: 0x040001F5 RID: 501
		private Index primaryKeyIndex;

		// Token: 0x040001F6 RID: 502
		private IIndex fullTextIndex;

		// Token: 0x040001F7 RID: 503
		private IList<PhysicalColumn> columns;

		// Token: 0x040001F8 RID: 504
		private IList<Index> indexes;

		// Token: 0x040001F9 RID: 505
		private SpecialColumns specialCols;

		// Token: 0x040001FA RID: 506
		private IList<PhysicalColumn> commonColumns;

		// Token: 0x040001FB RID: 507
		private bool trackDirtyObjects;

		// Token: 0x040001FC RID: 508
		private TableAccessHints tableAccessHints;

		// Token: 0x040001FD RID: 509
		private int minVersion;

		// Token: 0x040001FE RID: 510
		private int maxVersion;

		// Token: 0x040001FF RID: 511
		private Visibility visibility;
	}
}
