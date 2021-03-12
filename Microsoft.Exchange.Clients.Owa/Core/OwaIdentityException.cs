using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001C4 RID: 452
	[Serializable]
	public class OwaIdentityException : OwaTransientException
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x0005E8E0 File Offset: 0x0005CAE0
		public OwaIdentityException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
