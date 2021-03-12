using System;
using System.Collections.Generic;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Provisioning.Agent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000026 RID: 38
	[ProvisioningAgentClassFactory]
	internal class OABResourcesManagementAgentClassFactory : IProvisioningAgent
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00006955 File Offset: 0x00004B55
		public IEnumerable<string> GetSupportedCmdlets()
		{
			return this.supportedCmdlets;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000695D File Offset: 0x00004B5D
		public ProvisioningHandler GetCmdletHandler(string cmdletName)
		{
			if (cmdletName.ToLower() == this.supportedCmdlets[0])
			{
				return new MoveOfflineAddressbookProvisioningHandler();
			}
			return new NewOfflineAddressbookProvisioningHandler();
		}

		// Token: 0x0400009B RID: 155
		private readonly string[] supportedCmdlets = new string[]
		{
			"move-offlineaddressbook",
			"new-offlineaddressbook"
		};
	}
}
