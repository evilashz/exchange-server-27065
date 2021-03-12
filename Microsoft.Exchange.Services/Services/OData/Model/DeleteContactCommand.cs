using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F28 RID: 3880
	internal class DeleteContactCommand : ExchangeServiceCommand<DeleteContactRequest, DeleteContactResponse>
	{
		// Token: 0x06006315 RID: 25365 RVA: 0x00135003 File Offset: 0x00133203
		public DeleteContactCommand(DeleteContactRequest request) : base(request)
		{
		}

		// Token: 0x06006316 RID: 25366 RVA: 0x0013500C File Offset: 0x0013320C
		protected override DeleteContactResponse InternalExecute()
		{
			ContactProvider contactProvider = new ContactProvider(base.ExchangeService);
			contactProvider.Delete(base.Request.Id);
			return new DeleteContactResponse(base.Request);
		}
	}
}
