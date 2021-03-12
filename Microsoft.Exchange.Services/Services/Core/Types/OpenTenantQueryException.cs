using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000833 RID: 2099
	internal class OpenTenantQueryException : Exception
	{
		// Token: 0x06003C84 RID: 15492 RVA: 0x000D5E07 File Offset: 0x000D4007
		public OpenTenantQueryException(string message) : base(message)
		{
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x000D5E10 File Offset: 0x000D4010
		public OpenTenantQueryException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
