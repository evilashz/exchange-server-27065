using System;
using System.Collections.Generic;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Provisioning.Agent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000024 RID: 36
	[ProvisioningAgentClassFactory]
	internal class MailboxResourcesManagementAgentClassFactory : IProvisioningAgent
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00006896 File Offset: 0x00004A96
		public IEnumerable<string> GetSupportedCmdlets()
		{
			return this.supportedCmdlets;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000689E File Offset: 0x00004A9E
		public ProvisioningHandler GetCmdletHandler(string cmdletName)
		{
			return new MailboxProvisioningHandler();
		}

		// Token: 0x04000099 RID: 153
		private readonly string[] supportedCmdlets = new string[]
		{
			"new-mailbox",
			"New-SiteMailbox",
			"New-GroupMailbox",
			"new-syncmailbox",
			"enable-mailbox",
			"move-mailbox",
			"enable-mailuser",
			"undo-softdeletedmailbox",
			"undo-syncsoftdeletedmailbox"
		};
	}
}
