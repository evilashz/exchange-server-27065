using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.EntitySets.Commands;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000050 RID: 80
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetCalendarView : EntityCommand<Events, IEnumerable<Event>>
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000869A File Offset: 0x0000689A
		public GetCalendarView(ICalendarViewParameters parameters)
		{
			this.Parameters = parameters;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000086A9 File Offset: 0x000068A9
		// (set) Token: 0x06000204 RID: 516 RVA: 0x000086B1 File Offset: 0x000068B1
		public ICalendarViewParameters Parameters { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000086BA File Offset: 0x000068BA
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.GetCalendarViewTracer;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000086F8 File Offset: 0x000068F8
		protected override IEnumerable<Event> OnExecute()
		{
			EventDataProvider eventDataProvider = this.Scope.EventDataProvider;
			IEnumerable<PropertyDefinition> first = eventDataProvider.MapProperties(FindEvents.HardCodedProperties);
			PropertyDefinition[] array = first.Concat(FindEvents.HardCodedPropertyDependencies).ToArray<PropertyDefinition>();
			bool includeSeriesMasters;
			if (this.Context == null || !this.Context.TryGetCustomParameter<bool>("ReturnSeriesMaster", out includeSeriesMasters))
			{
				includeSeriesMasters = false;
			}
			bool flag = this.Context != null && this.Context.RequestedProperties != null && FindEvents.NeedsReread(this.Context.RequestedProperties, this.Trace);
			IEnumerable<Event> calendarView = eventDataProvider.GetCalendarView(this.Parameters.EffectiveStartTime, this.Parameters.EffectiveEndTime, includeSeriesMasters, flag ? this.Scope.EventDataProvider.MapProperties(FindEvents.IdProperty).ToArray<PropertyDefinition>() : array);
			if (flag)
			{
				int count = (this.Context != null) ? this.Context.PageSizeOnReread : 20;
				return (from x in calendarView.Take(count)
				select this.Scope.Read(x.Id, this.Context)).ToList<Event>();
			}
			EventTimeAdjuster adjuster = this.Scope.TimeAdjuster;
			ExTimeZone sessionTimeZone = this.Scope.Session.ExTimeZone;
			return from e in calendarView
			select adjuster.AdjustTimeProperties(e, sessionTimeZone);
		}

		// Token: 0x04000093 RID: 147
		public const string ReturnSeriesMastersParameter = "ReturnSeriesMaster";
	}
}
