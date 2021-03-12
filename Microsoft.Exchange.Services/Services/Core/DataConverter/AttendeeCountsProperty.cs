using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000D4 RID: 212
	internal sealed class AttendeeCountsProperty : ComplexPropertyBase, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x0001E598 File Offset: 0x0001C798
		public AttendeeCountsProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001E5A1 File Offset: 0x0001C7A1
		public static AttendeeCountsProperty CreateCommand(CommandContext commandContext)
		{
			return new AttendeeCountsProperty(commandContext);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001E5A9 File Offset: 0x0001C7A9
		public void ToXml()
		{
			throw new InvalidOperationException("AttendeeCountsProperty.ToXml should not be called.");
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001E5B8 File Offset: 0x0001C7B8
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			CalendarItemBase calendarItemBase = commandSettings.StoreObject as CalendarItemBase;
			if (calendarItemBase == null)
			{
				calendarItemBase = ((MeetingRequest)commandSettings.StoreObject).GetCachedEmbeddedItem();
				serviceObject[propertyInformation] = this.GetAttendeeCounts(calendarItemBase);
				return;
			}
			serviceObject[propertyInformation] = this.GetAttendeeCounts(calendarItemBase);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001E61C File Offset: 0x0001C81C
		private AttendeeCountsType GetAttendeeCounts(CalendarItemBase calendarItem)
		{
			if (calendarItem == null)
			{
				return null;
			}
			AttendeeCountsType attendeeCountsType = new AttendeeCountsType();
			if (calendarItem.AttendeeCollection != null)
			{
				foreach (Attendee attendee in calendarItem.AttendeeCollection)
				{
					switch (attendee.AttendeeType)
					{
					case AttendeeType.Required:
						attendeeCountsType.RequiredAttendeesCount++;
						break;
					case AttendeeType.Optional:
						attendeeCountsType.OptionalAttendeesCount++;
						break;
					case AttendeeType.Resource:
						attendeeCountsType.ResourcesCount++;
						break;
					}
				}
			}
			return attendeeCountsType;
		}
	}
}
