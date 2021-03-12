using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200011A RID: 282
	[Serializable]
	public sealed class OwaMethodArgumentException : OwaPermanentException
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x00022C24 File Offset: 0x00020E24
		public OwaMethodArgumentException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
