using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x020001A3 RID: 419
	[Serializable]
	internal class EntityMeetingStatusProperty : EntityProperty, IIntegerProperty, IProperty
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x00061F4A File Offset: 0x0006014A
		public EntityMeetingStatusProperty() : base(PropertyType.ReadOnly, false)
		{
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00061F54 File Offset: 0x00060154
		public virtual int IntegerData
		{
			get
			{
				AppointmentStateFlags appointmentStateFlags = AppointmentStateFlags.None;
				if (base.CalendaringEvent.IsCancelled)
				{
					appointmentStateFlags |= AppointmentStateFlags.Cancelled;
				}
				if (base.CalendaringEvent.HasAttendees)
				{
					appointmentStateFlags |= AppointmentStateFlags.Meeting;
				}
				if (((IEventInternal)base.CalendaringEvent).IsReceived)
				{
					appointmentStateFlags |= AppointmentStateFlags.Received;
				}
				return (int)appointmentStateFlags;
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00061F97 File Offset: 0x00060197
		public override void CopyFrom(IProperty srcProperty)
		{
			throw new InvalidOperationException("CopyFrom should not be called on a readonly EntityMeetingStatusProperty.");
		}
	}
}
