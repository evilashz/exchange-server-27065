using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001B9 RID: 441
	[Serializable]
	public class OwaUserNotIMEnabledException : OwaPermanentException
	{
		// Token: 0x06000F10 RID: 3856 RVA: 0x0005E7AA File Offset: 0x0005C9AA
		public OwaUserNotIMEnabledException(string message) : base(message)
		{
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0005E7B3 File Offset: 0x0005C9B3
		public OwaUserNotIMEnabledException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
