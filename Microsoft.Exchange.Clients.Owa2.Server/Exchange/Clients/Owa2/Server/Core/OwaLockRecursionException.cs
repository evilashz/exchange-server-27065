using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public sealed class OwaLockRecursionException : OwaLockException
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x00022C05 File Offset: 0x00020E05
		public OwaLockRecursionException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}
	}
}
