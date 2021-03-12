using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EF8 RID: 3832
	internal class UpdateMessageCommand : ExchangeServiceCommand<UpdateMessageRequest, UpdateMessageResponse>
	{
		// Token: 0x060062A3 RID: 25251 RVA: 0x00134504 File Offset: 0x00132704
		public UpdateMessageCommand(UpdateMessageRequest request) : base(request)
		{
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x00134510 File Offset: 0x00132710
		protected override UpdateMessageResponse InternalExecute()
		{
			MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
			Message result = messageProvider.Update(base.Request.Id, base.Request.Change, base.Request.ChangeKey);
			return new UpdateMessageResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
