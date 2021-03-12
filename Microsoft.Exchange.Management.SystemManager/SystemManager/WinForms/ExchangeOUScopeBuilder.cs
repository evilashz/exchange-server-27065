using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000134 RID: 308
	public class ExchangeOUScopeBuilder : IExchangeScopeBuilder
	{
		// Token: 0x06000C2F RID: 3119 RVA: 0x0002BA64 File Offset: 0x00029C64
		public string BuildScope(object scope)
		{
			if (scope != null && !string.IsNullOrEmpty(scope.ToString()))
			{
				return string.Format("-OrganizationalUnit '{0}'", scope.ToQuotationEscapedString());
			}
			return null;
		}
	}
}
