using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EB3 RID: 3763
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OrganizerPropertiesConstraint : StoreObjectConstraint
	{
		// Token: 0x06008242 RID: 33346 RVA: 0x00239050 File Offset: 0x00237250
		internal OrganizerPropertiesConstraint() : base(new PropertyDefinition[]
		{
			InternalSchema.AppointmentStateInternal
		})
		{
		}

		// Token: 0x06008243 RID: 33347 RVA: 0x00239074 File Offset: 0x00237274
		internal override StoreObjectValidationError Validate(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			string text = validatablePropertyBag.TryGetProperty(InternalSchema.ItemClass) as string;
			if (!string.IsNullOrEmpty(text) && (ObjectClass.IsCalendarItem(text) || ObjectClass.IsRecurrenceException(text) || ObjectClass.IsMeetingMessage(text)) && validatablePropertyBag.IsPropertyDirty(InternalSchema.AppointmentStateInternal))
			{
				object obj = validatablePropertyBag.TryGetProperty(InternalSchema.AppointmentStateInternal);
				if (obj is int)
				{
					AppointmentStateFlags appointmentStateFlags = (AppointmentStateFlags)obj;
					if (EnumValidator<AppointmentStateFlags>.IsValidValue(appointmentStateFlags))
					{
						PropertyValueTrackingData originalPropertyInformation = validatablePropertyBag.GetOriginalPropertyInformation(InternalSchema.AppointmentStateInternal);
						if (originalPropertyInformation.PropertyValueState == PropertyTrackingInformation.Modified && originalPropertyInformation.OriginalPropertyValue != null && !PropertyError.IsPropertyNotFound(originalPropertyInformation.OriginalPropertyValue))
						{
							AppointmentStateFlags appointmentStateFlags2 = (AppointmentStateFlags)originalPropertyInformation.OriginalPropertyValue;
							bool flag = (appointmentStateFlags2 & AppointmentStateFlags.Received) == AppointmentStateFlags.None;
							bool flag2 = (appointmentStateFlags & AppointmentStateFlags.Received) == AppointmentStateFlags.None;
							if (flag != flag2)
							{
								return new StoreObjectValidationError(context, InternalSchema.AppointmentStateInternal, obj, this);
							}
						}
					}
				}
			}
			return null;
		}
	}
}
