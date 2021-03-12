using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F22 RID: 3874
	internal class FindContactsCommand : ExchangeServiceCommand<FindContactsRequest, FindContactsResponse>
	{
		// Token: 0x0600630B RID: 25355 RVA: 0x00134F0C File Offset: 0x0013310C
		public FindContactsCommand(FindContactsRequest request) : base(request)
		{
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x00134F18 File Offset: 0x00133118
		protected override FindContactsResponse InternalExecute()
		{
			ContactProvider contactProvider = new ContactProvider(base.ExchangeService);
			ContactQueryAdapter queryAdapter = new ContactQueryAdapter(ContactSchema.SchemaInstance, base.Request.ODataQueryOptions);
			IFindEntitiesResult<Contact> result = contactProvider.Find(base.Request.ParentFolderId, queryAdapter);
			return new FindContactsResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
