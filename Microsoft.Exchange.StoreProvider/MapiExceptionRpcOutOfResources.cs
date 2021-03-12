using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000D2 RID: 210
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRpcOutOfResources : MapiRetryableException
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x00012D8C File Offset: 0x00010F8C
		internal MapiExceptionRpcOutOfResources(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRpcOutOfResources", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00012DA0 File Offset: 0x00010FA0
		private MapiExceptionRpcOutOfResources(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
