using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EDD RID: 3805
	[AllowedOAuthGrant("Mail.Write")]
	internal class CopyFolderRequest : CopyOrMoveEntityRequest<Folder>
	{
		// Token: 0x0600626E RID: 25198 RVA: 0x00133F1A File Offset: 0x0013211A
		public CopyFolderRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x00133F23 File Offset: 0x00132123
		public override ODataCommand GetODataCommand()
		{
			return new CopyFolderCommand(this);
		}
	}
}
