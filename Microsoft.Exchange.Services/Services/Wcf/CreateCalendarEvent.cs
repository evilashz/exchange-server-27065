using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200094B RID: 2379
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CreateCalendarEvent : MultiStepServiceCommand<CreateCalendarEventRequest, Event>
	{
		// Token: 0x060044B9 RID: 17593 RVA: 0x000EDBD0 File Offset: 0x000EBDD0
		public CreateCalendarEvent(CallContext callContext, CreateCalendarEventRequest request) : base(callContext, request)
		{
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x060044BA RID: 17594 RVA: 0x000EDBDA File Offset: 0x000EBDDA
		internal override int StepCount
		{
			get
			{
				return base.Request.Events.Length;
			}
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x000EDBE9 File Offset: 0x000EBDE9
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new CreateCalendarEventResponse(base.Results);
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x000EDBF8 File Offset: 0x000EBDF8
		internal override ServiceResult<Event> Execute()
		{
			EntitiesHelper entitiesHelper = new EntitiesHelper(base.CallContext);
			StoreSession session;
			IEvents events = entitiesHelper.GetEvents(base.Request.CalendarId.BaseFolderId, out session);
			Event input = base.Request.Events[base.CurrentStep];
			Event value = entitiesHelper.Execute<Event, Event>(new Func<Event, CommandContext, Event>(events.Create), session, BasicTypes.Item, input);
			return new ServiceResult<Event>(value);
		}
	}
}
