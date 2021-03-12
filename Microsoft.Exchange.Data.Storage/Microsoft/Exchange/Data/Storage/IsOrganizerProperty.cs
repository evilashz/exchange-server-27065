using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C70 RID: 3184
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class IsOrganizerProperty : SmartPropertyDefinition
	{
		// Token: 0x06006FF2 RID: 28658 RVA: 0x001EFFB8 File Offset: 0x001EE1B8
		internal IsOrganizerProperty() : base("IsOrganizerProperty", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.AppointmentStateInternal, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006FF3 RID: 28659 RVA: 0x001F0004 File Offset: 0x001EE204
		internal static bool GetForCalendarItem(string messageClass, AppointmentStateFlags flags)
		{
			if (!ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(messageClass) && !ObjectClass.IsCalendarItemSeries(messageClass))
			{
				throw new ArgumentException(string.Format("[IsOrganizerProperty.GetForCalendarItem] Message class MUST be a calendar item occurrence or recurrence exception in order to call this method.  ItemClass: {0}", messageClass));
			}
			return (flags & AppointmentStateFlags.Received) == AppointmentStateFlags.None;
		}

		// Token: 0x06006FF4 RID: 28660 RVA: 0x001F0030 File Offset: 0x001EE230
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			bool? flag = null;
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(valueOrDefault) || ObjectClass.IsCalendarItemSeries(valueOrDefault))
			{
				AppointmentStateFlags valueOrDefault2 = propertyBag.GetValueOrDefault<AppointmentStateFlags>(InternalSchema.AppointmentStateInternal);
				flag = new bool?(IsOrganizerProperty.GetForCalendarItem(valueOrDefault, valueOrDefault2));
			}
			else if (ObjectClass.IsMeetingMessage(valueOrDefault))
			{
				MeetingMessage meetingMessage = propertyBag.Context.StoreObject as MeetingMessage;
				if (meetingMessage != null)
				{
					CalendarItemBase calendarItemBase = null;
					try
					{
						calendarItemBase = meetingMessage.GetCorrelatedItemInternal(true);
					}
					catch (CorruptDataException)
					{
					}
					catch (CorrelationFailedException)
					{
					}
					if (calendarItemBase != null)
					{
						flag = new bool?(calendarItemBase.IsOrganizer());
					}
					else if (!(meetingMessage is MeetingResponse))
					{
						flag = new bool?(meetingMessage.IsMailboxOwnerTheSender());
					}
				}
			}
			if (flag != null)
			{
				return flag;
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x06006FF5 RID: 28661 RVA: 0x001F0114 File Offset: 0x001EE314
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			throw new NotSupportedException(ServerStrings.PropertyIsReadOnly("IsOrganizer"));
		}
	}
}
