using System;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000AA RID: 170
	internal sealed class OwaPerTenantRemoteDomains : TenantConfigurationCacheableItem<DomainContentConfig>
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00014AC4 File Offset: 0x00012CC4
		public override long ItemSize
		{
			get
			{
				if (this.remoteDomainMap == null)
				{
					return 18L;
				}
				long num = 18L;
				return num + (this.estimatedRemoteDomainEntryArraySize + 8L + 4L);
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00014AFC File Offset: 0x00012CFC
		public override void ReadData(IConfigurationSession session)
		{
			ADPagedReader<DomainContentConfig> adpagedReader = session.FindAllPaged<DomainContentConfig>();
			DomainContentConfig[] source = adpagedReader.ReadAllPages();
			RemoteDomainEntry[] array = (from domain in source
			select new RemoteDomainEntry(domain)).ToArray<RemoteDomainEntry>();
			this.remoteDomainMap = new RemoteDomainMap(array);
			int num = 0;
			foreach (RemoteDomainEntry remoteDomainEntry in array)
			{
				num += remoteDomainEntry.EstimateSize;
			}
			this.estimatedRemoteDomainEntryArraySize = (long)num;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00014B7D File Offset: 0x00012D7D
		internal RemoteDomainMap GetRemoteDomainMap(OrganizationId organizationId)
		{
			if (base.OrganizationId == organizationId)
			{
				return this.remoteDomainMap;
			}
			return null;
		}

		// Token: 0x040003B2 RID: 946
		private const int FixedClrObjectOverhead = 18;

		// Token: 0x040003B3 RID: 947
		private long estimatedRemoteDomainEntryArraySize;

		// Token: 0x040003B4 RID: 948
		private RemoteDomainMap remoteDomainMap;
	}
}
