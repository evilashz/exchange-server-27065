using System;
using System.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200026A RID: 618
	internal class OrganizationalUnitScopeBuilder : IExchangeScopeBuilder
	{
		// Token: 0x06001A90 RID: 6800 RVA: 0x00075520 File Offset: 0x00073720
		public string BuildScope(object scope)
		{
			if (scope == null || string.IsNullOrEmpty(scope.ToString()))
			{
				return "-SingleNodeOnly";
			}
			if (scope is DataRow)
			{
				ADObjectId item = (ADObjectId)(scope as DataRow)["Identity"];
				return string.Format("-Identity '{0}' -SingleNodeOnly", item.ToQuotationEscapedString());
			}
			return string.Format("-Identity '{0}'", scope.ToQuotationEscapedString());
		}
	}
}
