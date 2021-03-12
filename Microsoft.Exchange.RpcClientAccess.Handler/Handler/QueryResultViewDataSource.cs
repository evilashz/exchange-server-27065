using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200009F RID: 159
	internal sealed class QueryResultViewDataSource : BaseObject, IViewDataSource, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x0002A6AB File Offset: 0x000288AB
		internal QueryResultViewDataSource(StoreSession session, PropertyTag[] propertyTags, IQueryResult queryResult, bool useUnicodeForRestrictions)
		{
			this.session = session;
			this.propertyTags = propertyTags;
			this.queryResult = queryResult;
			this.useUnicodeForRestrictions = useUnicodeForRestrictions;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0002A6D0 File Offset: 0x000288D0
		public void SetColumns(NativeStorePropertyDefinition[] propertyDefinitions, PropertyTag[] propertyTags)
		{
			this.propertyTags = propertyTags;
			this.queryResult.SetTableColumns(propertyDefinitions);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0002A6E5 File Offset: 0x000288E5
		public int GetPosition()
		{
			return this.queryResult.CurrentRow;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0002A6F2 File Offset: 0x000288F2
		public int GetRowCount()
		{
			return this.queryResult.EstimatedRowCount;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0002A700 File Offset: 0x00028900
		public PropertyValue[][] GetRows(int rowCount, QueryRowsFlags flags)
		{
			bool flag;
			object[][] xsoRows = ((byte)(flags & QueryRowsFlags.DoNotAdvance) == 1) ? this.queryResult.GetRows(rowCount, QueryRowsFlags.NoAdvance, out flag) : this.queryResult.GetRows(rowCount, QueryRowsFlags.None, out flag);
			return this.ConvertFromXsoRows(xsoRows);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0002A73C File Offset: 0x0002893C
		public int SeekRow(BookmarkOrigin bookmarkOrigin, int rowCount)
		{
			SeekReference reference;
			QueryResultViewDataSource.BookmarkAndDirectionToSeekReference((uint)bookmarkOrigin, rowCount >= 0, out reference);
			return this.queryResult.SeekToOffset(reference, rowCount);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0002A768 File Offset: 0x00028968
		public bool FindRow(Restriction restriction, byte[] bookmark, bool useForwardDirection)
		{
			Util.ThrowOnNullArgument(bookmark, "bookmark");
			uint bookmark2 = QueryResultViewDataSource.BookmarkToBookmarkPosition(bookmark);
			return this.FindRow(restriction, bookmark2, useForwardDirection);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0002A790 File Offset: 0x00028990
		public bool FindRow(Restriction restriction, uint bookmark, bool useForwardDirection)
		{
			FilterRestrictionTranslator filterRestrictionTranslator = new FilterRestrictionTranslator(this.session);
			QueryFilter seekFilter = filterRestrictionTranslator.Translate(restriction);
			if ((bookmark & 4U) == 4U)
			{
				throw new ArgumentOutOfRangeException("bookmark");
			}
			return this.queryResult.SeekToCondition(bookmark, useForwardDirection, seekFilter, SeekToConditionFlags.AllowExtendedFilters | SeekToConditionFlags.AllowExtendedSeekReferences | SeekToConditionFlags.KeepCursorPositionWhenNoMatch);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0002A7D4 File Offset: 0x000289D4
		public PropertyValue[][] ExpandRow(int maxRows, StoreId categoryId, out int rowsInExpandedCategory)
		{
			object[][] xsoRows = this.queryResult.ExpandRow(maxRows, categoryId, out rowsInExpandedCategory);
			return this.ConvertFromXsoRows(xsoRows);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0002A7FC File Offset: 0x000289FC
		public int CollapseRow(StoreId categoryId)
		{
			return this.queryResult.CollapseRow(categoryId);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0002A81C File Offset: 0x00028A1C
		public byte[] CreateBookmark()
		{
			uint bookmarkPosition = this.queryResult.CreateBookmark();
			return QueryResultViewDataSource.BookmarkPositionToBookmark(bookmarkPosition);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0002A83C File Offset: 0x00028A3C
		public void FreeBookmark(byte[] bookmark)
		{
			Util.ThrowOnNullArgument(bookmark, "bookmark");
			uint bookmarkPosition = QueryResultViewDataSource.BookmarkToBookmarkPosition(bookmark);
			this.queryResult.FreeBookmark(bookmarkPosition);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0002A868 File Offset: 0x00028A68
		public int SeekRowBookmark(byte[] bookmark, int rowCount, bool wantMoveCount, out bool soughtLess, out bool positionChanged)
		{
			Util.ThrowOnNullArgument(bookmark, "bookmark");
			uint bookmarkPosition = QueryResultViewDataSource.BookmarkToBookmarkPosition(bookmark);
			return this.queryResult.SeekRowBookmark(bookmarkPosition, rowCount, wantMoveCount, out soughtLess, out positionChanged);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0002A89C File Offset: 0x00028A9C
		public PropertyTag[] QueryColumnsAll()
		{
			bool useUnicodeType = true;
			return MEDSPropertyTranslator.PropertyTagsFromPropertyDefinitions<NativeStorePropertyDefinition>(this.session, this.QueryResult.GetAllPropertyDefinitions(new PropertyTagPropertyDefinition[]
			{
				CoreItemSchema.XMsExchOrganizationAVStampMailbox
			}), useUnicodeType).ToArray<PropertyTag>();
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0002A8D7 File Offset: 0x00028AD7
		public byte[] GetCollapseState(byte[] instanceKey)
		{
			return this.queryResult.GetCollapseState(instanceKey);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0002A8E8 File Offset: 0x00028AE8
		public byte[] SetCollapseState(byte[] collapseState)
		{
			uint bookmarkPosition = this.queryResult.SetCollapseState(collapseState);
			return QueryResultViewDataSource.BookmarkPositionToBookmark(bookmarkPosition);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0002A908 File Offset: 0x00028B08
		public IQueryResult QueryResult
		{
			get
			{
				return this.queryResult;
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0002A910 File Offset: 0x00028B10
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<QueryResultViewDataSource>(this);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0002A918 File Offset: 0x00028B18
		protected override void InternalDispose()
		{
			this.queryResult.Dispose();
			base.InternalDispose();
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0002A92B File Offset: 0x00028B2B
		private static void BookmarkAndDirectionToSeekReference(uint bookmark, bool useForwardDirection, out SeekReference seekReference)
		{
			seekReference = (SeekReference)bookmark;
			if ((seekReference & SeekReference.SeekBackward) == SeekReference.SeekBackward)
			{
				throw new ArgumentOutOfRangeException("bookmark");
			}
			if (!useForwardDirection)
			{
				seekReference |= SeekReference.SeekBackward;
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0002A94C File Offset: 0x00028B4C
		private static uint BookmarkToBookmarkPosition(byte[] bookmark)
		{
			if (bookmark.Length != 4)
			{
				throw new RopExecutionException(string.Format("Invalid bookmark {0}.", new ArrayTracer<byte>(bookmark)), (ErrorCode)2147746821U);
			}
			uint num = BitConverter.ToUInt32(bookmark, 0);
			if ((num & 7U) != 0U)
			{
				throw new RopExecutionException(string.Format("Invalid bookmark {0}.", new ArrayTracer<byte>(bookmark)), (ErrorCode)2147746821U);
			}
			return num;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0002A9A3 File Offset: 0x00028BA3
		private static byte[] BookmarkPositionToBookmark(uint bookmarkPosition)
		{
			return BitConverter.GetBytes(bookmarkPosition);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0002A9AC File Offset: 0x00028BAC
		private PropertyValue[][] ConvertFromXsoRows(object[][] xsoRows)
		{
			PropertyValue[][] array = new PropertyValue[xsoRows.Length][];
			for (int i = 0; i < xsoRows.Length; i++)
			{
				array[i] = MEDSPropertyTranslator.TranslatePropertyValues(this.session, this.propertyTags, xsoRows[i], this.useUnicodeForRestrictions);
			}
			return array;
		}

		// Token: 0x040002A0 RID: 672
		private readonly StoreSession session;

		// Token: 0x040002A1 RID: 673
		private readonly IQueryResult queryResult;

		// Token: 0x040002A2 RID: 674
		private readonly bool useUnicodeForRestrictions;

		// Token: 0x040002A3 RID: 675
		private PropertyTag[] propertyTags;
	}
}
