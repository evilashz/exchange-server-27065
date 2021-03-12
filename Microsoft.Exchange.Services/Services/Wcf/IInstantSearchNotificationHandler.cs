using System;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009CA RID: 2506
	public interface IInstantSearchNotificationHandler
	{
		// Token: 0x060046E9 RID: 18153
		void DeliverInstantSearchPayload(InstantSearchPayloadType instantSearchPayload);
	}
}
