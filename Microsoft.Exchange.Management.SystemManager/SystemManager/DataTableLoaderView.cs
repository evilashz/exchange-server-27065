using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000017 RID: 23
	public class DataTableLoaderView : Component, IAdvancedBindingListView, IBindingListView, IBindingList, IList, ICollection, IEnumerable, ITypedList
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00005B87 File Offset: 0x00003D87
		public DataTableLoaderView()
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005BB0 File Offset: 0x00003DB0
		public DataTableLoaderView(DataTableLoader dataTableLoader, IObjectComparer defaultObjectComparer, ObjectToFilterableConverter defaultObjectToFilterableConverter)
		{
			if (dataTableLoader == null)
			{
				throw new ArgumentNullException("dataTableLoader");
			}
			if (dataTableLoader.Table == null)
			{
				throw new ArgumentException("dataTableLoader.Table is null");
			}
			if (defaultObjectComparer == null)
			{
				throw new ArgumentNullException("defaultObjectComparer");
			}
			if (defaultObjectToFilterableConverter == null)
			{
				throw new ArgumentNullException("defaultObjectToFilterableConverter");
			}
			this.DataTableLoader = dataTableLoader;
			this.DefaultObjectComparer = defaultObjectComparer;
			this.DefaultObjectToFilterableConverter = defaultObjectToFilterableConverter;
			this.FilterSupportDescriptions.ListChanging += this.FilterSupportDescriptions_ListChanging;
			this.FilterSupportDescriptions.ListChanged += this.FilterSupportDescriptions_ListChanged;
			this.SortSupportDescriptions.ListChanging += this.SortSupportDescriptions_ListChanging;
			this.SortSupportDescriptions.ListChanged += this.SortSupportDescriptions_ListChanged;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005C94 File Offset: 0x00003E94
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SortSupportDescriptions.Clear();
				this.SortSupportDescriptions.ListChanging -= this.SortSupportDescriptions_ListChanging;
				this.SortSupportDescriptions.ListChanged -= this.SortSupportDescriptions_ListChanged;
				this.FilterSupportDescriptions.Clear();
				this.FilterSupportDescriptions.ListChanging -= this.FilterSupportDescriptions_ListChanging;
				this.FilterSupportDescriptions.ListChanged -= this.FilterSupportDescriptions_ListChanged;
				this.DataTableLoader = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005D24 File Offset: 0x00003F24
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00005D2C File Offset: 0x00003F2C
		public DataTableLoader DataTableLoader
		{
			get
			{
				return this.dataTableLoader;
			}
			private set
			{
				if (this.DataTableLoader != value)
				{
					this.DetachSort();
					this.DetachFilter();
					if (this.DataTableLoader != null)
					{
						this.DataTableLoader.RefreshingChanged -= this.DataTableLoader_RefreshingChanged;
						this.DataView = null;
					}
					this.dataTableLoader = value;
					if (this.DataTableLoader != null)
					{
						this.DataView = new DataView(this.DataTableLoader.Table);
						this.DataTableLoader.RefreshingChanged += this.DataTableLoader_RefreshingChanged;
					}
					this.AttachSort();
					this.AttachFilter();
				}
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005DBC File Offset: 0x00003FBC
		private void DataTableLoader_RefreshingChanged(object sender, EventArgs e)
		{
			this.Filtering = this.DataTableLoader.Refreshing;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005DCF File Offset: 0x00003FCF
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00005DD8 File Offset: 0x00003FD8
		public DataView DataView
		{
			get
			{
				return this.dataView;
			}
			private set
			{
				if (this.DataView != value)
				{
					if (this.DataView != null)
					{
						this.DataView.ListChanged -= this.DataView_ListChanged;
						this.DataView.Table = null;
						this.DataView.Dispose();
					}
					this.dataView = value;
					if (this.DataView != null)
					{
						this.DataView.AllowEdit = false;
						this.DataView.AllowNew = false;
						this.DataView.AllowDelete = false;
						this.DataView.ListChanged += this.DataView_ListChanged;
					}
				}
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005E71 File Offset: 0x00004071
		public int Count
		{
			get
			{
				return this.DataView.Count;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005E7E File Offset: 0x0000407E
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.DataView).GetEnumerator();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005E8B File Offset: 0x0000408B
		private void DataView_ListChanged(object sender, ListChangedEventArgs e)
		{
			this.OnListChanged(e);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005E94 File Offset: 0x00004094
		public bool SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005E98 File Offset: 0x00004098
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler listChangedEventHandler = (ListChangedEventHandler)base.Events[DataTableLoaderView.EventListChanged];
			if (listChangedEventHandler != null)
			{
				listChangedEventHandler(this, e);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600011F RID: 287 RVA: 0x00005EC6 File Offset: 0x000040C6
		// (remove) Token: 0x06000120 RID: 288 RVA: 0x00005ED9 File Offset: 0x000040D9
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				base.Events.AddHandler(DataTableLoaderView.EventListChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataTableLoaderView.EventListChanged, value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005EEC File Offset: 0x000040EC
		public virtual bool SupportsFiltering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005EEF File Offset: 0x000040EF
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00005EF7 File Offset: 0x000040F7
		public ObjectToFilterableConverter DefaultObjectToFilterableConverter { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005F00 File Offset: 0x00004100
		public ChangeNotifyingCollection<FilterSupportDescription> FilterSupportDescriptions
		{
			get
			{
				return this.filterSupportDescriptions;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005F28 File Offset: 0x00004128
		public FilterSupportDescription GetFilterSupportDescription(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentException();
			}
			return this.FilterSupportDescriptions.FirstOrDefault((FilterSupportDescription arg) => string.Compare(arg.ColumnName, propertyName, StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005F8C File Offset: 0x0000418C
		public bool IsFilterSupported(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentException("propertyName");
			}
			if (!this.DataTableLoader.Table.Columns.Contains(propertyName))
			{
				return false;
			}
			FilterSupportDescription filterSupportDescription = this.FilterSupportDescriptions.FirstOrDefault((FilterSupportDescription arg) => string.Compare(arg.ColumnName, propertyName, StringComparison.OrdinalIgnoreCase) == 0);
			if (filterSupportDescription != null)
			{
				return !string.IsNullOrEmpty(filterSupportDescription.FilteredColumnName);
			}
			return typeof(IConvertible).IsAssignableFrom(this.DataTableLoader.Table.Columns[propertyName].DataType);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006036 File Offset: 0x00004236
		private void FilterSupportDescriptions_ListChanging(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				this.DetachFilter(this.FilterSupportDescriptions[e.NewIndex]);
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006058 File Offset: 0x00004258
		private void FilterSupportDescriptions_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				this.AttachFilter(this.FilterSupportDescriptions[e.NewIndex]);
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000607C File Offset: 0x0000427C
		private void AttachFilter()
		{
			foreach (FilterSupportDescription filterSupportDescription in this.FilterSupportDescriptions)
			{
				this.AttachFilter(filterSupportDescription);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000060CC File Offset: 0x000042CC
		private void DetachFilter()
		{
			foreach (FilterSupportDescription filterSupportDescription in this.FilterSupportDescriptions)
			{
				this.DetachFilter(filterSupportDescription);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000611C File Offset: 0x0000431C
		private void AttachFilter(FilterSupportDescription filterSupportDescription)
		{
			filterSupportDescription.DataTableLoaderView = this;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006125 File Offset: 0x00004325
		private void DetachFilter(FilterSupportDescription filterSupportDescription)
		{
			filterSupportDescription.DataTableLoaderView = this;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000612E File Offset: 0x0000432E
		public virtual void ApplyFilter(QueryFilter filter)
		{
			this.QueryFilter = filter;
			this.ApplyFilter();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000613D File Offset: 0x0000433D
		public void RemoveFilter()
		{
			this.additionalFilter = string.Empty;
			this.ApplyFilter(null);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006151 File Offset: 0x00004351
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00006159 File Offset: 0x00004359
		public QueryFilter QueryFilter { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006162 File Offset: 0x00004362
		public bool UseDataTableLoaderSideFilter
		{
			get
			{
				return this.DataTableLoader.ResultsLoaderProfile != null && this.DataTableLoader.ResultsLoaderProfile.InputTable.Columns.Contains("Filter");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006192 File Offset: 0x00004392
		// (set) Token: 0x06000133 RID: 307 RVA: 0x0000619A File Offset: 0x0000439A
		[DefaultValue(null)]
		public string Filter
		{
			get
			{
				return this.additionalFilter;
			}
			set
			{
				if (string.Compare(this.additionalFilter, value) != 0)
				{
					this.additionalFilter = value;
					this.ApplyFilter();
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000061B8 File Offset: 0x000043B8
		private void ApplyFilter()
		{
			if (this.UseDataTableLoaderSideFilter)
			{
				string text = (this.QueryFilter != null) ? this.QueryFilter.GenerateInfixString(FilterLanguage.Monad) : string.Empty;
				if (string.IsNullOrEmpty(text))
				{
					text = this.additionalFilter;
				}
				else if (!string.IsNullOrEmpty(this.additionalFilter))
				{
					text = string.Format(CultureInfo.InvariantCulture, "(({0}) -and ({1}))", new object[]
					{
						text,
						this.additionalFilter
					});
				}
				this.DataTableLoader.InputValue("Filter", text);
				this.DataTableLoader.Refresh(this.CreateProgress(this.DataTableLoader.RefreshCommandText));
			}
			else
			{
				string text2 = (this.QueryFilter != null) ? DataTableLoaderView.AdaptQueryFilterForAdo(this.GetTransformedFilter()).GenerateInfixString(FilterLanguage.Ado) : string.Empty;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = this.additionalFilter;
				}
				else if (!string.IsNullOrEmpty(this.additionalFilter))
				{
					text2 = string.Format(CultureInfo.InvariantCulture, "({0} AND {1})", new object[]
					{
						text2,
						this.additionalFilter
					});
				}
				this.DataView.RowFilter = text2;
			}
			if (this.DataTableLoader.ResultsLoaderProfile != null && this.DataTableLoader.ResultsLoaderProfile.PostRefreshAction != null)
			{
				this.DataTableLoader.ResultsLoaderProfile.PostRefreshAction.FilteredDataView = this.DataView;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006310 File Offset: 0x00004510
		private IProgress CreateProgress(string operationName)
		{
			IProgressProvider progressProvider = (IProgressProvider)this.GetService(typeof(IProgressProvider));
			if (progressProvider != null)
			{
				return progressProvider.CreateProgress(operationName);
			}
			return NullProgress.Value;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006343 File Offset: 0x00004543
		private QueryFilter GetTransformedFilter()
		{
			return this.QueryFilter;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000634C File Offset: 0x0000454C
		public static QueryFilter AdaptQueryFilterForAdo(QueryFilter filter)
		{
			QueryFilter queryFilter = filter;
			if (filter is CompositeFilter)
			{
				CompositeFilter compositeFilter = (CompositeFilter)filter;
				List<QueryFilter> list = new List<QueryFilter>(compositeFilter.FilterCount);
				for (int i = 0; i < compositeFilter.FilterCount; i++)
				{
					list.Add(DataTableLoaderView.AdaptQueryFilterForAdo(compositeFilter.Filters[i]));
				}
				if (filter is AndFilter)
				{
					queryFilter = new AndFilter(compositeFilter.IgnoreWhenVerifyingMaxDepth, list.ToArray());
				}
				else if (filter is OrFilter)
				{
					queryFilter = new OrFilter(compositeFilter.IgnoreWhenVerifyingMaxDepth, list.ToArray());
				}
			}
			else if (filter is TextFilter)
			{
				TextFilter textFilter = filter as TextFilter;
				queryFilter = new TextFilter(textFilter.Property, DataTableLoaderView.EscapeStringForADOLike(textFilter.Text), textFilter.MatchOptions, textFilter.MatchFlags);
			}
			else if (filter is ComparisonFilter)
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				ComparisonFilter comparisonFilter2 = new ComparisonFilter(comparisonFilter.ComparisonOperator, comparisonFilter.Property, comparisonFilter.PropertyValue);
				if (comparisonFilter.ComparisonOperator == ComparisonOperator.NotEqual || comparisonFilter.ComparisonOperator == ComparisonOperator.LessThanOrEqual || comparisonFilter.ComparisonOperator == ComparisonOperator.LessThan)
				{
					queryFilter = new OrFilter(new QueryFilter[]
					{
						comparisonFilter2,
						new NotFilter(new ExistsFilter(comparisonFilter.Property))
					});
				}
				else
				{
					queryFilter = comparisonFilter2;
				}
			}
			else if (filter is NotFilter)
			{
				NotFilter notFilter = filter as NotFilter;
				QueryFilter queryFilter2 = DataTableLoaderView.AdaptQueryFilterForAdo(notFilter.Filter);
				TextFilter textFilter2 = queryFilter2 as TextFilter;
				if (textFilter2 != null)
				{
					queryFilter = new OrFilter(new QueryFilter[]
					{
						new NotFilter(queryFilter2),
						new NotFilter(new ExistsFilter(textFilter2.Property))
					});
				}
				else
				{
					queryFilter = new NotFilter(queryFilter2);
				}
			}
			else if (filter is ExistsFilter)
			{
				ExistsFilter existsFilter = filter as ExistsFilter;
				queryFilter = new ExistsFilter(existsFilter.Property);
				if (existsFilter.Property.Type == typeof(string) || existsFilter.Property.Type == typeof(MultiValuedProperty<string>))
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new NotFilter(new TextFilter(existsFilter.Property, string.Empty, MatchOptions.ExactPhrase, MatchFlags.Default))
					});
				}
			}
			return queryFilter;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000065A3 File Offset: 0x000047A3
		private static string EscapeStringForADOLike(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			return DataTableLoaderView.regexForLike.Replace(text, (Match str) => string.Format("[{0}]", str));
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000065D7 File Offset: 0x000047D7
		// (set) Token: 0x0600013A RID: 314 RVA: 0x000065DF File Offset: 0x000047DF
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

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600013B RID: 315 RVA: 0x000065FC File Offset: 0x000047FC
		// (remove) Token: 0x0600013C RID: 316 RVA: 0x0000660F File Offset: 0x0000480F
		public event EventHandler FilteringChanged
		{
			add
			{
				base.Events.AddHandler(DataTableLoaderView.EventFilteringChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataTableLoaderView.EventFilteringChanged, value);
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006622 File Offset: 0x00004822
		protected virtual void OnFilteringChanged(EventArgs e)
		{
			if (base.Events[DataTableLoaderView.EventFilteringChanged] != null)
			{
				((EventHandler)base.Events[DataTableLoaderView.EventFilteringChanged])(this, e);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006652 File Offset: 0x00004852
		public bool SupportCancelFiltering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006655 File Offset: 0x00004855
		public void CancelFiltering()
		{
			if (!this.SupportCancelFiltering)
			{
				throw new InvalidOperationException("Do not support cancel filtering");
			}
			if (this.Filtering && this.DataTableLoader.Refreshing)
			{
				this.DataTableLoader.CancelRefresh();
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000668A File Offset: 0x0000488A
		public bool SupportsSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000668D File Offset: 0x0000488D
		public bool SupportsAdvancedSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006690 File Offset: 0x00004890
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00006698 File Offset: 0x00004898
		public IObjectComparer DefaultObjectComparer { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000066A1 File Offset: 0x000048A1
		public ITextComparer DefaultTextComparer
		{
			get
			{
				return this.DefaultObjectComparer.TextComparer;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000066AE File Offset: 0x000048AE
		public ChangeNotifyingCollection<SortSupportDescription> SortSupportDescriptions
		{
			get
			{
				return this.sortSupportDescriptions;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000066D8 File Offset: 0x000048D8
		public SortSupportDescription GetSortSupportDescription(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentException();
			}
			return this.SortSupportDescriptions.FirstOrDefault((SortSupportDescription arg) => string.Compare(arg.ColumnName, propertyName, StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000673C File Offset: 0x0000493C
		public bool IsSortSupported(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentException("propertyName");
			}
			if (!this.DataTableLoader.Table.Columns.Contains(propertyName))
			{
				return false;
			}
			SortSupportDescription sortSupportDescription = this.SortSupportDescriptions.FirstOrDefault((SortSupportDescription arg) => string.Compare(arg.ColumnName, propertyName, StringComparison.OrdinalIgnoreCase) == 0);
			if (sortSupportDescription != null)
			{
				return sortSupportDescription.SortMode != SortMode.NotSupported && !string.IsNullOrEmpty(sortSupportDescription.SortedColumnName);
			}
			return typeof(IComparable).IsAssignableFrom(this.DataTableLoader.Table.Columns[propertyName].DataType);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000067F1 File Offset: 0x000049F1
		private void SortSupportDescriptions_ListChanging(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				this.DetachSort(this.SortSupportDescriptions[e.NewIndex]);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006813 File Offset: 0x00004A13
		private void SortSupportDescriptions_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				this.AttachSort(this.SortSupportDescriptions[e.NewIndex]);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006838 File Offset: 0x00004A38
		private void AttachSort()
		{
			foreach (SortSupportDescription sortSupportDescription in this.SortSupportDescriptions)
			{
				this.AttachSort(sortSupportDescription);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006888 File Offset: 0x00004A88
		private void DetachSort()
		{
			foreach (SortSupportDescription sortSupportDescription in this.SortSupportDescriptions)
			{
				this.DetachSort(sortSupportDescription);
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000068D8 File Offset: 0x00004AD8
		private void AttachSort(SortSupportDescription sortSupportDescription)
		{
			sortSupportDescription.DataTableLoaderView = this;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000068E1 File Offset: 0x00004AE1
		private void DetachSort(SortSupportDescription sortSupportDescription)
		{
			sortSupportDescription.DataTableLoaderView = null;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000068EA File Offset: 0x00004AEA
		public bool IsSorted
		{
			get
			{
				return ((IBindingList)this.DataView).IsSorted;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000068F8 File Offset: 0x00004AF8
		public virtual void ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			if (!this.DataTableLoader.Table.Columns.Contains(property.Name))
			{
				throw new ArgumentException("property");
			}
			this.DataView.Sort = this.CreateSortString(property, direction);
			this.SortDescriptions = new ListSortDescriptionCollection(new ListSortDescription[]
			{
				new ListSortDescription(property, direction)
			});
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000696C File Offset: 0x00004B6C
		public virtual void ApplySort(ListSortDescriptionCollection sorts)
		{
			if (sorts == null)
			{
				throw new ArgumentNullException("sorts");
			}
			List<ListSortDescription> list = new List<ListSortDescription>();
			foreach (object obj in ((IEnumerable)sorts))
			{
				ListSortDescription listSortDescription = (ListSortDescription)obj;
				if (listSortDescription == null || listSortDescription.PropertyDescriptor == null || !this.DataTableLoader.Table.Columns.Contains(listSortDescription.PropertyDescriptor.Name))
				{
					throw new ArgumentException("sorts");
				}
				list.Add(new ListSortDescription(listSortDescription.PropertyDescriptor, listSortDescription.SortDirection));
			}
			this.DataView.Sort = this.CreateSortString(sorts);
			this.SortDescriptions = new ListSortDescriptionCollection(list.ToArray());
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006A40 File Offset: 0x00004C40
		public void RemoveSort()
		{
			this.ApplySort(new ListSortDescriptionCollection());
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006A4D File Offset: 0x00004C4D
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00006A55 File Offset: 0x00004C55
		public ListSortDescriptionCollection SortDescriptions
		{
			get
			{
				return this.sortDescriptions;
			}
			private set
			{
				this.sortDescriptions = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006A5E File Offset: 0x00004C5E
		public ListSortDirection SortDirection
		{
			get
			{
				return ((IBindingList)this.DataView).SortDirection;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006A6B File Offset: 0x00004C6B
		public PropertyDescriptor SortProperty
		{
			get
			{
				if (this.SortDescriptions.Count != 0)
				{
					return this.SortDescriptions[0].PropertyDescriptor;
				}
				return null;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006AAC File Offset: 0x00004CAC
		private string CreateSortString(PropertyDescriptor property, ListSortDirection direction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			string columnName = property.Name;
			if (this.DataTableLoader.Table.Columns.Contains(columnName))
			{
				SortSupportDescription sortSupportDescription = this.SortSupportDescriptions.FirstOrDefault((SortSupportDescription arg) => string.Compare(arg.ColumnName, columnName, StringComparison.OrdinalIgnoreCase) == 0);
				if (sortSupportDescription != null && !string.IsNullOrEmpty(sortSupportDescription.SortedColumnName))
				{
					columnName = sortSupportDescription.SortedColumnName;
				}
			}
			stringBuilder.Append(columnName);
			stringBuilder.Append(']');
			if (ListSortDirection.Descending == direction)
			{
				stringBuilder.Append(" DESC");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006B60 File Offset: 0x00004D60
		private string CreateSortString(ListSortDescriptionCollection sorts)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (object obj in ((IEnumerable)sorts))
			{
				ListSortDescription listSortDescription = (ListSortDescription)obj;
				if (!flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(this.CreateSortString(listSortDescription.PropertyDescriptor, listSortDescription.SortDirection));
				if (flag)
				{
					flag = false;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public bool SupportsSearching
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006BEB File Offset: 0x00004DEB
		public void AddIndex(PropertyDescriptor property)
		{
			((IBindingList)this.DataView).AddIndex(property);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006BF9 File Offset: 0x00004DF9
		public void RemoveIndex(PropertyDescriptor property)
		{
			((IBindingList)this.DataView).RemoveIndex(property);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006C07 File Offset: 0x00004E07
		public int Find(PropertyDescriptor property, object key)
		{
			return ((IBindingList)this.DataView).Find(property, key);
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006C16 File Offset: 0x00004E16
		bool IBindingList.AllowEdit
		{
			get
			{
				return ((IBindingList)this.DataView).AllowEdit;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006C23 File Offset: 0x00004E23
		bool IBindingList.AllowNew
		{
			get
			{
				return ((IBindingList)this.DataView).AllowNew;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00006C30 File Offset: 0x00004E30
		bool IBindingList.AllowRemove
		{
			get
			{
				return ((IBindingList)this.DataView).AllowRemove;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006C3D File Offset: 0x00004E3D
		object IBindingList.AddNew()
		{
			return ((IBindingList)this.DataView).AddNew();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006C4A File Offset: 0x00004E4A
		int IList.Add(object value)
		{
			return ((IList)this.DataView).Add(value);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006C58 File Offset: 0x00004E58
		void IList.Clear()
		{
			((IList)this.DataView).Clear();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006C65 File Offset: 0x00004E65
		bool IList.Contains(object value)
		{
			return ((IList)this.DataView).Contains(value);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006C73 File Offset: 0x00004E73
		int IList.IndexOf(object value)
		{
			return ((IList)this.DataView).IndexOf(value);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006C81 File Offset: 0x00004E81
		void IList.Insert(int index, object value)
		{
			((IList)this.DataView).Insert(index, value);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00006C90 File Offset: 0x00004E90
		bool IList.IsFixedSize
		{
			get
			{
				return ((IList)this.DataView).IsFixedSize;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006C9D File Offset: 0x00004E9D
		bool IList.IsReadOnly
		{
			get
			{
				return ((IList)this.DataView).IsReadOnly;
			}
		}

		// Token: 0x17000063 RID: 99
		object IList.this[int index]
		{
			get
			{
				return ((IList)this.DataView)[index];
			}
			set
			{
				((IList)this.DataView)[index] = value;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006CC7 File Offset: 0x00004EC7
		void IList.Remove(object value)
		{
			((IList)this.DataView).Remove(value);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006CD5 File Offset: 0x00004ED5
		void IList.RemoveAt(int index)
		{
			((IList)this.DataView).RemoveAt(index);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006CE3 File Offset: 0x00004EE3
		void ICollection.CopyTo(Array array, int index)
		{
			this.DataView.CopyTo(array, index);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006CF2 File Offset: 0x00004EF2
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this.DataView).IsSynchronized;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006CFF File Offset: 0x00004EFF
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.DataView).SyncRoot;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006D0C File Offset: 0x00004F0C
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			return ((ITypedList)this.DataView).GetItemProperties(listAccessors);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006D1A File Offset: 0x00004F1A
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return ((ITypedList)this.DataView).GetListName(listAccessors);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006D28 File Offset: 0x00004F28
		public static DataTableLoaderView Create(DataTableLoader dataTableLoader)
		{
			return new DataTableLoaderView(dataTableLoader, ObjectComparer.DefaultObjectComparer, ObjectToFilterableConverter.DefaultObjectToFilterableConverter);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006D3C File Offset: 0x00004F3C
		internal ComparisonFilter CreateComparisonFilter(ComparisonOperator comparisonOperator, string propertyName, object propertyValue)
		{
			FilterSupportDescription filterSupportDescription = this.GetFilterSupportDescription(propertyName);
			if (filterSupportDescription == null)
			{
				filterSupportDescription = new FilterSupportDescription(propertyName);
				this.FilterSupportDescriptions.Add(filterSupportDescription);
			}
			if (!this.DataTableLoader.Table.Columns.Contains(filterSupportDescription.FilteredColumnName))
			{
				return null;
			}
			return new ComparisonFilter(comparisonOperator, new DataTableLoaderView.FilterablePropertyDefinition(filterSupportDescription.FilteredColumnName, this.DataTableLoader.Table.Columns[filterSupportDescription.FilteredColumnName].DataType), this.DefaultObjectToFilterableConverter.ToFilterable(propertyValue));
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006DC4 File Offset: 0x00004FC4
		internal ExistsFilter CreateExistsFilter(string propertyName)
		{
			FilterSupportDescription filterSupportDescription = this.GetFilterSupportDescription(propertyName);
			if (filterSupportDescription == null)
			{
				filterSupportDescription = new FilterSupportDescription(propertyName);
				this.FilterSupportDescriptions.Add(filterSupportDescription);
			}
			if (!this.DataTableLoader.Table.Columns.Contains(filterSupportDescription.FilteredColumnName))
			{
				return null;
			}
			return new ExistsFilter(new DataTableLoaderView.FilterablePropertyDefinition(filterSupportDescription.FilteredColumnName, this.DataTableLoader.Table.Columns[filterSupportDescription.FilteredColumnName].DataType));
		}

		// Token: 0x0400004E RID: 78
		private DataTableLoader dataTableLoader;

		// Token: 0x0400004F RID: 79
		private DataView dataView;

		// Token: 0x04000050 RID: 80
		private static readonly object EventListChanged = new object();

		// Token: 0x04000051 RID: 81
		private ChangeNotifyingCollection<FilterSupportDescription> filterSupportDescriptions = new ChangeNotifyingCollection<FilterSupportDescription>();

		// Token: 0x04000052 RID: 82
		private string additionalFilter;

		// Token: 0x04000053 RID: 83
		private static readonly Regex regexForLike = new Regex("[\\*%\\[\\]]");

		// Token: 0x04000054 RID: 84
		private bool filtering;

		// Token: 0x04000055 RID: 85
		private static object EventFilteringChanged = new object();

		// Token: 0x04000056 RID: 86
		private ChangeNotifyingCollection<SortSupportDescription> sortSupportDescriptions = new ChangeNotifyingCollection<SortSupportDescription>();

		// Token: 0x04000057 RID: 87
		private ListSortDescriptionCollection sortDescriptions = new ListSortDescriptionCollection();

		// Token: 0x02000018 RID: 24
		private class FilterablePropertyDefinition : PropertyDefinition
		{
			// Token: 0x06000175 RID: 373 RVA: 0x00006E63 File Offset: 0x00005063
			public FilterablePropertyDefinition(string name, Type type) : base(name, type)
			{
			}
		}
	}
}
