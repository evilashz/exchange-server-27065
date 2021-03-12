using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001CD RID: 461
	[Serializable]
	public class OwaSharedFromOlderVersionException : OwaPermanentException
	{
		// Token: 0x06000F38 RID: 3896 RVA: 0x0005E969 File Offset: 0x0005CB69
		public OwaSharedFromOlderVersionException() : base(null)
		{
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0005E972 File Offset: 0x0005CB72
		public OwaSharedFromOlderVersionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0005E97C File Offset: 0x0005CB7C
		public OwaSharedFromOlderVersionException(string message) : base(message)
		{
		}
	}
}
