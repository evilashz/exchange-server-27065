using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007CD RID: 1997
	internal abstract class CookieManagerFactory
	{
		// Token: 0x1700234C RID: 9036
		// (get) Token: 0x0600634E RID: 25422 RVA: 0x00158A3E File Offset: 0x00156C3E
		// (set) Token: 0x0600634F RID: 25423 RVA: 0x00158A54 File Offset: 0x00156C54
		public static CookieManagerFactory Default
		{
			get
			{
				CookieManagerFactory result;
				if ((result = CookieManagerFactory.defaultInstance) == null)
				{
					result = (CookieManagerFactory.defaultInstance = new CookieManagerFactory.DefaultCookieManagerFactory());
				}
				return result;
			}
			set
			{
				CookieManagerFactory.defaultInstance = value;
			}
		}

		// Token: 0x06006350 RID: 25424
		public abstract CookieManager GetCookieManager(ForwardSyncCookieType cookieType, string serviceInstanceName, int maxCookieHistoryCount, TimeSpan cookieHistoryInterval);

		// Token: 0x0400423E RID: 16958
		private static CookieManagerFactory defaultInstance;

		// Token: 0x020007CE RID: 1998
		internal sealed class DefaultCookieManagerFactory : CookieManagerFactory
		{
			// Token: 0x06006352 RID: 25426 RVA: 0x00158A64 File Offset: 0x00156C64
			public override CookieManager GetCookieManager(ForwardSyncCookieType cookieType, string serviceInstanceName, int maxCookieHistoryCount, TimeSpan cookieHistoryInterval)
			{
				SyncServiceInstance syncServiceInstance = ServiceInstanceId.GetSyncServiceInstance(serviceInstanceName);
				switch (cookieType)
				{
				case ForwardSyncCookieType.RecipientIncremental:
					if (syncServiceInstance != null && syncServiceInstance.IsMultiObjectCookieEnabled)
					{
						return new MsoMultiObjectCookieManager(serviceInstanceName, maxCookieHistoryCount, cookieHistoryInterval, cookieType);
					}
					return new MsoRecipientMainStreamCookieManager(serviceInstanceName, maxCookieHistoryCount, cookieHistoryInterval);
				case ForwardSyncCookieType.CompanyIncremental:
					if (syncServiceInstance != null && syncServiceInstance.IsMultiObjectCookieEnabled)
					{
						return new MsoMultiObjectCookieManager(serviceInstanceName, maxCookieHistoryCount, cookieHistoryInterval, cookieType);
					}
					return new MsoCompanyMainStreamCookieManager(serviceInstanceName, maxCookieHistoryCount, cookieHistoryInterval);
				default:
					throw new InvalidOperationException("Cookie type not supported");
				}
			}
		}
	}
}
