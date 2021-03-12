using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000068 RID: 104
	public class CategorizedTableParams
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00014C5C File Offset: 0x00012E5C
		public CategorizedTableParams(Table headerTable, Table leafTable, IReadOnlyDictionary<Column, Column> headerRenameDictionary, IReadOnlyDictionary<Column, Column> leafRenameDictionary, IList<object> headerKeyPrefix, IList<object> leafKeyPrefix, SortOrder headerLogicalSortOrder, SortOrder leafLogicalSortOrder, int categoryCount, bool baseMessageViewInReverseOrder, IList<StorePropTag> headerOnlyPropTags, Column depthColumn, Column categIdColumn, Column rowTypeColumn)
		{
			this.headerTable = headerTable;
			this.leafTable = leafTable;
			this.headerRenameDictionary = headerRenameDictionary;
			this.leafRenameDictionary = leafRenameDictionary;
			this.headerKeyPrefix = headerKeyPrefix;
			this.leafKeyPrefix = leafKeyPrefix;
			this.headerLogicalSortOrder = headerLogicalSortOrder;
			this.leafLogicalSortOrder = leafLogicalSortOrder;
			this.categoryCount = categoryCount;
			this.baseMessageViewInReverseOrder = baseMessageViewInReverseOrder;
			this.headerOnlyPropTags = headerOnlyPropTags;
			this.depthColumn = depthColumn;
			this.categIdColumn = categIdColumn;
			this.rowTypeColumn = rowTypeColumn;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00014CDC File Offset: 0x00012EDC
		public Table HeaderTable
		{
			get
			{
				return this.headerTable;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00014CE4 File Offset: 0x00012EE4
		public Table LeafTable
		{
			get
			{
				return this.leafTable;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00014CEC File Offset: 0x00012EEC
		public IReadOnlyDictionary<Column, Column> HeaderRenameDictionary
		{
			get
			{
				return this.headerRenameDictionary;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00014CF4 File Offset: 0x00012EF4
		public IReadOnlyDictionary<Column, Column> LeafRenameDictionary
		{
			get
			{
				return this.leafRenameDictionary;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00014CFC File Offset: 0x00012EFC
		public IList<object> HeaderKeyPrefix
		{
			get
			{
				return this.headerKeyPrefix;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00014D04 File Offset: 0x00012F04
		public IList<object> LeafKeyPrefix
		{
			get
			{
				return this.leafKeyPrefix;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00014D0C File Offset: 0x00012F0C
		public SortOrder HeaderLogicalSortOrder
		{
			get
			{
				return this.headerLogicalSortOrder;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00014D14 File Offset: 0x00012F14
		public SortOrder LeafLogicalSortOrder
		{
			get
			{
				return this.leafLogicalSortOrder;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00014D1C File Offset: 0x00012F1C
		public int CategoryCount
		{
			get
			{
				return this.categoryCount;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00014D24 File Offset: 0x00012F24
		public bool BaseMessageViewInReverseOrder
		{
			get
			{
				return this.baseMessageViewInReverseOrder;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00014D2C File Offset: 0x00012F2C
		public IList<StorePropTag> HeaderOnlyPropTags
		{
			get
			{
				return this.headerOnlyPropTags;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00014D34 File Offset: 0x00012F34
		public Column DepthColumn
		{
			get
			{
				return this.depthColumn;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00014D3C File Offset: 0x00012F3C
		public Column CategIdColumn
		{
			get
			{
				return this.categIdColumn;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00014D44 File Offset: 0x00012F44
		public Column RowTypeColumn
		{
			get
			{
				return this.rowTypeColumn;
			}
		}

		// Token: 0x0400014E RID: 334
		private readonly Table headerTable;

		// Token: 0x0400014F RID: 335
		private readonly Table leafTable;

		// Token: 0x04000150 RID: 336
		private readonly IReadOnlyDictionary<Column, Column> headerRenameDictionary;

		// Token: 0x04000151 RID: 337
		private readonly IReadOnlyDictionary<Column, Column> leafRenameDictionary;

		// Token: 0x04000152 RID: 338
		private readonly IList<object> headerKeyPrefix;

		// Token: 0x04000153 RID: 339
		private readonly IList<object> leafKeyPrefix;

		// Token: 0x04000154 RID: 340
		private readonly SortOrder headerLogicalSortOrder;

		// Token: 0x04000155 RID: 341
		private readonly SortOrder leafLogicalSortOrder;

		// Token: 0x04000156 RID: 342
		private readonly int categoryCount;

		// Token: 0x04000157 RID: 343
		private readonly bool baseMessageViewInReverseOrder;

		// Token: 0x04000158 RID: 344
		private readonly IList<StorePropTag> headerOnlyPropTags;

		// Token: 0x04000159 RID: 345
		private readonly Column depthColumn;

		// Token: 0x0400015A RID: 346
		private readonly Column categIdColumn;

		// Token: 0x0400015B RID: 347
		private readonly Column rowTypeColumn;
	}
}
