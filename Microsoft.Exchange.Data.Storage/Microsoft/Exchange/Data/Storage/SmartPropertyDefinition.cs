using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200048B RID: 1163
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal abstract class SmartPropertyDefinition : StorePropertyDefinition
	{
		// Token: 0x0600339C RID: 13212 RVA: 0x000D1B90 File Offset: 0x000CFD90
		protected SmartPropertyDefinition(string displayName, Type valueType, PropertyFlags flags, PropertyDefinitionConstraint[] constraints, params PropertyDependency[] dependencies) : base(PropertyTypeSpecifier.Calculated, displayName, valueType, SmartPropertyDefinition.CalculateSmartPropertyFlags(flags), constraints)
		{
			for (int i = 0; i < dependencies.Length; i++)
			{
			}
			this.dependencies = dependencies;
			this.RegisterFilterTranslation();
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x000D1BD4 File Offset: 0x000CFDD4
		public override ICollection<PropertyDefinition> RequiredPropertyDefinitionsWhenReading
		{
			get
			{
				if (this.requiredPropertyDefinitionsWhenReading == null)
				{
					this.requiredPropertyDefinitionsWhenReading = new List<PropertyDefinition>();
					foreach (PropertyDependency propertyDependency in this.Dependencies)
					{
						if ((propertyDependency.Type & PropertyDependencyType.NeedForRead) == PropertyDependencyType.NeedForRead)
						{
							this.requiredPropertyDefinitionsWhenReading.Add(propertyDependency.Property);
						}
					}
				}
				return this.requiredPropertyDefinitionsWhenReading;
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x000D1C2F File Offset: 0x000CFE2F
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.None;
			}
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000D1C34 File Offset: 0x000CFE34
		internal override SortBy[] GetNativeSortBy(SortOrder sortOrder)
		{
			return new SortBy[]
			{
				new SortBy(this.GetSortProperty(), sortOrder)
			};
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000D1C58 File Offset: 0x000CFE58
		internal override NativeStorePropertyDefinition GetNativeGroupBy()
		{
			return this.GetSortProperty();
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000D1C60 File Offset: 0x000CFE60
		internal override GroupSort GetNativeGroupSort(SortOrder sortOrder, Aggregate aggregate)
		{
			return new GroupSort(this.GetSortProperty(), sortOrder, aggregate);
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000D1C6F File Offset: 0x000CFE6F
		internal virtual QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			throw this.CreateInvalidFilterConversionException(filter);
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000D1C78 File Offset: 0x000CFE78
		internal virtual QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			throw this.CreateInvalidFilterConversionException(filter);
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000D1C81 File Offset: 0x000CFE81
		internal virtual void RegisterFilterTranslation()
		{
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000D1C84 File Offset: 0x000CFE84
		public override bool Equals(object obj)
		{
			SmartPropertyDefinition smartPropertyDefinition = obj as SmartPropertyDefinition;
			return smartPropertyDefinition != null && this.GetHashCode() == smartPropertyDefinition.GetHashCode() && base.Name == smartPropertyDefinition.Name && base.Type.Equals(smartPropertyDefinition.Type);
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000D1CCF File Offset: 0x000CFECF
		public override int GetHashCode()
		{
			if (this.calcHashCode)
			{
				this.hashCode = (base.Name.GetHashCode() ^ base.Type.GetHashCode());
				this.calcHashCode = false;
			}
			return this.hashCode;
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000D1D04 File Offset: 0x000CFF04
		protected virtual NativeStorePropertyDefinition GetSortProperty()
		{
			Exception ex = new UnsupportedPropertyForSortGroupException(ServerStrings.ExSortGroupNotSupportedForProperty(base.Name), this);
			ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), ex.Message);
			throw ex;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000D1D3B File Offset: 0x000CFF3B
		protected override string GetPropertyDefinitionString()
		{
			return "Calc:" + base.Name;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000D1D4D File Offset: 0x000CFF4D
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			throw new NotSupportedException(ServerStrings.ExSetNotSupportedForCalculatedProperty(this));
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000D1D5F File Offset: 0x000CFF5F
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			throw new NotSupportedException(ServerStrings.ExGetNotSupportedForCalculatedProperty(this));
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000D1D71 File Offset: 0x000CFF71
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			throw new NotSupportedException(ServerStrings.ExDeleteNotSupportedForCalculatedProperty(this));
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000D1D84 File Offset: 0x000CFF84
		protected override bool InternalIsDirty(PropertyBag.BasicPropertyStore propertyBag)
		{
			foreach (PropertyDependency propertyDependency in this.dependencies)
			{
				if ((propertyDependency.Type & PropertyDependencyType.NeedForRead) == PropertyDependencyType.NeedForRead && propertyBag.IsDirty(propertyDependency.Property))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000D1DCB File Offset: 0x000CFFCB
		private static PropertyFlags CalculateSmartPropertyFlags(PropertyFlags flags)
		{
			return flags & (PropertyFlags)(-2147418113);
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000D1DD4 File Offset: 0x000CFFD4
		protected QueryFilter SinglePropertySmartFilterToNativeFilter(QueryFilter filter, PropertyDefinition nativeProperty)
		{
			MultivaluedInstanceComparisonFilter multivaluedInstanceComparisonFilter = filter as MultivaluedInstanceComparisonFilter;
			if (multivaluedInstanceComparisonFilter != null)
			{
				return new MultivaluedInstanceComparisonFilter(multivaluedInstanceComparisonFilter.ComparisonOperator, nativeProperty, multivaluedInstanceComparisonFilter.PropertyValue);
			}
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter != null)
			{
				return new ComparisonFilter(comparisonFilter.ComparisonOperator, nativeProperty, comparisonFilter.PropertyValue);
			}
			ExistsFilter existsFilter = filter as ExistsFilter;
			if (existsFilter != null)
			{
				return new ExistsFilter(nativeProperty);
			}
			TextFilter textFilter = filter as TextFilter;
			if (textFilter != null)
			{
				return new TextFilter(nativeProperty, textFilter.Text, textFilter.MatchOptions, textFilter.MatchFlags);
			}
			BitMaskFilter bitMaskFilter = filter as BitMaskFilter;
			if (bitMaskFilter != null)
			{
				return new BitMaskFilter(nativeProperty, bitMaskFilter.Mask, bitMaskFilter.IsNonZero);
			}
			BitMaskAndFilter bitMaskAndFilter = filter as BitMaskAndFilter;
			if (bitMaskAndFilter != null)
			{
				return new BitMaskAndFilter(nativeProperty, bitMaskAndFilter.Mask);
			}
			BitMaskOrFilter bitMaskOrFilter = filter as BitMaskOrFilter;
			if (bitMaskOrFilter != null)
			{
				return new BitMaskOrFilter(nativeProperty, bitMaskOrFilter.Mask);
			}
			throw this.CreateInvalidFilterConversionException(filter);
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000D1EAC File Offset: 0x000D00AC
		protected QueryFilter SinglePropertyNativeFilterToSmartFilter(QueryFilter filter, PropertyDefinition nativeProperty)
		{
			SinglePropertyFilter singlePropertyFilter = filter as SinglePropertyFilter;
			if (singlePropertyFilter != null && singlePropertyFilter.Property.Equals(nativeProperty))
			{
				MultivaluedInstanceComparisonFilter multivaluedInstanceComparisonFilter = filter as MultivaluedInstanceComparisonFilter;
				if (multivaluedInstanceComparisonFilter != null)
				{
					return new MultivaluedInstanceComparisonFilter(multivaluedInstanceComparisonFilter.ComparisonOperator, this, multivaluedInstanceComparisonFilter.PropertyValue);
				}
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				if (comparisonFilter != null)
				{
					return new ComparisonFilter(comparisonFilter.ComparisonOperator, this, comparisonFilter.PropertyValue);
				}
				ExistsFilter existsFilter = filter as ExistsFilter;
				if (existsFilter != null)
				{
					return new ExistsFilter(this);
				}
				TextFilter textFilter = filter as TextFilter;
				if (textFilter != null)
				{
					return new TextFilter(this, textFilter.Text, textFilter.MatchOptions, textFilter.MatchFlags);
				}
				BitMaskFilter bitMaskFilter = filter as BitMaskFilter;
				if (bitMaskFilter != null)
				{
					return new BitMaskFilter(this, bitMaskFilter.Mask, bitMaskFilter.IsNonZero);
				}
				BitMaskAndFilter bitMaskAndFilter = filter as BitMaskAndFilter;
				if (bitMaskAndFilter != null)
				{
					return new BitMaskAndFilter(this, bitMaskAndFilter.Mask);
				}
				BitMaskOrFilter bitMaskOrFilter = filter as BitMaskOrFilter;
				if (bitMaskOrFilter != null)
				{
					return new BitMaskOrFilter(this, bitMaskOrFilter.Mask);
				}
			}
			return null;
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000D1FA0 File Offset: 0x000D01A0
		internal Exception CreateInvalidFilterConversionException(QueryFilter filter)
		{
			Exception ex = new FilterNotSupportedException(ServerStrings.ExFilterNotSupportedForProperty(filter.ToString(), base.Name), filter, new PropertyDefinition[]
			{
				this
			});
			ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), ex.Message);
			return ex;
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x060033B1 RID: 13233 RVA: 0x000D1FE9 File Offset: 0x000D01E9
		internal PropertyDependency[] Dependencies
		{
			get
			{
				return this.dependencies;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x000D1FF1 File Offset: 0x000D01F1
		internal virtual PropertyDependency[] LegalTrackingDependencies
		{
			get
			{
				return this.dependencies;
			}
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x000D1FFC File Offset: 0x000D01FC
		protected override void ForEachMatch(PropertyDependencyType targetDependencyType, Action<NativeStorePropertyDefinition> action)
		{
			for (int i = 0; i < this.dependencies.Length; i++)
			{
				PropertyDependency propertyDependency = this.dependencies[i];
				if ((propertyDependency.Type & targetDependencyType) != PropertyDependencyType.None)
				{
					action(propertyDependency.Property);
				}
			}
		}

		// Token: 0x04001BD5 RID: 7125
		private ICollection<PropertyDefinition> requiredPropertyDefinitionsWhenReading;

		// Token: 0x04001BD6 RID: 7126
		private readonly PropertyDependency[] dependencies;

		// Token: 0x04001BD7 RID: 7127
		private bool calcHashCode = true;

		// Token: 0x04001BD8 RID: 7128
		private int hashCode;
	}
}
