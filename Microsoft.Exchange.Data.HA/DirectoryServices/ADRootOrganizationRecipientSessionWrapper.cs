using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADRootOrganizationRecipientSessionWrapper : IADRootOrganizationRecipientSession
	{
		// Token: 0x06000176 RID: 374 RVA: 0x000045E3 File Offset: 0x000027E3
		public static ADRootOrganizationRecipientSessionWrapper CreateWrapper(IRootOrganizationRecipientSession session)
		{
			ExAssert.RetailAssert(session != null, "'session' instance to wrap must not be null!");
			return new ADRootOrganizationRecipientSessionWrapper(session);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000045FC File Offset: 0x000027FC
		private ADRootOrganizationRecipientSessionWrapper(IRootOrganizationRecipientSession session)
		{
			ExAssert.RetailAssert(session != null, "'session' instance to wrap must not be null!");
			this.m_session = session;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000461C File Offset: 0x0000281C
		public SecurityIdentifier GetExchangeServersUsgSid()
		{
			return this.m_session.GetExchangeServersUsgSid();
		}

		// Token: 0x0400008B RID: 139
		private IRootOrganizationRecipientSession m_session;
	}
}
