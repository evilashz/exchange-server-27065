using System;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000022 RID: 34
	internal class AuthzException : Exception
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00004CAD File Offset: 0x00002EAD
		public AuthzException(string message) : base(message)
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004CB6 File Offset: 0x00002EB6
		public AuthzException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
