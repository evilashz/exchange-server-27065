using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200019A RID: 410
	internal sealed class CalendarItemSchema : Schema
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x00036B50 File Offset: 0x00034D50
		static CalendarItemSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				CalendarItemSchema.ICalendarUid,
				CalendarItemSchema.ICalendarRecurrenceId,
				CalendarItemSchema.ICalendarDateTimeStamp,
				CalendarItemSchema.Start,
				CalendarItemSchema.End,
				CalendarItemSchema.OriginalStart,
				CalendarItemSchema.IsAllDayEvent,
				CalendarItemSchema.LegacyFreeBusyStatus,
				CalendarItemSchema.Location,
				CalendarItemSchema.EnhancedLocation,
				CalendarItemSchema.When,
				CalendarItemSchema.IsMeeting,
				CalendarItemSchema.IsCancelled,
				CalendarItemSchema.IsRecurring,
				CalendarItemSchema.MeetingRequestWasSent,
				CalendarItemSchema.OrganizerSpecific.IsResponseRequested,
				CalendarItemSchema.CalendarItemType,
				CalendarItemSchema.MyResponseType,
				CalendarItemSchema.Organizer,
				CalendarItemSchema.OrganizerSpecific.RequiredAttendees,
				CalendarItemSchema.OrganizerSpecific.OptionalAttendees,
				CalendarItemSchema.OrganizerSpecific.Resources,
				CalendarItemSchema.ConflictingMeetingCount,
				CalendarItemSchema.AdjacentMeetingCount,
				CalendarItemSchema.ConflictingMeetings,
				CalendarItemSchema.AdjacentMeetings,
				CalendarItemSchema.Duration,
				CalendarItemSchema.TimeZone,
				CalendarItemSchema.AppointmentReplyTime,
				CalendarItemSchema.AppointmentSequenceNumber,
				CalendarItemSchema.AppointmentState,
				CalendarItemSchema.OrganizerSpecific.Recurrence,
				CalendarItemSchema.FirstOccurrence,
				CalendarItemSchema.LastOccurrence,
				CalendarItemSchema.ModifiedOccurrences,
				CalendarItemSchema.DeletedOccurrences,
				CalendarItemSchema.MeetingTimeZone,
				CalendarItemSchema.OrganizerSpecific.StartTimeZone,
				CalendarItemSchema.OrganizerSpecific.EndTimeZone,
				CalendarItemSchema.OrganizerSpecific.ConferenceType,
				CalendarItemSchema.OrganizerSpecific.AllowNewTimeProposal,
				CalendarItemSchema.OrganizerSpecific.IsOnlineMeeting,
				CalendarItemSchema.OrganizerSpecific.MeetingWorkspaceUrl,
				CalendarItemSchema.OrganizerSpecific.NetShowUrl,
				CalendarItemSchema.StartWallClock,
				CalendarItemSchema.EndWallClock,
				CalendarItemSchema.StartTimeZoneId,
				CalendarItemSchema.EndTimeZoneId,
				CalendarItemSchema.IntendedFreeBusyStatus,
				CalendarItemSchema.JoinOnlineMeetingUrl,
				CalendarItemSchema.OnlineMeetingSettings,
				CalendarItemSchema.IsOrganizer,
				CalendarItemSchema.AppointmentReplyName,
				CalendarItemSchema.IsSeriesCancelled,
				CalendarItemSchema.InboxReminders,
				CalendarItemSchema.OrganizerSpecific.AttendeeCounts
			};
			XmlElementInformation[] xmlElements2 = new XmlElementInformation[]
			{
				CalendarItemSchema.ICalendarUid,
				CalendarItemSchema.ICalendarRecurrenceId,
				CalendarItemSchema.ICalendarDateTimeStamp,
				CalendarItemSchema.Start,
				CalendarItemSchema.End,
				CalendarItemSchema.OriginalStart,
				CalendarItemSchema.IsAllDayEvent,
				CalendarItemSchema.LegacyFreeBusyStatus,
				CalendarItemSchema.Location,
				CalendarItemSchema.EnhancedLocation,
				CalendarItemSchema.When,
				CalendarItemSchema.IsMeeting,
				CalendarItemSchema.IsCancelled,
				CalendarItemSchema.IsRecurring,
				CalendarItemSchema.MeetingRequestWasSent,
				CalendarItemSchema.AttendeeSpecific.IsResponseRequested,
				CalendarItemSchema.CalendarItemType,
				CalendarItemSchema.MyResponseType,
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
				CalendarItemSchema.MeetingTimeZone,
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
				CalendarItemSchema.IntendedFreeBusyStatus,
				CalendarItemSchema.JoinOnlineMeetingUrl,
				CalendarItemSchema.OnlineMeetingSettings,
				CalendarItemSchema.IsOrganizer,
				CalendarItemSchema.AppointmentReplyName,
				CalendarItemSchema.IsSeriesCancelled,
				CalendarItemSchema.InboxReminders,
				CalendarItemSchema.AttendeeSpecific.AttendeeCounts
			};
			CalendarItemSchema.schemaForOrganizer_Exchange2010AndEarlier = new CalendarItemSchema(xmlElements, ExchangeVersion.Exchange2010);
			CalendarItemSchema.schemaForAttendee_Exchange2010AndEarlier = new CalendarItemSchema(xmlElements2, ExchangeVersion.Exchange2010);
			CalendarItemSchema.schemaForOrganizer_Exchange2010SP1AndLater = new CalendarItemSchema(xmlElements, ExchangeVersion.Exchange2010SP1);
			CalendarItemSchema.schemaForAttendee_Exchange2010SP1AndLater = new CalendarItemSchema(xmlElements2, ExchangeVersion.Exchange2010SP1);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000377B4 File Offset: 0x000359B4
		private CalendarItemSchema(XmlElementInformation[] xmlElements, ExchangeVersion exchangeVersion) : base(xmlElements)
		{
			IList<PropertyInformation> propertyInformationListByShapeEnum = base.GetPropertyInformationListByShapeEnum(ShapeEnum.AllProperties);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.OrganizerSpecific.StartTimeZone);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.OrganizerSpecific.EndTimeZone);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.AttendeeSpecific.StartTimeZone);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.AttendeeSpecific.EndTimeZone);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.EnhancedLocation);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.AdjacentMeetingCount);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.AdjacentMeetings);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.ConflictingMeetingCount);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.ConflictingMeetings);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.OrganizerSpecific.AttendeeCounts);
			propertyInformationListByShapeEnum.Remove(CalendarItemSchema.AttendeeSpecific.AttendeeCounts);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00037854 File Offset: 0x00035A54
		public static Schema GetSchema(bool forOrganizer)
		{
			if (forOrganizer)
			{
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010SP1))
				{
					return CalendarItemSchema.schemaForOrganizer_Exchange2010AndEarlier;
				}
				return CalendarItemSchema.schemaForOrganizer_Exchange2010SP1AndLater;
			}
			else
			{
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010SP1))
				{
					return CalendarItemSchema.schemaForAttendee_Exchange2010AndEarlier;
				}
				return CalendarItemSchema.schemaForAttendee_Exchange2010SP1AndLater;
			}
		}

		// Token: 0x04000829 RID: 2089
		public static readonly PropertyInformation AdjacentMeetingCount = new PropertyInformation(PropertyUriEnum.AdjacentMeetingCount, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(AdjacentOrConflictingMeetingsProperty.CreateCommandForAdjacentMeetingCount), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x0400082A RID: 2090
		public static readonly PropertyInformation AdjacentMeetings = new PropertyInformation(PropertyUriEnum.AdjacentMeetings, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(AdjacentOrConflictingMeetingsProperty.CreateCommandForAdjacentMeetings), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x0400082B RID: 2091
		public static readonly PropertyInformation AppointmentReplyTime = new PropertyInformation(PropertyUriEnum.AppointmentReplyTime, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.AppointmentReplyTime, new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400082C RID: 2092
		public static readonly PropertyInformation AppointmentSequenceNumber = new PropertyInformation(PropertyUriEnum.AppointmentSequenceNumber, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.AppointmentSequenceNumber, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400082D RID: 2093
		public static readonly PropertyInformation AppointmentState = new PropertyInformation(PropertyUriEnum.AppointmentState, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.AppointmentState, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400082E RID: 2094
		public static readonly PropertyInformation CalendarItemType = new PropertyInformation(PropertyUriEnum.CalendarItemType, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.CalendarItemType, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400082F RID: 2095
		public static readonly PropertyInformation ConflictingMeetingCount = new PropertyInformation(PropertyUriEnum.ConflictingMeetingCount, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(AdjacentOrConflictingMeetingsProperty.CreateCommandForConflictingMeetingCount), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000830 RID: 2096
		public static readonly PropertyInformation ConflictingMeetings = new PropertyInformation(PropertyUriEnum.ConflictingMeetings, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(AdjacentOrConflictingMeetingsProperty.CreateCommandForConflictingMeetings), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000831 RID: 2097
		public static readonly PropertyInformation DeletedOccurrences = new PropertyInformation(PropertyUriEnum.DeletedOccurrences, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(OccurrenceInfoProperty.CreateCommandForDeletedOccurrences), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000832 RID: 2098
		public static readonly PropertyInformation Duration = new PropertyInformation(PropertyUriEnum.Duration.ToString(), ServiceXml.GetFullyQualifiedName(PropertyUriEnum.Duration.ToString()), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007, new PropertyDefinition[]
		{
			CalendarItemInstanceSchema.StartTime,
			CalendarItemInstanceSchema.EndTime
		}, new PropertyUri(PropertyUriEnum.Duration), new PropertyCommand.CreatePropertyCommand(DurationProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000833 RID: 2099
		public static readonly PropertyInformation End = new PropertyInformation(PropertyUriEnum.End, ExchangeVersion.Exchange2007, CalendarItemInstanceSchema.EndTime, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForEnd), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000834 RID: 2100
		public static readonly PropertyInformation FirstOccurrence = new PropertyInformation(PropertyUriEnum.FirstOccurrence, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(OccurrenceInfoProperty.CreateCommandForFirstOccurrence), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000835 RID: 2101
		public static readonly PropertyInformation ICalendarDateTimeStamp = new PropertyInformation(PropertyUriEnum.DateTimeStamp.ToString(), ServiceXml.GetFullyQualifiedName(PropertyUriEnum.DateTimeStamp.ToString()), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007SP1, ICalendar.DateTimeStampProperty.PropertiesToLoad, new PropertyUri(PropertyUriEnum.DateTimeStamp), new PropertyCommand.CreatePropertyCommand(ICalendar.DateTimeStampProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000836 RID: 2102
		public static readonly PropertyInformation ICalendarRecurrenceId = new PropertyInformation(PropertyUriEnum.RecurrenceId.ToString(), ServiceXml.GetFullyQualifiedName(PropertyUriEnum.RecurrenceId.ToString()), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007SP1, ICalendar.RecurrenceIdProperty.PropertiesToLoad, new PropertyUri(PropertyUriEnum.RecurrenceId), new PropertyCommand.CreatePropertyCommand(ICalendar.RecurrenceIdProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000837 RID: 2103
		public static readonly PropertyInformation ICalendarUid = new PropertyInformation(PropertyUriEnum.UID, ExchangeVersion.Exchange2007SP1, ICalendar.UidProperty.PropertyToLoad, new PropertyCommand.CreatePropertyCommand(ICalendar.UidProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000838 RID: 2104
		public static readonly PropertyInformation IsAllDayEvent = new PropertyInformation(PropertyUriEnum.IsAllDayEvent, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.MapiIsAllDayEvent, new PropertyCommand.CreatePropertyCommand(IsAllDayEventProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000839 RID: 2105
		public static readonly PropertyInformation IsCancelled = new PropertyInformation(PropertyUriEnum.IsCancelled, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(IsCancelledProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400083A RID: 2106
		public static readonly PropertyInformation IsSeriesCancelled = new PropertyInformation(PropertyUriEnum.IsSeriesCancelled, ExchangeVersion.Exchange2012, CalendarItemOccurrenceSchema.IsSeriesCancelled, new PropertyCommand.CreatePropertyCommand(IsSeriesCancelledProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400083B RID: 2107
		public static readonly PropertyInformation IsMeeting = new PropertyInformation(PropertyUriEnum.IsMeeting, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.IsMeeting, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400083C RID: 2108
		public static readonly PropertyInformation IsRecurring = new PropertyInformation(PropertyUriEnum.IsRecurring, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.IsRecurring, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400083D RID: 2109
		public static readonly PropertyInformation LastOccurrence = new PropertyInformation(PropertyUriEnum.LastOccurrence, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(OccurrenceInfoProperty.CreateCommandForLastOccurrence), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x0400083E RID: 2110
		public static readonly PropertyInformation LegacyFreeBusyStatus = new PropertyInformation(PropertyUriEnum.LegacyFreeBusyStatus, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.FreeBusyStatus, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400083F RID: 2111
		public static readonly PropertyInformation Location = new PropertyInformation(PropertyUriEnum.Location, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.Location, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000840 RID: 2112
		public static readonly PropertyInformation EnhancedLocation = new PropertyInformation(PropertyUriEnum.EnhancedLocation.ToString(), ServiceXml.GetFullyQualifiedName(PropertyUriEnum.EnhancedLocation.ToString()), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2012, EnhancedLocationProperty.LocationProperties, new PropertyUri(PropertyUriEnum.EnhancedLocation), new PropertyCommand.CreatePropertyCommand(EnhancedLocationProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000841 RID: 2113
		public static readonly PropertyInformation MeetingRequestWasSent = new PropertyInformation(PropertyUriEnum.MeetingRequestWasSent, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.MeetingRequestWasSent, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000842 RID: 2114
		public static readonly PropertyInformation MeetingTimeZone = new PropertyInformation(PropertyUriEnum.MeetingTimeZone, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(MeetingTimeZoneProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000843 RID: 2115
		public static readonly PropertyInformation ModifiedOccurrences = new PropertyInformation(PropertyUriEnum.ModifiedOccurrences, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(OccurrenceInfoProperty.CreateCommandForModifiedOccurrences), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000844 RID: 2116
		public static readonly PropertyInformation MyResponseType = new PropertyInformation(PropertyUriEnum.MyResponseType, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.ResponseType, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000845 RID: 2117
		public static readonly PropertyInformation Organizer = new PropertyInformation(PropertyUriEnum.Organizer.ToString(), ServiceXml.GetFullyQualifiedName(PropertyUriEnum.Organizer.ToString()), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007, new PropertyDefinition[]
		{
			CalendarItemBaseSchema.OrganizerDisplayName,
			CalendarItemBaseSchema.OrganizerEmailAddress,
			CalendarItemBaseSchema.OrganizerType
		}, new PropertyUri(PropertyUriEnum.Organizer), new PropertyCommand.CreatePropertyCommand(OrganizerProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000846 RID: 2118
		public static readonly PropertyInformation OriginalStart = new PropertyInformation(PropertyUriEnum.OriginalStart, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(OriginalStartProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000847 RID: 2119
		public static readonly PropertyInformation StartWallClock = new PropertyInformation(PropertyUriEnum.StartWallClock, ExchangeVersion.Exchange2012, CalendarItemInstanceSchema.StartWallClock, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForStartWallClock), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000848 RID: 2120
		public static readonly PropertyInformation EndWallClock = new PropertyInformation(PropertyUriEnum.EndWallClock, ExchangeVersion.Exchange2012, CalendarItemInstanceSchema.EndWallClock, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForEndWallClock), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000849 RID: 2121
		public static readonly PropertyInformation StartTimeZoneId = new PropertyInformation(PropertyUriEnum.StartTimeZoneId, ExchangeVersion.Exchange2012, CalendarItemBaseSchema.StartTimeZoneId, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400084A RID: 2122
		public static readonly PropertyInformation EndTimeZoneId = new PropertyInformation(PropertyUriEnum.EndTimeZoneId, ExchangeVersion.Exchange2012, CalendarItemBaseSchema.EndTimeZoneId, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400084B RID: 2123
		public static readonly PropertyInformation Start = new PropertyInformation(PropertyUriEnum.Start, ExchangeVersion.Exchange2007, CalendarItemInstanceSchema.StartTime, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForStart), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400084C RID: 2124
		public static readonly PropertyInformation TimeZone = new PropertyInformation(PropertyUriEnum.TimeZone, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.TimeZone, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400084D RID: 2125
		public static readonly PropertyInformation When = new PropertyInformation(PropertyUriEnum.When, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.When, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400084E RID: 2126
		public static readonly PropertyInformation IntendedFreeBusyStatus = new PropertyInformation(PropertyUriEnum.IntendedFreeBusyStatus, ExchangeVersion.Exchange2012, MeetingRequestSchema.IntendedFreeBusyStatus, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400084F RID: 2127
		public static readonly PropertyInformation JoinOnlineMeetingUrl = new PropertyInformation(PropertyUriEnum.JoinOnlineMeetingUrl, ExchangeVersion.Exchange2012, CalendarItemBaseSchema.OnlineMeetingExternalLink, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000850 RID: 2128
		public static readonly PropertyInformation OnlineMeetingSettings = new PropertyInformation(PropertyUriEnum.OnlineMeetingSettings, ExchangeVersion.Exchange2012, null, new PropertyCommand.CreatePropertyCommand(OnlineMeetingSettingsProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000851 RID: 2129
		public static readonly PropertyInformation IsOrganizer = new PropertyInformation(PropertyUriEnum.IsOrganizer, ExchangeVersion.Exchange2012, CalendarItemBaseSchema.IsOrganizer, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000852 RID: 2130
		public static readonly PropertyInformation AppointmentReplyName = new PropertyInformation(PropertyUriEnum.AppointmentReplyName, ExchangeVersion.Exchange2012, CalendarItemBaseSchema.AppointmentReplyName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000853 RID: 2131
		public static readonly PropertyInformation InboxReminders = new PropertyInformation(PropertyUriEnum.InboxReminders, ExchangeVersion.Exchange2012, null, new PropertyCommand.CreatePropertyCommand(InboxRemindersProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000854 RID: 2132
		private static Schema schemaForOrganizer_Exchange2010AndEarlier;

		// Token: 0x04000855 RID: 2133
		private static Schema schemaForAttendee_Exchange2010AndEarlier;

		// Token: 0x04000856 RID: 2134
		private static Schema schemaForOrganizer_Exchange2010SP1AndLater;

		// Token: 0x04000857 RID: 2135
		private static Schema schemaForAttendee_Exchange2010SP1AndLater;

		// Token: 0x0200019B RID: 411
		internal static class OrganizerSpecific
		{
			// Token: 0x04000858 RID: 2136
			public static readonly PropertyInformation AllowNewTimeProposal = new PropertyInformation(PropertyUriEnum.AllowNewTimeProposal, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.DisallowNewTimeProposal, new PropertyCommand.CreatePropertyCommand(AllowNewTimeProposalProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

			// Token: 0x04000859 RID: 2137
			public static readonly PropertyInformation ConferenceType = new PropertyInformation(PropertyUriEnum.ConferenceType, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.ConferenceType, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

			// Token: 0x0400085A RID: 2138
			public static readonly PropertyInformation IsOnlineMeeting = new PropertyInformation(PropertyUriEnum.IsOnlineMeeting, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.IsOnlineMeeting, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

			// Token: 0x0400085B RID: 2139
			public static readonly PropertyInformation IsResponseRequested = new PropertyInformation("IsResponseRequested", ExchangeVersion.Exchange2007, ItemSchema.IsResponseRequested, new PropertyUri(PropertyUriEnum.CalendarIsResponseRequested), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

			// Token: 0x0400085C RID: 2140
			public static readonly PropertyInformation MeetingWorkspaceUrl = new PropertyInformation(PropertyUriEnum.MeetingWorkspaceUrl, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.MeetingWorkspaceUrl, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

			// Token: 0x0400085D RID: 2141
			public static readonly PropertyInformation NetShowUrl = new PropertyInformation(PropertyUriEnum.NetShowUrl, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.NetShowURL, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

			// Token: 0x0400085E RID: 2142
			public static readonly PropertyInformation OptionalAttendees = new PropertyInformation(PropertyUriEnum.OptionalAttendees, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(AttendeesProperty.CreateCommandForOptionalAttendees), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsAppendUpdateCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

			// Token: 0x0400085F RID: 2143
			public static readonly PropertyInformation Recurrence = new PropertyInformation(PropertyUriEnum.Recurrence, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(CalendarItemRecurrenceProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

			// Token: 0x04000860 RID: 2144
			public static readonly PropertyInformation RequiredAttendees = new PropertyInformation(PropertyUriEnum.RequiredAttendees, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(AttendeesProperty.CreateCommandForRequiredAttendees), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsAppendUpdateCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

			// Token: 0x04000861 RID: 2145
			public static readonly PropertyInformation Resources = new PropertyInformation(PropertyUriEnum.Resources, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(AttendeesProperty.CreateCommandForResources), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsAppendUpdateCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

			// Token: 0x04000862 RID: 2146
			public static readonly PropertyInformation StartTimeZone = new PropertyInformation(PropertyUriEnum.StartTimeZone, ExchangeVersion.Exchange2010, TimeZoneProperty.StartTimeZonePropertyDefinition, new PropertyCommand.CreatePropertyCommand(TimeZoneProperty.CreateCommandForStart), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

			// Token: 0x04000863 RID: 2147
			public static readonly PropertyInformation EndTimeZone = new PropertyInformation(PropertyUriEnum.EndTimeZone, ExchangeVersion.Exchange2010, TimeZoneProperty.EndTimeZonePropertyDefinition, new PropertyCommand.CreatePropertyCommand(TimeZoneProperty.CreateCommandForEnd), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

			// Token: 0x04000864 RID: 2148
			public static readonly PropertyInformation AttendeeCounts = new PropertyInformation(PropertyUriEnum.AttendeeCounts, ExchangeVersion.Exchange2013, null, new PropertyCommand.CreatePropertyCommand(AttendeeCountsProperty.CreateCommand), PropertyInformationAttributes.ImplementsToServiceObjectCommand);
		}

		// Token: 0x0200019C RID: 412
		internal static class AttendeeSpecific
		{
			// Token: 0x04000865 RID: 2149
			public static readonly PropertyInformation AllowNewTimeProposal = new PropertyInformation(PropertyUriEnum.AllowNewTimeProposal, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.DisallowNewTimeProposal, new PropertyCommand.CreatePropertyCommand(AllowNewTimeProposalProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

			// Token: 0x04000866 RID: 2150
			public static readonly PropertyInformation ConferenceType = new PropertyInformation(PropertyUriEnum.ConferenceType, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.ConferenceType, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

			// Token: 0x04000867 RID: 2151
			public static readonly PropertyInformation IsOnlineMeeting = new PropertyInformation(PropertyUriEnum.IsOnlineMeeting, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.IsOnlineMeeting, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

			// Token: 0x04000868 RID: 2152
			public static readonly PropertyInformation IsResponseRequested = new PropertyInformation("IsResponseRequested", ExchangeVersion.Exchange2007, ItemSchema.IsResponseRequested, new PropertyUri(PropertyUriEnum.CalendarIsResponseRequested), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

			// Token: 0x04000869 RID: 2153
			public static readonly PropertyInformation MeetingWorkspaceUrl = new PropertyInformation(PropertyUriEnum.MeetingWorkspaceUrl, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.MeetingWorkspaceUrl, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

			// Token: 0x0400086A RID: 2154
			public static readonly PropertyInformation NetShowUrl = new PropertyInformation(PropertyUriEnum.NetShowUrl, ExchangeVersion.Exchange2007, CalendarItemBaseSchema.NetShowURL, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

			// Token: 0x0400086B RID: 2155
			public static readonly PropertyInformation OptionalAttendees = new PropertyInformation(PropertyUriEnum.OptionalAttendees, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(AttendeesProperty.CreateCommandForOptionalAttendees), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

			// Token: 0x0400086C RID: 2156
			public static readonly PropertyInformation Recurrence = new PropertyInformation(PropertyUriEnum.Recurrence, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(CalendarItemRecurrenceProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

			// Token: 0x0400086D RID: 2157
			public static readonly PropertyInformation RequiredAttendees = new PropertyInformation(PropertyUriEnum.RequiredAttendees, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(AttendeesProperty.CreateCommandForRequiredAttendees), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

			// Token: 0x0400086E RID: 2158
			public static readonly PropertyInformation Resources = new PropertyInformation(PropertyUriEnum.Resources, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(AttendeesProperty.CreateCommandForResources), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

			// Token: 0x0400086F RID: 2159
			public static readonly PropertyInformation StartTimeZone = new PropertyInformation(PropertyUriEnum.StartTimeZone, ExchangeVersion.Exchange2010, TimeZoneProperty.StartTimeZonePropertyDefinition, new PropertyCommand.CreatePropertyCommand(TimeZoneProperty.CreateCommandForStart), PropertyInformationAttributes.ImplementsReadOnlyCommands);

			// Token: 0x04000870 RID: 2160
			public static readonly PropertyInformation EndTimeZone = new PropertyInformation(PropertyUriEnum.EndTimeZone, ExchangeVersion.Exchange2010, TimeZoneProperty.EndTimeZonePropertyDefinition, new PropertyCommand.CreatePropertyCommand(TimeZoneProperty.CreateCommandForEnd), PropertyInformationAttributes.ImplementsReadOnlyCommands);

			// Token: 0x04000871 RID: 2161
			public static readonly PropertyInformation AttendeeCounts = new PropertyInformation(PropertyUriEnum.AttendeeCounts, ExchangeVersion.Exchange2013, null, new PropertyCommand.CreatePropertyCommand(AttendeeCountsProperty.CreateCommand), PropertyInformationAttributes.ImplementsToServiceObjectCommand);
		}
	}
}
