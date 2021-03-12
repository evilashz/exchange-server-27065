using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200063C RID: 1596
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OrganizationFederatedOrganizationIdCache : OrganizationBaseCache
	{
		// Token: 0x06004B58 RID: 19288 RVA: 0x00115AC2 File Offset: 0x00113CC2
		public OrganizationFederatedOrganizationIdCache(OrganizationId organizationId, IConfigurationSession session) : base(organizationId, session)
		{
		}

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x06004B59 RID: 19289 RVA: 0x00115ACC File Offset: 0x00113CCC
		public FederatedOrganizationId Value
		{
			get
			{
				this.PopulateCacheIfNeeded();
				return this.value;
			}
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x00115ADC File Offset: 0x00113CDC
		private void PopulateCacheIfNeeded()
		{
			if (!this.cached)
			{
				OrganizationBaseCache.Tracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "Cache miss, get the FederatedOrganizationId id for: {0}", base.OrganizationId);
				this.value = base.Session.GetFederatedOrganizationId(base.Session.SessionSettings.CurrentOrganizationId);
				this.cached = true;
			}
		}

		// Token: 0x040033C3 RID: 13251
		private FederatedOrganizationId value;

		// Token: 0x040033C4 RID: 13252
		private bool cached;
	}
}
