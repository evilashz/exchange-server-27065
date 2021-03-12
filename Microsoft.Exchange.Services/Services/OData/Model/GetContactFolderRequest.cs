using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F29 RID: 3881
	[AllowedOAuthGrant("Contacts.Write")]
	[AllowedOAuthGrant("Contacts.Read")]
	internal class GetContactFolderRequest : GetEntityRequest<ContactFolder>
	{
		// Token: 0x06006317 RID: 25367 RVA: 0x00135041 File Offset: 0x00133241
		public GetContactFolderRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x0013504A File Offset: 0x0013324A
		public override ODataCommand GetODataCommand()
		{
			return new GetContactFolderCommand(this);
		}
	}
}
