using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ReliableActions;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors
{
	// Token: 0x02000092 RID: 146
	internal static class CalendarItemAccessors
	{
		// Token: 0x06000367 RID: 871 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		private static DelegatedStoragePropertyAccessor<ICalendarItem, bool> CreateSeriesMasterDataPropagationOperationAccessor(PropertyChangeMetadataProcessingFlags propertyChangeMetadataProcessingFlag, uint lid)
		{
			return new DelegatedStoragePropertyAccessor<ICalendarItem, bool>(delegate(ICalendarItem container, out bool value)
			{
				PropertyChangeMetadataProcessingFlags valueOrDefault = container.GetValueOrDefault<PropertyChangeMetadataProcessingFlags>(CalendarItemSchema.PropertyChangeMetadataProcessingFlags, PropertyChangeMetadataProcessingFlags.None);
				value = ((valueOrDefault & propertyChangeMetadataProcessingFlag) == propertyChangeMetadataProcessingFlag);
				return true;
			}, delegate(ICalendarItem container, bool value)
			{
				PropertyChangeMetadataProcessingFlags propertyChangeMetadataProcessingFlags = container.GetValueOrDefault<PropertyChangeMetadataProcessingFlags>(CalendarItemSchema.PropertyChangeMetadataProcessingFlags, PropertyChangeMetadataProcessingFlags.None);
				if (value)
				{
					propertyChangeMetadataProcessingFlags |= propertyChangeMetadataProcessingFlag;
				}
				else
				{
					propertyChangeMetadataProcessingFlags &= ~propertyChangeMetadataProcessingFlag;
				}
				LocationIdentifierHelper locationIdentifierHelperInstance = container.LocationIdentifierHelperInstance;
				if (locationIdentifierHelperInstance != null)
				{
					locationIdentifierHelperInstance.SetLocationIdentifier(lid);
				}
				container[CalendarItemSchema.PropertyChangeMetadataProcessingFlags] = propertyChangeMetadataProcessingFlags;
			}, null, null, new PropertyDefinition[0]);
		}

		// Token: 0x04000109 RID: 265
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, ExDateTime> Birthday = new DefaultStoragePropertyAccessor<ICalendarItemBase, ExDateTime>(CalendarItemBaseSchema.Birthday, false);

		// Token: 0x0400010A RID: 266
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, string> BirthdayContactAttributionDisplayName = new DefaultStoragePropertyAccessor<ICalendarItemBase, string>(CalendarItemBaseSchema.BirthdayContactAttributionDisplayName, false);

		// Token: 0x0400010B RID: 267
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, StoreObjectId> BirthdayContactId = new DefaultStoragePropertyAccessor<ICalendarItemBase, StoreObjectId>(CalendarItemBaseSchema.BirthdayContactId, false);

		// Token: 0x0400010C RID: 268
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, PersonId> BirthdayContactPersonId = new DefaultStoragePropertyAccessor<ICalendarItemBase, PersonId>(CalendarItemBaseSchema.BirthdayContactPersonId, false);

		// Token: 0x0400010D RID: 269
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, bool> IsBirthdayContactWritable = new DefaultStoragePropertyAccessor<ICalendarItemBase, bool>(CalendarItemBaseSchema.IsBirthdayContactWritable, false);

		// Token: 0x0400010E RID: 270
		public static readonly IStoragePropertyAccessor<ICalendarItem, int> InstanceCreationIndex = new DefaultStoreObjectPropertyAccessor<ICalendarItem, int>(39036U, CalendarItemSchema.InstanceCreationIndex, false);

		// Token: 0x0400010F RID: 271
		public static readonly IStoragePropertyAccessor<ICalendarItem, bool> MarkAllPropagatedPropertiesAsException = CalendarItemAccessors.CreateSeriesMasterDataPropagationOperationAccessor(PropertyChangeMetadataProcessingFlags.MarkAllPropagatedPropertiesAsException, 46332U);

		// Token: 0x04000110 RID: 272
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, PropertyChangeMetadata> PropertyChangeMetadata = new DefaultStoragePropertyAccessor<ICalendarItemBase, PropertyChangeMetadata>(CalendarItemInstanceSchema.PropertyChangeMetadata, true);

		// Token: 0x04000111 RID: 273
		public static readonly IStoragePropertyAccessor<ICalendarItem, Recurrence> Recurrence = new DelegatedStoragePropertyAccessor<ICalendarItem, Recurrence>(delegate(ICalendarItem container, out Recurrence value)
		{
			value = container.Recurrence;
			return value != null;
		}, delegate(ICalendarItem item, Recurrence recurrence)
		{
			item.Recurrence = recurrence;
		}, null, null, new PropertyDefinition[0]);

		// Token: 0x04000112 RID: 274
		public static readonly IStoragePropertyAccessor<ICalendarItem, bool> SeriesMasterDataPropagationOperation = CalendarItemAccessors.CreateSeriesMasterDataPropagationOperationAccessor(PropertyChangeMetadataProcessingFlags.SeriesMasterDataPropagationOperation, 44572U);

		// Token: 0x04000113 RID: 275
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, IList<Attendee>> Attendees = new StorageAttendeesPropertyAccessor();

		// Token: 0x04000114 RID: 276
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, CalendarItemType> CalendarItemType = new DefaultStoragePropertyAccessor<ICalendarItemBase, CalendarItemType>(CalendarItemBaseSchema.CalendarItemType, false);

		// Token: 0x04000115 RID: 277
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, bool> DisallowNewTimeProposal = new DefaultStoragePropertyAccessor<ICalendarItemBase, bool>(CalendarItemBaseSchema.DisallowNewTimeProposal, false);

		// Token: 0x04000116 RID: 278
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, ExDateTime> EndTime = new DelegatedStoragePropertyAccessor<ICalendarItemBase, ExDateTime>(delegate(ICalendarItemBase container, out ExDateTime value)
		{
			value = container.EndTime;
			return true;
		}, delegate(ICalendarItemBase calendarItemBase, ExDateTime time)
		{
			calendarItemBase.EndTime = time;
		}, delegate(IDictionary<PropertyDefinition, int> indices, IList values, out ExDateTime value)
		{
			int index;
			object obj;
			if ((indices.TryGetValue(CalendarItemInstanceSchema.EndTime, out index) && (obj = values[index]) is ExDateTime) || (indices.TryGetValue(CalendarItemBaseSchema.ClipEndTime, out index) && (obj = values[index]) is ExDateTime))
			{
				value = (ExDateTime)obj;
				return true;
			}
			value = default(ExDateTime);
			return false;
		}, Microsoft.Exchange.Data.Storage.PropertyChangeMetadata.PropertyGroup.EndTime, new PropertyDefinition[]
		{
			CalendarItemInstanceSchema.EndTime,
			CalendarItemBaseSchema.ClipEndTime
		});

		// Token: 0x04000117 RID: 279
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, ExTimeZone> EndTimeZone = new DefaultStoragePropertyAccessor<ICalendarItemBase, ExTimeZone>(CalendarItemBaseSchema.EndTimeZone, false);

		// Token: 0x04000118 RID: 280
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, BusyType> FreeBusyStatus = new DefaultStoreObjectPropertyAccessor<ICalendarItemBase, BusyType>(50364U, CalendarItemBaseSchema.FreeBusyStatus, false);

		// Token: 0x04000119 RID: 281
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, GlobalObjectId> GlobalObjectId = new DelegatedStoragePropertyAccessor<ICalendarItemBase, GlobalObjectId>(delegate(ICalendarItemBase container, out GlobalObjectId value)
		{
			value = container.GlobalObjectId;
			return value != null;
		}, delegate(ICalendarItemBase calendarItemBase, GlobalObjectId goid)
		{
			if (calendarItemBase.IsNew)
			{
				calendarItemBase[CalendarItemBaseSchema.GlobalObjectId] = goid.Bytes;
				return;
			}
			GlobalObjectId globalObjectId = calendarItemBase.GlobalObjectId;
			if (!object.Equals(globalObjectId, goid))
			{
				throw new InvalidOperationException("GOID can be changed only on new items.");
			}
		}, null, null, new PropertyDefinition[0]);

		// Token: 0x0400011A RID: 282
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, bool> HasAttendees = new DelegatedStoragePropertyAccessor<ICalendarItemBase, bool>(delegate(ICalendarItemBase container, out bool value)
		{
			object obj = container.TryGetProperty(CalendarItemBaseSchema.HasAttendees);
			if (obj is bool)
			{
				value = (bool)obj;
			}
			else
			{
				if (container.AttendeeCollection == null)
				{
					value = false;
					return false;
				}
				value = container.AttendeeCollection.Any((Attendee attendee) => !attendee.IsOrganizer);
			}
			return true;
		}, null, delegate(IDictionary<PropertyDefinition, int> indices, IList values, out bool value)
		{
			int index;
			object obj;
			if (indices.TryGetValue(CalendarItemBaseSchema.HasAttendees, out index) && (obj = values[index]) is bool)
			{
				value = (bool)obj;
				return true;
			}
			if (indices.TryGetValue(CalendarItemBaseSchema.IsMeeting, out index) && (obj = values[index]) is bool)
			{
				value = (bool)obj;
				return true;
			}
			value = false;
			return false;
		}, Microsoft.Exchange.Data.Storage.PropertyChangeMetadata.PropertyGroup.IsMeeting, new PropertyDefinition[]
		{
			CalendarItemBaseSchema.HasAttendees,
			CalendarItemBaseSchema.IsMeeting
		});

		// Token: 0x0400011B RID: 283
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, BusyType> IntendedFreeBusyStatus = new DefaultStoragePropertyAccessor<ICalendarItemBase, BusyType>(CalendarItemBaseSchema.IntendedFreeBusyStatus, true);

		// Token: 0x0400011C RID: 284
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, bool> IsAllDay = new DefaultStoragePropertyAccessor<ICalendarItemBase, bool>(CalendarItemBaseSchema.MapiIsAllDayEvent, false);

		// Token: 0x0400011D RID: 285
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, bool> IsCancelled = new DelegatedStoragePropertyAccessor<ICalendarItemBase, bool>(delegate(ICalendarItemBase container, out bool value)
		{
			value = container.IsCancelled;
			return true;
		}, null, delegate(IDictionary<PropertyDefinition, int> indices, IList values, out bool value)
		{
			int index;
			object obj;
			if (indices.TryGetValue(CalendarItemBaseSchema.AppointmentState, out index) && (obj = values[index]) is int)
			{
				AppointmentStateFlags appointmentState = (AppointmentStateFlags)obj;
				value = CalendarItemBase.IsAppointmentStateCancelled(appointmentState);
				return true;
			}
			value = false;
			return false;
		}, Microsoft.Exchange.Data.Storage.PropertyChangeMetadata.PropertyGroup.IsCancelled, new PropertyDefinition[]
		{
			CalendarItemBaseSchema.AppointmentState
		});

		// Token: 0x0400011E RID: 286
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, bool> IsDraft = new DefaultStoragePropertyAccessor<ICalendarItemBase, bool>(CalendarItemBaseSchema.IsDraft, false);

		// Token: 0x0400011F RID: 287
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, bool> IsOnlineMeeting = new DefaultStoragePropertyAccessor<ICalendarItemBase, bool>(CalendarItemBaseSchema.IsOnlineMeeting, false);

		// Token: 0x04000120 RID: 288
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, bool> IsOrganizer = new DefaultStoragePropertyAccessor<ICalendarItemBase, bool>(CalendarItemBaseSchema.IsOrganizer, false);

		// Token: 0x04000121 RID: 289
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, bool> IsReceivedAccessor = new DelegatedStoragePropertyAccessor<ICalendarItemBase, bool>(delegate(ICalendarItemBase container, out bool isReceived)
		{
			AppointmentStateFlags valueOrDefault = container.GetValueOrDefault<AppointmentStateFlags>(CalendarItemBaseSchema.AppointmentState, AppointmentStateFlags.None);
			isReceived = ((valueOrDefault & AppointmentStateFlags.Received) == AppointmentStateFlags.Received);
			return true;
		}, delegate(ICalendarItemBase calendarItemBase, bool isReceived)
		{
			AppointmentStateFlags appointmentStateFlags = calendarItemBase.GetValueOrDefault<AppointmentStateFlags>(CalendarItemBaseSchema.AppointmentState, AppointmentStateFlags.None);
			if (isReceived)
			{
				appointmentStateFlags |= AppointmentStateFlags.Received;
			}
			else
			{
				appointmentStateFlags &= ~AppointmentStateFlags.Received;
			}
			calendarItemBase[CalendarItemBaseSchema.AppointmentState] = appointmentStateFlags;
		}, null, null, new PropertyDefinition[0]);

		// Token: 0x04000122 RID: 290
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, Guid?> LastExecutedInteropAction = new DefaultStoreObjectPropertyAccessor<ICalendarItemBase, Guid?>(63004U, CalendarItemSchema.LastExecutedCalendarInteropAction, false);

		// Token: 0x04000123 RID: 291
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, Location> Location = new StorageLocationPropertyAccessor();

		// Token: 0x04000124 RID: 292
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, string> OnlineMeetingConfLink = new DefaultStoragePropertyAccessor<ICalendarItemBase, string>(CalendarItemBaseSchema.OnlineMeetingConfLink, false);

		// Token: 0x04000125 RID: 293
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, string> OnlineMeetingExternalLink = new DefaultStoragePropertyAccessor<ICalendarItemBase, string>(CalendarItemBaseSchema.OnlineMeetingExternalLink, false);

		// Token: 0x04000126 RID: 294
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, Organizer> Organizer = new DelegatedStoragePropertyAccessor<ICalendarItemBase, Organizer>(delegate(ICalendarItemBase container, out Organizer value)
		{
			ParticipantRoutingTypeConverter routingTypeConverter = new ParticipantRoutingTypeConverter(container.Session);
			OrganizerConverter organizerConverter = new OrganizerConverter(routingTypeConverter);
			value = organizerConverter.Convert(container.Organizer);
			return value != null;
		}, null, delegate(IDictionary<PropertyDefinition, int> indices, IList values, out Organizer value)
		{
			int index;
			object obj;
			if (indices.TryGetValue(CalendarItemBaseSchema.Organizer, out index) && (obj = values[index]) is Participant)
			{
				Participant value2 = (Participant)obj;
				ParticipantRoutingTypeConverter.PassThru singletonInstance = ParticipantRoutingTypeConverter.PassThru.SingletonInstance;
				OrganizerConverter organizerConverter = new OrganizerConverter(singletonInstance);
				value = organizerConverter.Convert(value2);
				return true;
			}
			value = null;
			return false;
		}, null, new PropertyDefinition[]
		{
			CalendarItemBaseSchema.Organizer
		});

		// Token: 0x04000127 RID: 295
		public static readonly DefaultStoreObjectPropertyAccessor<ICalendarItemBase, ExDateTime> ReplyTime = new DefaultStoreObjectPropertyAccessor<ICalendarItemBase, ExDateTime>(42524U, CalendarItemBaseSchema.AppointmentReplyTime, false);

		// Token: 0x04000128 RID: 296
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, bool> ResponseRequested = new DefaultStoreObjectPropertyAccessor<ICalendarItemBase, bool>(54812U, ItemSchema.IsResponseRequested, false);

		// Token: 0x04000129 RID: 297
		public static readonly DelegatedStoragePropertyAccessor<ICalendarItemBase, ResponseType> ResponseType = new DelegatedStoragePropertyAccessor<ICalendarItemBase, ResponseType>(delegate(ICalendarItemBase container, out ResponseType value)
		{
			value = container.ResponseType;
			return true;
		}, delegate(ICalendarItemBase calendarItemBase, ResponseType type)
		{
			calendarItemBase.ResponseType = type;
		}, delegate(IDictionary<PropertyDefinition, int> indices, IList values, out ResponseType value)
		{
			int index;
			object obj;
			if (indices.TryGetValue(CalendarItemBaseSchema.ResponseType, out index) && (obj = values[index]) is ResponseType)
			{
				value = (ResponseType)obj;
				return true;
			}
			value = Microsoft.Exchange.Data.Storage.ResponseType.None;
			return false;
		}, Microsoft.Exchange.Data.Storage.PropertyChangeMetadata.PropertyGroup.Response, new PropertyDefinition[]
		{
			CalendarItemBaseSchema.ResponseType
		});

		// Token: 0x0400012A RID: 298
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, string> ClientId = new DefaultStoreObjectPropertyAccessor<ICalendarItemBase, string>(38428U, CalendarItemBaseSchema.EventClientId, false);

		// Token: 0x0400012B RID: 299
		public static readonly IStoragePropertyAccessor<ICalendarItemSeries, int> SeriesCreationHash = new DefaultStoreObjectPropertyAccessor<ICalendarItemSeries, int>(53372U, CalendarItemSeriesSchema.SeriesCreationHash, false);

		// Token: 0x0400012C RID: 300
		public static readonly IStoragePropertyAccessor<ICalendarItemSeries, int> SeriesSequenceNumber = new DefaultStoreObjectPropertyAccessor<ICalendarItemSeries, int>(52220U, CalendarItemBaseSchema.AppointmentSequenceNumber, false);

		// Token: 0x0400012D RID: 301
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, string> SeriesId = new DefaultStoragePropertyAccessor<ICalendarItemBase, string>(CalendarItemBaseSchema.SeriesId, false);

		// Token: 0x0400012E RID: 302
		public static readonly IStoragePropertyAccessor<ICalendarItem, string> SeriesMasterId = new DefaultStoragePropertyAccessor<ICalendarItem, string>(CalendarItemSchema.SeriesMasterId, false);

		// Token: 0x0400012F RID: 303
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, ExDateTime> StartTime = new DelegatedStoragePropertyAccessor<ICalendarItemBase, ExDateTime>(delegate(ICalendarItemBase container, out ExDateTime value)
		{
			value = container.StartTime;
			return true;
		}, delegate(ICalendarItemBase calendarItemBase, ExDateTime time)
		{
			calendarItemBase.StartTime = time;
		}, delegate(IDictionary<PropertyDefinition, int> indices, IList values, out ExDateTime value)
		{
			int index;
			object obj;
			if ((indices.TryGetValue(CalendarItemInstanceSchema.StartTime, out index) && (obj = values[index]) is ExDateTime) || (indices.TryGetValue(CalendarItemBaseSchema.ClipStartTime, out index) && (obj = values[index]) is ExDateTime))
			{
				value = (ExDateTime)obj;
				return true;
			}
			value = default(ExDateTime);
			return false;
		}, Microsoft.Exchange.Data.Storage.PropertyChangeMetadata.PropertyGroup.StartTime, new PropertyDefinition[]
		{
			CalendarItemInstanceSchema.StartTime,
			CalendarItemBaseSchema.ClipStartTime
		});

		// Token: 0x04000130 RID: 304
		public static readonly IStoragePropertyAccessor<ICalendarItemBase, ExTimeZone> StartTimeZone = new DefaultStoragePropertyAccessor<ICalendarItemBase, ExTimeZone>(CalendarItemBaseSchema.StartTimeZone, false);

		// Token: 0x04000131 RID: 305
		public static readonly IStoragePropertyAccessor<ICalendarItemSeries, ActionInfo[]> CalendarInteropActionQueue = new DefaultStoreObjectPropertyAccessor<ICalendarItemSeries, ActionInfo[]>(43548U, CalendarItemSeriesSchema.CalendarInteropActionQueue, false);

		// Token: 0x04000132 RID: 306
		public static readonly IStoragePropertyAccessor<ICalendarItemSeries, bool> CalendarInteropActionQueueHasData = new DefaultStoragePropertyAccessor<ICalendarItemSeries, bool>(CalendarItemSeriesSchema.CalendarInteropActionQueueHasData, false);
	}
}
