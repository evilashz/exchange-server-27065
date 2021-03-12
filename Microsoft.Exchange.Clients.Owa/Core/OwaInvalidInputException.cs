using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200019E RID: 414
	[Serializable]
	public sealed class OwaInvalidInputException : OwaPermanentException
	{
		// Token: 0x06000EDB RID: 3803 RVA: 0x0005E427 File Offset: 0x0005C627
		public OwaInvalidInputException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0005E432 File Offset: 0x0005C632
		public OwaInvalidInputException(string message) : base(message)
		{
		}
	}
}
