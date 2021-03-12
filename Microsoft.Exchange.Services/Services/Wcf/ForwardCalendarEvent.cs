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
	// Token: 0x02000952 RID: 2386
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ForwardCalendarEvent : SingleStepServiceCommand<ForwardCalendarEventRequest, VoidResult>
	{
		// Token: 0x060044CD RID: 17613 RVA: 0x000EE587 File Offset: 0x000EC787
		public ForwardCalendarEvent(CallContext callContext, ForwardCalendarEventRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x000EE591 File Offset: 0x000EC791
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new ForwardCalendarEventResponse(base.Result);
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x000EE5C8 File Offset: 0x000EC7C8
		internal override ServiceResult<VoidResult> Execute()
		{
			EntitiesHelper entitiesHelper = new EntitiesHelper(base.CallContext);
			StoreSession session;
			IEvents events = entitiesHelper.GetEvents(base.Request.CalendarId.BaseFolderId, out session);
			Microsoft.Exchange.Services.Core.Types.ItemId eventId2 = base.Request.EventId;
			entitiesHelper.Execute(delegate(string eventId, CommandContext context)
			{
				events.Forward(eventId, this.Request.Parameters, context);
			}, session, BasicTypes.Item, eventId2.Id, eventId2.ChangeKey);
			return new ServiceResult<VoidResult>(VoidResult.Value);
		}
	}
}
