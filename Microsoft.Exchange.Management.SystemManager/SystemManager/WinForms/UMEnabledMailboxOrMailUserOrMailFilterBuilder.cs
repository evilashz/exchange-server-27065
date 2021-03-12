using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000253 RID: 595
	internal class UMEnabledMailboxOrMailUserOrMailFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A5F RID: 6751 RVA: 0x00074A77 File Offset: 0x00072C77
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			filter = "| Filter-PropertyEqualTo -Property UMEnabled -Value true";
			parameterList = null;
			preArgs = null;
		}
	}
}
