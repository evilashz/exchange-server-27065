using System;
using System.Collections.Generic;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000289 RID: 649
	internal class WlmResourceCache<TResource> where TResource : ResourceBase
	{
		// Token: 0x06001FEA RID: 8170 RVA: 0x00043B23 File Offset: 0x00041D23
		public WlmResourceCache(Func<Guid, WorkloadType, TResource> createResourceDelegate)
		{
			this.createResourceDelegate = createResourceDelegate;
			this.resourceCaches = new Dictionary<WorkloadType, ResourceCache<TResource>>(4);
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x00043B4C File Offset: 0x00041D4C
		public void ForEach(Action<TResource> action)
		{
			lock (this.locker)
			{
				foreach (ResourceCache<TResource> resourceCache in this.resourceCaches.Values)
				{
					foreach (TResource obj2 in resourceCache.Values)
					{
						action(obj2);
					}
				}
			}
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x00043C30 File Offset: 0x00041E30
		public TResource GetInstance(Guid resourceID, WorkloadType workloadType)
		{
			ResourceCache<TResource> resourceCache;
			lock (this.locker)
			{
				if (!this.resourceCaches.TryGetValue(workloadType, out resourceCache))
				{
					resourceCache = new ResourceCache<TResource>((Guid id) => this.createResourceDelegate(id, workloadType));
					this.resourceCaches.Add(workloadType, resourceCache);
				}
			}
			return resourceCache.GetInstance(resourceID);
		}

		// Token: 0x04000CE4 RID: 3300
		private readonly Dictionary<WorkloadType, ResourceCache<TResource>> resourceCaches;

		// Token: 0x04000CE5 RID: 3301
		private readonly object locker = new object();

		// Token: 0x04000CE6 RID: 3302
		private Func<Guid, WorkloadType, TResource> createResourceDelegate;
	}
}
