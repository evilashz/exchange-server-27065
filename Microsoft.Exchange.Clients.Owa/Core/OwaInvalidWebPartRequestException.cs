using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001C1 RID: 449
	[Serializable]
	public class OwaInvalidWebPartRequestException : OwaPermanentException
	{
		// Token: 0x06000F22 RID: 3874 RVA: 0x0005E88F File Offset: 0x0005CA8F
		public OwaInvalidWebPartRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0005E899 File Offset: 0x0005CA99
		public OwaInvalidWebPartRequestException(string message) : base(message)
		{
		}
	}
}
