using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008ED RID: 2285
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CountAggregatorRule<T> : IAggregatorRule
	{
		// Token: 0x060055B5 RID: 21941 RVA: 0x00162B71 File Offset: 0x00160D71
		internal CountAggregatorRule(PropertyDefinition propertyDefinition, T value)
		{
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			Util.ThrowOnMismatchType<T>(propertyDefinition, "propertyDefinition");
			this.propertyDefinition = propertyDefinition;
			this.compareValue = value;
			this.result = 0;
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x00162BA4 File Offset: 0x00160DA4
		public void BeginAggregation()
		{
			this.result = 0;
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x00162BAD File Offset: 0x00160DAD
		public void EndAggregation()
		{
		}

		// Token: 0x060055B8 RID: 21944 RVA: 0x00162BAF File Offset: 0x00160DAF
		public void AddToAggregation(IStorePropertyBag propertyBag)
		{
			if (object.Equals(this.compareValue, propertyBag.TryGetProperty(this.propertyDefinition)))
			{
				this.result++;
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x060055B9 RID: 21945 RVA: 0x00162BDD File Offset: 0x00160DDD
		public int Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04002E00 RID: 11776
		private int result;

		// Token: 0x04002E01 RID: 11777
		private PropertyDefinition propertyDefinition;

		// Token: 0x04002E02 RID: 11778
		private T compareValue;
	}
}
