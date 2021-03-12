using System;
using Microsoft.Exchange.AnchorService;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000005 RID: 5
	internal class CacheEntryProxy : ICacheEntry
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002667 File Offset: 0x00000867
		public CacheEntryProxy(CacheEntryBase cacheEntryBase)
		{
			this.CacheEntryBase = cacheEntryBase;
			this.ADSessionProxy = new ADSessionProxy(cacheEntryBase.ADProvider);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002687 File Offset: 0x00000887
		public string OrgName
		{
			get
			{
				if (this.CacheEntryBase.OrganizationId.OrganizationalUnit == null)
				{
					return "<null>";
				}
				return this.CacheEntryBase.OrganizationId.OrganizationalUnit.Name;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000026B6 File Offset: 0x000008B6
		public string ExternalDirectoryOrganizationId
		{
			get
			{
				return this.CacheEntryBase.OrganizationId.ToExternalDirectoryOrganizationId();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000026C8 File Offset: 0x000008C8
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000026D0 File Offset: 0x000008D0
		public CacheEntryBase CacheEntryBase { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000026D9 File Offset: 0x000008D9
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000026E1 File Offset: 0x000008E1
		public IADSession ADSessionProxy { get; private set; }
	}
}
