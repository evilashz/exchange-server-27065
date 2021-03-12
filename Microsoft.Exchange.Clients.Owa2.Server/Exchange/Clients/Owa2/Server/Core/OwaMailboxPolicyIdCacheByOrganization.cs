using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000E6 RID: 230
	internal sealed class OwaMailboxPolicyIdCacheByOrganization : LazyLookupTimeoutCache<OrganizationId, ADObjectId>
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x0001BB1D File Offset: 0x00019D1D
		private OwaMailboxPolicyIdCacheByOrganization() : base(5, 1000, false, TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(60.0))
		{
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001BB48 File Offset: 0x00019D48
		internal static OwaMailboxPolicyIdCacheByOrganization Instance
		{
			get
			{
				return OwaMailboxPolicyIdCacheByOrganization.instance;
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001BB4F File Offset: 0x00019D4F
		protected override ADObjectId CreateOnCacheMiss(OrganizationId key, ref bool shouldAdd)
		{
			shouldAdd = true;
			return this.GetPolicyIdFromAD(key);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001BB5C File Offset: 0x00019D5C
		private ADObjectId GetPolicyIdFromAD(OrganizationId key)
		{
			OwaMailboxPolicy defaultOwaMailboxPolicy = OwaSegmentationSettings.GetDefaultOwaMailboxPolicy(key);
			if (defaultOwaMailboxPolicy == null)
			{
				return null;
			}
			return defaultOwaMailboxPolicy.Id;
		}

		// Token: 0x04000526 RID: 1318
		private const int OwaMailboxPolicyIdCacheByOrganizationBucketSize = 1000;

		// Token: 0x04000527 RID: 1319
		private const int OwaMailboxPolicyIdCacheByOrganizationBuckets = 5;

		// Token: 0x04000528 RID: 1320
		private static OwaMailboxPolicyIdCacheByOrganization instance = new OwaMailboxPolicyIdCacheByOrganization();
	}
}
