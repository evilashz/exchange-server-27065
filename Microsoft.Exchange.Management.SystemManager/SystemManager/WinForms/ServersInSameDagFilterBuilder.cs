using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000261 RID: 609
	internal class ServersInSameDagFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A80 RID: 6784 RVA: 0x00075280 File Offset: 0x00073480
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			string item = null;
			if (!DBNull.Value.Equals(row["DagMemberServer"]))
			{
				item = (string)row["DagMemberServer"];
			}
			filter = string.Format(" | Filter-ServersInSameDag -dagMemberServer '{0}'", item.ToQuotationEscapedString());
			preArgs = null;
			parameterList = null;
		}
	}
}
