using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000008 RID: 8
	public interface IExchangeWebMethodResponse
	{
		// Token: 0x06000047 RID: 71
		ResponseType GetResponseType();

		// Token: 0x06000048 RID: 72
		ResponseCodeType GetErrorCodeToLog();
	}
}
