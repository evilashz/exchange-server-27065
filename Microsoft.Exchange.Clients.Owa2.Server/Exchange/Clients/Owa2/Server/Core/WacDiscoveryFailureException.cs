using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	public sealed class WacDiscoveryFailureException : OwaPermanentException
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x00022DA2 File Offset: 0x00020FA2
		public WacDiscoveryFailureException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00022DAD File Offset: 0x00020FAD
		public WacDiscoveryFailureException(string message) : base(message)
		{
		}
	}
}
