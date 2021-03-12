using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003DB RID: 987
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MeetingCancellation : MeetingMessageInstance, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002CD5 RID: 11477 RVA: 0x000B7D47 File Offset: 0x000B5F47
		internal MeetingCancellation(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000B7D50 File Offset: 0x000B5F50
		internal override void Initialize()
		{
			this.Initialize("IPM.Schedule.Meeting.Canceled");
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000B7D5D File Offset: 0x000B5F5D
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingCancellation>(this);
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000B7D65 File Offset: 0x000B5F65
		public new static MeetingCancellation Bind(StoreSession session, StoreId storeId)
		{
			return MeetingCancellation.Bind(session, storeId, null);
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000B7D6F File Offset: 0x000B5F6F
		public new static MeetingCancellation Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<MeetingCancellation>(session, storeId, MeetingMessageInstanceSchema.Instance, propsToReturn);
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000B7D80 File Offset: 0x000B5F80
		public static MeetingCancellation CreateMeetingCancellation(MailboxSession mailboxSession)
		{
			MeetingCancellation meetingCancellation = ItemBuilder.CreateNewItem<MeetingCancellation>(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts), ItemCreateInfo.MeetingCancellationInfo);
			meetingCancellation.Initialize("IPM.Schedule.Meeting.Canceled");
			return meetingCancellation;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000B7DAC File Offset: 0x000B5FAC
		public static MeetingCancellation CreateMeetingCancellationSeries(MailboxSession mailboxSession)
		{
			MeetingCancellation meetingCancellation = ItemBuilder.CreateNewItem<MeetingCancellation>(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts), ItemCreateInfo.MeetingCancellationSeriesInfo);
			meetingCancellation.Initialize("IPM.MeetingMessageSeries.Canceled");
			return meetingCancellation;
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000B7DD8 File Offset: 0x000B5FD8
		public override MessageItem CreateForward(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("CreateForward");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(configuration, "configuration");
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingCancellation.CreateForward: GOID={0}", this.GlobalObjectId);
			MeetingCancellation meetingCancellation = null;
			bool flag = false;
			MessageItem result;
			try
			{
				meetingCancellation = (base.IsSeriesMessage ? MeetingCancellation.CreateMeetingCancellationSeries(session) : MeetingCancellation.CreateMeetingCancellation(session));
				ForwardCreation forwardCreation = new ForwardCreation(this, meetingCancellation, configuration);
				forwardCreation.PopulateProperties();
				meetingCancellation.AdjustAppointmentStateFlagsForForward();
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(33397U, LastChangeAction.CreateForward);
				flag = true;
				result = meetingCancellation;
			}
			finally
			{
				if (!flag && meetingCancellation != null)
				{
					meetingCancellation.Dispose();
					meetingCancellation = null;
				}
			}
			return result;
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06002CDD RID: 11485 RVA: 0x000B7E94 File Offset: 0x000B6094
		protected override bool ShouldBeSentFromOrganizer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000B7E98 File Offset: 0x000B6098
		protected override void UpdateCalendarItemInternal(ref CalendarItemBase originalCalendarItem)
		{
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingCancellation.UpdateCalendarItemInternal: GOID={0}", this.GlobalObjectId);
			CalendarItemBase calendarItemBase = originalCalendarItem;
			if (base.IsOutOfDate(calendarItemBase))
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingCancellation.UpdateCalendarItemInternal: GOID={0}; NOOP because message is out of date.", this.GlobalObjectId);
				return;
			}
			calendarItemBase = base.GetCalendarItemToUpdate(calendarItemBase);
			base.AdjustAppointmentState();
			calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(49781U);
			CalendarItemBase.CopyPropertiesTo(this, calendarItemBase, MeetingMessage.MeetingMessageProperties);
			this.CopyParticipantsToCalendarItem(calendarItemBase);
			string valueOrDefault = base.GetValueOrDefault<string>(InternalSchema.AppointmentClass);
			if (valueOrDefault != null && ObjectClass.IsDerivedClass(valueOrDefault, "IPM.Appointment"))
			{
				calendarItemBase.ClassName = valueOrDefault;
			}
			Microsoft.Exchange.Data.Storage.Item.CopyCustomPublicStrings(this, calendarItemBase);
			if (!base.IsRepairUpdateMessage)
			{
				calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(65013U);
				Body.CopyBody(this, calendarItemBase, false);
				calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(56821U);
				base.ReplaceAttachments(calendarItemBase);
			}
			calendarItemBase.FreeBusyStatus = BusyType.Free;
			calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(40437U);
			calendarItemBase[InternalSchema.AppointmentState] = base.AppointmentState;
			originalCalendarItem = calendarItemBase;
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000B7FAC File Offset: 0x000B61AC
		protected override bool CheckPreConditions(CalendarItemBase originalCalendarItem, bool shouldThrow, bool canUpdatePrincipalCalendar)
		{
			if (!base.CheckPreConditions(originalCalendarItem, shouldThrow, canUpdatePrincipalCalendar))
			{
				return false;
			}
			bool flag = (originalCalendarItem != null) ? originalCalendarItem.IsOrganizer() : base.IsMailboxOwnerTheSender();
			if (!flag)
			{
				return true;
			}
			if (shouldThrow)
			{
				throw new InvalidOperationException(ServerStrings.ExOrganizerCannotCallUpdateCalendarItem);
			}
			return false;
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000B7FF1 File Offset: 0x000B61F1
		protected override AppointmentStateFlags CalculatedAppointmentState()
		{
			this.CheckDisposed("CalculatedAppointmentState");
			return base.CalculatedAppointmentState() | AppointmentStateFlags.Cancelled;
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000B8008 File Offset: 0x000B6208
		private void CopyParticipantsToCalendarItem(CalendarItemBase calendarItem)
		{
			this.CheckDisposed("CopyParticipantsToCalendarItem");
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(44533U, LastChangeAction.CopyParticipantsToCalendarItem);
			IAttendeeCollection attendeeCollection = calendarItem.AttendeeCollection;
			calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(60917U);
			attendeeCollection.Clear();
			if (base.From != null)
			{
				calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(36341U);
				attendeeCollection.Add(base.From, AttendeeType.Required, null, null, false).RecipientFlags = (RecipientFlags.Sendable | RecipientFlags.Organizer);
			}
			foreach (Recipient recipient in base.Recipients)
			{
				calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(52725U);
				attendeeCollection.Add(recipient.Participant, Attendee.RecipientItemTypeToAttendeeType(recipient.RecipientItemType), null, null, false);
			}
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000B8110 File Offset: 0x000B6310
		private void Initialize(string itemClass)
		{
			base.Initialize();
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(39029U);
			this[InternalSchema.ItemClass] = itemClass;
			this[InternalSchema.IconIndex] = IconIndex.AppointmentMeetCancel;
		}
	}
}
