using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EE8 RID: 3816
	internal class FindMessagesCommand : ExchangeServiceCommand<FindMessagesRequest, FindMessagesResponse>
	{
		// Token: 0x06006284 RID: 25220 RVA: 0x00134143 File Offset: 0x00132343
		public FindMessagesCommand(FindMessagesRequest request) : base(request)
		{
		}

		// Token: 0x06006285 RID: 25221 RVA: 0x0013414C File Offset: 0x0013234C
		protected override FindMessagesResponse InternalExecute()
		{
			MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
			MessageQueryAdapter queryAdapter = new MessageQueryAdapter(MessageSchema.SchemaInstance, base.Request.ODataQueryOptions);
			IFindEntitiesResult<Message> result = messageProvider.Find(base.Request.ParentFolderId, queryAdapter);
			return new FindMessagesResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
