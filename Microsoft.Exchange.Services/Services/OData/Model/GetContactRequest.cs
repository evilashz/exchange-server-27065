using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F1D RID: 3869
	[AllowedOAuthGrant("Contacts.Read")]
	[AllowedOAuthGrant("Contacts.Write")]
	internal class GetContactRequest : GetEntityRequest<Contact>
	{
		// Token: 0x060062FF RID: 25343 RVA: 0x00134DD2 File Offset: 0x00132FD2
		public GetContactRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x00134DDB File Offset: 0x00132FDB
		public override ODataCommand GetODataCommand()
		{
			return new GetContactCommand(this);
		}
	}
}
