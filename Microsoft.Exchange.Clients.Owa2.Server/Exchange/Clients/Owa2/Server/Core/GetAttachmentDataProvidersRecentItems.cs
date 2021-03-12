using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200030A RID: 778
	internal class GetAttachmentDataProvidersRecentItems : ServiceCommand<GetAttachmentDataProviderItemsResponse>
	{
		// Token: 0x060019E9 RID: 6633 RVA: 0x0005D5A1 File Offset: 0x0005B7A1
		public GetAttachmentDataProvidersRecentItems(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0005D5AC File Offset: 0x0005B7AC
		protected override GetAttachmentDataProviderItemsResponse InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			return userContext.AttachmentDataProviderManager.GetRecentItems(base.CallContext);
		}
	}
}
