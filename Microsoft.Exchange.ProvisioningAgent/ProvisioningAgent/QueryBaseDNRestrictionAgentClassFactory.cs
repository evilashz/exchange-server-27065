using System;
using System.Collections.Generic;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Provisioning.Agent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200003A RID: 58
	[ProvisioningAgentClassFactory]
	internal class QueryBaseDNRestrictionAgentClassFactory : IProvisioningAgent
	{
		// Token: 0x06000187 RID: 391 RVA: 0x000094B4 File Offset: 0x000076B4
		static QueryBaseDNRestrictionAgentClassFactory()
		{
			QueryBaseDNRestrictionAgentClassFactory.supportedCmdletsForUpdate.CopyTo(QueryBaseDNRestrictionAgentClassFactory.allSupportedCmdlets, 0);
			QueryBaseDNRestrictionAgentClassFactory.supportedCmdletsForOnComplete.CopyTo(QueryBaseDNRestrictionAgentClassFactory.allSupportedCmdlets, QueryBaseDNRestrictionAgentClassFactory.supportedCmdletsForUpdate.Length);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009572 File Offset: 0x00007772
		IEnumerable<string> IProvisioningAgent.GetSupportedCmdlets()
		{
			return QueryBaseDNRestrictionAgentClassFactory.allSupportedCmdlets;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000957C File Offset: 0x0000777C
		ProvisioningHandler IProvisioningAgent.GetCmdletHandler(string cmdletName)
		{
			foreach (string text in QueryBaseDNRestrictionAgentClassFactory.supportedCmdletsForOnComplete)
			{
				if (text.Equals(cmdletName, StringComparison.OrdinalIgnoreCase))
				{
					return new QueryBaseDNRestrictionNewObjectProvisioningHandler();
				}
			}
			return new QueryBaseDNRestrictionModifyObjectProvisioningHandler();
		}

		// Token: 0x040000B9 RID: 185
		private static readonly string[] supportedCmdletsForOnComplete = new string[]
		{
			"new-mailbox",
			"New-SiteMailbox",
			"New-GroupMailbox",
			"new-syncmailbox",
			"undo-softdeletedmailbox",
			"undo-syncsoftdeletedmailbox"
		};

		// Token: 0x040000BA RID: 186
		private static readonly string[] supportedCmdletsForUpdate = new string[]
		{
			"set-mailbox",
			"Set-SiteMailbox",
			"set-syncmailbox",
			"enable-mailbox"
		};

		// Token: 0x040000BB RID: 187
		private static readonly string[] allSupportedCmdlets = new string[QueryBaseDNRestrictionAgentClassFactory.supportedCmdletsForOnComplete.Length + QueryBaseDNRestrictionAgentClassFactory.supportedCmdletsForUpdate.Length];
	}
}
