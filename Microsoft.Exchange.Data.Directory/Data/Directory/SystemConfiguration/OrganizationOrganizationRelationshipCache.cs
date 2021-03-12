using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200063F RID: 1599
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OrganizationOrganizationRelationshipCache : OrganizationBaseCache
	{
		// Token: 0x06004B64 RID: 19300 RVA: 0x00115EF7 File Offset: 0x001140F7
		public OrganizationOrganizationRelationshipCache(OrganizationId organizationId, IConfigurationSession session) : base(organizationId, session)
		{
			this.cache = new Dictionary<string, OrganizationRelationship>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x00115F14 File Offset: 0x00114114
		public OrganizationRelationship Get(string domain)
		{
			Dictionary<string, OrganizationRelationship> dictionary = this.cache;
			OrganizationRelationship organizationRelationship;
			lock (dictionary)
			{
				if (!dictionary.TryGetValue(domain, out organizationRelationship))
				{
					OrganizationBaseCache.Tracer.TraceDebug<string, OrganizationId>((long)this.GetHashCode(), "OrganizationRelationship cache miss for: {0} in organization {1}", domain, base.OrganizationId);
					organizationRelationship = base.Session.GetOrganizationRelationship(domain);
					dictionary.Add(domain, organizationRelationship);
					OrganizationBaseCache.Tracer.TraceDebug((long)this.GetHashCode(), "Cached OrganizationRelationship {0}", new object[]
					{
						(organizationRelationship == null) ? "<null>" : organizationRelationship.Id
					});
				}
				else
				{
					OrganizationBaseCache.Tracer.TraceDebug<string, object>((long)this.GetHashCode(), "Found OrganizationRelationship in cache for domain {0}: {1}", domain, (organizationRelationship == null) ? "<null>" : organizationRelationship.Id);
				}
			}
			return organizationRelationship;
		}

		// Token: 0x040033CB RID: 13259
		private Dictionary<string, OrganizationRelationship> cache;
	}
}
