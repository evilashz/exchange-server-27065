using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200030C RID: 780
	internal class GetAttachmentDataProviderUploadFolderName : ServiceCommand<string>
	{
		// Token: 0x060019ED RID: 6637 RVA: 0x0005D5F3 File Offset: 0x0005B7F3
		public GetAttachmentDataProviderUploadFolderName(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0005D5FC File Offset: 0x0005B7FC
		protected override string InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			AttachmentDataProvider defaultUploadDataProvider = userContext.AttachmentDataProviderManager.GetDefaultUploadDataProvider(base.CallContext);
			if (defaultUploadDataProvider is OneDriveProAttachmentDataProvider)
			{
				return ((OneDriveProAttachmentDataProvider)defaultUploadDataProvider).GetUploadFolderName(userContext);
			}
			return null;
		}
	}
}
