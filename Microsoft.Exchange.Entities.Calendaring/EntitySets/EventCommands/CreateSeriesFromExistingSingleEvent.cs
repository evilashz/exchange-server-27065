using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.Serialization;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000045 RID: 69
	internal class CreateSeriesFromExistingSingleEvent : CreateSeriesInternalBase
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000072D4 File Offset: 0x000054D4
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000072DC File Offset: 0x000054DC
		internal IList<Event> AdditionalInstancesToAdd { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000072E5 File Offset: 0x000054E5
		// (set) Token: 0x060001AF RID: 431 RVA: 0x000072ED File Offset: 0x000054ED
		internal string SingleEventId { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000072F6 File Offset: 0x000054F6
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.ConvertSingleEventToNprSeriesTracer;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007310 File Offset: 0x00005510
		protected override Event OnExecute()
		{
			this.ValidateParameters();
			Event @event = this.Scope.Read(this.SingleEventId, this.Context);
			Event event2 = this.ConstructMasterFromSingleEvent(@event);
			Dictionary<int, Event> dictionary = new Dictionary<int, Event>();
			Event event3 = this.FindSeriesObjects(event2, dictionary);
			if (event3 != null)
			{
				base.ValidateCreationHash(event3, event2);
				event2 = this.Scope.Read(event3.Id, null);
			}
			else
			{
				event2 = base.CreateSeriesMaster(event2);
			}
			if (dictionary.Values.FirstOrDefault((Event e) => e.Id.Equals(this.SingleEventId)) == null)
			{
				this.StampSeriesPropertiesOnSingleEvent(@event, event2);
			}
			event2.Occurrences = this.AdditionalInstancesToAdd;
			IEnumerable<Event> second = base.CreateSeriesInstances(event2, event2, dictionary, 1);
			if (@event.IsDraft)
			{
				return event2;
			}
			return this.SendNprMeetingMessages(event2, new List<Event>
			{
				@event
			}.Concat(second));
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000073E0 File Offset: 0x000055E0
		protected override int CalculateSeriesCreationHash(Event master)
		{
			List<byte> list = new List<byte>();
			string s = EntitySerializer.Serialize<IList<Event>>(this.AdditionalInstancesToAdd);
			list.AddRange(Encoding.UTF8.GetBytes(s));
			list.AddRange(Encoding.UTF8.GetBytes(this.SingleEventId));
			byte[] array = list.ToArray();
			return (int)ComputeCRC.Compute(0U, array, 0, array.Count<byte>());
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007440 File Offset: 0x00005640
		private Event ConstructMasterFromSingleEvent(Event singleEvent)
		{
			Event @event = new Event();
			@event.MergeMasterAndInstanceProperties(singleEvent, true, new Func<Event, Event, PropertyDefinition, bool>(EventExtensions.ShouldCopyProperty));
			@event.Type = EventType.SeriesMaster;
			@event.ClientId = base.ClientId;
			@event.Occurrences = new List<Event>
			{
				singleEvent
			}.Concat(this.AdditionalInstancesToAdd).ToList<Event>();
			return @event;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000749F File Offset: 0x0000569F
		private void StampSeriesPropertiesOnSingleEvent(Event singleEvent, Event master)
		{
			singleEvent.SeriesId = master.SeriesId;
			singleEvent.SeriesMasterId = master.Id;
			((IEventInternal)singleEvent).InstanceCreationIndex = 0;
			this.Scope.EventDataProvider.Update(singleEvent, this.Context);
		}
	}
}
