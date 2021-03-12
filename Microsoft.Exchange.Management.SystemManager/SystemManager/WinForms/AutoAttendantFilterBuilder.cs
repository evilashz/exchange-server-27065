using System;
using System.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000252 RID: 594
	internal class AutoAttendantFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A5D RID: 6749 RVA: 0x00074A28 File Offset: 0x00072C28
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			ADObjectId adobjectId = row["UMDialPlan"] as ADObjectId;
			filter = ((adobjectId != null) ? string.Format(" | Filter-PropertyEqualTo -Property UMDialPlan -Value '{0}'", adobjectId.ObjectGuid.ToQuotationEscapedString()) : null);
			parameterList = null;
			preArgs = null;
		}
	}
}
