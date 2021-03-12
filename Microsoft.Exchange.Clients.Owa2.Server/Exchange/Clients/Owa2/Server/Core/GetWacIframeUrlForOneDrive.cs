using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000330 RID: 816
	internal class GetWacIframeUrlForOneDrive : ServiceCommand<string>
	{
		// Token: 0x06001B15 RID: 6933 RVA: 0x00066C1C File Offset: 0x00064E1C
		public GetWacIframeUrlForOneDrive(CallContext callContext, GetWacIframeUrlForOneDriveRequest request) : base(callContext)
		{
			throw new OwaInvalidRequestException();
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00066C2A File Offset: 0x00064E2A
		protected override string InternalExecute()
		{
			throw new OwaInvalidRequestException();
		}
	}
}
