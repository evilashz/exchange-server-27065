using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000346 RID: 838
	internal interface IQueueQuotaComponentPerformanceCounters
	{
		// Token: 0x0600241E RID: 9246
		long IncrementThrottledEntities(QueueQuotaEntity entity, Guid organizationId);

		// Token: 0x0600241F RID: 9247
		long DecrementThrottledEntities(QueueQuotaEntity entity, Guid organizationId);

		// Token: 0x06002420 RID: 9248
		long IncrementMessagesRejected(QueueQuotaEntity? entity = null, Guid? organizationId = null);

		// Token: 0x06002421 RID: 9249
		void UpdateOldestThrottledEntity(QueueQuotaEntity entity, TimeSpan throttledInterval, Guid organizationId);

		// Token: 0x06002422 RID: 9250
		void Refresh(QueueQuotaEntity? entity);
	}
}
