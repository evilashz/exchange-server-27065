using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000256 RID: 598
	internal class DatabaseFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A65 RID: 6757 RVA: 0x00074B48 File Offset: 0x00072D48
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			filter = (((bool)row["IsExchange2007OrLaterOnly"]) ? " | Filter-PropertyEqualOrGreaterThan -Property ExchangeVersion -Value 0x72000000" : null);
			parameterList = null;
			preArgs = null;
		}
	}
}
