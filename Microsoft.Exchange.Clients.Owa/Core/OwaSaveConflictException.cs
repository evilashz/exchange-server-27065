using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001AF RID: 431
	[Serializable]
	public sealed class OwaSaveConflictException : OwaPermanentException
	{
		// Token: 0x06000F01 RID: 3841 RVA: 0x0005E701 File Offset: 0x0005C901
		public OwaSaveConflictException(string message, object thisObject) : base(message, null, thisObject)
		{
		}
	}
}
