using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000008 RID: 8
	public class Directory : IDirectory
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000247C File Offset: 0x0000067C
		public SecurityIdentifier GetExchangeServersUsgSid()
		{
			SecurityIdentifier exchangeServersSid = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 31, "GetExchangeServersUsgSid", "f:\\15.00.1497\\sources\\dev\\mapimt\\src\\RpcHttpModules\\Directory.cs");
				exchangeServersSid = rootOrganizationRecipientSession.GetExchangeServersUsgSid();
			});
			return exchangeServersSid;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024B0 File Offset: 0x000006B0
		public bool AllowsTokenSerializationBy(WindowsIdentity windowsIdentity)
		{
			bool result;
			using (ClientSecurityContext clientSecurityContext = new ClientSecurityContext(windowsIdentity))
			{
				result = LocalServer.AllowsTokenSerializationBy(clientSecurityContext);
			}
			return result;
		}
	}
}
