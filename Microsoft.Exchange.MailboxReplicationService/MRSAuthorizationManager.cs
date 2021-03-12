using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000043 RID: 67
	public class MRSAuthorizationManager : AuthorizationManagerBase
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x000171C8 File Offset: 0x000153C8
		internal override AdminRoleDefinition[] ComputeAdminRoles(IRootOrganizationRecipientSession recipientSession, ITopologyConfigurationSession configSession)
		{
			string containerDN = configSession.ConfigurationNamingContext.ToDNString();
			ADGroup adgroup = recipientSession.ResolveWellKnownGuid<ADGroup>(WellKnownGuid.EmaWkGuid, containerDN);
			return new AdminRoleDefinition[]
			{
				new AdminRoleDefinition(adgroup.Sid, "RecipientAdmins"),
				new AdminRoleDefinition(recipientSession.GetExchangeServersUsgSid(), "ExchangeServers"),
				new AdminRoleDefinition(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), "LocalSystem"),
				new AdminRoleDefinition(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), "BuiltinAdmins")
			};
		}
	}
}
