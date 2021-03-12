using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200012A RID: 298
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class CalendarItemType : ItemType
	{
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00022001 File Offset: 0x00020201
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x00022009 File Offset: 0x00020209
		public string UID
		{
			get
			{
				return this.uIDField;
			}
			set
			{
				this.uIDField = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00022012 File Offset: 0x00020212
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x0002201A File Offset: 0x0002021A
		public DateTime RecurrenceId
		{
			get
			{
				return this.recurrenceIdField;
			}
			set
			{
				this.recurrenceIdField = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00022023 File Offset: 0x00020223
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x0002202B File Offset: 0x0002022B
		[XmlIgnore]
		public bool RecurrenceIdSpecified
		{
			get
			{
				return this.recurrenceIdFieldSpecified;
			}
			set
			{
				this.recurrenceIdFieldSpecified = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00022034 File Offset: 0x00020234
		// (set) Token: 0x06000D55 RID: 3413 RVA: 0x0002203C File Offset: 0x0002023C
		public DateTime DateTimeStamp
		{
			get
			{
				return this.dateTimeStampField;
			}
			set
			{
				this.dateTimeStampField = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00022045 File Offset: 0x00020245
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x0002204D File Offset: 0x0002024D
		[XmlIgnore]
		public bool DateTimeStampSpecified
		{
			get
			{
				return this.dateTimeStampFieldSpecified;
			}
			set
			{
				this.dateTimeStampFieldSpecified = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00022056 File Offset: 0x00020256
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0002205E File Offset: 0x0002025E
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

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00022067 File Offset: 0x00020267
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x0002206F File Offset: 0x0002026F
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

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00022078 File Offset: 0x00020278
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x00022080 File Offset: 0x00020280
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

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x00022089 File Offset: 0x00020289
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x00022091 File Offset: 0x00020291
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

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0002209A File Offset: 0x0002029A
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x000220A2 File Offset: 0x000202A2
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

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x000220AB File Offset: 0x000202AB
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x000220B3 File Offset: 0x000202B3
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

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x000220BC File Offset: 0x000202BC
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x000220C4 File Offset: 0x000202C4
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

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000220CD File Offset: 0x000202CD
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x000220D5 File Offset: 0x000202D5
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000220DE File Offset: 0x000202DE
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x000220E6 File Offset: 0x000202E6
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

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x000220EF File Offset: 0x000202EF
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x000220F7 File Offset: 0x000202F7
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

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00022100 File Offset: 0x00020300
		// (set) Token: 0x06000D6D RID: 3437 RVA: 0x00022108 File Offset: 0x00020308
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

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00022111 File Offset: 0x00020311
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x00022119 File Offset: 0x00020319
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

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x00022122 File Offset: 0x00020322
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x0002212A File Offset: 0x0002032A
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

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00022133 File Offset: 0x00020333
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x0002213B File Offset: 0x0002033B
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

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00022144 File Offset: 0x00020344
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x0002214C File Offset: 0x0002034C
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

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00022155 File Offset: 0x00020355
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x0002215D File Offset: 0x0002035D
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

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00022166 File Offset: 0x00020366
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x0002216E File Offset: 0x0002036E
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

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00022177 File Offset: 0x00020377
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x0002217F File Offset: 0x0002037F
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

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00022188 File Offset: 0x00020388
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x00022190 File Offset: 0x00020390
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

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00022199 File Offset: 0x00020399
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x000221A1 File Offset: 0x000203A1
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

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x000221AA File Offset: 0x000203AA
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x000221B2 File Offset: 0x000203B2
		public bool IsResponseRequested
		{
			get
			{
				return this.isResponseRequestedField;
			}
			set
			{
				this.isResponseRequestedField = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x000221BB File Offset: 0x000203BB
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x000221C3 File Offset: 0x000203C3
		[XmlIgnore]
		public bool IsResponseRequestedSpecified
		{
			get
			{
				return this.isResponseRequestedFieldSpecified;
			}
			set
			{
				this.isResponseRequestedFieldSpecified = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x000221CC File Offset: 0x000203CC
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x000221D4 File Offset: 0x000203D4
		[XmlElement("CalendarItemType")]
		public CalendarItemTypeType CalendarItemType1
		{
			get
			{
				return this.calendarItemType1Field;
			}
			set
			{
				this.calendarItemType1Field = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x000221DD File Offset: 0x000203DD
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x000221E5 File Offset: 0x000203E5
		[XmlIgnore]
		public bool CalendarItemType1Specified
		{
			get
			{
				return this.calendarItemType1FieldSpecified;
			}
			set
			{
				this.calendarItemType1FieldSpecified = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x000221EE File Offset: 0x000203EE
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x000221F6 File Offset: 0x000203F6
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

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x000221FF File Offset: 0x000203FF
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x00022207 File Offset: 0x00020407
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

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00022210 File Offset: 0x00020410
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x00022218 File Offset: 0x00020418
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

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00022221 File Offset: 0x00020421
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x00022229 File Offset: 0x00020429
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

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00022232 File Offset: 0x00020432
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x0002223A File Offset: 0x0002043A
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

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00022243 File Offset: 0x00020443
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x0002224B File Offset: 0x0002044B
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

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00022254 File Offset: 0x00020454
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x0002225C File Offset: 0x0002045C
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

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00022265 File Offset: 0x00020465
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x0002226D File Offset: 0x0002046D
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

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00022276 File Offset: 0x00020476
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0002227E File Offset: 0x0002047E
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

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00022287 File Offset: 0x00020487
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x0002228F File Offset: 0x0002048F
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

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00022298 File Offset: 0x00020498
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x000222A0 File Offset: 0x000204A0
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

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x000222A9 File Offset: 0x000204A9
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x000222B1 File Offset: 0x000204B1
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

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x000222BA File Offset: 0x000204BA
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x000222C2 File Offset: 0x000204C2
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

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x000222CB File Offset: 0x000204CB
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x000222D3 File Offset: 0x000204D3
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

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x000222DC File Offset: 0x000204DC
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x000222E4 File Offset: 0x000204E4
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

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x000222ED File Offset: 0x000204ED
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x000222F5 File Offset: 0x000204F5
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

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x000222FE File Offset: 0x000204FE
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x00022306 File Offset: 0x00020506
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

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0002230F File Offset: 0x0002050F
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x00022317 File Offset: 0x00020517
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

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00022320 File Offset: 0x00020520
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x00022328 File Offset: 0x00020528
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

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00022331 File Offset: 0x00020531
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x00022339 File Offset: 0x00020539
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

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00022342 File Offset: 0x00020542
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x0002234A File Offset: 0x0002054A
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

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00022353 File Offset: 0x00020553
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x0002235B File Offset: 0x0002055B
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

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00022364 File Offset: 0x00020564
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x0002236C File Offset: 0x0002056C
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

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00022375 File Offset: 0x00020575
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x0002237D File Offset: 0x0002057D
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00022386 File Offset: 0x00020586
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x0002238E File Offset: 0x0002058E
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

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00022397 File Offset: 0x00020597
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0002239F File Offset: 0x0002059F
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x000223A8 File Offset: 0x000205A8
		// (set) Token: 0x06000DBD RID: 3517 RVA: 0x000223B0 File Offset: 0x000205B0
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x000223B9 File Offset: 0x000205B9
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x000223C1 File Offset: 0x000205C1
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

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x000223CA File Offset: 0x000205CA
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x000223D2 File Offset: 0x000205D2
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

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x000223DB File Offset: 0x000205DB
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x000223E3 File Offset: 0x000205E3
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

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x000223EC File Offset: 0x000205EC
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x000223F4 File Offset: 0x000205F4
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

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x000223FD File Offset: 0x000205FD
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x00022405 File Offset: 0x00020605
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

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0002240E File Offset: 0x0002060E
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x00022416 File Offset: 0x00020616
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

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0002241F File Offset: 0x0002061F
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x00022427 File Offset: 0x00020627
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

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00022430 File Offset: 0x00020630
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x00022438 File Offset: 0x00020638
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

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00022441 File Offset: 0x00020641
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x00022449 File Offset: 0x00020649
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

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00022452 File Offset: 0x00020652
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0002245A File Offset: 0x0002065A
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

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00022463 File Offset: 0x00020663
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0002246B File Offset: 0x0002066B
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

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00022474 File Offset: 0x00020674
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0002247C File Offset: 0x0002067C
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

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00022485 File Offset: 0x00020685
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0002248D File Offset: 0x0002068D
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

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00022496 File Offset: 0x00020696
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0002249E File Offset: 0x0002069E
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

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x000224A7 File Offset: 0x000206A7
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x000224AF File Offset: 0x000206AF
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

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x000224B8 File Offset: 0x000206B8
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x000224C0 File Offset: 0x000206C0
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

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x000224C9 File Offset: 0x000206C9
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x000224D1 File Offset: 0x000206D1
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

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x000224DA File Offset: 0x000206DA
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x000224E2 File Offset: 0x000206E2
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

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x000224EB File Offset: 0x000206EB
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x000224F3 File Offset: 0x000206F3
		public string JoinOnlineMeetingUrl
		{
			get
			{
				return this.joinOnlineMeetingUrlField;
			}
			set
			{
				this.joinOnlineMeetingUrlField = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000224FC File Offset: 0x000206FC
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x00022504 File Offset: 0x00020704
		public OnlineMeetingSettingsType OnlineMeetingSettings
		{
			get
			{
				return this.onlineMeetingSettingsField;
			}
			set
			{
				this.onlineMeetingSettingsField = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0002250D File Offset: 0x0002070D
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x00022515 File Offset: 0x00020715
		public bool IsOrganizer
		{
			get
			{
				return this.isOrganizerField;
			}
			set
			{
				this.isOrganizerField = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0002251E File Offset: 0x0002071E
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x00022526 File Offset: 0x00020726
		[XmlIgnore]
		public bool IsOrganizerSpecified
		{
			get
			{
				return this.isOrganizerFieldSpecified;
			}
			set
			{
				this.isOrganizerFieldSpecified = value;
			}
		}

		// Token: 0x04000935 RID: 2357
		private string uIDField;

		// Token: 0x04000936 RID: 2358
		private DateTime recurrenceIdField;

		// Token: 0x04000937 RID: 2359
		private bool recurrenceIdFieldSpecified;

		// Token: 0x04000938 RID: 2360
		private DateTime dateTimeStampField;

		// Token: 0x04000939 RID: 2361
		private bool dateTimeStampFieldSpecified;

		// Token: 0x0400093A RID: 2362
		private DateTime startField;

		// Token: 0x0400093B RID: 2363
		private bool startFieldSpecified;

		// Token: 0x0400093C RID: 2364
		private DateTime endField;

		// Token: 0x0400093D RID: 2365
		private bool endFieldSpecified;

		// Token: 0x0400093E RID: 2366
		private DateTime originalStartField;

		// Token: 0x0400093F RID: 2367
		private bool originalStartFieldSpecified;

		// Token: 0x04000940 RID: 2368
		private bool isAllDayEventField;

		// Token: 0x04000941 RID: 2369
		private bool isAllDayEventFieldSpecified;

		// Token: 0x04000942 RID: 2370
		private LegacyFreeBusyType legacyFreeBusyStatusField;

		// Token: 0x04000943 RID: 2371
		private bool legacyFreeBusyStatusFieldSpecified;

		// Token: 0x04000944 RID: 2372
		private string locationField;

		// Token: 0x04000945 RID: 2373
		private string whenField;

		// Token: 0x04000946 RID: 2374
		private bool isMeetingField;

		// Token: 0x04000947 RID: 2375
		private bool isMeetingFieldSpecified;

		// Token: 0x04000948 RID: 2376
		private bool isCancelledField;

		// Token: 0x04000949 RID: 2377
		private bool isCancelledFieldSpecified;

		// Token: 0x0400094A RID: 2378
		private bool isRecurringField;

		// Token: 0x0400094B RID: 2379
		private bool isRecurringFieldSpecified;

		// Token: 0x0400094C RID: 2380
		private bool meetingRequestWasSentField;

		// Token: 0x0400094D RID: 2381
		private bool meetingRequestWasSentFieldSpecified;

		// Token: 0x0400094E RID: 2382
		private bool isResponseRequestedField;

		// Token: 0x0400094F RID: 2383
		private bool isResponseRequestedFieldSpecified;

		// Token: 0x04000950 RID: 2384
		private CalendarItemTypeType calendarItemType1Field;

		// Token: 0x04000951 RID: 2385
		private bool calendarItemType1FieldSpecified;

		// Token: 0x04000952 RID: 2386
		private ResponseTypeType myResponseTypeField;

		// Token: 0x04000953 RID: 2387
		private bool myResponseTypeFieldSpecified;

		// Token: 0x04000954 RID: 2388
		private SingleRecipientType organizerField;

		// Token: 0x04000955 RID: 2389
		private AttendeeType[] requiredAttendeesField;

		// Token: 0x04000956 RID: 2390
		private AttendeeType[] optionalAttendeesField;

		// Token: 0x04000957 RID: 2391
		private AttendeeType[] resourcesField;

		// Token: 0x04000958 RID: 2392
		private int conflictingMeetingCountField;

		// Token: 0x04000959 RID: 2393
		private bool conflictingMeetingCountFieldSpecified;

		// Token: 0x0400095A RID: 2394
		private int adjacentMeetingCountField;

		// Token: 0x0400095B RID: 2395
		private bool adjacentMeetingCountFieldSpecified;

		// Token: 0x0400095C RID: 2396
		private NonEmptyArrayOfAllItemsType conflictingMeetingsField;

		// Token: 0x0400095D RID: 2397
		private NonEmptyArrayOfAllItemsType adjacentMeetingsField;

		// Token: 0x0400095E RID: 2398
		private string durationField;

		// Token: 0x0400095F RID: 2399
		private string timeZoneField;

		// Token: 0x04000960 RID: 2400
		private DateTime appointmentReplyTimeField;

		// Token: 0x04000961 RID: 2401
		private bool appointmentReplyTimeFieldSpecified;

		// Token: 0x04000962 RID: 2402
		private int appointmentSequenceNumberField;

		// Token: 0x04000963 RID: 2403
		private bool appointmentSequenceNumberFieldSpecified;

		// Token: 0x04000964 RID: 2404
		private int appointmentStateField;

		// Token: 0x04000965 RID: 2405
		private bool appointmentStateFieldSpecified;

		// Token: 0x04000966 RID: 2406
		private RecurrenceType recurrenceField;

		// Token: 0x04000967 RID: 2407
		private OccurrenceInfoType firstOccurrenceField;

		// Token: 0x04000968 RID: 2408
		private OccurrenceInfoType lastOccurrenceField;

		// Token: 0x04000969 RID: 2409
		private OccurrenceInfoType[] modifiedOccurrencesField;

		// Token: 0x0400096A RID: 2410
		private DeletedOccurrenceInfoType[] deletedOccurrencesField;

		// Token: 0x0400096B RID: 2411
		private TimeZoneType meetingTimeZoneField;

		// Token: 0x0400096C RID: 2412
		private TimeZoneDefinitionType startTimeZoneField;

		// Token: 0x0400096D RID: 2413
		private TimeZoneDefinitionType endTimeZoneField;

		// Token: 0x0400096E RID: 2414
		private int conferenceTypeField;

		// Token: 0x0400096F RID: 2415
		private bool conferenceTypeFieldSpecified;

		// Token: 0x04000970 RID: 2416
		private bool allowNewTimeProposalField;

		// Token: 0x04000971 RID: 2417
		private bool allowNewTimeProposalFieldSpecified;

		// Token: 0x04000972 RID: 2418
		private bool isOnlineMeetingField;

		// Token: 0x04000973 RID: 2419
		private bool isOnlineMeetingFieldSpecified;

		// Token: 0x04000974 RID: 2420
		private string meetingWorkspaceUrlField;

		// Token: 0x04000975 RID: 2421
		private string netShowUrlField;

		// Token: 0x04000976 RID: 2422
		private EnhancedLocationType enhancedLocationField;

		// Token: 0x04000977 RID: 2423
		private DateTime startWallClockField;

		// Token: 0x04000978 RID: 2424
		private bool startWallClockFieldSpecified;

		// Token: 0x04000979 RID: 2425
		private DateTime endWallClockField;

		// Token: 0x0400097A RID: 2426
		private bool endWallClockFieldSpecified;

		// Token: 0x0400097B RID: 2427
		private string startTimeZoneIdField;

		// Token: 0x0400097C RID: 2428
		private string endTimeZoneIdField;

		// Token: 0x0400097D RID: 2429
		private LegacyFreeBusyType intendedFreeBusyStatusField;

		// Token: 0x0400097E RID: 2430
		private bool intendedFreeBusyStatusFieldSpecified;

		// Token: 0x0400097F RID: 2431
		private string joinOnlineMeetingUrlField;

		// Token: 0x04000980 RID: 2432
		private OnlineMeetingSettingsType onlineMeetingSettingsField;

		// Token: 0x04000981 RID: 2433
		private bool isOrganizerField;

		// Token: 0x04000982 RID: 2434
		private bool isOrganizerFieldSpecified;
	}
}
