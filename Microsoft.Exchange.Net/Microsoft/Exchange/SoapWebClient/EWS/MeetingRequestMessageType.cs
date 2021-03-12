using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200023E RID: 574
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MeetingRequestMessageType : MeetingMessageType
	{
		// Token: 0x04000EE7 RID: 3815
		public MeetingRequestTypeType MeetingRequestType;

		// Token: 0x04000EE8 RID: 3816
		[XmlIgnore]
		public bool MeetingRequestTypeSpecified;

		// Token: 0x04000EE9 RID: 3817
		public LegacyFreeBusyType IntendedFreeBusyStatus;

		// Token: 0x04000EEA RID: 3818
		[XmlIgnore]
		public bool IntendedFreeBusyStatusSpecified;

		// Token: 0x04000EEB RID: 3819
		public DateTime Start;

		// Token: 0x04000EEC RID: 3820
		[XmlIgnore]
		public bool StartSpecified;

		// Token: 0x04000EED RID: 3821
		public DateTime End;

		// Token: 0x04000EEE RID: 3822
		[XmlIgnore]
		public bool EndSpecified;

		// Token: 0x04000EEF RID: 3823
		public DateTime OriginalStart;

		// Token: 0x04000EF0 RID: 3824
		[XmlIgnore]
		public bool OriginalStartSpecified;

		// Token: 0x04000EF1 RID: 3825
		public bool IsAllDayEvent;

		// Token: 0x04000EF2 RID: 3826
		[XmlIgnore]
		public bool IsAllDayEventSpecified;

		// Token: 0x04000EF3 RID: 3827
		public LegacyFreeBusyType LegacyFreeBusyStatus;

		// Token: 0x04000EF4 RID: 3828
		[XmlIgnore]
		public bool LegacyFreeBusyStatusSpecified;

		// Token: 0x04000EF5 RID: 3829
		public string Location;

		// Token: 0x04000EF6 RID: 3830
		public string When;

		// Token: 0x04000EF7 RID: 3831
		public bool IsMeeting;

		// Token: 0x04000EF8 RID: 3832
		[XmlIgnore]
		public bool IsMeetingSpecified;

		// Token: 0x04000EF9 RID: 3833
		public bool IsCancelled;

		// Token: 0x04000EFA RID: 3834
		[XmlIgnore]
		public bool IsCancelledSpecified;

		// Token: 0x04000EFB RID: 3835
		public bool IsRecurring;

		// Token: 0x04000EFC RID: 3836
		[XmlIgnore]
		public bool IsRecurringSpecified;

		// Token: 0x04000EFD RID: 3837
		public bool MeetingRequestWasSent;

		// Token: 0x04000EFE RID: 3838
		[XmlIgnore]
		public bool MeetingRequestWasSentSpecified;

		// Token: 0x04000EFF RID: 3839
		public CalendarItemTypeType CalendarItemType;

		// Token: 0x04000F00 RID: 3840
		[XmlIgnore]
		public bool CalendarItemTypeSpecified;

		// Token: 0x04000F01 RID: 3841
		public ResponseTypeType MyResponseType;

		// Token: 0x04000F02 RID: 3842
		[XmlIgnore]
		public bool MyResponseTypeSpecified;

		// Token: 0x04000F03 RID: 3843
		public SingleRecipientType Organizer;

		// Token: 0x04000F04 RID: 3844
		[XmlArrayItem("Attendee", IsNullable = false)]
		public AttendeeType[] RequiredAttendees;

		// Token: 0x04000F05 RID: 3845
		[XmlArrayItem("Attendee", IsNullable = false)]
		public AttendeeType[] OptionalAttendees;

		// Token: 0x04000F06 RID: 3846
		[XmlArrayItem("Attendee", IsNullable = false)]
		public AttendeeType[] Resources;

		// Token: 0x04000F07 RID: 3847
		public int ConflictingMeetingCount;

		// Token: 0x04000F08 RID: 3848
		[XmlIgnore]
		public bool ConflictingMeetingCountSpecified;

		// Token: 0x04000F09 RID: 3849
		public int AdjacentMeetingCount;

		// Token: 0x04000F0A RID: 3850
		[XmlIgnore]
		public bool AdjacentMeetingCountSpecified;

		// Token: 0x04000F0B RID: 3851
		public NonEmptyArrayOfAllItemsType ConflictingMeetings;

		// Token: 0x04000F0C RID: 3852
		public NonEmptyArrayOfAllItemsType AdjacentMeetings;

		// Token: 0x04000F0D RID: 3853
		public string Duration;

		// Token: 0x04000F0E RID: 3854
		public string TimeZone;

		// Token: 0x04000F0F RID: 3855
		public DateTime AppointmentReplyTime;

		// Token: 0x04000F10 RID: 3856
		[XmlIgnore]
		public bool AppointmentReplyTimeSpecified;

		// Token: 0x04000F11 RID: 3857
		public int AppointmentSequenceNumber;

		// Token: 0x04000F12 RID: 3858
		[XmlIgnore]
		public bool AppointmentSequenceNumberSpecified;

		// Token: 0x04000F13 RID: 3859
		public int AppointmentState;

		// Token: 0x04000F14 RID: 3860
		[XmlIgnore]
		public bool AppointmentStateSpecified;

		// Token: 0x04000F15 RID: 3861
		public RecurrenceType Recurrence;

		// Token: 0x04000F16 RID: 3862
		public OccurrenceInfoType FirstOccurrence;

		// Token: 0x04000F17 RID: 3863
		public OccurrenceInfoType LastOccurrence;

		// Token: 0x04000F18 RID: 3864
		[XmlArrayItem("Occurrence", IsNullable = false)]
		public OccurrenceInfoType[] ModifiedOccurrences;

		// Token: 0x04000F19 RID: 3865
		[XmlArrayItem("DeletedOccurrence", IsNullable = false)]
		public DeletedOccurrenceInfoType[] DeletedOccurrences;

		// Token: 0x04000F1A RID: 3866
		public TimeZoneType MeetingTimeZone;

		// Token: 0x04000F1B RID: 3867
		public TimeZoneDefinitionType StartTimeZone;

		// Token: 0x04000F1C RID: 3868
		public TimeZoneDefinitionType EndTimeZone;

		// Token: 0x04000F1D RID: 3869
		public int ConferenceType;

		// Token: 0x04000F1E RID: 3870
		[XmlIgnore]
		public bool ConferenceTypeSpecified;

		// Token: 0x04000F1F RID: 3871
		public bool AllowNewTimeProposal;

		// Token: 0x04000F20 RID: 3872
		[XmlIgnore]
		public bool AllowNewTimeProposalSpecified;

		// Token: 0x04000F21 RID: 3873
		public bool IsOnlineMeeting;

		// Token: 0x04000F22 RID: 3874
		[XmlIgnore]
		public bool IsOnlineMeetingSpecified;

		// Token: 0x04000F23 RID: 3875
		public string MeetingWorkspaceUrl;

		// Token: 0x04000F24 RID: 3876
		public string NetShowUrl;

		// Token: 0x04000F25 RID: 3877
		public EnhancedLocationType EnhancedLocation;

		// Token: 0x04000F26 RID: 3878
		public ChangeHighlightsType ChangeHighlights;

		// Token: 0x04000F27 RID: 3879
		public DateTime StartWallClock;

		// Token: 0x04000F28 RID: 3880
		[XmlIgnore]
		public bool StartWallClockSpecified;

		// Token: 0x04000F29 RID: 3881
		public DateTime EndWallClock;

		// Token: 0x04000F2A RID: 3882
		[XmlIgnore]
		public bool EndWallClockSpecified;

		// Token: 0x04000F2B RID: 3883
		public string StartTimeZoneId;

		// Token: 0x04000F2C RID: 3884
		public string EndTimeZoneId;
	}
}
