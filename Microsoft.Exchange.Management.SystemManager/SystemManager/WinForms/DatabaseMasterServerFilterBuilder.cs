using System;
using System.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000260 RID: 608
	internal class DatabaseMasterServerFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A7E RID: 6782 RVA: 0x000751D8 File Offset: 0x000733D8
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			ADObjectId item = null;
			if (!DBNull.Value.Equals(row["DatabaseIdentity"]))
			{
				item = (ADObjectId)row["DatabaseIdentity"];
			}
			string text = null;
			if (!DBNull.Value.Equals(row["ExcludeServer"]))
			{
				text = (string)row["ExcludeServer"];
			}
			filter = " | Filter-DatabaseMasterServer";
			if (text != null)
			{
				filter += string.Format(" | Filter-PropertyNotEqualTo -Property 'Fqdn' -Value '{0}'", text.ToQuotationEscapedString());
			}
			preArgs = null;
			parameterList = string.Format("-Identity '{0}'", item.ToQuotationEscapedString());
		}
	}
}
