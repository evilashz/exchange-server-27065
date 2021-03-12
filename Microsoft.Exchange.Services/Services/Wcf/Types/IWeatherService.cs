using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B5B RID: 2907
	internal interface IWeatherService
	{
		// Token: 0x06005271 RID: 21105
		string Get(Uri weatherServiceUri);

		// Token: 0x06005272 RID: 21106
		void VerifyServiceAvailability(CallContext callContext);

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x06005273 RID: 21107
		string PartnerId { get; }

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06005274 RID: 21108
		string BaseUrl { get; }
	}
}
