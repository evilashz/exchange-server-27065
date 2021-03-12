using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002D3 RID: 723
	internal class PipelineResource
	{
		// Token: 0x06001608 RID: 5640 RVA: 0x0005E754 File Offset: 0x0005C954
		protected PipelineResource(int totalCount)
		{
			this.totalCount = totalCount;
			this.numberOfResourcesRemaining = totalCount;
			this.acquisitionTimes = new Dictionary<Guid, ExDateTime>(this.totalCount);
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0005E794 File Offset: 0x0005C994
		public static PipelineResource CreatePipelineResource(PipelineDispatcher.PipelineResourceType resourceType)
		{
			int processorCount = Environment.ProcessorCount;
			switch (resourceType)
			{
			case PipelineDispatcher.PipelineResourceType.LowPriorityCpuBound:
				return new PipelineResource(processorCount * AppConfig.Instance.Service.PipelineScaleFactorCPU);
			case PipelineDispatcher.PipelineResourceType.CpuBound:
				return new PipelineResource(processorCount * AppConfig.Instance.Service.PipelineScaleFactorCPU);
			case PipelineDispatcher.PipelineResourceType.NetworkBound:
				return new PipelineResourceNetworkBound(Math.Max(100, processorCount * AppConfig.Instance.Service.PipelineScaleFactorNetworkBound));
			default:
				throw new InvalidOperationException("Unknown PipelineResourceType.");
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0005E813 File Offset: 0x0005CA13
		public int TotalCount
		{
			get
			{
				return this.totalCount;
			}
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0005E81C File Offset: 0x0005CA1C
		public virtual bool TryAcquire(PipelineWorkItem workItem)
		{
			bool result;
			lock (this.lockObj)
			{
				if (this.numberOfResourcesRemaining > 0)
				{
					this.numberOfResourcesRemaining--;
					this.acquisitionTimes[workItem.WorkId] = ExDateTime.UtcNow;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0005E88C File Offset: 0x0005CA8C
		public virtual void Release(PipelineWorkItem workItem)
		{
			lock (this.lockObj)
			{
				this.numberOfResourcesRemaining++;
				ExAssert.RetailAssert(this.acquisitionTimes.ContainsKey(workItem.WorkId), "Request to release resource for workitem={0} that doesn't have that resource!", new object[]
				{
					workItem.WorkId
				});
				this.acquisitionTimes.Remove(workItem.WorkId);
			}
		}

		// Token: 0x04000D14 RID: 3348
		private Dictionary<Guid, ExDateTime> acquisitionTimes;

		// Token: 0x04000D15 RID: 3349
		private int numberOfResourcesRemaining;

		// Token: 0x04000D16 RID: 3350
		private int totalCount;

		// Token: 0x04000D17 RID: 3351
		private object lockObj = new object();
	}
}
