using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x0200005C RID: 92
	public interface IEvents : IEntitySet<Event>
	{
		// Token: 0x17000132 RID: 306
		IEventReference this[string eventId]
		{
			get;
		}

		// Token: 0x060002F0 RID: 752
		void Cancel(string key, CancelEventParameters parameters, CommandContext context = null);

		// Token: 0x060002F1 RID: 753
		void Forward(string key, ForwardEventParameters parameters, CommandContext context = null);

		// Token: 0x060002F2 RID: 754
		ExpandedEvent Expand(string key, ExpandEventParameters parameters, CommandContext context = null);

		// Token: 0x060002F3 RID: 755
		IEnumerable<Event> GetCalendarView(ICalendarViewParameters parameters, CommandContext context = null);

		// Token: 0x060002F4 RID: 756
		void Respond(string key, RespondToEventParameters parameters, CommandContext context = null);

		// Token: 0x060002F5 RID: 757
		Event Update(string key, Event entity, UpdateEventParameters updateEventParameters, CommandContext context = null);

		// Token: 0x060002F6 RID: 758
		Event ConvertSingleEventToNprSeries(string key, IList<Event> additionalInstancesToAdd, string clientId, CommandContext context = null);
	}
}
