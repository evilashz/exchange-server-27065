using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001A8 RID: 424
	internal sealed class MeetingRequestSchema : Schema
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x00039990 File Offset: 0x00037B90
		static MeetingRequestSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				MeetingRequestSchema.MeetingRequestType,
				MeetingRequestSchema.IntendedFreeBusyStatus,
				MeetingRequestSchema.ChangeHighlights,
				MeetingRequestSchema.Start,
				MeetingRequestSchema.End,
				CalendarItemSchema.OriginalStart,
				MeetingRequestSchema.IsAllDayEvent,
				MeetingRequestSchema.LegacyFreeBusyStatus,
				MeetingRequestSchema.Location,
				MeetingRequestSchema.EnhancedLocation,
				MeetingRequestSchema.When,
				CalendarItemSchema.IsMeeting,
				CalendarItemSchema.IsCancelled,
				CalendarItemSchema.IsRecurring,
				CalendarItemSchema.MeetingRequestWasSent,
				CalendarItemSchema.CalendarItemType,
				MeetingRequestSchema.MyResponseType,
				CalendarItemSchema.Organizer,
				CalendarItemSchema.AttendeeSpecific.RequiredAttendees,
				CalendarItemSchema.AttendeeSpecific.OptionalAttendees,
				CalendarItemSchema.AttendeeSpecific.Resources,
				CalendarItemSchema.ConflictingMeetingCount,
				CalendarItemSchema.AdjacentMeetingCount,
				CalendarItemSchema.ConflictingMeetings,
				CalendarItemSchema.AdjacentMeetings,
				CalendarItemSchema.Duration,
				CalendarItemSchema.TimeZone,
				CalendarItemSchema.AppointmentReplyTime,
				CalendarItemSchema.AppointmentSequenceNumber,
				CalendarItemSchema.AppointmentState,
				CalendarItemSchema.AttendeeSpecific.Recurrence,
				CalendarItemSchema.FirstOccurrence,
				CalendarItemSchema.LastOccurrence,
				CalendarItemSchema.ModifiedOccurrences,
				CalendarItemSchema.DeletedOccurrences,
				MeetingRequestSchema.MeetingTimeZone,
				CalendarItemSchema.AttendeeSpecific.StartTimeZone,
				CalendarItemSchema.AttendeeSpecific.EndTimeZone,
				CalendarItemSchema.AttendeeSpecific.ConferenceType,
				CalendarItemSchema.AttendeeSpecific.AllowNewTimeProposal,
				CalendarItemSchema.AttendeeSpecific.IsOnlineMeeting,
				CalendarItemSchema.AttendeeSpecific.MeetingWorkspaceUrl,
				CalendarItemSchema.AttendeeSpecific.NetShowUrl,
				CalendarItemSchema.StartWallClock,
				CalendarItemSchema.EndWallClock,
				CalendarItemSchema.StartTimeZoneId,
				CalendarItemSchema.EndTimeZoneId,
				CalendarItemSchema.AttendeeSpecific.AttendeeCounts
			};
			MeetingRequestSchema.schema = new MeetingRequestSchema(xmlElements);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00039D58 File Offset: 0x00037F58
		private MeetingRequestSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
			IList<PropertyInformation> propertyInformationListByShapeEnum = base.GetPropertyInformationListByShapeEnum(ShapeEnum.AllProperties);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.AttendeeSpecific.StartTimeZone);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.AttendeeSpecific.EndTimeZone);
			propertyInformationListByShapeEnum.Remove(MeetingRequestSchema.EnhancedLocation);
			propertyInformationListByShapeEnum.Remove(MeetingRequestSchema.ChangeHighlights);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.AttendeeSpecific.AttendeeCounts);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00039DB0 File Offset: 0x00037FB0
		public static Schema GetSchema()
		{
			return MeetingRequestSchema.schema;
		}

		// Token: 0x040008E4 RID: 2276
		public static readonly PropertyInformation ChangeHighlights = new PropertyInformation(PropertyUriEnum.ChangeHighlights.ToString(), ServiceXml.GetFullyQualifiedName(PropertyUriEnum.ChangeHighlights.ToString()), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2012, new PropertyDefinition[]
		{
			CalendarItemBaseSchema.ChangeHighlight,
			CalendarItemBaseSchema.OldLocation,
			MeetingRequestSchema.OldStartWhole,
			MeetingRequestSchema.OldEndWhole
		}, new PropertyUri(PropertyUriEnum.ChangeHighlights), new PropertyCommand.CreatePropertyCommand(ChangeHighlightsProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x040008E5 RID: 2277
		public static readonly PropertyInformation End = new PropertyInformation(PropertyUriEnum.End, ExchangeVersion.Exchange2007, CalendarItemInstanceSchema.EndTime, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForEnd), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008E6 RID: 2278
		public static readonly PropertyInformation IntendedFreeBusyStatus = new PropertyInformation(PropertyUriEnum.IntendedFreeBusyStatus, ExchangeVersion.Exchange2007, MeetingRequestSchema.IntendedFreeBusyStatus, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008E7 RID: 2279
		public static readonly PropertyInformation IsAllDayEvent = new PropertyInformation(PropertyUriEnum.IsAllDayEvent, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.IsAllDayEvent, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008E8 RID: 2280
		public static readonly PropertyInformation LegacyFreeBusyStatus = new PropertyInformation(PropertyUriEnum.LegacyFreeBusyStatus, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.FreeBusyStatus, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008E9 RID: 2281
		public static readonly PropertyInformation Location = new PropertyInformation(PropertyUriEnum.Location, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.Location, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008EA RID: 2282
		public static readonly PropertyInformation MeetingRequestType = new PropertyInformation(PropertyUriEnum.MeetingRequestType, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(MeetingRequestTypeProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x040008EB RID: 2283
		public static readonly PropertyInformation MeetingTimeZone = new PropertyInformation(PropertyUriEnum.MeetingTimeZone, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(MeetingTimeZoneProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x040008EC RID: 2284
		public static readonly PropertyInformation MyResponseType = new PropertyInformation(PropertyUriEnum.MyResponseType, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.ResponseType, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008ED RID: 2285
		public static readonly PropertyInformation Start = new PropertyInformation(PropertyUriEnum.Start, ExchangeVersion.Exchange2007, CalendarItemInstanceSchema.StartTime, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForStart), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008EE RID: 2286
		public static readonly PropertyInformation When = new PropertyInformation(PropertyUriEnum.When, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.When, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008EF RID: 2287
		public static readonly PropertyInformation EnhancedLocation = CalendarItemSchema.EnhancedLocation;

		// Token: 0x040008F0 RID: 2288
		private static Schema schema;
	}
}
