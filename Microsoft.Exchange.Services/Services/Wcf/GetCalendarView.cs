using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200095C RID: 2396
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetCalendarView : SingleStepServiceCommand<GetCalendarViewRequest, Event[]>
	{
		// Token: 0x06004505 RID: 17669 RVA: 0x000F032E File Offset: 0x000EE52E
		public GetCalendarView(CallContext callContext, GetCalendarViewRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x000F0338 File Offset: 0x000EE538
		internal override ServiceResult<Event[]> Execute()
		{
			EntitiesHelper entitiesHelper = new EntitiesHelper(base.CallContext);
			StoreSession session;
			IEvents events = entitiesHelper.GetEvents(base.Request.CalendarId.BaseFolderId, out session);
			events.AsQueryable(null);
			CalendarViewParameters parameters = new CalendarViewParameters(new ExDateTime?(base.Request.StartRange), new ExDateTime?(base.Request.EndRange));
			CommandContext commandContext = new CommandContext();
			commandContext.SetCustomParameter("ReturnSeriesMaster", base.Request.ReturnMasterItems);
			IEnumerable<Event> calendarView = events.GetCalendarView(parameters, commandContext);
			Event[] value = calendarView.ToArray<Event>();
			entitiesHelper.TransformEntityIdsToEwsIds<Event[]>(value, session);
			return new ServiceResult<Event[]>(value);
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x000F03E4 File Offset: 0x000EE5E4
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetCalendarViewResponse(base.Result);
		}
	}
}
