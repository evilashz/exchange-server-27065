using System;
using System.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000269 RID: 617
	internal class AddressListScopeBuilder : IExchangeScopeBuilder
	{
		// Token: 0x06001A8E RID: 6798 RVA: 0x000754C4 File Offset: 0x000736C4
		public string BuildScope(object scope)
		{
			if (scope == null || string.IsNullOrEmpty(scope.ToString()))
			{
				return "\\";
			}
			if (scope is DataRow)
			{
				scope = (ADObjectId)(scope as DataRow)["Identity"];
			}
			return string.Format("-Container '{0}'", scope.ToQuotationEscapedString());
		}
	}
}
