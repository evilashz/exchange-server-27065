using System;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007E2 RID: 2018
	internal sealed class MsoRecipientMainStreamCookieManager : MsoMainStreamCookieManager
	{
		// Token: 0x060063E4 RID: 25572 RVA: 0x0015AD2D File Offset: 0x00158F2D
		public MsoRecipientMainStreamCookieManager(string serviceInstanceName, int maxCookieHistoryCount, TimeSpan cookieHistoryInterval) : base(serviceInstanceName, maxCookieHistoryCount, cookieHistoryInterval)
		{
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x0015AD38 File Offset: 0x00158F38
		protected override MultiValuedProperty<byte[]> RetrievePersistedCookies(MsoMainStreamCookieContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			return container.MsoForwardSyncRecipientCookie;
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x0015AD50 File Offset: 0x00158F50
		protected override void LogPersistCookieEvent()
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_RecipientMainStreamCookiePersisted, base.ServiceInstanceName, new object[]
			{
				base.ServiceInstanceName
			});
		}
	}
}
