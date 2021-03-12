using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001A9 RID: 425
	internal sealed class MeetingRequestShape : Shape
	{
		// Token: 0x06000BA8 RID: 2984 RVA: 0x00039DB8 File Offset: 0x00037FB8
		static MeetingRequestShape()
		{
			MeetingRequestShape.defaultProperties.Add(ItemSchema.ItemId);
			MeetingRequestShape.defaultProperties.Add(ItemSchema.Attachments);
			MeetingRequestShape.defaultProperties.Add(ItemSchema.Subject);
			MeetingRequestShape.defaultProperties.Add(ItemSchema.Sensitivity);
			MeetingRequestShape.defaultProperties.Add(ItemSchema.ResponseObjects);
			MeetingRequestShape.defaultProperties.Add(ItemSchema.HasAttachments);
			MeetingRequestShape.defaultProperties.Add(ItemSchema.IsAssociated);
			MeetingRequestShape.defaultProperties.Add(MessageSchema.ToRecipients);
			MeetingRequestShape.defaultProperties.Add(MessageSchema.CcRecipients);
			MeetingRequestShape.defaultProperties.Add(MessageSchema.BccRecipients);
			MeetingRequestShape.defaultProperties.Add(MessageSchema.IsRead);
			MeetingRequestShape.defaultProperties.Add(MeetingMessageSchema.AssociatedCalendarItemId);
			MeetingRequestShape.defaultProperties.Add(MeetingMessageSchema.IsDelegated);
			MeetingRequestShape.defaultProperties.Add(MeetingMessageSchema.IsOutOfDate);
			MeetingRequestShape.defaultProperties.Add(MeetingMessageSchema.HasBeenProcessed);
			MeetingRequestShape.defaultProperties.Add(MeetingMessageSchema.ResponseType);
			MeetingRequestShape.defaultProperties.Add(MeetingRequestSchema.MeetingRequestType);
			MeetingRequestShape.defaultProperties.Add(MeetingRequestSchema.IntendedFreeBusyStatus);
			MeetingRequestShape.defaultProperties.Add(MeetingRequestSchema.Start);
			MeetingRequestShape.defaultProperties.Add(MeetingRequestSchema.End);
			MeetingRequestShape.defaultProperties.Add(MeetingRequestSchema.Location);
			MeetingRequestShape.defaultProperties.Add(CalendarItemSchema.Organizer);
			MeetingRequestShape.defaultProperties.Add(CalendarItemSchema.AttendeeSpecific.RequiredAttendees);
			MeetingRequestShape.defaultProperties.Add(CalendarItemSchema.AttendeeSpecific.OptionalAttendees);
			MeetingRequestShape.defaultProperties.Add(CalendarItemSchema.AttendeeSpecific.Resources);
			MeetingRequestShape.defaultProperties.Add(CalendarItemSchema.AdjacentMeetingCount);
			MeetingRequestShape.defaultProperties.Add(CalendarItemSchema.ConflictingMeetingCount);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00039F64 File Offset: 0x00038164
		private MeetingRequestShape() : base(Schema.MeetingRequest, MeetingRequestSchema.GetSchema(), MeetingMessageShape.CreateShape(), MeetingRequestShape.defaultProperties)
		{
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00039F80 File Offset: 0x00038180
		internal static MeetingRequestShape CreateShape()
		{
			return new MeetingRequestShape();
		}

		// Token: 0x040008F1 RID: 2289
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
