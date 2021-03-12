using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C11 RID: 3089
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class ApplicationAggregatedProperty : SmartPropertyDefinition
	{
		// Token: 0x06006E24 RID: 28196 RVA: 0x001D9D40 File Offset: 0x001D7F40
		public ApplicationAggregatedProperty(string displayName, Type valueType, PropertyFlags propertyFlags, PropertyAggregationStrategy propertyAggregationStrategy, SortByAndFilterStrategy sortByAndFilterStrategy) : this(displayName, valueType, propertyFlags, propertyAggregationStrategy, sortByAndFilterStrategy, new SimpleVirtualPropertyDefinition("InternalStorage:" + displayName, valueType, propertyFlags, new PropertyDefinitionConstraint[0]))
		{
		}

		// Token: 0x06006E25 RID: 28197 RVA: 0x001D9D72 File Offset: 0x001D7F72
		public ApplicationAggregatedProperty(ApplicationAggregatedProperty basePropertyDefinition, PropertyAggregationStrategy propertyAggregationStrategy) : this(basePropertyDefinition.Name, basePropertyDefinition.Type, basePropertyDefinition.PropertyFlags, propertyAggregationStrategy, basePropertyDefinition.sortByAndFilterStrategy, basePropertyDefinition.aggregatedProperty)
		{
		}

		// Token: 0x06006E26 RID: 28198 RVA: 0x001D9D99 File Offset: 0x001D7F99
		private ApplicationAggregatedProperty(string displayName, Type valueType, PropertyFlags propertyFlags, PropertyAggregationStrategy propertyAggregationStrategy, SortByAndFilterStrategy sortByAndFilterStrategy, SimpleVirtualPropertyDefinition aggregatedProperty) : base(displayName, valueType, propertyFlags, PropertyDefinitionConstraint.None, propertyAggregationStrategy.Dependencies)
		{
			this.propertyAggregationStrategy = propertyAggregationStrategy;
			this.sortByAndFilterStrategy = sortByAndFilterStrategy;
			this.aggregatedProperty = aggregatedProperty;
		}

		// Token: 0x17001DD9 RID: 7641
		// (get) Token: 0x06006E27 RID: 28199 RVA: 0x001D9DC8 File Offset: 0x001D7FC8
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return this.sortByAndFilterStrategy.Capabilities;
			}
		}

		// Token: 0x06006E28 RID: 28200 RVA: 0x001D9DD5 File Offset: 0x001D7FD5
		public static IStorePropertyBag Aggregate(PropertyAggregationContext context, IEnumerable<PropertyDefinition> properties)
		{
			return ApplicationAggregatedProperty.AggregateAsPropertyBag(context, properties).AsIStorePropertyBag();
		}

		// Token: 0x06006E29 RID: 28201 RVA: 0x001D9DE4 File Offset: 0x001D7FE4
		internal static PropertyBag AggregateAsPropertyBag(PropertyAggregationContext context, IEnumerable<PropertyDefinition> properties)
		{
			MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				ApplicationAggregatedProperty applicationAggregatedProperty = propertyDefinition as ApplicationAggregatedProperty;
				if (applicationAggregatedProperty != null)
				{
					applicationAggregatedProperty.Aggregate(context, memoryPropertyBag);
				}
			}
			memoryPropertyBag.SetAllPropertiesLoaded();
			return memoryPropertyBag;
		}

		// Token: 0x06006E2A RID: 28202 RVA: 0x001D9E44 File Offset: 0x001D8044
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			NativeStorePropertyDefinition sortProperty = this.sortByAndFilterStrategy.GetSortProperty();
			if (sortProperty == null)
			{
				return base.GetSortProperty();
			}
			return sortProperty;
		}

		// Token: 0x06006E2B RID: 28203 RVA: 0x001D9E68 File Offset: 0x001D8068
		internal override SortBy[] GetNativeSortBy(SortOrder sortOrder)
		{
			SortBy[] nativeSortBy = this.sortByAndFilterStrategy.GetNativeSortBy(sortOrder);
			if (nativeSortBy == null)
			{
				return base.GetNativeSortBy(sortOrder);
			}
			return nativeSortBy;
		}

		// Token: 0x06006E2C RID: 28204 RVA: 0x001D9E90 File Offset: 0x001D8090
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			QueryFilter queryFilter = this.sortByAndFilterStrategy.NativeFilterToSmartFilter(filter, this);
			if (queryFilter == null)
			{
				return base.NativeFilterToSmartFilter(filter);
			}
			return queryFilter;
		}

		// Token: 0x06006E2D RID: 28205 RVA: 0x001D9EB8 File Offset: 0x001D80B8
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			QueryFilter queryFilter = this.sortByAndFilterStrategy.SmartFilterToNativeFilter(filter, this);
			if (queryFilter == null)
			{
				return base.SmartFilterToNativeFilter(filter);
			}
			return queryFilter;
		}

		// Token: 0x06006E2E RID: 28206 RVA: 0x001D9EDF File Offset: 0x001D80DF
		internal void Aggregate(PropertyAggregationContext context, PropertyBag target)
		{
			this.propertyAggregationStrategy.Aggregate(this.aggregatedProperty, context, target);
		}

		// Token: 0x06006E2F RID: 28207 RVA: 0x001D9EF4 File Offset: 0x001D80F4
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.GetValue(this.aggregatedProperty);
		}

		// Token: 0x04004049 RID: 16457
		private readonly SimpleVirtualPropertyDefinition aggregatedProperty;

		// Token: 0x0400404A RID: 16458
		private readonly SortByAndFilterStrategy sortByAndFilterStrategy;

		// Token: 0x0400404B RID: 16459
		private readonly PropertyAggregationStrategy propertyAggregationStrategy;
	}
}
