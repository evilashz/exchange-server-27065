using System;
using System.Collections.Generic;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Provisioning.Agent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000025 RID: 37
	[ProvisioningAgentClassFactory]
	internal class MailboxPlansAgentClassFactory : IProvisioningAgent
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00006912 File Offset: 0x00004B12
		public IEnumerable<string> GetSupportedCmdlets()
		{
			return this.supportedCmdlets;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000691A File Offset: 0x00004B1A
		public ProvisioningHandler GetCmdletHandler(string cmdletName)
		{
			return new MailboxPlansProvisioningHandler();
		}

		// Token: 0x0400009A RID: 154
		private readonly string[] supportedCmdlets = new string[]
		{
			"new-mailbox",
			"New-SiteMailbox",
			"New-GroupMailbox",
			"new-syncmailbox",
			"enable-mailbox",
			"update-movedmailbox",
			"undo-softdeletedmailbox",
			"undo-syncsoftdeletedmailbox",
			"new-moverequest"
		};
	}
}
