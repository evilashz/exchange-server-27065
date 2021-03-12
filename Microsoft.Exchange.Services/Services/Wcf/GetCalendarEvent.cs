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
	// Token: 0x02000953 RID: 2387
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetCalendarEvent : MultiStepServiceCommand<GetCalendarEventRequest, Event>
	{
		// Token: 0x060044D0 RID: 17616 RVA: 0x000EE642 File Offset: 0x000EC842
		public GetCalendarEvent(CallContext callContext, GetCalendarEventRequest request) : base(callContext, request)
		{
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x060044D1 RID: 17617 RVA: 0x000EE64C File Offset: 0x000EC84C
		internal override int StepCount
		{
			get
			{
				return base.Request.EventIds.Length;
			}
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x000EE65B File Offset: 0x000EC85B
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetCalendarEventResponse(base.Results);
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x000EE668 File Offset: 0x000EC868
		internal override ServiceResult<Event> Execute()
		{
			EntitiesHelper entitiesHelper = new EntitiesHelper(base.CallContext);
			StoreSession session;
			IEvents events = entitiesHelper.GetEvents(base.Request.CalendarId.BaseFolderId, out session);
			Microsoft.Exchange.Services.Core.Types.ItemId itemId = base.Request.EventIds[base.CurrentStep];
			Event value = entitiesHelper.Execute<Event>(new Func<string, CommandContext, Event>(events.Read), session, BasicTypes.Item, itemId.Id, itemId.ChangeKey);
			return new ServiceResult<Event>(value);
		}
	}
}
