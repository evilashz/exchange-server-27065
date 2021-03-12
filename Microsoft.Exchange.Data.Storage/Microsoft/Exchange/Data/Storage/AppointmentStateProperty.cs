using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C12 RID: 3090
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class AppointmentStateProperty : SmartPropertyDefinition
	{
		// Token: 0x06006E30 RID: 28208 RVA: 0x001D9F04 File Offset: 0x001D8104
		internal AppointmentStateProperty() : base("AppointmentState", typeof(AppointmentStateFlags), PropertyFlags.None, Array<PropertyDefinitionConstraint>.Empty, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.AppointmentStateInternal, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x06006E31 RID: 28209 RVA: 0x001D9F42 File Offset: 0x001D8142
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.GetValue(InternalSchema.AppointmentStateInternal);
		}

		// Token: 0x06006E32 RID: 28210 RVA: 0x001D9F50 File Offset: 0x001D8150
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			AppointmentStateFlags? valueAsNullable = propertyBag.GetValueAsNullable<AppointmentStateFlags>(InternalSchema.AppointmentStateInternal);
			AppointmentStateFlags appointmentStateFlags = (AppointmentStateFlags)value;
			if (valueAsNullable != null && (valueAsNullable.Value & AppointmentStateFlags.Received) == AppointmentStateFlags.Received && (appointmentStateFlags & AppointmentStateFlags.Received) != AppointmentStateFlags.Received)
			{
				propertyBag.SetLocationIdentifier(63651U, LastChangeAction.SmartPropertyFixup);
				ExTraceGlobals.StorageTracer.TraceInformation(63651, (long)propertyBag.GetHashCode(), "Prevent from removing Received flag on AppointmentState");
				appointmentStateFlags |= AppointmentStateFlags.Received;
			}
			propertyBag.SetValueWithFixup(InternalSchema.AppointmentStateInternal, appointmentStateFlags);
		}

		// Token: 0x06006E33 RID: 28211 RVA: 0x001D9FD2 File Offset: 0x001D81D2
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, InternalSchema.AppointmentStateInternal);
		}

		// Token: 0x06006E34 RID: 28212 RVA: 0x001D9FE0 File Offset: 0x001D81E0
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, InternalSchema.AppointmentStateInternal);
		}

		// Token: 0x06006E35 RID: 28213 RVA: 0x001D9FEE File Offset: 0x001D81EE
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001DDA RID: 7642
		// (get) Token: 0x06006E36 RID: 28214 RVA: 0x001DA010 File Offset: 0x001D8210
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06006E37 RID: 28215 RVA: 0x001DA013 File Offset: 0x001D8213
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.AppointmentStateInternal;
		}
	}
}
