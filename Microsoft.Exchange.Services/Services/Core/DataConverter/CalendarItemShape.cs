using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200019D RID: 413
	internal sealed class CalendarItemShape : Shape
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x00037CDC File Offset: 0x00035EDC
		static CalendarItemShape()
		{
			CalendarItemShape.defaultPropertiesForOrganizer.Add(ItemSchema.ItemId);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(ItemSchema.Subject);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(ItemSchema.Attachments);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(ItemSchema.ResponseObjects);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(ItemSchema.HasAttachments);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(ItemSchema.IsAssociated);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(CalendarItemSchema.Start);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(CalendarItemSchema.End);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(CalendarItemSchema.OriginalStart);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(CalendarItemSchema.LegacyFreeBusyStatus);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(CalendarItemSchema.Location);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(CalendarItemSchema.CalendarItemType);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(CalendarItemSchema.Organizer);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(CalendarItemSchema.OrganizerSpecific.RequiredAttendees);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(CalendarItemSchema.OrganizerSpecific.OptionalAttendees);
			CalendarItemShape.defaultPropertiesForOrganizer.Add(CalendarItemSchema.OrganizerSpecific.Resources);
			CalendarItemShape.defaultPropertiesForAttendee = new List<PropertyInformation>();
			CalendarItemShape.defaultPropertiesForAttendee.AddRange(CalendarItemShape.defaultPropertiesForOrganizer);
			CalendarItemShape.defaultPropertiesForAttendee[CalendarItemShape.defaultPropertiesForAttendee.IndexOf(CalendarItemSchema.OrganizerSpecific.RequiredAttendees)] = CalendarItemSchema.AttendeeSpecific.RequiredAttendees;
			CalendarItemShape.defaultPropertiesForAttendee[CalendarItemShape.defaultPropertiesForAttendee.IndexOf(CalendarItemSchema.OrganizerSpecific.OptionalAttendees)] = CalendarItemSchema.AttendeeSpecific.OptionalAttendees;
			CalendarItemShape.defaultPropertiesForAttendee[CalendarItemShape.defaultPropertiesForAttendee.IndexOf(CalendarItemSchema.OrganizerSpecific.Resources)] = CalendarItemSchema.AttendeeSpecific.Resources;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00037E56 File Offset: 0x00036056
		private CalendarItemShape(bool forOrganizer) : base(Schema.CalendarItem, CalendarItemSchema.GetSchema(forOrganizer), ItemShape.CreateShape(), forOrganizer ? CalendarItemShape.defaultPropertiesForOrganizer : CalendarItemShape.defaultPropertiesForAttendee)
		{
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00037E7D File Offset: 0x0003607D
		internal static CalendarItemShape CreateShapeForAttendee()
		{
			return new CalendarItemShape(false);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00037E88 File Offset: 0x00036088
		internal static CalendarItemShape CreateShape(StoreObject storeObject)
		{
			CalendarItemBase calendarItemBase = storeObject as CalendarItemBase;
			return new CalendarItemShape(calendarItemBase.IsOrganizer());
		}

		// Token: 0x04000872 RID: 2162
		private static List<PropertyInformation> defaultPropertiesForOrganizer = new List<PropertyInformation>();

		// Token: 0x04000873 RID: 2163
		private static List<PropertyInformation> defaultPropertiesForAttendee;
	}
}
