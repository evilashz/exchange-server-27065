using System;
using System.Collections.Generic;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Provisioning.Agent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000027 RID: 39
	[ProvisioningAgentClassFactory]
	internal class ProvisioningPolicyAgentClassFactory : IProvisioningAgent
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00006A5F File Offset: 0x00004C5F
		public IEnumerable<string> GetSupportedCmdlets()
		{
			return this.supportedCmdlets;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006A67 File Offset: 0x00004C67
		public ProvisioningHandler GetCmdletHandler(string cmdletName)
		{
			return new ADPolicyProvisioningHandler();
		}

		// Token: 0x0400009C RID: 156
		private readonly string[] supportedCmdlets = new string[]
		{
			"enable-Mailbox",
			"enable-MailContact",
			"Enable-MailPublicFolder",
			"enable-MailUser",
			"enable-RemoteMailbox",
			"enable-DistributionGroup",
			"new-Mailbox",
			"New-SiteMailbox",
			"New-GroupMailbox",
			"new-MailContact",
			"new-MailUser",
			"new-RemoteMailbox",
			"new-SyncMailbox",
			"new-SyncMailContact",
			"New-SyncMailPublicFolder",
			"new-SyncMailUser",
			"new-DistributionGroup",
			"new-SyncDistributionGroup",
			"new-DynamicDistributionGroup",
			"undo-softdeletedmailbox",
			"undo-syncsoftdeletedmailbox",
			"undo-syncsoftdeletedmailuser"
		};
	}
}
