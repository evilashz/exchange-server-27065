using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F25 RID: 3877
	internal class UpdateContactCommand : ExchangeServiceCommand<UpdateContactRequest, UpdateContactResponse>
	{
		// Token: 0x06006310 RID: 25360 RVA: 0x00134F88 File Offset: 0x00133188
		public UpdateContactCommand(UpdateContactRequest request) : base(request)
		{
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x00134F94 File Offset: 0x00133194
		protected override UpdateContactResponse InternalExecute()
		{
			ContactProvider contactProvider = new ContactProvider(base.ExchangeService);
			Contact result = contactProvider.Update(base.Request.Id, base.Request.Change, base.Request.ChangeKey);
			return new UpdateContactResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
