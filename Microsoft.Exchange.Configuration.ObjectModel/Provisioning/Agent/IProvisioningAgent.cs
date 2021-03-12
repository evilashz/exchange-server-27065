using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Provisioning.Agent
{
	// Token: 0x02000200 RID: 512
	public interface IProvisioningAgent
	{
		// Token: 0x060011F3 RID: 4595
		IEnumerable<string> GetSupportedCmdlets();

		// Token: 0x060011F4 RID: 4596
		ProvisioningHandler GetCmdletHandler(string cmdletName);
	}
}
