using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.MessageDepot;

namespace Microsoft.Exchange.Transport.Scheduler.Contracts
{
	// Token: 0x02000008 RID: 8
	internal interface ISchedulableMessage
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17
		TransportMessageId Id { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18
		MessageEnvelope MessageEnvelope { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19
		DateTime SubmitTime { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20
		IEnumerable<IMessageScope> Scopes { get; }
	}
}
