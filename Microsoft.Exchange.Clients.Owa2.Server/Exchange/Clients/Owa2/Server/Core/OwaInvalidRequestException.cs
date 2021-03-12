using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000116 RID: 278
	[Serializable]
	public sealed class OwaInvalidRequestException : OwaPermanentException
	{
		// Token: 0x060009B8 RID: 2488 RVA: 0x00022BC9 File Offset: 0x00020DC9
		public OwaInvalidRequestException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00022BD4 File Offset: 0x00020DD4
		public OwaInvalidRequestException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00022BDF File Offset: 0x00020DDF
		public OwaInvalidRequestException(string message) : base(message)
		{
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00022BE8 File Offset: 0x00020DE8
		public OwaInvalidRequestException() : base(null)
		{
		}
	}
}
