using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008F0 RID: 2288
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MultiValuedUnionAggregator<T> : IAggregatorRule
	{
		// Token: 0x060055C5 RID: 21957 RVA: 0x00162E44 File Offset: 0x00161044
		public MultiValuedUnionAggregator(PropertyDefinition propertyDefinition)
		{
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			this.propertyDefinition = propertyDefinition;
			if (!typeof(ICollection<T>).GetTypeInfo().IsAssignableFrom(this.propertyDefinition.Type.GetTypeInfo()))
			{
				throw new ArgumentException("propertyDefinition");
			}
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x00162EB0 File Offset: 0x001610B0
		public void BeginAggregation()
		{
			this.result.Clear();
			this.propertyBags.Clear();
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x00162EC8 File Offset: 0x001610C8
		public void EndAggregation()
		{
			Set<T> set = new Set<T>();
			for (int i = 0; i < this.propertyBags.Count; i++)
			{
				object obj = this.propertyBags[i].TryGetProperty(this.propertyDefinition);
				ICollection<T> collection = obj as ICollection<T>;
				if (collection != null)
				{
					foreach (T t in collection)
					{
						if (!set.Contains(t))
						{
							this.result.Add(t);
							set.Add(t);
						}
					}
				}
			}
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x00162F6C File Offset: 0x0016116C
		public void AddToAggregation(IStorePropertyBag propertyBag)
		{
			this.propertyBags.Add(propertyBag);
		}

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x060055C9 RID: 21961 RVA: 0x00162F7A File Offset: 0x0016117A
		public List<T> Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04002E0C RID: 11788
		private List<T> result = new List<T>();

		// Token: 0x04002E0D RID: 11789
		private List<IStorePropertyBag> propertyBags = new List<IStorePropertyBag>();

		// Token: 0x04002E0E RID: 11790
		private PropertyDefinition propertyDefinition;
	}
}
