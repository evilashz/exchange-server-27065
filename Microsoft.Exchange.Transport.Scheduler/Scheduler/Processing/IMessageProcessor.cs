using System;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200000D RID: 13
	internal interface IMessageProcessor
	{
		// Token: 0x0600002D RID: 45
		void Process(ISchedulableMessage message);
	}
}
