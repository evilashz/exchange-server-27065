using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ActiveAnchorContext : AnchorContext
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000225A File Offset: 0x0000045A
		public ActiveAnchorContext(string applicationName, OrganizationCapability anchorCapability, OrganizationCapability activeCapability) : base(applicationName, anchorCapability)
		{
			this.activeCapability = activeCapability;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000226B File Offset: 0x0000046B
		public override OrganizationCapability ActiveCapability
		{
			get
			{
				return this.activeCapability;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002273 File Offset: 0x00000473
		public override CacheEntryBase CreateCacheEntry(ADUser user)
		{
			return new ToggleableCapabilityCacheEntry(this, user);
		}

		// Token: 0x04000006 RID: 6
		private OrganizationCapability activeCapability;
	}
}
