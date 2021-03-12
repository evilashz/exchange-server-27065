using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200009D RID: 157
	internal interface IViewDataSource : IDisposable
	{
		// Token: 0x06000656 RID: 1622
		int GetPosition();

		// Token: 0x06000657 RID: 1623
		int GetRowCount();

		// Token: 0x06000658 RID: 1624
		void SetColumns(NativeStorePropertyDefinition[] propertyDefinitions, PropertyTag[] propertyTags);

		// Token: 0x06000659 RID: 1625
		PropertyValue[][] GetRows(int rowCount, QueryRowsFlags flags);

		// Token: 0x0600065A RID: 1626
		int SeekRow(BookmarkOrigin bookmarkOrigin, int rowCount);

		// Token: 0x0600065B RID: 1627
		bool FindRow(Restriction restriction, uint bookmark, bool useForwardDirection);

		// Token: 0x0600065C RID: 1628
		bool FindRow(Restriction restriction, byte[] bookmark, bool useForwardDirection);

		// Token: 0x0600065D RID: 1629
		PropertyValue[][] ExpandRow(int maxRows, StoreId categoryId, out int rowsInExpandedCategory);

		// Token: 0x0600065E RID: 1630
		int CollapseRow(StoreId categoryId);

		// Token: 0x0600065F RID: 1631
		byte[] CreateBookmark();

		// Token: 0x06000660 RID: 1632
		void FreeBookmark(byte[] bookmark);

		// Token: 0x06000661 RID: 1633
		int SeekRowBookmark(byte[] bookmark, int rowCount, bool wantMoveCount, out bool soughtLess, out bool positionChanged);

		// Token: 0x06000662 RID: 1634
		PropertyTag[] QueryColumnsAll();

		// Token: 0x06000663 RID: 1635
		byte[] GetCollapseState(byte[] instanceKey);

		// Token: 0x06000664 RID: 1636
		byte[] SetCollapseState(byte[] collapseState);

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000665 RID: 1637
		IQueryResult QueryResult { get; }
	}
}
