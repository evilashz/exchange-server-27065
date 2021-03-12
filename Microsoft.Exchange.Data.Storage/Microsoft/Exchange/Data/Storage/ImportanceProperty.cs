using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C67 RID: 3175
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ImportanceProperty : SmartPropertyDefinition
	{
		// Token: 0x06006FBF RID: 28607 RVA: 0x001E1440 File Offset: 0x001DF640
		internal ImportanceProperty() : base("Importance", typeof(Importance), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiImportance, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006FC0 RID: 28608 RVA: 0x001E1480 File Offset: 0x001DF680
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(InternalSchema.MapiImportance);
			if (value is int)
			{
				return (Importance)value;
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x06006FC1 RID: 28609 RVA: 0x001E14B5 File Offset: 0x001DF6B5
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			propertyBag.SetValueWithFixup(InternalSchema.MapiPriority, (int)value - 1);
			propertyBag.SetValueWithFixup(InternalSchema.MapiImportance, (int)value);
		}

		// Token: 0x06006FC2 RID: 28610 RVA: 0x001E14E7 File Offset: 0x001DF6E7
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(InternalSchema.MapiImportance);
		}

		// Token: 0x06006FC3 RID: 28611 RVA: 0x001E14F5 File Offset: 0x001DF6F5
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, InternalSchema.MapiImportance);
		}

		// Token: 0x06006FC4 RID: 28612 RVA: 0x001E1503 File Offset: 0x001DF703
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, InternalSchema.MapiImportance);
		}

		// Token: 0x06006FC5 RID: 28613 RVA: 0x001E1511 File Offset: 0x001DF711
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001E1D RID: 7709
		// (get) Token: 0x06006FC6 RID: 28614 RVA: 0x001E1533 File Offset: 0x001DF733
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06006FC7 RID: 28615 RVA: 0x001E1536 File Offset: 0x001DF736
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.MapiImportance;
		}
	}
}
