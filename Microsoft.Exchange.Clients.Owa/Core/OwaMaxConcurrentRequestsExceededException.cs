using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001D0 RID: 464
	[Serializable]
	public class OwaMaxConcurrentRequestsExceededException : OwaTransientException
	{
		// Token: 0x06000F41 RID: 3905 RVA: 0x0005E9C2 File Offset: 0x0005CBC2
		public OwaMaxConcurrentRequestsExceededException(string message) : base(message)
		{
		}
	}
}
