using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C09 RID: 3081
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ActivityObjectIdProperty : SmartPropertyDefinition
	{
		// Token: 0x06006DFC RID: 28156 RVA: 0x001D8638 File Offset: 0x001D6838
		internal ActivityObjectIdProperty(string propertyName, NativeStorePropertyDefinition backingPropertyDefinition) : base(propertyName, typeof(StoreObjectId), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(backingPropertyDefinition, PropertyDependencyType.NeedForRead)
		})
		{
			this.backingPropertyDefinition = backingPropertyDefinition;
		}

		// Token: 0x06006DFD RID: 28157 RVA: 0x001D8675 File Offset: 0x001D6875
		protected sealed override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(this.backingPropertyDefinition);
		}

		// Token: 0x06006DFE RID: 28158 RVA: 0x001D8684 File Offset: 0x001D6884
		protected sealed override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			StoreObjectId storeObjectId = value as StoreObjectId;
			if (storeObjectId == null)
			{
				throw new ArgumentException("value", "Must be a non-null StoreObjectId");
			}
			byte[] bytes = storeObjectId.GetBytes();
			propertyBag.SetValue(this.backingPropertyDefinition, bytes);
		}

		// Token: 0x06006DFF RID: 28159 RVA: 0x001D86C0 File Offset: 0x001D68C0
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(this.backingPropertyDefinition);
			byte[] array = value as byte[];
			if (array != null)
			{
				object result;
				try
				{
					result = StoreObjectId.Parse(array, 0);
				}
				catch (CorruptDataException)
				{
					result = new PropertyError(this, PropertyErrorCode.CorruptedData);
				}
				return result;
			}
			PropertyError propertyError = (PropertyError)value;
			if (propertyError.PropertyErrorCode == PropertyErrorCode.NotEnoughMemory)
			{
				return new PropertyError(this, PropertyErrorCode.CorruptedData);
			}
			return new PropertyError(this, propertyError.PropertyErrorCode);
		}

		// Token: 0x06006E00 RID: 28160 RVA: 0x001D8734 File Offset: 0x001D6934
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			SinglePropertyFilter singlePropertyFilter = filter as SinglePropertyFilter;
			if (singlePropertyFilter != null && singlePropertyFilter.Property.Equals(this.backingPropertyDefinition))
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				if (comparisonFilter != null)
				{
					return new ComparisonFilter(comparisonFilter.ComparisonOperator, this, StoreObjectId.Parse((byte[])comparisonFilter.PropertyValue, 0));
				}
				ExistsFilter existsFilter = filter as ExistsFilter;
				if (existsFilter != null)
				{
					return new ExistsFilter(this);
				}
			}
			return null;
		}

		// Token: 0x06006E01 RID: 28161 RVA: 0x001D8798 File Offset: 0x001D6998
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter != null)
			{
				return new ComparisonFilter(comparisonFilter.ComparisonOperator, this.backingPropertyDefinition, ((StoreObjectId)comparisonFilter.PropertyValue).GetBytes());
			}
			ExistsFilter existsFilter = filter as ExistsFilter;
			if (existsFilter != null)
			{
				return new ExistsFilter(this.backingPropertyDefinition);
			}
			throw base.CreateInvalidFilterConversionException(filter);
		}

		// Token: 0x06006E02 RID: 28162 RVA: 0x001D87EE File Offset: 0x001D69EE
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return this.backingPropertyDefinition;
		}

		// Token: 0x17001DD2 RID: 7634
		// (get) Token: 0x06006E03 RID: 28163 RVA: 0x001D87F6 File Offset: 0x001D69F6
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x04003EB5 RID: 16053
		private NativeStorePropertyDefinition backingPropertyDefinition;
	}
}
