using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000D7 RID: 215
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMailboxQuarantined : MapiRetryableException
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x00012E22 File Offset: 0x00011022
		internal MapiExceptionMailboxQuarantined(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMailboxQuarantined", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00012E36 File Offset: 0x00011036
		private MapiExceptionMailboxQuarantined(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
