using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200018E RID: 398
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRpcBufferTooSmall : MapiPermanentException
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x0001435A File Offset: 0x0001255A
		internal MapiExceptionRpcBufferTooSmall(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRpcBufferTooSmall", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001436E File Offset: 0x0001256E
		private MapiExceptionRpcBufferTooSmall(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
