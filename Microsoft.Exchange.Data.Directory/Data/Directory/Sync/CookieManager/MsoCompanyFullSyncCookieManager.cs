using System;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007D3 RID: 2003
	internal sealed class MsoCompanyFullSyncCookieManager : MsoFullSyncCookieManager
	{
		// Token: 0x06006375 RID: 25461 RVA: 0x00158FFA File Offset: 0x001571FA
		public MsoCompanyFullSyncCookieManager(Guid contextId) : base(contextId)
		{
		}

		// Token: 0x06006376 RID: 25462 RVA: 0x00159003 File Offset: 0x00157203
		protected override MultiValuedProperty<byte[]> RetrievePersistedPageTokens(MsoTenantCookieContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			return container.MsoForwardSyncNonRecipientCookie;
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x0015901C File Offset: 0x0015721C
		protected override void LogPersistPageTokenEvent()
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_TenantFullSyncCompanyPageTokenPersisted, base.ContextId.ToString(), new object[]
			{
				base.ContextId.ToString()
			});
		}

		// Token: 0x06006378 RID: 25464 RVA: 0x00159068 File Offset: 0x00157268
		protected override void LogClearPageTokenEvent()
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_TenantFullSyncCompanyPageTokenCleared, base.ContextId.ToString(), new object[]
			{
				base.ContextId.ToString()
			});
		}
	}
}
