using System;
using Microsoft.Exchange.Transport.Scheduler.Contracts;
using Microsoft.Exchange.Transport.Scheduler.Processing;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000023 RID: 35
	internal interface IProcessingSchedulerComponent : ITransportComponent
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BD RID: 189
		IProcessingScheduler ProcessingScheduler { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BE RID: 190
		IProcessingSchedulerAdmin ProcessingSchedulerAdmin { get; }

		// Token: 0x060000BF RID: 191
		void SetLoadTimeDependencies(ITransportConfiguration transportConfiguration, IMessageDepotComponent messageDepotComponent, IMessageProcessor messageProcessor, IMessagingDatabaseComponent messagingDatabaseComponent);

		// Token: 0x060000C0 RID: 192
		void Pause();

		// Token: 0x060000C1 RID: 193
		void Resume();
	}
}
