using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.ReliableActions;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.DataModel.ReliableActions;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x0200004B RID: 75
	public sealed class Event : Item<EventSchema>, IEvent, IItem, IStorageEntity, IEntity, IVersioned, IActionQueue, IActionPropagationState, IEventInternal, IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000474D File Offset: 0x0000294D
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00004760 File Offset: 0x00002960
		Guid? IActionPropagationState.LastExecutedAction
		{
			get
			{
				return base.GetPropertyValueOrDefault<Guid?>(base.Schema.LastExecutedInteropActionProperty);
			}
			set
			{
				base.SetPropertyValue<Guid?>(base.Schema.LastExecutedInteropActionProperty, value);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00004774 File Offset: 0x00002974
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000477C File Offset: 0x0000297C
		ActionInfo[] IActionQueue.ActionsToAdd { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00004785 File Offset: 0x00002985
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000478D File Offset: 0x0000298D
		Guid[] IActionQueue.ActionsToRemove { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00004796 File Offset: 0x00002996
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000479E File Offset: 0x0000299E
		bool IActionQueue.HasData { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000047A7 File Offset: 0x000029A7
		// (set) Token: 0x06000204 RID: 516 RVA: 0x000047AF File Offset: 0x000029AF
		ActionInfo[] IActionQueue.OriginalActions { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000047B8 File Offset: 0x000029B8
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000047CB File Offset: 0x000029CB
		public IList<Attendee> Attendees
		{
			get
			{
				return base.GetPropertyValueOrDefault<IList<Attendee>>(base.Schema.AttendeesProperty);
			}
			set
			{
				base.SetPropertyValue<IList<Attendee>>(base.Schema.AttendeesProperty, value);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000047DF File Offset: 0x000029DF
		// (set) Token: 0x06000208 RID: 520 RVA: 0x000047F2 File Offset: 0x000029F2
		public Calendar Calendar
		{
			get
			{
				return base.GetPropertyValueOrDefault<Calendar>(base.Schema.CalendarProperty);
			}
			set
			{
				base.SetPropertyValue<Calendar>(base.Schema.CalendarProperty, value);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00004806 File Offset: 0x00002A06
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00004819 File Offset: 0x00002A19
		public string ClientId
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.ClientIdProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.ClientIdProperty, value);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000482D File Offset: 0x00002A2D
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00004840 File Offset: 0x00002A40
		public bool DisallowNewTimeProposal
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.DisallowNewTimeProposalProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.DisallowNewTimeProposalProperty, value);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00004854 File Offset: 0x00002A54
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00004867 File Offset: 0x00002A67
		public ExDateTime End
		{
			get
			{
				return base.GetPropertyValueOrDefault<ExDateTime>(base.Schema.EndProperty);
			}
			set
			{
				base.SetPropertyValue<ExDateTime>(base.Schema.EndProperty, value);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000487B File Offset: 0x00002A7B
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000488E File Offset: 0x00002A8E
		public IList<string> ExceptionalProperties
		{
			get
			{
				return base.GetPropertyValueOrDefault<IList<string>>(base.Schema.ExceptionalPropertiesProperty);
			}
			set
			{
				base.SetPropertyValue<IList<string>>(base.Schema.ExceptionalPropertiesProperty, value);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000048A2 File Offset: 0x00002AA2
		// (set) Token: 0x06000212 RID: 530 RVA: 0x000048B5 File Offset: 0x00002AB5
		public bool HasAttendees
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.HasAttendeesProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.HasAttendeesProperty, value);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000048C9 File Offset: 0x00002AC9
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000048DC File Offset: 0x00002ADC
		public string IntendedEndTimeZoneId
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.IntendedEndTimeZoneIdProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.IntendedEndTimeZoneIdProperty, value);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000048F0 File Offset: 0x00002AF0
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00004903 File Offset: 0x00002B03
		public string IntendedStartTimeZoneId
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.IntendedStartTimeZoneIdProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.IntendedStartTimeZoneIdProperty, value);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00004917 File Offset: 0x00002B17
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000492A File Offset: 0x00002B2A
		public FreeBusyStatus IntendedStatus
		{
			get
			{
				return base.GetPropertyValueOrDefault<FreeBusyStatus>(base.Schema.IntendedStatusProperty);
			}
			set
			{
				base.SetPropertyValue<FreeBusyStatus>(base.Schema.IntendedStatusProperty, value);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000493E File Offset: 0x00002B3E
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00004951 File Offset: 0x00002B51
		public bool IsAllDay
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.IsAllDayProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.IsAllDayProperty, value);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00004965 File Offset: 0x00002B65
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00004978 File Offset: 0x00002B78
		public bool IsCancelled
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.IsCancelledProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.IsCancelledProperty, value);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000498C File Offset: 0x00002B8C
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000499F File Offset: 0x00002B9F
		public bool IsDraft
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.IsDraftProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.IsDraftProperty, value);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000049B3 File Offset: 0x00002BB3
		// (set) Token: 0x06000220 RID: 544 RVA: 0x000049C6 File Offset: 0x00002BC6
		public bool IsOnlineMeeting
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.IsOnlineMeetingProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.IsOnlineMeetingProperty, value);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000049DA File Offset: 0x00002BDA
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000049ED File Offset: 0x00002BED
		public bool IsOrganizer
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.IsOrganizerProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.IsOrganizerProperty, value);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00004A01 File Offset: 0x00002C01
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00004A14 File Offset: 0x00002C14
		public Location Location
		{
			get
			{
				return base.GetPropertyValueOrDefault<Location>(base.Schema.LocationProperty);
			}
			set
			{
				base.SetPropertyValue<Location>(base.Schema.LocationProperty, value);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00004A28 File Offset: 0x00002C28
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00004A3B File Offset: 0x00002C3B
		public IList<Event> Occurrences
		{
			get
			{
				return base.GetPropertyValueOrDefault<IList<Event>>(base.Schema.OccurrencesProperty);
			}
			set
			{
				base.SetPropertyValue<IList<Event>>(base.Schema.OccurrencesProperty, value);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00004A4F File Offset: 0x00002C4F
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00004A62 File Offset: 0x00002C62
		public string OnlineMeetingConfLink
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.OnlineMeetingConfLinkProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.OnlineMeetingConfLinkProperty, value);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00004A76 File Offset: 0x00002C76
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00004A89 File Offset: 0x00002C89
		public string OnlineMeetingExternalLink
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.OnlineMeetingExternalLinkProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.OnlineMeetingExternalLinkProperty, value);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00004A9D File Offset: 0x00002C9D
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public Organizer Organizer
		{
			get
			{
				return base.GetPropertyValueOrDefault<Organizer>(base.Schema.OrganizerProperty);
			}
			set
			{
				base.SetPropertyValue<Organizer>(base.Schema.OrganizerProperty, value);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00004AC4 File Offset: 0x00002CC4
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00004AD7 File Offset: 0x00002CD7
		public PatternedRecurrence PatternedRecurrence
		{
			get
			{
				return base.GetPropertyValueOrDefault<PatternedRecurrence>(base.Schema.PatternedRecurrenceProperty);
			}
			set
			{
				base.SetPropertyValue<PatternedRecurrence>(base.Schema.PatternedRecurrenceProperty, value);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00004AEB File Offset: 0x00002CEB
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00004AFE File Offset: 0x00002CFE
		public bool ResponseRequested
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.ResponseRequestedProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.ResponseRequestedProperty, value);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00004B12 File Offset: 0x00002D12
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00004B25 File Offset: 0x00002D25
		public ResponseStatus ResponseStatus
		{
			get
			{
				return base.GetPropertyValueOrDefault<ResponseStatus>(base.Schema.ResponseStatusProperty);
			}
			set
			{
				base.SetPropertyValue<ResponseStatus>(base.Schema.ResponseStatusProperty, value);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00004B39 File Offset: 0x00002D39
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00004B4C File Offset: 0x00002D4C
		public string SeriesId
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.SeriesIdProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.SeriesIdProperty, value);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00004B60 File Offset: 0x00002D60
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00004B73 File Offset: 0x00002D73
		public Event SeriesMaster
		{
			get
			{
				return base.GetPropertyValueOrDefault<Event>(base.Schema.SeriesMasterProperty);
			}
			set
			{
				base.SetPropertyValue<Event>(base.Schema.SeriesMasterProperty, value);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00004B87 File Offset: 0x00002D87
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00004B9A File Offset: 0x00002D9A
		public string SeriesMasterId
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.SeriesMasterIdProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.SeriesMasterIdProperty, value);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00004BAE File Offset: 0x00002DAE
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00004BC1 File Offset: 0x00002DC1
		public FreeBusyStatus ShowAs
		{
			get
			{
				return base.GetPropertyValueOrDefault<FreeBusyStatus>(base.Schema.ShowAsProperty);
			}
			set
			{
				base.SetPropertyValue<FreeBusyStatus>(base.Schema.ShowAsProperty, value);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00004BD5 File Offset: 0x00002DD5
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00004BE8 File Offset: 0x00002DE8
		public ExDateTime Start
		{
			get
			{
				return base.GetPropertyValueOrDefault<ExDateTime>(base.Schema.StartProperty);
			}
			set
			{
				base.SetPropertyValue<ExDateTime>(base.Schema.StartProperty, value);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00004BFC File Offset: 0x00002DFC
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00004C0F File Offset: 0x00002E0F
		public EventType Type
		{
			get
			{
				return base.GetPropertyValueOrDefault<EventType>(base.Schema.TypeProperty);
			}
			set
			{
				base.SetPropertyValue<EventType>(base.Schema.TypeProperty, value);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00004C23 File Offset: 0x00002E23
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00004C36 File Offset: 0x00002E36
		public IList<EventPopupReminderSetting> PopupReminderSettings
		{
			get
			{
				return base.GetPropertyValueOrDefault<IList<EventPopupReminderSetting>>(base.Schema.PopupReminderSettingsProperty);
			}
			set
			{
				base.SetPropertyValue<IList<EventPopupReminderSetting>>(base.Schema.PopupReminderSettingsProperty, value);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00004C4A File Offset: 0x00002E4A
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00004C5D File Offset: 0x00002E5D
		string IEventInternal.GlobalObjectId
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.InternalGlobalObjectIdProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.InternalGlobalObjectIdProperty, value);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00004C71 File Offset: 0x00002E71
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00004C84 File Offset: 0x00002E84
		bool IEventInternal.MarkAllPropagatedPropertiesAsException
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.InternalMarkAllPropagatedPropertiesAsExceptionProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.InternalMarkAllPropagatedPropertiesAsExceptionProperty, value);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00004C98 File Offset: 0x00002E98
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00004CAB File Offset: 0x00002EAB
		bool IEventInternal.IsReceived
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.InternalIsReceived);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.InternalIsReceived, value);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00004CBF File Offset: 0x00002EBF
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00004CD2 File Offset: 0x00002ED2
		int IEventInternal.InstanceCreationIndex
		{
			get
			{
				return base.GetPropertyValueOrDefault<int>(base.Schema.InternalInstanceCreationIndexProperty);
			}
			set
			{
				base.SetPropertyValue<int>(base.Schema.InternalInstanceCreationIndexProperty, value);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00004CE6 File Offset: 0x00002EE6
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00004CF9 File Offset: 0x00002EF9
		int IEventInternal.SeriesCreationHash
		{
			get
			{
				return base.GetPropertyValueOrDefault<int>(base.Schema.InternalSeriesCreationHashProperty);
			}
			set
			{
				base.SetPropertyValue<int>(base.Schema.InternalSeriesCreationHashProperty, value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00004D0D File Offset: 0x00002F0D
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00004D20 File Offset: 0x00002F20
		int IEventInternal.SeriesSequenceNumber
		{
			get
			{
				return base.GetPropertyValueOrDefault<int>(base.Schema.InternalSeriesSequenceNumberProperty);
			}
			set
			{
				base.SetPropertyValue<int>(base.Schema.InternalSeriesSequenceNumberProperty, value);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00004D34 File Offset: 0x00002F34
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00004D47 File Offset: 0x00002F47
		bool IEventInternal.SeriesToInstancePropagation
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.InternalSeriesToInstancePropagationProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.InternalSeriesToInstancePropagationProperty, value);
			}
		}

		// Token: 0x0200004C RID: 76
		public new static class Accessors
		{
			// Token: 0x040000B2 RID: 178
			public static readonly EntityPropertyAccessor<IEvent, IList<Attendee>> Attendees = new EntityPropertyAccessor<IEvent, IList<Attendee>>(SchematizedObject<EventSchema>.SchemaInstance.AttendeesProperty, (IEvent theEvent) => theEvent.Attendees, delegate(IEvent theEvent, IList<Attendee> attendees)
			{
				theEvent.Attendees = attendees;
			});

			// Token: 0x040000B3 RID: 179
			public static readonly EntityPropertyAccessor<IEvent, Calendar> Calendar = new EntityPropertyAccessor<IEvent, Calendar>(SchematizedObject<EventSchema>.SchemaInstance.CalendarProperty, (IEvent theEvent) => theEvent.Calendar, delegate(IEvent theEvent, Calendar calendar)
			{
				theEvent.Calendar = calendar;
			});

			// Token: 0x040000B4 RID: 180
			public static readonly EntityPropertyAccessor<IEvent, string> ClientId = new EntityPropertyAccessor<IEvent, string>(SchematizedObject<EventSchema>.SchemaInstance.ClientIdProperty, (IEvent theEvent) => theEvent.ClientId, delegate(IEvent theEvent, string clientId)
			{
				theEvent.ClientId = clientId;
			});

			// Token: 0x040000B5 RID: 181
			public static readonly EntityPropertyAccessor<IEvent, bool> DisallowNewTimeProposal = new EntityPropertyAccessor<IEvent, bool>(SchematizedObject<EventSchema>.SchemaInstance.DisallowNewTimeProposalProperty, (IEvent theEvent) => theEvent.DisallowNewTimeProposal, delegate(IEvent theEvent, bool disallow)
			{
				theEvent.DisallowNewTimeProposal = disallow;
			});

			// Token: 0x040000B6 RID: 182
			public static readonly EntityPropertyAccessor<IEvent, ExDateTime> End = new EntityPropertyAccessor<IEvent, ExDateTime>(SchematizedObject<EventSchema>.SchemaInstance.EndProperty, (IEvent theEvent) => theEvent.End, delegate(IEvent theEvent, ExDateTime end)
			{
				theEvent.End = end;
			});

			// Token: 0x040000B7 RID: 183
			public static readonly EntityPropertyAccessor<IEvent, IList<string>> ExceptionalProperties = new EntityPropertyAccessor<IEvent, IList<string>>(SchematizedObject<EventSchema>.SchemaInstance.ExceptionalPropertiesProperty, (IEvent theEvent) => theEvent.ExceptionalProperties, delegate(IEvent theEvent, IList<string> properties)
			{
				theEvent.ExceptionalProperties = properties;
			});

			// Token: 0x040000B8 RID: 184
			public static readonly EntityPropertyAccessor<IEvent, bool> HasAttendees = new EntityPropertyAccessor<IEvent, bool>(SchematizedObject<EventSchema>.SchemaInstance.HasAttendeesProperty, (IEvent theEvent) => theEvent.HasAttendees, delegate(IEvent theEvent, bool hasAttendees)
			{
				theEvent.HasAttendees = hasAttendees;
			});

			// Token: 0x040000B9 RID: 185
			public static readonly EntityPropertyAccessor<IEvent, string> IntendedEndTimeZoneId = new EntityPropertyAccessor<IEvent, string>(SchematizedObject<EventSchema>.SchemaInstance.IntendedEndTimeZoneIdProperty, (IEvent theEvent) => theEvent.IntendedEndTimeZoneId, delegate(IEvent theEvent, string timeZoneId)
			{
				theEvent.IntendedEndTimeZoneId = timeZoneId;
			});

			// Token: 0x040000BA RID: 186
			public static readonly EntityPropertyAccessor<IEvent, string> IntendedStartTimeZoneId = new EntityPropertyAccessor<IEvent, string>(SchematizedObject<EventSchema>.SchemaInstance.IntendedStartTimeZoneIdProperty, (IEvent theEvent) => theEvent.IntendedStartTimeZoneId, delegate(IEvent theEvent, string timeZoneId)
			{
				theEvent.IntendedStartTimeZoneId = timeZoneId;
			});

			// Token: 0x040000BB RID: 187
			public static readonly EntityPropertyAccessor<IEvent, FreeBusyStatus> IntendedStatus = new EntityPropertyAccessor<IEvent, FreeBusyStatus>(SchematizedObject<EventSchema>.SchemaInstance.IntendedStatusProperty, (IEvent theEvent) => theEvent.IntendedStatus, delegate(IEvent theEvent, FreeBusyStatus status)
			{
				theEvent.IntendedStatus = status;
			});

			// Token: 0x040000BC RID: 188
			public static readonly EntityPropertyAccessor<IEvent, bool> IsAllDay = new EntityPropertyAccessor<IEvent, bool>(SchematizedObject<EventSchema>.SchemaInstance.IsAllDayProperty, (IEvent theEvent) => theEvent.IsAllDay, delegate(IEvent theEvent, bool allDay)
			{
				theEvent.IsAllDay = allDay;
			});

			// Token: 0x040000BD RID: 189
			public static readonly EntityPropertyAccessor<IEvent, bool> IsCancelled = new EntityPropertyAccessor<IEvent, bool>(SchematizedObject<EventSchema>.SchemaInstance.IsCancelledProperty, (IEvent theEvent) => theEvent.IsCancelled, delegate(IEvent theEvent, bool cancelled)
			{
				theEvent.IsCancelled = cancelled;
			});

			// Token: 0x040000BE RID: 190
			public static readonly EntityPropertyAccessor<IEvent, bool> IsDraft = new EntityPropertyAccessor<IEvent, bool>(SchematizedObject<EventSchema>.SchemaInstance.IsDraftProperty, (IEvent theEvent) => theEvent.IsDraft, delegate(IEvent theEvent, bool isDraft)
			{
				theEvent.IsDraft = isDraft;
			});

			// Token: 0x040000BF RID: 191
			public static readonly EntityPropertyAccessor<IEvent, bool> IsOnlineMeeting = new EntityPropertyAccessor<IEvent, bool>(SchematizedObject<EventSchema>.SchemaInstance.IsOnlineMeetingProperty, (IEvent theEvent) => theEvent.IsOnlineMeeting, delegate(IEvent theEvent, bool onlineMeeting)
			{
				theEvent.IsOnlineMeeting = onlineMeeting;
			});

			// Token: 0x040000C0 RID: 192
			public static readonly EntityPropertyAccessor<IEvent, bool> IsOrganizer = new EntityPropertyAccessor<IEvent, bool>(SchematizedObject<EventSchema>.SchemaInstance.IsOrganizerProperty, (IEvent theEvent) => theEvent.IsOrganizer, delegate(IEvent theEvent, bool organizer)
			{
				theEvent.IsOrganizer = organizer;
			});

			// Token: 0x040000C1 RID: 193
			public static readonly EntityPropertyAccessor<IEvent, Location> Location = new EntityPropertyAccessor<IEvent, Location>(SchematizedObject<EventSchema>.SchemaInstance.LocationProperty, (IEvent theEvent) => theEvent.Location, delegate(IEvent theEvent, Location location)
			{
				theEvent.Location = location;
			});

			// Token: 0x040000C2 RID: 194
			public static readonly EntityPropertyAccessor<IEvent, IList<Event>> Occurrences = new EntityPropertyAccessor<IEvent, IList<Event>>(SchematizedObject<EventSchema>.SchemaInstance.OccurrencesProperty, (IEvent theEvent) => theEvent.Occurrences, delegate(IEvent theEvent, IList<Event> occurrences)
			{
				theEvent.Occurrences = occurrences;
			});

			// Token: 0x040000C3 RID: 195
			public static readonly EntityPropertyAccessor<IEvent, string> OnlineMeetingConfLink = new EntityPropertyAccessor<IEvent, string>(SchematizedObject<EventSchema>.SchemaInstance.OnlineMeetingConfLinkProperty, (IEvent theEvent) => theEvent.OnlineMeetingConfLink, delegate(IEvent theEvent, string link)
			{
				theEvent.OnlineMeetingConfLink = link;
			});

			// Token: 0x040000C4 RID: 196
			public static readonly EntityPropertyAccessor<IEvent, string> OnlineMeetingExternalLink = new EntityPropertyAccessor<IEvent, string>(SchematizedObject<EventSchema>.SchemaInstance.OnlineMeetingExternalLinkProperty, (IEvent theEvent) => theEvent.OnlineMeetingExternalLink, delegate(IEvent theEvent, string link)
			{
				theEvent.OnlineMeetingExternalLink = link;
			});

			// Token: 0x040000C5 RID: 197
			public static readonly EntityPropertyAccessor<IEvent, Organizer> Organizer = new EntityPropertyAccessor<IEvent, Organizer>(SchematizedObject<EventSchema>.SchemaInstance.OrganizerProperty, (IEvent theEvent) => theEvent.Organizer, delegate(IEvent theEvent, Organizer organizer)
			{
				theEvent.Organizer = organizer;
			});

			// Token: 0x040000C6 RID: 198
			public static readonly EntityPropertyAccessor<IEvent, PatternedRecurrence> PatternedRecurrence = new EntityPropertyAccessor<IEvent, PatternedRecurrence>(SchematizedObject<EventSchema>.SchemaInstance.PatternedRecurrenceProperty, (IEvent theEvent) => theEvent.PatternedRecurrence, delegate(IEvent theEvent, PatternedRecurrence recurrence)
			{
				theEvent.PatternedRecurrence = recurrence;
			});

			// Token: 0x040000C7 RID: 199
			public static readonly EntityPropertyAccessor<IEvent, bool> ResponseRequested = new EntityPropertyAccessor<IEvent, bool>(SchematizedObject<EventSchema>.SchemaInstance.ResponseRequestedProperty, (IEvent theEvent) => theEvent.ResponseRequested, delegate(IEvent theEvent, bool responseRequested)
			{
				theEvent.ResponseRequested = responseRequested;
			});

			// Token: 0x040000C8 RID: 200
			public static readonly EntityPropertyAccessor<IEvent, ResponseStatus> ResponseStatus = new EntityPropertyAccessor<IEvent, ResponseStatus>(SchematizedObject<EventSchema>.SchemaInstance.ResponseStatusProperty, (IEvent theEvent) => theEvent.ResponseStatus, delegate(IEvent theEvent, ResponseStatus responseStatus)
			{
				theEvent.ResponseStatus = responseStatus;
			});

			// Token: 0x040000C9 RID: 201
			public static readonly EntityPropertyAccessor<IEvent, string> SeriesId = new EntityPropertyAccessor<IEvent, string>(SchematizedObject<EventSchema>.SchemaInstance.SeriesIdProperty, (IEvent theEvent) => theEvent.SeriesId, delegate(IEvent theEvent, string seriesId)
			{
				theEvent.SeriesId = seriesId;
			});

			// Token: 0x040000CA RID: 202
			public static readonly EntityPropertyAccessor<IEvent, string> SeriesMasterId = new EntityPropertyAccessor<IEvent, string>(SchematizedObject<EventSchema>.SchemaInstance.SeriesMasterIdProperty, (IEvent theEvent) => theEvent.SeriesMasterId, delegate(IEvent theEvent, string seriesMasterId)
			{
				theEvent.SeriesMasterId = seriesMasterId;
			});

			// Token: 0x040000CB RID: 203
			public static readonly EntityPropertyAccessor<IEvent, Event> SeriesMaster = new EntityPropertyAccessor<IEvent, Event>(SchematizedObject<EventSchema>.SchemaInstance.SeriesMasterProperty, (IEvent theEvent) => theEvent.SeriesMaster, delegate(IEvent theEvent, Event master)
			{
				theEvent.SeriesMaster = master;
			});

			// Token: 0x040000CC RID: 204
			public static readonly EntityPropertyAccessor<IEvent, FreeBusyStatus> ShowAs = new EntityPropertyAccessor<IEvent, FreeBusyStatus>(SchematizedObject<EventSchema>.SchemaInstance.ShowAsProperty, (IEvent theEvent) => theEvent.ShowAs, delegate(IEvent theEvent, FreeBusyStatus status)
			{
				theEvent.ShowAs = status;
			});

			// Token: 0x040000CD RID: 205
			public static readonly EntityPropertyAccessor<IEvent, ExDateTime> Start = new EntityPropertyAccessor<IEvent, ExDateTime>(SchematizedObject<EventSchema>.SchemaInstance.StartProperty, (IEvent theEvent) => theEvent.Start, delegate(IEvent theEvent, ExDateTime start)
			{
				theEvent.Start = start;
			});

			// Token: 0x040000CE RID: 206
			public static readonly EntityPropertyAccessor<IEvent, EventType> Type = new EntityPropertyAccessor<IEvent, EventType>(SchematizedObject<EventSchema>.SchemaInstance.TypeProperty, (IEvent theEvent) => theEvent.Type, delegate(IEvent theEvent, EventType type)
			{
				theEvent.Type = type;
			});

			// Token: 0x040000CF RID: 207
			internal static readonly EntityPropertyAccessor<IActionPropagationState, Guid?> LastExecutedInteropAction = new EntityPropertyAccessor<IActionPropagationState, Guid?>(SchematizedObject<EventSchema>.SchemaInstance.LastExecutedInteropActionProperty, (IActionPropagationState actionPropagationState) => actionPropagationState.LastExecutedAction, delegate(IActionPropagationState actionPropagationState, Guid? action)
			{
				actionPropagationState.LastExecutedAction = action;
			});

			// Token: 0x040000D0 RID: 208
			internal static readonly EntityPropertyAccessor<IEventInternal, string> InternalGlobalObjectId = new EntityPropertyAccessor<IEventInternal, string>(SchematizedObject<EventSchema>.SchemaInstance.InternalGlobalObjectIdProperty, (IEventInternal theEvent) => theEvent.GlobalObjectId, delegate(IEventInternal theEvent, string globalObjectId)
			{
				theEvent.GlobalObjectId = globalObjectId;
			});

			// Token: 0x040000D1 RID: 209
			internal static readonly EntityPropertyAccessor<IEventInternal, bool> InternalIsReceived = new EntityPropertyAccessor<IEventInternal, bool>(SchematizedObject<EventSchema>.SchemaInstance.InternalIsReceived, (IEventInternal theEvent) => theEvent.IsReceived, delegate(IEventInternal theEvent, bool isReceived)
			{
				theEvent.IsReceived = isReceived;
			});

			// Token: 0x040000D2 RID: 210
			internal static readonly EntityPropertyAccessor<IEventInternal, bool> InternalMarkAllPropagatedPropertiesAsException = new EntityPropertyAccessor<IEventInternal, bool>(SchematizedObject<EventSchema>.SchemaInstance.InternalMarkAllPropagatedPropertiesAsExceptionProperty, (IEventInternal theEvent) => theEvent.MarkAllPropagatedPropertiesAsException, delegate(IEventInternal theEvent, bool mark)
			{
				theEvent.MarkAllPropagatedPropertiesAsException = mark;
			});

			// Token: 0x040000D3 RID: 211
			internal static readonly EntityPropertyAccessor<IEventInternal, bool> InternalSeriesToInstancePropagation = new EntityPropertyAccessor<IEventInternal, bool>(SchematizedObject<EventSchema>.SchemaInstance.InternalSeriesToInstancePropagationProperty, (IEventInternal theEvent) => theEvent.SeriesToInstancePropagation, delegate(IEventInternal theEvent, bool seriesToInstancePropagation)
			{
				theEvent.SeriesToInstancePropagation = seriesToInstancePropagation;
			});

			// Token: 0x040000D4 RID: 212
			internal static readonly EntityPropertyAccessor<IEventInternal, int> InternalInstanceCreationIndex = new EntityPropertyAccessor<IEventInternal, int>(SchematizedObject<EventSchema>.SchemaInstance.InternalInstanceCreationIndexProperty, (IEventInternal theEvent) => theEvent.InstanceCreationIndex, delegate(IEventInternal theEvent, int index)
			{
				theEvent.InstanceCreationIndex = index;
			});

			// Token: 0x040000D5 RID: 213
			internal static readonly EntityPropertyAccessor<IEventInternal, int> InternalSeriesSequenceNumber = new EntityPropertyAccessor<IEventInternal, int>(SchematizedObject<EventSchema>.SchemaInstance.InternalSeriesSequenceNumberProperty, (IEventInternal theEvent) => theEvent.SeriesSequenceNumber, delegate(IEventInternal theEvent, int seriesSequenceNumber)
			{
				theEvent.SeriesSequenceNumber = seriesSequenceNumber;
			});

			// Token: 0x040000D6 RID: 214
			internal static readonly EntityPropertyAccessor<IEventInternal, int> InternalSeriesCreationHash = new EntityPropertyAccessor<IEventInternal, int>(SchematizedObject<EventSchema>.SchemaInstance.InternalSeriesCreationHashProperty, (IEventInternal theEvent) => theEvent.SeriesCreationHash, delegate(IEventInternal theEvent, int hash)
			{
				theEvent.SeriesCreationHash = hash;
			});
		}
	}
}
