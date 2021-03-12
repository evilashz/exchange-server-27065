using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EE5 RID: 3813
	internal class GetMessageCommand : ExchangeServiceCommand<GetMessageRequest, GetMessageResponse>
	{
		// Token: 0x0600627B RID: 25211 RVA: 0x00134014 File Offset: 0x00132214
		public GetMessageCommand(GetMessageRequest request) : base(request)
		{
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x00134020 File Offset: 0x00132220
		protected override GetMessageResponse InternalExecute()
		{
			MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
			Message result = messageProvider.Read(base.Request.Id, new MessageQueryAdapter(MessageSchema.SchemaInstance, base.Request.ODataQueryOptions));
			return new GetMessageResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
