using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EEF RID: 3823
	internal class DeleteMessageCommand : ExchangeServiceCommand<DeleteMessageRequest, DeleteMessageResponse>
	{
		// Token: 0x06006294 RID: 25236 RVA: 0x001343CF File Offset: 0x001325CF
		public DeleteMessageCommand(DeleteMessageRequest request) : base(request)
		{
		}

		// Token: 0x06006295 RID: 25237 RVA: 0x001343D8 File Offset: 0x001325D8
		protected override DeleteMessageResponse InternalExecute()
		{
			MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
			messageProvider.Delete(base.Request.Id);
			return new DeleteMessageResponse(base.Request);
		}
	}
}
