using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008EF RID: 2287
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnionAggregatorRule<T> : IAggregatorRule
	{
		// Token: 0x060055BF RID: 21951 RVA: 0x00162CD0 File Offset: 0x00160ED0
		public UnionAggregatorRule(PropertyDefinition propertyDefinition, params SortBy[] sortByFields)
		{
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			Util.ThrowOnMismatchType<T>(propertyDefinition, "propertyDefinition");
			this.propertyDefinition = propertyDefinition;
			this.sortByFields = sortByFields;
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x00162D1D File Offset: 0x00160F1D
		public void BeginAggregation()
		{
			this.result.Clear();
			this.propertyBags.Clear();
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x00162D38 File Offset: 0x00160F38
		public void EndAggregation()
		{
			if (this.sortByFields != null)
			{
				this.propertyBags.Sort(new Comparison<IStorePropertyBag>(this.ComparePropertyBags));
			}
			Set<T> set = new Set<T>();
			for (int i = 0; i < this.propertyBags.Count; i++)
			{
				object obj = this.propertyBags[i].TryGetProperty(this.propertyDefinition);
				if (obj is T)
				{
					T t = (T)((object)obj);
					if (!set.Contains(t))
					{
						this.result.Add(t);
						set.Add(t);
					}
				}
			}
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x00162DC3 File Offset: 0x00160FC3
		public void AddToAggregation(IStorePropertyBag propertyBag)
		{
			this.propertyBags.Add(propertyBag);
		}

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x060055C3 RID: 21955 RVA: 0x00162DD1 File Offset: 0x00160FD1
		public List<T> Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x00162DDC File Offset: 0x00160FDC
		private int ComparePropertyBags(IStorePropertyBag leftPropertyBag, IStorePropertyBag rightPropertyBag)
		{
			int i = 0;
			while (i < this.sortByFields.Length)
			{
				int num = Util.CompareValues(leftPropertyBag.TryGetProperty(this.sortByFields[i].ColumnDefinition), rightPropertyBag.TryGetProperty(this.sortByFields[i].ColumnDefinition));
				if (num != 0)
				{
					if (this.sortByFields[i].SortOrder == SortOrder.Ascending)
					{
						return num;
					}
					return -1 * num;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x04002E08 RID: 11784
		private SortBy[] sortByFields;

		// Token: 0x04002E09 RID: 11785
		private List<T> result = new List<T>();

		// Token: 0x04002E0A RID: 11786
		private List<IStorePropertyBag> propertyBags = new List<IStorePropertyBag>();

		// Token: 0x04002E0B RID: 11787
		private PropertyDefinition propertyDefinition;
	}
}
