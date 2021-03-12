using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003B0 RID: 944
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class CalendarItemProperties
	{
		// Token: 0x04001829 RID: 6185
		private static readonly CalendarItemProperties.Property[] allProperties = new CalendarItemProperties.Property[]
		{
			new CalendarItemProperties.Property(InternalSchema.AppointmentRecurring, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.AppointmentSequenceNumber, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.CleanGlobalObjectId, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.GlobalObjectId, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.IsException, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.IsRecurring, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.MapiEndTime, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.MapiStartTime, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.OwnerAppointmentID, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.OwnerCriticalChangeTime, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.StartRecurDate, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.StartRecurTime, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.TimeZone, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.TimeZoneBlob, true, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.LocationDisplayName, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.LocationAnnotation, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.LocationSource, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.LocationUri, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.Latitude, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.Longitude, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.Accuracy, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.Altitude, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.AltitudeAccuracy, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.LocationStreet, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.LocationCity, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.LocationState, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.LocationCountry, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.LocationPostalCode, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.AppointmentRecurrenceBlob, false, true, true, false),
			new CalendarItemProperties.Property(InternalSchema.Location, false, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.AppointmentState, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.ClipEndTime, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.ClipStartTime, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.Duration, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.MapiIsAllDayEvent, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.MapiPREndDate, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.MapiPRStartDate, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.MapiRecurrenceType, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.OutlookUserPropsPropDefStream, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.RecurrencePattern, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.ReminderDueByInternal, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.TimeZoneDefinitionEnd, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.TimeZoneDefinitionRecurring, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.TimeZoneDefinitionStart, true, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.CalendarOriginatorId, true, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.ConferenceInfo, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.ConferenceTelURI, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.FreeBusyStatus, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.IntendedFreeBusyStatus, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.OnlineMeetingConfLink, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.OnlineMeetingExternalLink, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.OnlineMeetingInternalLink, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.ReminderIsSetInternal, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.ReminderMinutesBeforeStartInternal, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.ReminderNextTime, false, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.ReplyTime, false, false, true, false),
			new CalendarItemProperties.Property(InternalSchema.UCCapabilities, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.UCInband, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.UCMeetingSetting, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.UCMeetingSettingSent, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.UCOpenedConferenceID, false, false, true, true),
			new CalendarItemProperties.Property(InternalSchema.ConversationId, false, true, false, true),
			new CalendarItemProperties.Property(InternalSchema.MapiSubject, false, true, false, true),
			new CalendarItemProperties.Property(InternalSchema.NormalizedSubjectInternal, false, true, false, true),
			new CalendarItemProperties.Property(InternalSchema.SubjectPrefixInternal, false, true, false, true),
			new CalendarItemProperties.Property(InternalSchema.AppointmentSequenceTime, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.AttendeeCriticalChangeTime, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.BillingInformation, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.Categories, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.ChangeHighlight, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.Companies, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.ConferenceType, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.Contact, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.ContactURL, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.ConversationIndex, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.ConversationTopic, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.DisallowNewTimeProposal, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.DisplayAttendeesAll, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.DisplayAttendeesCc, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.DisplayAttendeesTo, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.Importance, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.InternetCpid, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.IsOnlineMeeting, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.IsReplyRequested, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.IsResponseRequested, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.IsSilent, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.Keywords, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.LocationURL, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.MarkedForDownload, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.MeetingRequestType, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.MeetingWorkspaceUrl, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.Mileage, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.NetMeetingConferenceSerPassword, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.NetMeetingConferenceServerAllowExternal, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.NetMeetingDocPathName, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.NetMeetingOrganizerAlias, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.NetMeetingServer, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.NetShowURL, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.OldLocation, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.OnlineMeetingChanged, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.OutlookUserPropsCustomFlag, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.OutlookUserPropsFormPropStream, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.OutlookUserPropsFormStorage, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.OutlookUserPropsPageDirStream, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.OutlookUserPropsScriptStream, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.OutlookUserPropsVerbStream, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.Privacy, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.ResponseState, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.RtfInSync, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.SentRepresentingDisplayName, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.SentRepresentingEmailAddress, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.SentRepresentingEntryId, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.SentRepresentingType, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.SentRepresentingSmtpAddress, true, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.DlpSenderOverride, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.DlpFalsePositive, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.DlpDetectedClassifications, true, false, false, false),
			new CalendarItemProperties.Property(InternalSchema.SeriesId, true, true, true, true),
			new CalendarItemProperties.Property(InternalSchema.EventClientId, false, false, false, true),
			new CalendarItemProperties.Property(InternalSchema.SeriesCreationHash, false, false, false, false)
		};

		// Token: 0x0400182A RID: 6186
		internal static readonly StorePropertyDefinition[] NonPreservableMeetingMessageProperties = (from p in CalendarItemProperties.allProperties
		where p.CopyToMeetingRequest
		select (StorePropertyDefinition)p.PropertyDefinition).ToArray<StorePropertyDefinition>();

		// Token: 0x0400182B RID: 6187
		internal static readonly StorePropertyDefinition[] MeetingResponseProperties = (from p in CalendarItemProperties.allProperties
		where p.CopyToMeetingResponse
		select (StorePropertyDefinition)p.PropertyDefinition).ToArray<StorePropertyDefinition>();

		// Token: 0x0400182C RID: 6188
		internal static readonly PropertyDefinition[] MeetingReplyForwardProperties = (from p in CalendarItemProperties.allProperties
		where p.CopyToMeetingReplyForward
		select p.PropertyDefinition).ToArray<PropertyDefinition>();

		// Token: 0x0400182D RID: 6189
		internal static readonly PropertyDefinition[] NPRInstanceProperties = (from p in CalendarItemProperties.allProperties
		where p.CopyToNPRInstance
		select p.PropertyDefinition).ToArray<PropertyDefinition>();

		// Token: 0x0400182E RID: 6190
		internal static readonly Tuple<StorePropertyDefinition, object>[] EnhancedLocationPropertiesWithDefaultValues = new Tuple<StorePropertyDefinition, object>[]
		{
			new Tuple<StorePropertyDefinition, object>(InternalSchema.LocationDisplayName, string.Empty),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.LocationAnnotation, string.Empty),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.LocationSource, LocationSource.None),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.LocationUri, string.Empty),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.Latitude, double.NaN),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.Longitude, double.NaN),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.Accuracy, double.NaN),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.Altitude, double.NaN),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.AltitudeAccuracy, double.NaN),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.LocationStreet, string.Empty),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.LocationCity, string.Empty),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.LocationState, string.Empty),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.LocationCountry, string.Empty),
			new Tuple<StorePropertyDefinition, object>(InternalSchema.LocationPostalCode, string.Empty)
		};

		// Token: 0x0400182F RID: 6191
		public static readonly StorePropertyDefinition[] EnhancedLocationPropertyDefinitions = (from p in CalendarItemProperties.EnhancedLocationPropertiesWithDefaultValues
		select p.Item1).ToArray<StorePropertyDefinition>();

		// Token: 0x020003B1 RID: 945
		private class Property
		{
			// Token: 0x17000E15 RID: 3605
			// (get) Token: 0x06002B2D RID: 11053 RVA: 0x000ACC4C File Offset: 0x000AAE4C
			// (set) Token: 0x06002B2E RID: 11054 RVA: 0x000ACC54 File Offset: 0x000AAE54
			public PropertyDefinition PropertyDefinition { get; set; }

			// Token: 0x17000E16 RID: 3606
			// (get) Token: 0x06002B2F RID: 11055 RVA: 0x000ACC5D File Offset: 0x000AAE5D
			// (set) Token: 0x06002B30 RID: 11056 RVA: 0x000ACC65 File Offset: 0x000AAE65
			public bool CopyToMeetingRequest { get; set; }

			// Token: 0x17000E17 RID: 3607
			// (get) Token: 0x06002B31 RID: 11057 RVA: 0x000ACC6E File Offset: 0x000AAE6E
			// (set) Token: 0x06002B32 RID: 11058 RVA: 0x000ACC76 File Offset: 0x000AAE76
			public bool CopyToMeetingResponse { get; set; }

			// Token: 0x17000E18 RID: 3608
			// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000ACC7F File Offset: 0x000AAE7F
			// (set) Token: 0x06002B34 RID: 11060 RVA: 0x000ACC87 File Offset: 0x000AAE87
			public bool CopyToMeetingReplyForward { get; set; }

			// Token: 0x17000E19 RID: 3609
			// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000ACC90 File Offset: 0x000AAE90
			// (set) Token: 0x06002B36 RID: 11062 RVA: 0x000ACC98 File Offset: 0x000AAE98
			public bool CopyToNPRInstance { get; set; }

			// Token: 0x06002B37 RID: 11063 RVA: 0x000ACCA1 File Offset: 0x000AAEA1
			public Property(PropertyDefinition propertyDefinition, bool copyToMeetingRequest, bool copyToMeetingResponse, bool copyToMeetingReplyForward, bool copyToNPRInstance)
			{
				this.PropertyDefinition = propertyDefinition;
				this.CopyToMeetingRequest = copyToMeetingRequest;
				this.CopyToMeetingResponse = copyToMeetingResponse;
				this.CopyToMeetingReplyForward = copyToMeetingReplyForward;
				this.CopyToNPRInstance = copyToNPRInstance;
			}
		}
	}
}
