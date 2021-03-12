using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x020000A2 RID: 162
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ViewCache : BaseObject
	{
		// Token: 0x06000687 RID: 1671 RVA: 0x0002AA73 File Offset: 0x00028C73
		internal ViewCache(IViewDataSource dataSource)
		{
			this.dataSource = dataSource;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0002AA82 File Offset: 0x00028C82
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ViewCache>(this);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0002AA8A File Offset: 0x00028C8A
		protected override void InternalDispose()
		{
			this.dataSource.Dispose();
			base.InternalDispose();
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0002AA9D File Offset: 0x00028C9D
		public int GetPosition()
		{
			return Math.Max(this.dataSource.GetPosition() - this.serverPosition + this.position, 0);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0002AAC0 File Offset: 0x00028CC0
		public int GetRowCount()
		{
			int num = this.dataSource.GetRowCount();
			if (num == 1 && this.GetPosition() == 0)
			{
				num = this.FetchNextBatch(1);
				if (num > 0)
				{
					this.SeekRow(BookmarkOrigin.Beginning, 0);
				}
			}
			return num;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0002AAFC File Offset: 0x00028CFC
		public PropertyValue[][] FetchNoAdvance(uint rowCount)
		{
			if (rowCount > 0U)
			{
				PropertyValue[][] array2;
				if (this.IsPositionWithinCache(this.position))
				{
					int num = this.data.Length - this.position;
					PropertyValue[][] array = Array<PropertyValue[]>.Empty;
					if ((ulong)rowCount > (ulong)((long)num))
					{
						array = this.dataSource.GetRows((int)(rowCount - (uint)num), QueryRowsFlags.DoNotAdvance);
					}
					int num2 = Math.Min((int)rowCount, num + array.Length);
					array2 = new PropertyValue[num2][];
					Array.Copy(this.data, (long)this.position, array2, 0L, Math.Min((long)((ulong)rowCount), (long)num));
					if (array.Length > 0)
					{
						Array.Copy(array, 0, array2, num, array.Length);
					}
				}
				else
				{
					array2 = this.dataSource.GetRows((int)rowCount, QueryRowsFlags.DoNotAdvance);
				}
				return array2;
			}
			return Array<PropertyValue[]>.Empty;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0002ABA8 File Offset: 0x00028DA8
		public bool TryGetCurrentRow(int currentRowIndex, int fetchRowCount, out PropertyValue[] row)
		{
			int num = this.position + currentRowIndex;
			if (this.IsPositionWithinCache(num))
			{
				row = this.data[num];
				return true;
			}
			int num2 = this.FetchNextBatch(fetchRowCount);
			if (num2 > 0)
			{
				this.TryGetCurrentRow(currentRowIndex, 0, out row);
				return true;
			}
			row = null;
			return false;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0002ABEF File Offset: 0x00028DEF
		public int MoveNext(int step)
		{
			this.position += step;
			return step;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0002AC00 File Offset: 0x00028E00
		public int SeekRow(BookmarkOrigin bookmarkOrigin, int rowCount)
		{
			if (bookmarkOrigin != BookmarkOrigin.Current || this.data == null)
			{
				this.ClearCache();
				return this.dataSource.SeekRow(bookmarkOrigin, rowCount);
			}
			int num = this.position;
			this.position += rowCount;
			if (this.IsPositionWithinCache(num) && this.IsPositionWithinCache(this.position))
			{
				return rowCount;
			}
			int num2 = this.serverPosition;
			return num2 + this.SyncServerCursor(rowCount).Value - num;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0002AC75 File Offset: 0x00028E75
		public int SeekRowBookmark(byte[] bookmark, int rowCount, bool wantMoveCount, out bool soughtLess, out bool positionChanged)
		{
			return this.dataSource.SeekRowBookmark(bookmark, rowCount, wantMoveCount, out soughtLess, out positionChanged);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0002AC89 File Offset: 0x00028E89
		public bool FindRow(Restriction restriction, uint bookmark, bool useForwardDirection, out PropertyValue[] row)
		{
			if (bookmark == 1U)
			{
				this.SyncServerCursor();
			}
			if (this.dataSource.FindRow(restriction, bookmark, useForwardDirection))
			{
				this.ClearCache();
				return this.TryGetCurrentRowNoAdvance(out row);
			}
			row = null;
			return false;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0002ACBA File Offset: 0x00028EBA
		public bool FindRow(Restriction restriction, byte[] bookmark, bool useForwardDirection, out PropertyValue[] row)
		{
			this.ClearCache();
			if (this.dataSource.FindRow(restriction, bookmark, useForwardDirection))
			{
				return this.TryGetCurrentRowNoAdvance(out row);
			}
			row = null;
			return false;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0002ACE0 File Offset: 0x00028EE0
		public IViewDataSource DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0002ACE8 File Offset: 0x00028EE8
		public void SetColumns(NativeStorePropertyDefinition[] propertyDefinitions, PropertyTag[] propertyTags)
		{
			this.SyncServerCursor();
			this.dataSource.SetColumns(propertyDefinitions, propertyTags);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0002ACFE File Offset: 0x00028EFE
		public PropertyValue[][] ExpandRow(int maxRows, StoreId categoryId, out int rowsInExpandedCategory)
		{
			this.ClearCache();
			return this.DataSource.ExpandRow(maxRows, categoryId, out rowsInExpandedCategory);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0002AD14 File Offset: 0x00028F14
		public int CollapseRow(StoreId categoryId)
		{
			this.ClearCache();
			return this.DataSource.CollapseRow(categoryId);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0002AD28 File Offset: 0x00028F28
		internal PropertyTag[] QueryColumnsAll()
		{
			return this.DataSource.QueryColumnsAll();
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0002AD35 File Offset: 0x00028F35
		public byte[] GetCollapseState(byte[] instanceKey)
		{
			return this.dataSource.GetCollapseState(instanceKey);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0002AD43 File Offset: 0x00028F43
		public byte[] SetCollapseState(byte[] collapseState)
		{
			this.ClearCache();
			return this.dataSource.SetCollapseState(collapseState);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0002AD57 File Offset: 0x00028F57
		public byte[] CreateBookmark()
		{
			this.SyncServerCursor();
			return this.dataSource.CreateBookmark();
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0002AD6B File Offset: 0x00028F6B
		public void FreeBookmark(byte[] bookmark)
		{
			this.dataSource.FreeBookmark(bookmark);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0002AD7C File Offset: 0x00028F7C
		internal bool IsRowWithinUnreadCache(int instanceKeyColumnIndex, byte[] instanceKey, View.RowLookupPosition rowLookupPosition)
		{
			if (this.data == null || this.data.Length == 0 || instanceKey == null || instanceKey.Length == 0)
			{
				return false;
			}
			int num;
			int num2;
			if (rowLookupPosition == View.RowLookupPosition.Previous)
			{
				num = this.position - 1;
				num2 = this.data.Length - 1;
			}
			else
			{
				num = this.position;
				num2 = this.data.Length;
			}
			num = ((num > 0) ? num : 0);
			for (int i = num; i < num2; i++)
			{
				if (ArrayComparer<byte>.Comparer.Equals(this.data[i][instanceKeyColumnIndex].GetServerValue<byte[]>(), instanceKey))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0002AE08 File Offset: 0x00029008
		private static bool IsSameRow(PropertyValue[] row1, PropertyValue[] row2)
		{
			if (row1.Length != row2.Length)
			{
				return false;
			}
			for (int i = 0; i < row1.Length; i++)
			{
				if (row1[i].PropertyTag == PropertyTag.InstanceKey)
				{
					return row2[i].PropertyTag == row1[i].PropertyTag && ArrayComparer<byte>.Comparer.Equals(row1[i].GetValueAssert<byte[]>(), row2[i].GetValueAssert<byte[]>());
				}
			}
			return false;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0002AE8A File Offset: 0x0002908A
		private void ClearCache()
		{
			this.position = 0;
			this.serverPosition = 0;
			this.data = null;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0002AEA1 File Offset: 0x000290A1
		private bool IsPositionWithinCache(int somePosition)
		{
			return this.data != null && somePosition >= 0 && somePosition < this.data.Length;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0002AEBC File Offset: 0x000290BC
		public int FetchNextBatch(int nRowsToFetch)
		{
			this.SyncServerCursor();
			QueryRowsFlags flags = (nRowsToFetch >= 0) ? QueryRowsFlags.None : QueryRowsFlags.DoNotAdvance;
			this.FaultInjectRowsToFetchIfApplicable(ref nRowsToFetch);
			PropertyValue[][] rows = this.dataSource.GetRows(nRowsToFetch, flags);
			this.data = rows;
			if (nRowsToFetch < 0)
			{
				this.serverPosition = 0;
				this.position = this.data.Length;
			}
			else
			{
				this.serverPosition = this.data.Length;
				this.position = 0;
			}
			return rows.Length;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0002AF2C File Offset: 0x0002912C
		private void FaultInjectRowsToFetchIfApplicable(ref int nRows)
		{
			int num = 0;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(3961924925U, ref num);
			if (num != 0 && num <= Math.Abs(nRows))
			{
				nRows = ((nRows > 0) ? num : (-num));
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0002AF65 File Offset: 0x00029165
		private int? SyncServerCursor()
		{
			return this.SyncServerCursor(0);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0002AF70 File Offset: 0x00029170
		private int? SyncServerCursor(int seekRow)
		{
			int? result;
			if (this.data != null)
			{
				if (this.serverPosition == this.position)
				{
					result = new int?(0);
				}
				else
				{
					result = new int?(this.dataSource.SeekRow(BookmarkOrigin.Current, this.position - this.serverPosition));
					this.InstrumentToCaptureOutlookBlankView(seekRow);
				}
				this.ClearCache();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0002AFD8 File Offset: 0x000291D8
		private void InstrumentToCaptureOutlookBlankView(int seekRow)
		{
			if (this.data.Length != 0 && seekRow == -1 && this.IsPositionWithinCache(this.position) && !this.IsPositionWithinCache(this.position + 1))
			{
				PropertyValue[][] rows = this.dataSource.GetRows(1, QueryRowsFlags.DoNotAdvance);
				if (rows.Length == 1)
				{
					PropertyValue[] row = rows[0];
					PropertyValue[] row2 = this.data[this.data.Length - 1];
					if (!ViewCache.IsSameRow(row, row2))
					{
						this.debugInformation = row;
						WatsonHelper.SendGenericWatsonReport("Table has been changed: cached row differs from server row.", string.Format("Server cursor position: {0}, ViewCache cursor position: {1}, Cached rows: {2}", this.serverPosition, this.position, this.data.Length));
						return;
					}
				}
				else
				{
					WatsonHelper.SendGenericWatsonReport("Table has been changed - last row removed", string.Format("Server cursor position: {0}, ViewCache cursor position: {1}, Cached rows: {2}", this.serverPosition, this.position, this.data.Length));
				}
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0002B0C8 File Offset: 0x000292C8
		private bool TryGetCurrentRowNoAdvance(out PropertyValue[] row)
		{
			this.SyncServerCursor();
			PropertyValue[][] rows = this.dataSource.GetRows(1, QueryRowsFlags.DoNotAdvance);
			if (rows.Length == 1)
			{
				row = rows[0];
				return true;
			}
			row = null;
			return false;
		}

		// Token: 0x040002A8 RID: 680
		private readonly IViewDataSource dataSource;

		// Token: 0x040002A9 RID: 681
		private PropertyValue[][] data;

		// Token: 0x040002AA RID: 682
		private int position;

		// Token: 0x040002AB RID: 683
		private int serverPosition;

		// Token: 0x040002AC RID: 684
		private PropertyValue[] debugInformation;
	}
}
