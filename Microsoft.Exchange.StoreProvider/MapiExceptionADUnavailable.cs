using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000CB RID: 203
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionADUnavailable : MapiRetryableException
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x00012CA9 File Offset: 0x00010EA9
		internal MapiExceptionADUnavailable(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionADUnavailable", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00012CBD File Offset: 0x00010EBD
		private MapiExceptionADUnavailable(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
