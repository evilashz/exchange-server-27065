using System;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002DF RID: 735
	internal sealed class PerTenantCacheTracer : DefaultCacheTracer<OrganizationId>
	{
		// Token: 0x06002078 RID: 8312 RVA: 0x0007C401 File Offset: 0x0007A601
		public PerTenantCacheTracer(Trace tracer, string cacheName) : base(tracer, cacheName)
		{
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x0007C40B File Offset: 0x0007A60B
		protected override string GetKeyString(OrganizationId key)
		{
			if (key == null)
			{
				return string.Empty;
			}
			if (key.ConfigurationUnit != null)
			{
				return key.ConfigurationUnit.DistinguishedName;
			}
			return "FirstOrg";
		}
	}
}
