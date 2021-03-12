using System;
using System.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200002B RID: 43
	internal class OwaPingStrategy : ProtocolPingStrategyBase
	{
		// Token: 0x06000140 RID: 320 RVA: 0x0000761D File Offset: 0x0000581D
		protected override void PrepareRequest(HttpWebRequest request)
		{
			base.PrepareRequest(request);
			request.Method = "GET";
		}
	}
}
