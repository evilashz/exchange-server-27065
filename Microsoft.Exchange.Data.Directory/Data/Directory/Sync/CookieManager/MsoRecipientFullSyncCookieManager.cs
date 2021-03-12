using System;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007E0 RID: 2016
	internal sealed class MsoRecipientFullSyncCookieManager : MsoFullSyncCookieManager
	{
		// Token: 0x060063DE RID: 25566 RVA: 0x0015AC00 File Offset: 0x00158E00
		public MsoRecipientFullSyncCookieManager(Guid contextId) : base(contextId)
		{
		}

		// Token: 0x060063DF RID: 25567 RVA: 0x0015AC09 File Offset: 0x00158E09
		protected override MultiValuedProperty<byte[]> RetrievePersistedPageTokens(MsoTenantCookieContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			return container.MsoForwardSyncRecipientCookie;
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x0015AC20 File Offset: 0x00158E20
		protected override void LogPersistPageTokenEvent()
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_TenantFullSyncRecipientPageTokenPersisted, base.ContextId.ToString(), new object[]
			{
				base.ContextId.ToString()
			});
		}

		// Token: 0x060063E1 RID: 25569 RVA: 0x0015AC6C File Offset: 0x00158E6C
		protected override void LogClearPageTokenEvent()
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_TenantFullSyncRecipientPageTokenCleared, base.ContextId.ToString(), new object[]
			{
				base.ContextId.ToString()
			});
		}
	}
}
