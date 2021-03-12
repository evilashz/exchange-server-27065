using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000EF RID: 239
	internal interface IMessagingDatabaseComponent : IStartableTransportComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600093B RID: 2363
		IMessagingDatabase Database { get; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600093C RID: 2364
		IEnumerable<RoutedQueueBase> Queues { get; }

		// Token: 0x0600093D RID: 2365
		void SetLoadTimeDependencies(IMessagingDatabaseConfig config);

		// Token: 0x0600093E RID: 2366
		IBootLoader CreateBootScanner();

		// Token: 0x0600093F RID: 2367
		RoutedQueueBase GetQueue(NextHopSolutionKey queueNextHopSolutionKey);

		// Token: 0x06000940 RID: 2368
		bool TryGetQueue(NextHopSolutionKey queueNextHopSolutionKey, out RoutedQueueBase queue);

		// Token: 0x06000941 RID: 2369
		RoutedQueueBase GetOrAddQueue(NextHopSolutionKey queueNextHopSolutionKey);
	}
}
