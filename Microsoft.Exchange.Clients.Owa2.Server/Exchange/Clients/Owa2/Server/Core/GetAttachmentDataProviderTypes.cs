using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200030B RID: 779
	internal class GetAttachmentDataProviderTypes : ServiceCommand<AttachmentDataProviderType>
	{
		// Token: 0x060019EB RID: 6635 RVA: 0x0005D5E7 File Offset: 0x0005B7E7
		public GetAttachmentDataProviderTypes(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x0005D5F0 File Offset: 0x0005B7F0
		protected override AttachmentDataProviderType InternalExecute()
		{
			return AttachmentDataProviderType.OneDrivePro;
		}
	}
}
