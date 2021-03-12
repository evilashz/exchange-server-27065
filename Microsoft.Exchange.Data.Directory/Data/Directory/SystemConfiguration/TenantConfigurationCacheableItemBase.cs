using System;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200062E RID: 1582
	internal abstract class TenantConfigurationCacheableItemBase : CachableItem
	{
		// Token: 0x06004AFF RID: 19199
		public abstract bool InitializeWithoutRegistration(IConfigurationSession adSession, bool allowExceptions);

		// Token: 0x06004B00 RID: 19200
		public abstract bool TryInitialize(OrganizationId organizationId, CacheNotificationHandler cacheNotificationHandler, object state);

		// Token: 0x06004B01 RID: 19201
		public abstract bool Initialize(OrganizationId organizationId, CacheNotificationHandler cacheNotificationHandler, object state);

		// Token: 0x06004B02 RID: 19202 RVA: 0x00114CA9 File Offset: 0x00112EA9
		public virtual void UnregisterChangeNotification()
		{
		}
	}
}
