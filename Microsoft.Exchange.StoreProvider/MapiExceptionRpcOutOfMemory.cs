using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000D0 RID: 208
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRpcOutOfMemory : MapiRetryableException
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x00012D50 File Offset: 0x00010F50
		internal MapiExceptionRpcOutOfMemory(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRpcOutOfMemory", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00012D64 File Offset: 0x00010F64
		private MapiExceptionRpcOutOfMemory(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
