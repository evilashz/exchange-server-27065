using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000119 RID: 281
	[Serializable]
	public sealed class OwaLockTimeoutException : OwaLockException
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x00022C10 File Offset: 0x00020E10
		public OwaLockTimeoutException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00022C1B File Offset: 0x00020E1B
		public OwaLockTimeoutException(string message) : base(message)
		{
		}
	}
}
