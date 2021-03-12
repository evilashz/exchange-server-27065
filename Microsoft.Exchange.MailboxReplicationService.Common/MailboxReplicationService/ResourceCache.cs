using System;
using Microsoft.Exchange.Collections.TimeoutCache;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000282 RID: 642
	internal class ResourceCache<TResource> : ExactTimeoutCache<Guid, TResource> where TResource : ResourceBase
	{
		// Token: 0x06001FA4 RID: 8100 RVA: 0x00043498 File Offset: 0x00041698
		public ResourceCache(Func<Guid, TResource> createResourceDelegate) : base(null, new ShouldRemoveDelegate<Guid, TResource>(ResourceCache<TResource>.ShouldRemoveResource), null, 10000, false)
		{
			this.createResourceDelegate = createResourceDelegate;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000434C8 File Offset: 0x000416C8
		public void ForEach(Action<TResource> action)
		{
			lock (this.locker)
			{
				foreach (TResource obj2 in base.Values)
				{
					action(obj2);
				}
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x00043544 File Offset: 0x00041744
		public TResource GetInstance(Guid resourceID)
		{
			TResource tresource;
			lock (this.locker)
			{
				if (!base.TryGetValue(resourceID, out tresource))
				{
					tresource = this.createResourceDelegate(resourceID);
					base.TryInsertSliding(resourceID, tresource, ResourceCache<TResource>.Timeout);
				}
			}
			return tresource;
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000435A8 File Offset: 0x000417A8
		private static bool ShouldRemoveResource(Guid resourceID, TResource resource)
		{
			return resource.Utilization == 0;
		}

		// Token: 0x04000CC1 RID: 3265
		private static readonly TimeSpan Timeout = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000CC2 RID: 3266
		private object locker = new object();

		// Token: 0x04000CC3 RID: 3267
		private Func<Guid, TResource> createResourceDelegate;
	}
}
