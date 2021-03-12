using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000642 RID: 1602
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OrganizationAvailabilityAddressSpaceCache : OrganizationBaseCache
	{
		// Token: 0x06004B6E RID: 19310 RVA: 0x0011628D File Offset: 0x0011448D
		public OrganizationAvailabilityAddressSpaceCache(OrganizationId organizationId, IConfigurationSession session) : base(organizationId, session)
		{
			this.cache = new Dictionary<string, AvailabilityAddressSpace>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x001162A8 File Offset: 0x001144A8
		public AvailabilityAddressSpace Get(string domain)
		{
			Dictionary<string, AvailabilityAddressSpace> dictionary = this.cache;
			AvailabilityAddressSpace availabilityAddressSpace;
			lock (dictionary)
			{
				if (!dictionary.TryGetValue(domain, out availabilityAddressSpace))
				{
					OrganizationBaseCache.Tracer.TraceDebug<string, OrganizationId>((long)this.GetHashCode(), "AvailabilityAddressSpace cache miss for: {0} in organization {1}", domain, base.OrganizationId);
					availabilityAddressSpace = base.Session.GetAvailabilityAddressSpace(domain);
					dictionary.Add(domain, availabilityAddressSpace);
					OrganizationBaseCache.Tracer.TraceDebug((long)this.GetHashCode(), "Cached AvailabilityAddressSpace {0}", new object[]
					{
						(availabilityAddressSpace == null) ? "<null>" : availabilityAddressSpace.Id
					});
				}
				else
				{
					OrganizationBaseCache.Tracer.TraceDebug<string, object>((long)this.GetHashCode(), "Found AvailabilityAddressSpace in cache for domain {0}: {1}", domain, (availabilityAddressSpace == null) ? "<null>" : availabilityAddressSpace.Id);
				}
			}
			return availabilityAddressSpace;
		}

		// Token: 0x040033D3 RID: 13267
		private Dictionary<string, AvailabilityAddressSpace> cache;
	}
}
