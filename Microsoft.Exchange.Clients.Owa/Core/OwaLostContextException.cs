using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001A9 RID: 425
	[Serializable]
	public sealed class OwaLostContextException : OwaTransientException
	{
		// Token: 0x06000EF9 RID: 3833 RVA: 0x0005E6B2 File Offset: 0x0005C8B2
		public OwaLostContextException() : base(null)
		{
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0005E6BB File Offset: 0x0005C8BB
		public OwaLostContextException(string message) : base(message)
		{
		}
	}
}
