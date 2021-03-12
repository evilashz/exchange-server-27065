using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200009A RID: 154
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionExiting : MapiRetryableException
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x000126EB File Offset: 0x000108EB
		internal MapiExceptionExiting(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionExiting", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000126FF File Offset: 0x000108FF
		private MapiExceptionExiting(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
