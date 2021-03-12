using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200019A RID: 410
	[Serializable]
	public sealed class OwaInvalidRequestException : OwaPermanentException
	{
		// Token: 0x06000ED3 RID: 3795 RVA: 0x0005E3D7 File Offset: 0x0005C5D7
		public OwaInvalidRequestException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0005E3E2 File Offset: 0x0005C5E2
		public OwaInvalidRequestException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0005E3ED File Offset: 0x0005C5ED
		public OwaInvalidRequestException(string message) : base(message)
		{
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0005E3F6 File Offset: 0x0005C5F6
		public OwaInvalidRequestException() : base(null)
		{
		}
	}
}
