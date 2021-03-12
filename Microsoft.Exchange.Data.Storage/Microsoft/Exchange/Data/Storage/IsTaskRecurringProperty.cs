using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C72 RID: 3186
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class IsTaskRecurringProperty : SmartPropertyDefinition
	{
		// Token: 0x06006FF8 RID: 28664 RVA: 0x001F01A4 File Offset: 0x001EE3A4
		internal IsTaskRecurringProperty() : base("IsTaskRecurring", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.TaskRecurrence, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.IsOneOff, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006FF9 RID: 28665 RVA: 0x001F01F0 File Offset: 0x001EE3F0
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			bool valueOrDefault = propertyBag.GetValueOrDefault<bool>(InternalSchema.IsOneOff);
			if (valueOrDefault)
			{
				return false;
			}
			byte[] valueOrDefault2 = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.TaskRecurrence);
			return valueOrDefault2 != null;
		}

		// Token: 0x06006FFA RID: 28666 RVA: 0x001F022D File Offset: 0x001EE42D
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			propertyBag.SetValueWithFixup(InternalSchema.MapiIsTaskRecurring, (bool)value);
		}

		// Token: 0x06006FFB RID: 28667 RVA: 0x001F0246 File Offset: 0x001EE446
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(InternalSchema.MapiIsTaskRecurring);
		}

		// Token: 0x06006FFC RID: 28668 RVA: 0x001F0254 File Offset: 0x001EE454
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, InternalSchema.MapiIsTaskRecurring);
		}

		// Token: 0x06006FFD RID: 28669 RVA: 0x001F0262 File Offset: 0x001EE462
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, InternalSchema.MapiIsTaskRecurring);
		}

		// Token: 0x06006FFE RID: 28670 RVA: 0x001F0270 File Offset: 0x001EE470
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001E20 RID: 7712
		// (get) Token: 0x06006FFF RID: 28671 RVA: 0x001F0292 File Offset: 0x001EE492
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06007000 RID: 28672 RVA: 0x001F0295 File Offset: 0x001EE495
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.MapiIsTaskRecurring;
		}
	}
}
