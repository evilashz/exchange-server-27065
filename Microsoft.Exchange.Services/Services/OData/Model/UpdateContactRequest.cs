using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F23 RID: 3875
	[AllowedOAuthGrant("Contacts.Write")]
	internal class UpdateContactRequest : UpdateEntityRequest<Contact>
	{
		// Token: 0x0600630D RID: 25357 RVA: 0x00134F6E File Offset: 0x0013316E
		public UpdateContactRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x0600630E RID: 25358 RVA: 0x00134F77 File Offset: 0x00133177
		public override ODataCommand GetODataCommand()
		{
			return new UpdateContactCommand(this);
		}
	}
}
