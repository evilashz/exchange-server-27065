using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000199 RID: 409
	[Serializable]
	public sealed class OwaInvalidIdFormatException : OwaPermanentException
	{
		// Token: 0x06000ECF RID: 3791 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		public OwaInvalidIdFormatException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0005E3BA File Offset: 0x0005C5BA
		public OwaInvalidIdFormatException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0005E3C5 File Offset: 0x0005C5C5
		public OwaInvalidIdFormatException(string message) : base(message)
		{
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0005E3CE File Offset: 0x0005C5CE
		public OwaInvalidIdFormatException() : base(null)
		{
		}
	}
}
