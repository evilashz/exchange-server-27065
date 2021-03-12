using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001A2 RID: 418
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionEventNotFound : MapiRetryableException
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x000145C0 File Offset: 0x000127C0
		internal MapiExceptionEventNotFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionEventNotFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000145D4 File Offset: 0x000127D4
		private MapiExceptionEventNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
