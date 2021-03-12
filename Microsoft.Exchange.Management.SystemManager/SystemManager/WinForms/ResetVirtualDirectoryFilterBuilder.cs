using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200025F RID: 607
	internal class ResetVirtualDirectoryFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A7C RID: 6780 RVA: 0x0007518D File Offset: 0x0007338D
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			filter = null;
			preArgs = null;
			parameterList = ((!DBNull.Value.Equals(row["Server"])) ? string.Format("-Server '{0}'", row["Server"].ToQuotationEscapedString()) : null);
		}
	}
}
