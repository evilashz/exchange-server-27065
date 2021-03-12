using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001AE RID: 430
	[Serializable]
	public sealed class OwaNoMailboxException : OwaPermanentException
	{
		// Token: 0x06000F00 RID: 3840 RVA: 0x0005E6F7 File Offset: 0x0005C8F7
		public OwaNoMailboxException(Exception innerException) : base(null, innerException)
		{
		}
	}
}
