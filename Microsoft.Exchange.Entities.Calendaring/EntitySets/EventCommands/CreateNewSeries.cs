using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.Serialization;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000041 RID: 65
	internal class CreateNewSeries : CreateSeriesInternalBase
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00006AFC File Offset: 0x00004CFC
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00006B04 File Offset: 0x00004D04
		internal Event Entity { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006B0D File Offset: 0x00004D0D
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.CreateSeriesTracer;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006B14 File Offset: 0x00004D14
		protected override Event OnExecute()
		{
			this.ValidateParameters();
			Dictionary<int, Event> occurrencesAlreadyCreated = new Dictionary<int, Event>();
			Event @event = this.FindSeriesObjects(this.Entity, occurrencesAlreadyCreated);
			Event event2;
			if (@event != null)
			{
				base.ValidateCreationHash(@event, this.Entity);
				event2 = this.Scope.SeriesEventDataProvider.Read(this.Scope.IdConverter.GetStoreId(@event));
			}
			else
			{
				event2 = base.CreateSeriesMaster(this.Entity);
			}
			IEnumerable<Event> instances = base.CreateSeriesInstances(event2, this.Entity, occurrencesAlreadyCreated, 0);
			if (event2.IsDraft)
			{
				return event2;
			}
			return this.SendNprMeetingMessages(event2, instances);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006BA4 File Offset: 0x00004DA4
		protected override void ValidateParameters()
		{
			base.ValidateParameters();
			if (this.Entity.IsPropertySet(this.Entity.Schema.SeriesIdProperty))
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorCallerCantSpecifySeriesId);
			}
			if (this.Entity.Occurrences == null || this.Entity.Occurrences.Count == 0)
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorOccurrencesListRequired);
			}
			if ((long)this.Entity.Occurrences.Count > 50L)
			{
				throw new InvalidRequestException(ServerStrings.ExTooManyInstancesOnSeries(50U));
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006C2C File Offset: 0x00004E2C
		protected override int CalculateSeriesCreationHash(Event master)
		{
			string s = EntitySerializer.Serialize<Event>(master);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			return (int)ComputeCRC.Compute(0U, bytes, 0, bytes.Length);
		}
	}
}
