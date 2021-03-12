using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003AE RID: 942
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarItemOccurrence : CalendarItemInstance, ICalendarItemOccurrence, ICalendarItemInstance, ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002AEE RID: 10990 RVA: 0x000AB7A4 File Offset: 0x000A99A4
		internal CalendarItemOccurrence(ICoreItem coreItem) : base(coreItem)
		{
			this.usingMasterAttachments = !this.IsException;
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x000AB7BC File Offset: 0x000A99BC
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return CalendarItemOccurrenceSchema.Instance;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000AB7CE File Offset: 0x000A99CE
		public ExDateTime OriginalStartTime
		{
			get
			{
				this.CheckDisposed("OriginalStartTime::get");
				return this.OccurrencePropertyBag.OriginalStartTime;
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x000AB7E6 File Offset: 0x000A99E6
		public override bool IsDirty
		{
			get
			{
				return base.IsDirty || this.IsAttendeeListDirty;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000AB7F8 File Offset: 0x000A99F8
		public OccurrencePropertyBag OccurrencePropertyBag
		{
			get
			{
				this.CheckDisposed("OccurrencePropertyBag::get");
				OccurrencePropertyBag occurrencePropertyBag = base.PropertyBag as OccurrencePropertyBag;
				if (occurrencePropertyBag == null)
				{
					occurrencePropertyBag = (OccurrencePropertyBag)((AcrPropertyBag)base.PropertyBag).PropertyBag;
				}
				return occurrencePropertyBag;
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000AB838 File Offset: 0x000A9A38
		public VersionedId MasterId
		{
			get
			{
				this.CheckDisposed("MasterId::get");
				StoreObjectId storeObjectId = base.StoreObjectId;
				StoreObjectId itemId = StoreObjectId.FromProviderSpecificId(storeObjectId.ProviderLevelItemId, StoreObjectType.CalendarItem);
				return new VersionedId(itemId, base.Id.ChangeKeyAsByteArray());
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000AB876 File Offset: 0x000A9A76
		public bool IsException
		{
			get
			{
				this.CheckDisposed("IsException::get");
				return this.OccurrencePropertyBag.IsException;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000AB88E File Offset: 0x000A9A8E
		public override bool IsForwardAllowed
		{
			get
			{
				this.CheckDisposed("IsForwardAllowed::get");
				return base.IsMeeting || this.IsException;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000AB8AB File Offset: 0x000A9AAB
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x000AB8C8 File Offset: 0x000A9AC8
		public override int AppointmentLastSequenceNumber
		{
			get
			{
				this.CheckDisposed("AppointmentLastSequenceNumber::get");
				return this.OccurrencePropertyBag.MasterCalendarItem.AppointmentLastSequenceNumber;
			}
			set
			{
				this.CheckDisposed("AppointmentLastSequenceNumber::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(57343U);
				this.OccurrencePropertyBag.MasterCalendarItem.AppointmentLastSequenceNumber = value;
				this.OccurrencePropertyBag.MasterCalendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(45055U);
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000AB91B File Offset: 0x000A9B1B
		internal override bool AreAttachmentsDirty
		{
			get
			{
				return base.AreAttachmentsDirty || (this.IsException && this.OccurrencePropertyBag.ExceptionMessage != null && this.OccurrencePropertyBag.ExceptionMessage.AreAttachmentsDirty);
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x000AB94E File Offset: 0x000A9B4E
		internal override bool IsAttendeeListDirty
		{
			get
			{
				this.FetchAttendeeCollection(false);
				return this.attendees != null && this.attendees.IsDirty;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000AB96D File Offset: 0x000A9B6D
		internal override bool IsAttendeeListCreated
		{
			get
			{
				this.FetchAttendeeCollection(false);
				return this.attendees != null;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x000AB983 File Offset: 0x000A9B83
		protected override bool IsInThePast
		{
			get
			{
				return this.EndTime < ExDateTime.GetNow(base.PropertyBag.ExTimeZone);
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000AB9A0 File Offset: 0x000A9BA0
		protected override bool CanDoObjectUpdate
		{
			get
			{
				return this.IsDirty;
			}
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000AB9A8 File Offset: 0x000A9BA8
		public new static CalendarItemOccurrence Bind(StoreSession session, StoreId storeId)
		{
			return CalendarItemOccurrence.Bind(session, storeId, null);
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000AB9B2 File Offset: 0x000A9BB2
		public new static CalendarItemOccurrence Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<CalendarItemOccurrence>(session, storeId, CalendarItemOccurrenceSchema.Instance, propsToReturn);
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000AB9C1 File Offset: 0x000A9BC1
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CalendarItemOccurrence>(this);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000AB9C9 File Offset: 0x000A9BC9
		public CalendarItem GetMaster()
		{
			this.CheckDisposed("GetMaster");
			return CalendarItem.Bind(base.Session, this.MasterId);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000AB9E8 File Offset: 0x000A9BE8
		public void MakeModifiedOccurrence()
		{
			this.CheckDisposed("MakeModifiedOccurrence");
			ExTraceGlobals.RecurrenceTracer.Information<int>((long)this.GetHashCode(), "Storage.CalendarItemOccurrence.MakeModifiedOccurrence. HashCode = {0}.", this.GetHashCode());
			OccurrencePropertyBag occurrencePropertyBag = base.PropertyBag as OccurrencePropertyBag;
			if (!this.IsException)
			{
				if (occurrencePropertyBag == null)
				{
					occurrencePropertyBag = (OccurrencePropertyBag)((AcrPropertyBag)base.PropertyBag).PropertyBag;
				}
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(51317U);
				occurrencePropertyBag.MakeException();
			}
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000ABA5F File Offset: 0x000A9C5F
		protected override void ValidateForwardArguments(MailboxSession session, StoreObjectId parentFolderId, ReplyForwardConfiguration replyForwardParameters)
		{
			base.ValidateForwardArguments(session, parentFolderId, replyForwardParameters);
			if (!this.IsForwardAllowed)
			{
				throw new InvalidOperationException("A forward can't be created on a read-only calendar item. Call MakeModifiedOccurrence() first.");
			}
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000ABA80 File Offset: 0x000A9C80
		public override string GenerateWhen()
		{
			return CalendarItem.InternalWhen(this, null, false).ToString(base.Session.InternalPreferedCulture);
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000ABAA8 File Offset: 0x000A9CA8
		public bool IsModifiedProperty(PropertyDefinition propertyDefinition)
		{
			this.CheckDisposed("IsModifiedProperty");
			if (!this.IsException)
			{
				return false;
			}
			SmartPropertyDefinition smartPropertyDefinition = propertyDefinition as SmartPropertyDefinition;
			if (smartPropertyDefinition != null)
			{
				for (int i = 0; i < smartPropertyDefinition.Dependencies.Length; i++)
				{
					PropertyDependency propertyDependency = smartPropertyDefinition.Dependencies[i];
					if ((propertyDependency.Type & PropertyDependencyType.NeedForRead) != PropertyDependencyType.None && this.OccurrencePropertyBag.IsModifiedProperty(propertyDependency.Property))
					{
						return true;
					}
				}
				return false;
			}
			return this.OccurrencePropertyBag.IsModifiedProperty(propertyDefinition);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000ABB1D File Offset: 0x000A9D1D
		public override MeetingResponse RespondToMeetingRequest(ResponseType responseType, string subjectPrefix, ExDateTime? proposedStart = null, ExDateTime? proposedEnd = null)
		{
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, ResponseType>((long)this.GetHashCode(), "Storage.CalendarItem.RespondToMeetingRequest: GOID={0}; responseType={1}.", base.GlobalObjectId, responseType);
			return base.RespondToMeetingRequest(responseType, subjectPrefix, proposedStart, proposedEnd);
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000ABB47 File Offset: 0x000A9D47
		protected override void SetDeclineIntent(bool intendToSendResponse)
		{
			this.OccurrencePropertyBag.MasterCalendarItem.ClientIntent = CalendarItemOccurrence.GetDeclineIntent(intendToSendResponse);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000ABB5F File Offset: 0x000A9D5F
		internal static ClientIntentFlags GetDeclineIntent(bool intendToSendResponse)
		{
			if (!intendToSendResponse)
			{
				return ClientIntentFlags.DeletedExceptionWithNoResponse;
			}
			return ClientIntentFlags.RespondedExceptionDecline;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000ABB6B File Offset: 0x000A9D6B
		public override void CopyToFolder(MailboxSession destinationSession, StoreObjectId destinationFolderId)
		{
			this.CheckDisposed("CopyToFolder");
			throw new InvalidOperationException("Occurrences can't be copied to another folder");
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000ABB82 File Offset: 0x000A9D82
		protected override void InitializeMeetingResponse(MeetingResponse meetingResponse, ResponseType responseType, bool isCalendarDelegateAccess, ExDateTime? proposedStart, ExDateTime? proposedEnd)
		{
			base.InitializeMeetingResponse(meetingResponse, responseType, isCalendarDelegateAccess, proposedStart, proposedEnd);
			meetingResponse[InternalSchema.AppointmentRecurrenceBlob] = this.OccurrencePropertyBag.MasterCalendarItem.PropertyBag.GetLargeBinaryProperty(InternalSchema.AppointmentRecurrenceBlob);
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000ABBB6 File Offset: 0x000A9DB6
		public override void MoveToFolder(MailboxSession destinationSession, StoreObjectId destinationFolderId)
		{
			this.CheckDisposed("MoveToFolder");
			throw new InvalidOperationException("Occurrences can't be moved to another folder");
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000ABBD0 File Offset: 0x000A9DD0
		internal override IAttendeeCollection FetchAttendeeCollection(bool forceOpen)
		{
			if (this.attendees == null)
			{
				CoreRecipientCollection coreRecipientCollection = null;
				if (this.OccurrencePropertyBag.ExceptionMessage != null)
				{
					coreRecipientCollection = this.OccurrencePropertyBag.ExceptionMessage.CoreItem.GetRecipientCollection(forceOpen);
				}
				CoreRecipientCollection recipientCollection = this.OccurrencePropertyBag.MasterCalendarItem.CoreItem.GetRecipientCollection(forceOpen);
				if (coreRecipientCollection == null && recipientCollection == null)
				{
					return null;
				}
				this.attendees = new OccurrenceAttendeeCollection(this);
				base.ResetAttendeeCache();
			}
			return this.attendees;
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000ABC44 File Offset: 0x000A9E44
		protected override AttachmentCollection FetchAttachmentCollection()
		{
			if (this.usingMasterAttachments && this.IsException)
			{
				base.CoreItem.DisposeAttachmentCollection();
				this.attachmentCollection = null;
			}
			if (this.attachmentCollection == null)
			{
				if (this.IsException)
				{
					this.usingMasterAttachments = false;
					base.CoreItem.OpenAttachmentCollection();
					this.attachmentCollection = new AttachmentCollection(this, false);
				}
				else
				{
					Item masterCalendarItem = this.OccurrencePropertyBag.MasterCalendarItem;
					base.CoreItem.OpenAttachmentCollection(masterCalendarItem.CoreItem);
					this.attachmentCollection = new AttachmentCollection(this, true);
				}
			}
			return this.attachmentCollection;
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000ABCD4 File Offset: 0x000A9ED4
		protected override Reminders<EventTimeBasedInboxReminder> FetchEventTimeBasedInboxReminders()
		{
			Reminders<EventTimeBasedInboxReminder> reminders = Reminders<EventTimeBasedInboxReminder>.Get(this, CalendarItemBaseSchema.EventTimeBasedInboxReminders);
			if (this.IsException)
			{
				Reminders<EventTimeBasedInboxReminder> reminders2 = Reminders<EventTimeBasedInboxReminder>.Get(this.OccurrencePropertyBag.MasterCalendarItem, CalendarItemBaseSchema.EventTimeBasedInboxReminders);
				if (reminders2 == null)
				{
					return reminders;
				}
				if (reminders == null)
				{
					return reminders2;
				}
				List<EventTimeBasedInboxReminder> list = new List<EventTimeBasedInboxReminder>();
				foreach (EventTimeBasedInboxReminder eventTimeBasedInboxReminder in reminders.ReminderList)
				{
					if (eventTimeBasedInboxReminder.OccurrenceChange == EmailReminderChangeType.Deleted && reminders2.GetReminder(eventTimeBasedInboxReminder.SeriesReminderId) == null)
					{
						list.Add(eventTimeBasedInboxReminder);
					}
				}
				foreach (EventTimeBasedInboxReminder item in list)
				{
					reminders.ReminderList.Remove(item);
				}
				foreach (EventTimeBasedInboxReminder eventTimeBasedInboxReminder2 in reminders2.ReminderList)
				{
					if (EventTimeBasedInboxReminder.GetSeriesReminder(reminders, eventTimeBasedInboxReminder2.Identifier) == null)
					{
						reminders.ReminderList.Add(eventTimeBasedInboxReminder2);
					}
				}
			}
			return reminders;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000ABE20 File Offset: 0x000AA020
		protected override void UpdateEventTimeBasedInboxRemindersForSave(Reminders<EventTimeBasedInboxReminder> reminders)
		{
			if (reminders == null)
			{
				return;
			}
			List<EventTimeBasedInboxReminder> list = new List<EventTimeBasedInboxReminder>();
			foreach (EventTimeBasedInboxReminder eventTimeBasedInboxReminder in reminders.ReminderList)
			{
				if (eventTimeBasedInboxReminder.OccurrenceChange == EmailReminderChangeType.None)
				{
					list.Add(eventTimeBasedInboxReminder);
				}
			}
			foreach (EventTimeBasedInboxReminder item in list)
			{
				reminders.ReminderList.Remove(item);
			}
			EventTimeBasedInboxReminder.UpdateIdentifiersForModifiedReminders(reminders);
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000ABED0 File Offset: 0x000AA0D0
		protected override void OnAfterSave(ConflictResolutionResult acrResults)
		{
			base.OnAfterSave(acrResults);
			if (!base.IsInMemoryObject)
			{
				base.CoreItem.DisposeAttachmentCollection();
			}
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000ABEEC File Offset: 0x000AA0EC
		protected override Reminder CreateReminderObject()
		{
			return new CalendarItemOccurrence.CustomReminder(this);
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000ABEF4 File Offset: 0x000AA0F4
		protected override void UpdateAttendeesOnException()
		{
			if (this.IsAttendeeListDirty)
			{
				this.MakeModifiedOccurrence();
				this.attendees.ApplyChangesToExceptionAttendeeCollection(base.MapiMessage);
			}
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000ABF15 File Offset: 0x000AA115
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			this.OnBeforeSaveUpdateExceptionProperties();
			this.attendees = null;
			this.attachmentCollection = null;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000ABF31 File Offset: 0x000AA131
		private void OnBeforeSaveUpdateExceptionProperties()
		{
			if (this.IsAttendeeListDirty)
			{
				this[InternalSchema.ExceptionalAttendees] = true;
			}
		}

		// Token: 0x04001827 RID: 6183
		private OccurrenceAttendeeCollection attendees;

		// Token: 0x04001828 RID: 6184
		private bool usingMasterAttachments;

		// Token: 0x020003AF RID: 943
		private class CustomReminder : Reminder
		{
			// Token: 0x06002B14 RID: 11028 RVA: 0x000ABF4C File Offset: 0x000AA14C
			internal CustomReminder(CalendarItemOccurrence item) : base(item)
			{
			}

			// Token: 0x17000E0F RID: 3599
			// (get) Token: 0x06002B15 RID: 11029 RVA: 0x000ABF55 File Offset: 0x000AA155
			// (set) Token: 0x06002B16 RID: 11030 RVA: 0x000ABF5D File Offset: 0x000AA15D
			public override ExDateTime? DueBy
			{
				get
				{
					return base.DueBy;
				}
				set
				{
					throw base.PropertyNotSupported("DueBy");
				}
			}

			// Token: 0x17000E10 RID: 3600
			// (get) Token: 0x06002B17 RID: 11031 RVA: 0x000ABF6A File Offset: 0x000AA16A
			// (set) Token: 0x06002B18 RID: 11032 RVA: 0x000ABF72 File Offset: 0x000AA172
			public override int MinutesBeforeStart
			{
				get
				{
					return base.MinutesBeforeStart;
				}
				set
				{
					this.Item[InternalSchema.ReminderMinutesBeforeStart] = value;
				}
			}

			// Token: 0x17000E11 RID: 3601
			// (get) Token: 0x06002B19 RID: 11033 RVA: 0x000ABF8A File Offset: 0x000AA18A
			// (set) Token: 0x06002B1A RID: 11034 RVA: 0x000ABF9C File Offset: 0x000AA19C
			public override ExDateTime? ReminderNextTime
			{
				get
				{
					return this.Master.Reminder.ReminderNextTime;
				}
				protected set
				{
					throw base.PropertyNotSupported("ReminderNextTime::set");
				}
			}

			// Token: 0x17000E12 RID: 3602
			// (get) Token: 0x06002B1B RID: 11035 RVA: 0x000ABFAC File Offset: 0x000AA1AC
			private bool IsItemStateValid
			{
				get
				{
					ExDateTime? valueAsNullable = this.Item.GetValueAsNullable<ExDateTime>(InternalSchema.StartTime);
					ExDateTime? valueAsNullable2 = this.Item.GetValueAsNullable<ExDateTime>(InternalSchema.EndTime);
					return valueAsNullable != null && valueAsNullable2 != null && valueAsNullable.Value <= valueAsNullable2.Value;
				}
			}

			// Token: 0x17000E13 RID: 3603
			// (get) Token: 0x06002B1C RID: 11036 RVA: 0x000AC002 File Offset: 0x000AA202
			private new CalendarItemOccurrence Item
			{
				get
				{
					return (CalendarItemOccurrence)base.Item;
				}
			}

			// Token: 0x17000E14 RID: 3604
			// (get) Token: 0x06002B1D RID: 11037 RVA: 0x000AC00F File Offset: 0x000AA20F
			private Item Master
			{
				get
				{
					return this.Item.OccurrencePropertyBag.MasterCalendarItem;
				}
			}

			// Token: 0x06002B1E RID: 11038 RVA: 0x000AC021 File Offset: 0x000AA221
			public override void Adjust()
			{
				this.Item.LocationIdentifierHelperInstance.SetLocationIdentifier(53877U);
				base.Adjust();
			}

			// Token: 0x06002B1F RID: 11039 RVA: 0x000AC03E File Offset: 0x000AA23E
			protected internal override void SaveStateAsInitial(bool throwOnFailure)
			{
			}

			// Token: 0x06002B20 RID: 11040 RVA: 0x000AC040 File Offset: 0x000AA240
			protected override void Adjust(ExDateTime actualizationTime)
			{
				if (Reminder.GetDefaultReminderNextTime(new ExDateTime?(this.Item.StartTime), this.MinutesBeforeStart) != null && this.IsItemStateValid)
				{
					this.Item.OccurrencePropertyBag.UpdateMasterRecurrence();
					this.Item.LocationIdentifierHelperInstance.SetLocationIdentifier(37493U);
					Reminder.Adjust(this.Master.Reminder, actualizationTime);
				}
			}

			// Token: 0x06002B21 RID: 11041 RVA: 0x000AC0B0 File Offset: 0x000AA2B0
			protected override Reminder.ReminderInfo GetPertinentItemInfo(ExDateTime actualizationTime)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06002B22 RID: 11042 RVA: 0x000AC0B7 File Offset: 0x000AA2B7
			protected override Reminder.ReminderInfo GetNextPertinentItemInfo(ExDateTime actualizationTime)
			{
				throw new NotSupportedException();
			}
		}
	}
}
