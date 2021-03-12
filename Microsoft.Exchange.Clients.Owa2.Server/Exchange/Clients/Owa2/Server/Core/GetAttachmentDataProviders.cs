using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000307 RID: 775
	internal class GetAttachmentDataProviders : ServiceCommand<AttachmentDataProvider[]>
	{
		// Token: 0x060019E3 RID: 6627 RVA: 0x0005D42C File Offset: 0x0005B62C
		public GetAttachmentDataProviders(CallContext callContext, GetAttachmentDataProvidersRequest request) : base(callContext)
		{
			this.request = request;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x0005D43C File Offset: 0x0005B63C
		protected override AttachmentDataProvider[] InternalExecute()
		{
			return UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true).AttachmentDataProviderManager.GetProviders(base.CallContext, this.request);
		}

		// Token: 0x04000E53 RID: 3667
		private readonly GetAttachmentDataProvidersRequest request;
	}
}
