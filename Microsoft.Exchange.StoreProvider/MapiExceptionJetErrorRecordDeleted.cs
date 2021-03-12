using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000C3 RID: 195
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorRecordDeleted : MapiRetryableException
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x00012BB9 File Offset: 0x00010DB9
		internal MapiExceptionJetErrorRecordDeleted(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorRecordDeleted", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00012BCD File Offset: 0x00010DCD
		private MapiExceptionJetErrorRecordDeleted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
