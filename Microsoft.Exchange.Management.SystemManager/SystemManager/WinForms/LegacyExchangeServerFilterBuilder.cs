using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200025B RID: 603
	internal class LegacyExchangeServerFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A70 RID: 6768 RVA: 0x00074D25 File Offset: 0x00072F25
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			filter = "| where {-not($_.IsExchange2007OrLater)}";
			parameterList = null;
			preArgs = null;
		}
	}
}
