using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200020B RID: 523
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class CalendarItemType : ItemType
	{
		// Token: 0x04000D87 RID: 3463
		public string UID;

		// Token: 0x04000D88 RID: 3464
		public DateTime RecurrenceId;

		// Token: 0x04000D89 RID: 3465
		[XmlIgnore]
		public bool RecurrenceIdSpecified;

		// Token: 0x04000D8A RID: 3466
		public DateTime DateTimeStamp;

		// Token: 0x04000D8B RID: 3467
		[XmlIgnore]
		public bool DateTimeStampSpecified;

		// Token: 0x04000D8C RID: 3468
		public DateTime Start;

		// Token: 0x04000D8D RID: 3469
		[XmlIgnore]
		public bool StartSpecified;

		// Token: 0x04000D8E RID: 3470
		public DateTime End;

		// Token: 0x04000D8F RID: 3471
		[XmlIgnore]
		public bool EndSpecified;

		// Token: 0x04000D90 RID: 3472
		public DateTime OriginalStart;

		// Token: 0x04000D91 RID: 3473
		[XmlIgnore]
		public bool OriginalStartSpecified;

		// Token: 0x04000D92 RID: 3474
		public bool IsAllDayEvent;

		// Token: 0x04000D93 RID: 3475
		[XmlIgnore]
		public bool IsAllDayEventSpecified;

		// Token: 0x04000D94 RID: 3476
		public LegacyFreeBusyType LegacyFreeBusyStatus;

		// Token: 0x04000D95 RID: 3477
		[XmlIgnore]
		public bool LegacyFreeBusyStatusSpecified;

		// Token: 0x04000D96 RID: 3478
		public string Location;

		// Token: 0x04000D97 RID: 3479
		public string When;

		// Token: 0x04000D98 RID: 3480
		public bool IsMeeting;

		// Token: 0x04000D99 RID: 3481
		[XmlIgnore]
		public bool IsMeetingSpecified;

		// Token: 0x04000D9A RID: 3482
		public bool IsCancelled;

		// Token: 0x04000D9B RID: 3483
		[XmlIgnore]
		public bool IsCancelledSpecified;

		// Token: 0x04000D9C RID: 3484
		public bool IsRecurring;

		// Token: 0x04000D9D RID: 3485
		[XmlIgnore]
		public bool IsRecurringSpecified;

		// Token: 0x04000D9E RID: 3486
		public bool MeetingRequestWasSent;

		// Token: 0x04000D9F RID: 3487
		[XmlIgnore]
		public bool MeetingRequestWasSentSpecified;

		// Token: 0x04000DA0 RID: 3488
		public bool IsResponseRequested;

		// Token: 0x04000DA1 RID: 3489
		[XmlIgnore]
		public bool IsResponseRequestedSpecified;

		// Token: 0x04000DA2 RID: 3490
		[XmlElement("CalendarItemType")]
		public CalendarItemTypeType CalendarItemType1;

		// Token: 0x04000DA3 RID: 3491
		[XmlIgnore]
		public bool CalendarItemType1Specified;

		// Token: 0x04000DA4 RID: 3492
		public ResponseTypeType MyResponseType;

		// Token: 0x04000DA5 RID: 3493
		[XmlIgnore]
		public bool MyResponseTypeSpecified;

		// Token: 0x04000DA6 RID: 3494
		public SingleRecipientType Organizer;

		// Token: 0x04000DA7 RID: 3495
		[XmlArrayItem("Attendee", IsNullable = false)]
		public AttendeeType[] RequiredAttendees;

		// Token: 0x04000DA8 RID: 3496
		[XmlArrayItem("Attendee", IsNullable = false)]
		public AttendeeType[] OptionalAttendees;

		// Token: 0x04000DA9 RID: 3497
		[XmlArrayItem("Attendee", IsNullable = false)]
		public AttendeeType[] Resources;

		// Token: 0x04000DAA RID: 3498
		public int ConflictingMeetingCount;

		// Token: 0x04000DAB RID: 3499
		[XmlIgnore]
		public bool ConflictingMeetingCountSpecified;

		// Token: 0x04000DAC RID: 3500
		public int AdjacentMeetingCount;

		// Token: 0x04000DAD RID: 3501
		[XmlIgnore]
		public bool AdjacentMeetingCountSpecified;

		// Token: 0x04000DAE RID: 3502
		public NonEmptyArrayOfAllItemsType ConflictingMeetings;

		// Token: 0x04000DAF RID: 3503
		public NonEmptyArrayOfAllItemsType AdjacentMeetings;

		// Token: 0x04000DB0 RID: 3504
		public string Duration;

		// Token: 0x04000DB1 RID: 3505
		public string TimeZone;

		// Token: 0x04000DB2 RID: 3506
		public DateTime AppointmentReplyTime;

		// Token: 0x04000DB3 RID: 3507
		[XmlIgnore]
		public bool AppointmentReplyTimeSpecified;

		// Token: 0x04000DB4 RID: 3508
		public int AppointmentSequenceNumber;

		// Token: 0x04000DB5 RID: 3509
		[XmlIgnore]
		public bool AppointmentSequenceNumberSpecified;

		// Token: 0x04000DB6 RID: 3510
		public int AppointmentState;

		// Token: 0x04000DB7 RID: 3511
		[XmlIgnore]
		public bool AppointmentStateSpecified;

		// Token: 0x04000DB8 RID: 3512
		public RecurrenceType Recurrence;

		// Token: 0x04000DB9 RID: 3513
		public OccurrenceInfoType FirstOccurrence;

		// Token: 0x04000DBA RID: 3514
		public OccurrenceInfoType LastOccurrence;

		// Token: 0x04000DBB RID: 3515
		[XmlArrayItem("Occurrence", IsNullable = false)]
		public OccurrenceInfoType[] ModifiedOccurrences;

		// Token: 0x04000DBC RID: 3516
		[XmlArrayItem("DeletedOccurrence", IsNullable = false)]
		public DeletedOccurrenceInfoType[] DeletedOccurrences;

		// Token: 0x04000DBD RID: 3517
		public TimeZoneType MeetingTimeZone;

		// Token: 0x04000DBE RID: 3518
		public TimeZoneDefinitionType StartTimeZone;

		// Token: 0x04000DBF RID: 3519
		public TimeZoneDefinitionType EndTimeZone;

		// Token: 0x04000DC0 RID: 3520
		public int ConferenceType;

		// Token: 0x04000DC1 RID: 3521
		[XmlIgnore]
		public bool ConferenceTypeSpecified;

		// Token: 0x04000DC2 RID: 3522
		public bool AllowNewTimeProposal;

		// Token: 0x04000DC3 RID: 3523
		[XmlIgnore]
		public bool AllowNewTimeProposalSpecified;

		// Token: 0x04000DC4 RID: 3524
		public bool IsOnlineMeeting;

		// Token: 0x04000DC5 RID: 3525
		[XmlIgnore]
		public bool IsOnlineMeetingSpecified;

		// Token: 0x04000DC6 RID: 3526
		public string MeetingWorkspaceUrl;

		// Token: 0x04000DC7 RID: 3527
		public string NetShowUrl;

		// Token: 0x04000DC8 RID: 3528
		public EnhancedLocationType EnhancedLocation;

		// Token: 0x04000DC9 RID: 3529
		public DateTime StartWallClock;

		// Token: 0x04000DCA RID: 3530
		[XmlIgnore]
		public bool StartWallClockSpecified;

		// Token: 0x04000DCB RID: 3531
		public DateTime EndWallClock;

		// Token: 0x04000DCC RID: 3532
		[XmlIgnore]
		public bool EndWallClockSpecified;

		// Token: 0x04000DCD RID: 3533
		public string StartTimeZoneId;

		// Token: 0x04000DCE RID: 3534
		public string EndTimeZoneId;

		// Token: 0x04000DCF RID: 3535
		public LegacyFreeBusyType IntendedFreeBusyStatus;

		// Token: 0x04000DD0 RID: 3536
		[XmlIgnore]
		public bool IntendedFreeBusyStatusSpecified;

		// Token: 0x04000DD1 RID: 3537
		public string JoinOnlineMeetingUrl;

		// Token: 0x04000DD2 RID: 3538
		public OnlineMeetingSettingsType OnlineMeetingSettings;

		// Token: 0x04000DD3 RID: 3539
		public bool IsOrganizer;

		// Token: 0x04000DD4 RID: 3540
		[XmlIgnore]
		public bool IsOrganizerSpecified;
	}
}
