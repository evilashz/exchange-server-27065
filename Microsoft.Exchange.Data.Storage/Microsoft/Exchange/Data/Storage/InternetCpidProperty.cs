using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C6F RID: 3183
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class InternetCpidProperty : SmartPropertyDefinition
	{
		// Token: 0x06006FE9 RID: 28649 RVA: 0x001EFE10 File Offset: 0x001EE010
		internal InternetCpidProperty() : base("InternetCpidProperty", typeof(int), PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 65535)
		}, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.MapiInternetCpid, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x06006FEA RID: 28650 RVA: 0x001EFE70 File Offset: 0x001EE070
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(InternalSchema.MapiInternetCpid);
			if (value is int && (int)value == 20127)
			{
				string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass);
				if (ObjectClass.IsOfClass(valueOrDefault, "IPM.InfoPathForm"))
				{
					return 28591;
				}
			}
			return value;
		}

		// Token: 0x06006FEB RID: 28651 RVA: 0x001EFEC5 File Offset: 0x001EE0C5
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			propertyBag.SetValueWithFixup(InternalSchema.MapiInternetCpid, value);
		}

		// Token: 0x06006FEC RID: 28652 RVA: 0x001EFED4 File Offset: 0x001EE0D4
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			base.InternalDeleteValue(propertyBag);
		}

		// Token: 0x06006FED RID: 28653 RVA: 0x001EFEE0 File Offset: 0x001EE0E0
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter != null)
			{
				return new ComparisonFilter(comparisonFilter.ComparisonOperator, InternalSchema.MapiInternetCpid, (string)comparisonFilter.PropertyValue);
			}
			if (filter is ExistsFilter)
			{
				return new ExistsFilter(InternalSchema.MapiInternetCpid);
			}
			return base.SmartFilterToNativeFilter(filter);
		}

		// Token: 0x06006FEE RID: 28654 RVA: 0x001EFF30 File Offset: 0x001EE130
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			SinglePropertyFilter singlePropertyFilter = filter as SinglePropertyFilter;
			if (singlePropertyFilter != null && singlePropertyFilter.Property.Equals(InternalSchema.MapiInternetCpid))
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				if (comparisonFilter != null)
				{
					return new ComparisonFilter(comparisonFilter.ComparisonOperator, this, (string)comparisonFilter.PropertyValue);
				}
				if (filter is ExistsFilter)
				{
					return new ExistsFilter(this);
				}
			}
			return null;
		}

		// Token: 0x06006FEF RID: 28655 RVA: 0x001EFF8B File Offset: 0x001EE18B
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001E1F RID: 7711
		// (get) Token: 0x06006FF0 RID: 28656 RVA: 0x001EFFAD File Offset: 0x001EE1AD
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06006FF1 RID: 28657 RVA: 0x001EFFB0 File Offset: 0x001EE1B0
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.MapiInternetCpid;
		}
	}
}
