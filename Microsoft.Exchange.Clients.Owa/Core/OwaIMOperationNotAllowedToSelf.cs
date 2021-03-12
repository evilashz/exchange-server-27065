using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001BA RID: 442
	[Serializable]
	public class OwaIMOperationNotAllowedToSelf : OwaPermanentException
	{
		// Token: 0x06000F12 RID: 3858 RVA: 0x0005E7BD File Offset: 0x0005C9BD
		public OwaIMOperationNotAllowedToSelf(string message) : base(message)
		{
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0005E7C6 File Offset: 0x0005C9C6
		public OwaIMOperationNotAllowedToSelf(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
