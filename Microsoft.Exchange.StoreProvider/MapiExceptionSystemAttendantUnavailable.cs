using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000B5 RID: 181
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSystemAttendantUnavailable : MapiRetryableException
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x00012A15 File Offset: 0x00010C15
		internal MapiExceptionSystemAttendantUnavailable(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSystemAttendantUnavailable", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00012A29 File Offset: 0x00010C29
		private MapiExceptionSystemAttendantUnavailable(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
