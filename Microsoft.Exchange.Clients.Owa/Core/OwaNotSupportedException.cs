using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001A1 RID: 417
	[Serializable]
	public sealed class OwaNotSupportedException : OwaPermanentException
	{
		// Token: 0x06000EE0 RID: 3808 RVA: 0x0005E458 File Offset: 0x0005C658
		public OwaNotSupportedException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0005E463 File Offset: 0x0005C663
		public OwaNotSupportedException(string message) : base(message)
		{
		}
	}
}
