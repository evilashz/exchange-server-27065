using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000050 RID: 80
	public sealed class EventSchema : ItemSchema
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x00005D90 File Offset: 0x00003F90
		public EventSchema()
		{
			base.RegisterPropertyDefinition(EventSchema.StaticAttendeesProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticCalendarProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticDisallowNewTimeProposalProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticEndProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticHasAttendeesProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticIntendedEndTimeZoneIdProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticIntendedStartTimeZoneIdProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticIntendedStatusProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticIsAllDayProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticIsCancelledProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticIsDraftProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticIsOnlineMeetingProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticIsOrganizerProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticLastExecutedInteropActionProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticLocationProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticOccurrencesProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticOnlineMeetingConfLinkProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticOnlineMeetingExternalLinkProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticOrganizerProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticPatternedRecurrenceProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticResponseRequestedProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticResponseStatusProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticSeriesIdProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticSeriesMasterProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticSeriesMasterIdProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticShowAsProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticStartProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticTypeProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticPopupReminderSettingsProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticInternalGlobalObjectIdProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticInternalIsReceived);
			base.RegisterPropertyDefinition(EventSchema.StaticInternalMarkAllPropagatedPropertiesAsExceptionProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticInternalSeriesToInstancePropagationProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticClientIdProperty);
			base.RegisterPropertyDefinition(EventSchema.StaticInternalInstanceCreationIndexProperty);
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00005F24 File Offset: 0x00004124
		public TypedPropertyDefinition<IList<Attendee>> AttendeesProperty
		{
			get
			{
				return EventSchema.StaticAttendeesProperty;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00005F2B File Offset: 0x0000412B
		public TypedPropertyDefinition<Calendar> CalendarProperty
		{
			get
			{
				return EventSchema.StaticCalendarProperty;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00005F32 File Offset: 0x00004132
		public TypedPropertyDefinition<bool> DisallowNewTimeProposalProperty
		{
			get
			{
				return EventSchema.StaticDisallowNewTimeProposalProperty;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00005F39 File Offset: 0x00004139
		public TypedPropertyDefinition<ExDateTime> EndProperty
		{
			get
			{
				return EventSchema.StaticEndProperty;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00005F40 File Offset: 0x00004140
		public TypedPropertyDefinition<IList<string>> ExceptionalPropertiesProperty
		{
			get
			{
				return EventSchema.StaticExceptionalPropertiesProperty;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00005F47 File Offset: 0x00004147
		public TypedPropertyDefinition<bool> HasAttendeesProperty
		{
			get
			{
				return EventSchema.StaticHasAttendeesProperty;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00005F4E File Offset: 0x0000414E
		public TypedPropertyDefinition<string> IntendedEndTimeZoneIdProperty
		{
			get
			{
				return EventSchema.StaticIntendedEndTimeZoneIdProperty;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00005F55 File Offset: 0x00004155
		public TypedPropertyDefinition<string> IntendedStartTimeZoneIdProperty
		{
			get
			{
				return EventSchema.StaticIntendedStartTimeZoneIdProperty;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00005F5C File Offset: 0x0000415C
		public TypedPropertyDefinition<FreeBusyStatus> IntendedStatusProperty
		{
			get
			{
				return EventSchema.StaticIntendedStatusProperty;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00005F63 File Offset: 0x00004163
		public TypedPropertyDefinition<bool> IsAllDayProperty
		{
			get
			{
				return EventSchema.StaticIsAllDayProperty;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00005F6A File Offset: 0x0000416A
		public TypedPropertyDefinition<bool> IsCancelledProperty
		{
			get
			{
				return EventSchema.StaticIsCancelledProperty;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00005F71 File Offset: 0x00004171
		public TypedPropertyDefinition<bool> IsDraftProperty
		{
			get
			{
				return EventSchema.StaticIsDraftProperty;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00005F78 File Offset: 0x00004178
		public TypedPropertyDefinition<bool> IsOnlineMeetingProperty
		{
			get
			{
				return EventSchema.StaticIsOnlineMeetingProperty;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00005F7F File Offset: 0x0000417F
		public TypedPropertyDefinition<bool> IsOrganizerProperty
		{
			get
			{
				return EventSchema.StaticIsOrganizerProperty;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00005F86 File Offset: 0x00004186
		public TypedPropertyDefinition<Location> LocationProperty
		{
			get
			{
				return EventSchema.StaticLocationProperty;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00005F8D File Offset: 0x0000418D
		public TypedPropertyDefinition<IList<Event>> OccurrencesProperty
		{
			get
			{
				return EventSchema.StaticOccurrencesProperty;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00005F94 File Offset: 0x00004194
		public TypedPropertyDefinition<string> OnlineMeetingConfLinkProperty
		{
			get
			{
				return EventSchema.StaticOnlineMeetingConfLinkProperty;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00005F9B File Offset: 0x0000419B
		public TypedPropertyDefinition<string> OnlineMeetingExternalLinkProperty
		{
			get
			{
				return EventSchema.StaticOnlineMeetingExternalLinkProperty;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00005FA2 File Offset: 0x000041A2
		public TypedPropertyDefinition<Organizer> OrganizerProperty
		{
			get
			{
				return EventSchema.StaticOrganizerProperty;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00005FA9 File Offset: 0x000041A9
		public TypedPropertyDefinition<PatternedRecurrence> PatternedRecurrenceProperty
		{
			get
			{
				return EventSchema.StaticPatternedRecurrenceProperty;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00005FB0 File Offset: 0x000041B0
		public TypedPropertyDefinition<bool> ResponseRequestedProperty
		{
			get
			{
				return EventSchema.StaticResponseRequestedProperty;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00005FB7 File Offset: 0x000041B7
		public TypedPropertyDefinition<ResponseStatus> ResponseStatusProperty
		{
			get
			{
				return EventSchema.StaticResponseStatusProperty;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00005FBE File Offset: 0x000041BE
		public TypedPropertyDefinition<string> SeriesIdProperty
		{
			get
			{
				return EventSchema.StaticSeriesIdProperty;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00005FC5 File Offset: 0x000041C5
		public TypedPropertyDefinition<Event> SeriesMasterProperty
		{
			get
			{
				return EventSchema.StaticSeriesMasterProperty;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00005FCC File Offset: 0x000041CC
		public TypedPropertyDefinition<string> SeriesMasterIdProperty
		{
			get
			{
				return EventSchema.StaticSeriesMasterIdProperty;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00005FD3 File Offset: 0x000041D3
		public TypedPropertyDefinition<FreeBusyStatus> ShowAsProperty
		{
			get
			{
				return EventSchema.StaticShowAsProperty;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00005FDA File Offset: 0x000041DA
		public TypedPropertyDefinition<ExDateTime> StartProperty
		{
			get
			{
				return EventSchema.StaticStartProperty;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00005FE1 File Offset: 0x000041E1
		public TypedPropertyDefinition<EventType> TypeProperty
		{
			get
			{
				return EventSchema.StaticTypeProperty;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00005FE8 File Offset: 0x000041E8
		public TypedPropertyDefinition<IList<EventPopupReminderSetting>> PopupReminderSettingsProperty
		{
			get
			{
				return EventSchema.StaticPopupReminderSettingsProperty;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00005FEF File Offset: 0x000041EF
		internal TypedPropertyDefinition<Guid?> LastExecutedInteropActionProperty
		{
			get
			{
				return EventSchema.StaticLastExecutedInteropActionProperty;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00005FF6 File Offset: 0x000041F6
		internal TypedPropertyDefinition<string> InternalGlobalObjectIdProperty
		{
			get
			{
				return EventSchema.StaticInternalGlobalObjectIdProperty;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00005FFD File Offset: 0x000041FD
		internal TypedPropertyDefinition<bool> InternalMarkAllPropagatedPropertiesAsExceptionProperty
		{
			get
			{
				return EventSchema.StaticInternalMarkAllPropagatedPropertiesAsExceptionProperty;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00006004 File Offset: 0x00004204
		internal TypedPropertyDefinition<bool> InternalSeriesToInstancePropagationProperty
		{
			get
			{
				return EventSchema.StaticInternalSeriesToInstancePropagationProperty;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000600B File Offset: 0x0000420B
		internal TypedPropertyDefinition<bool> InternalIsReceived
		{
			get
			{
				return EventSchema.StaticInternalIsReceived;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00006012 File Offset: 0x00004212
		internal TypedPropertyDefinition<string> ClientIdProperty
		{
			get
			{
				return EventSchema.StaticClientIdProperty;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00006019 File Offset: 0x00004219
		internal TypedPropertyDefinition<int> InternalSeriesCreationHashProperty
		{
			get
			{
				return EventSchema.StaticInternalSeriesCreationHashProperty;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00006020 File Offset: 0x00004220
		internal TypedPropertyDefinition<int> InternalSeriesSequenceNumberProperty
		{
			get
			{
				return EventSchema.StaticInternalSeriesSequenceNumberProperty;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00006027 File Offset: 0x00004227
		internal TypedPropertyDefinition<int> InternalInstanceCreationIndexProperty
		{
			get
			{
				return EventSchema.StaticInternalInstanceCreationIndexProperty;
			}
		}

		// Token: 0x0400012E RID: 302
		private static readonly TypedPropertyDefinition<IList<Attendee>> StaticAttendeesProperty = new TypedPropertyDefinition<IList<Attendee>>("Event.Attendees", null, true);

		// Token: 0x0400012F RID: 303
		private static readonly TypedPropertyDefinition<Calendar> StaticCalendarProperty = new TypedPropertyDefinition<Calendar>("Event.Calendar", null, true);

		// Token: 0x04000130 RID: 304
		private static readonly TypedPropertyDefinition<string> StaticClientIdProperty = new TypedPropertyDefinition<string>("Event.ClientId", null, true);

		// Token: 0x04000131 RID: 305
		private static readonly TypedPropertyDefinition<bool> StaticDisallowNewTimeProposalProperty = new TypedPropertyDefinition<bool>("Event.DisallowNewTimeProposal", false, true);

		// Token: 0x04000132 RID: 306
		private static readonly TypedPropertyDefinition<ExDateTime> StaticEndProperty = new TypedPropertyDefinition<ExDateTime>("Event.End", default(ExDateTime), true);

		// Token: 0x04000133 RID: 307
		private static readonly TypedPropertyDefinition<IList<string>> StaticExceptionalPropertiesProperty = new TypedPropertyDefinition<IList<string>>("Event.ExceptionalProperties", null, true);

		// Token: 0x04000134 RID: 308
		private static readonly TypedPropertyDefinition<bool> StaticHasAttendeesProperty = new TypedPropertyDefinition<bool>("Event.HasAttendee", false, true);

		// Token: 0x04000135 RID: 309
		private static readonly TypedPropertyDefinition<string> StaticIntendedEndTimeZoneIdProperty = new TypedPropertyDefinition<string>("Event.IntendedEndTimeZoneId", null, true);

		// Token: 0x04000136 RID: 310
		private static readonly TypedPropertyDefinition<string> StaticIntendedStartTimeZoneIdProperty = new TypedPropertyDefinition<string>("Event.IntendedStartTimeZoneId", null, true);

		// Token: 0x04000137 RID: 311
		private static readonly TypedPropertyDefinition<FreeBusyStatus> StaticIntendedStatusProperty = new TypedPropertyDefinition<FreeBusyStatus>("Event.IntendedStatus", FreeBusyStatus.Free, true);

		// Token: 0x04000138 RID: 312
		private static readonly TypedPropertyDefinition<bool> StaticIsAllDayProperty = new TypedPropertyDefinition<bool>("Event.IsAllDay", false, true);

		// Token: 0x04000139 RID: 313
		private static readonly TypedPropertyDefinition<bool> StaticIsCancelledProperty = new TypedPropertyDefinition<bool>("Event.IsCancelled", false, true);

		// Token: 0x0400013A RID: 314
		private static readonly TypedPropertyDefinition<bool> StaticIsDraftProperty = new TypedPropertyDefinition<bool>("Event.IsDraft", false, true);

		// Token: 0x0400013B RID: 315
		private static readonly TypedPropertyDefinition<bool> StaticIsOnlineMeetingProperty = new TypedPropertyDefinition<bool>("Event.IsOnlineMeeting", false, true);

		// Token: 0x0400013C RID: 316
		private static readonly TypedPropertyDefinition<bool> StaticIsOrganizerProperty = new TypedPropertyDefinition<bool>("Event.IsOrganizer", false, true);

		// Token: 0x0400013D RID: 317
		private static readonly TypedPropertyDefinition<Guid?> StaticLastExecutedInteropActionProperty = new TypedPropertyDefinition<Guid?>("Event.LastExecutedInteropAction", null, true);

		// Token: 0x0400013E RID: 318
		private static readonly TypedPropertyDefinition<Location> StaticLocationProperty = new TypedPropertyDefinition<Location>("Event.Location", null, true);

		// Token: 0x0400013F RID: 319
		private static readonly TypedPropertyDefinition<IList<Event>> StaticOccurrencesProperty = new TypedPropertyDefinition<IList<Event>>("Event.Occurrences", null, true);

		// Token: 0x04000140 RID: 320
		private static readonly TypedPropertyDefinition<Organizer> StaticOrganizerProperty = new TypedPropertyDefinition<Organizer>("Event.Organizer", null, true);

		// Token: 0x04000141 RID: 321
		private static readonly TypedPropertyDefinition<string> StaticOnlineMeetingConfLinkProperty = new TypedPropertyDefinition<string>("Event.OnlineMeetingConfLink", null, true);

		// Token: 0x04000142 RID: 322
		private static readonly TypedPropertyDefinition<string> StaticOnlineMeetingExternalLinkProperty = new TypedPropertyDefinition<string>("Event.OnlineMeetingExternalLink", null, true);

		// Token: 0x04000143 RID: 323
		private static readonly TypedPropertyDefinition<PatternedRecurrence> StaticPatternedRecurrenceProperty = new TypedPropertyDefinition<PatternedRecurrence>("Event.PatternedRecurrence", null, true);

		// Token: 0x04000144 RID: 324
		private static readonly TypedPropertyDefinition<bool> StaticResponseRequestedProperty = new TypedPropertyDefinition<bool>("Event.ResponseRequested", false, true);

		// Token: 0x04000145 RID: 325
		private static readonly TypedPropertyDefinition<ResponseStatus> StaticResponseStatusProperty = new TypedPropertyDefinition<ResponseStatus>("Event.ResponseStatus", null, true);

		// Token: 0x04000146 RID: 326
		private static readonly TypedPropertyDefinition<string> StaticSeriesIdProperty = new TypedPropertyDefinition<string>("Event.SeriesId", null, true);

		// Token: 0x04000147 RID: 327
		private static readonly TypedPropertyDefinition<Event> StaticSeriesMasterProperty = new TypedPropertyDefinition<Event>("Event.SeriesMaster", null, true);

		// Token: 0x04000148 RID: 328
		private static readonly TypedPropertyDefinition<string> StaticSeriesMasterIdProperty = new TypedPropertyDefinition<string>("Event.SeriesMasterId", null, true);

		// Token: 0x04000149 RID: 329
		private static readonly TypedPropertyDefinition<FreeBusyStatus> StaticShowAsProperty = new TypedPropertyDefinition<FreeBusyStatus>("Event.ShowAs", FreeBusyStatus.Free, true);

		// Token: 0x0400014A RID: 330
		private static readonly TypedPropertyDefinition<ExDateTime> StaticStartProperty = new TypedPropertyDefinition<ExDateTime>("Event.Start", default(ExDateTime), true);

		// Token: 0x0400014B RID: 331
		private static readonly TypedPropertyDefinition<EventType> StaticTypeProperty = new TypedPropertyDefinition<EventType>("Event.Type", EventType.SingleInstance, true);

		// Token: 0x0400014C RID: 332
		private static readonly TypedPropertyDefinition<IList<EventPopupReminderSetting>> StaticPopupReminderSettingsProperty = new TypedPropertyDefinition<IList<EventPopupReminderSetting>>("Event.PopupReminderSettings", null, true);

		// Token: 0x0400014D RID: 333
		private static readonly TypedPropertyDefinition<string> StaticInternalGlobalObjectIdProperty = new TypedPropertyDefinition<string>("Event.InternalGlobalObjectId", null, true);

		// Token: 0x0400014E RID: 334
		private static readonly TypedPropertyDefinition<bool> StaticInternalMarkAllPropagatedPropertiesAsExceptionProperty = new TypedPropertyDefinition<bool>("Event.InternalMarkAllPropagatedPropertiesAsException", false, true);

		// Token: 0x0400014F RID: 335
		private static readonly TypedPropertyDefinition<bool> StaticInternalSeriesToInstancePropagationProperty = new TypedPropertyDefinition<bool>("Event.InternalSeriesToInstancePropagation", false, true);

		// Token: 0x04000150 RID: 336
		private static readonly TypedPropertyDefinition<bool> StaticInternalIsReceived = new TypedPropertyDefinition<bool>("InternalEvent.IsReceived", false, true);

		// Token: 0x04000151 RID: 337
		private static readonly TypedPropertyDefinition<int> StaticInternalInstanceCreationIndexProperty = new TypedPropertyDefinition<int>("InternalEvent.InstanceCreationIndex", 0, true);

		// Token: 0x04000152 RID: 338
		private static readonly TypedPropertyDefinition<int> StaticInternalSeriesCreationHashProperty = new TypedPropertyDefinition<int>("InternalEvent.SeriesCreationHash", 0, true);

		// Token: 0x04000153 RID: 339
		private static readonly TypedPropertyDefinition<int> StaticInternalSeriesSequenceNumberProperty = new TypedPropertyDefinition<int>("InternalEvent.SeriesSequenceNumber", 0, true);
	}
}
