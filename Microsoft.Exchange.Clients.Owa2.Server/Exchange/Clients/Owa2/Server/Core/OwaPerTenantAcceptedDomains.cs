using System;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000A9 RID: 169
	internal sealed class OwaPerTenantAcceptedDomains : TenantConfigurationCacheableItem<AcceptedDomain>
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x000149F0 File Offset: 0x00012BF0
		public override long ItemSize
		{
			get
			{
				if (this.acceptedDomainMap == null)
				{
					return 18L;
				}
				long num = 18L;
				return num + (this.estimatedAcceptedDomainEntryArraySize + 8L + 4L);
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00014A2C File Offset: 0x00012C2C
		public override void ReadData(IConfigurationSession session)
		{
			ADPagedReader<AcceptedDomain> adpagedReader = session.FindAllPaged<AcceptedDomain>();
			AcceptedDomain[] source = adpagedReader.ReadAllPages();
			AcceptedDomainEntry[] array = (from domain in source
			select new AcceptedDomainEntry(domain, base.OrganizationId)).ToArray<AcceptedDomainEntry>();
			this.acceptedDomainMap = new AcceptedDomainMap(array);
			int num = 0;
			foreach (AcceptedDomainEntry acceptedDomainEntry in array)
			{
				num += acceptedDomainEntry.EstimatedSize;
			}
			this.estimatedAcceptedDomainEntryArraySize = (long)num;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00014A9C File Offset: 0x00012C9C
		internal AcceptedDomainMap GetAcceptedDomainMap(OrganizationId organizationId, out bool scopedToOrganization)
		{
			scopedToOrganization = false;
			if (base.OrganizationId == organizationId)
			{
				scopedToOrganization = true;
				return this.acceptedDomainMap;
			}
			return null;
		}

		// Token: 0x040003AF RID: 943
		private const int FixedClrObjectOverhead = 18;

		// Token: 0x040003B0 RID: 944
		private long estimatedAcceptedDomainEntryArraySize;

		// Token: 0x040003B1 RID: 945
		private AcceptedDomainMap acceptedDomainMap;
	}
}
