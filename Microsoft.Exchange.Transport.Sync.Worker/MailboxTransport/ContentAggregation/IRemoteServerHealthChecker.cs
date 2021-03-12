using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Worker.Health;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000204 RID: 516
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IRemoteServerHealthChecker
	{
		// Token: 0x06001164 RID: 4452
		RemoteServerHealthState GetRemoteServerHealthState(ISyncWorkerData subscription);

		// Token: 0x06001165 RID: 4453
		bool IsRemoteServerSlow(SyncEngineState syncEngineState, ISyncWorkerData subscription, out RemoteServerTooSlowException exception);
	}
}
