using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002D7 RID: 727
	internal class AddAttachmentDataProvider : ServiceCommand<AttachmentDataProvider>
	{
		// Token: 0x06001891 RID: 6289 RVA: 0x0005463E File Offset: 0x0005283E
		public AddAttachmentDataProvider(CallContext callContext, AttachmentDataProvider attachmentDataProvider) : base(callContext)
		{
			if (attachmentDataProvider == null)
			{
				throw new ArgumentNullException("attachmentDataProvider");
			}
			this.attachmentDataProvider = attachmentDataProvider;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0005465C File Offset: 0x0005285C
		protected override AttachmentDataProvider InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			return userContext.AttachmentDataProviderManager.AddProvider(base.CallContext, this.attachmentDataProvider);
		}

		// Token: 0x04000D2D RID: 3373
		private AttachmentDataProvider attachmentDataProvider;
	}
}
