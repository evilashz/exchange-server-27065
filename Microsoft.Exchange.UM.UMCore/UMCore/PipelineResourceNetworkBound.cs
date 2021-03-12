using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002D4 RID: 724
	internal class PipelineResourceNetworkBound : PipelineResource
	{
		// Token: 0x0600160D RID: 5645 RVA: 0x0005E918 File Offset: 0x0005CB18
		public PipelineResourceNetworkBound(int totalCount) : base(totalCount)
		{
			this.perServerResources = new Dictionary<string, int>(2);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0005E938 File Offset: 0x0005CB38
		public override bool TryAcquire(PipelineWorkItem workItem)
		{
			IUMNetworkResource iumnetworkResource = workItem.CurrentStage as IUMNetworkResource;
			ExAssert.RetailAssert(iumnetworkResource != null, "Stages asking for a Network resource must implement IUMNetworkResource");
			bool result;
			lock (this.lockObj)
			{
				int num;
				if (!this.perServerResources.TryGetValue(iumnetworkResource.NetworkResourceId, out num))
				{
					num = AppConfig.Instance.Service.MaxRPCThreadsPerServer;
					this.perServerResources.Add(iumnetworkResource.NetworkResourceId, num);
				}
				if (num > 0 && base.TryAcquire(workItem))
				{
					num = (this.perServerResources[iumnetworkResource.NetworkResourceId] = num - 1);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0005E9F4 File Offset: 0x0005CBF4
		public override void Release(PipelineWorkItem workItem)
		{
			IUMNetworkResource iumnetworkResource = workItem.CurrentStage as IUMNetworkResource;
			ExAssert.RetailAssert(iumnetworkResource != null, "Stages asking for a Network resource must implement IUMNetworkResource");
			lock (this.lockObj)
			{
				Dictionary<string, int> dictionary;
				string networkResourceId;
				(dictionary = this.perServerResources)[networkResourceId = iumnetworkResource.NetworkResourceId] = dictionary[networkResourceId] + 1;
				base.Release(workItem);
			}
		}

		// Token: 0x04000D18 RID: 3352
		public const int MinimumNumberOfResources = 100;

		// Token: 0x04000D19 RID: 3353
		private Dictionary<string, int> perServerResources;

		// Token: 0x04000D1A RID: 3354
		private object lockObj = new object();
	}
}
