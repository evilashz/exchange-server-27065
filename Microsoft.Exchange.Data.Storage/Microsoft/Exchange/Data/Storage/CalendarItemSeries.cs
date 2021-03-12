using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003B4 RID: 948
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarItemSeries : CalendarItemBase, ICalendarItemSeries, ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002B3B RID: 11067 RVA: 0x000ACD69 File Offset: 0x000AAF69
		internal CalendarItemSeries(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000ACD72 File Offset: 0x000AAF72
		public new static CalendarItemSeries Bind(StoreSession session, StoreId storeId)
		{
			return CalendarItemSeries.Bind(session, storeId, null);
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000ACD7C File Offset: 0x000AAF7C
		public new static CalendarItemSeries Bind(StoreSession session, StoreId storeId, params PropertyDefinition[] propsToReturn)
		{
			return CalendarItemSeries.Bind(session, storeId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x000ACD8B File Offset: 0x000AAF8B
		public new static CalendarItemSeries Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<CalendarItemSeries>(session, storeId, CalendarItemSeriesSchema.Instance, propsToReturn);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x000ACD9C File Offset: 0x000AAF9C
		public static CalendarItemSeries CreateSeries(StoreSession session, StoreId parentFolderId, bool forOrganizer = true)
		{
			if (parentFolderId == null)
			{
				throw new ArgumentNullException("parentFolderId");
			}
			CalendarItemSeries result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CalendarItemSeries calendarItemSeries = ItemBuilder.CreateNewItem<CalendarItemSeries>(session, parentFolderId, ItemCreateInfo.CalendarItemSeriesInfo);
				disposeGuard.Add<CalendarItemSeries>(calendarItemSeries);
				calendarItemSeries.Initialize(forOrganizer);
				disposeGuard.Success();
				result = calendarItemSeries;
			}
			return result;
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000ACE08 File Offset: 0x000AB008
		public override void CopyToFolder(MailboxSession destinationSession, StoreObjectId destinationFolderId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000ACE0F File Offset: 0x000AB00F
		public override void MoveToFolder(MailboxSession destinationSession, StoreObjectId destinationFolderId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000ACE16 File Offset: 0x000AB016
		public override string GenerateWhen()
		{
			return string.Empty;
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000ACE1D File Offset: 0x000AB01D
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CalendarItemSeries>(this);
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x000ACE25 File Offset: 0x000AB025
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return CalendarItemSeriesSchema.Instance;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x000ACE37 File Offset: 0x000AB037
		// (set) Token: 0x06002B46 RID: 11078 RVA: 0x000ACE4F File Offset: 0x000AB04F
		public override int AppointmentLastSequenceNumber
		{
			get
			{
				this.CheckDisposed("AppointmentLastSequenceNumber::get");
				return base.GetValueOrDefault<int>(CalendarItemBaseSchema.AppointmentLastSequenceNumber);
			}
			set
			{
				this.CheckDisposed("AppointmentLastSequenceNumber::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(61813U);
				this[CalendarItemBaseSchema.AppointmentLastSequenceNumber] = value;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x000ACE7D File Offset: 0x000AB07D
		// (set) Token: 0x06002B48 RID: 11080 RVA: 0x000ACE95 File Offset: 0x000AB095
		public override ExDateTime StartTime
		{
			get
			{
				this.CheckDisposed("StartTime::get");
				return base.GetValueOrDefault<ExDateTime>(CalendarItemBaseSchema.ClipStartTime);
			}
			set
			{
				this.CheckDisposed("StartTime::set");
				this[CalendarItemBaseSchema.ClipStartTime] = value;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x000ACEB3 File Offset: 0x000AB0B3
		// (set) Token: 0x06002B4A RID: 11082 RVA: 0x000ACECB File Offset: 0x000AB0CB
		public override ExDateTime EndTime
		{
			get
			{
				this.CheckDisposed("EndTime::get");
				return base.GetValueOrDefault<ExDateTime>(CalendarItemBaseSchema.ClipEndTime);
			}
			set
			{
				this.CheckDisposed("EndTime::set");
				this[CalendarItemBaseSchema.ClipEndTime] = value;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000ACEE9 File Offset: 0x000AB0E9
		public override ExDateTime StartWallClock
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x000ACEF0 File Offset: 0x000AB0F0
		public override ExDateTime EndWallClock
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x000ACEF7 File Offset: 0x000AB0F7
		public bool CalendarInteropActionQueueHasData
		{
			get
			{
				this.CheckDisposed("CalendarInteropActionQueueHasData::get");
				return base.GetValueOrDefault<bool>(CalendarItemSeriesSchema.CalendarInteropActionQueueHasData);
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06002B4E RID: 11086 RVA: 0x000ACF0F File Offset: 0x000AB10F
		internal override bool IsAttendeeListDirty
		{
			get
			{
				this.FetchAttendeeCollection(false);
				return this.attendees != null && this.attendees.IsDirty;
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x000ACF2E File Offset: 0x000AB12E
		internal override bool IsAttendeeListCreated
		{
			get
			{
				this.FetchAttendeeCollection(false);
				return this.attendees != null;
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x000ACF44 File Offset: 0x000AB144
		// (set) Token: 0x06002B51 RID: 11089 RVA: 0x000ACF5D File Offset: 0x000AB15D
		public int SeriesSequenceNumber
		{
			get
			{
				this.CheckDisposed("SeriesSequenceNumber::get");
				return base.GetValueOrDefault<int>(CalendarItemBaseSchema.AppointmentSequenceNumber, -1);
			}
			set
			{
				this.CheckDisposed("SeriesSequenceNumber::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(34812U);
				this[CalendarItemBaseSchema.AppointmentSequenceNumber] = value;
			}
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000ACF8C File Offset: 0x000AB18C
		internal override void Initialize(bool forOrganizer)
		{
			base.Initialize(forOrganizer);
			this[InternalSchema.ItemClass] = "IPM.AppointmentSeries";
			base.IsHiddenFromLegacyClients = true;
			this[InternalSchema.ConversationIndexTracking] = true;
			if (forOrganizer)
			{
				base.SeriesId = Guid.NewGuid().ToString();
				this.SeriesSequenceNumber = 0;
			}
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000ACFEB File Offset: 0x000AB1EB
		protected override void InternalUpdateSequencingProperties(bool isToAllAttendees, MeetingMessage message, int minSequenceNumber, int? seriesSequenceNumber = null)
		{
			if (seriesSequenceNumber != null)
			{
				this.SeriesSequenceNumber = seriesSequenceNumber.Value;
				message[InternalSchema.AppointmentSequenceNumber] = seriesSequenceNumber.Value;
			}
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000AD01A File Offset: 0x000AB21A
		protected override void SetSequencingPropertiesForForward(MeetingRequest meetingRequest)
		{
			meetingRequest.SeriesSequenceNumber = this.SeriesSequenceNumber;
			meetingRequest[CalendarItemBaseSchema.AppointmentSequenceNumber] = this.SeriesSequenceNumber;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000AD040 File Offset: 0x000AB240
		internal override IAttendeeCollection FetchAttendeeCollection(bool forceOpen)
		{
			this.CheckDisposed("FetchAttendeeCollection");
			if (this.attendees == null)
			{
				CoreRecipientCollection recipientCollection = base.CoreItem.GetRecipientCollection(forceOpen);
				if (recipientCollection != null)
				{
					this.attendees = new AttendeeCollection(recipientCollection);
					base.ResetAttendeeCache();
				}
			}
			return this.attendees;
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000AD088 File Offset: 0x000AB288
		protected override void SendMeetingCancellations(MailboxSession mailboxSession, bool isToAllAttendees, IList<Attendee> removedAttendeeList, bool copyToSentItems, bool ignoreSendAsRight, CancellationRumInfo rumInfo)
		{
			if (removedAttendeeList.Count == 0)
			{
				return;
			}
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, int>((long)this.GetHashCode(), "Storage.CalendarItemBase.SendMeetingCancellations: GOID={0}; users={1}", base.GlobalObjectId, removedAttendeeList.Count);
			using (MeetingCancellation meetingCancellation = base.CreateMeetingCancellation(mailboxSession, isToAllAttendees, null, null))
			{
				meetingCancellation.CopySendableParticipantsToMessage(removedAttendeeList);
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(33141U, LastChangeAction.SendMeetingCancellations);
				base.SendMessage(mailboxSession, meetingCancellation, copyToSentItems, ignoreSendAsRight);
			}
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000AD114 File Offset: 0x000AB314
		protected override void SetDeclineIntent(bool intendToSendResponse)
		{
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000AD116 File Offset: 0x000AB316
		protected override MeetingRequest CreateNewMeetingRequest(MailboxSession mailboxSession)
		{
			return MeetingRequest.CreateMeetingRequestSeries(mailboxSession);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000AD11E File Offset: 0x000AB31E
		protected override MeetingCancellation CreateNewMeetingCancelation(MailboxSession mailboxSession)
		{
			return MeetingCancellation.CreateMeetingCancellationSeries(mailboxSession);
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000AD126 File Offset: 0x000AB326
		protected override MeetingResponse CreateNewMeetingResponse(MailboxSession mailboxSession, ResponseType responseType)
		{
			return MeetingResponse.CreateMeetingResponseSeries(mailboxSession, responseType);
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06002B5B RID: 11099 RVA: 0x000AD12F File Offset: 0x000AB32F
		protected override bool IsInThePast
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000AD136 File Offset: 0x000AB336
		protected override Reminder CreateReminderObject()
		{
			return new CalendarItemSeries.SeriesReminder(this);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000AD13E File Offset: 0x000AB33E
		protected override void CopyMeetingRequestProperties(MeetingRequest meetingRequest)
		{
			base.CopyMeetingRequestProperties(meetingRequest);
			CalendarItemBase.CopyPropertiesTo(this, meetingRequest, MeetingMessage.WriteOnCreateSeriesProperties);
		}

		// Token: 0x04001845 RID: 6213
		private AttendeeCollection attendees;

		// Token: 0x020003B5 RID: 949
		private class SeriesReminder : Reminder
		{
			// Token: 0x06002B5E RID: 11102 RVA: 0x000AD153 File Offset: 0x000AB353
			internal SeriesReminder(CalendarItemSeries item) : base(item)
			{
			}

			// Token: 0x17000E26 RID: 3622
			// (get) Token: 0x06002B5F RID: 11103 RVA: 0x000AD15C File Offset: 0x000AB35C
			// (set) Token: 0x06002B60 RID: 11104 RVA: 0x000AD172 File Offset: 0x000AB372
			public override ExDateTime? DueBy
			{
				get
				{
					return null;
				}
				set
				{
					throw base.PropertyNotSupported("DueBy");
				}
			}

			// Token: 0x17000E27 RID: 3623
			// (get) Token: 0x06002B61 RID: 11105 RVA: 0x000AD17F File Offset: 0x000AB37F
			// (set) Token: 0x06002B62 RID: 11106 RVA: 0x000AD187 File Offset: 0x000AB387
			public override int MinutesBeforeStart
			{
				get
				{
					return base.MinutesBeforeStart;
				}
				set
				{
					base.Item[ItemSchema.ReminderMinutesBeforeStart] = value;
				}
			}

			// Token: 0x17000E28 RID: 3624
			// (get) Token: 0x06002B63 RID: 11107 RVA: 0x000AD19F File Offset: 0x000AB39F
			// (set) Token: 0x06002B64 RID: 11108 RVA: 0x000AD1B1 File Offset: 0x000AB3B1
			public override bool IsSet
			{
				get
				{
					return base.Item.GetValueOrDefault<bool>(CalendarItemSeriesSchema.SeriesReminderIsSet);
				}
				set
				{
					base.Item[CalendarItemSeriesSchema.SeriesReminderIsSet] = value;
				}
			}

			// Token: 0x06002B65 RID: 11109 RVA: 0x000AD1C9 File Offset: 0x000AB3C9
			protected override void Adjust(ExDateTime actualizationTime)
			{
			}

			// Token: 0x06002B66 RID: 11110 RVA: 0x000AD1CB File Offset: 0x000AB3CB
			protected override Reminder.ReminderInfo GetNextPertinentItemInfo(ExDateTime actualizationTime)
			{
				return null;
			}

			// Token: 0x06002B67 RID: 11111 RVA: 0x000AD1CE File Offset: 0x000AB3CE
			protected override Reminder.ReminderInfo GetPertinentItemInfo(ExDateTime actualizationTime)
			{
				return null;
			}

			// Token: 0x17000E29 RID: 3625
			// (get) Token: 0x06002B68 RID: 11112 RVA: 0x000AD1D4 File Offset: 0x000AB3D4
			// (set) Token: 0x06002B69 RID: 11113 RVA: 0x000AD1EA File Offset: 0x000AB3EA
			public override ExDateTime? ReminderNextTime
			{
				get
				{
					return null;
				}
				protected set
				{
					throw base.PropertyNotSupported("ReminderNextTime");
				}
			}

			// Token: 0x06002B6A RID: 11114 RVA: 0x000AD1F7 File Offset: 0x000AB3F7
			public override void Dismiss(ExDateTime actualizationTime)
			{
			}

			// Token: 0x06002B6B RID: 11115 RVA: 0x000AD1F9 File Offset: 0x000AB3F9
			public override void Snooze(ExDateTime actualizationTime, ExDateTime snoozeTime)
			{
			}

			// Token: 0x06002B6C RID: 11116 RVA: 0x000AD1FB File Offset: 0x000AB3FB
			public override void Adjust()
			{
			}

			// Token: 0x06002B6D RID: 11117 RVA: 0x000AD1FD File Offset: 0x000AB3FD
			protected internal override void SaveStateAsInitial(bool throwOnFailure)
			{
			}
		}
	}
}
