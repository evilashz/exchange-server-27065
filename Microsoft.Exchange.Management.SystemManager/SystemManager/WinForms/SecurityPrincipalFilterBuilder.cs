using System;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000266 RID: 614
	internal class SecurityPrincipalFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A88 RID: 6792 RVA: 0x00075414 File Offset: 0x00073614
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			filter = null;
			preArgs = null;
			parameterList = null;
			if (!DBNull.Value.Equals(row["IncludeDomainLocalFrom"]))
			{
				SmtpDomain item = (SmtpDomain)row["IncludeDomainLocalFrom"];
				parameterList = string.Format("-IncludeDomainLocalFrom '{0}'", item.ToQuotationEscapedString());
			}
		}
	}
}
