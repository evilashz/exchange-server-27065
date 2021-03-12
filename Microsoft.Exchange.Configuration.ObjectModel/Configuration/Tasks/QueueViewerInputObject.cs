using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	internal class QueueViewerInputObject : ConfigurableObject
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000041E0 File Offset: 0x000023E0
		public QueueViewerInputObject(int bookmarkIndex, IConfigurable bookmarkObject, bool includeBookmark, bool includeComponentLatencyInfo, bool includeDetails, int pageSize, QueryFilter queryFilter, bool searchForward, QueueViewerSortOrderEntry[] sortOrder) : base(new QueueViewerInputPropertyBag())
		{
			this.BookmarkIndex = bookmarkIndex;
			this.BookmarkObject = bookmarkObject;
			this.IncludeBookmark = includeBookmark;
			this.IncludeComponentLatencyInfo = includeComponentLatencyInfo;
			this.IncludeDetails = includeDetails;
			this.PageSize = pageSize;
			this.QueryFilter = queryFilter;
			this.SearchForward = searchForward;
			this.SortOrder = sortOrder;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000423D File Offset: 0x0000243D
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return QueueViewerInputObject.schema;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004244 File Offset: 0x00002444
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000425B File Offset: 0x0000245B
		public QueryFilter QueryFilter
		{
			get
			{
				return (QueryFilter)this.propertyBag[QueueViewerInputObjectSchema.QueryFilter];
			}
			internal set
			{
				this.propertyBag[QueueViewerInputObjectSchema.QueryFilter] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004270 File Offset: 0x00002470
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000042A7 File Offset: 0x000024A7
		public QueueViewerSortOrderEntry[] SortOrder
		{
			get
			{
				MultiValuedProperty<QueueViewerSortOrderEntry> multiValuedProperty = (MultiValuedProperty<QueueViewerSortOrderEntry>)this.propertyBag[QueueViewerInputObjectSchema.SortOrder];
				if (multiValuedProperty == null || multiValuedProperty.Count <= 0)
				{
					return null;
				}
				return multiValuedProperty.ToArray();
			}
			internal set
			{
				this.propertyBag[QueueViewerInputObjectSchema.SortOrder] = ((value != null) ? new MultiValuedProperty<QueueViewerSortOrderEntry>(value) : null);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000042C5 File Offset: 0x000024C5
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000042DC File Offset: 0x000024DC
		public bool SearchForward
		{
			get
			{
				return (bool)this.propertyBag[QueueViewerInputObjectSchema.SearchForward];
			}
			internal set
			{
				this.propertyBag[QueueViewerInputObjectSchema.SearchForward] = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000042F4 File Offset: 0x000024F4
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000430B File Offset: 0x0000250B
		public int PageSize
		{
			get
			{
				return (int)this.propertyBag[QueueViewerInputObjectSchema.PageSize];
			}
			internal set
			{
				this.propertyBag[QueueViewerInputObjectSchema.PageSize] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004323 File Offset: 0x00002523
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000433A File Offset: 0x0000253A
		public IConfigurable BookmarkObject
		{
			get
			{
				return (IConfigurable)this.propertyBag[QueueViewerInputObjectSchema.BookmarkObject];
			}
			internal set
			{
				this.propertyBag[QueueViewerInputObjectSchema.BookmarkObject] = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000434D File Offset: 0x0000254D
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004364 File Offset: 0x00002564
		public int BookmarkIndex
		{
			get
			{
				return (int)this.propertyBag[QueueViewerInputObjectSchema.BookmarkIndex];
			}
			internal set
			{
				this.propertyBag[QueueViewerInputObjectSchema.BookmarkIndex] = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000437C File Offset: 0x0000257C
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004393 File Offset: 0x00002593
		public bool IncludeBookmark
		{
			get
			{
				return (bool)this.propertyBag[QueueViewerInputObjectSchema.IncludeBookmark];
			}
			internal set
			{
				this.propertyBag[QueueViewerInputObjectSchema.IncludeBookmark] = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000043AB File Offset: 0x000025AB
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000043C2 File Offset: 0x000025C2
		public bool IncludeDetails
		{
			get
			{
				return (bool)this.propertyBag[QueueViewerInputObjectSchema.IncludeDetails];
			}
			internal set
			{
				this.propertyBag[QueueViewerInputObjectSchema.IncludeDetails] = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000043DA File Offset: 0x000025DA
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000043F1 File Offset: 0x000025F1
		public bool IncludeComponentLatencyInfo
		{
			get
			{
				return (bool)this.propertyBag[QueueViewerInputObjectSchema.IncludeComponentLatencyInfo];
			}
			internal set
			{
				this.propertyBag[QueueViewerInputObjectSchema.IncludeComponentLatencyInfo] = value;
			}
		}

		// Token: 0x0400003B RID: 59
		private static QueueViewerInputObjectSchema schema = ObjectSchema.GetInstance<QueueViewerInputObjectSchema>();
	}
}
