using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000D0 RID: 208
	internal class SpeechRecoProxyRequestHandler : BEServerCookieProxyRequestHandler<WebServicesService>
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0002DBE5 File Offset: 0x0002BDE5
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.InternalNLBBypass;
			}
		}
	}
}
