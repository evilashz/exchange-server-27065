using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200063B RID: 1595
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class OrganizationBaseCache
	{
		// Token: 0x06004B54 RID: 19284 RVA: 0x00115A90 File Offset: 0x00113C90
		public OrganizationBaseCache(OrganizationId organizationId, IConfigurationSession session)
		{
			this.organizationId = organizationId;
			this.session = session;
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x06004B55 RID: 19285 RVA: 0x00115AA6 File Offset: 0x00113CA6
		protected IConfigurationSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x06004B56 RID: 19286 RVA: 0x00115AAE File Offset: 0x00113CAE
		protected OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x040033C0 RID: 13248
		private OrganizationId organizationId;

		// Token: 0x040033C1 RID: 13249
		private IConfigurationSession session;

		// Token: 0x040033C2 RID: 13250
		protected static readonly Trace Tracer = ExTraceGlobals.SystemConfigurationCacheTracer;
	}
}
