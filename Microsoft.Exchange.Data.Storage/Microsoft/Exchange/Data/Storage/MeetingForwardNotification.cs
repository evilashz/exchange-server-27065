using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003DC RID: 988
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MeetingForwardNotification : MeetingMessageInstance, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002CE3 RID: 11491 RVA: 0x000B8149 File Offset: 0x000B6349
		internal MeetingForwardNotification(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000B8152 File Offset: 0x000B6352
		public new static MeetingForwardNotification Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<MeetingForwardNotification>(session, storeId, MeetingMessageInstanceSchema.Instance, propsToReturn);
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000B8161 File Offset: 0x000B6361
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingForwardNotification>(this);
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x000B8169 File Offset: 0x000B6369
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return MeetingForwardNotificationSchema.Instance;
			}
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000B817C File Offset: 0x000B637C
		public List<Participant> GetParticipantCollection()
		{
			List<Participant> list = new List<Participant>();
			List<BlobRecipient> forwardedAttendees = this.GetForwardedAttendees();
			if (forwardedAttendees != null)
			{
				foreach (BlobRecipient blobRecipient in forwardedAttendees)
				{
					if (blobRecipient.Participant != null)
					{
						list.Add(blobRecipient.Participant);
					}
				}
			}
			return list;
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000B81F0 File Offset: 0x000B63F0
		public void SendRumUpdate(ref CalendarItemBase originalCalendarItem)
		{
			if (originalCalendarItem == null)
			{
				return;
			}
			CalendarInconsistencyFlag inconsistencyFlag;
			if (this.MatchesOrganizerItem(ref originalCalendarItem, out inconsistencyFlag))
			{
				return;
			}
			List<BlobRecipient> mfnaddedAttendees = this.GetMFNAddedAttendees();
			IAttendeeCollection attendeeCollection = originalCalendarItem.AttendeeCollection;
			Participant organizer = originalCalendarItem.Organizer;
			List<Attendee> list = new List<Attendee>();
			MailboxSession mailboxSession = base.MailboxSession;
			foreach (BlobRecipient blobRecipient in mfnaddedAttendees)
			{
				list.Add(originalCalendarItem.AttendeeCollection.Add(blobRecipient.Participant, AttendeeType.Required, null, null, false));
			}
			if (list.Count > 0)
			{
				UpdateRumInfo rumInfo;
				if (originalCalendarItem.GlobalObjectId.IsCleanGlobalObjectId)
				{
					rumInfo = UpdateRumInfo.CreateMasterInstance(list, inconsistencyFlag);
				}
				else
				{
					rumInfo = UpdateRumInfo.CreateOccurrenceInstance(originalCalendarItem.GlobalObjectId.Date, list, inconsistencyFlag);
				}
				originalCalendarItem.SendUpdateRums(rumInfo, false);
			}
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000B82DC File Offset: 0x000B64DC
		internal static MeetingForwardNotification Create(MeetingRequest request)
		{
			MailboxSession mailboxSession = request.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new NotSupportedException();
			}
			MeetingForwardNotification meetingForwardNotification = null;
			bool flag = false;
			MeetingForwardNotification result;
			try
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId((mailboxSession.LogonType == LogonType.Transport) ? DefaultFolderType.SentItems : DefaultFolderType.Drafts);
				meetingForwardNotification = ItemBuilder.CreateNewItem<MeetingForwardNotification>(mailboxSession, defaultFolderId, request.IsSeriesMessage ? ItemCreateInfo.MeetingForwardNotificationSeriesInfo : ItemCreateInfo.MeetingForwardNotificationInfo);
				meetingForwardNotification.Load(InternalSchema.ContentConversionProperties);
				meetingForwardNotification.Initialize(request, request.IsSeriesMessage ? "IPM.MeetingMessageSeries.Notification.Forward" : "IPM.Schedule.Meeting.Notification.Forward");
				flag = true;
				result = meetingForwardNotification;
			}
			finally
			{
				if (!flag && meetingForwardNotification != null)
				{
					meetingForwardNotification.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000B8380 File Offset: 0x000B6580
		internal List<BlobRecipient> GetForwardedAttendees()
		{
			return BlobRecipientParser.ReadRecipients(this, InternalSchema.ForwardNotificationRecipients);
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000B838D File Offset: 0x000B658D
		internal List<BlobRecipient> GetMFNAddedAttendees()
		{
			return BlobRecipientParser.ReadRecipients(this, InternalSchema.MFNAddedRecipients);
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000B839A File Offset: 0x000B659A
		internal void SetForwardedAttendees(List<BlobRecipient> list)
		{
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(60277U, LastChangeAction.SetForwardedAttendees);
			BlobRecipientParser.WriteRecipients(this, InternalSchema.ForwardNotificationRecipients, list);
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x000B83BC File Offset: 0x000B65BC
		private bool IsMfnProcessed
		{
			get
			{
				CalendarProcessingSteps valueOrDefault = base.GetValueOrDefault<CalendarProcessingSteps>(InternalSchema.CalendarProcessingSteps);
				return (valueOrDefault & CalendarProcessingSteps.ProcessedMeetingForwardNotification) == CalendarProcessingSteps.ProcessedMeetingForwardNotification;
			}
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000B83E4 File Offset: 0x000B65E4
		private void Initialize(MeetingRequest request, string className)
		{
			base.Initialize();
			this.ClassName = className;
			base.Recipients.Add(request.From);
			CalendarItemBase.CopyPropertiesTo(request, this, MeetingForwardNotification.MeetingForwardNotificationProperties);
			List<BlobRecipient> list = new List<BlobRecipient>();
			foreach (Recipient recipient in request.Recipients)
			{
				BlobRecipient item = new BlobRecipient(recipient);
				list.Add(item);
			}
			this.SetForwardedAttendees(list);
			this.Subject = base.GetValueOrDefault<string>(InternalSchema.NormalizedSubjectInternal, string.Empty);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000B8488 File Offset: 0x000B6688
		private bool AddAttendee(IAttendeeCollection attendees, Participant organizer, BlobRecipient recipient, ILocationIdentifierSetter locationIdentifierSetter)
		{
			Participant participant = recipient.Participant;
			MailboxSession mailboxSession = base.MailboxSession;
			if (Participant.HasSameEmail(participant, organizer, mailboxSession, true))
			{
				return false;
			}
			foreach (Attendee attendee in attendees)
			{
				if (Participant.HasSameEmail(participant, attendee.Participant, mailboxSession, true))
				{
					return false;
				}
			}
			locationIdentifierSetter.SetLocationIdentifier(43893U);
			attendees.Add(participant, AttendeeType.Optional, null, null, false);
			return true;
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000B852C File Offset: 0x000B672C
		private bool MatchesOrganizerItem(ref CalendarItemBase organizerCalendarItem, out CalendarInconsistencyFlag inconsistencyFlag)
		{
			inconsistencyFlag = CalendarInconsistencyFlag.None;
			int appointmentSequenceNumber = organizerCalendarItem.AppointmentSequenceNumber;
			int valueOrDefault = base.GetValueOrDefault<int>(CalendarItemBaseSchema.AppointmentSequenceNumber, -1);
			if (valueOrDefault < appointmentSequenceNumber)
			{
				inconsistencyFlag = CalendarInconsistencyFlag.VersionInfo;
				return false;
			}
			ExDateTime startTime = organizerCalendarItem.StartTime;
			ExDateTime endTime = organizerCalendarItem.EndTime;
			ExDateTime valueOrDefault2 = base.GetValueOrDefault<ExDateTime>(InternalSchema.StartTime);
			ExDateTime valueOrDefault3 = base.GetValueOrDefault<ExDateTime>(InternalSchema.EndTime);
			if (!startTime.Equals(valueOrDefault2))
			{
				inconsistencyFlag = CalendarInconsistencyFlag.StartTime;
				return false;
			}
			if (!endTime.Equals(valueOrDefault3))
			{
				inconsistencyFlag = CalendarInconsistencyFlag.EndTime;
				return false;
			}
			string location = organizerCalendarItem.Location;
			string valueOrDefault4 = base.GetValueOrDefault<string>(InternalSchema.Location, string.Empty);
			if (!string.Equals(location, valueOrDefault4, StringComparison.OrdinalIgnoreCase))
			{
				inconsistencyFlag = CalendarInconsistencyFlag.Location;
				return false;
			}
			return true;
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x000B85D4 File Offset: 0x000B67D4
		protected override bool ShouldBeSentFromOrganizer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000B85D8 File Offset: 0x000B67D8
		protected override void UpdateCalendarItemInternal(ref CalendarItemBase originalCalendarItem)
		{
			if (!this.IsMfnProcessed)
			{
				List<BlobRecipient> forwardedAttendees = this.GetForwardedAttendees();
				IAttendeeCollection attendeeCollection = originalCalendarItem.AttendeeCollection;
				Participant organizer = originalCalendarItem.Organizer;
				List<BlobRecipient> list = new List<BlobRecipient>();
				foreach (BlobRecipient blobRecipient in forwardedAttendees)
				{
					bool flag = this.AddAttendee(attendeeCollection, organizer, blobRecipient, originalCalendarItem.LocationIdentifierHelperInstance);
					if (flag)
					{
						list.Add(blobRecipient);
					}
				}
				if (list.Count > 0)
				{
					BlobRecipientParser.WriteRecipients(this, InternalSchema.MFNAddedRecipients, list);
				}
				this.SetCalendarProcessingSteps(CalendarProcessingSteps.ProcessedMeetingForwardNotification);
			}
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000B8688 File Offset: 0x000B6888
		protected internal override int CompareToCalendarItem(CalendarItemBase correlatedCalendarItem)
		{
			return 1;
		}

		// Token: 0x040018EA RID: 6378
		internal static readonly StorePropertyDefinition[] MeetingForwardNotificationProperties = new StorePropertyDefinition[]
		{
			InternalSchema.OwnerAppointmentID,
			InternalSchema.GlobalObjectId,
			InternalSchema.CleanGlobalObjectId,
			InternalSchema.OwnerCriticalChangeTime,
			InternalSchema.AppointmentSequenceNumber,
			InternalSchema.Subject,
			InternalSchema.StartTime,
			InternalSchema.EndTime,
			InternalSchema.TimeZone,
			InternalSchema.IsResponseRequested,
			InternalSchema.StartRecurDate,
			InternalSchema.StartRecurTime,
			InternalSchema.AppointmentRecurring,
			InternalSchema.AppointmentRecurrenceBlob,
			InternalSchema.TimeZoneBlob,
			InternalSchema.MapiSensitivity,
			InternalSchema.Location
		};
	}
}
