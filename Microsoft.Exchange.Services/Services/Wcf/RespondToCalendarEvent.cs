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
	// Token: 0x02000965 RID: 2405
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RespondToCalendarEvent : SingleStepServiceCommand<RespondToCalendarEventRequest, VoidResult>
	{
		// Token: 0x06004523 RID: 17699 RVA: 0x000F196C File Offset: 0x000EFB6C
		public RespondToCalendarEvent(CallContext callContext, RespondToCalendarEventRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x000F1976 File Offset: 0x000EFB76
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new RespondToCalendarEventResponse(base.Result);
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x000F19AC File Offset: 0x000EFBAC
		internal override ServiceResult<VoidResult> Execute()
		{
			EntitiesHelper entitiesHelper = new EntitiesHelper(base.CallContext);
			StoreSession session;
			IEvents events = entitiesHelper.GetEvents(base.Request.CalendarId.BaseFolderId, out session);
			Microsoft.Exchange.Services.Core.Types.ItemId eventId = base.Request.EventId;
			entitiesHelper.Execute(delegate(string id, CommandContext context)
			{
				events.Respond(id, this.Request.Parameters, context);
			}, session, BasicTypes.Item, eventId.Id, eventId.ChangeKey);
			return new ServiceResult<VoidResult>(VoidResult.Value);
		}
	}
}
