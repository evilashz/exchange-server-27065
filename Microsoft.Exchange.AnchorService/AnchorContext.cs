using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnchorContext : SimpleContext
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000021BF File Offset: 0x000003BF
		public AnchorContext(string applicationName, OrganizationCapability anchorCapability) : base(applicationName)
		{
			this.AnchorCapability = anchorCapability;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021CF File Offset: 0x000003CF
		internal AnchorContext(string applicationName, OrganizationCapability anchorCapability, AnchorConfig config) : base(applicationName, config)
		{
			this.AnchorCapability = anchorCapability;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021E0 File Offset: 0x000003E0
		protected AnchorContext(OrganizationCapability anchorCapability)
		{
			this.AnchorCapability = anchorCapability;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021EF File Offset: 0x000003EF
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021F7 File Offset: 0x000003F7
		public OrganizationCapability AnchorCapability { get; protected set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002200 File Offset: 0x00000400
		public virtual OrganizationCapability ActiveCapability
		{
			get
			{
				return this.AnchorCapability;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002208 File Offset: 0x00000408
		public virtual CacheEntryBase CreateCacheEntry(ADUser user)
		{
			return new CapabilityCacheEntry(this, user);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002214 File Offset: 0x00000414
		public virtual CacheProcessorBase[] CreateCacheComponents(WaitHandle stopEvent)
		{
			return new CacheProcessorBase[]
			{
				new CacheScanner(this, stopEvent),
				new CacheScheduler(this, stopEvent)
			};
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000223D File Offset: 0x0000043D
		public virtual TimedOperationRunner CreateOperationRunner()
		{
			return new TimedOperationRunner(base.Logger, base.Config.GetConfig<TimeSpan>("SlowOperationThreshold"));
		}
	}
}
