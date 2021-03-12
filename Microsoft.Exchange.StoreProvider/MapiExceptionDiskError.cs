using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000A7 RID: 167
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionDiskError : MapiRetryableException
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x00012871 File Offset: 0x00010A71
		internal MapiExceptionDiskError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionDiskError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00012885 File Offset: 0x00010A85
		private MapiExceptionDiskError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
