using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000043 RID: 67
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CreateReceivedSeries : CreateEntityCommand<Events, Event>
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00007065 File Offset: 0x00005265
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.CreateReceivedSeriesTracer;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000706C File Offset: 0x0000526C
		protected override Event OnExecute()
		{
			this.ValidateParameters();
			this.CreateSeriesInstances();
			return base.Entity;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007080 File Offset: 0x00005280
		protected void ValidateParameters()
		{
			if (!base.Entity.IsPropertySet(base.Entity.Schema.SeriesIdProperty))
			{
				throw new ArgumentException("SeriesId should be populated from meeting message", "SeriesId");
			}
			if (base.Entity.Occurrences != null)
			{
				foreach (Event @event in base.Entity.Occurrences)
				{
					ArgumentValidator.ThrowIfNull("occurrence", @event);
					ArgumentValidator.ThrowIfNullOrEmpty("GOID", ((IEventInternal)@event).GlobalObjectId);
					ArgumentValidator.ThrowIfNull("Start", @event.Start);
					ArgumentValidator.ThrowIfNull("End", @event.End);
				}
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000716C File Offset: 0x0000536C
		private IDictionary<string, Event> FindOccurences()
		{
			Dictionary<string, Event> occurrencesAlreadyCreated = new Dictionary<string, Event>();
			this.Scope.EventDataProvider.ForEachSeriesItem(base.Entity, delegate(Event instance)
			{
				occurrencesAlreadyCreated[((IEventInternal)instance).GlobalObjectId] = instance;
				return true;
			}, new Func<IStorePropertyBag, Event>(this.GetBasicEventData), null, null, null, new PropertyDefinition[]
			{
				CalendarItemBaseSchema.GlobalObjectId
			});
			return occurrencesAlreadyCreated;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000071D4 File Offset: 0x000053D4
		private Event GetBasicEventData(IStorePropertyBag propertyBag)
		{
			Event basicSeriesEventData = EventExtensions.GetBasicSeriesEventData(propertyBag, this.Scope);
			byte[] valueOrDefault = propertyBag.GetValueOrDefault<byte[]>(CalendarItemBaseSchema.GlobalObjectId, null);
			if (valueOrDefault != null)
			{
				IEventInternal eventInternal = basicSeriesEventData;
				eventInternal.GlobalObjectId = new GlobalObjectId(valueOrDefault).ToString();
			}
			return basicSeriesEventData;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000721C File Offset: 0x0000541C
		private void CreateSeriesInstances()
		{
			IDictionary<string, Event> occurrencesAlreadyCreated = this.FindOccurences();
			CreateSeriesInternalBase.CreateSeriesInstances<string>(base.Entity, base.Entity, this.Scope.EventDataProvider, occurrencesAlreadyCreated, (IEventInternal e) => e.GlobalObjectId, null);
		}
	}
}
