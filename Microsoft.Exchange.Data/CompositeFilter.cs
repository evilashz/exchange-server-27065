using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	internal abstract class CompositeFilter : QueryFilter
	{
		// Token: 0x06000179 RID: 377 RVA: 0x00006B9C File Offset: 0x00004D9C
		public CompositeFilter(bool ignoreWhenVerifyingMaxDepth, params QueryFilter[] filters)
		{
			if (filters == null)
			{
				throw new ArgumentNullException("filters");
			}
			for (int i = 0; i < filters.Length; i++)
			{
				if (filters[i] == null)
				{
					throw new ArgumentNullException("filters[" + i + "]");
				}
			}
			this.filters = filters;
			this.ignoreWhenVerifyingMaxDepth = ignoreWhenVerifyingMaxDepth;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00006BF9 File Offset: 0x00004DF9
		public ReadOnlyCollection<QueryFilter> Filters
		{
			get
			{
				if (this.filterCollection == null)
				{
					this.filterCollection = new ReadOnlyCollection<QueryFilter>(this.filters);
				}
				return this.filterCollection;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006C1A File Offset: 0x00004E1A
		public int FilterCount
		{
			get
			{
				return this.filters.Length;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006C24 File Offset: 0x00004E24
		public override bool Equals(object obj)
		{
			CompositeFilter compositeFilter = obj as CompositeFilter;
			if (compositeFilter != null && compositeFilter.filters.Length == this.filters.Length && compositeFilter.GetType() == base.GetType() && compositeFilter.GetHashCode() == this.GetHashCode())
			{
				for (int i = 0; i < this.filters.Length; i++)
				{
					if (Array.IndexOf<QueryFilter>(compositeFilter.filters, this.filters[i]) == -1)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006C9C File Offset: 0x00004E9C
		protected QueryFilter[] CloneFiltersWithPropertyReplacement(IDictionary<PropertyDefinition, PropertyDefinition> propertyMap)
		{
			QueryFilter[] array = new QueryFilter[this.filters.Length];
			for (int i = 0; i < this.filters.Length; i++)
			{
				array[i] = this.filters[i].CloneWithPropertyReplacement(propertyMap);
			}
			return array;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006CDC File Offset: 0x00004EDC
		public override int GetHashCode()
		{
			if (this.hashCode == null)
			{
				this.hashCode = new int?(base.GetType().GetHashCode());
				for (int i = 0; i < this.filters.Length; i++)
				{
					this.hashCode ^= this.filters[i].GetHashCode();
				}
			}
			return this.hashCode.Value;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006D68 File Offset: 0x00004F68
		public override IEnumerable<string> Keywords()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < this.filters.Length; i++)
			{
				list.AddRange(this.filters[i].Keywords());
			}
			return list;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006DA4 File Offset: 0x00004FA4
		internal override IEnumerable<PropertyDefinition> FilterProperties()
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			for (int i = 0; i < this.filters.Length; i++)
			{
				foreach (PropertyDefinition item in this.filters[i].FilterProperties())
				{
					hashSet.Add(item);
				}
			}
			return hashSet;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00006E14 File Offset: 0x00005014
		public override string PropertyName
		{
			get
			{
				return this.Filters[0].PropertyName;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006E28 File Offset: 0x00005028
		protected override int GetSize()
		{
			int num = base.GetSize();
			int num2 = this.filters.Length;
			for (int i = 0; i < num2; i++)
			{
				num += this.filters[i].Size;
			}
			return num;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00006E62 File Offset: 0x00005062
		public bool IgnoreWhenVerifyingMaxDepth
		{
			get
			{
				return this.ignoreWhenVerifyingMaxDepth;
			}
		}

		// Token: 0x04000089 RID: 137
		private int? hashCode;

		// Token: 0x0400008A RID: 138
		private ReadOnlyCollection<QueryFilter> filterCollection;

		// Token: 0x0400008B RID: 139
		private bool ignoreWhenVerifyingMaxDepth;

		// Token: 0x0400008C RID: 140
		protected readonly QueryFilter[] filters;
	}
}
