using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001F2 RID: 498
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiTable : MapiUnk
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x0002162C File Offset: 0x0001F82C
		internal MapiTable(IExMapiTable iMAPITable, MapiStore mapiStore) : base(iMAPITable, null, mapiStore)
		{
			if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(13154, 27, (long)this.GetHashCode(), "MapiTable.MapiTable: this={0}", TraceUtils.MakeHash(this));
			}
			this.iMAPITable = iMAPITable;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00021665 File Offset: 0x0001F865
		protected override void MapiInternalDispose()
		{
			if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(11106, 27, (long)this.GetHashCode(), "MapiTable.InternalDispose: this={0}", TraceUtils.MakeHash(this));
			}
			this.iMAPITable = null;
			base.MapiInternalDispose();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0002169B File Offset: 0x0001F89B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiTable>(this);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000216A3 File Offset: 0x0001F8A3
		public virtual MapiNotificationHandle Advise(MapiNotificationHandler handler)
		{
			return this.Advise(handler, NotificationCallbackMode.Async);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000216B0 File Offset: 0x0001F8B0
		public virtual MapiNotificationHandle Advise(MapiNotificationHandler handler, NotificationCallbackMode mode)
		{
			base.CheckDisposed();
			base.LockStore();
			MapiNotificationHandle result;
			try
			{
				MapiNotificationHandle mapiNotificationHandle = new MapiNotificationHandle(handler);
				NotificationHelper notificationHelper = new NotificationHelper(handler, mode == NotificationCallbackMode.Sync);
				IntPtr zero = IntPtr.Zero;
				bool flag = false;
				ulong num = base.RegisterNotificationHelper(notificationHelper);
				try
				{
					int num2 = this.iMAPITable.AdviseEx(AdviseFlags.TableModified, NotificationCallbackHelper.Instance.IntPtrOnNotifyDelegate, num, out zero);
					mapiNotificationHandle.SetConnection(zero, num);
					if (num2 != 0)
					{
						base.ThrowIfError("Unable to register a table event notification callback.", num2);
					}
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						base.UnregisterNotificationHelper(num);
					}
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string>(15202, 27, (long)this.GetHashCode(), "MapiTable.Advise: handler={0}, connection={1}", TraceUtils.MakeHash(handler), zero.ToString());
				}
				result = mapiNotificationHandle;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00021794 File Offset: 0x0001F994
		public int GetRowCount()
		{
			base.CheckDisposed();
			base.LockStore();
			int result;
			try
			{
				int num = 0;
				int rowCount = this.iMAPITable.GetRowCount(0, out num);
				if (rowCount != 0)
				{
					base.ThrowIfError("Unable to get table row count.", rowCount);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(10082, 27, (long)this.GetHashCode(), "MapiTable.GetRowCount: count={0}", num.ToString());
				}
				result = num;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00021814 File Offset: 0x0001FA14
		public PropValue[] QueryOneRow(Restriction restriction, ICollection<PropTag> tags)
		{
			base.CheckDisposed();
			base.LockStore();
			PropValue[] result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string>(14178, 27, (long)this.GetHashCode(), "MapiTable.QueryOneRow params: restriction={0}, tags={1}", TraceUtils.MakeHash(restriction), TraceUtils.DumpPropTagsArray(tags));
				}
				PropValue[][] array = null;
				this.SetColumns(tags);
				if (restriction != null)
				{
					if (this.FindRow(restriction, BookMark.Beginning, FindRowFlag.DeferredErrors))
					{
						array = this.QueryRows(1, QueryRowsFlags.None);
					}
				}
				else
				{
					array = this.QueryRows(1, QueryRowsFlags.None);
				}
				if (array == null || array.Length <= 0)
				{
					throw MapiExceptionHelper.NotFoundException("Row was not found.");
				}
				PropValue[] array2 = array[0];
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(12130, 27, (long)this.GetHashCode(), "MapiTable.QueryOneRow results: propValues={0}", TraceUtils.DumpPropValsArray(array2));
				}
				result = array2;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000218E4 File Offset: 0x0001FAE4
		public object QueryOneValue(PropTag tag, Restriction restriction)
		{
			base.CheckDisposed();
			base.LockStore();
			object result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string>(16226, 27, (long)this.GetHashCode(), "MapiTable.QueryOneValue params: tag={0}, restriction={1}", TraceUtils.DumpPropTag(tag), TraceUtils.MakeHash(restriction));
				}
				PropValue[] array = this.QueryOneRow(restriction, new PropTag[]
				{
					tag
				});
				PropValue propVal = new PropValue(PropTag.Null, null);
				object obj = null;
				if (array != null && array.Length > 0)
				{
					propVal = array[0];
					obj = propVal.Value;
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(8546, 27, (long)this.GetHashCode(), "MapiTable.QueryOneValue results: value={0}", TraceUtils.DumpPropVal(propVal));
				}
				result = obj;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000219B0 File Offset: 0x0001FBB0
		public PropValue[][] QueryAllRows(Restriction restriction, ICollection<PropTag> propTags)
		{
			base.CheckDisposed();
			base.LockStore();
			PropValue[][] result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string>(12642, 27, (long)this.GetHashCode(), "MapiTable.QueryAllRows params: restriction={0}, propTags={1}", TraceUtils.MakeHash(restriction), TraceUtils.DumpPropTagsArray(propTags));
				}
				if (restriction != null)
				{
					this.Restrict(restriction);
				}
				this.SeekRow(BookMark.Beginning, 0);
				this.SetColumns(propTags);
				List<PropValue[]> list = new List<PropValue[]>();
				for (;;)
				{
					PropValue[][] array = this.QueryRows(1000);
					if (array.GetLength(0) == 0)
					{
						break;
					}
					for (int i = 0; i < array.GetLength(0); i++)
					{
						list.Add(array[i]);
					}
				}
				PropValue[][] array2 = list.ToArray();
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(10594, 27, (long)this.GetHashCode(), "MapiTable.QueryAllRows results: result={0}", TraceUtils.DumpPropValsMatrix(array2));
				}
				result = array2;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00021A94 File Offset: 0x0001FC94
		public int QueryPosition()
		{
			base.CheckDisposed();
			base.LockStore();
			int result;
			try
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = this.iMAPITable.QueryPosition(ref num, ref num2, ref num3);
				if (num4 != 0)
				{
					base.ThrowIfError("Unable to query table position.", num4);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(14690, 27, (long)this.GetHashCode(), "MapiTable.QueryPosition results: pos={0}", num.ToString());
				}
				result = num;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00021B1C File Offset: 0x0001FD1C
		public virtual void SetColumns(ICollection<PropTag> propTags)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(9570, 27, (long)this.GetHashCode(), "MapiTable.SetColumns params: propTags={0}", TraceUtils.DumpPropTagsArray(propTags));
				}
				PropTag[] lpPropTagArray = PropTagHelper.SPropTagArray(propTags);
				int num = this.iMAPITable.SetColumns(lpPropTagArray, 0);
				if (num != 0)
				{
					base.ThrowIfError("Unable to set table columns.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00021B9C File Offset: 0x0001FD9C
		public int SeekRow(BookMark bookmark, int crowsSeek)
		{
			base.CheckDisposed();
			base.LockStore();
			int result;
			try
			{
				int num = 0;
				int num2 = this.iMAPITable.SeekRow((uint)bookmark, crowsSeek, ref num);
				if (num2 != 0)
				{
					base.ThrowIfError("Unable to seek table row.", num2);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string, string>(13666, 27, (long)this.GetHashCode(), "MapiTable.SeekRow: bookmark={0}, crowsSeek={1}, result={2}", bookmark.ToString(), crowsSeek.ToString(), num.ToString());
				}
				result = num;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00021C2C File Offset: 0x0001FE2C
		public int SeekRowBookmark(BookMark bookmark, int crowsSeek, bool fWantRowsSought, out bool fSoughtLess, out bool fPositionChanged)
		{
			base.CheckDisposed();
			base.LockStore();
			int result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<BookMark, int, bool>(42595, 27, (long)this.GetHashCode(), "MapiTable.SeekRowBookmark params: bookmark={0}, crowsSeek={1}, fWantRowsSought={2}.", bookmark, crowsSeek, fWantRowsSought);
				}
				int num = 0;
				int num2 = this.iMAPITable.SeekRowBookmark((uint)bookmark, crowsSeek, fWantRowsSought, out fSoughtLess, ref num, out fPositionChanged);
				if (num2 != 0)
				{
					base.ThrowIfError("Unable to seek table bookmark.", num2);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<bool, int, bool>(58979, 27, (long)this.GetHashCode(), "MapiTable.SeekRowBookmark: fSoughtLess={0}, crowsSought={1}, fPositionChanged={2}", fSoughtLess, num, fPositionChanged);
				}
				result = num;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00021CD4 File Offset: 0x0001FED4
		public virtual bool FindRow(Restriction restriction, BookMark bookmark, FindRowFlag flag)
		{
			base.CheckDisposed();
			if (restriction == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("restriction");
			}
			base.LockStore();
			bool result;
			try
			{
				int num = this.iMAPITable.FindRow(restriction, (uint)bookmark, (int)flag);
				bool flag2 = true;
				if (num == -2147221233)
				{
					flag2 = false;
				}
				else if (num != 0)
				{
					base.ThrowIfError("Unable to find table row.", num);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace(11618, 27, (long)this.GetHashCode(), "MapiTable.FindRow: restriction={0}, bookmark={1}, flag={2}, result={3}", new object[]
					{
						TraceUtils.MakeHash(restriction),
						bookmark.ToString(),
						flag.ToString(),
						flag2.ToString()
					});
				}
				result = flag2;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00021D9C File Offset: 0x0001FF9C
		public virtual void SortTable(SortOrder sortOrder, SortTableFlags flags)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string>(15714, 27, (long)this.GetHashCode(), "MapiTable.SortTable: sortOrder={0}, flags={1}", TraceUtils.MakeString(sortOrder), flags.ToString());
				}
				int num = this.iMAPITable.SortTable(sortOrder, (int)flags);
				if (num != 0)
				{
					base.ThrowIfError("Unable to sort table.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00021E20 File Offset: 0x00020020
		public void Restrict(Restriction restriction)
		{
			this.Restrict(restriction, RestrictFlags.Batch);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00021E2C File Offset: 0x0002002C
		public virtual void Restrict(Restriction restriction, RestrictFlags flags)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(8802, 27, (long)this.GetHashCode(), "MapiTable.Restrict params: restriction={0}", TraceUtils.MakeHash(restriction));
				}
				int num = this.iMAPITable.Restrict(restriction, (int)flags);
				if (num != 0)
				{
					base.ThrowIfError("Unable to set restriction on table.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00021EA4 File Offset: 0x000200A4
		public void GetStatus(out TableStatus status, out TableType type)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace(21842, 27, (long)this.GetHashCode(), "MapiTable.GetStatus");
				}
				int num = 0;
				int num2 = 0;
				int status2 = this.iMAPITable.GetStatus(out num, out num2);
				if (status2 != 0)
				{
					base.ThrowIfError("Unable to get status on table.", status2);
				}
				status = (TableStatus)num;
				type = (TableType)num2;
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00021F20 File Offset: 0x00020120
		public void InitializeTable(Restriction restriction, SortOrder sortOrder, params PropTag[] tags)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string, string>(12898, 27, (long)this.GetHashCode(), "MapiTable.InitializeTable params: restriction={0}, sortOrder={1}, propTags={2}", TraceUtils.MakeHash(restriction), TraceUtils.MakeString(sortOrder), TraceUtils.DumpPropTagsArray(tags));
				}
				if (tags.Length > 0)
				{
					this.SetColumns(tags);
				}
				if (restriction != null)
				{
					this.Restrict(restriction);
				}
				if (sortOrder != null)
				{
					this.SortTable(sortOrder, SortTableFlags.None);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00021FA8 File Offset: 0x000201A8
		public PropValue[][] QueryRows(int crows)
		{
			return this.QueryRows(crows, QueryRowsFlags.None);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00021FB4 File Offset: 0x000201B4
		public virtual PropValue[][] QueryRows(int crows, QueryRowsFlags flags)
		{
			base.CheckDisposed();
			base.LockStore();
			PropValue[][] result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string>(10850, 27, (long)this.GetHashCode(), "MapiTable.QueryRows params: crows={0}, flags={1}", crows.ToString(), flags.ToString());
				}
				PropValue[][] array;
				int num = this.iMAPITable.QueryRows(crows, (int)flags, out array);
				if (num != 0)
				{
					base.ThrowIfError("Unable to query table rows.", num);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(14946, 27, (long)this.GetHashCode(), "MapiTable.QueryRows results: result={0}", TraceUtils.DumpPropValsMatrix(array));
				}
				result = array;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00022064 File Offset: 0x00020264
		public virtual PropValue[][] ExpandRow(long categoryId, int maxRows, int flags, out int expandedRows)
		{
			base.CheckDisposed();
			base.LockStore();
			PropValue[][] result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string>(43827, 27, (long)this.GetHashCode(), "MapiTable.ExpandRow params: rowCount={0}, flags={1}", maxRows.ToString(), flags.ToString());
				}
				PropValue[][] array;
				int num = this.iMAPITable.ExpandRow(categoryId, maxRows, flags, out array, out expandedRows);
				if (num != 0)
				{
					base.ThrowIfError("Unable to expand table row.", num);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(60211, 27, (long)this.GetHashCode(), "MapiTable.ExpandRow results: result={0}", TraceUtils.DumpPropValsMatrix(array));
				}
				result = array;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00022114 File Offset: 0x00020314
		public virtual int CollapseRow(long categoryId, int flags)
		{
			base.CheckDisposed();
			base.LockStore();
			int result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(35635, 27, (long)this.GetHashCode(), "MapiTable.CollapseRow params: flags={0}", flags.ToString());
				}
				int num = 0;
				int num2 = this.iMAPITable.CollapseRow(categoryId, flags, out num);
				if (num2 != 0)
				{
					base.ThrowIfError("Unable to collapse table row.", num2);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(52019, 27, (long)this.GetHashCode(), "MapiTable.CollapseRow results: result={0}", num.ToString());
				}
				result = num;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000221BC File Offset: 0x000203BC
		public virtual BookMark CreateBookmark()
		{
			base.CheckDisposed();
			base.LockStore();
			BookMark result;
			try
			{
				uint num2;
				int num = this.iMAPITable.CreateBookmark(out num2);
				if (num != 0)
				{
					base.ThrowIfError("Unable to create bookmark.", num);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<BookMark>(38499, 27, (long)this.GetHashCode(), "MapiTable.CreateBookmark results: bookmark={0}", (BookMark)num2);
				}
				result = (BookMark)num2;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00022230 File Offset: 0x00020430
		public virtual void FreeBookmark(BookMark bookmark)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<BookMark>(54883, 27, (long)this.GetHashCode(), "MapiTable.FreeBookmark params: bookmark={0}", bookmark);
				}
				int num = this.iMAPITable.FreeBookmark((uint)bookmark);
				if (num != 0)
				{
					base.ThrowIfError("Unable to free bookmark.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000222A0 File Offset: 0x000204A0
		public virtual byte[] GetCollapseState(byte[] instanceKey)
		{
			base.CheckDisposed();
			base.LockStore();
			byte[] result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(50611, 27, (long)this.GetHashCode(), "MapiTable.GetCollapseState params: instanceKey={0}", TraceUtils.DumpBytes(instanceKey));
				}
				byte[] array;
				int collapseState = this.iMAPITable.GetCollapseState(instanceKey, out array);
				if (collapseState != 0)
				{
					base.ThrowIfError("Unable to get collapse state on table.", collapseState);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(47539, 27, (long)this.GetHashCode(), "MapiTable.GetCollapseState results: collapseState={0}", TraceUtils.DumpBytes(array));
				}
				result = array;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00022344 File Offset: 0x00020544
		public virtual BookMark SetCollapseState(byte[] collapseState)
		{
			base.CheckDisposed();
			base.LockStore();
			BookMark result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(63923, 27, (long)this.GetHashCode(), "MapiTable.SetCollapseState params: collapseState={0}", TraceUtils.DumpBytes(collapseState));
				}
				uint num2;
				int num = this.iMAPITable.SetCollapseState(collapseState, out num2);
				if (num != 0)
				{
					base.ThrowIfError("Unable to set collapse state on table.", num);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<BookMark>(39347, 27, (long)this.GetHashCode(), "MapiTable.SetCollapseState results: bookmark={0}", (BookMark)num2);
				}
				result = (BookMark)num2;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000223E0 File Offset: 0x000205E0
		public virtual PropTag[] QueryColumns(QueryColumnsFlags flags)
		{
			base.CheckDisposed();
			base.LockStore();
			PropTag[] result;
			try
			{
				PropTag[] array = null;
				int num = this.iMAPITable.QueryColumns((int)flags, out array);
				if (num != 0)
				{
					base.ThrowIfError("Unable to get table property list.", num);
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(9826, 27, (long)this.GetHashCode(), "MapiTable.QueryColumns results: tagsRet={0}", TraceUtils.DumpPropTagsArray(array));
				}
				result = array;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0002245C File Offset: 0x0002065C
		public virtual void Unadvise(MapiNotificationHandle handle)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				if (handle.IsValid)
				{
					if (ComponentTrace<MapiNetTags>.CheckEnabled(27))
					{
						ComponentTrace<MapiNetTags>.Trace<string>(13922, 27, (long)this.GetHashCode(), "MapiTable.Unadvise params: connection={0}", handle.Connection.ToString());
					}
					base.UnregisterNotificationHelper(handle.NotificationCallbackId);
					int num = this.iMAPITable.Unadvise(handle.Connection);
					handle.MarkAsInvalid();
					if (num != 0)
					{
						base.ThrowIfError("Unable to unregister a table event notification callback.", num);
					}
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x040006CC RID: 1740
		private IExMapiTable iMAPITable;
	}
}
