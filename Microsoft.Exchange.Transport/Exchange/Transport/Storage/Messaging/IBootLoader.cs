using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.RemoteDelivery;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000E9 RID: 233
	internal interface IBootLoader : IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x06000877 RID: 2167
		void SetLoadTimeDependencies(ExEventLog eventLogger, IMessagingDatabase database, ShadowRedundancyComponent shadowRedundancyComponent, PoisonMessage poisonComponent, ICategorizer categorizerComponent, QueueManager queueManagerComponent, IBootLoaderConfig bootLoaderConfiguration);

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000878 RID: 2168
		// (remove) Token: 0x06000879 RID: 2169
		event Action OnBootLoadCompleted;
	}
}
