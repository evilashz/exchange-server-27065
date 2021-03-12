using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F1C RID: 3868
	internal class CreateContactCommand : ExchangeServiceCommand<CreateContactRequest, CreateContactResponse>
	{
		// Token: 0x060062FD RID: 25341 RVA: 0x00134D7C File Offset: 0x00132F7C
		public CreateContactCommand(CreateContactRequest request) : base(request)
		{
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x00134D88 File Offset: 0x00132F88
		protected override CreateContactResponse InternalExecute()
		{
			ContactProvider contactProvider = new ContactProvider(base.ExchangeService);
			Contact result = contactProvider.Create(base.Request.ParentFolderId, base.Request.Template);
			return new CreateContactResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
