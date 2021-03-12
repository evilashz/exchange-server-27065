using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ResourceAdmissionControlManager : LazyLookupExactTimeoutCache<ResourceKey, IResourceAdmissionControl>
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00008062 File Offset: 0x00006262
		public ResourceAdmissionControlManager() : this(null)
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000806B File Offset: 0x0000626B
		public ResourceAdmissionControlManager(ResourceAvailabilityChangeDelegate availabilityChangeDelegate) : this(availabilityChangeDelegate, null)
		{
			this.availabilityChangeDelegate = availabilityChangeDelegate;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000807C File Offset: 0x0000627C
		public ResourceAdmissionControlManager(ResourceAvailabilityChangeDelegate availabilityChangeDelegate, Func<ResourceAdmissionControlManager, ResourceKey, IResourceAdmissionControl> initializationDelegate) : this(availabilityChangeDelegate, initializationDelegate, TimeSpan.FromMinutes(10.0))
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008094 File Offset: 0x00006294
		public ResourceAdmissionControlManager(ResourceAvailabilityChangeDelegate availabilityChangeDelegate, Func<ResourceAdmissionControlManager, ResourceKey, IResourceAdmissionControl> initializationDelegate, TimeSpan unusedAdmissionExpiration) : base(10000, false, unusedAdmissionExpiration, TimeSpan.MaxValue, CacheFullBehavior.FailNew)
		{
			this.availabilityChangeDelegate = availabilityChangeDelegate;
			this.initializationDelegate = initializationDelegate;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000080B7 File Offset: 0x000062B7
		protected override bool HandleShouldRemove(ResourceKey resourceKey, IResourceAdmissionControl admissionControl)
		{
			return !admissionControl.IsAcquired;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000080CC File Offset: 0x000062CC
		protected override IResourceAdmissionControl CreateOnCacheMiss(ResourceKey resourceKey, ref bool shouldAdd)
		{
			if (this.initializationDelegate != null)
			{
				return this.initializationDelegate(this, resourceKey);
			}
			return new DefaultAdmissionControl(resourceKey, delegate(ResourceKey key)
			{
				base.Remove(key);
			}, this.availabilityChangeDelegate, "ResourceAdmissionControlManager");
		}

		// Token: 0x040000ED RID: 237
		private const string AdmissionControlOwner = "ResourceAdmissionControlManager";

		// Token: 0x040000EE RID: 238
		private const int ArbitraryCacheLimit = 10000;

		// Token: 0x040000EF RID: 239
		private ResourceAvailabilityChangeDelegate availabilityChangeDelegate;

		// Token: 0x040000F0 RID: 240
		private Func<ResourceAdmissionControlManager, ResourceKey, IResourceAdmissionControl> initializationDelegate;
	}
}
