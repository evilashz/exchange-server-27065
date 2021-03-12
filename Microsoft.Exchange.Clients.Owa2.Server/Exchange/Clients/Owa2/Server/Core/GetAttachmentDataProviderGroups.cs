using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000308 RID: 776
	internal class GetAttachmentDataProviderGroups : ServiceCommand<GetAttachmentDataProviderItemsResponse>
	{
		// Token: 0x060019E5 RID: 6629 RVA: 0x0005D470 File Offset: 0x0005B670
		public GetAttachmentDataProviderGroups(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0005D47C File Offset: 0x0005B67C
		protected override GetAttachmentDataProviderItemsResponse InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			return userContext.AttachmentDataProviderManager.GetGroups(base.CallContext, base.MailboxIdentityMailboxSession);
		}
	}
}
