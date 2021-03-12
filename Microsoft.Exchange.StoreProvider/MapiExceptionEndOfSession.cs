using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000A8 RID: 168
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionEndOfSession : MapiRetryableException
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x0001288F File Offset: 0x00010A8F
		internal MapiExceptionEndOfSession(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionEndOfSession", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000128A3 File Offset: 0x00010AA3
		private MapiExceptionEndOfSession(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
