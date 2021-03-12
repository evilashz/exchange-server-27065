using System;
using Microsoft.Exchange.Data.Metering;
using Microsoft.Exchange.Data.Metering.Throttling;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000345 RID: 837
	internal interface IQueueQuotaComponent : ITransportComponent
	{
		// Token: 0x06002418 RID: 9240
		void SetRunTimeDependencies(IQueueQuotaConfig config, IFlowControlLog log, IQueueQuotaComponentPerformanceCounters perfCounters, IProcessingQuotaComponent processingQuotaComponent, IQueueQuotaObservableComponent submissionQueue, IQueueQuotaObservableComponent deliveryQueue, ICountTracker<MeteredEntity, MeteredCount> metering);

		// Token: 0x06002419 RID: 9241
		void TrackEnteringQueue(IQueueQuotaMailItem mailItem, QueueQuotaResources resource);

		// Token: 0x0600241A RID: 9242
		void TrackExitingQueue(IQueueQuotaMailItem mailItem, QueueQuotaResources resource);

		// Token: 0x0600241B RID: 9243
		bool IsOrganizationOverQuota(string accountForest, Guid externalOrganizationId, string sender, out string reason);

		// Token: 0x0600241C RID: 9244
		bool IsOrganizationOverWarning(string accountForest, Guid externalOrganizationId, string sender, QueueQuotaResources resource);

		// Token: 0x0600241D RID: 9245
		void TimedUpdate();
	}
}
