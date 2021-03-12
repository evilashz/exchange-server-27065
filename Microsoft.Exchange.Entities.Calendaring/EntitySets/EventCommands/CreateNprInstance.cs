using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.ReliableActions;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000042 RID: 66
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CreateNprInstance : CreateSingleEventBase
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006C61 File Offset: 0x00004E61
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.CreateOccurrenceTracer;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006C68 File Offset: 0x00004E68
		protected override void ValidateSeriesId()
		{
			if (!base.Entity.IsPropertySet(base.Entity.Schema.SeriesIdProperty))
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorCallerMustSpecifySeriesId);
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006C92 File Offset: 0x00004E92
		protected override void ValidateParameters()
		{
			base.ValidateParameters();
			if (!base.Entity.IsPropertySet(base.Entity.Schema.ClientIdProperty))
			{
				throw new InvalidRequestException(CalendaringStrings.MandatoryParameterClientIdNotSpecified);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006D3C File Offset: 0x00004F3C
		protected override Event CreateNewEvent()
		{
			Event master = new Event
			{
				SeriesId = base.Entity.SeriesId,
				Occurrences = new List<Event>(),
				Type = EventType.SeriesMaster
			};
			this.Scope.EventDataProvider.ForEachSeriesItem(master, delegate(Event instance)
			{
				master.Occurrences.Add(instance);
				return true;
			}, (IStorePropertyBag propertyBag) => CreateNprInstance.GetBasicSeriesEventDataWithClinetId(propertyBag, this.Scope), delegate(Event series)
			{
				master.Id = series.Id;
				master.ChangeKey = series.ChangeKey;
				master.ClientId = series.ClientId;
			}, null, null, new PropertyDefinition[]
			{
				CalendarItemBaseSchema.EventClientId
			});
			if (!master.IsPropertySet(master.Schema.IdProperty))
			{
				throw new SeriesNotFoundException(base.Entity.SeriesId);
			}
			int count = master.Occurrences.Count;
			if ((long)(count + 1) > 50L)
			{
				throw new InvalidOperationException(ServerStrings.ExTooManyInstancesOnSeries(50U));
			}
			if (string.Equals(master.ClientId, base.Entity.ClientId))
			{
				throw new ClientIdAlreadyInUseException();
			}
			Event @event = this.Scope.SeriesEventDataProvider.Read(this.Scope.IdConverter.ToStoreObjectId(master.Id));
			if (@event.IsPropertySet(@event.Schema.LastExecutedInteropActionProperty))
			{
				((IActionPropagationState)base.Entity).LastExecutedAction = ((IActionPropagationState)@event).LastExecutedAction;
			}
			if (@event.AdjustSeriesStartAndEndTimes(new Event[]
			{
				base.Entity
			}))
			{
				this.Scope.SeriesEventDataProvider.Update(@event, this.Context);
			}
			Event event2 = master.Occurrences.FirstOrDefault((Event occurrence) => string.Equals(occurrence.ClientId, base.Entity.ClientId));
			if (event2 == null)
			{
				event2 = CreateNprInstance.CreateCopyOccurrenceWithStartAndEnd(base.Entity, @event);
				try
				{
					this.Scope.EventDataProvider.BeforeStoreObjectSaved += new Action<Event, ICalendarItemBase>(this.StampRetryProperties);
					event2 = this.Scope.EventDataProvider.Create(event2);
				}
				finally
				{
					this.Scope.EventDataProvider.BeforeStoreObjectSaved -= new Action<Event, ICalendarItemBase>(this.StampRetryProperties);
				}
			}
			base.Entity.Id = event2.Id;
			if (!base.Entity.IsPropertySet(base.Entity.Schema.IsDraftProperty))
			{
				base.Entity.IsDraft = @event.IsDraft;
			}
			return this.Scope.EventDataProvider.Update(base.Entity, this.Context);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006FD0 File Offset: 0x000051D0
		private static Event GetBasicSeriesEventDataWithClinetId(IStorePropertyBag propertyBag, IStorageEntitySetScope<IStoreSession> scope)
		{
			Event basicSeriesEventData = EventExtensions.GetBasicSeriesEventData(propertyBag, scope);
			basicSeriesEventData.ClientId = propertyBag.GetValueOrDefault<string>(CalendarItemBaseSchema.EventClientId, null);
			return basicSeriesEventData;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006FF8 File Offset: 0x000051F8
		private static Event CreateCopyOccurrenceWithStartAndEnd(Event instance, Event masterFromStore)
		{
			Event @event = new Event
			{
				Start = instance.Start,
				End = instance.End,
				Type = EventType.Exception
			};
			masterFromStore.CopyMasterPropertiesTo(@event, true, null, true);
			@event.IsDraft = true;
			@event.ClientId = instance.ClientId;
			return @event;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000704A File Offset: 0x0000524A
		private void StampRetryProperties(IEvent sourceEntity, ICalendarItemBase calendarItemBase)
		{
			calendarItemBase.ClientId = base.Entity.ClientId;
		}
	}
}
