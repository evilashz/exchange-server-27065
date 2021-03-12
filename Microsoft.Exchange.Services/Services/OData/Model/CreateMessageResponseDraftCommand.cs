using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F13 RID: 3859
	internal class CreateMessageResponseDraftCommand : ExchangeServiceCommand<CreateMessageResponseDraftRequest, CreateMessageResponseDraftResponse>
	{
		// Token: 0x060062E0 RID: 25312 RVA: 0x00134A50 File Offset: 0x00132C50
		public CreateMessageResponseDraftCommand(CreateMessageResponseDraftRequest request) : base(request)
		{
		}

		// Token: 0x060062E1 RID: 25313 RVA: 0x00134A5C File Offset: 0x00132C5C
		protected override CreateMessageResponseDraftResponse InternalExecute()
		{
			MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
			Message result = messageProvider.PerformMessageResponseAction(base.Request.Id, base.Request.ResponseType, false, null, null);
			return new CreateMessageResponseDraftResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
