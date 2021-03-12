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
	// Token: 0x02000948 RID: 2376
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CancelCalendarEvent : SingleStepServiceCommand<CancelCalendarEventRequest, VoidResult>
	{
		// Token: 0x060044B0 RID: 17584 RVA: 0x000ED446 File Offset: 0x000EB646
		public CancelCalendarEvent(CallContext callContext, CancelCalendarEventRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x000ED450 File Offset: 0x000EB650
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new CancelCalendarEventResponse(base.Result);
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x000ED484 File Offset: 0x000EB684
		internal override ServiceResult<VoidResult> Execute()
		{
			EntitiesHelper entitiesHelper = new EntitiesHelper(base.CallContext);
			StoreSession session;
			IEvents events = entitiesHelper.GetEvents(base.Request.CalendarId.BaseFolderId, out session);
			entitiesHelper.Execute(delegate(string key, CommandContext context)
			{
				events.Cancel(key, this.Request.Parameters, context);
			}, session, base.Request.EventId);
			return new ServiceResult<VoidResult>(VoidResult.Value);
		}
	}
}
