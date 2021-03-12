using System;
using System.Collections.Generic;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Provisioning.Agent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000022 RID: 34
	[ProvisioningAgentClassFactory]
	internal class MailboxCreationTimeAgentClassFactory : IProvisioningAgent
	{
		// Token: 0x06000116 RID: 278 RVA: 0x000067C8 File Offset: 0x000049C8
		public IEnumerable<string> GetSupportedCmdlets()
		{
			return this.supportedCmdlets;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000067D0 File Offset: 0x000049D0
		public ProvisioningHandler GetCmdletHandler(string cmdletName)
		{
			return new MailboxCreationTimeProvisioningHandler();
		}

		// Token: 0x04000096 RID: 150
		private readonly string[] supportedCmdlets = new string[]
		{
			"new-mailbox",
			"New-SiteMailbox",
			"New-GroupMailbox",
			"new-syncmailbox",
			"set-mailbox",
			"Set-SiteMailbox",
			"set-syncmailbox",
			"enable-mailbox",
			"update-movedmailbox",
			"undo-softdeletedmailbox",
			"undo-syncsoftdeletedmailbox"
		};
	}
}
