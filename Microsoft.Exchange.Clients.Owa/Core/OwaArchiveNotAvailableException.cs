using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001D6 RID: 470
	[Serializable]
	public class OwaArchiveNotAvailableException : OwaPermanentException
	{
		// Token: 0x06000F47 RID: 3911 RVA: 0x0005E9FE File Offset: 0x0005CBFE
		public OwaArchiveNotAvailableException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
