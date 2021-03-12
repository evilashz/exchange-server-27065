using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EF5 RID: 3829
	internal class MoveMessageCommand : ExchangeServiceCommand<MoveMessageRequest, MoveMessageResponse>
	{
		// Token: 0x0600629E RID: 25246 RVA: 0x00134494 File Offset: 0x00132694
		public MoveMessageCommand(MoveMessageRequest request) : base(request)
		{
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x001344A0 File Offset: 0x001326A0
		protected override MoveMessageResponse InternalExecute()
		{
			MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
			Message result = messageProvider.Move(base.Request.Id, base.Request.DestinationId);
			return new MoveMessageResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
