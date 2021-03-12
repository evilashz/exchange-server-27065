using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000DD RID: 221
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMaxMultiMailboxSearchExceeded : MapiRetryableException
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x00012ED6 File Offset: 0x000110D6
		internal MapiExceptionMaxMultiMailboxSearchExceeded(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMaxMultiMailboxSearchExceeded", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00012EEA File Offset: 0x000110EA
		private MapiExceptionMaxMultiMailboxSearchExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
