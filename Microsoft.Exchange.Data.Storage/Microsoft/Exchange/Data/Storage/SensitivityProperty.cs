using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C68 RID: 3176
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class SensitivityProperty : SmartPropertyDefinition
	{
		// Token: 0x06006FC8 RID: 28616 RVA: 0x001E1540 File Offset: 0x001DF740
		internal SensitivityProperty() : base("Sensitivity", typeof(Sensitivity), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiSensitivity, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006FC9 RID: 28617 RVA: 0x001E1580 File Offset: 0x001DF780
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(InternalSchema.MapiSensitivity);
			if (value is int)
			{
				return (Sensitivity)value;
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x06006FCA RID: 28618 RVA: 0x001E15B8 File Offset: 0x001DF7B8
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			Sensitivity sensitivity = (Sensitivity)value;
			propertyBag.SetValueWithFixup(InternalSchema.MapiSensitivity, (int)value);
			propertyBag.SetValueWithFixup(InternalSchema.Privacy, sensitivity == Sensitivity.Private);
		}

		// Token: 0x06006FCB RID: 28619 RVA: 0x001E15F8 File Offset: 0x001DF7F8
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(InternalSchema.MapiSensitivity);
		}

		// Token: 0x06006FCC RID: 28620 RVA: 0x001E1606 File Offset: 0x001DF806
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, InternalSchema.MapiSensitivity);
		}

		// Token: 0x06006FCD RID: 28621 RVA: 0x001E1614 File Offset: 0x001DF814
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, InternalSchema.MapiSensitivity);
		}

		// Token: 0x06006FCE RID: 28622 RVA: 0x001E1622 File Offset: 0x001DF822
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001E1E RID: 7710
		// (get) Token: 0x06006FCF RID: 28623 RVA: 0x001E1644 File Offset: 0x001DF844
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06006FD0 RID: 28624 RVA: 0x001E1647 File Offset: 0x001DF847
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.MapiSensitivity;
		}
	}
}
