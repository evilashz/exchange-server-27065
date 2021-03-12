using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000268 RID: 616
	internal class WithPrimarySmtpAddressRecipientFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A8C RID: 6796 RVA: 0x000754AC File Offset: 0x000736AC
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			filter = " | Filter-Recipient";
			parameterList = null;
			preArgs = null;
		}
	}
}
