using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001B3 RID: 435
	[Serializable]
	public class OwaSpellCheckerException : OwaPermanentException
	{
		// Token: 0x06000F05 RID: 3845 RVA: 0x0005E72B File Offset: 0x0005C92B
		public OwaSpellCheckerException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0005E735 File Offset: 0x0005C935
		public OwaSpellCheckerException(string message) : base(message)
		{
		}
	}
}
