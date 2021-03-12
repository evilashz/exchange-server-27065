using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F19 RID: 3865
	internal class SendMessageCommand : ExchangeServiceCommand<SendMessageRequest, SendMessageResponse>
	{
		// Token: 0x060062F4 RID: 25332 RVA: 0x00134C45 File Offset: 0x00132E45
		public SendMessageCommand(SendMessageRequest request) : base(request)
		{
		}

		// Token: 0x060062F5 RID: 25333 RVA: 0x00134C50 File Offset: 0x00132E50
		protected override SendMessageResponse InternalExecute()
		{
			MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
			messageProvider.SendDraft(base.Request.Id);
			return new SendMessageResponse(base.Request);
		}
	}
}
