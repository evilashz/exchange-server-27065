using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000153 RID: 339
	public abstract class ViewTable
	{
		// Token: 0x06000CFD RID: 3325 RVA: 0x00041503 File Offset: 0x0003F703
		public ViewTable(Mailbox mailbox, Table table)
		{
			this.mailbox = mailbox;
			this.table = table;
			this.bookmark = Bookmark.BOT;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00041524 File Offset: 0x0003F724
		public ViewTable(Mailbox mailbox, TableFunction tableFunction, object[] tableFunctionParameters)
		{
			this.mailbox = mailbox;
			this.table = tableFunction;
			this.bookmark = Bookmark.BOT;
			this.tableFunctionParameters = tableFunctionParameters;
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0004154C File Offset: 0x0003F74C
		public Mailbox Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00041554 File Offset: 0x0003F754
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0004155C File Offset: 0x0003F75C
		public Bookmark Bookmark
		{
			get
			{
				return this.bookmark;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00041564 File Offset: 0x0003F764
		public SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0004156C File Offset: 0x0003F76C
		public virtual ViewTable QueryRowsViewTable
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0004156F File Offset: 0x0003F76F
		public int CategoryCount
		{
			get
			{
				return this.categoryCount;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00041577 File Offset: 0x0003F777
		public CategorizedTableCollapseState CollapseState
		{
			get
			{
				return this.collapseState;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0004157F File Offset: 0x0003F77F
		public SortOrder CategoryHeadersSortOrder
		{
			get
			{
				return this.categoryHeadersSortOrder;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00041587 File Offset: 0x0003F787
		public CategoryHeaderSortOverride[] CategoryHeaderSortOverrides
		{
			get
			{
				return this.categoryHeaderSortOverrides;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0004158F File Offset: 0x0003F78F
		public bool IsCategorizedView
		{
			get
			{
				return this.categoryCount > 0;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0004159A File Offset: 0x0003F79A
		public virtual IList<Column> LongValueColumnsToPreread
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0004159D File Offset: 0x0003F79D
		internal IList<Column> ViewColumns
		{
			get
			{
				return this.viewColumns;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x000415A5 File Offset: 0x0003F7A5
		internal SearchCriteria ImplicitCriteria
		{
			get
			{
				return this.implicitCriteria;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x000415AD File Offset: 0x0003F7AD
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x000415B5 File Offset: 0x0003F7B5
		internal SearchCriteria RestrictCriteria
		{
			get
			{
				return this.restrictCriteria;
			}
			set
			{
				this.restrictCriteria = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x000415BE File Offset: 0x0003F7BE
		protected bool RowCountValid
		{
			get
			{
				return this.rowCountValid;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x000415C6 File Offset: 0x0003F7C6
		protected int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x000415CE File Offset: 0x0003F7CE
		protected virtual Index LogicalKeyIndex
		{
			get
			{
				return this.table.PrimaryKeyIndex;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x000415DB File Offset: 0x0003F7DB
		protected virtual Dictionary<Column, FilterFactorHint> FilterFactorHints
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x000415DE File Offset: 0x0003F7DE
		protected bool MvExplosion
		{
			get
			{
				return this.multiValueInstanceColumn != null;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x000415EC File Offset: 0x0003F7EC
		protected virtual bool MustUseLazyIndex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000415EF File Offset: 0x0003F7EF
		public static bool ClientTypeExcludedFromDefaultPromotedValidation(ClientType clientType)
		{
			return clientType == ClientType.MoMT || clientType == ClientType.WebServices || clientType == ClientType.Pop || clientType == ClientType.Imap || clientType == ClientType.AirSync || clientType == ClientType.User || clientType == ClientType.LoadGen;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00041620 File Offset: 0x0003F820
		public virtual void Reset()
		{
			this.viewColumns = null;
			this.restrictCriteria = null;
			this.sortOrder = this.LogicalKeyIndex.SortOrder;
			this.categoryCount = 0;
			this.collapseState = null;
			this.categoryHeadersSortOrder = SortOrder.Empty;
			this.categoryHeaderSortOverrides = null;
			this.InvalidateBookmarkAndRowCount();
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00041672 File Offset: 0x0003F872
		public void SetColumns(Context context, IList<Column> columns)
		{
			this.SetColumns(context, columns, ViewSetColumnsFlag.None);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0004167D File Offset: 0x0003F87D
		public virtual void SetColumns(Context context, IList<Column> columns, ViewSetColumnsFlag flags)
		{
			this.viewColumns = columns;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00041688 File Offset: 0x0003F888
		public virtual void Restrict(Context context, SearchCriteria restrictCriteria)
		{
			if (restrictCriteria == Factory.CreateSearchCriteriaTrue())
			{
				this.restrictCriteria = null;
			}
			else if (this.IsCategorizedView && restrictCriteria != null)
			{
				if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.TraceDebug<int>(0L, "Mailbox {0}: Don't currently support restricting a categorized view, so the restriction request will be ignored.", this.Mailbox.MailboxNumber);
				}
				this.restrictCriteria = null;
			}
			else
			{
				this.restrictCriteria = this.RewriteSearchCriteria(context, restrictCriteria);
			}
			this.InvalidateBookmarkAndRowCount();
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000416F8 File Offset: 0x0003F8F8
		public virtual void SortTable(SortOrder sortOrder)
		{
			SortOrderBuilder sortOrderBuilder = new SortOrderBuilder(sortOrder);
			bool ascending = sortOrder.Count == 0 || sortOrder[0].Ascending;
			if (this.LogicalKeyIndex != null)
			{
				IList<Column> columns = this.LogicalKeyIndex.Columns;
				for (int i = 0; i < columns.Count; i++)
				{
					Column column = columns[i];
					if (!sortOrderBuilder.Contains(column))
					{
						sortOrderBuilder.Add(column, ascending);
					}
				}
			}
			Column col = null;
			foreach (SortColumn sortColumn in ((IEnumerable<SortColumn>)sortOrderBuilder))
			{
				if (PropertySchema.IsMultiValueInstanceColumn(sortColumn.Column))
				{
					col = (sortColumn.Column as ExtendedPropertyColumn);
					Column column2 = PropertySchema.MapToColumn(this.Mailbox.Database, ObjectType.Message, PropTag.Message.InstanceNum);
					if (!sortOrderBuilder.Contains(column2))
					{
						sortOrderBuilder.Add(column2, ascending);
						break;
					}
					break;
				}
			}
			this.Uncategorize();
			this.sortOrder = sortOrderBuilder.ToSortOrder();
			if (col != this.multiValueInstanceColumn)
			{
				this.rowCountValid = false;
				this.multiValueInstanceColumn = col;
			}
			this.bookmark = Bookmark.BOT;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0004182C File Offset: 0x0003FA2C
		public virtual void Categorize(Context context, int categoryCount, int expandedCount, CategoryHeaderSortOverride[] categoryHeaderSortOverrides)
		{
			throw new StoreException((LID)50872U, ErrorCodeValue.NotSupported, "This view type does not support categorized views.");
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00041847 File Offset: 0x0003FA47
		public virtual int CollapseRow(Context context, ExchangeId categoryId)
		{
			throw new StoreException((LID)41759U, ErrorCodeValue.Leaf, "This view type only contains leaf rows, so there are no header rows to collapse.");
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00041862 File Offset: 0x0003FA62
		public virtual int ExpandRow(Context context, ExchangeId categoryId)
		{
			throw new StoreException((LID)58143U, ErrorCodeValue.Leaf, "This view type only contains leaf rows, so there are no header rows to expand.");
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0004187D File Offset: 0x0003FA7D
		public virtual byte[] GetCollapseState(Context context, ExchangeId rowInstanceId, int rowInstanceNumber)
		{
			throw new StoreException((LID)40832U, ErrorCodeValue.Leaf, "This view type only contains leaf rows.");
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00041898 File Offset: 0x0003FA98
		public virtual byte[] SetCollapseState(Context context, byte[] collapseState)
		{
			throw new StoreException((LID)57216U, ErrorCodeValue.Leaf, "This view type only contains leaf rows.");
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000418B3 File Offset: 0x0003FAB3
		public byte[] CreateExternalBookmark()
		{
			return ViewTable.SerializeBookmark(this.GetSortOrderForView(), this.bookmark);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000418C6 File Offset: 0x0003FAC6
		public void FreeExternalBookmark(byte[] bookmark)
		{
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000418C8 File Offset: 0x0003FAC8
		public int GetPosition(Context context)
		{
			if (!this.bookmark.PositionValid)
			{
				if (this.bookmark.IsEOT)
				{
					return this.GetRowCount(context);
				}
				if (this.IsViewEmpty(context))
				{
					return 0;
				}
				using (OrdinalPositionOperator ordinalPositionOperator = this.GetOrdinalPositionOperator(context))
				{
					int position = (int)ordinalPositionOperator.ExecuteScalar();
					this.bookmark = new Bookmark(this.bookmark.KeyValues, this.bookmark.PositionedOn, position);
				}
			}
			return this.bookmark.Position;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00041960 File Offset: 0x0003FB60
		public int? GetCachedPosition()
		{
			if (!this.bookmark.PositionValid)
			{
				return null;
			}
			return new int?(this.bookmark.Position);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00041994 File Offset: 0x0003FB94
		public void SeekRow(Context context, ViewSeekOrigin origin, int numberOfRows)
		{
			bool flag;
			int num;
			bool flag2;
			this.SeekRow(context, origin, null, numberOfRows, false, out flag, out num, false, out flag2);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000419B4 File Offset: 0x0003FBB4
		public virtual void SeekRow(Context context, ViewSeekOrigin origin, byte[] bookmark, int numberOfRows, bool wantSoughtRowCount, out bool soughtLessThanRowCount, out int rowCountActuallySought, bool validateBookmarkPosition, out bool bookmarkPositionChanged)
		{
			soughtLessThanRowCount = false;
			rowCountActuallySought = 0;
			bookmarkPositionChanged = false;
			int num = numberOfRows;
			Bookmark bookmark2;
			if (origin == ViewSeekOrigin.Beginning)
			{
				bookmark2 = Bookmark.BOT;
				if (num > 0 && this.bookmark.PositionValid)
				{
					int num2 = num - this.bookmark.Position;
					int num3 = (num2 >= 0) ? num2 : (-num2);
					if (num3 < num && this.optimizedSeeks++ % 32 != 0)
					{
						bookmark2 = this.bookmark;
						num = num2;
					}
				}
			}
			else if (origin == ViewSeekOrigin.End)
			{
				bookmark2 = Bookmark.EOT;
			}
			else if (origin == ViewSeekOrigin.Current)
			{
				bookmark2 = this.bookmark;
			}
			else
			{
				if (origin != ViewSeekOrigin.Bookmark)
				{
					throw new NotSupportedException("SeekRow does not support this origin");
				}
				bookmark2 = ViewTable.DeserializeBookmark(this.GetSortOrderForView(), bookmark);
			}
			if (num == -2147483648)
			{
				num++;
			}
			if (bookmark2.IsBOT)
			{
				if (num <= 0)
				{
					this.bookmark = Bookmark.BOT;
					if (num < 0)
					{
						soughtLessThanRowCount = true;
						rowCountActuallySought = 0;
						return;
					}
				}
				else
				{
					this.DoSeekQuery(context, bookmark2, false, num, new int?(num), out soughtLessThanRowCount, out rowCountActuallySought);
					if (soughtLessThanRowCount)
					{
						this.rowCount = rowCountActuallySought;
						this.rowCountValid = true;
						return;
					}
				}
			}
			else if (bookmark2.IsEOT)
			{
				if (num < 0)
				{
					int? newPosition = null;
					if (this.rowCountValid)
					{
						newPosition = new int?(this.rowCount + num);
					}
					this.DoSeekQuery(context, bookmark2, true, -num, newPosition, out soughtLessThanRowCount, out rowCountActuallySought);
					if (soughtLessThanRowCount)
					{
						this.rowCount = rowCountActuallySought;
						this.rowCountValid = true;
					}
					rowCountActuallySought = -rowCountActuallySought;
					return;
				}
				this.bookmark = Bookmark.EOT;
				if (num > 0)
				{
					soughtLessThanRowCount = true;
					rowCountActuallySought = 0;
					return;
				}
			}
			else
			{
				if (validateBookmarkPosition)
				{
					bookmarkPositionChanged = !this.CheckBookmarkPosition(context, bookmark2);
				}
				if (num == 0)
				{
					this.bookmark = bookmark2;
				}
				else if (bookmark2.PositionedOn && num == 1)
				{
					if (bookmark2.PositionValid)
					{
						this.bookmark = new Bookmark(bookmark2.KeyValues, false, bookmark2.Position + 1);
					}
					else
					{
						this.bookmark = new Bookmark(bookmark2.KeyValues, false);
					}
					rowCountActuallySought = 1;
				}
				else if (!bookmark2.PositionedOn && num == -1)
				{
					if (bookmark2.PositionValid)
					{
						this.bookmark = new Bookmark(bookmark2.KeyValues, true, bookmark2.Position - 1);
					}
					else
					{
						this.bookmark = new Bookmark(bookmark2.KeyValues, true);
					}
					rowCountActuallySought = -1;
				}
				else
				{
					bool flag = num < 0;
					int rowsToSeek = flag ? (-num) : num;
					int? newPosition2 = null;
					if (bookmark2.PositionValid)
					{
						newPosition2 = new int?(bookmark2.Position + num);
					}
					this.DoSeekQuery(context, bookmark2, flag, rowsToSeek, newPosition2, out soughtLessThanRowCount, out rowCountActuallySought);
					if (flag)
					{
						rowCountActuallySought = -rowCountActuallySought;
					}
				}
				if (origin == ViewSeekOrigin.Beginning && wantSoughtRowCount)
				{
					if (soughtLessThanRowCount)
					{
						this.DoSeekQuery(context, Bookmark.BOT, false, numberOfRows, new int?(numberOfRows), out soughtLessThanRowCount, out rowCountActuallySought);
						if (soughtLessThanRowCount)
						{
							this.rowCount = rowCountActuallySought;
							this.rowCountValid = true;
							return;
						}
					}
					else
					{
						rowCountActuallySought = numberOfRows;
					}
				}
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00041C88 File Offset: 0x0003FE88
		public virtual Reader QueryRows(Context context, int rowCount, bool backwards)
		{
			SimpleQueryOperator simpleQueryOperator = this.GetQueryRowsOperator(context, rowCount, backwards);
			if (simpleQueryOperator == null)
			{
				return null;
			}
			Reader result;
			try
			{
				Reader reader = simpleQueryOperator.ExecuteReader(true);
				simpleQueryOperator = null;
				result = reader;
			}
			finally
			{
				if (simpleQueryOperator != null)
				{
					simpleQueryOperator.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00041CD0 File Offset: 0x0003FED0
		public bool FindRow(Context context, SearchCriteria criteria, ViewSeekOrigin origin, bool backwards)
		{
			bool flag;
			bool result;
			using (Reader reader = this.FindRow(context, criteria, origin, null, backwards, out flag))
			{
				result = (reader != null);
			}
			return result;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00041D14 File Offset: 0x0003FF14
		public virtual Reader FindRow(Context context, SearchCriteria criteria, ViewSeekOrigin origin, byte[] bookmark, bool backwards, out bool bookmarkPositionChanged)
		{
			bookmarkPositionChanged = false;
			Bookmark bookmark2;
			switch (origin)
			{
			case ViewSeekOrigin.Beginning:
				bookmark2 = Bookmark.BOT;
				break;
			case ViewSeekOrigin.Current:
				bookmark2 = this.bookmark;
				break;
			case ViewSeekOrigin.End:
				bookmark2 = Bookmark.EOT;
				break;
			case ViewSeekOrigin.Bookmark:
				bookmark2 = ViewTable.DeserializeBookmark(this.GetSortOrderForView(), bookmark);
				break;
			default:
				throw new NotSupportedException("bookmark origin is not supported");
			}
			if (!bookmark2.IsBOT && !bookmark2.IsEOT)
			{
				bookmarkPositionChanged = !this.CheckBookmarkPosition(context, bookmark2);
			}
			if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug(0L, "{0} FindRow:  view:[{1}]  categoryCount:{2}  implicitCriteria:[{3}]  restrictCriteria:[{4}]  findRowCriteria:[{5}]  startBookmark:[{6}], backwards:{7}", new object[]
				{
					base.GetType().Name,
					this.GetSortOrderForView(),
					this.CategoryCount,
					this.implicitCriteria,
					this.restrictCriteria,
					criteria,
					bookmark2,
					backwards
				});
			}
			return this.FindRow(context, criteria, bookmark2, backwards);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00041E18 File Offset: 0x00040018
		public virtual int GetRowCount(Context context)
		{
			if (!this.rowCountValid)
			{
				this.rowCount = 0;
				if (!this.IsViewEmpty(context))
				{
					using (CountOperator countOperator = this.GetCountOperator(context))
					{
						this.rowCount = (int)countOperator.ExecuteScalar();
					}
				}
				this.rowCountValid = true;
			}
			return this.rowCount;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00041E80 File Offset: 0x00040080
		public virtual bool NeedIndexForPositionOrRowCount(Context context)
		{
			return false;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00041E83 File Offset: 0x00040083
		public virtual IChunked PrepareIndexes(Context context, SearchCriteria findRowCriteria)
		{
			return null;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00041E88 File Offset: 0x00040088
		public virtual void ForceReload(Context context, bool invalidateRowCount)
		{
			if (invalidateRowCount)
			{
				this.rowCountValid = false;
			}
			if (this.bookmark.PositionValid && !this.bookmark.IsBOT)
			{
				this.bookmark = new Bookmark(this.bookmark.KeyValues, this.bookmark.PositionedOn);
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00041EDC File Offset: 0x000400DC
		public void BookmarkCurrentRow(Reader reader, bool positionedOn)
		{
			this.bookmark = new Bookmark(this.GetSortOrderForView(), reader, positionedOn, null);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00041F05 File Offset: 0x00040105
		public void BookmarkCurrentRow(Reader reader, bool positionedOn, int? position)
		{
			this.bookmark = new Bookmark(this.GetSortOrderForView(), reader, positionedOn, position);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00041F1B File Offset: 0x0004011B
		public void SaveLastBookmark()
		{
			this.lastBookmark = this.bookmark;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00041F29 File Offset: 0x00040129
		public void RevertToLastBookmark()
		{
			this.bookmark = this.lastBookmark;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00041F38 File Offset: 0x00040138
		public bool CheckBookmarkMatchesRow(Context context, Reader reader, Bookmark expectedBookmark, bool checkHeaderBookmarkOnly)
		{
			CompareInfo compareInfo = (context.Culture == null) ? null : context.Culture.CompareInfo;
			Bookmark bookmark = new Bookmark(this.GetSortOrderForView(), reader, expectedBookmark.PositionedOn, null);
			bool result = false;
			if (checkHeaderBookmarkOnly && this.IsCategorizedView)
			{
				if (expectedBookmark.KeyValues.Count == bookmark.KeyValues.Count)
				{
					result = true;
					for (int i = 0; i < this.CategoryHeadersSortOrder.Count; i++)
					{
						if (!ValueHelper.ValuesEqual(expectedBookmark.KeyValues[i], bookmark.KeyValues[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
						{
							result = false;
							break;
						}
					}
				}
			}
			else
			{
				result = ValueHelper.ListsEqual(expectedBookmark.KeyValues, bookmark.KeyValues, compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
			}
			return result;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00042004 File Offset: 0x00040204
		public bool IsHeaderRowBookmark(Bookmark bookmark)
		{
			bool result = false;
			if (this.IsCategorizedView)
			{
				int num = (int)bookmark.KeyValues[this.categoryHeadersSortOrder.Count - 1];
				if (num < this.CategoryCount)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00042048 File Offset: 0x00040248
		public bool IsLeafRowBookmark(Bookmark bookmark)
		{
			bool result = false;
			if (this.IsCategorizedView)
			{
				int num = (int)bookmark.KeyValues[this.categoryHeadersSortOrder.Count - 1];
				if (num == this.CategoryCount)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0004208A File Offset: 0x0004028A
		internal static IDisposable SetFindRowTestHook(Action action)
		{
			return ViewTable.findRowTestHook.SetTestHook(action);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00042097 File Offset: 0x00040297
		internal static IDisposable SetFindRowOperatorTestHook(Action<SimpleQueryOperator, int, int> action)
		{
			return ViewTable.findRowOperatorTestHook.SetTestHook(action);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000420A4 File Offset: 0x000402A4
		internal static byte[] SerializeBookmark(SortOrder sortOrder, Bookmark bookmark)
		{
			int num = 4;
			if (!bookmark.IsBOT && !bookmark.IsEOT)
			{
				num += 5 + SerializedValue.ComputeSize(bookmark.KeyValues);
			}
			byte[] array = new byte[num];
			if (bookmark.IsBOT)
			{
				ParseSerialize.SerializeInt32(173305666, array, 0);
			}
			else if (bookmark.IsEOT)
			{
				ParseSerialize.SerializeInt32(173305669, array, 0);
			}
			else
			{
				ParseSerialize.SerializeInt32(175727947, array, 0);
				ParseSerialize.SerializeInt32(sortOrder.GetHashCode(), array, 4);
				array[8] = (bookmark.PositionedOn ? 1 : 0);
				int num2 = 9;
				SerializedValue.Serialize(bookmark.KeyValues, array, ref num2);
			}
			return array;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00042154 File Offset: 0x00040354
		internal static Bookmark DeserializeBookmark(SortOrder sortOrder, byte[] serializedBookmark)
		{
			if (serializedBookmark != null && serializedBookmark.Length >= 4)
			{
				int num = ParseSerialize.ParseInt32(serializedBookmark, 0);
				if (num == 173305666)
				{
					if (serializedBookmark.Length != 4)
					{
						throw new StoreException((LID)32888U, ErrorCodeValue.InvalidParameter);
					}
					return Bookmark.BOT;
				}
				else if (num == 173305669)
				{
					if (serializedBookmark.Length != 4)
					{
						throw new StoreException((LID)41080U, ErrorCodeValue.InvalidParameter);
					}
					return Bookmark.EOT;
				}
				else if (num == 175727947)
				{
					if (serializedBookmark.Length < 9)
					{
						throw new StoreException((LID)57464U, ErrorCodeValue.InvalidParameter);
					}
					int num2 = ParseSerialize.ParseInt32(serializedBookmark, 4);
					bool positionedOn = 0 != serializedBookmark[8];
					int num3 = 9;
					IList<object> list;
					if (!SerializedValue.TryParseList(serializedBookmark, ref num3, out list))
					{
						throw new StoreException((LID)49272U, ErrorCodeValue.InvalidParameter);
					}
					if (num2 != sortOrder.GetHashCode() || list == null || list.Count != sortOrder.Count)
					{
						throw new StoreException((LID)48760U, ErrorCodeValue.InvalidBookmark);
					}
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i] != null && list[i].GetType() != sortOrder[i].Column.Type)
						{
							throw new StoreException((LID)65144U, ErrorCodeValue.InvalidBookmark);
						}
					}
					return new Bookmark(list, positionedOn);
				}
			}
			throw new StoreException((LID)40568U, ErrorCodeValue.InvalidParameter);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000422E0 File Offset: 0x000404E0
		protected internal IList<IIndex> GetInScopePseudoIndexes(Context context, SearchCriteria findRowCriteria)
		{
			IList<IIndex> list;
			IList<IIndex> inScopePseudoIndexes = this.GetInScopePseudoIndexes(context, findRowCriteria, out list);
			if (list == null)
			{
				return inScopePseudoIndexes;
			}
			List<IIndex> list2 = new List<IIndex>(list);
			list2.AddRange(inScopePseudoIndexes);
			return list2;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0004230C File Offset: 0x0004050C
		protected internal virtual IList<IIndex> GetInScopePseudoIndexes(Context context, SearchCriteria findRowCriteria, out IList<IIndex> masterIndexes)
		{
			masterIndexes = null;
			return null;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00042314 File Offset: 0x00040514
		internal CountOperator GetCountOperator(Context context)
		{
			IList<IIndex> list;
			IList<IIndex> inScopePseudoIndexes = this.GetInScopePseudoIndexes(context, null, out list);
			QueryPlanner queryPlanner = new QueryPlanner(context, this.Table, this.tableFunctionParameters, this.implicitCriteria, this.restrictCriteria, null, null, null, this.GetColumnRenames(context), this.GetCategorizedQueryParams(context), inScopePseudoIndexes, list, SortOrder.Empty, Bookmark.BOT, 0, 0, false, this.MustUseLazyIndex, false, true, false, list == null, (this.FilterFactorHints == null) ? QueryPlanner.Hints.Empty : new QueryPlanner.Hints
			{
				FilterFactorHints = this.FilterFactorHints
			});
			CountOperator result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CountOperator countOperator = disposeGuard.Add<CountOperator>(queryPlanner.CreateCountPlan());
				if (list != null)
				{
					this.BringIndexesToCurrent(context, list, countOperator);
				}
				this.BringIndexesToCurrent(context, inScopePseudoIndexes, countOperator);
				disposeGuard.Success();
				result = countOperator;
			}
			return result;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000423F8 File Offset: 0x000405F8
		internal OrdinalPositionOperator GetOrdinalPositionOperator(Context context)
		{
			IList<IIndex> list;
			IList<IIndex> inScopePseudoIndexes = this.GetInScopePseudoIndexes(context, null, out list);
			QueryPlanner queryPlanner = new QueryPlanner(context, this.Table, this.tableFunctionParameters, this.implicitCriteria, this.restrictCriteria, null, null, null, this.GetColumnRenames(context), this.GetCategorizedQueryParams(context), inScopePseudoIndexes, list, this.GetSortOrderForView(), this.Bookmark, 0, 0, false, this.MustUseLazyIndex, false, true, false, list == null, (this.FilterFactorHints == null) ? QueryPlanner.Hints.Empty : new QueryPlanner.Hints
			{
				FilterFactorHints = this.FilterFactorHints
			});
			OrdinalPositionOperator result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				OrdinalPositionOperator ordinalPositionOperator = disposeGuard.Add<OrdinalPositionOperator>(queryPlanner.CreateOrdinalPositionPlan());
				if (list != null)
				{
					this.BringIndexesToCurrent(context, list, ordinalPositionOperator);
				}
				this.BringIndexesToCurrent(context, inScopePseudoIndexes, ordinalPositionOperator);
				disposeGuard.Success();
				result = ordinalPositionOperator;
			}
			return result;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000424E0 File Offset: 0x000406E0
		internal SimpleQueryOperator GetQueryRowsOperator(Context context, int maxRows, bool backwards)
		{
			SimpleQueryOperator simpleQueryOperator = null;
			IList<IIndex> list = null;
			IList<IIndex> list2 = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				if (!this.IsViewEmpty(context))
				{
					list = this.GetInScopePseudoIndexes(context, null, out list2);
					QueryPlanner queryPlanner = new QueryPlanner(context, this.Table, this.tableFunctionParameters, this.implicitCriteria, this.restrictCriteria, null, this.ViewColumns, this.LongValueColumnsToPreread, this.GetColumnRenames(context), this.GetCategorizedQueryParams(context), list, list2, this.GetSortOrderForView(), this.bookmark, 0, maxRows, backwards, this.MustUseLazyIndex, false, true, false, list2 == null, (this.FilterFactorHints == null) ? QueryPlanner.Hints.Empty : new QueryPlanner.Hints
					{
						FilterFactorHints = this.FilterFactorHints
					});
					simpleQueryOperator = disposeGuard.Add<SimpleQueryOperator>(queryPlanner.CreatePlan());
				}
				if (simpleQueryOperator != null)
				{
					if (list2 != null)
					{
						this.BringIndexesToCurrent(context, list2, simpleQueryOperator);
					}
					this.BringIndexesToCurrent(context, list, simpleQueryOperator);
				}
				disposeGuard.Success();
			}
			return simpleQueryOperator;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000425E0 File Offset: 0x000407E0
		internal SimpleQueryOperator GetFindRowOperator(Context context, Bookmark startBookmark, SearchCriteria findRowCriteria, bool backwards)
		{
			int num;
			int num2;
			return this.GetFindRowOperator(context, startBookmark, findRowCriteria, backwards, out num, out num2);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000425FC File Offset: 0x000407FC
		internal SimpleQueryOperator GetFindRowOperator(Context context, Bookmark startBookmark, SearchCriteria findRowCriteria, bool backwards, out int planCost, out int planCardinality)
		{
			planCost = 0;
			planCardinality = 0;
			SimpleQueryOperator simpleQueryOperator = null;
			IList<IIndex> list = null;
			IList<IIndex> list2 = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				if (!this.IsViewEmpty(context))
				{
					findRowCriteria = this.RewriteSearchCriteria(context, findRowCriteria);
					if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug<SearchCriteria>(0L, "FindRow: Generating FindRow query operator for criteria [{0}].", findRowCriteria);
					}
					list = this.GetInScopePseudoIndexes(context, findRowCriteria, out list2);
					QueryPlanner queryPlanner = new QueryPlanner(context, this.Table, this.tableFunctionParameters, this.implicitCriteria, this.restrictCriteria, findRowCriteria, this.ViewColumns, null, this.GetColumnRenames(context), this.GetCategorizedQueryParams(context), list, list2, this.GetSortOrderForView(), startBookmark, 0, 1, backwards, this.MustUseLazyIndex, false, true, false, list2 == null, (this.FilterFactorHints == null) ? QueryPlanner.Hints.Empty : new QueryPlanner.Hints
					{
						FilterFactorHints = this.FilterFactorHints
					});
					simpleQueryOperator = disposeGuard.Add<SimpleQueryOperator>(queryPlanner.CreatePlan(out planCost, out planCardinality));
				}
				if (simpleQueryOperator != null)
				{
					if (list2 != null)
					{
						this.BringIndexesToCurrent(context, list2, simpleQueryOperator);
					}
					this.BringIndexesToCurrent(context, list, simpleQueryOperator);
				}
				disposeGuard.Success();
			}
			if (ViewTable.findRowOperatorTestHook.Value != null)
			{
				ViewTable.findRowOperatorTestHook.Value(simpleQueryOperator, planCost, planCardinality);
			}
			return simpleQueryOperator;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0004274C File Offset: 0x0004094C
		internal void ResetCollapseStateForTest(int expandedCount)
		{
			this.collapseState = new CategorizedTableCollapseState(this.categoryCount, expandedCount);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00042760 File Offset: 0x00040960
		protected virtual bool IsViewEmpty(Context context)
		{
			return false;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00042763 File Offset: 0x00040963
		protected void SetCategorizedView(int categoryCount, int expandedCount, SortOrder categoryHeadersSortOrder, CategoryHeaderSortOverride[] categoryHeaderSortOverrides)
		{
			this.categoryCount = categoryCount;
			this.collapseState = new CategorizedTableCollapseState(categoryCount, expandedCount);
			this.categoryHeadersSortOrder = categoryHeadersSortOrder;
			this.categoryHeaderSortOverrides = categoryHeaderSortOverrides;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00042788 File Offset: 0x00040988
		protected SortOrder GetSortOrderForView()
		{
			if (this.IsCategorizedView)
			{
				List<Column> list = new List<Column>(this.CategoryHeadersSortOrder.Columns);
				list.AddRange(this.SortOrder.Columns);
				List<bool> list2 = new List<bool>(this.CategoryHeadersSortOrder.Ascending);
				list2.AddRange(this.SortOrder.Ascending);
				return new SortOrder(list, list2);
			}
			return this.SortOrder;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00042800 File Offset: 0x00040A00
		protected virtual Reader FindRow(Context context, SearchCriteria findRowCriteria, Bookmark startBookmark, bool backwards)
		{
			FaultInjection.InjectFault(ViewTable.findRowTestHook);
			SimpleQueryOperator simpleQueryOperator = this.GetFindRowOperator(context, startBookmark, findRowCriteria, backwards);
			if (simpleQueryOperator == null)
			{
				if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug(0L, "FindRow: Failed (no operator).");
				}
				return null;
			}
			Reader reader = null;
			try
			{
				reader = simpleQueryOperator.ExecuteReader(true);
				simpleQueryOperator = null;
				bool flag = reader.Read();
				if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug<string>(0L, "FindRow: {0}.", flag ? "Succeeded" : "Failed");
				}
				if (flag)
				{
					this.BookmarkCurrentRow(reader, true);
					Reader result = reader;
					reader = null;
					return result;
				}
			}
			finally
			{
				if (reader != null)
				{
					reader.Dispose();
				}
				if (simpleQueryOperator != null)
				{
					simpleQueryOperator.Dispose();
				}
			}
			return null;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000428C4 File Offset: 0x00040AC4
		protected void InvalidateBookmarkAndRowCount()
		{
			this.bookmark = Bookmark.BOT;
			this.rowCountValid = false;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000428D8 File Offset: 0x00040AD8
		protected void AdjustRowCountAfterExpandOrCollapse(int delta)
		{
			if (this.rowCountValid)
			{
				this.rowCount += delta;
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000428F0 File Offset: 0x00040AF0
		protected void BookmarkRow(IList<object> keyValues, bool positionedOn)
		{
			this.bookmark = new Bookmark(keyValues, positionedOn);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000428FF File Offset: 0x00040AFF
		protected void SetImplicitCriteria(SearchCriteria implicitCriteria)
		{
			this.implicitCriteria = implicitCriteria;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00042908 File Offset: 0x00040B08
		protected virtual void BringIndexesToCurrent(Context context, IList<IIndex> indexList, DataAccessOperator queryPlan)
		{
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0004299F File Offset: 0x00040B9F
		protected virtual SearchCriteria RewriteSearchCriteria(Context context, SearchCriteria criteriaToRewrite)
		{
			if (criteriaToRewrite == null)
			{
				return null;
			}
			return criteriaToRewrite.InspectAndFix(delegate(SearchCriteria criteria, CompareInfo compareInfo)
			{
				if (this.MvExplosion)
				{
					SearchCriteriaCompare searchCriteriaCompare = criteria as SearchCriteriaCompare;
					if (searchCriteriaCompare != null)
					{
						ExtendedPropertyColumn extendedPropertyColumn = searchCriteriaCompare.Lhs as ExtendedPropertyColumn;
						if (extendedPropertyColumn != null)
						{
							StorePropTag storePropTag = ((ExtendedPropertyColumn)this.multiValueInstanceColumn).StorePropTag;
							if (extendedPropertyColumn.StorePropTag.IsMultiValued && extendedPropertyColumn.StorePropTag == storePropTag.ChangeType(storePropTag.PropType & (PropertyType)57343))
							{
								return Factory.CreateSearchCriteriaCompare(this.multiValueInstanceColumn, searchCriteriaCompare.RelOp, searchCriteriaCompare.Rhs);
							}
						}
					}
				}
				return criteria;
			}, (context.Culture == null) ? null : context.Culture.CompareInfo, false);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000429CF File Offset: 0x00040BCF
		protected virtual IReadOnlyDictionary<Column, Column> GetColumnRenames(Context context)
		{
			return null;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x000429D2 File Offset: 0x00040BD2
		protected virtual CategorizedQueryParams GetCategorizedQueryParams(Context context)
		{
			return null;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x000429D8 File Offset: 0x00040BD8
		protected virtual bool CheckBookmarkPosition(Context context, Bookmark bookmark)
		{
			if (!this.IsViewEmpty(context))
			{
				IList<IIndex> list;
				IList<IIndex> inScopePseudoIndexes = this.GetInScopePseudoIndexes(context, null, out list);
				QueryPlanner queryPlanner = new QueryPlanner(context, this.Table, this.tableFunctionParameters, this.implicitCriteria, this.restrictCriteria, null, null, null, this.GetColumnRenames(context), this.GetCategorizedQueryParams(context), inScopePseudoIndexes, list, this.GetSortOrderForView(), bookmark, 0, 1, !bookmark.PositionedOn, this.MustUseLazyIndex, false, true, false, list == null, (this.FilterFactorHints == null) ? QueryPlanner.Hints.Empty : new QueryPlanner.Hints
				{
					FilterFactorHints = this.FilterFactorHints
				});
				using (SimpleQueryOperator simpleQueryOperator = queryPlanner.CreatePlan())
				{
					if (list != null)
					{
						this.BringIndexesToCurrent(context, list, simpleQueryOperator);
					}
					this.BringIndexesToCurrent(context, inScopePseudoIndexes, simpleQueryOperator);
					using (Reader reader = simpleQueryOperator.ExecuteReader(false))
					{
						if (reader.Read())
						{
							return this.CheckBookmarkMatchesRow(context, reader, bookmark, false);
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00042AEC File Offset: 0x00040CEC
		protected virtual void DoSeekQuery(Context context, Bookmark bookmark, bool backwards, int rowsToSeek, int? newPosition, out bool soughtLessThanRowCount, out int rowCountActuallySought)
		{
			soughtLessThanRowCount = true;
			rowCountActuallySought = 0;
			if (!this.IsViewEmpty(context))
			{
				IList<IIndex> list;
				IList<IIndex> inScopePseudoIndexes = this.GetInScopePseudoIndexes(context, null, out list);
				IList<Column> columnsToFetch = this.IsCategorizedView ? new Column[]
				{
					PropertySchema.MapToColumn(context.Database, ObjectType.Message, PropTag.Message.ContentCount)
				} : null;
				QueryPlanner queryPlanner = new QueryPlanner(context, this.Table, this.tableFunctionParameters, this.implicitCriteria, this.restrictCriteria, null, columnsToFetch, null, this.GetColumnRenames(context), this.GetCategorizedQueryParams(context), inScopePseudoIndexes, list, this.GetSortOrderForView(), bookmark, rowsToSeek - 1, 1, backwards, this.MustUseLazyIndex, false, true, false, list == null, (this.FilterFactorHints == null) ? QueryPlanner.Hints.Empty : new QueryPlanner.Hints
				{
					FilterFactorHints = this.FilterFactorHints
				});
				using (SimpleQueryOperator simpleQueryOperator = queryPlanner.CreatePlan())
				{
					if (simpleQueryOperator != null)
					{
						if (list != null)
						{
							this.BringIndexesToCurrent(context, list, simpleQueryOperator);
						}
						this.BringIndexesToCurrent(context, inScopePseudoIndexes, simpleQueryOperator);
						using (Reader reader = simpleQueryOperator.ExecuteReader(false))
						{
							int num;
							if (reader.Read(out num))
							{
								this.BookmarkCurrentRow(reader, backwards, newPosition);
								if (this.rowCountValid && this.rowCount <= rowsToSeek - 1)
								{
									this.rowCountValid = false;
								}
								soughtLessThanRowCount = false;
								rowCountActuallySought = rowsToSeek;
							}
							else
							{
								soughtLessThanRowCount = true;
								rowCountActuallySought = num;
							}
						}
					}
				}
			}
			if (soughtLessThanRowCount)
			{
				this.bookmark = (backwards ? Bookmark.BOT : Bookmark.EOT);
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00042C78 File Offset: 0x00040E78
		private void Uncategorize()
		{
			if (this.IsCategorizedView)
			{
				this.categoryCount = 0;
				this.collapseState = null;
				this.categoryHeadersSortOrder = SortOrder.Empty;
				this.categoryHeaderSortOverrides = null;
				this.InvalidateBookmarkAndRowCount();
			}
		}

		// Token: 0x0400074C RID: 1868
		public const int MaximumNumberOfCategoryHeaders = 4;

		// Token: 0x0400074D RID: 1869
		private const int MagicBookmarkBOT = 173305666;

		// Token: 0x0400074E RID: 1870
		private const int MagicBookmarkEOT = 173305669;

		// Token: 0x0400074F RID: 1871
		private const int MagicBookmarkKey = 175727947;

		// Token: 0x04000750 RID: 1872
		private static readonly ClientType[] excludedFromColumnValidation = new ClientType[]
		{
			ClientType.WebServices,
			ClientType.MoMT,
			ClientType.User,
			ClientType.Migration,
			ClientType.PublicFolderSystem,
			ClientType.Management,
			ClientType.LoadGen,
			ClientType.Pop,
			ClientType.Imap
		};

		// Token: 0x04000751 RID: 1873
		private static Hookable<Action> findRowTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x04000752 RID: 1874
		private static Hookable<Action<SimpleQueryOperator, int, int>> findRowOperatorTestHook = Hookable<Action<SimpleQueryOperator, int, int>>.Create(true, null);

		// Token: 0x04000753 RID: 1875
		private readonly Mailbox mailbox;

		// Token: 0x04000754 RID: 1876
		private readonly Table table;

		// Token: 0x04000755 RID: 1877
		private IList<Column> viewColumns;

		// Token: 0x04000756 RID: 1878
		private SearchCriteria implicitCriteria;

		// Token: 0x04000757 RID: 1879
		private SearchCriteria restrictCriteria;

		// Token: 0x04000758 RID: 1880
		private SortOrder sortOrder;

		// Token: 0x04000759 RID: 1881
		private int categoryCount;

		// Token: 0x0400075A RID: 1882
		private CategorizedTableCollapseState collapseState;

		// Token: 0x0400075B RID: 1883
		private SortOrder categoryHeadersSortOrder;

		// Token: 0x0400075C RID: 1884
		private CategoryHeaderSortOverride[] categoryHeaderSortOverrides;

		// Token: 0x0400075D RID: 1885
		private Bookmark bookmark;

		// Token: 0x0400075E RID: 1886
		private Bookmark lastBookmark;

		// Token: 0x0400075F RID: 1887
		private int rowCount;

		// Token: 0x04000760 RID: 1888
		private bool rowCountValid;

		// Token: 0x04000761 RID: 1889
		private Column multiValueInstanceColumn;

		// Token: 0x04000762 RID: 1890
		private object[] tableFunctionParameters;

		// Token: 0x04000763 RID: 1891
		private int optimizedSeeks;
	}
}
