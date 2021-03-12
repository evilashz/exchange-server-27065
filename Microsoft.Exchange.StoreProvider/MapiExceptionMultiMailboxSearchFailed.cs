using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000DC RID: 220
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMultiMailboxSearchFailed : MapiRetryableException
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00012EB8 File Offset: 0x000110B8
		internal MapiExceptionMultiMailboxSearchFailed(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMultiMailboxSearchFailed", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00012ECC File Offset: 0x000110CC
		private MapiExceptionMultiMailboxSearchFailed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
