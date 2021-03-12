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
	// Token: 0x02000976 RID: 2422
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UpdateCalendarEvent : MultiStepServiceCommand<UpdateCalendarEventRequest, Event>
	{
		// Token: 0x06004584 RID: 17796 RVA: 0x000F4148 File Offset: 0x000F2348
		public UpdateCalendarEvent(CallContext callContext, UpdateCalendarEventRequest request) : base(callContext, request)
		{
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06004585 RID: 17797 RVA: 0x000F4152 File Offset: 0x000F2352
		internal override int StepCount
		{
			get
			{
				return base.Request.Events.Length;
			}
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x000F4161 File Offset: 0x000F2361
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new UpdateCalendarEventResponse(base.Results);
		}

		// Token: 0x06004587 RID: 17799 RVA: 0x000F4170 File Offset: 0x000F2370
		internal override ServiceResult<Event> Execute()
		{
			EntitiesHelper entitiesHelper = new EntitiesHelper(base.CallContext);
			StoreSession session;
			IEvents events = entitiesHelper.GetEvents(base.Request.CalendarId.BaseFolderId, out session);
			Event input = base.Request.Events[base.CurrentStep];
			Event value = entitiesHelper.Execute<Event, Event>(new Func<Event, CommandContext, Event>(events.Update<Event>), session, BasicTypes.Item, input);
			return new ServiceResult<Event>(value);
		}
	}
}
