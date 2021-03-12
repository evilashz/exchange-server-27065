using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F2B RID: 3883
	internal class GetContactFolderCommand : ExchangeServiceCommand<GetContactFolderRequest, GetContactFolderResponse>
	{
		// Token: 0x0600631A RID: 25370 RVA: 0x0013505B File Offset: 0x0013325B
		public GetContactFolderCommand(GetContactFolderRequest request) : base(request)
		{
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x00135064 File Offset: 0x00133264
		protected override GetContactFolderResponse InternalExecute()
		{
			ContactFolderProvider contactFolderProvider = new ContactFolderProvider(base.ExchangeService);
			ContactFolder contactFolder = contactFolderProvider.Read(base.Request.Id, new ContactFolderQueryAdapter(ContactFolderSchema.SchemaInstance, base.Request.ODataQueryOptions));
			if (base.Request.ODataQueryOptions.Expands(ContactFolderSchema.ChildFolders.Name))
			{
				contactFolder.ChildFolders = contactFolderProvider.Find(contactFolder.Id, null);
			}
			if (base.Request.ODataQueryOptions.Expands(ContactFolderSchema.ChildFolders.Name))
			{
				ContactProvider contactProvider = new ContactProvider(base.ExchangeService);
				contactFolder.Contacts = contactProvider.Find(contactFolder.Id, null);
			}
			return new GetContactFolderResponse(base.Request)
			{
				Result = contactFolder
			};
		}
	}
}
