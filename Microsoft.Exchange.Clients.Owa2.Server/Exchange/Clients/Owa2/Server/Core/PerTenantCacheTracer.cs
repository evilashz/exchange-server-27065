using System;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000AF RID: 175
	internal sealed class PerTenantCacheTracer : DefaultCacheTracer<OrganizationId>
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x00014FA5 File Offset: 0x000131A5
		public PerTenantCacheTracer(Trace tracer, string cacheName) : base(tracer, cacheName)
		{
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00014FAF File Offset: 0x000131AF
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
