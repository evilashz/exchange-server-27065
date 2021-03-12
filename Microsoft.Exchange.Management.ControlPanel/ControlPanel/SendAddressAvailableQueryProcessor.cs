using System;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000381 RID: 897
	internal class SendAddressAvailableQueryProcessor : EcpCmdletQueryProcessor
	{
		// Token: 0x0600305D RID: 12381 RVA: 0x000936B4 File Offset: 0x000918B4
		internal override bool? IsInRoleCmdlet(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			SendAddress sendAddress = new SendAddress();
			PowerShellResults<SendAddressRow> list = sendAddress.GetList(null, null);
			if (list.Succeeded)
			{
				return new bool?(list.Output.Length > 2);
			}
			base.LogCmdletError(list, "SendAddressAvailable");
			return new bool?(false);
		}

		// Token: 0x04002361 RID: 9057
		internal const string RoleName = "SendAddressAvailable";
	}
}
