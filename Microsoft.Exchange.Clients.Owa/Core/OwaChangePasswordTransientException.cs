using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001C5 RID: 453
	[Serializable]
	public class OwaChangePasswordTransientException : OwaTransientException
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x0005E8EA File Offset: 0x0005CAEA
		public OwaChangePasswordTransientException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
