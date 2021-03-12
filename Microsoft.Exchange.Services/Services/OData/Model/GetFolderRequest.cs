using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000ECE RID: 3790
	[AllowedOAuthGrant("Mail.Write")]
	[AllowedOAuthGrant("Mail.Read")]
	internal class GetFolderRequest : GetEntityRequest<Folder>
	{
		// Token: 0x0600624D RID: 25165 RVA: 0x00133A4D File Offset: 0x00131C4D
		public GetFolderRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x00133A56 File Offset: 0x00131C56
		public override ODataCommand GetODataCommand()
		{
			return new GetFolderCommand(this);
		}
	}
}
