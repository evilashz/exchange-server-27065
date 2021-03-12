using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x02000071 RID: 113
	internal class AttendeeConverter : ParticipantConverter<Attendee, AttendeeParticipantWrapper, Attendee>
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0000B0AC File Offset: 0x000092AC
		public AttendeeConverter(IResponseTypeConverter responseTypeConverter, IAttendeeTypeConverter attendeeTypeConverter, ICalendarItemBase calendarItem) : base(new ParticipantRoutingTypeConverter(calendarItem.AssertNotNull("calendarItem").Session))
		{
			this.includeStatus = calendarItem.IsOrganizer();
			this.responseTypeConverter = responseTypeConverter;
			this.attendeeTypeConverter = attendeeTypeConverter;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000B0E3 File Offset: 0x000092E3
		protected virtual bool IncludeStatus
		{
			get
			{
				return this.includeStatus;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B0EC File Offset: 0x000092EC
		public void ToXso(IList<Attendee> attendees, ICalendarItemBase calendarItem)
		{
			if (calendarItem == null)
			{
				throw new ExArgumentNullException("calendarItem");
			}
			if (calendarItem.IsOrganizer())
			{
				IAttendeeCollection attendeeCollection = calendarItem.AttendeeCollection;
				attendeeCollection.Clear();
				AttendeeConverter.StorageAttendeeData[] array;
				Participant[] array2;
				if (this.TryGetAttendeesData(calendarItem.Session, attendees, out array, out array2))
				{
					for (int i = 0; i < array2.Length; i++)
					{
						AttendeeConverter.StorageAttendeeData storageAttendeeData = array[i];
						calendarItem.AttendeeCollection.Add(array2[i], storageAttendeeData.AttendeeType, storageAttendeeData.ResponseType, storageAttendeeData.ReplyTime, true);
					}
				}
				if (calendarItem.AttendeeCollection.Count > 0)
				{
					calendarItem.IsMeeting = true;
				}
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B185 File Offset: 0x00009385
		protected override AttendeeParticipantWrapper WrapStorageType(Attendee value)
		{
			return new AttendeeParticipantWrapper(value);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B190 File Offset: 0x00009390
		protected override Attendee CopyFromParticipant(Participant value, Attendee original)
		{
			Attendee attendee = base.CopyFromParticipant(value, original);
			if (attendee != null)
			{
				attendee.Type = this.attendeeTypeConverter.Convert(original.AttendeeType);
				if (this.IncludeStatus)
				{
					this.CopyStatusProperties(original, attendee);
				}
			}
			return attendee;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B1D4 File Offset: 0x000093D4
		protected virtual void CopyStatusProperties(Attendee source, Attendee destination)
		{
			destination.Status = new ResponseStatus
			{
				Response = this.responseTypeConverter.Convert(source.ResponseType)
			};
			ExDateTime time;
			if (AttendeeAccessors.ReplyTime.TryGetValue(source, out time))
			{
				destination.Status.Time = time;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B220 File Offset: 0x00009420
		protected virtual AttendeeConverter.StorageAttendeeData GetAttendeeData(Attendee attendee, out Participant participant)
		{
			if (attendee == null)
			{
				throw new ExArgumentNullException("attendee");
			}
			attendee.ThrowIfPropertyNotSet(attendee.Schema.TypeProperty);
			string routingType = base.GetRoutingType(attendee);
			participant = new Participant(attendee.Name, attendee.EmailAddress, routingType);
			AttendeeConverter.StorageAttendeeData result = new AttendeeConverter.StorageAttendeeData
			{
				AttendeeType = this.attendeeTypeConverter.Convert(attendee.Type)
			};
			if (attendee.IsPropertySet(attendee.Schema.StatusProperty) && attendee.Status != null)
			{
				ResponseStatus status = attendee.Status;
				if (status.IsPropertySet(status.Schema.ResponseProperty))
				{
					result.ResponseType = new ResponseType?(this.responseTypeConverter.Convert(status.Response));
				}
				if (status.IsPropertySet(status.Schema.TimeProperty))
				{
					result.ReplyTime = new ExDateTime?(status.Time);
				}
			}
			return result;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000B304 File Offset: 0x00009504
		protected virtual bool TryGetAttendeesData(IStoreSession session, IList<Attendee> attendees, out AttendeeConverter.StorageAttendeeData[] data, out Participant[] convertedParticipants)
		{
			if (attendees == null)
			{
				data = null;
				convertedParticipants = null;
				return false;
			}
			int count = attendees.Count;
			data = new AttendeeConverter.StorageAttendeeData[count];
			if (count == 0)
			{
				convertedParticipants = new Participant[0];
				return false;
			}
			Participant[] array = new Participant[count];
			for (int i = 0; i < count; i++)
			{
				try
				{
					data[i] = this.GetAttendeeData(attendees[i], out array[i]);
				}
				catch (ExArgumentNullException innerException)
				{
					throw new InvalidRequestException(CalendaringStrings.ErrorInvalidAttendee, innerException);
				}
			}
			convertedParticipants = this.RoutingTypeConverter.ConvertToStorage(array);
			return true;
		}

		// Token: 0x040000D4 RID: 212
		private readonly bool includeStatus;

		// Token: 0x040000D5 RID: 213
		private readonly IAttendeeTypeConverter attendeeTypeConverter;

		// Token: 0x040000D6 RID: 214
		private readonly IResponseTypeConverter responseTypeConverter;

		// Token: 0x02000072 RID: 114
		public struct StorageAttendeeData
		{
			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000300 RID: 768 RVA: 0x0000B3A4 File Offset: 0x000095A4
			// (set) Token: 0x06000301 RID: 769 RVA: 0x0000B3AC File Offset: 0x000095AC
			public Microsoft.Exchange.Data.Storage.AttendeeType AttendeeType { get; set; }

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000302 RID: 770 RVA: 0x0000B3B5 File Offset: 0x000095B5
			// (set) Token: 0x06000303 RID: 771 RVA: 0x0000B3BD File Offset: 0x000095BD
			public ExDateTime? ReplyTime { get; set; }

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x06000304 RID: 772 RVA: 0x0000B3C6 File Offset: 0x000095C6
			// (set) Token: 0x06000305 RID: 773 RVA: 0x0000B3CE File Offset: 0x000095CE
			public ResponseType? ResponseType { get; set; }
		}
	}
}
