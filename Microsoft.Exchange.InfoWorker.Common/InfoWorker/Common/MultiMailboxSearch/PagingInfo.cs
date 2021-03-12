using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000209 RID: 521
	internal class PagingInfo
	{
		// Token: 0x06000DEA RID: 3562 RVA: 0x0003CD34 File Offset: 0x0003AF34
		public PagingInfo(List<PropertyDefinition> data, SortBy sort, int pageSize, PageDirection direction, ReferenceItem referenceItem, ExTimeZone timeZone) : this(data, sort, pageSize, direction, referenceItem, timeZone, false)
		{
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003CD48 File Offset: 0x0003AF48
		public PagingInfo(List<PropertyDefinition> data, SortBy sort, int pageSize, PageDirection direction, ReferenceItem referenceItem, ExTimeZone timeZone, bool excludeDuplicatesItems) : this(data, sort, pageSize, direction, referenceItem, timeZone, excludeDuplicatesItems, PreviewItemBaseShape.Default, null)
		{
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0003CD68 File Offset: 0x0003AF68
		public PagingInfo(List<PropertyDefinition> data, SortBy sort, int pageSize, PageDirection direction, ReferenceItem referenceItem, ExTimeZone timeZone, bool excludeDuplicatesItems, PreviewItemBaseShape baseShape, List<ExtendedPropertyInfo> additionalProperties)
		{
			Util.ThrowOnNull(data, "data");
			Util.ThrowOnNull(sort, "sort");
			if (pageSize == 0)
			{
				throw new ArgumentException("Page size cannot be 0");
			}
			if (referenceItem == null && direction == PageDirection.Previous)
			{
				throw new ArgumentException("PagingInfo: Have to provide sort column value to view previous page");
			}
			if (!PagingInfo.ValidateDataColumns(data))
			{
				throw new ArgumentException("PagingInfo: Invalid data columns");
			}
			this.originalDataColumns = data;
			this.dataColumns = new List<PropertyDefinition>(data);
			this.sort = sort;
			this.referenceItem = referenceItem;
			this.direction = direction;
			this.pageSize = pageSize;
			this.timeZone = timeZone;
			this.excludeDuplicateItems = excludeDuplicatesItems;
			this.baseShape = baseShape;
			this.additionalProperties = additionalProperties;
			if (!this.originalDataColumns.Contains(sort.ColumnDefinition))
			{
				this.dataColumns.Add(sort.ColumnDefinition);
			}
			foreach (PropertyDefinition item in PagingInfo.RequiredDataPropertiesFromStore)
			{
				if (!this.originalDataColumns.Contains(item))
				{
					this.dataColumns.Add(item);
				}
			}
			if (this.additionalProperties != null)
			{
				foreach (ExtendedPropertyInfo extendedPropertyInfo in this.additionalProperties)
				{
					if (extendedPropertyInfo.XsoPropertyDefinition != null && !this.dataColumns.Contains(extendedPropertyInfo.XsoPropertyDefinition))
					{
						this.dataColumns.Add(extendedPropertyInfo.XsoPropertyDefinition);
					}
				}
			}
			this.sorts = new SortBy[]
			{
				sort,
				new SortBy(ItemSchema.DocumentId, SortOrder.Ascending)
			};
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0003CF08 File Offset: 0x0003B108
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0003CF10 File Offset: 0x0003B110
		public SortBy[] Sorts
		{
			get
			{
				return this.sorts;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0003CF18 File Offset: 0x0003B118
		public List<PropertyDefinition> DataColumns
		{
			get
			{
				return this.dataColumns;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0003CF20 File Offset: 0x0003B120
		public List<PropertyDefinition> OriginalDataColumns
		{
			get
			{
				return this.originalDataColumns;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0003CF28 File Offset: 0x0003B128
		public int DataColumnCount
		{
			get
			{
				return this.dataColumns.Count + 1;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0003CF37 File Offset: 0x0003B137
		public SortBy SortBy
		{
			get
			{
				return this.sort;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0003CF3F File Offset: 0x0003B13F
		public PropertyDefinition SortColumn
		{
			get
			{
				return this.sort.ColumnDefinition;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0003CF4C File Offset: 0x0003B14C
		public bool AscendingSort
		{
			get
			{
				return this.sort.SortOrder == SortOrder.Ascending;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0003CF5C File Offset: 0x0003B15C
		public ReferenceItem SortValue
		{
			get
			{
				return this.referenceItem;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0003CF64 File Offset: 0x0003B164
		public PageDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0003CF6C File Offset: 0x0003B16C
		public ExTimeZone TimeZone
		{
			get
			{
				return this.timeZone;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0003CF74 File Offset: 0x0003B174
		public bool ExcludeDuplicates
		{
			get
			{
				return this.excludeDuplicateItems;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0003CF7C File Offset: 0x0003B17C
		public PreviewItemBaseShape BaseShape
		{
			get
			{
				return this.baseShape;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0003CF84 File Offset: 0x0003B184
		public List<ExtendedPropertyInfo> AdditionalProperties
		{
			get
			{
				return this.additionalProperties;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x0003CF8C File Offset: 0x0003B18C
		// (set) Token: 0x06000DFC RID: 3580 RVA: 0x0003CF94 File Offset: 0x0003B194
		public string OriginalSortByReference { get; set; }

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003CFA0 File Offset: 0x0003B1A0
		public QueryFilter GetPagingFilter(MailboxInfo mailbox)
		{
			if (this.referenceItem == null || this.referenceItem.SortColumnValue == null)
			{
				return null;
			}
			if (this.direction == PageDirection.Next)
			{
				if (this.sort.SortOrder == SortOrder.Ascending)
				{
					if (this.referenceItem.MailboxIdHash < mailbox.MailboxGuid.GetHashCode())
					{
						return new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					}
					if (this.referenceItem.MailboxIdHash > mailbox.MailboxGuid.GetHashCode())
					{
						return new ComparisonFilter(ComparisonOperator.GreaterThan, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					}
					ComparisonFilter comparisonFilter = new ComparisonFilter(ComparisonOperator.GreaterThan, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					ComparisonFilter comparisonFilter2 = new ComparisonFilter(ComparisonOperator.Equal, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					ComparisonFilter comparisonFilter3 = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.DocumentId, this.referenceItem.DocId);
					AndFilter andFilter = new AndFilter(new QueryFilter[]
					{
						comparisonFilter2,
						comparisonFilter3
					});
					return new OrFilter(new QueryFilter[]
					{
						comparisonFilter,
						andFilter
					});
				}
				else
				{
					if (this.referenceItem.MailboxIdHash < mailbox.MailboxGuid.GetHashCode())
					{
						return new ComparisonFilter(ComparisonOperator.LessThanOrEqual, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					}
					if (this.referenceItem.MailboxIdHash > mailbox.MailboxGuid.GetHashCode())
					{
						return new ComparisonFilter(ComparisonOperator.LessThan, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					}
					ComparisonFilter comparisonFilter4 = new ComparisonFilter(ComparisonOperator.LessThan, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					ComparisonFilter comparisonFilter5 = new ComparisonFilter(ComparisonOperator.Equal, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					ComparisonFilter comparisonFilter6 = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.DocumentId, this.referenceItem.DocId);
					AndFilter andFilter2 = new AndFilter(new QueryFilter[]
					{
						comparisonFilter5,
						comparisonFilter6
					});
					return new OrFilter(new QueryFilter[]
					{
						comparisonFilter4,
						andFilter2
					});
				}
			}
			else
			{
				if (this.direction != PageDirection.Previous)
				{
					return null;
				}
				if (this.sort.SortOrder == SortOrder.Ascending)
				{
					if (this.referenceItem.MailboxIdHash < mailbox.MailboxGuid.GetHashCode())
					{
						return new ComparisonFilter(ComparisonOperator.LessThan, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					}
					if (this.referenceItem.MailboxIdHash > mailbox.MailboxGuid.GetHashCode())
					{
						return new ComparisonFilter(ComparisonOperator.LessThanOrEqual, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					}
					ComparisonFilter comparisonFilter7 = new ComparisonFilter(ComparisonOperator.LessThan, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					ComparisonFilter comparisonFilter8 = new ComparisonFilter(ComparisonOperator.Equal, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					ComparisonFilter comparisonFilter9 = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.DocumentId, this.referenceItem.DocId);
					AndFilter andFilter3 = new AndFilter(new QueryFilter[]
					{
						comparisonFilter8,
						comparisonFilter9
					});
					return new OrFilter(new QueryFilter[]
					{
						comparisonFilter7,
						andFilter3
					});
				}
				else
				{
					if (this.referenceItem.MailboxIdHash < mailbox.MailboxGuid.GetHashCode())
					{
						return new ComparisonFilter(ComparisonOperator.GreaterThan, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					}
					if (this.referenceItem.MailboxIdHash > mailbox.MailboxGuid.GetHashCode())
					{
						return new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					}
					ComparisonFilter comparisonFilter10 = new ComparisonFilter(ComparisonOperator.GreaterThan, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					ComparisonFilter comparisonFilter11 = new ComparisonFilter(ComparisonOperator.Equal, this.sort.ColumnDefinition, this.referenceItem.SortColumnValue);
					ComparisonFilter comparisonFilter12 = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.DocumentId, this.referenceItem.DocId);
					AndFilter andFilter4 = new AndFilter(new QueryFilter[]
					{
						comparisonFilter11,
						comparisonFilter12
					});
					return new OrFilter(new QueryFilter[]
					{
						comparisonFilter10,
						andFilter4
					});
				}
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003D444 File Offset: 0x0003B644
		public override bool Equals(object obj)
		{
			PagingInfo pagingInfo = obj as PagingInfo;
			return pagingInfo != null && (this.PageSize == pagingInfo.PageSize && this.SortColumn.Equals(pagingInfo.SortColumn) && this.AscendingSort == pagingInfo.AscendingSort && this.direction == pagingInfo.Direction && PagingInfo.SameSet(this.DataColumns, pagingInfo.DataColumns) && (this.referenceItem == null || this.referenceItem.Equals(pagingInfo.SortValue))) && (this.referenceItem != null || pagingInfo.SortValue == null);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003D4DB File Offset: 0x0003B6DB
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003D4E4 File Offset: 0x0003B6E4
		private static bool SameSet(List<PropertyDefinition> left, List<PropertyDefinition> right)
		{
			if (left.Count != right.Count)
			{
				return false;
			}
			for (int i = 0; i < left.Count; i++)
			{
				if (!left[i].Equals(right[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0003D558 File Offset: 0x0003B758
		private static bool ValidateDataColumns(List<PropertyDefinition> data)
		{
			PagingInfo.<>c__DisplayClass1 CS$<>8__locals1 = new PagingInfo.<>c__DisplayClass1();
			CS$<>8__locals1.data = data;
			if (CS$<>8__locals1.data == null)
			{
				return false;
			}
			int i;
			for (i = 0; i < CS$<>8__locals1.data.Count; i++)
			{
				if (!Array.Exists<PropertyDefinition>(PagingInfo.allowedDataProperties, (PropertyDefinition obj) => CS$<>8__locals1.data[i].Equals(obj)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400099B RID: 2459
		private static PropertyDefinition[] allowedDataProperties = new PropertyDefinition[]
		{
			StoreObjectSchema.ParentItemId,
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ItemSchema.HasAttachment,
			ItemSchema.Size,
			ItemSchema.BodyTag,
			ItemSchema.InternetMessageId,
			ItemSchema.ConversationId,
			ItemSchema.ConversationTopic,
			ItemSchema.ConversationIndexTracking,
			ItemSchema.Subject,
			MessageItemSchema.IsRead,
			ItemSchema.SentTime,
			ItemSchema.ReceivedTime,
			MessageItemSchema.SenderDisplayName,
			MessageItemSchema.SenderSmtpAddress,
			ItemSchema.Importance,
			ItemSchema.Categories,
			ItemSchema.DisplayCc,
			ItemSchema.DisplayBcc,
			ItemSchema.DisplayTo,
			StoreObjectSchema.CreationTime
		};

		// Token: 0x0400099C RID: 2460
		private static readonly PropertyDefinition[] RequiredDataPropertiesFromStore = new PropertyDefinition[]
		{
			StoreObjectSchema.ParentItemId,
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ItemSchema.Size,
			ItemSchema.DocumentId,
			ItemSchema.InternetMessageId,
			ItemSchema.ConversationId,
			ItemSchema.ConversationTopic,
			ItemSchema.BodyTag
		};

		// Token: 0x0400099D RID: 2461
		private readonly List<PropertyDefinition> originalDataColumns;

		// Token: 0x0400099E RID: 2462
		private readonly List<PropertyDefinition> dataColumns;

		// Token: 0x0400099F RID: 2463
		private readonly SortBy sort;

		// Token: 0x040009A0 RID: 2464
		private readonly SortBy[] sorts;

		// Token: 0x040009A1 RID: 2465
		private readonly int pageSize;

		// Token: 0x040009A2 RID: 2466
		private readonly ReferenceItem referenceItem;

		// Token: 0x040009A3 RID: 2467
		private readonly PageDirection direction;

		// Token: 0x040009A4 RID: 2468
		private readonly ExTimeZone timeZone;

		// Token: 0x040009A5 RID: 2469
		private readonly bool excludeDuplicateItems;

		// Token: 0x040009A6 RID: 2470
		private readonly PreviewItemBaseShape baseShape;

		// Token: 0x040009A7 RID: 2471
		private readonly List<ExtendedPropertyInfo> additionalProperties;
	}
}
