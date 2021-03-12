using System;
using Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x02000035 RID: 53
	internal interface IEventCommandFactory : IEntityCommandFactory<Events, Event>
	{
		// Token: 0x06000129 RID: 297
		CancelEventBase CreateCancelCommand(string key, Events scope);

		// Token: 0x0600012A RID: 298
		ConvertSingleEventToNprSeries CreateConvertSingleEventToNprCommand(string key, Events scope);

		// Token: 0x0600012B RID: 299
		ExpandSeries CreateExpandCommand(string key, Events scope);

		// Token: 0x0600012C RID: 300
		ForwardEventBase CreateForwardCommand(string key, Events scope);

		// Token: 0x0600012D RID: 301
		GetCalendarView CreateGetCalendarViewCommand(ICalendarViewParameters parameters, Events scope);

		// Token: 0x0600012E RID: 302
		RespondToEventBase CreateRespondToCommand(string key, Events scope);

		// Token: 0x0600012F RID: 303
		UpdateEventBase CreateUpdateCommand(string key, Event calEvent, Events scope, UpdateEventParameters updateEventParameters);
	}
}
