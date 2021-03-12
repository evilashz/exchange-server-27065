using System;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200041C RID: 1052
	internal interface IRemoteArchiveRequest
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001E38 RID: 7736
		// (set) Token: 0x06001E39 RID: 7737
		ExchangeServiceBinding ArchiveService { get; set; }

		// Token: 0x06001E3A RID: 7738
		bool IsRemoteArchiveRequest(CallContext callContext);

		// Token: 0x06001E3B RID: 7739
		ServiceCommandBase GetRemoteArchiveServiceCommand(CallContext callContext);
	}
}
