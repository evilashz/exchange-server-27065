using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000096 RID: 150
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class View : ServerObject
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x00028E1C File Offset: 0x0002701C
		protected View(Logon logon, TableFlags tableFlags, View.Capabilities capabilities, ViewType viewType, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandle, QueryFilter defaultQueryFilter = null) : base(logon)
		{
			this.tableFlags = tableFlags;
			this.capabilities = capabilities;
			this.viewType = viewType;
			this.notificationHandler = notificationHandler;
			this.returnNotificationHandle = returnNotificationHandle;
			this.defaultQueryFilter = defaultQueryFilter;
			this.filter = defaultQueryFilter;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00028E74 File Offset: 0x00027074
		internal virtual byte[] CreateBookmark()
		{
			throw new RopExecutionException("View not supported", (ErrorCode)2147746050U);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00028E85 File Offset: 0x00027085
		internal virtual void FreeBookmark(byte[] bookmark)
		{
			throw new RopExecutionException("View not supported", (ErrorCode)2147746050U);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00028E96 File Offset: 0x00027096
		internal virtual int SeekRowBookmark(byte[] bookmark, int rowCount, bool wantMoveCount, out bool soughtLess, out bool positionChanged)
		{
			throw new RopExecutionException("View not supported", (ErrorCode)2147746050U);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00028EA7 File Offset: 0x000270A7
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x00028EB8 File Offset: 0x000270B8
		internal PropertyTag[] Columns
		{
			get
			{
				base.CheckDisposed();
				return this.originalColumns;
			}
			private set
			{
				base.CheckDisposed();
				this.originalColumns = value;
				this.serverColumns = this.PropertyConverter.ConvertPropertyTagsFromClient(this.originalColumns);
				if (value == null)
				{
					this.instanceKeyColumnIndex = -1;
					Util.DisposeIfPresent(this.viewNotificationSink);
					this.viewNotificationSink = null;
					return;
				}
				this.instanceKeyColumnIndex = Array.IndexOf<PropertyTag>(this.serverColumns, PropertyTag.InstanceKey);
				if (this.instanceKeyColumnIndex < 0)
				{
					this.serverColumns = View.AddColumn(PropertyTag.InstanceKey, this.serverColumns);
					this.instanceKeyColumnIndex = 0;
				}
				if (this.viewCache != null)
				{
					this.viewCache.SetColumns(this.ColumnPropertyDefinitions, this.ServerColumns);
				}
				this.RegisterNotification();
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00028F67 File Offset: 0x00027167
		internal PropertyTag[] ServerColumns
		{
			get
			{
				base.CheckDisposed();
				return this.serverColumns;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00028F75 File Offset: 0x00027175
		internal IViewDataSource DataSource
		{
			get
			{
				if (this.viewCache != null)
				{
					return this.viewCache.DataSource;
				}
				return null;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00028F8C File Offset: 0x0002718C
		internal QueryFilter Filter
		{
			get
			{
				base.CheckDisposed();
				return this.filter;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060005FE RID: 1534
		protected abstract PropertyConverter PropertyConverter { get; }

		// Token: 0x060005FF RID: 1535 RVA: 0x00028F9C File Offset: 0x0002719C
		internal RopResult ProtectAgainstNoColumnsSet(Func<View, RopResult> protectedMethod)
		{
			bool flag = this.Columns == null;
			RopResult result;
			try
			{
				if (flag)
				{
					this.Columns = Array<PropertyTag>.Empty;
					this.Validate();
				}
				result = protectedMethod(this);
			}
			finally
			{
				if (flag)
				{
					this.Columns = null;
				}
			}
			return result;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00028FF0 File Offset: 0x000271F0
		internal bool TryConvertOriginalRowFromServerRow(PropertyValue[] serverRow, out PropertyValue[] originalRow)
		{
			if (serverRow == null || this.serverColumns == null || serverRow.Length != this.serverColumns.Length)
			{
				originalRow = null;
				return false;
			}
			if (this.originalColumns.Length != this.serverColumns.Length)
			{
				if (this.serverColumns[0] != PropertyTag.InstanceKey)
				{
					throw new ArgumentException(string.Format("We should have added instance key at index = 0. Columns[0] = {0},", this.serverColumns[0]));
				}
				originalRow = new PropertyValue[serverRow.Length - 1];
				Array.Copy(serverRow, 1, originalRow, 0, originalRow.Length);
			}
			else
			{
				originalRow = (PropertyValue[])serverRow.Clone();
			}
			int num = 0;
			this.PropertyConverter.ConvertPropertyValuesToClientAndSuppressClientSide(base.LogonObject.Session, this.StorageObjectProperties, originalRow, this.originalColumns, this.ClientSideProperties);
			for (int i = 0; i < originalRow.Length; i++)
			{
				PropertyValue propertyValue = originalRow[i];
				if (propertyValue.PropertyTag.PropertyId != this.originalColumns[num].PropertyId)
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0002910A File Offset: 0x0002730A
		internal bool IsAvailable
		{
			get
			{
				return !base.IsDisposed;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00029115 File Offset: 0x00027315
		internal bool IsRowWithinUnreadCache(byte[] instanceKey, View.RowLookupPosition rowLookupPosition)
		{
			return !base.IsDisposed && this.viewCache != null && this.viewCache.IsRowWithinUnreadCache(this.instanceKeyColumnIndex, instanceKey, rowLookupPosition);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0002913C File Offset: 0x0002733C
		internal SortBy[] SortBys
		{
			get
			{
				base.CheckDisposed();
				return this.sortBys;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0002914A File Offset: 0x0002734A
		internal GroupByAndOrder[] GroupBys
		{
			get
			{
				base.CheckDisposed();
				return this.groupBys;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00029158 File Offset: 0x00027358
		internal int ExpandedCount
		{
			get
			{
				base.CheckDisposed();
				return this.expandedCount;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00029168 File Offset: 0x00027368
		internal void SetColumns(SetColumnsFlags flags, PropertyTag[] propertyTags)
		{
			base.CheckDisposed();
			this.Columns = null;
			RopHandler.CheckEnum<SetColumnsFlags>(flags);
			Util.ThrowOnNullArgument(propertyTags, "propertyTags");
			if (propertyTags.Length == 0)
			{
				throw new RopExecutionException("Empty PropertyTag[] is invalid.", (ErrorCode)2147942487U);
			}
			foreach (PropertyTag propertyTag in propertyTags)
			{
				if (!PropertyValue.IsSupportedPropertyType(propertyTag) || propertyTag.PropertyType == PropertyType.Unspecified)
				{
					throw new RopExecutionException(string.Format("Invalid PropertyTag used in columns: {0}", propertyTag), (ErrorCode)2147942487U);
				}
			}
			this.CheckPropertiesAllowed(propertyTags);
			this.Columns = propertyTags;
		}

		// Token: 0x06000607 RID: 1543
		protected abstract void CheckPropertiesAllowed(PropertyTag[] propertyTags);

		// Token: 0x06000608 RID: 1544 RVA: 0x00029200 File Offset: 0x00027400
		protected NativeStorePropertyDefinition[] GetColumnPropertyDefinitions(StoreSession session, ICorePropertyBag propertyBag)
		{
			NativeStorePropertyDefinition[] array;
			if (!MEDSPropertyTranslator.TryGetPropertyDefinitionsFromPropertyTags(session, propertyBag, this.ServerColumns, out array) && !View.TryFixUnresolvedPropertyDefinitions(array, this.serverColumns))
			{
				Util.DisposeIfPresent(this.viewCache);
				this.viewCache = null;
				throw new RopExecutionException("Properties for some columns could not be resolved", (ErrorCode)2147746050U);
			}
			return array;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000609 RID: 1545
		protected abstract NativeStorePropertyDefinition[] ColumnPropertyDefinitions { get; }

		// Token: 0x0600060A RID: 1546 RVA: 0x0002924F File Offset: 0x0002744F
		internal int GetPosition()
		{
			base.CheckDisposed();
			this.ValidateCursorPosition();
			this.Validate();
			return this.GetViewCache().GetPosition();
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002926E File Offset: 0x0002746E
		internal int GetRowCount()
		{
			base.CheckDisposed();
			return this.GetViewCache().GetRowCount();
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00029284 File Offset: 0x00027484
		internal void Reset()
		{
			base.CheckDisposed();
			if ((this.capabilities & View.Capabilities.CanReset) != View.Capabilities.CanReset)
			{
				throw new RopExecutionException(string.Format("Reset not supported for view of type {0}.", this.ViewType), (ErrorCode)2147746050U);
			}
			this.originalColumns = null;
			this.serverColumns = null;
			this.sortBys = null;
			this.groupBys = null;
			this.expandedCount = 0;
			this.filter = this.defaultQueryFilter;
			this.hasRestrictFailed = false;
			this.hasSortTableFailed = false;
			this.isCursorPositionUndefined = false;
			this.ClearViewCache();
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0002930C File Offset: 0x0002750C
		internal bool CollectRows(RowCollector rowCollector, QueryRowsFlags flags, bool useForwardDirection, ushort rowCount)
		{
			base.CheckDisposed();
			if (!useForwardDirection && (byte)(flags & QueryRowsFlags.DoNotAdvance) != 0)
			{
				throw Feature.NotImplemented(220256, "DoNotAdvance is not supported on query backward.");
			}
			if (rowCount <= 0)
			{
				throw Feature.NotImplemented(25408, "Consider supporting QueryRows(rowCount = 0)");
			}
			this.ValidateCursorPosition();
			this.Validate();
			ViewCache cache = this.GetViewCache();
			this.UpdateRowCollectorColumns(rowCollector);
			this.UseForwardDirection = useForwardDirection;
			if ((byte)(flags & QueryRowsFlags.DoNotAdvance) == 1)
			{
				return this.CollectRowsNoAdvance(cache, rowCollector, (uint)rowCount);
			}
			return this.CollectRowsAdvance(cache, rowCollector, rowCount);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0002938C File Offset: 0x0002758C
		internal void Sort(SortTableFlags flags, ushort categoryCount, ushort expandedCount, SortOrder[] sortOrders)
		{
			base.CheckDisposed();
			this.ClearViewCache();
			this.expandedCount = 0;
			this.hasSortTableFailed = true;
			if ((this.capabilities & View.Capabilities.CanSort) != View.Capabilities.CanSort)
			{
				throw new RopExecutionException(string.Format("Sort not supported for view of type {0}.", this.ViewType), (ErrorCode)2147746050U);
			}
			RopHandler.CheckEnum<SortTableFlags>(flags);
			if (sortOrders.Length > 6)
			{
				throw new RopExecutionException(string.Format("View does not support more than {0} sort orders.", 6), (ErrorCode)2147746071U);
			}
			if (categoryCount != 0 && categoryCount < expandedCount)
			{
				throw new RopExecutionException("Number of categories cannot be less than expandedCount.", (ErrorCode)2147942487U);
			}
			foreach (SortOrder sortOrder in sortOrders)
			{
				if (!PropertyValue.IsSupportedPropertyType(sortOrder.Tag) || sortOrder.Tag.PropertyType == PropertyType.Unspecified || sortOrder.Tag.PropertyType == PropertyType.Null || sortOrder.Tag.PropertyType == PropertyType.Error)
				{
					throw new RopExecutionException(string.Format("Invalid PropertyTag used in sort order: {0}", sortOrder.Tag), (ErrorCode)2147942487U);
				}
			}
			this.UpdateSortParameters(sortOrders, (int)categoryCount);
			this.expandedCount = (int)expandedCount;
			this.hasSortTableFailed = false;
			this.isCursorPositionUndefined = false;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000294B4 File Offset: 0x000276B4
		internal void Restrict(RestrictFlags flags, Restriction restriction)
		{
			base.CheckDisposed();
			this.ClearViewCache();
			this.filter = this.defaultQueryFilter;
			this.hasRestrictFailed = true;
			if ((this.capabilities & View.Capabilities.CanRestrict) != View.Capabilities.CanRestrict)
			{
				throw new RopExecutionException(string.Format("Restrict not supported for view of type {0}.", this.ViewType), (ErrorCode)2147746050U);
			}
			RopHandler.CheckEnum<RestrictFlags>(flags);
			RestrictionHelper.ConvertRestrictionFromClient(this.Session, ref restriction, this.ViewType);
			if (restriction != null)
			{
				FilterRestrictionTranslator filterRestrictionTranslator = new FilterRestrictionTranslator(this.Session);
				QueryFilter queryFilter = filterRestrictionTranslator.Translate(restriction);
				if (this.defaultQueryFilter != null)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						this.defaultQueryFilter,
						queryFilter
					});
				}
				this.filter = queryFilter;
			}
			this.hasRestrictFailed = false;
			this.isCursorPositionUndefined = false;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00029574 File Offset: 0x00027774
		internal int SeekRow(BookmarkOrigin bookmarkOrigin, int rowCount)
		{
			base.CheckDisposed();
			if (bookmarkOrigin == BookmarkOrigin.Current)
			{
				this.ValidateCursorPosition();
			}
			this.isCursorPositionUndefined = true;
			if (bookmarkOrigin > BookmarkOrigin.End)
			{
				throw new RopExecutionException("User-defined bookmarks are not supported by SeekRow", (ErrorCode)2147942487U);
			}
			this.Validate();
			int result = this.GetViewCache().SeekRow(bookmarkOrigin, rowCount);
			this.isCursorPositionUndefined = false;
			return result;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000295C8 File Offset: 0x000277C8
		internal void SeekRowApproximate(uint numerator, uint denominator)
		{
			base.CheckDisposed();
			if (denominator == 0U)
			{
				throw new RopExecutionException("SeekRowApproximate does not accept denominator = 0", (ErrorCode)2147942487U);
			}
			if (numerator > denominator)
			{
				numerator = denominator;
			}
			int rowCount = this.GetRowCount();
			ulong num = (ulong)((long)rowCount * (long)((ulong)numerator) / (long)((ulong)denominator));
			this.SeekRow(BookmarkOrigin.Beginning, (int)num);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00029614 File Offset: 0x00027814
		internal virtual bool TryFindRow(FindRowFlags flags, Restriction restriction, BookmarkOrigin bookmarkOrigin, byte[] bookmark, RowCollector rowCollector)
		{
			base.CheckDisposed();
			if (bookmarkOrigin == BookmarkOrigin.Current)
			{
				this.ValidateCursorPosition();
			}
			this.isCursorPositionUndefined = true;
			if ((this.capabilities & View.Capabilities.CanRestrict) != View.Capabilities.CanRestrict)
			{
				throw new RopExecutionException(string.Format("Restrict not supported for views of type {0}.", this.ViewType), (ErrorCode)2147746050U);
			}
			if (restriction == null)
			{
				throw new RopExecutionException("Invalid Restriction.", (ErrorCode)2147942487U);
			}
			if (bookmarkOrigin > BookmarkOrigin.Custom)
			{
				throw new RopExecutionException("Invalid BookmarkOrigin.", (ErrorCode)2147942487U);
			}
			if (bookmarkOrigin != BookmarkOrigin.Custom != (bookmark == null || bookmark.Length == 0))
			{
				string message = string.Format("BookmarkOrigin indicated that a custom bookmark is {0}expected, but then a custom bookmark was {1}provided", (bookmarkOrigin != BookmarkOrigin.Custom) ? "not " : " ", (bookmark == null || bookmark.Length == 0) ? "not " : " ");
				throw new RopExecutionException(message, (ErrorCode)2147942487U);
			}
			RestrictionHelper.ConvertRestrictionFromClient(this.Session, ref restriction, this.ViewType);
			this.Validate();
			ViewCache viewCache = this.GetViewCache();
			this.UpdateRowCollectorColumns(rowCollector);
			PropertyValue[] serverRow;
			bool flag;
			if (bookmark != null)
			{
				flag = viewCache.FindRow(restriction, bookmark, (byte)(flags & FindRowFlags.Backward) == 0, out serverRow);
			}
			else
			{
				flag = viewCache.FindRow(restriction, (uint)bookmarkOrigin, (byte)(flags & FindRowFlags.Backward) == 0, out serverRow);
			}
			PropertyValue[] rowValues;
			if (flag && this.TryConvertOriginalRowFromServerRow(serverRow, out rowValues))
			{
				rowCollector.TryAddRow(rowValues);
			}
			this.isCursorPositionUndefined = false;
			return flag;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00029754 File Offset: 0x00027954
		internal int ExpandRow(short maxRows, StoreId categoryId, RowCollector rowCollector)
		{
			base.CheckDisposed();
			if ((this.capabilities & View.Capabilities.CanGroup) != View.Capabilities.CanGroup)
			{
				throw new RopExecutionException(string.Format("ExpandRow not supported for view of type {0}.", this.ViewType), (ErrorCode)2147746050U);
			}
			ViewCache viewCache = this.GetViewCache();
			this.UpdateRowCollectorColumns(rowCollector);
			int result = 0;
			PropertyValue[][] array = viewCache.ExpandRow((int)maxRows, categoryId, out result);
			ushort num = 0;
			PropertyValue[] rowValues;
			while ((int)num < array.Length && this.TryConvertOriginalRowFromServerRow(array[(int)num], out rowValues) && rowCollector.TryAddRow(rowValues))
			{
				num += 1;
			}
			return result;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000297D8 File Offset: 0x000279D8
		internal int CollapseRow(StoreId categoryId)
		{
			base.CheckDisposed();
			if ((this.capabilities & View.Capabilities.CanGroup) != View.Capabilities.CanGroup)
			{
				throw new RopExecutionException(string.Format("CollapseRow not supported for view of type {0}.", this.ViewType), (ErrorCode)2147746050U);
			}
			ViewCache viewCache = this.GetViewCache();
			return viewCache.CollapseRow(categoryId);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0002984C File Offset: 0x00027A4C
		internal PropertyTag[] QueryColumnsAll()
		{
			base.CheckDisposed();
			ViewCache viewCache = this.GetViewCache();
			PropertyTag[] source = viewCache.QueryColumnsAll();
			return (from propertyTag in source
			where this.ClientSideProperties.ShouldBeReturnedIfRequested(propertyTag.PropertyId)
			select this.PropertyConverter.ConvertPropertyTagToClient(propertyTag)).ToArray<PropertyTag>();
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00029895 File Offset: 0x00027A95
		public StoreSession Session
		{
			get
			{
				base.CheckDisposed();
				return this.CoreObject.Session;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000617 RID: 1559
		protected abstract StoreId? ContainerFolderId { get; }

		// Token: 0x06000618 RID: 1560 RVA: 0x000298A8 File Offset: 0x00027AA8
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<View>(this);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000298B0 File Offset: 0x00027AB0
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.viewNotificationSink);
			this.viewNotificationSink = null;
			Util.DisposeIfPresent(this.viewCache);
			base.InternalDispose();
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600061A RID: 1562
		protected abstract ICoreObject CoreObject { get; }

		// Token: 0x0600061B RID: 1563
		protected abstract IViewDataSource CreateDataSource();

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x000298D5 File Offset: 0x00027AD5
		protected TableFlags TableFlags
		{
			get
			{
				base.CheckDisposed();
				return this.tableFlags;
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000298E3 File Offset: 0x00027AE3
		protected ViewCache GetViewCache()
		{
			base.CheckDisposed();
			if (this.viewCache == null)
			{
				this.Validate();
				this.viewCache = new ViewCache(this.CreateDataSource());
				this.RegisterNotification();
			}
			return this.viewCache;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00029918 File Offset: 0x00027B18
		protected int InternalSeekRowBookmark(byte[] bookmark, int rowCount, bool wantMoveCount, out bool soughtLess, out bool positionChanged)
		{
			base.CheckDisposed();
			this.isCursorPositionUndefined = true;
			this.Validate();
			int result = this.GetViewCache().SeekRowBookmark(bookmark, rowCount, wantMoveCount, out soughtLess, out positionChanged);
			this.isCursorPositionUndefined = false;
			return result;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00029953 File Offset: 0x00027B53
		protected virtual ClientSideProperties ClientSideProperties
		{
			get
			{
				return ClientSideProperties.EmptyInstance;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0002995A File Offset: 0x00027B5A
		private IStorageObjectProperties StorageObjectProperties
		{
			get
			{
				return new CoreObjectProperties(this.CoreObject.PropertyBag);
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0002996C File Offset: 0x00027B6C
		private static bool ContainsPropertyId(PropertyTag[] tags, PropertyId propertyId)
		{
			foreach (PropertyTag propertyTag in tags)
			{
				if (propertyTag.PropertyId == propertyId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000299A8 File Offset: 0x00027BA8
		private static PropertyTag[] AddColumn(PropertyTag newColumn, PropertyTag[] columns)
		{
			PropertyTag[] array = new PropertyTag[columns.Length + 1];
			array[0] = newColumn;
			Array.Copy(columns, 0, array, 1, columns.Length);
			return array;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000299DC File Offset: 0x00027BDC
		private static bool TryFixUnresolvedPropertyDefinitions(NativeStorePropertyDefinition[] propertyDefinitions, PropertyTag[] columns)
		{
			if (columns == null || propertyDefinitions == null || propertyDefinitions.Length != columns.Length)
			{
				return false;
			}
			for (int i = 0; i < propertyDefinitions.Length; i++)
			{
				if (propertyDefinitions[i] == null)
				{
					if (!columns[i].IsNamedProperty)
					{
						return false;
					}
					propertyDefinitions[i] = GuidIdPropertyDefinition.CreateCustom(columns[i].PropertyId.ToString(), (ushort)columns[i].PropertyType, View.mapiNamespace, (int)columns[i].PropertyId, PropertyFlags.None);
				}
			}
			return true;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00029A5C File Offset: 0x00027C5C
		private PropertyValue[] ConvertOriginalRowFromServerRow(PropertyValue[] serverRow)
		{
			PropertyValue[] result;
			if (this.TryConvertOriginalRowFromServerRow(serverRow, out result))
			{
				return result;
			}
			throw new InvalidOperationException("We failed to convert the rows, because the columns are not properly set.");
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00029A80 File Offset: 0x00027C80
		private bool CollectRowsAdvance(ViewCache cache, RowCollector rowCollector, ushort rowCount)
		{
			int num = (int)(this.UseForwardDirection ? rowCount : (-(int)rowCount));
			for (ushort num2 = 0; num2 < rowCount; num2 += 1)
			{
				PropertyValue[] rowValues;
				if (!this.TryGetCurrentRow(cache, num, out rowValues))
				{
					return false;
				}
				if (!rowCollector.TryAddRow(rowValues))
				{
					break;
				}
				num -= cache.MoveNext(this.UseForwardDirection ? 1 : -1);
			}
			return true;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00029AD8 File Offset: 0x00027CD8
		private bool CollectRowsNoAdvance(ViewCache cache, RowCollector rowCollector, uint rowCount)
		{
			PropertyValue[][] array = cache.FetchNoAdvance(rowCount);
			foreach (PropertyValue[] serverRow in array)
			{
				PropertyValue[] rowValues = this.ConvertOriginalRowFromServerRow(serverRow);
				if (!rowCollector.TryAddRow(rowValues))
				{
					break;
				}
			}
			return array.Length > 0;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00029B20 File Offset: 0x00027D20
		private void RegisterNotification()
		{
			if (this.viewCache != null && this.viewNotificationSink == null && this.originalColumns != null && this.originalColumns.Length > 0)
			{
				this.viewNotificationSink = base.LogonObject.NotificationQueue.Register(this.notificationHandler, this, this.tableFlags & ~NotificationSink.NonNotificationTableFlags, this.ContainerFolderId, this.returnNotificationHandle, this.String8Encoding);
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00029B90 File Offset: 0x00027D90
		private void UpdateRowCollectorColumns(RowCollector rowCollector)
		{
			foreach (PropertyTag propertyTag in this.originalColumns)
			{
				if (propertyTag.IsMultiValueInstanceProperty && (this.sortPropertyTags == null || !View.ContainsPropertyId(this.sortPropertyTags, propertyTag.PropertyId)))
				{
					throw new RopExecutionException("MVI flag on the property that is not part of the sort order is not supported.", (ErrorCode)2147746050U);
				}
			}
			if (this.sortPropertyTags != null)
			{
				foreach (PropertyTag propertyTag2 in this.sortPropertyTags)
				{
					if (propertyTag2.IsMultiValuedProperty)
					{
						foreach (PropertyTag propertyTag3 in this.originalColumns)
						{
							if (propertyTag2.PropertyId == propertyTag3.PropertyId && !propertyTag3.IsMultiValueInstanceProperty)
							{
								throw new RopExecutionException("Sorting by MV values is not supported if the corresponding SetColumns flag is not MVI.", (ErrorCode)2147746050U);
							}
						}
					}
				}
			}
			rowCollector.SetColumns(this.originalColumns);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00029C98 File Offset: 0x00027E98
		private void Validate()
		{
			if (this.originalColumns == null || this.hasSortTableFailed || this.hasRestrictFailed)
			{
				throw new RopExecutionException(string.Format("The current view is not valid. Columns not defined = {0}. Failed sort = {1}. Failed restrict = {2}.", this.originalColumns == null, this.hasSortTableFailed, this.hasRestrictFailed), ErrorCode.NullObject);
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00029CF6 File Offset: 0x00027EF6
		private void ValidateCursorPosition()
		{
			if (this.isCursorPositionUndefined)
			{
				throw new RopExecutionException("Current cursor position is unknown.", ErrorCode.NullObject);
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00029D10 File Offset: 0x00027F10
		private void ClearViewCache()
		{
			base.CheckDisposed();
			if (this.viewNotificationSink != null)
			{
				this.viewNotificationSink.Dispose();
				this.viewNotificationSink = null;
			}
			if (this.viewCache != null)
			{
				this.viewCache.Dispose();
				this.viewCache = null;
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00029D4C File Offset: 0x00027F4C
		private void UpdateSortParameters(SortOrder[] sortOrders, int categoryCount)
		{
			this.sortBys = null;
			this.groupBys = null;
			this.sortPropertyTags = null;
			if (sortOrders.Length == 0)
			{
				return;
			}
			PropertyTag[] array = new PropertyTag[sortOrders.Length];
			for (int i = 0; i < sortOrders.Length; i++)
			{
				if (sortOrders[i].Tag.IsMultiValuedProperty && !sortOrders[i].Tag.IsMultiValueInstanceProperty)
				{
					throw new RopExecutionException("Multi value columns sort is not support without MVI flag set.", (ErrorCode)2147746071U);
				}
				array[i] = sortOrders[i].Tag;
			}
			NativeStorePropertyDefinition[] array2;
			if (!MEDSPropertyTranslator.TryGetPropertyDefinitionsFromPropertyTags(this.Session, this.CoreObject.PropertyBag, array, out array2))
			{
				throw new RopExecutionException("Properties in some SortOrders could not be resolved", (ErrorCode)2147746050U);
			}
			int num = 0;
			int num2 = 0;
			List<SortBy> list = new List<SortBy>();
			List<GroupByAndOrder> list2 = new List<GroupByAndOrder>();
			for (int j = 0; j < sortOrders.Length; j++)
			{
				SortOrder sortOrder;
				switch (sortOrders[j].Flags)
				{
				case SortOrderFlags.Ascending:
					sortOrder = SortOrder.Ascending;
					break;
				case SortOrderFlags.Descending:
					sortOrder = SortOrder.Descending;
					break;
				default:
					throw new RopExecutionException("Out of order aggregation sort entry.", (ErrorCode)2147746071U);
				}
				if (num < categoryCount)
				{
					NativeStorePropertyDefinition nativeStorePropertyDefinition = array2[j];
					NativeStorePropertyDefinition columnDefinition = nativeStorePropertyDefinition;
					Aggregate aggregate = Aggregate.Min;
					if (j + 1 < sortOrders.Length && (sortOrders[j + 1].Flags == SortOrderFlags.CategoryMaximum || sortOrders[j + 1].Flags == SortOrderFlags.CategoryMinimum))
					{
						columnDefinition = array2[j + 1];
						aggregate = ((sortOrders[j + 1].Flags == SortOrderFlags.CategoryMaximum) ? Aggregate.Max : Aggregate.Min);
						j++;
					}
					list2.Add(new GroupByAndOrder(nativeStorePropertyDefinition, new GroupSort(columnDefinition, sortOrder, aggregate)));
					num++;
					num2 = j;
				}
				else
				{
					list.Add(new SortBy(array2[j], sortOrder));
				}
			}
			if (num != categoryCount)
			{
				throw new RopExecutionException("Number of categories doesn't match the sortOrders array length.", (ErrorCode)2147746071U);
			}
			if (num2 >= 4)
			{
				throw new RopExecutionException(string.Format("View doesn't support more than {0} categories and aggregations.", 4), (ErrorCode)2147746071U);
			}
			if (list.Count != 0)
			{
				this.sortBys = list.ToArray();
			}
			if (list2.Count != 0)
			{
				this.groupBys = list2.ToArray();
			}
			this.sortPropertyTags = array;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00029F58 File Offset: 0x00028158
		private bool TryGetCurrentRow(ViewCache cache, int fetchRowCount, out PropertyValue[] row)
		{
			PropertyValue[] serverRow;
			if (cache != null && cache.TryGetCurrentRow(this.UseForwardDirection ? 0 : -1, fetchRowCount, out serverRow))
			{
				row = this.ConvertOriginalRowFromServerRow(serverRow);
				return true;
			}
			row = null;
			return false;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00029F8E File Offset: 0x0002818E
		private ViewType ViewType
		{
			get
			{
				base.CheckDisposed();
				return this.viewType;
			}
		}

		// Token: 0x04000279 RID: 633
		private const int MaximumOfCategoriesAndAggregations = 4;

		// Token: 0x0400027A RID: 634
		private const int MaximumOfSortEntries = 6;

		// Token: 0x0400027B RID: 635
		private static readonly Guid mapiNamespace = new Guid("{00020328-0000-0000-C000-000000000046}");

		// Token: 0x0400027C RID: 636
		private readonly TableFlags tableFlags;

		// Token: 0x0400027D RID: 637
		private readonly View.Capabilities capabilities;

		// Token: 0x0400027E RID: 638
		private readonly ServerObjectHandle returnNotificationHandle;

		// Token: 0x0400027F RID: 639
		private readonly NotificationHandler notificationHandler;

		// Token: 0x04000280 RID: 640
		private PropertyTag[] originalColumns;

		// Token: 0x04000281 RID: 641
		private PropertyTag[] serverColumns;

		// Token: 0x04000282 RID: 642
		private int instanceKeyColumnIndex = -1;

		// Token: 0x04000283 RID: 643
		private ViewCache viewCache;

		// Token: 0x04000284 RID: 644
		private int expandedCount;

		// Token: 0x04000285 RID: 645
		private bool hasSortTableFailed;

		// Token: 0x04000286 RID: 646
		private bool hasRestrictFailed;

		// Token: 0x04000287 RID: 647
		private bool isCursorPositionUndefined;

		// Token: 0x04000288 RID: 648
		private QueryFilter filter;

		// Token: 0x04000289 RID: 649
		private QueryFilter defaultQueryFilter;

		// Token: 0x0400028A RID: 650
		private ViewType viewType;

		// Token: 0x0400028B RID: 651
		private NotificationSink viewNotificationSink;

		// Token: 0x0400028C RID: 652
		private GroupByAndOrder[] groupBys;

		// Token: 0x0400028D RID: 653
		private SortBy[] sortBys;

		// Token: 0x0400028E RID: 654
		private PropertyTag[] sortPropertyTags;

		// Token: 0x0400028F RID: 655
		internal bool UseForwardDirection = true;

		// Token: 0x02000097 RID: 151
		internal enum RowLookupPosition : byte
		{
			// Token: 0x04000291 RID: 657
			Previous,
			// Token: 0x04000292 RID: 658
			Current
		}

		// Token: 0x02000098 RID: 152
		[Flags]
		internal enum Capabilities
		{
			// Token: 0x04000294 RID: 660
			Basic = 0,
			// Token: 0x04000295 RID: 661
			CanRestrict = 1,
			// Token: 0x04000296 RID: 662
			CanSort = 2,
			// Token: 0x04000297 RID: 663
			CanReset = 4,
			// Token: 0x04000298 RID: 664
			CanAbort = 8,
			// Token: 0x04000299 RID: 665
			CanUseBookmarks = 16,
			// Token: 0x0400029A RID: 666
			CanFreeBookmarks = 32,
			// Token: 0x0400029B RID: 667
			CanGroup = 64,
			// Token: 0x0400029C RID: 668
			All = 127
		}
	}
}
