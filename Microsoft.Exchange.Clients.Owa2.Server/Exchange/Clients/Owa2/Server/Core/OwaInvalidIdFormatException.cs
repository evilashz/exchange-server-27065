using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	public sealed class OwaInvalidIdFormatException : OwaPermanentException
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x00022B72 File Offset: 0x00020D72
		public OwaInvalidIdFormatException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00022B7D File Offset: 0x00020D7D
		public OwaInvalidIdFormatException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00022B88 File Offset: 0x00020D88
		public OwaInvalidIdFormatException(string message) : base(message)
		{
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00022B91 File Offset: 0x00020D91
		public OwaInvalidIdFormatException() : base(null)
		{
		}
	}
}
