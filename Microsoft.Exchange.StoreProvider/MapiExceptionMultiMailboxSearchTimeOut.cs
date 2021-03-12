using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000DE RID: 222
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMultiMailboxSearchTimeOut : MapiRetryableException
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x00012EF4 File Offset: 0x000110F4
		internal MapiExceptionMultiMailboxSearchTimeOut(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMultiMailboxSearchTimeOut", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00012F08 File Offset: 0x00011108
		private MapiExceptionMultiMailboxSearchTimeOut(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
