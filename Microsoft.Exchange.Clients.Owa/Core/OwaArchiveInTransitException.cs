using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001D5 RID: 469
	[Serializable]
	public class OwaArchiveInTransitException : OwaPermanentException
	{
		// Token: 0x06000F46 RID: 3910 RVA: 0x0005E9F4 File Offset: 0x0005CBF4
		public OwaArchiveInTransitException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
