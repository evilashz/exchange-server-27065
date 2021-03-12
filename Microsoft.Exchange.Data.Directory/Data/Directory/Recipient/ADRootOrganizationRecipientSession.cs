using System;
using System.Net;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000209 RID: 521
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ADRootOrganizationRecipientSession : ADRecipientObjectSession, IRootOrganizationRecipientSession, IRecipientSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x06001B4D RID: 6989 RVA: 0x00073468 File Offset: 0x00071668
		public ADRootOrganizationRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0007347B File Offset: 0x0007167B
		public ADRootOrganizationRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope) : this(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
			base.CheckConfigScopeParameter(configScope);
			base.ConfigScope = configScope;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0007349E File Offset: 0x0007169E
		public SecurityIdentifier GetExchangeServersUsgSid()
		{
			return base.GetWellKnownExchangeGroupSid(WellKnownGuid.ExSWkGuid);
		}
	}
}
