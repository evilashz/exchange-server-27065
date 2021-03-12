using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000099 RID: 153
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoRpcInterface : MapiRetryableException
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x000126CD File Offset: 0x000108CD
		internal MapiExceptionNoRpcInterface(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoRpcInterface", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000126E1 File Offset: 0x000108E1
		private MapiExceptionNoRpcInterface(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
