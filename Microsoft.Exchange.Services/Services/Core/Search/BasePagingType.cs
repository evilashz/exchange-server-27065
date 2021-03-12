using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200025C RID: 604
	[XmlInclude(typeof(IndexedPageView))]
	[KnownType(typeof(FractionalPageView))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(CalendarPageView))]
	[XmlInclude(typeof(SeekToConditionPageView))]
	[KnownType(typeof(SeekToConditionWithOffsetPageView))]
	[XmlInclude(typeof(CalendarPageView))]
	[XmlInclude(typeof(ContactsPageView))]
	[XmlInclude(typeof(FractionalPageView))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(ContactsPageView))]
	[KnownType(typeof(IndexedPageView))]
	[KnownType(typeof(SeekToConditionPageView))]
	[Serializable]
	public abstract class BasePagingType
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0004D003 File Offset: 0x0004B203
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x0004D019 File Offset: 0x0004B219
		[XmlAttribute(AttributeName = "MaxEntriesReturned")]
		[DataMember(Name = "MaxEntriesReturned", IsRequired = false)]
		public int MaxRows
		{
			get
			{
				if (!this.MaxRowsSpecified)
				{
					return int.MaxValue;
				}
				return this.maxRows;
			}
			set
			{
				this.maxRowsSpecified = true;
				this.maxRows = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0004D029 File Offset: 0x0004B229
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x0004D031 File Offset: 0x0004B231
		[XmlIgnore]
		public bool MaxRowsSpecified
		{
			get
			{
				return this.maxRowsSpecified;
			}
			set
			{
				this.maxRowsSpecified = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0004D03A File Offset: 0x0004B23A
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x0004D042 File Offset: 0x0004B242
		[IgnoreDataMember]
		[XmlIgnore]
		public int RowsFetched
		{
			get
			{
				return this.rowsFetched;
			}
			set
			{
				this.rowsFetchedSpecified = true;
				this.rowsFetched = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0004D052 File Offset: 0x0004B252
		[IgnoreDataMember]
		[XmlIgnore]
		public bool RowsFetchedSpecified
		{
			get
			{
				return this.rowsFetchedSpecified;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0004D05A File Offset: 0x0004B25A
		// (set) Token: 0x06000FCF RID: 4047 RVA: 0x0004D062 File Offset: 0x0004B262
		[XmlIgnore]
		[IgnoreDataMember]
		public bool LoadPartialPageRows { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0004D06B File Offset: 0x0004B26B
		// (set) Token: 0x06000FD1 RID: 4049 RVA: 0x0004D073 File Offset: 0x0004B273
		[IgnoreDataMember]
		[XmlIgnore]
		public bool NoRowCountRetrieval { get; set; }

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0004D07C File Offset: 0x0004B27C
		internal static BasePageResult ApplyPostQueryPaging(IQueryResult queryResult, BasePagingType paging)
		{
			if (paging == null)
			{
				return new BasePageResult(new NormalQueryView(queryResult, int.MaxValue, null));
			}
			return paging.ApplyPostQueryPaging(queryResult);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0004D09C File Offset: 0x0004B29C
		internal static BasePageResult ApplyPostQueryGroupedPaging(GroupedQueryResult groupedResult, BasePagingType paging, int groupByPropDefIndex)
		{
			if (paging == null)
			{
				GroupedQueryView view = new GroupedQueryView(groupedResult, int.MaxValue, groupByPropDefIndex, paging);
				return new BasePageResult(view);
			}
			return paging.ApplyPostQueryGroupedPaging(groupedResult, groupByPropDefIndex);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0004D0C9 File Offset: 0x0004B2C9
		internal static QueryFilter ApplyQueryAppend(QueryFilter filter, BasePagingType paging)
		{
			if (paging == null)
			{
				return filter;
			}
			return paging.ApplyQueryAppend(filter);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0004D0D7 File Offset: 0x0004B2D7
		internal static void Validate(BasePagingType paging)
		{
			if (paging == null)
			{
				return;
			}
			if (paging.MaxRowsSpecified && paging.MaxRows <= 0)
			{
				throw new InvalidPagingMaxRowsException();
			}
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0004D0F4 File Offset: 0x0004B2F4
		internal virtual BasePageResult ApplyPostQueryPaging(IQueryResult queryResult)
		{
			this.PositionResultSet(queryResult);
			return this.CreatePageResult(queryResult, new NormalQueryView(queryResult, this.MaxRows, this));
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0004D114 File Offset: 0x0004B314
		internal virtual BasePageResult ApplyPostQueryGroupedPaging(GroupedQueryResult groupedQuery, int groupByPropDefIndex)
		{
			this.PositionResultSet(groupedQuery);
			GroupedQueryView view = new GroupedQueryView(groupedQuery, this.MaxRows, groupByPropDefIndex, this);
			return this.CreatePageResult(groupedQuery, view);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0004D13F File Offset: 0x0004B33F
		internal virtual QueryFilter ApplyQueryAppend(QueryFilter filter)
		{
			return filter;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0004D142 File Offset: 0x0004B342
		internal virtual void PositionResultSet(IQueryResult queryResult)
		{
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0004D144 File Offset: 0x0004B344
		internal virtual BasePageResult CreatePageResult(IQueryResult queryResult, BaseQueryView view)
		{
			return new BasePageResult(view);
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0004D14C File Offset: 0x0004B34C
		internal virtual bool BudgetInducedTruncationAllowed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000BDA RID: 3034
		private int maxRows;

		// Token: 0x04000BDB RID: 3035
		private bool maxRowsSpecified;

		// Token: 0x04000BDC RID: 3036
		private int rowsFetched;

		// Token: 0x04000BDD RID: 3037
		private bool rowsFetchedSpecified;

		// Token: 0x0200025D RID: 605
		[XmlType(TypeName = "BasePointType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[Serializable]
		public enum PagingOrigin
		{
			// Token: 0x04000BE1 RID: 3041
			Beginning,
			// Token: 0x04000BE2 RID: 3042
			End = 6
		}
	}
}
