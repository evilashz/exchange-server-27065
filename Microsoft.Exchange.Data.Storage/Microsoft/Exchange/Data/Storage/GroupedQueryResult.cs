using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007A8 RID: 1960
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupedQueryResult : QueryResult, IDisposable
	{
		// Token: 0x060049EB RID: 18923 RVA: 0x00135352 File Offset: 0x00133552
		internal GroupedQueryResult(MapiTable mapiTable, PropertyDefinition[] propertyDefinitions, IList<PropTag> alteredProperties, int rowTypeColumnIndex, bool isRowTypeInOriginalDataColumns, StoreSession session, int estimatedItemCount, SortOrder sortOrder) : base(mapiTable, propertyDefinitions, alteredProperties, session, true, sortOrder)
		{
			this.estimatedItemCount = estimatedItemCount;
			this.rowTypeColumnIndex = rowTypeColumnIndex;
			this.isRowTypeInOriginalDataColumns = isRowTypeInOriginalDataColumns;
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x0013537A File Offset: 0x0013357A
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<GroupedQueryResult>(this);
		}

		// Token: 0x060049ED RID: 18925 RVA: 0x00135384 File Offset: 0x00133584
		public object[][][] GetViewArray(int rowCount)
		{
			object[][] rows = base.GetRows(rowCount);
			List<object[][]> list = new List<object[][]>();
			List<object[]> list2 = null;
			foreach (object[] row in rows)
			{
				int rowType = this.GetRowType(row);
				if (rowType == 1)
				{
					if (list2 == null)
					{
						list2 = new List<object[]>();
					}
					list2.Add(this.TrimRow(row));
				}
				else if (list2 != null)
				{
					list.Add(list2.ToArray());
					list2 = null;
				}
			}
			if (list2 != null)
			{
				list.Add(list2.ToArray());
			}
			return list.ToArray();
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x060049EE RID: 18926 RVA: 0x0013540B File Offset: 0x0013360B
		public int EstimatedItemCount
		{
			get
			{
				base.CheckDisposed("EstimatedItemCount");
				return this.estimatedItemCount;
			}
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x00135420 File Offset: 0x00133620
		private int GetRowType(object[] row)
		{
			object obj = row[this.rowTypeColumnIndex];
			if (obj is int)
			{
				return (int)obj;
			}
			ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), "GroupedQueryResult::GetRowType. Invalid row type. Row type found = {0}.", new object[]
			{
				obj
			});
			throw new CorruptDataException(ServerStrings.ExFoundInvalidRowType);
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x00135474 File Offset: 0x00133674
		private object[] TrimRow(object[] row)
		{
			object[] array = row;
			if (!this.isRowTypeInOriginalDataColumns)
			{
				array = new object[row.Length - 1];
				Array.Copy(row, 0, array, 0, array.Length);
			}
			return array;
		}

		// Token: 0x040027E5 RID: 10213
		private readonly int rowTypeColumnIndex;

		// Token: 0x040027E6 RID: 10214
		private readonly bool isRowTypeInOriginalDataColumns;

		// Token: 0x040027E7 RID: 10215
		private readonly int estimatedItemCount;
	}
}
