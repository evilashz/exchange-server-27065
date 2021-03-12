using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000133 RID: 307
	internal class NullScopeBuilder : IExchangeScopeBuilder
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x0002BA55 File Offset: 0x00029C55
		public string BuildScope(object scope)
		{
			return string.Empty;
		}
	}
}
