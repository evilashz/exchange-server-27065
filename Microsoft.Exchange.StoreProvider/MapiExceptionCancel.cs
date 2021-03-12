using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000AD RID: 173
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCancel : MapiRetryableException
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x00012925 File Offset: 0x00010B25
		internal MapiExceptionCancel(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCancel", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00012939 File Offset: 0x00010B39
		private MapiExceptionCancel(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
