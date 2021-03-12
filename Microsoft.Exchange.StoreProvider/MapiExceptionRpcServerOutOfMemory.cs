using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000D1 RID: 209
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRpcServerOutOfMemory : MapiRetryableException
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x00012D6E File Offset: 0x00010F6E
		internal MapiExceptionRpcServerOutOfMemory(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRpcServerOutOfMemory", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00012D82 File Offset: 0x00010F82
		private MapiExceptionRpcServerOutOfMemory(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
