using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementGUI.Tasks;

namespace Microsoft.Exchange.Management.SystemManager.Tasks
{
	// Token: 0x0200010C RID: 268
	public class TaskDataSource : DataTableLoader, IPagedList, IAdvancedBindingListView, IBindingListView, IBindingList, IList, ICollection, IEnumerable, ITypedList
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x00021B64 File Offset: 0x0001FD64
		public TaskDataSource()
		{
			this.defaultTable = new DataTable();
			base.Table = this.defaultTable;
			base.Table.Locale = CultureInfo.InvariantCulture;
			this.InnerList.AllowEdit = false;
			this.InnerList.AllowNew = false;
			this.InnerList.AllowDelete = false;
			this.selectCommand = new LoggableMonadCommand("Get-");
			this.selectCommand.Parameters.AddWithValue("ResultSize", 1000);
			this.selectCommand.Parameters.AddWithValue("ReturnPageInfo", true);
			base.Table.ExtendedProperties["TotalCount"] = 0;
			base.Table.ExtendedProperties["Position"] = 0;
			this.sortOrder = new ArrayList();
			this.PreparePage(TaskDataSource.PageChangeType.FirstPage, 0);
			base.RefreshArgument = this.DefaultRefreshArgument;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00021C79 File Offset: 0x0001FE79
		public TaskDataSource(string noun) : this()
		{
			this.Noun = noun;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00021C88 File Offset: 0x0001FE88
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.dataView.Dispose();
				this.defaultTable.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00021CAA File Offset: 0x0001FEAA
		protected sealed override DataTable DefaultTable
		{
			get
			{
				return this.defaultTable;
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00021CB4 File Offset: 0x0001FEB4
		protected override void OnTableChanged(EventArgs e)
		{
			if (base.Table == null)
			{
				base.Table = this.dataView.Table;
				throw new ArgumentNullException("dataTable");
			}
			this.InnerList = new DataView(base.Table);
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			base.OnTableChanged(e);
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00021D0A File Offset: 0x0001FF0A
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x00021D14 File Offset: 0x0001FF14
		private protected DataView InnerList
		{
			protected get
			{
				return this.dataView;
			}
			private set
			{
				if (this.dataView != null)
				{
					this.dataView.ListChanged -= this.InnerList_ListChanged;
				}
				this.dataView = value;
				if (this.dataView != null)
				{
					this.dataView.ListChanged += this.InnerList_ListChanged;
				}
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00021D66 File Offset: 0x0001FF66
		private void InnerList_ListChanged(object sender, ListChangedEventArgs e)
		{
			this.OnListChanged(e);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00021D6F File Offset: 0x0001FF6F
		public void Refresh()
		{
			if (base.IsInitialized)
			{
				base.Refresh(base.CreateProgress(base.RefreshCommandText));
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00021D90 File Offset: 0x0001FF90
		internal MonadParameterCollection GetCommandParameters
		{
			get
			{
				return this.selectCommand.Parameters;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00021D9D File Offset: 0x0001FF9D
		protected override ICloneable DefaultRefreshArgument
		{
			get
			{
				return this.selectCommand;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00021DA5 File Offset: 0x0001FFA5
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00021DAD File Offset: 0x0001FFAD
		[DefaultValue("")]
		public string Noun
		{
			get
			{
				return this.noun;
			}
			set
			{
				if (value != this.Noun)
				{
					this.noun = value;
					this.selectCommand.CommandText = "get-" + this.Noun;
				}
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00021DE0 File Offset: 0x0001FFE0
		protected override void OnRefreshStarting(CancelEventArgs e)
		{
			if (this.SupportsFiltering)
			{
				this.selectCommand.Parameters.Remove("Filter");
				if (!string.IsNullOrEmpty(this.Filter))
				{
					this.selectCommand.Parameters.AddWithValue("Filter", this.Filter);
				}
			}
			if (!this.pagingParametersChanged)
			{
				this.PreparePage(TaskDataSource.PageChangeType.ReloadPage, -1);
			}
			this.pagingParametersChanged = false;
			base.OnRefreshStarting(e);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00021E51 File Offset: 0x00020051
		protected override void OnRefreshingChanged(EventArgs e)
		{
			this.Filtering = base.Refreshing;
			base.OnRefreshingChanged(e);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00021E68 File Offset: 0x00020068
		protected override bool TryToGetPartialRefreshArgument(object[] ids, out object partialRefreshArgument)
		{
			if (1 != ids.Length)
			{
				partialRefreshArgument = null;
				return false;
			}
			MonadCommand monadCommand = this.selectCommand.Clone();
			monadCommand.Parameters.Clear();
			monadCommand.Parameters.AddWithValue("Identity", ids[0]);
			partialRefreshArgument = monadCommand;
			return true;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00021EB0 File Offset: 0x000200B0
		private void InternalSort()
		{
			StringBuilder stringBuilder = new StringBuilder();
			QueueViewerSortOrderEntry[] array = (QueueViewerSortOrderEntry[])this.SortOrder.ToArray(typeof(QueueViewerSortOrderEntry));
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString());
				if (i != array.Length - 1)
				{
					stringBuilder.Append(",");
				}
			}
			this.selectCommand.Parameters.Remove("SortOrder");
			this.selectCommand.Parameters.AddWithValue("SortOrder", array);
			this.PreparePage(TaskDataSource.PageChangeType.ReloadPage, -1);
			this.Refresh();
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00021F49 File Offset: 0x00020149
		public bool SupportsPaging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00021F4C File Offset: 0x0002014C
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00021F68 File Offset: 0x00020168
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int CurrentItem
		{
			get
			{
				return (int)base.Table.ExtendedProperties["Position"];
			}
			set
			{
				this.PreparePage(TaskDataSource.PageChangeType.PageToIndex, value);
				this.Refresh();
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00021F78 File Offset: 0x00020178
		public int ItemsInCurrentPage
		{
			get
			{
				return base.Table.Rows.Count;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00021F8A File Offset: 0x0002018A
		public int TotalItems
		{
			get
			{
				return (int)base.Table.ExtendedProperties["TotalCount"];
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00021FA6 File Offset: 0x000201A6
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x00021FAE File Offset: 0x000201AE
		[DefaultValue(1000)]
		public int PageSize
		{
			get
			{
				return base.ExpectedResultSize;
			}
			set
			{
				base.ExpectedResultSize = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00021FB7 File Offset: 0x000201B7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int DefaultExpectedResultSize
		{
			get
			{
				return 1000;
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00021FBE File Offset: 0x000201BE
		protected override void OnExpectedResultSizeChanged(EventArgs e)
		{
			base.OnExpectedResultSizeChanged(e);
			this.selectCommand.Parameters["ResultSize"].Value = base.ExpectedResultSize;
			this.PreparePage(TaskDataSource.PageChangeType.ReloadPage, -1);
			this.Refresh();
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00021FFA File Offset: 0x000201FA
		public void GoToFirstPage()
		{
			this.PreparePage(TaskDataSource.PageChangeType.FirstPage, 0);
			this.Refresh();
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002200A File Offset: 0x0002020A
		public void GoToLastPage()
		{
			this.PreparePage(TaskDataSource.PageChangeType.LastPage, 0);
			this.Refresh();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002201A File Offset: 0x0002021A
		public void GoToNextPage()
		{
			this.PreparePage(TaskDataSource.PageChangeType.NextPage, 0);
			this.Refresh();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002202A File Offset: 0x0002022A
		public void GoToPreviousPage()
		{
			this.PreparePage(TaskDataSource.PageChangeType.PreviousPage, 0);
			this.Refresh();
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0002203A File Offset: 0x0002023A
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x00022042 File Offset: 0x00020242
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ConfigObject Bookmark
		{
			get
			{
				return this.bookmark;
			}
			set
			{
				this.bookmark = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0002204B File Offset: 0x0002024B
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x0002206C File Offset: 0x0002026C
		private bool IncludeBookmark
		{
			get
			{
				return (bool)this.selectCommand.Parameters["IncludeBookmark"].Value;
			}
			set
			{
				this.selectCommand.Parameters["IncludeBookmark"].Value = value;
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00022090 File Offset: 0x00020290
		private void PreparePage(TaskDataSource.PageChangeType type, int index)
		{
			if (type == TaskDataSource.PageChangeType.PageToIndex && index < 0)
			{
				throw new ArgumentException("Index must be greater or equal to 0.", "index");
			}
			switch (type)
			{
			case TaskDataSource.PageChangeType.PageToIndex:
				this.ClearPagingParameters();
				this.selectCommand.Parameters.AddWithValue("SearchForward", true);
				this.selectCommand.Parameters.AddWithValue("BookmarkObject", null);
				this.selectCommand.Parameters.AddWithValue("BookmarkIndex", index);
				this.selectCommand.Parameters.AddWithValue("IncludeBookmark", true);
				break;
			case TaskDataSource.PageChangeType.FirstPage:
				this.ClearPagingParameters();
				this.selectCommand.Parameters.AddWithValue("SearchForward", true);
				this.selectCommand.Parameters.AddWithValue("BookmarkObject", null);
				this.selectCommand.Parameters.AddWithValue("BookmarkIndex", -1);
				this.selectCommand.Parameters.AddWithValue("IncludeBookmark", false);
				break;
			case TaskDataSource.PageChangeType.LastPage:
				this.ClearPagingParameters();
				this.selectCommand.Parameters.AddWithValue("SearchForward", false);
				this.selectCommand.Parameters.AddWithValue("BookmarkObject", null);
				this.selectCommand.Parameters.AddWithValue("BookmarkIndex", -1);
				this.selectCommand.Parameters.AddWithValue("IncludeBookmark", false);
				break;
			case TaskDataSource.PageChangeType.NextPage:
				this.ClearPagingParameters();
				this.selectCommand.Parameters.AddWithValue("SearchForward", true);
				this.selectCommand.Parameters.AddWithValue("BookmarkObject", base.Table.ExtendedProperties["BookmarkNext"]);
				this.selectCommand.Parameters.AddWithValue("BookmarkIndex", -1);
				this.selectCommand.Parameters.AddWithValue("IncludeBookmark", false);
				break;
			case TaskDataSource.PageChangeType.PreviousPage:
				this.ClearPagingParameters();
				this.selectCommand.Parameters.AddWithValue("SearchForward", false);
				this.selectCommand.Parameters.AddWithValue("BookmarkObject", base.Table.ExtendedProperties["BookmarkPrevious"]);
				this.selectCommand.Parameters.AddWithValue("BookmarkIndex", -1);
				this.selectCommand.Parameters.AddWithValue("IncludeBookmark", false);
				break;
			case TaskDataSource.PageChangeType.ReloadPage:
				if (base.Table.ExtendedProperties["BookmarkPrevious"] == null)
				{
					this.PreparePage(TaskDataSource.PageChangeType.FirstPage, 0);
				}
				else
				{
					this.ClearPagingParameters();
					this.selectCommand.Parameters.AddWithValue("SearchForward", true);
					this.selectCommand.Parameters.AddWithValue("BookmarkObject", base.Table.ExtendedProperties["BookmarkPrevious"]);
					this.selectCommand.Parameters.AddWithValue("BookmarkIndex", -1);
					this.selectCommand.Parameters.AddWithValue("IncludeBookmark", true);
				}
				break;
			}
			this.pagingParametersChanged = true;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00022400 File Offset: 0x00020600
		private void ClearPagingParameters()
		{
			this.selectCommand.Parameters.Remove("SearchForward");
			this.selectCommand.Parameters.Remove("BookmarkObject");
			this.selectCommand.Parameters.Remove("BookmarkIndex");
			this.selectCommand.Parameters.Remove("IncludeBookmark");
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00022461 File Offset: 0x00020661
		public bool IsSortSupported(string propertyName)
		{
			return true;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00022464 File Offset: 0x00020664
		public bool IsFilterSupported(string propertyName)
		{
			return true;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00022467 File Offset: 0x00020667
		public virtual void ApplyFilter(QueryFilter filter)
		{
			this.QueryFilter = filter;
			this.ApplyFilter();
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00022476 File Offset: 0x00020676
		private void ApplyFilter()
		{
			this.filter = ((this.QueryFilter != null) ? this.QueryFilter.GenerateInfixString(FilterLanguage.Monad) : string.Empty);
			this.Refresh();
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0002249F File Offset: 0x0002069F
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x000224A7 File Offset: 0x000206A7
		public QueryFilter QueryFilter { get; private set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000224B0 File Offset: 0x000206B0
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x000224B8 File Offset: 0x000206B8
		public bool Filtering
		{
			get
			{
				return this.filtering;
			}
			private set
			{
				if (this.Filtering != value)
				{
					this.filtering = value;
					this.OnFilteringChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060009C3 RID: 2499 RVA: 0x000224D5 File Offset: 0x000206D5
		// (remove) Token: 0x060009C4 RID: 2500 RVA: 0x000224E8 File Offset: 0x000206E8
		public event EventHandler FilteringChanged
		{
			add
			{
				base.Events.AddHandler(TaskDataSource.EventFilteringChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(TaskDataSource.EventFilteringChanged, value);
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x000224FB File Offset: 0x000206FB
		protected virtual void OnFilteringChanged(EventArgs e)
		{
			if (base.Events[TaskDataSource.EventFilteringChanged] != null)
			{
				((EventHandler)base.Events[TaskDataSource.EventFilteringChanged])(this, e);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0002252B File Offset: 0x0002072B
		public bool SupportCancelFiltering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002252E File Offset: 0x0002072E
		public void CancelFiltering()
		{
			if (this.Filtering)
			{
				base.CancelRefresh();
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0002253E File Offset: 0x0002073E
		public bool SupportsFiltering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00022541 File Offset: 0x00020741
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x00022549 File Offset: 0x00020749
		[DefaultValue("")]
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				throw new NotSupportedException("Do not support set TaskDataSource.Filter, please set QueryFilter");
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00022555 File Offset: 0x00020755
		public void RemoveFilter()
		{
			this.ApplyFilter(null);
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002255E File Offset: 0x0002075E
		public bool SupportsAdvancedSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00022561 File Offset: 0x00020761
		public ListSortDescriptionCollection SortDescriptions
		{
			get
			{
				return this.sortDescriptions;
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00022569 File Offset: 0x00020769
		public void ApplySort(ListSortDescriptionCollection sorts)
		{
			if (!this.SupportsAdvancedSorting)
			{
				throw new NotSupportedException();
			}
			this.OnApplySort(sorts);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00022580 File Offset: 0x00020780
		protected virtual void OnApplySort(ListSortDescriptionCollection sorts)
		{
			this.sortOrder.Clear();
			this.sortDescriptions = sorts;
			foreach (object obj in ((IEnumerable)sorts))
			{
				ListSortDescription listSortDescription = (ListSortDescription)obj;
				this.sortOrder.Add(new QueueViewerSortOrderEntry(listSortDescription.PropertyDescriptor.Name, listSortDescription.SortDirection));
			}
			this.InternalSort();
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00022608 File Offset: 0x00020808
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			((IBindingList)this.InnerList).AddIndex(property);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00022616 File Offset: 0x00020816
		object IBindingList.AddNew()
		{
			return ((IBindingList)this.InnerList).AddNew();
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00022623 File Offset: 0x00020823
		bool IBindingList.AllowEdit
		{
			get
			{
				return ((IBindingList)this.InnerList).AllowEdit;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00022630 File Offset: 0x00020830
		bool IBindingList.AllowNew
		{
			get
			{
				return ((IBindingList)this.InnerList).AllowNew;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0002263D File Offset: 0x0002083D
		bool IBindingList.AllowRemove
		{
			get
			{
				return ((IBindingList)this.InnerList).AllowRemove;
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002264A File Offset: 0x0002084A
		public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			this.sortProperty = property;
			this.sortDirection = direction;
			this.OnApplySort();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00022660 File Offset: 0x00020860
		protected virtual void OnApplySort()
		{
			QueueViewerSortOrderEntry value = new QueueViewerSortOrderEntry(this.SortProperty.Name, this.SortDirection);
			this.sortOrder.Clear();
			this.sortOrder.Add(value);
			this.InternalSort();
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000226A2 File Offset: 0x000208A2
		protected ArrayList SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000226AA File Offset: 0x000208AA
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			return ((IBindingList)this.InnerList).Find(property, key);
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x000226B9 File Offset: 0x000208B9
		bool IBindingList.IsSorted
		{
			get
			{
				return null != this.sortProperty;
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000226C8 File Offset: 0x000208C8
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler listChangedEventHandler = (ListChangedEventHandler)base.Events[TaskDataSource.EventListChanged];
			if (listChangedEventHandler != null)
			{
				listChangedEventHandler(this, e);
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060009DB RID: 2523 RVA: 0x000226F6 File Offset: 0x000208F6
		// (remove) Token: 0x060009DC RID: 2524 RVA: 0x00022709 File Offset: 0x00020909
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				base.Events.AddHandler(TaskDataSource.EventListChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(TaskDataSource.EventListChanged, value);
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002271C File Offset: 0x0002091C
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			((IBindingList)this.InnerList).RemoveIndex(property);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002272A File Offset: 0x0002092A
		public void RemoveSort()
		{
			this.sortProperty = null;
			this.sortDirection = ListSortDirection.Ascending;
			this.OnRemoveSort();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00022740 File Offset: 0x00020940
		protected virtual void OnRemoveSort()
		{
			this.sortDescriptions = null;
			this.SortOrder.Clear();
			this.InternalSort();
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0002275A File Offset: 0x0002095A
		public ListSortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00022762 File Offset: 0x00020962
		public PropertyDescriptor SortProperty
		{
			get
			{
				return this.sortProperty;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002276A File Offset: 0x0002096A
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0002276D File Offset: 0x0002096D
		bool IBindingList.SupportsSearching
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00022770 File Offset: 0x00020970
		bool IBindingList.SupportsSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00022773 File Offset: 0x00020973
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002277A File Offset: 0x0002097A
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00022781 File Offset: 0x00020981
		bool IList.Contains(object value)
		{
			return ((IList)this.InnerList).Contains(value);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002278F File Offset: 0x0002098F
		int IList.IndexOf(object value)
		{
			return ((IList)this.InnerList).IndexOf(value);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002279D File Offset: 0x0002099D
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x000227A4 File Offset: 0x000209A4
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x000227A7 File Offset: 0x000209A7
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000288 RID: 648
		object IList.this[int index]
		{
			get
			{
				return this.InnerList[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000227BF File Offset: 0x000209BF
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000227C6 File Offset: 0x000209C6
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000227CD File Offset: 0x000209CD
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x000227DC File Offset: 0x000209DC
		public int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x000227E9 File Offset: 0x000209E9
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x000227F6 File Offset: 0x000209F6
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00022803 File Offset: 0x00020A03
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00022810 File Offset: 0x00020A10
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			return ((ITypedList)this.InnerList).GetItemProperties(listAccessors);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002281E File Offset: 0x00020A1E
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return ((ITypedList)this.InnerList).GetListName(listAccessors);
		}

		// Token: 0x04000428 RID: 1064
		private const int DefaultPageSize = 1000;

		// Token: 0x04000429 RID: 1065
		private DataTable defaultTable;

		// Token: 0x0400042A RID: 1066
		private DataView dataView;

		// Token: 0x0400042B RID: 1067
		private MonadCommand selectCommand;

		// Token: 0x0400042C RID: 1068
		private string noun = "";

		// Token: 0x0400042D RID: 1069
		private bool pagingParametersChanged;

		// Token: 0x0400042E RID: 1070
		private ConfigObject bookmark;

		// Token: 0x0400042F RID: 1071
		private bool filtering;

		// Token: 0x04000430 RID: 1072
		private static object EventFilteringChanged = new object();

		// Token: 0x04000431 RID: 1073
		private string filter = "";

		// Token: 0x04000432 RID: 1074
		private ListSortDescriptionCollection sortDescriptions;

		// Token: 0x04000433 RID: 1075
		private ArrayList sortOrder;

		// Token: 0x04000434 RID: 1076
		private static readonly object EventListChanged = new object();

		// Token: 0x04000435 RID: 1077
		private ListSortDirection sortDirection;

		// Token: 0x04000436 RID: 1078
		private PropertyDescriptor sortProperty;

		// Token: 0x0200010D RID: 269
		private enum PageChangeType
		{
			// Token: 0x04000439 RID: 1081
			PageToIndex,
			// Token: 0x0400043A RID: 1082
			FirstPage,
			// Token: 0x0400043B RID: 1083
			LastPage,
			// Token: 0x0400043C RID: 1084
			NextPage,
			// Token: 0x0400043D RID: 1085
			PreviousPage,
			// Token: 0x0400043E RID: 1086
			ReloadPage
		}
	}
}
