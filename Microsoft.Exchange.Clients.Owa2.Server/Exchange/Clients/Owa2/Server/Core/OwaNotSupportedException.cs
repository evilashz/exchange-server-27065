using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200011D RID: 285
	[Serializable]
	public sealed class OwaNotSupportedException : OwaPermanentException
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x00022C52 File Offset: 0x00020E52
		public OwaNotSupportedException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00022C5D File Offset: 0x00020E5D
		public OwaNotSupportedException(string message) : base(message)
		{
		}
	}
}
