using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200034D RID: 845
	internal class QueueQuotaComponentPerformanceCountersWrapper : IQueueQuotaComponentPerformanceCounters
	{
		// Token: 0x06002496 RID: 9366 RVA: 0x0008BE88 File Offset: 0x0008A088
		public QueueQuotaComponentPerformanceCountersWrapper(TimeSpan rejectedWindow, TimeSpan rejectBucketSize)
		{
			this.rejectedWindow = rejectedWindow;
			this.rejectBucketSize = rejectBucketSize;
			this.rejectedRecently = new SlidingTotalCounter[Enum.GetNames(typeof(QueueQuotaEntity)).Length + 3];
			this.rejectedTotalIndex = this.rejectedRecently.Length - 1;
			this.rejectedSafeTenantIndex = this.rejectedRecently.Length - 2;
			this.rejectedOutlookTenantIndex = this.rejectedRecently.Length - 3;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x0008BF02 File Offset: 0x0008A102
		public long IncrementThrottledEntities(QueueQuotaEntity entity, Guid organizationId)
		{
			return this.GetInstance(new QueueQuotaEntity?(entity), new Guid?(organizationId)).EntitiesInThrottledState.Increment();
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x0008BF20 File Offset: 0x0008A120
		public long DecrementThrottledEntities(QueueQuotaEntity entity, Guid organizationId)
		{
			return this.GetInstance(new QueueQuotaEntity?(entity), new Guid?(organizationId)).EntitiesInThrottledState.Decrement();
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x0008BF40 File Offset: 0x0008A140
		public long IncrementMessagesRejected(QueueQuotaEntity? entity = null, Guid? organizationId = null)
		{
			int num;
			QueueQuotaComponentPerfCountersInstance instance = this.GetInstance(entity, organizationId, out num);
			if (this.rejectedRecently[num] == null)
			{
				lock (this.syncObject)
				{
					if (this.rejectedRecently[num] == null)
					{
						this.rejectedRecently[num] = new SlidingTotalCounter(this.rejectedWindow, this.rejectBucketSize);
					}
				}
			}
			instance.MessagesTempRejectedRecently.RawValue = this.rejectedRecently[num].AddValue(1L);
			return instance.MessagesTempRejectedTotal.Increment();
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x0008BFD8 File Offset: 0x0008A1D8
		public void UpdateOldestThrottledEntity(QueueQuotaEntity entity, TimeSpan throttledInterval, Guid organizationId)
		{
			this.GetInstance(new QueueQuotaEntity?(entity), new Guid?(organizationId)).OldestThrottledEntityIntervalInSeconds.RawValue = (long)throttledInterval.TotalSeconds;
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x0008C000 File Offset: 0x0008A200
		public void Refresh(QueueQuotaEntity? entity = null)
		{
			int num;
			QueueQuotaComponentPerfCountersInstance instance = this.GetInstance(entity, new Guid?(Guid.Empty), out num);
			SlidingTotalCounter slidingTotalCounter = this.rejectedRecently[num];
			if (slidingTotalCounter != null)
			{
				instance.MessagesTempRejectedRecently.RawValue = slidingTotalCounter.Sum;
			}
			if (entity == QueueQuotaEntity.Organization)
			{
				instance = this.GetInstance(entity, new Guid?(MultiTenantTransport.SafeTenantId), out num);
				slidingTotalCounter = this.rejectedRecently[num];
				if (slidingTotalCounter != null)
				{
					instance.MessagesTempRejectedRecently.RawValue = slidingTotalCounter.Sum;
				}
			}
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x0008C088 File Offset: 0x0008A288
		private QueueQuotaComponentPerfCountersInstance GetInstance(QueueQuotaEntity? entity, Guid? organizationId)
		{
			int num;
			return this.GetInstance(entity, organizationId, out num);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x0008C0A0 File Offset: 0x0008A2A0
		private QueueQuotaComponentPerfCountersInstance GetInstance(QueueQuotaEntity? entity, Guid? organizationId, out int entityIndex)
		{
			QueueQuotaComponentPerfCountersInstance result;
			if (entity == null)
			{
				result = QueueQuotaComponentPerfCounters.TotalInstance;
				entityIndex = this.rejectedTotalIndex;
			}
			else if (entity == QueueQuotaEntity.Organization && organizationId == MultiTenantTransport.SafeTenantId)
			{
				result = QueueQuotaComponentPerfCounters.GetInstance("SafeTenantOrg");
				entityIndex = this.rejectedSafeTenantIndex;
			}
			else if (entity == QueueQuotaEntity.Organization && organizationId == TemplateTenantConfiguration.TemplateTenantExternalDirectoryOrganizationIdGuid)
			{
				result = QueueQuotaComponentPerfCounters.GetInstance("OutlookTenantOrg");
				entityIndex = this.rejectedOutlookTenantIndex;
			}
			else
			{
				result = QueueQuotaComponentPerfCounters.GetInstance(entity.ToString());
				entityIndex = (int)entity.Value;
			}
			return result;
		}

		// Token: 0x040012EB RID: 4843
		private const string SafeTenantOrgInstance = "SafeTenantOrg";

		// Token: 0x040012EC RID: 4844
		private const string OutlookTenantOrgInstance = "OutlookTenantOrg";

		// Token: 0x040012ED RID: 4845
		private readonly TimeSpan rejectedWindow;

		// Token: 0x040012EE RID: 4846
		private readonly TimeSpan rejectBucketSize;

		// Token: 0x040012EF RID: 4847
		private readonly int rejectedTotalIndex;

		// Token: 0x040012F0 RID: 4848
		private readonly int rejectedSafeTenantIndex;

		// Token: 0x040012F1 RID: 4849
		private readonly int rejectedOutlookTenantIndex;

		// Token: 0x040012F2 RID: 4850
		private SlidingTotalCounter[] rejectedRecently;

		// Token: 0x040012F3 RID: 4851
		private object syncObject = new object();
	}
}
