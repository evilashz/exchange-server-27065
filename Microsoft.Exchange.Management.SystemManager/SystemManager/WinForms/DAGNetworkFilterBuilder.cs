using System;
using System.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000254 RID: 596
	internal class DAGNetworkFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A61 RID: 6753 RVA: 0x00074A90 File Offset: 0x00072C90
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			ADObjectId adobjectId = row["Server"] as ADObjectId;
			filter = null;
			parameterList = ((adobjectId != null) ? string.Format(" -Server '{0}'", adobjectId.ToQuotationEscapedString()) : null);
			preArgs = null;
		}
	}
}
