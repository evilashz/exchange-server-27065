using System;
using System.Data;
using System.Text;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000262 RID: 610
	internal class PFDBInstalledExchangeServerFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A82 RID: 6786 RVA: 0x000752DC File Offset: 0x000734DC
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			long num = 0L;
			if (!DBNull.Value.Equals(row["MinVersion"]))
			{
				num = (long)row["MinVersion"];
			}
			string text = null;
			if (!DBNull.Value.Equals(row["ExcludeServer"]))
			{
				text = (string)row["ExcludeServer"];
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(" | Filter-PublicFolderInstalledExchangeServer -minVersion {0}", num);
			if (text != null)
			{
				stringBuilder.AppendFormat(" -excludeServer '{0}'", text.ToQuotationEscapedString());
			}
			filter = stringBuilder.ToString();
			preArgs = null;
			parameterList = null;
		}
	}
}
