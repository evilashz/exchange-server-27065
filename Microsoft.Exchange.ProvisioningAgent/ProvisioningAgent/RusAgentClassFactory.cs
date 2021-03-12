using System;
using System.Collections.Generic;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Provisioning.Agent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000028 RID: 40
	[ProvisioningAgentClassFactory]
	internal class RusAgentClassFactory : IProvisioningAgent
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00006A70 File Offset: 0x00004C70
		static RusAgentClassFactory()
		{
			List<string> list = new List<string>();
			list.AddRange(AddressBookRUSProvisioningHandler.SupportedTasks);
			list.AddRange(EmailAddressPolicyRUSProvisioningHandler.SupportedTasks);
			list.AddRange(RusProvisioningHandlerForRemove.SupportedTasks);
			list.AddRange(DefaultRUSProvisioningHandler.SupportedTasks);
			RusAgentClassFactory.supportedCmdlets = list.ToArray();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006AC2 File Offset: 0x00004CC2
		public IEnumerable<string> GetSupportedCmdlets()
		{
			return RusAgentClassFactory.supportedCmdlets;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006B08 File Offset: 0x00004D08
		public ProvisioningHandler GetCmdletHandler(string cmdletName)
		{
			if (Array.Exists<string>(AddressBookRUSProvisioningHandler.SupportedTasks, (string value) => StringComparer.InvariantCultureIgnoreCase.Equals(value, cmdletName)))
			{
				return new AddressBookRUSProvisioningHandler();
			}
			if (Array.Exists<string>(EmailAddressPolicyRUSProvisioningHandler.SupportedTasks, (string value) => StringComparer.InvariantCultureIgnoreCase.Equals(value, cmdletName)))
			{
				return new EmailAddressPolicyRUSProvisioningHandler();
			}
			if (Array.Exists<string>(RusProvisioningHandlerForRemove.SupportedTasks, (string value) => value.Equals(cmdletName, StringComparison.InvariantCultureIgnoreCase)))
			{
				return new RusProvisioningHandlerForRemove();
			}
			return new DefaultRUSProvisioningHandler();
		}

		// Token: 0x0400009D RID: 157
		private static readonly string[] supportedCmdlets;
	}
}
