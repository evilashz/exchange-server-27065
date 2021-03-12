using System;
using System.Collections.Generic;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Provisioning.Agent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000031 RID: 49
	[ProvisioningAgentClassFactory]
	internal class MailboxPermissionsAgentClassFactory : IProvisioningAgent
	{
		// Token: 0x0600014E RID: 334 RVA: 0x0000819D File Offset: 0x0000639D
		public IEnumerable<string> GetSupportedCmdlets()
		{
			return this.supportedCmdlets;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000081A5 File Offset: 0x000063A5
		public ProvisioningHandler GetCmdletHandler(string cmdletName)
		{
			return new MailboxPermissionsProvisioningHandler();
		}

		// Token: 0x040000A7 RID: 167
		private readonly string[] supportedCmdlets = new string[]
		{
			"new-mailbox",
			"New-SiteMailbox",
			"enable-mailbox",
			"move-mailbox",
			"disable-mailbox",
			"update-movedmailbox",
			"undo-softdeletedmailbox",
			"New-GroupMailbox"
		};
	}
}
