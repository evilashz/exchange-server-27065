using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200064A RID: 1610
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TenantPublicFolderConfigurationCache : TenantConfigurationCache<TenantPublicFolderConfiguration>
	{
		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x06004BAF RID: 19375 RVA: 0x00117732 File Offset: 0x00115932
		public static TenantPublicFolderConfigurationCache Instance
		{
			get
			{
				return TenantPublicFolderConfigurationCache.instance;
			}
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x00117739 File Offset: 0x00115939
		private TenantPublicFolderConfigurationCache() : base(TenantPublicFolderConfigurationCache.CacheSizeInBytes, CacheTimeToLive.OrgPropertyCacheTimeToLive, TimeSpan.Zero, null, null)
		{
		}

		// Token: 0x040033F1 RID: 13297
		private static readonly long CacheSizeInBytes = (long)ByteQuantifiedSize.FromMB(10UL).ToBytes();

		// Token: 0x040033F2 RID: 13298
		private static TenantPublicFolderConfigurationCache instance = new TenantPublicFolderConfigurationCache();
	}
}
