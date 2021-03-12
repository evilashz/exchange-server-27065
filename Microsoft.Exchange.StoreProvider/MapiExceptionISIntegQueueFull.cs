using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001AF RID: 431
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionISIntegQueueFull : MapiRetryableException
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x00014741 File Offset: 0x00012941
		internal MapiExceptionISIntegQueueFull(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionISIntegQueueFull", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00014755 File Offset: 0x00012955
		private MapiExceptionISIntegQueueFull(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
