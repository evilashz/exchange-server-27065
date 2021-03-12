using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Eas.Model.Response.Calendar;
using Microsoft.Exchange.Connections.Eas.Model.Response.ItemOperations;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000011 RID: 17
	internal class EasFxCalendarMessage : IMessage, IDisposable
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00006C4A File Offset: 0x00004E4A
		public EasFxCalendarMessage(Properties calendarItemProperties)
		{
			this.propertyBag = EasFxCalendarMessage.CreatePropertyBag(calendarItemProperties);
			this.recipients = EasFxCalendarMessage.CreateRecipientsCollection(calendarItemProperties.Attendees);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006C6F File Offset: 0x00004E6F
		IPropertyBag IMessage.PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006C77 File Offset: 0x00004E77
		bool IMessage.IsAssociated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006C7A File Offset: 0x00004E7A
		IEnumerable<IRecipient> IMessage.GetRecipients()
		{
			return this.recipients;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006C82 File Offset: 0x00004E82
		IRecipient IMessage.CreateRecipient()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006C89 File Offset: 0x00004E89
		void IMessage.RemoveRecipient(int rowId)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006D30 File Offset: 0x00004F30
		IEnumerable<IAttachmentHandle> IMessage.GetAttachments()
		{
			yield break;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006D4D File Offset: 0x00004F4D
		IAttachment IMessage.CreateAttachment()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006D54 File Offset: 0x00004F54
		void IMessage.Save()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006D5B File Offset: 0x00004F5B
		void IMessage.SetLongTermId(StoreLongTermId longTermId)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006D62 File Offset: 0x00004F62
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006D64 File Offset: 0x00004F64
		private static IEnumerable<IRecipient> CreateRecipientsCollection(List<Attendee> attendees)
		{
			List<IRecipient> list = new List<IRecipient>();
			for (int i = 0; i < attendees.Count; i++)
			{
				Attendee attendee = attendees[i];
				FxPropertyBag fxPropertyBag = new FxPropertyBag(new FxSession(SyncCalendarUtils.AttendeePropertyTagsToNamedProperties));
				fxPropertyBag[SyncCalendarUtils.RowId] = i;
				int attendeeStatus = attendee.AttendeeStatus;
				EasFxCalendarMessage.SetOrThrowIfInvalid<ResponseType>(fxPropertyBag, SyncCalendarUtils.RecipientTrackStatus, (ResponseType)attendeeStatus, attendeeStatus);
				int attendeeType = attendee.AttendeeType;
				EasFxCalendarMessage.SetOrThrowIfInvalid<AttendeeType>(fxPropertyBag, SyncCalendarUtils.RecipientType, (AttendeeType)attendeeType, attendeeType);
				fxPropertyBag[SyncCalendarUtils.EmailAddress] = attendee.Email;
				fxPropertyBag[SyncCalendarUtils.DisplayName] = attendee.Name;
				EasFxCalendarRecipient item = new EasFxCalendarRecipient(fxPropertyBag);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006E18 File Offset: 0x00005018
		private static FxPropertyBag CreatePropertyBag(Properties calendarItemProperties)
		{
			FxPropertyBag fxPropertyBag = new FxPropertyBag(new FxSession(SyncCalendarUtils.CalendarItemPropertyTagsToNamedProperties));
			ExDateTime exDateTime = SyncCalendarUtils.ToUtcExDateTime(calendarItemProperties.StartTime);
			ExDateTime exDateTime2 = SyncCalendarUtils.ToUtcExDateTime(calendarItemProperties.EndTime);
			if (exDateTime > exDateTime2)
			{
				throw new EasFetchFailedPermanentException(new LocalizedString(string.Format("Start {0} is greater than end {1}.", exDateTime, exDateTime2)));
			}
			fxPropertyBag[SyncCalendarUtils.Start] = exDateTime;
			fxPropertyBag[SyncCalendarUtils.End] = exDateTime2;
			fxPropertyBag[SyncCalendarUtils.GlobalObjectId] = new GlobalObjectId(calendarItemProperties.Uid).Bytes;
			ExTimeZone exTimeZone = SyncCalendarUtils.ToExTimeZone(calendarItemProperties.TimeZone);
			fxPropertyBag[SyncCalendarUtils.TimeZoneBlob] = O11TimeZoneFormatter.GetTimeZoneBlob(exTimeZone);
			int busyStatus = calendarItemProperties.BusyStatus;
			EasFxCalendarMessage.SetOrThrowIfInvalid<BusyType>(fxPropertyBag, SyncCalendarUtils.BusyStatus, (BusyType)busyStatus, busyStatus);
			int sensitivity = calendarItemProperties.Sensitivity;
			EasFxCalendarMessage.SetOrThrowIfInvalid<Sensitivity>(fxPropertyBag, SyncCalendarUtils.Sensitivity, (Sensitivity)sensitivity, sensitivity);
			int meetingStatus = calendarItemProperties.MeetingStatus;
			EasFxCalendarMessage.SetOrThrowIfInvalid<AppointmentStateFlags>(fxPropertyBag, SyncCalendarUtils.MeetingStatus, (AppointmentStateFlags)meetingStatus, meetingStatus);
			fxPropertyBag[PropertyTag.MessageClass] = "IPM.Appointment";
			fxPropertyBag[PropertyTag.Subject] = calendarItemProperties.CalendarSubject;
			fxPropertyBag[SyncCalendarUtils.AllDayEvent] = calendarItemProperties.AllDayEvent;
			fxPropertyBag[SyncCalendarUtils.Location] = calendarItemProperties.Location;
			fxPropertyBag[SyncCalendarUtils.Reminder] = calendarItemProperties.Reminder;
			fxPropertyBag[PropertyTag.Body] = calendarItemProperties.Body.Data;
			fxPropertyBag[SyncCalendarUtils.SentRepresentingName] = calendarItemProperties.OrganizerName;
			fxPropertyBag[SyncCalendarUtils.SentRepresentingEmailAddress] = calendarItemProperties.OrganizerEmail;
			fxPropertyBag[SyncCalendarUtils.ResponseType] = calendarItemProperties.ResponseType;
			Recurrence recurrence = calendarItemProperties.Recurrence;
			if (recurrence != null)
			{
				fxPropertyBag[SyncCalendarUtils.AppointmentRecurrenceBlob] = SyncCalendarUtils.ToRecurrenceBlob(calendarItemProperties, exDateTime, exDateTime2, exTimeZone);
			}
			return fxPropertyBag;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006FE8 File Offset: 0x000051E8
		private static void SetOrThrowIfInvalid<T>(FxPropertyBag propertyBag, PropertyTag propertyTag, T typedValue, int valueToSet) where T : struct
		{
			if (!EnumValidator<T>.IsValidValue(typedValue))
			{
				throw new EasFetchFailedPermanentException(new LocalizedString(propertyTag + ": " + typedValue));
			}
			propertyBag[propertyTag] = valueToSet;
		}

		// Token: 0x0400005D RID: 93
		private readonly IPropertyBag propertyBag;

		// Token: 0x0400005E RID: 94
		private readonly IEnumerable<IRecipient> recipients;
	}
}
