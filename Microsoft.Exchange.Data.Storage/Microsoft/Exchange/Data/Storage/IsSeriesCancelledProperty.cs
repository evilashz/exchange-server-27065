using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C32 RID: 3122
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class IsSeriesCancelledProperty : SmartPropertyDefinition
	{
		// Token: 0x06006ED0 RID: 28368 RVA: 0x001DCDD8 File Offset: 0x001DAFD8
		internal IsSeriesCancelledProperty() : base("IsSeriesCancelled", typeof(bool), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.AppointmentRecurring, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.AppointmentStateInternal, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006ED1 RID: 28369 RVA: 0x001DCE24 File Offset: 0x001DB024
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			bool valueOrDefault = propertyBag.GetValueOrDefault<bool>(InternalSchema.AppointmentRecurring);
			if (valueOrDefault)
			{
				AppointmentStateFlags valueOrDefault2 = propertyBag.GetValueOrDefault<AppointmentStateFlags>(InternalSchema.AppointmentStateInternal);
				return CalendarItemBase.IsAppointmentStateCancelled(valueOrDefault2);
			}
			CalendarItemOccurrence calendarItemOccurrence = propertyBag.Context.StoreObject as CalendarItemOccurrence;
			if (calendarItemOccurrence != null)
			{
				AppointmentStateFlags valueOrDefault3 = calendarItemOccurrence.OccurrencePropertyBag.MasterCalendarItem.GetValueOrDefault<AppointmentStateFlags>(CalendarItemBaseSchema.AppointmentState);
				return CalendarItemBase.IsAppointmentStateCancelled(valueOrDefault3);
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}
	}
}
