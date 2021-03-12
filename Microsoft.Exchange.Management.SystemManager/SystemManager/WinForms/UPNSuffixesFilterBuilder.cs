using System;
using System.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000264 RID: 612
	internal class UPNSuffixesFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A86 RID: 6790 RVA: 0x0007539C File Offset: 0x0007359C
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			ADObjectId adobjectId = null;
			if (row.Table.Columns.Contains("OrganizationalUnit") && !DBNull.Value.Equals(row["OrganizationalUnit"]))
			{
				adobjectId = (ADObjectId)row["OrganizationalUnit"];
			}
			filter = null;
			preArgs = null;
			parameterList = ((adobjectId != null) ? string.Format("-OrganizationalUnit '{0}'", adobjectId.ToQuotationEscapedString()) : null);
		}
	}
}
