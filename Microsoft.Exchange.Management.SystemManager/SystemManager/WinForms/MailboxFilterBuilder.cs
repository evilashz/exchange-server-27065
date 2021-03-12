using System;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200025D RID: 605
	internal class MailboxFilterBuilder : ExcludeObjectFilterBuilder
	{
		// Token: 0x06001A74 RID: 6772 RVA: 0x00074DA8 File Offset: 0x00072FA8
		public override void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			base.BuildFilter(out parameterList, out filter, out preArgs, row);
			ExchangeObjectVersion exchangeObjectVersion = null;
			if (!DBNull.Value.Equals(row["MinVersion"]))
			{
				exchangeObjectVersion = (row["MinVersion"] as ExchangeObjectVersion);
			}
			if (exchangeObjectVersion != null)
			{
				string text = string.Format(" | Filter-Mailbox -Value {0}", exchangeObjectVersion.ToInt64());
				filter = ((filter == null) ? text : (filter + text));
			}
		}
	}
}
