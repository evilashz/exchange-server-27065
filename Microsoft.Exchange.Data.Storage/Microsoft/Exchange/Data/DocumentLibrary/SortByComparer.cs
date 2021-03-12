using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006BC RID: 1724
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SortByComparer : IComparer<object[]>
	{
		// Token: 0x06004564 RID: 17764 RVA: 0x00127434 File Offset: 0x00125634
		internal SortByComparer(SortBy[] sortBy, IList<PropertyDefinition> propertyDefinition)
		{
			Dictionary<PropertyDefinition, int> dictionary = new Dictionary<PropertyDefinition, int>();
			for (int i = 0; i < propertyDefinition.Count; i++)
			{
				dictionary[propertyDefinition[i]] = i;
			}
			this.sortBy = new List<KeyValuePair<int, int>>(sortBy.Length);
			for (int j = 0; j < sortBy.Length; j++)
			{
				int key = 0;
				if (!dictionary.TryGetValue(sortBy[j].ColumnDefinition, out key))
				{
					throw new ArgumentException("SortBy");
				}
				if (sortBy[j].ColumnDefinition.Type.GetInterface("System.IComparable") == null)
				{
					throw new ArgumentException("SortBy");
				}
				this.sortBy.Add(new KeyValuePair<int, int>(key, (sortBy[j].SortOrder == SortOrder.Ascending) ? 1 : -1));
			}
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x001274F0 File Offset: 0x001256F0
		public int Compare(object[] x, object[] y)
		{
			for (int i = 0; i < this.sortBy.Count; i++)
			{
				int num = Utils.CompareValues(x[this.sortBy[i].Key], y[this.sortBy[i].Key]);
				if (num != 0)
				{
					return this.sortBy[i].Value * num;
				}
			}
			return 0;
		}

		// Token: 0x040025F8 RID: 9720
		private List<KeyValuePair<int, int>> sortBy;
	}
}
