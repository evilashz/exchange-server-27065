using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000257 RID: 599
	internal class DelegationFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A67 RID: 6759 RVA: 0x00074B82 File Offset: 0x00072D82
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			filter = " | Filter-Delegation";
			parameterList = null;
			preArgs = null;
		}
	}
}
