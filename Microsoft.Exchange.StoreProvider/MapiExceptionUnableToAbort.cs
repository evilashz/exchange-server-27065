using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000A5 RID: 165
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnableToAbort : MapiRetryableException
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x00012835 File Offset: 0x00010A35
		internal MapiExceptionUnableToAbort(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnableToAbort", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00012849 File Offset: 0x00010A49
		private MapiExceptionUnableToAbort(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
