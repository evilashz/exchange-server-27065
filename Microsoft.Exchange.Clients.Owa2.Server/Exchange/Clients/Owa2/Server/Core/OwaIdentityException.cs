using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000112 RID: 274
	[Serializable]
	public class OwaIdentityException : OwaTransientException
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x00022B5F File Offset: 0x00020D5F
		public OwaIdentityException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00022B69 File Offset: 0x00020D69
		public OwaIdentityException(string message) : base(message)
		{
		}
	}
}
