using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets.Commands;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000040 RID: 64
	internal abstract class CreateSeriesInternalBase : EntityCommand<Events, Event>
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000065AE File Offset: 0x000047AE
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000065B6 File Offset: 0x000047B6
		internal string ClientId { get; set; }

		// Token: 0x06000180 RID: 384 RVA: 0x000065C0 File Offset: 0x000047C0
		internal static IEnumerable<Event> CreateSeriesInstances<T>(Event master, Event masterChange, EventDataProvider eventDataProvider, IDictionary<T, Event> occurrencesAlreadyCreated, Func<IEventInternal, T> selectKey, Action<IEventInternal> prepareOccurrence = null)
		{
			List<Event> list = new List<Event>();
			if (masterChange == null || masterChange.Occurrences == null)
			{
				return list;
			}
			bool flag = occurrencesAlreadyCreated.Count > 0;
			foreach (Event @event in masterChange.Occurrences)
			{
				if (prepareOccurrence != null)
				{
					prepareOccurrence(@event);
				}
				if (!flag || !occurrencesAlreadyCreated.ContainsKey(selectKey(@event)))
				{
					master.CopyMasterPropertiesTo(@event, true, null, true);
					Event item = eventDataProvider.Create(@event);
					list.Add(item);
				}
			}
			list.AddRange(occurrencesAlreadyCreated.Values);
			return list;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006670 File Offset: 0x00004870
		internal Event SendMessagesForSeries(Event masterForInstanceCreation, int seriesSequenceNumber, string occurrencesViewPropertiesBlob)
		{
			if (!masterForInstanceCreation.IsDraft)
			{
				SeriesEventDataProvider seriesEventDataProvider = this.Scope.SeriesEventDataProvider;
				using (ICalendarItemBase calendarItemBase = seriesEventDataProvider.BindToWrite(masterForInstanceCreation.StoreId, masterForInstanceCreation.ChangeKey))
				{
					if (this.ShouldSendMeetingRequest(calendarItemBase))
					{
						calendarItemBase.SendMeetingMessages(true, new int?(seriesSequenceNumber), false, true, occurrencesViewPropertiesBlob, null);
						calendarItemBase.Load();
						return seriesEventDataProvider.ConvertToEntity(calendarItemBase);
					}
				}
				return masterForInstanceCreation;
			}
			return masterForInstanceCreation;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000066F4 File Offset: 0x000048F4
		internal void SendMessagesForInstances(IEnumerable<Event> occurrences, int seriesSequenceNumber, byte[] masterGoid)
		{
			SeriesEventDataProvider seriesEventDataProvider = this.Scope.SeriesEventDataProvider;
			foreach (Event @event in occurrences)
			{
				using (ICalendarItemBase calendarItemBase = seriesEventDataProvider.BindToWrite(@event.StoreId, @event.ChangeKey))
				{
					if (this.ShouldSendMeetingRequest(calendarItemBase))
					{
						calendarItemBase.SendMeetingMessages(true, new int?(seriesSequenceNumber), false, true, null, masterGoid);
					}
				}
			}
		}

		// Token: 0x06000183 RID: 387
		protected abstract int CalculateSeriesCreationHash(Event master);

		// Token: 0x06000184 RID: 388 RVA: 0x00006788 File Offset: 0x00004988
		protected virtual void ValidateParameters()
		{
			if (string.IsNullOrEmpty(this.ClientId))
			{
				throw new InvalidRequestException(CalendaringStrings.MandatoryParameterClientIdNotSpecified);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000067A4 File Offset: 0x000049A4
		protected virtual Event SendNprMeetingMessages(Event master, IEnumerable<Event> instances)
		{
			Event result = this.SendMessagesForSeries(master, 1, this.Scope.EventDataProvider.CreateOccurrenceViewPropertiesBlob(master));
			byte[] masterGoid = (((IEventInternal)master).GlobalObjectId != null) ? new GlobalObjectId(((IEventInternal)master).GlobalObjectId).Bytes : null;
			this.SendMessagesForInstances(instances, 1, masterGoid);
			return result;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000067F4 File Offset: 0x000049F4
		protected void ValidateCreationHash(Event eventWithKnownHash, Event otherEvent)
		{
			int num = this.CalculateSeriesCreationHash(otherEvent);
			if (num != ((IEventInternal)eventWithKnownHash).SeriesCreationHash)
			{
				throw new ClientIdAlreadyInUseException();
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000684C File Offset: 0x00004A4C
		protected IEnumerable<Event> CreateSeriesInstances(Event master, Event masterChange, Dictionary<int, Event> occurrencesAlreadyCreated, int initialInstanceCreationIndex = 0)
		{
			IEnumerable<Event> result;
			try
			{
				this.Scope.EventDataProvider.BeforeStoreObjectSaved += new Action<Event, ICalendarItemBase>(this.StampRetryProperties);
				result = CreateSeriesInternalBase.CreateSeriesInstances<int>(master, masterChange, this.Scope.EventDataProvider, occurrencesAlreadyCreated, (IEventInternal e) => e.InstanceCreationIndex, delegate(IEventInternal e)
				{
					e.InstanceCreationIndex = initialInstanceCreationIndex++;
				});
			}
			finally
			{
				this.Scope.EventDataProvider.BeforeStoreObjectSaved -= new Action<Event, ICalendarItemBase>(this.StampRetryProperties);
			}
			return result;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000068F8 File Offset: 0x00004AF8
		protected Event CreateSeriesMaster(Event master)
		{
			SeriesEventDataProvider seriesEventDataProvider = this.Scope.SeriesEventDataProvider;
			Event result;
			try
			{
				seriesEventDataProvider.BeforeStoreObjectSaved += new Action<Event, ICalendarItemBase>(this.StampRetryProperties);
				this.AdjustMasterParametersForCreation(master);
				Event @event = seriesEventDataProvider.Create(master);
				result = @event;
			}
			finally
			{
				seriesEventDataProvider.BeforeStoreObjectSaved -= new Action<Event, ICalendarItemBase>(this.StampRetryProperties);
			}
			return result;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000695C File Offset: 0x00004B5C
		protected void AdjustMasterParametersForCreation(Event master)
		{
			((IEventInternal)master).SeriesCreationHash = this.CalculateSeriesCreationHash(master);
			master.Start = ExDateTime.MaxValue;
			master.End = ExDateTime.MinValue;
			master.AdjustSeriesStartAndEndTimes(master.Occurrences);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000069B4 File Offset: 0x00004BB4
		protected virtual Event FindSeriesObjects(Event nprMaster, Dictionary<int, Event> occurrencesAlreadyCreated)
		{
			Event masterAlreadyCreated = null;
			this.Scope.EventDataProvider.ForEachSeriesItem(nprMaster, delegate(Event instance)
			{
				occurrencesAlreadyCreated[((IEventInternal)instance).InstanceCreationIndex] = instance;
				return true;
			}, new Func<IStorePropertyBag, Event>(this.GetBasicEventDataForRetry), delegate(Event master)
			{
				masterAlreadyCreated = master;
			}, null, CalendarItemBaseSchema.EventClientId, new PropertyDefinition[]
			{
				CalendarItemBaseSchema.EventClientId,
				CalendarItemSchema.InstanceCreationIndex,
				CalendarItemSeriesSchema.SeriesCreationHash
			});
			return masterAlreadyCreated;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006A38 File Offset: 0x00004C38
		private Event GetBasicEventDataForRetry(IStorePropertyBag propertyBag)
		{
			Event basicSeriesEventData = EventExtensions.GetBasicSeriesEventData(propertyBag, this.Scope);
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(CalendarItemBaseSchema.EventClientId, null);
			int valueOrDefault2 = propertyBag.GetValueOrDefault<int>(CalendarItemSchema.InstanceCreationIndex, -1);
			int valueOrDefault3 = propertyBag.GetValueOrDefault<int>(CalendarItemSeriesSchema.SeriesCreationHash, -1);
			basicSeriesEventData.ClientId = valueOrDefault;
			IEventInternal eventInternal = basicSeriesEventData;
			if (valueOrDefault2 != -1)
			{
				eventInternal.InstanceCreationIndex = valueOrDefault2;
			}
			if (valueOrDefault3 != -1)
			{
				eventInternal.SeriesCreationHash = valueOrDefault3;
			}
			return basicSeriesEventData;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006A9C File Offset: 0x00004C9C
		private bool ShouldSendMeetingRequest(ICalendarItemBase calendarItemBase)
		{
			return !calendarItemBase.MeetingRequestWasSent && calendarItemBase.AttendeeCollection != null && calendarItemBase.AttendeeCollection.Count > 0;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006AC0 File Offset: 0x00004CC0
		private void StampRetryProperties(IEvent sourceEntity, ICalendarItemBase calendarItemBase)
		{
			calendarItemBase.ClientId = this.ClientId;
			ICalendarItem calendarItem = calendarItemBase as ICalendarItem;
			if (calendarItem != null)
			{
				calendarItem.InstanceCreationIndex = ((IEventInternal)sourceEntity).InstanceCreationIndex;
			}
		}
	}
}
