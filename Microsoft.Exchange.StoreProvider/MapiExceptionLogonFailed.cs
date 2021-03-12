using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000A2 RID: 162
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionLogonFailed : MapiRetryableException
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x000127DB File Offset: 0x000109DB
		internal MapiExceptionLogonFailed(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionLogonFailed", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000127EF File Offset: 0x000109EF
		private MapiExceptionLogonFailed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
