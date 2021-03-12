using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200006C RID: 108
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IQueryResult : IDisposable
	{
		// Token: 0x0600076E RID: 1902
		object[][] GetRows(int rowCount, out bool mightBeMoreRows);

		// Token: 0x0600076F RID: 1903
		object[][] GetRows(int rowCount, QueryRowsFlags flags, out bool mightBeMoreRows);

		// Token: 0x06000770 RID: 1904
		void SetTableColumns(ICollection<PropertyDefinition> propertyDefinitions);

		// Token: 0x06000771 RID: 1905
		int SeekToOffset(SeekReference reference, int offset);

		// Token: 0x06000772 RID: 1906
		bool SeekToCondition(SeekReference reference, QueryFilter seekFilter, SeekToConditionFlags flags);

		// Token: 0x06000773 RID: 1907
		bool SeekToCondition(SeekReference reference, QueryFilter seekFilter);

		// Token: 0x06000774 RID: 1908
		bool SeekToCondition(uint bookMark, bool useForwardDirection, QueryFilter seekFilter, SeekToConditionFlags flags);

		// Token: 0x06000775 RID: 1909
		object[][] ExpandRow(int rowCount, long categoryId, out int rowsInExpandedCategory);

		// Token: 0x06000776 RID: 1910
		int CollapseRow(long categoryId);

		// Token: 0x06000777 RID: 1911
		uint CreateBookmark();

		// Token: 0x06000778 RID: 1912
		void FreeBookmark(uint bookmarkPosition);

		// Token: 0x06000779 RID: 1913
		int SeekRowBookmark(uint bookmarkPosition, int rowCount, bool wantRowsSought, out bool soughtLess, out bool positionChanged);

		// Token: 0x0600077A RID: 1914
		NativeStorePropertyDefinition[] GetAllPropertyDefinitions(params PropertyTagPropertyDefinition[] excludeProperties);

		// Token: 0x0600077B RID: 1915
		byte[] GetCollapseState(byte[] instanceKey);

		// Token: 0x0600077C RID: 1916
		uint SetCollapseState(byte[] collapseState);

		// Token: 0x0600077D RID: 1917
		object Advise(SubscriptionSink subscriptionSink, bool asyncMode);

		// Token: 0x0600077E RID: 1918
		void Unadvise(object notificationHandle);

		// Token: 0x0600077F RID: 1919
		IStorePropertyBag[] GetPropertyBags(int rowCount);

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000780 RID: 1920
		StoreSession StoreSession { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000781 RID: 1921
		ColumnPropertyDefinitions Columns { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000782 RID: 1922
		int CurrentRow { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000783 RID: 1923
		int EstimatedRowCount { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000784 RID: 1924
		bool IsDisposed { get; }
	}
}
