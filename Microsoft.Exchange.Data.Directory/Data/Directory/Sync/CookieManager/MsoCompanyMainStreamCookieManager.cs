using System;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007D7 RID: 2007
	internal sealed class MsoCompanyMainStreamCookieManager : MsoMainStreamCookieManager
	{
		// Token: 0x0600638B RID: 25483 RVA: 0x00159627 File Offset: 0x00157827
		public MsoCompanyMainStreamCookieManager(string serviceInstanceName, int maxCookieHistoryCount, TimeSpan cookieHistoryInterval) : base(serviceInstanceName, maxCookieHistoryCount, cookieHistoryInterval)
		{
		}

		// Token: 0x0600638C RID: 25484 RVA: 0x00159632 File Offset: 0x00157832
		protected override MultiValuedProperty<byte[]> RetrievePersistedCookies(MsoMainStreamCookieContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			return container.MsoForwardSyncNonRecipientCookie;
		}

		// Token: 0x0600638D RID: 25485 RVA: 0x00159648 File Offset: 0x00157848
		protected override void LogPersistCookieEvent()
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_CompanyMainStreamCookiePersisted, base.ServiceInstanceName, new object[]
			{
				base.ServiceInstanceName
			});
		}
	}
}
