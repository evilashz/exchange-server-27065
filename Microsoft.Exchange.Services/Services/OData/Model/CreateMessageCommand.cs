using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EEC RID: 3820
	internal class CreateMessageCommand : ExchangeServiceCommand<CreateMessageRequest, CreateMessageResponse>
	{
		// Token: 0x0600628F RID: 25231 RVA: 0x00134357 File Offset: 0x00132557
		public CreateMessageCommand(CreateMessageRequest request) : base(request)
		{
		}

		// Token: 0x06006290 RID: 25232 RVA: 0x00134360 File Offset: 0x00132560
		protected override CreateMessageResponse InternalExecute()
		{
			MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
			Message result = messageProvider.Create(base.Request.ParentFolderId, base.Request.Template, base.Request.MessageDisposition);
			return new CreateMessageResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
