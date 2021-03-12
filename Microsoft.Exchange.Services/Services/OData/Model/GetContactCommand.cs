using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F1F RID: 3871
	internal class GetContactCommand : ExchangeServiceCommand<GetContactRequest, GetContactResponse>
	{
		// Token: 0x06006302 RID: 25346 RVA: 0x00134DEC File Offset: 0x00132FEC
		public GetContactCommand(GetContactRequest request) : base(request)
		{
		}

		// Token: 0x06006303 RID: 25347 RVA: 0x00134DF8 File Offset: 0x00132FF8
		protected override GetContactResponse InternalExecute()
		{
			ContactProvider contactProvider = new ContactProvider(base.ExchangeService);
			Contact result = contactProvider.Read(base.Request.Id, new ContactQueryAdapter(ContactSchema.SchemaInstance, base.Request.ODataQueryOptions));
			return new GetContactResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
