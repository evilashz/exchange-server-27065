using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000D9 RID: 217
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionDismountInProgress : MapiRetryableException
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x00012E5E File Offset: 0x0001105E
		internal MapiExceptionDismountInProgress(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionDismountInProgress", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00012E72 File Offset: 0x00011072
		private MapiExceptionDismountInProgress(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
