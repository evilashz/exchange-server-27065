using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200025A RID: 602
	internal class RecipientPreviewFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A6E RID: 6766 RVA: 0x00074CDC File Offset: 0x00072EDC
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			QueryFilter queryFilter = (QueryFilter)row["RecipientPreviewFilter"];
			filter = null;
			preArgs = null;
			parameterList = "-RecipientPreviewFilter '" + LdapFilterBuilder.LdapFilterFromQueryFilter(queryFilter).ToQuotationEscapedString() + "'";
		}
	}
}
