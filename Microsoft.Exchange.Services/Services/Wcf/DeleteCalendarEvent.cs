using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200094F RID: 2383
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DeleteCalendarEvent : SingleStepServiceCommand<DeleteCalendarEventRequest, VoidResult>
	{
		// Token: 0x060044C4 RID: 17604 RVA: 0x000EE136 File Offset: 0x000EC336
		public DeleteCalendarEvent(CallContext callContext, DeleteCalendarEventRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x000EE140 File Offset: 0x000EC340
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new DeleteCalendarEventResponse(base.Result);
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x000EE150 File Offset: 0x000EC350
		internal override ServiceResult<VoidResult> Execute()
		{
			EntitiesHelper entitiesHelper = new EntitiesHelper(base.CallContext);
			StoreSession session;
			IEvents events = entitiesHelper.GetEvents(base.Request.CalendarId.BaseFolderId, out session);
			Microsoft.Exchange.Services.Core.Types.ItemId eventId = base.Request.EventId;
			entitiesHelper.Execute(new Action<string, CommandContext>(events.Delete), session, BasicTypes.Item, eventId.Id, eventId.ChangeKey);
			return new ServiceResult<VoidResult>(VoidResult.Value);
		}
	}
}
