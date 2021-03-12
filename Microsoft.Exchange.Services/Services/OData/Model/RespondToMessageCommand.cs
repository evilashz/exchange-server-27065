using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F16 RID: 3862
	internal class RespondToMessageCommand : ExchangeServiceCommand<RespondToMessageRequest, RespondToMessageResponse>
	{
		// Token: 0x060062EE RID: 25326 RVA: 0x00134BC2 File Offset: 0x00132DC2
		public RespondToMessageCommand(RespondToMessageRequest request) : base(request)
		{
		}

		// Token: 0x060062EF RID: 25327 RVA: 0x00134BCC File Offset: 0x00132DCC
		protected override RespondToMessageResponse InternalExecute()
		{
			MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
			messageProvider.PerformMessageResponseAction(base.Request.Id, base.Request.ResponseType, true, base.Request.Comment, base.Request.ToRecipients);
			return new RespondToMessageResponse(base.Request);
		}
	}
}
