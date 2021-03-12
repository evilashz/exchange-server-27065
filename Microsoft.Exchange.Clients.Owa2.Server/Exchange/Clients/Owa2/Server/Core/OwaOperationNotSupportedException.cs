using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200011E RID: 286
	[Serializable]
	public sealed class OwaOperationNotSupportedException : OwaPermanentException
	{
		// Token: 0x060009C6 RID: 2502 RVA: 0x00022C66 File Offset: 0x00020E66
		public OwaOperationNotSupportedException(string message) : base(message)
		{
		}
	}
}
