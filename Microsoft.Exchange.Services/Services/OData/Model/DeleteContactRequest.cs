using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F26 RID: 3878
	[AllowedOAuthGrant("Contacts.Write")]
	internal class DeleteContactRequest : DeleteEntityRequest<Contact>
	{
		// Token: 0x06006312 RID: 25362 RVA: 0x00134FE9 File Offset: 0x001331E9
		public DeleteContactRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006313 RID: 25363 RVA: 0x00134FF2 File Offset: 0x001331F2
		public override ODataCommand GetODataCommand()
		{
			return new DeleteContactCommand(this);
		}
	}
}
