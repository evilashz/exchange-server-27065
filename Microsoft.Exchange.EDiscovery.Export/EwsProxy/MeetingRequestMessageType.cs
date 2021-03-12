using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200015D RID: 349
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MeetingRequestMessageType : MeetingMessageType
	{
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00023081 File Offset: 0x00021281
		// (set) Token: 0x06000F42 RID: 3906 RVA: 0x00023089 File Offset: 0x00021289
		public MeetingRequestTypeType MeetingRequestType
		{
			get
			{
				return this.meetingRequestTypeField;
			}
			set
			{
				this.meetingRequestTypeField = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00023092 File Offset: 0x00021292
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x0002309A File Offset: 0x0002129A
		[XmlIgnore]
		public bool MeetingRequestTypeSpecified
		{
			get
			{
				return this.meetingRequestTypeFieldSpecified;
			}
			set
			{
				this.meetingRequestTypeFieldSpecified = value;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000230A3 File Offset: 0x000212A3
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x000230AB File Offset: 0x000212AB
		public LegacyFreeBusyType IntendedFreeBusyStatus
		{
			get
			{
				return this.intendedFreeBusyStatusField;
			}
			set
			{
				this.intendedFreeBusyStatusField = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x000230B4 File Offset: 0x000212B4
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x000230BC File Offset: 0x000212BC
		[XmlIgnore]
		public bool IntendedFreeBusyStatusSpecified
		{
			get
			{
				return this.intendedFreeBusyStatusFieldSpecified;
			}
			set
			{
				this.intendedFreeBusyStatusFieldSpecified = value;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x000230C5 File Offset: 0x000212C5
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x000230CD File Offset: 0x000212CD
		public DateTime Start
		{
			get
			{
				return this.startField;
			}
			set
			{
				this.startField = value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x000230D6 File Offset: 0x000212D6
		// (set) Token: 0x06000F4C RID: 3916 RVA: 0x000230DE File Offset: 0x000212DE
		[XmlIgnore]
		public bool StartSpecified
		{
			get
			{
				return this.startFieldSpecified;
			}
			set
			{
				this.startFieldSpecified = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x000230E7 File Offset: 0x000212E7
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x000230EF File Offset: 0x000212EF
		public DateTime End
		{
			get
			{
				return this.endField;
			}
			set
			{
				this.endField = value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x000230F8 File Offset: 0x000212F8
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x00023100 File Offset: 0x00021300
		[XmlIgnore]
		public bool EndSpecified
		{
			get
			{
				return this.endFieldSpecified;
			}
			set
			{
				this.endFieldSpecified = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00023109 File Offset: 0x00021309
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x00023111 File Offset: 0x00021311
		public DateTime OriginalStart
		{
			get
			{
				return this.originalStartField;
			}
			set
			{
				this.originalStartField = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0002311A File Offset: 0x0002131A
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x00023122 File Offset: 0x00021322
		[XmlIgnore]
		public bool OriginalStartSpecified
		{
			get
			{
				return this.originalStartFieldSpecified;
			}
			set
			{
				this.originalStartFieldSpecified = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0002312B File Offset: 0x0002132B
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x00023133 File Offset: 0x00021333
		public bool IsAllDayEvent
		{
			get
			{
				return this.isAllDayEventField;
			}
			set
			{
				this.isAllDayEventField = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0002313C File Offset: 0x0002133C
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x00023144 File Offset: 0x00021344
		[XmlIgnore]
		public bool IsAllDayEventSpecified
		{
			get
			{
				return this.isAllDayEventFieldSpecified;
			}
			set
			{
				this.isAllDayEventFieldSpecified = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0002314D File Offset: 0x0002134D
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x00023155 File Offset: 0x00021355
		public LegacyFreeBusyType LegacyFreeBusyStatus
		{
			get
			{
				return this.legacyFreeBusyStatusField;
			}
			set
			{
				this.legacyFreeBusyStatusField = value;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0002315E File Offset: 0x0002135E
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x00023166 File Offset: 0x00021366
		[XmlIgnore]
		public bool LegacyFreeBusyStatusSpecified
		{
			get
			{
				return this.legacyFreeBusyStatusFieldSpecified;
			}
			set
			{
				this.legacyFreeBusyStatusFieldSpecified = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0002316F File Offset: 0x0002136F
		// (set) Token: 0x06000F5E RID: 3934 RVA: 0x00023177 File Offset: 0x00021377
		public string Location
		{
			get
			{
				return this.locationField;
			}
			set
			{
				this.locationField = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00023180 File Offset: 0x00021380
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x00023188 File Offset: 0x00021388
		public string When
		{
			get
			{
				return this.whenField;
			}
			set
			{
				this.whenField = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x00023191 File Offset: 0x00021391
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x00023199 File Offset: 0x00021399
		public bool IsMeeting
		{
			get
			{
				return this.isMeetingField;
			}
			set
			{
				this.isMeetingField = value;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x000231A2 File Offset: 0x000213A2
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x000231AA File Offset: 0x000213AA
		[XmlIgnore]
		public bool IsMeetingSpecified
		{
			get
			{
				return this.isMeetingFieldSpecified;
			}
			set
			{
				this.isMeetingFieldSpecified = value;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x000231B3 File Offset: 0x000213B3
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x000231BB File Offset: 0x000213BB
		public bool IsCancelled
		{
			get
			{
				return this.isCancelledField;
			}
			set
			{
				this.isCancelledField = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x000231C4 File Offset: 0x000213C4
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x000231CC File Offset: 0x000213CC
		[XmlIgnore]
		public bool IsCancelledSpecified
		{
			get
			{
				return this.isCancelledFieldSpecified;
			}
			set
			{
				this.isCancelledFieldSpecified = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x000231D5 File Offset: 0x000213D5
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x000231DD File Offset: 0x000213DD
		public bool IsRecurring
		{
			get
			{
				return this.isRecurringField;
			}
			set
			{
				this.isRecurringField = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x000231E6 File Offset: 0x000213E6
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x000231EE File Offset: 0x000213EE
		[XmlIgnore]
		public bool IsRecurringSpecified
		{
			get
			{
				return this.isRecurringFieldSpecified;
			}
			set
			{
				this.isRecurringFieldSpecified = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x000231F7 File Offset: 0x000213F7
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x000231FF File Offset: 0x000213FF
		public bool MeetingRequestWasSent
		{
			get
			{
				return this.meetingRequestWasSentField;
			}
			set
			{
				this.meetingRequestWasSentField = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00023208 File Offset: 0x00021408
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x00023210 File Offset: 0x00021410
		[XmlIgnore]
		public bool MeetingRequestWasSentSpecified
		{
			get
			{
				return this.meetingRequestWasSentFieldSpecified;
			}
			set
			{
				this.meetingRequestWasSentFieldSpecified = value;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x00023219 File Offset: 0x00021419
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x00023221 File Offset: 0x00021421
		public CalendarItemTypeType CalendarItemType
		{
			get
			{
				return this.calendarItemTypeField;
			}
			set
			{
				this.calendarItemTypeField = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0002322A File Offset: 0x0002142A
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x00023232 File Offset: 0x00021432
		[XmlIgnore]
		public bool CalendarItemTypeSpecified
		{
			get
			{
				return this.calendarItemTypeFieldSpecified;
			}
			set
			{
				this.calendarItemTypeFieldSpecified = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0002323B File Offset: 0x0002143B
		// (set) Token: 0x06000F76 RID: 3958 RVA: 0x00023243 File Offset: 0x00021443
		public ResponseTypeType MyResponseType
		{
			get
			{
				return this.myResponseTypeField;
			}
			set
			{
				this.myResponseTypeField = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0002324C File Offset: 0x0002144C
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x00023254 File Offset: 0x00021454
		[XmlIgnore]
		public bool MyResponseTypeSpecified
		{
			get
			{
				return this.myResponseTypeFieldSpecified;
			}
			set
			{
				this.myResponseTypeFieldSpecified = value;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0002325D File Offset: 0x0002145D
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x00023265 File Offset: 0x00021465
		public SingleRecipientType Organizer
		{
			get
			{
				return this.organizerField;
			}
			set
			{
				this.organizerField = value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0002326E File Offset: 0x0002146E
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x00023276 File Offset: 0x00021476
		[XmlArrayItem("Attendee", IsNullable = false)]
		public AttendeeType[] RequiredAttendees
		{
			get
			{
				return this.requiredAttendeesField;
			}
			set
			{
				this.requiredAttendeesField = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0002327F File Offset: 0x0002147F
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x00023287 File Offset: 0x00021487
		[XmlArrayItem("Attendee", IsNullable = false)]
		public AttendeeType[] OptionalAttendees
		{
			get
			{
				return this.optionalAttendeesField;
			}
			set
			{
				this.optionalAttendeesField = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00023290 File Offset: 0x00021490
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x00023298 File Offset: 0x00021498
		[XmlArrayItem("Attendee", IsNullable = false)]
		public AttendeeType[] Resources
		{
			get
			{
				return this.resourcesField;
			}
			set
			{
				this.resourcesField = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x000232A1 File Offset: 0x000214A1
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x000232A9 File Offset: 0x000214A9
		public int ConflictingMeetingCount
		{
			get
			{
				return this.conflictingMeetingCountField;
			}
			set
			{
				this.conflictingMeetingCountField = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x000232B2 File Offset: 0x000214B2
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x000232BA File Offset: 0x000214BA
		[XmlIgnore]
		public bool ConflictingMeetingCountSpecified
		{
			get
			{
				return this.conflictingMeetingCountFieldSpecified;
			}
			set
			{
				this.conflictingMeetingCountFieldSpecified = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x000232C3 File Offset: 0x000214C3
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x000232CB File Offset: 0x000214CB
		public int AdjacentMeetingCount
		{
			get
			{
				return this.adjacentMeetingCountField;
			}
			set
			{
				this.adjacentMeetingCountField = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x000232D4 File Offset: 0x000214D4
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x000232DC File Offset: 0x000214DC
		[XmlIgnore]
		public bool AdjacentMeetingCountSpecified
		{
			get
			{
				return this.adjacentMeetingCountFieldSpecified;
			}
			set
			{
				this.adjacentMeetingCountFieldSpecified = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x000232E5 File Offset: 0x000214E5
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x000232ED File Offset: 0x000214ED
		public NonEmptyArrayOfAllItemsType ConflictingMeetings
		{
			get
			{
				return this.conflictingMeetingsField;
			}
			set
			{
				this.conflictingMeetingsField = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x000232F6 File Offset: 0x000214F6
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x000232FE File Offset: 0x000214FE
		public NonEmptyArrayOfAllItemsType AdjacentMeetings
		{
			get
			{
				return this.adjacentMeetingsField;
			}
			set
			{
				this.adjacentMeetingsField = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00023307 File Offset: 0x00021507
		// (set) Token: 0x06000F8E RID: 3982 RVA: 0x0002330F File Offset: 0x0002150F
		public string Duration
		{
			get
			{
				return this.durationField;
			}
			set
			{
				this.durationField = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00023318 File Offset: 0x00021518
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x00023320 File Offset: 0x00021520
		public string TimeZone
		{
			get
			{
				return this.timeZoneField;
			}
			set
			{
				this.timeZoneField = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00023329 File Offset: 0x00021529
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x00023331 File Offset: 0x00021531
		public DateTime AppointmentReplyTime
		{
			get
			{
				return this.appointmentReplyTimeField;
			}
			set
			{
				this.appointmentReplyTimeField = value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0002333A File Offset: 0x0002153A
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x00023342 File Offset: 0x00021542
		[XmlIgnore]
		public bool AppointmentReplyTimeSpecified
		{
			get
			{
				return this.appointmentReplyTimeFieldSpecified;
			}
			set
			{
				this.appointmentReplyTimeFieldSpecified = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0002334B File Offset: 0x0002154B
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x00023353 File Offset: 0x00021553
		public int AppointmentSequenceNumber
		{
			get
			{
				return this.appointmentSequenceNumberField;
			}
			set
			{
				this.appointmentSequenceNumberField = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0002335C File Offset: 0x0002155C
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x00023364 File Offset: 0x00021564
		[XmlIgnore]
		public bool AppointmentSequenceNumberSpecified
		{
			get
			{
				return this.appointmentSequenceNumberFieldSpecified;
			}
			set
			{
				this.appointmentSequenceNumberFieldSpecified = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0002336D File Offset: 0x0002156D
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x00023375 File Offset: 0x00021575
		public int AppointmentState
		{
			get
			{
				return this.appointmentStateField;
			}
			set
			{
				this.appointmentStateField = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0002337E File Offset: 0x0002157E
		// (set) Token: 0x06000F9C RID: 3996 RVA: 0x00023386 File Offset: 0x00021586
		[XmlIgnore]
		public bool AppointmentStateSpecified
		{
			get
			{
				return this.appointmentStateFieldSpecified;
			}
			set
			{
				this.appointmentStateFieldSpecified = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0002338F File Offset: 0x0002158F
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x00023397 File Offset: 0x00021597
		public RecurrenceType Recurrence
		{
			get
			{
				return this.recurrenceField;
			}
			set
			{
				this.recurrenceField = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x000233A0 File Offset: 0x000215A0
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x000233A8 File Offset: 0x000215A8
		public OccurrenceInfoType FirstOccurrence
		{
			get
			{
				return this.firstOccurrenceField;
			}
			set
			{
				this.firstOccurrenceField = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x000233B1 File Offset: 0x000215B1
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x000233B9 File Offset: 0x000215B9
		public OccurrenceInfoType LastOccurrence
		{
			get
			{
				return this.lastOccurrenceField;
			}
			set
			{
				this.lastOccurrenceField = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x000233C2 File Offset: 0x000215C2
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x000233CA File Offset: 0x000215CA
		[XmlArrayItem("Occurrence", IsNullable = false)]
		public OccurrenceInfoType[] ModifiedOccurrences
		{
			get
			{
				return this.modifiedOccurrencesField;
			}
			set
			{
				this.modifiedOccurrencesField = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x000233D3 File Offset: 0x000215D3
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x000233DB File Offset: 0x000215DB
		[XmlArrayItem("DeletedOccurrence", IsNullable = false)]
		public DeletedOccurrenceInfoType[] DeletedOccurrences
		{
			get
			{
				return this.deletedOccurrencesField;
			}
			set
			{
				this.deletedOccurrencesField = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x000233E4 File Offset: 0x000215E4
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x000233EC File Offset: 0x000215EC
		public TimeZoneType MeetingTimeZone
		{
			get
			{
				return this.meetingTimeZoneField;
			}
			set
			{
				this.meetingTimeZoneField = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x000233F5 File Offset: 0x000215F5
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x000233FD File Offset: 0x000215FD
		public TimeZoneDefinitionType StartTimeZone
		{
			get
			{
				return this.startTimeZoneField;
			}
			set
			{
				this.startTimeZoneField = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00023406 File Offset: 0x00021606
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x0002340E File Offset: 0x0002160E
		public TimeZoneDefinitionType EndTimeZone
		{
			get
			{
				return this.endTimeZoneField;
			}
			set
			{
				this.endTimeZoneField = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00023417 File Offset: 0x00021617
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x0002341F File Offset: 0x0002161F
		public int ConferenceType
		{
			get
			{
				return this.conferenceTypeField;
			}
			set
			{
				this.conferenceTypeField = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00023428 File Offset: 0x00021628
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00023430 File Offset: 0x00021630
		[XmlIgnore]
		public bool ConferenceTypeSpecified
		{
			get
			{
				return this.conferenceTypeFieldSpecified;
			}
			set
			{
				this.conferenceTypeFieldSpecified = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00023439 File Offset: 0x00021639
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00023441 File Offset: 0x00021641
		public bool AllowNewTimeProposal
		{
			get
			{
				return this.allowNewTimeProposalField;
			}
			set
			{
				this.allowNewTimeProposalField = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0002344A File Offset: 0x0002164A
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x00023452 File Offset: 0x00021652
		[XmlIgnore]
		public bool AllowNewTimeProposalSpecified
		{
			get
			{
				return this.allowNewTimeProposalFieldSpecified;
			}
			set
			{
				this.allowNewTimeProposalFieldSpecified = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0002345B File Offset: 0x0002165B
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x00023463 File Offset: 0x00021663
		public bool IsOnlineMeeting
		{
			get
			{
				return this.isOnlineMeetingField;
			}
			set
			{
				this.isOnlineMeetingField = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0002346C File Offset: 0x0002166C
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x00023474 File Offset: 0x00021674
		[XmlIgnore]
		public bool IsOnlineMeetingSpecified
		{
			get
			{
				return this.isOnlineMeetingFieldSpecified;
			}
			set
			{
				this.isOnlineMeetingFieldSpecified = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0002347D File Offset: 0x0002167D
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x00023485 File Offset: 0x00021685
		public string MeetingWorkspaceUrl
		{
			get
			{
				return this.meetingWorkspaceUrlField;
			}
			set
			{
				this.meetingWorkspaceUrlField = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0002348E File Offset: 0x0002168E
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x00023496 File Offset: 0x00021696
		public string NetShowUrl
		{
			get
			{
				return this.netShowUrlField;
			}
			set
			{
				this.netShowUrlField = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0002349F File Offset: 0x0002169F
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x000234A7 File Offset: 0x000216A7
		public EnhancedLocationType EnhancedLocation
		{
			get
			{
				return this.enhancedLocationField;
			}
			set
			{
				this.enhancedLocationField = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x000234B0 File Offset: 0x000216B0
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x000234B8 File Offset: 0x000216B8
		public ChangeHighlightsType ChangeHighlights
		{
			get
			{
				return this.changeHighlightsField;
			}
			set
			{
				this.changeHighlightsField = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000234C1 File Offset: 0x000216C1
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x000234C9 File Offset: 0x000216C9
		public DateTime StartWallClock
		{
			get
			{
				return this.startWallClockField;
			}
			set
			{
				this.startWallClockField = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x000234D2 File Offset: 0x000216D2
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x000234DA File Offset: 0x000216DA
		[XmlIgnore]
		public bool StartWallClockSpecified
		{
			get
			{
				return this.startWallClockFieldSpecified;
			}
			set
			{
				this.startWallClockFieldSpecified = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x000234E3 File Offset: 0x000216E3
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x000234EB File Offset: 0x000216EB
		public DateTime EndWallClock
		{
			get
			{
				return this.endWallClockField;
			}
			set
			{
				this.endWallClockField = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x000234F4 File Offset: 0x000216F4
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x000234FC File Offset: 0x000216FC
		[XmlIgnore]
		public bool EndWallClockSpecified
		{
			get
			{
				return this.endWallClockFieldSpecified;
			}
			set
			{
				this.endWallClockFieldSpecified = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00023505 File Offset: 0x00021705
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x0002350D File Offset: 0x0002170D
		public string StartTimeZoneId
		{
			get
			{
				return this.startTimeZoneIdField;
			}
			set
			{
				this.startTimeZoneIdField = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00023516 File Offset: 0x00021716
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x0002351E File Offset: 0x0002171E
		public string EndTimeZoneId
		{
			get
			{
				return this.endTimeZoneIdField;
			}
			set
			{
				this.endTimeZoneIdField = value;
			}
		}

		// Token: 0x04000A95 RID: 2709
		private MeetingRequestTypeType meetingRequestTypeField;

		// Token: 0x04000A96 RID: 2710
		private bool meetingRequestTypeFieldSpecified;

		// Token: 0x04000A97 RID: 2711
		private LegacyFreeBusyType intendedFreeBusyStatusField;

		// Token: 0x04000A98 RID: 2712
		private bool intendedFreeBusyStatusFieldSpecified;

		// Token: 0x04000A99 RID: 2713
		private DateTime startField;

		// Token: 0x04000A9A RID: 2714
		private bool startFieldSpecified;

		// Token: 0x04000A9B RID: 2715
		private DateTime endField;

		// Token: 0x04000A9C RID: 2716
		private bool endFieldSpecified;

		// Token: 0x04000A9D RID: 2717
		private DateTime originalStartField;

		// Token: 0x04000A9E RID: 2718
		private bool originalStartFieldSpecified;

		// Token: 0x04000A9F RID: 2719
		private bool isAllDayEventField;

		// Token: 0x04000AA0 RID: 2720
		private bool isAllDayEventFieldSpecified;

		// Token: 0x04000AA1 RID: 2721
		private LegacyFreeBusyType legacyFreeBusyStatusField;

		// Token: 0x04000AA2 RID: 2722
		private bool legacyFreeBusyStatusFieldSpecified;

		// Token: 0x04000AA3 RID: 2723
		private string locationField;

		// Token: 0x04000AA4 RID: 2724
		private string whenField;

		// Token: 0x04000AA5 RID: 2725
		private bool isMeetingField;

		// Token: 0x04000AA6 RID: 2726
		private bool isMeetingFieldSpecified;

		// Token: 0x04000AA7 RID: 2727
		private bool isCancelledField;

		// Token: 0x04000AA8 RID: 2728
		private bool isCancelledFieldSpecified;

		// Token: 0x04000AA9 RID: 2729
		private bool isRecurringField;

		// Token: 0x04000AAA RID: 2730
		private bool isRecurringFieldSpecified;

		// Token: 0x04000AAB RID: 2731
		private bool meetingRequestWasSentField;

		// Token: 0x04000AAC RID: 2732
		private bool meetingRequestWasSentFieldSpecified;

		// Token: 0x04000AAD RID: 2733
		private CalendarItemTypeType calendarItemTypeField;

		// Token: 0x04000AAE RID: 2734
		private bool calendarItemTypeFieldSpecified;

		// Token: 0x04000AAF RID: 2735
		private ResponseTypeType myResponseTypeField;

		// Token: 0x04000AB0 RID: 2736
		private bool myResponseTypeFieldSpecified;

		// Token: 0x04000AB1 RID: 2737
		private SingleRecipientType organizerField;

		// Token: 0x04000AB2 RID: 2738
		private AttendeeType[] requiredAttendeesField;

		// Token: 0x04000AB3 RID: 2739
		private AttendeeType[] optionalAttendeesField;

		// Token: 0x04000AB4 RID: 2740
		private AttendeeType[] resourcesField;

		// Token: 0x04000AB5 RID: 2741
		private int conflictingMeetingCountField;

		// Token: 0x04000AB6 RID: 2742
		private bool conflictingMeetingCountFieldSpecified;

		// Token: 0x04000AB7 RID: 2743
		private int adjacentMeetingCountField;

		// Token: 0x04000AB8 RID: 2744
		private bool adjacentMeetingCountFieldSpecified;

		// Token: 0x04000AB9 RID: 2745
		private NonEmptyArrayOfAllItemsType conflictingMeetingsField;

		// Token: 0x04000ABA RID: 2746
		private NonEmptyArrayOfAllItemsType adjacentMeetingsField;

		// Token: 0x04000ABB RID: 2747
		private string durationField;

		// Token: 0x04000ABC RID: 2748
		private string timeZoneField;

		// Token: 0x04000ABD RID: 2749
		private DateTime appointmentReplyTimeField;

		// Token: 0x04000ABE RID: 2750
		private bool appointmentReplyTimeFieldSpecified;

		// Token: 0x04000ABF RID: 2751
		private int appointmentSequenceNumberField;

		// Token: 0x04000AC0 RID: 2752
		private bool appointmentSequenceNumberFieldSpecified;

		// Token: 0x04000AC1 RID: 2753
		private int appointmentStateField;

		// Token: 0x04000AC2 RID: 2754
		private bool appointmentStateFieldSpecified;

		// Token: 0x04000AC3 RID: 2755
		private RecurrenceType recurrenceField;

		// Token: 0x04000AC4 RID: 2756
		private OccurrenceInfoType firstOccurrenceField;

		// Token: 0x04000AC5 RID: 2757
		private OccurrenceInfoType lastOccurrenceField;

		// Token: 0x04000AC6 RID: 2758
		private OccurrenceInfoType[] modifiedOccurrencesField;

		// Token: 0x04000AC7 RID: 2759
		private DeletedOccurrenceInfoType[] deletedOccurrencesField;

		// Token: 0x04000AC8 RID: 2760
		private TimeZoneType meetingTimeZoneField;

		// Token: 0x04000AC9 RID: 2761
		private TimeZoneDefinitionType startTimeZoneField;

		// Token: 0x04000ACA RID: 2762
		private TimeZoneDefinitionType endTimeZoneField;

		// Token: 0x04000ACB RID: 2763
		private int conferenceTypeField;

		// Token: 0x04000ACC RID: 2764
		private bool conferenceTypeFieldSpecified;

		// Token: 0x04000ACD RID: 2765
		private bool allowNewTimeProposalField;

		// Token: 0x04000ACE RID: 2766
		private bool allowNewTimeProposalFieldSpecified;

		// Token: 0x04000ACF RID: 2767
		private bool isOnlineMeetingField;

		// Token: 0x04000AD0 RID: 2768
		private bool isOnlineMeetingFieldSpecified;

		// Token: 0x04000AD1 RID: 2769
		private string meetingWorkspaceUrlField;

		// Token: 0x04000AD2 RID: 2770
		private string netShowUrlField;

		// Token: 0x04000AD3 RID: 2771
		private EnhancedLocationType enhancedLocationField;

		// Token: 0x04000AD4 RID: 2772
		private ChangeHighlightsType changeHighlightsField;

		// Token: 0x04000AD5 RID: 2773
		private DateTime startWallClockField;

		// Token: 0x04000AD6 RID: 2774
		private bool startWallClockFieldSpecified;

		// Token: 0x04000AD7 RID: 2775
		private DateTime endWallClockField;

		// Token: 0x04000AD8 RID: 2776
		private bool endWallClockFieldSpecified;

		// Token: 0x04000AD9 RID: 2777
		private string startTimeZoneIdField;

		// Token: 0x04000ADA RID: 2778
		private string endTimeZoneIdField;
	}
}
