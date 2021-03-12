using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000098 RID: 152
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRpcAuthentication : MapiRetryableException
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x000126AF File Offset: 0x000108AF
		internal MapiExceptionRpcAuthentication(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRpcAuthentication", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000126C3 File Offset: 0x000108C3
		private MapiExceptionRpcAuthentication(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
