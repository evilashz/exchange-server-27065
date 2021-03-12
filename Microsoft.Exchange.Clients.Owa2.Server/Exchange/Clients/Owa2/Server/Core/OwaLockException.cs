using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000117 RID: 279
	[Serializable]
	public class OwaLockException : OwaTransientException
	{
		// Token: 0x060009BC RID: 2492 RVA: 0x00022BF1 File Offset: 0x00020DF1
		public OwaLockException(string message) : base(message)
		{
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00022BFA File Offset: 0x00020DFA
		public OwaLockException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}
	}
}
