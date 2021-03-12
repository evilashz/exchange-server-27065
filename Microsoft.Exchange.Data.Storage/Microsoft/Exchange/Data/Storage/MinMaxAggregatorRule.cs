using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008EE RID: 2286
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MinMaxAggregatorRule<T> : IAggregatorRule where T : IComparable<T>
	{
		// Token: 0x060055BA RID: 21946 RVA: 0x00162BE5 File Offset: 0x00160DE5
		public MinMaxAggregatorRule(PropertyDefinition propertyDefinition, bool isMin, T defaultValue)
		{
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			Util.ThrowOnMismatchType<T>(propertyDefinition, "propertyDefinition");
			this.propertyDefinition = propertyDefinition;
			this.isMinCompare = isMin;
			this.result = defaultValue;
			this.defaultValue = defaultValue;
		}

		// Token: 0x060055BB RID: 21947 RVA: 0x00162C1F File Offset: 0x00160E1F
		public void BeginAggregation()
		{
			this.aggregatorInitialized = false;
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x00162C28 File Offset: 0x00160E28
		public void EndAggregation()
		{
			if (!this.aggregatorInitialized)
			{
				this.result = this.defaultValue;
			}
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x00162C40 File Offset: 0x00160E40
		public void AddToAggregation(IStorePropertyBag propertyBag)
		{
			object obj = propertyBag.TryGetProperty(this.propertyDefinition);
			if (obj is T)
			{
				T other = (T)((object)obj);
				if (!this.aggregatorInitialized)
				{
					this.result = other;
					this.aggregatorInitialized = true;
					return;
				}
				int num = this.result.CompareTo(other);
				if (num > 0 && this.isMinCompare)
				{
					this.result = other;
					this.aggregatorInitialized = true;
					return;
				}
				if (num < 0 && !this.isMinCompare)
				{
					this.result = other;
					this.aggregatorInitialized = true;
				}
			}
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x060055BE RID: 21950 RVA: 0x00162CC8 File Offset: 0x00160EC8
		public T Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04002E03 RID: 11779
		private PropertyDefinition propertyDefinition;

		// Token: 0x04002E04 RID: 11780
		private bool isMinCompare;

		// Token: 0x04002E05 RID: 11781
		private bool aggregatorInitialized;

		// Token: 0x04002E06 RID: 11782
		private T result;

		// Token: 0x04002E07 RID: 11783
		private T defaultValue;
	}
}
