using System;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DDA RID: 3546
	internal interface IDisposableResponse<TResponse> : IDisposable
	{
		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x06005AAC RID: 23212
		// (set) Token: 0x06005AAD RID: 23213
		TResponse Response { get; set; }
	}
}
