using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000186 RID: 390
	[DataContract]
	internal class InstantSearchNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x06000E08 RID: 3592 RVA: 0x00034E3D File Offset: 0x0003303D
		public InstantSearchNotificationPayload(string subscriptionId, InstantSearchPayloadType instantSearchPayload)
		{
			base.SubscriptionId = subscriptionId;
			this.InstantSearchPayload = instantSearchPayload;
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00034E53 File Offset: 0x00033053
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x00034E5B File Offset: 0x0003305B
		[DataMember]
		public InstantSearchPayloadType InstantSearchPayload { get; private set; }
	}
}
